
// Programmers: Cyril Alonzo & Emmanuel Valdueza
// SECTION: BSCS-1A
// Program Info: A simple to do list application with multi user functionality

namespace To_do_list_system
{
    public class Program
    {
        static void Main(string[] args)
        {
            Login login = new Login();
            string username = login.runLoginForm();

             UserMenu menu = new UserMenu();
             menu.runMainMenu(username);
        }
    }
}
