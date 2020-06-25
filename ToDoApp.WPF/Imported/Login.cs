using System;
using System.IO;

namespace To_do_list_system
{
    class Login
    {
        public string runLoginForm()
        {
            bool logging_in = true;
            string username = "";
            string title = "LOGIN";
            string[] options = {
                 "Login",
                 "Register",
                 "Recover",
                 "Exit"
            };
            while (logging_in)
            {
                Console.Clear();

                int choice = ValidateChoice.GetNumber(4, title, options);

                switch (choice)
                {
                    case 1:
                        username = ValidateChoice.GetUserName(title, new string[] { "Please enter your username." });
                        string password;
                        Console.Write("Passowrd> ");
                        password = Console.ReadLine();
                        logging_in = checkUser(password, username);
                        break;
                    case 2:
                        Register register = new Register();
                        register.runRegistrationForm();
                        break;
                    case 3:
                        Recovery recovery = new Recovery();
                        recovery.RunRecoveryForm();
                        break;
                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
            return username;
        }

        public void error_message(string field)
        {
            Console.WriteLine("\nUnrecognized "+field+"!\nPlease try again...");
            Console.ReadLine();
            Console.Clear();
        }
        
        public bool checkUser(string _password, string _username)
        {
            MyFileHandler file_handler = new MyFileHandler(_username);
            string username_path = "./account_list/"+_username;
            if (file_handler.CheckDirectory(username_path))
            {   
                string password_path = "./account_list/"+_username+"/key.txt";
                string[] fetched_password = File.ReadAllLines(password_path);

                if (_password == fetched_password[0])
                {
                    Console.Clear();

                    //Console.WriteLine("Log in Successfully! ");
                    //Console.ReadLine();
                    //Console.Clear();

                    file_handler.WriteToFile("./account_list./active_user.txt", _username);
                    return false;
                }
                else
                {
                    error_message("password");
                    return true;
                }
            }
            else
            {
                error_message("username");
                return true;
            }
        }

        public void exit(string username)
        {
            if (username.ToUpper() == "EXIT")
            {
                Console.Clear();
                Console.WriteLine("System terminating...");
                Console.ReadLine();

                Environment.Exit(0);
            }
        }
    }
}