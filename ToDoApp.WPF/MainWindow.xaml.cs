using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using To_do_list_system;

namespace ToDoApp.WPF
{
    public partial class MainWindow : Window
    {
        List<UserList> lists = new List<UserList>();
        public string Username { get; set; }
        ListOption listOptions;
        UserList userListObject;
        ListItem itemListObject;
        MyFileHandler fileHandler;

        public MainWindow(string username) // Constructor
        {
            InitializeComponent();

            fileHandler = new MyFileHandler(username);
            listOptions = new ListOption(username);
            Username = username;
            userTitleName.Text = username;
            itemGrid.Visibility = Visibility.Hidden;
            gridAddToList.Visibility = Visibility.Hidden;
        }

        // Required to make full use of datagrid
        class UserList
        {
            public string listName { get; set; }
        }

        class ListItem
        {
            public string item { get; set; }
        }

        // Shows all items on clicked list
        void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            
            PopulateItems();
        }

        void PopulateItems()
        {
            itemGrid.Visibility = Visibility.Visible;
            gridAddToList.Visibility = Visibility.Visible;

            List<ListItem> listItems = new List<ListItem>();

            userListObject = dataGrid.SelectedItem as UserList; // Get all data from selected list
            if (userListObject != null)
            {
                string[] strListArr = listOptions.ShowItems2(Username, userListObject.listName);
                foreach (var itemx in strListArr)
                {
                    listItems.Add(new ListItem { item = itemx });
                }

                itemGrid.ItemsSource = listItems;
            }
        }

        // Add a list
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            string listName = txtListNameAdd.Text;
            
            if(txtListNameAdd.Text != "")
            {
                listOptions.CreateList(listName);
                txtSuccess.Text = "Success";
                txtListNameAdd.Text = "";
                dataGrid.ItemsSource = null;
                PopulateList();
            }
            else
            {
                txtSuccess.Text = "Cannot be empty";
            }
        }

        // Update list on tab change
        private void UpdateCollections(Object sender, SelectionChangedEventArgs args)
        {
            PopulateList();
        }

        private void PopulateList()
        {
            lists.Clear();
            string[] strLists = listOptions.ShowList2(Username);
            if(strLists.Length == 0)
            {
                ProvideEmptyList();
            }
            else
            {
                dataGrid.IsHitTestVisible = true;
                lists.Clear();
                foreach (var list in strLists)
                {
                    
                    lists.Add(new UserList { listName = list });
                }
                dataGrid.ItemsSource = lists;
            }
        }

        private void ProvideEmptyList()
        {
            lists.Clear();
            if(lists.Count == 0)
            {
                lists.Add(new UserList { listName = "-There are no lists available-" });
                dataGrid.IsHitTestVisible = false;
            }
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            if(txtItemAdd.Text != "")
            {
                fileHandler.AppendToFile($@"./account_list/{Username}/lists/{userListObject.listName}.txt", txtItemAdd.Text);
                PopulateItems();
                txtItemAdd.Text = "";
            }
            else
            {
                MessageBox.Show("Cannot be empty");
            }
            
        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            itemListObject = itemGrid.SelectedItem as ListItem; // Get all data from selected list
            Delete(itemListObject.item);
            PopulateItems();
            txtItemAdd.Text = "";
        }

        public void Delete(string itemToDelete)
        {
            string path = $@"./account_list/{Username}/lists/{userListObject.listName}.txt";
            List<string> xLists = new List<string>();
            string[] toDoList = File.ReadAllLines($@"{path}");

            for (int i = 0; i < toDoList.Length; i++)
            {
                if (itemToDelete == toDoList[i])
                {
                    continue;
                }
                xLists.Add(toDoList[i]);
            }

            try
            {
                File.Delete($@"{path}");
                File.WriteAllLines($@"{path}", xLists);
            }
            catch
            {
            }
        }

        void DeleteList(object sender, RoutedEventArgs e)
        {
            string path = $@"./account_list/{Username}/lists/{userListObject.listName}.txt";
            try
            {
                File.Delete($@"{path}");
                dataGrid.ItemsSource = null;
                itemGrid.ItemsSource = null;
                PopulateList();
            }
            catch
            {
                MessageBox.Show("Oops... Something went wrong");
            }
        }
    }
}
