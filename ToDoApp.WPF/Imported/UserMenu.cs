using System;

namespace To_do_list_system
{
    class UserMenu
    {
        public void runMainMenu(string username)
        {
            ListOption listOptions = new ListOption(username);
            string title = "Welcome " + username + "!";
            string[] options = {
                        "show my entries from list",
                        "create a list",
                        "add an entry to a list",
                        "delete a list",
                        "delete an entry from a list",
                        "exit"
                };

            while (true)
            {
                Console.Clear();

                int switch_on = ValidateChoice.GetNumber(6, title, options);

                switch (switch_on)
                {
                    case 1:
                        listOptions.ShowList(username);
                        Console.WriteLine("> Press any key to continue...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        listOptions.CreateList();
                        break;
                    case 3:
                        listOptions.AddDelUpdate(username, "add");
                        break;
                    case 4:
                        listOptions.AddDelUpdate(username, "delete");
                        break;
                    case 5:
                        listOptions.AddDelUpdate(username, "update");
                        break;
                    case 6:
                        Environment.Exit(0);
                        break;
                    default:
                        break;
                }
            }
        }

    }
}