using System;
using System.IO;

namespace To_do_list_system
{
    public class Register 
    {
        string [] questions =
        {
            "What is your favorite pet name?",
            "Who is your favorite Marvel hero?",
            "What month is your birth day?"
        };

        
        public void successEntry()
        {
            Console.WriteLine("\nData saved Successfully...");
            Console.ReadLine();

        }

        public void makeListDirectory(string username)
        {
            string path = "./account_list/"+username+"/lists";
            Directory.CreateDirectory(path);
        }

        public void makeDirectory(string username)
        {
            string path = "./account_list/"+username;
            Directory.CreateDirectory(path);
        }

        public void savePassword(string username,string password)
        {
            var file_handler = new MyFileHandler(username);
            string path = "./account_list/"+username+"/key.txt";
            file_handler.WriteToFile(path, password);
        }

        public void saveRecovery(string the_question, string username, string recovery_answer)
        {
            var file_handler = new MyFileHandler(username);
            string question_path = "./account_list/"+username+"/recovery_question.txt";
            file_handler.WriteToFile(question_path, the_question);

            string answer_path = "./account_list/"+username+"/recovery_answer.txt";
            file_handler.WriteToFile(answer_path, recovery_answer);
        }

        public bool checkDirectory(string given_name)
        {
            string path = "./account_list/"+given_name;
            if (Directory.Exists(path))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void runRegistrationForm()
        {
            string re_password;
            string password;
            string recovery_answer;

            try
            {
                Login login = new Login();

                string title = "Registration Form";
                string[] options = { 
                    "Enter your new username",
                    "Type 'exit' to exit"
                };

                Console.Write("Username: ");
                string xUsername = ValidateChoice.GetUserName(title, options);

                if(xUsername == "exit")
                {
                    Environment.Exit(0);
                }

                bool not_existing = checkDirectory(xUsername);

                if (not_existing)
                {
                    while (true)
                    {
                        Console.Write("Password: ");
                        password = Console.ReadLine();

                        Console.Write("Re-enter password: ");
                        re_password = Console.ReadLine();
                    

                        if (re_password != password)
                        {
                            Console.WriteLine("\nPassword don't match!");
                            Console.ReadLine();
                        }
                        else
                        {

                            int input = ValidateChoice.GetNumber(3, "Pick a recovery question", questions);

                            Console.Write("Answer to question> ");
                            recovery_answer = Console.ReadLine();

                            makeDirectory(xUsername);
                            makeListDirectory(xUsername);
                            savePassword(xUsername, password);
                            saveRecovery(questions[input-1], xUsername, password);
                            successEntry();
                            break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nUsername already taken...\nExiting...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
            catch
            {
                Console.WriteLine("\nInvalid input.\nTry again...");
            }
        }
    }
}