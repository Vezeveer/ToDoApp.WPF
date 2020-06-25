using System;
using System.Collections.Generic;
using System.IO;

namespace To_do_list_system
{
    class ListOption
    {
        public string listDirectory { get; set; }
        public string activeUser { get; set; }
        public MyFileHandler file_handler { get; set; }
        public string[] lists { get; set; }

        public ListOption(string username) // constructor
        {
            listDirectory = "./account_list/" + username + "/lists";
            activeUser = username;
            file_handler = new MyFileHandler(username);
            lists = file_handler.GetAvailableList(listDirectory);
        }

        public FileInfo[] GetList(string username)
        {
            DirectoryInfo d = new DirectoryInfo($@"./accounts/{username}");
            FileInfo[] Files = d.GetFiles("*.txt");
            string str = "";
            foreach (FileInfo file in Files)
            {
                str = str + ", " + file.Name;
                Console.WriteLine(str);
            }
            return Files;
        }

        public string[] ShowList(string username) // for current user show list, or get list
        {
            var file = new MyFileHandler(username);
            string listDirectory = $@"./account_list/{username}/lists";
            string[] lists = file.GetAvailableList(listDirectory);

            if (lists.Length != 0) //check if any list exists
            {
                while(true)
                {
                    int listNumber = ValidateChoice.GetNumber(lists.Length, "Lists created by " + username, lists);

                    if(listNumber == 0) //exit
                    {
                        break;
                    }

                    try
                    {
                        string[] toDoList = File.ReadAllLines($@"{listDirectory}/{lists[listNumber-1]}.txt");
                        if (toDoList.Length == 0)
                        {
                            MenuPrinter.PrintMenu(lists[listNumber-1] + " List", null ,new string[] { "List is empty..." });
                            break;
                        }
                        else
                        {
                            MenuPrinter.PrintMenu(lists[listNumber-1] + " List", toDoList, null);
                            return toDoList;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("> The list you gave us does not exist!");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                }
            }
            else
            {
                Console.WriteLine("> No List Exists...");
            }
            return null;
        }

        public void inputIntercepter(string purpose,string list_path)
        {
            string data;
            bool editing = true;
            while (editing)
            {
                if (purpose.ToUpper() == "DELETE")
                {                     
                    Console.ReadLine();
                    Console.Clear();
                    editing = false;
                }

                Console.WriteLine("\nWhat do you want to add to the list? \n");
                Console.Write("> ");
                data = Console.ReadLine();

                if (purpose.ToUpper() == "ADD")
                {
                   
                    file_handler.AppendToFile(list_path, data);

                    Console.WriteLine("> Success.\n");
                    Console.ReadLine();
                    Console.Clear();
                    editing = false;
                }
            }
        }

        public void AddDelUpdate(string username, string addDeleteUpdate)
        {
            bool loop = true;
            string[] pathLists = Directory.GetFiles("./account_list/" + username + "/lists");
            List<string> xList = new List<string>();
            string[] currentLists = file_handler.GetAvailableList(listDirectory);

            if (currentLists.Length == 0) //check if any list exists
            {
                Console.WriteLine("> No list exists...");
                Console.WriteLine("> Press any key to continue...");

                Console.ReadKey();
            }
            else
            {
                foreach (var path in pathLists)
                {
                    xList.Add(Path.GetFileName(path));
                }

                string[] lists = new string[xList.Count];
                int x = 0;
                foreach (var y in xList)
                {
                    lists[x] = xList[x].Substring(0, xList[x].Length - 4);
                    x++;
                }

                string[] textMorph = { "Add item to list", "Delete List", "Delete Item from List" };
                string textShown = "Error";
                if (addDeleteUpdate == "add")
                    textShown = textMorph[0];
                if (addDeleteUpdate == "delete")
                    textShown = textMorph[1];
                if (addDeleteUpdate == "update")
                    textShown = textMorph[2];

                while (loop)
                {
                    int item = ValidateChoice.GetNumber(lists.Length, textShown, lists);

                    for (int i = 0; i < lists.Length; i++)
                    {
                        if (item == i + 1)
                        {
                            if (addDeleteUpdate == "add")
                            {
                                inputIntercepter("add", $@"{listDirectory}/{lists[i]}.txt");
                            }

                            if (addDeleteUpdate == "delete")
                            {
                                file_handler.DelteFile(listDirectory, username, lists[i], lists);
                            }

                            if (addDeleteUpdate == "update")
                            {
                                file_handler.DeleteItem(listDirectory, username, lists[i], lists);
                            }

                            loop = false;
                            break;
                        }
                    }

                    if (loop)
                    {
                        Console.WriteLine("> It does not exist!");
                        Console.ReadKey();
                    }
                }
            }

                
        }

        public bool ifDuplicating(string path)
        {
            if (File.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void inputCheckerCreateDeleteList(string purpose, string username, string list, string[] lists)
        //{

        //            if (purpose.ToUpper() == "DELETE")
        //            {
        //                file_handler.DelteFile(listDirectory, username, list, lists);
        //            }

        //            if (purpose.ToUpper() == "CREATE")
        //            {
        //                bool existing = ifDuplicating(listDirectory);
        //                if (!existing)
        //                {   
        //                    file_handler.WriteToFile(listDirectory, "");
        //                    Console.WriteLine("\n> success.");
        //                }
        //                else
        //                {
        //                    Console.WriteLine("\nStatus: A title list is already taken...");
        //                    Console.ReadLine();
        //                    Console.WriteLine("\nGoing back to the main menu...");
        //                }    
        //            }
        //            Console.Clear();
        //}

        public void CreateList()
        {
            MenuPrinter.PrintMenu("Create List", null, new string[] { "What is the name of the list?" });
            Console.Write("> ");
            string list = Console.ReadLine();

            File.Create($@"{listDirectory}/{list}.txt").Dispose();

            Console.WriteLine("\n> Done...");
            Console.ReadKey();
        }

        //public void DeleteList(string username)
        //{
        //    bool loop = true;
        //    string[] pathLists = Directory.GetFiles("./account_list/" + username + "/lists");
        //    List<string> xList = new List<string>();

        //    foreach(var path in pathLists)
        //    {
        //        xList.Add(Path.GetFileName(path));
        //    }

        //    string[] lists = new string[xList.Count];
        //    int x = 0;
        //    foreach(var y in xList)
        //    {
        //        lists[x] = xList[x].Substring(0, xList[x].Length - 4);
        //        x++;
        //    }

        //    while (loop)
        //    {
        //        int item = ValidateChoice.GetNumber(lists.Length, "Delete List", lists);

        //        for(int i = 0; i < lists.Length; i++)
        //        {
        //            if(item == i+1)
        //            {
        //                file_handler.DelteFile(listDirectory, username, lists[i], lists);
        //                loop = false;
        //                break;
        //            }
        //        }

        //        if (loop)
        //        {
        //            Console.WriteLine("> Item does not exist!");
        //            Console.ReadKey();
        //        }
        //    }
        //}
        
        //public void DeleteItemFromList(string username)
        //{
        //    bool loop = true;
        //    string[] pathLists = Directory.GetFiles("./account_list/" + username + "/lists");
        //    List<string> xList = new List<string>();

        //    foreach (var path in pathLists)
        //    {
        //        xList.Add(Path.GetFileName(path));
        //    }

        //    string[] lists = new string[xList.Count];
        //    int x = 0;
        //    foreach (var y in xList)
        //    {
        //        lists[x] = xList[x].Substring(0, xList[x].Length - 4);
        //        x++;
        //    }

        //    while (loop)
        //    {
        //        int item = ValidateChoice.GetNumber(lists.Length, "Delete Item from List", lists);

        //        for (int i = 0; i < lists.Length; i++)
        //        {
        //            if (item == i + 1)
        //            {
        //                file_handler.DeleteItem(listDirectory, username, lists[i], lists);
        //                loop = false;
        //                break;
        //            }
        //        }

        //        if (loop)
        //        {
        //            Console.WriteLine("> Item does not exist!");
        //            Console.ReadKey();
        //        }
        //    }

        //}
    }
}