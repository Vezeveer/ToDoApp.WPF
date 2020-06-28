using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using To_do_list_system;

namespace ToDoApp.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<UserList> lists = new List<UserList>();
        public string Username { get; set; }

        public MainWindow(string username)
        {
            ListOption listOption = new ListOption(username);

            string[] strLists = listOption.ShowList2(username);
            foreach (var list in strLists)
            {
                lists.Add(new UserList { listName = list });
            }

            Username = username;
            InitializeComponent();

            itemGrid.Visibility = Visibility.Hidden;

            dataGrid.ItemsSource = lists;
        }

        // Allows us to make full use of datagrid
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
            itemGrid.Visibility = Visibility.Visible;

            ListOption listOption = new ListOption(Username);
            List<ListItem> listItems = new List<ListItem>();

            UserList ListObject = dataGrid.SelectedItem as UserList;
            string[] strListArr = listOption.ShowItems2(Username, ListObject.listName);
            foreach(var itemx in strListArr)
            {
                listItems.Add(new ListItem { item = itemx });
            }

            itemGrid.ItemsSource = listItems;
            //items.Text = string.Join("\n", strListArr);

        }

    }
}
