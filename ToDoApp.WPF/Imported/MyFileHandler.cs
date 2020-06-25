using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace To_do_list_system
{
    public class MyFileHandler
    {
        public string username { get; set; }
        public MyFileHandler(string username) // constructor
        {
            this.username = username;
        }

        public string removeSpecialCharacters(string path)
        {
            int index = 0;
            foreach (var character in path)
            {
                if (Char.IsLetter(character))
                {
                    index = 0;
                }
                else
                {
                    index++;
                    break;
                }
            }

            return path.Substring(index);
        }

        public string[] GetAvailableList(string listPath)
        {
           if (Directory.Exists(listPath))
           {
                string [] fileEntries = Directory.GetFiles(listPath);
                string[] listNames = new string[fileEntries.Length];
                int i = 0;
                foreach (var file in fileEntries)
                {
                    string path_substring = file.Substring(26);
                    path_substring = path_substring.Substring(0, path_substring.Length - 4);
                    path_substring = removeSpecialCharacters(path_substring);
                    listNames[i] = path_substring;
                    i++;
                }
                return listNames;
            }
            return null;
        }

        public string ReadFile(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            byte[] to_read = new byte[file.Length];
            int bytes_read = file.Read(to_read, 0, to_read.Length);
            string fetched_data = Encoding.ASCII.GetString(to_read, 0, bytes_read);
            file.Flush();
            file.Close();
            return fetched_data;
        }

        public void WriteToFile(string fileNameAndPath, string data)
        {
            TextWriter file = new StreamWriter(fileNameAndPath);
            file.WriteLine(data);

            file.Flush();
            file.Close();
        }

        //public void updateFile(string path, string data)
        //{
        //    try
        //    {
        //        StreamWriter write = new StreamWriter(path);
        //        write.WriteLine(data);
        //        write.Close();
        //    }
        //    catch 
        //    {
        //        Console.WriteLine("Oops... something went wrong.");
        //        Console.ReadKey();
        //    }
        //}

        public void AppendToFile(string path, string data)
        {
            string[] toDoList = File.ReadAllLines(path);
            List<string> xList = new List<string>();
            foreach(var list in toDoList)
            {
                xList.Add(list);
            }
            xList.Add(data);
            string[] newToDoList = new string[xList.Count];
            for(int i = 0; i < xList.Count; i++)
            {
                newToDoList[i] = xList[i];
            }

            try
            {
                File.WriteAllLines(path, newToDoList);
            }
            catch 
            {
                Console.WriteLine("Oops... something went wrong.");
                Console.ReadKey();
            }
        }

        public bool CheckDirectory (string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception)
            {
                Console.WriteLine("\nUsername does not exist...\n");
                return false;
            }
        }

        public bool ifCanWrite(string path)
        {
            bool to_return;
            try
            {
                FileStream file = new FileStream(path, FileMode.Open, FileAccess.Write);
                if (file.CanWrite)
                {
                    to_return = true;
                }
                else
                {
                    to_return = false;
                }

                file.Flush();
                file.Close();
                return to_return;
            }
            catch
            {
                //Console.WriteLine("Writing file error!\nFile not found!...\n\n");
                //Console.ReadLine();
                return false;
            }
        }
        
        public void DelteFile(string path, string username, string list, string[] lists)
        {
           try
           {
                File.Delete($@"{path}/{list}.txt");
                Console.WriteLine("> Deleted");
                Console.ReadKey();
                Console.Clear();
            }
           catch
           {
                Console.WriteLine("File does not exits, ..");
                Console.ReadKey();
                Console.Clear();
           }
        }

        public void DeleteItem(string path, string username, string list, string[] lists)
        {
            List<string> xLists = new List<string>();
            string[] toDoList = File.ReadAllLines($@"{path}/{list}.txt");

                int item2del = ValidateChoice.GetNumber(toDoList.Length, "Delete item from list", toDoList);
                Console.Write("> ");
                for(int i = 0; i < toDoList.Length; i++)
                {
                    if (item2del == i+1)
                    {
                        continue;
                    }
                    xLists.Add(toDoList[i]);
                }

            try
            {
                File.Delete($@"{path}/{list}.txt");
                File.WriteAllLines($@"{path}/{list}.txt", xLists);
                Console.WriteLine("deleted...");
                Console.ReadKey();
                Console.Clear();
            }
            catch
            {
                Console.WriteLine("> List file is missing...");
                Console.ReadKey();
            }

            //// YOLO

            //for (int i = 0; i < lists.Length; i++) // keep all lines except chosen one
            //{
            //    if (list == lists[i])
            //    {
            //        continue;
            //    }
            //    xLists.Add(lists[i]);
            //}
        }

        //public void deleteFileEntry(string path)
        //{
        //    try
        //    {
        //        string fetched_data;
        //        int index_number = 0;
        //        List<string> list = new List<string>(); //the size of list is dynamic.
        //        if (ifCanRead(path))
        //        {
        //            StreamReader list_file = new StreamReader(path);

        //            while ((fetched_data = list_file.ReadLine()) != null)
        //            {
        //                Console.WriteLine(fetched_data +" < "+index_number+" >");
        //                index_number++;
        //                list.Add(fetched_data);
        //            }
        //            list_file.Close();

        //            int input;
        //            Console.WriteLine("\nPlease select an index number to delete your desired entry.\n");
        //            bool deleting = true;
        //            while (deleting)
        //            {
        //                Console.Write("\nEnter index: ");  
        //                input = int.Parse(Console.ReadLine());

        //                if (input >= 0 && input < list.Count)
        //                {
        //                    Console.Write("\nYou are about to delete '"+list[input]+"' from the list...\nContinue? Y/N: ");
        //                    string confirmation = Console.ReadLine();

        //                    if (confirmation.ToUpper() == "Y")
        //                    {
        //                        list.Remove(list[input]);
        //                        foreach (var item in list)
        //                        {
        //                           updateFile(path, item);
        //                        }

        //                        Console.WriteLine("Status:  Entry successfully Deleted!\n\n");
        //                        deleting = false;
        //                    }
        //                    else if (confirmation.ToUpper() == "N")
        //                    {
        //                        Console.WriteLine("\nStatus:    Cancelling request...");
        //                        Console.ReadLine();
        //                        Console.WriteLine("\nStatus:    Cancelled...");
        //                        Console.ReadLine();
        //                        Console.Clear();
        //                        deleting = false;
        //                    }
        //                    else
        //                    {
        //                        Console.WriteLine("\nSorry, invalid input.\nPlease try again...");
        //                    }
        //                }
        //                else
        //                {
        //                    Console.WriteLine("\nSorry, invalid input.\nPlease try again...");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            Console.WriteLine("\nSorry, there is nothing to delete...\n");
        //            Console.WriteLine("Status:  Unsuccessfull!\n\n");
        //        }
        //    }
        //    catch (System.Exception x)
        //    {
        //        Console.WriteLine("Deleting error!\nExiting...\n\n");
        //        Console.ReadLine();
        //        Console.Clear();
        //    }
        //}
    }
}