using System;

namespace To_do_list_system
{
    class MenuPrinter
    {
        static public void PrintMenu(string title, string[] options, string[] info)
        {
            Console.Clear();
            Console.WriteLine("|| " + title);
            Console.Write("|| ");
            foreach(var letter in title)
            {
                Console.Write("-");
            }
            Console.Write("\n");
            if(options != null)
            {
                int x = 1;
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine($"|| <{x}> " + options[i]);
                    x++;
                }
            }
            else
            {
                //Console.WriteLine("||\n");
            }
            if (info != null)
            {
                for (int i = 0; i < info.Length; i++)
                {
                    Console.WriteLine($"|| {info[i]}");
                }
            }
            Console.WriteLine("||\n");
        }
    }
}
