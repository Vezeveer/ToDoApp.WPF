using System;
using System.IO;

namespace To_do_list_system
{
    public class Recovery
    {
        public void RecoveryMode(string username, string the_question)
        {
            string recovery_answer_path = "./account_list/"+username+"/recovery_answer.txt";
            string[] fetched_answer = File.ReadAllLines(recovery_answer_path);

            bool answering = true;

            while (answering)
            {
                Console.WriteLine("This is your recovery question:\n");
                Console.WriteLine(the_question);

                Console.Write("Answer: ");
                string answer = Console.ReadLine();

                if (answer == fetched_answer[0])
                {
                    Console.WriteLine("We are resetting your password...\n");
                    Console.ReadLine();

                    Console.Write("Enter new password: ");
                    string new_password = Console.ReadLine();

                    bool verifying = true;
                    while (verifying)
                    {
                        Console.Write("Re-enter password: ");
                        string re_password = Console.ReadLine();

                        if (re_password == new_password)
                        {
                            Register register = new Register();
                            register.savePassword(username, new_password);
                            register.successEntry();
                            verifying = false;
                            answering = false;
                        }
                        else
                        {
                            Console.WriteLine("\nPassword don't match!");
                            Console.ReadLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nSorry, you entered a wrong answer.\nPlease try again...");
                    Console.ReadLine();
                    Console.Clear();
                }
            }
        }

        public bool CheckRecoveryInfo(string username)
        {
            var file_handler = new MyFileHandler(username);
            string username_path = "./account_list/"+username;
            if (file_handler.CheckDirectory(username_path))
            {
                string recovery_question_path = "./account_list/"+username+"/recovery_question.txt";
                string fetched_recovery_question = file_handler.ReadFile(recovery_question_path);

                RecoveryMode(username, fetched_recovery_question);

                return false;
            }
            else
            {
                Console.WriteLine("Username is unrecognize...\nPlease try again...");
                Console.ReadLine();
                return true;
            }
        }

        public void RunRecoveryForm()
        {
            bool loop = true;
            while (loop)
            {
                string username = ValidateChoice.GetUserName("RECOVERY",
                    new string[] { "",
                        "Type your username.",
                        "Type 'exit' to exit."
                    });

                Console.Write(">  ");
                if (username == "exit")
                {
                    Environment.Exit(0);
                }

                if(username == "")
                {
                    Console.WriteLine("Invalid Input...");
                    Console.ReadKey();
                }
                else
                {
                    loop = CheckRecoveryInfo(username);
                }
            }
        }
    }
}