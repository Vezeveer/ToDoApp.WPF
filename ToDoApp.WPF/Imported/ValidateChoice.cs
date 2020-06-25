using System;

namespace To_do_list_system
{
    class ValidateChoice
    {
        static public int GetNumber(int choiceRange, string title, string[] options)
        {
            int number;
            bool badchoice = false;
            while (true)
            {
                MenuPrinter.PrintMenu(title, options, null);
                if(badchoice)
                {
                    Console.Write("> Invalid input.\n");
                }
                try
                {
                    Console.Write("> ");
                    number = Convert.ToInt32(Console.ReadLine());
                    if(number > choiceRange)
                    {
                        badchoice = true;
                        continue;
                    }
                    if(number < 1)
                    {
                        badchoice = true;
                        continue;
                    }
                    return number;
                }
                catch
                {
                    badchoice = true;
                }
            }
        }

        static public string GetUserName(string title,string[] options)
        {
            bool badchoice = false;
            while (true)
            {
                MenuPrinter.PrintMenu(title, null, options);
                if (badchoice)
                {
                    Console.Write("> Invalid input.\n> ");
                }

                Console.Write("Username> ");
                string username = Console.ReadLine();
                if (username.Contains(" "))
                {

                }
                else
                {
                    return username;
                }
                badchoice = true;
            }

        }
    }
}
