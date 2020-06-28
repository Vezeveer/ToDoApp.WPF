using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using To_do_list_system;

namespace ToDoApp.WPF
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void btnNewUser_Click(object sender, RoutedEventArgs e)
        {
            Register register = new Register();
            bool userDirectoryExists = register.checkDirectory(txtUsername.Text);

            if(txtUsername.Text == "" || txtPassword.Password == "")
            {
                Background = new SolidColorBrush(Colors.Red);
                MessageBox.Show("Please provide a username and password");
                Background = new SolidColorBrush(Colors.DarkSlateBlue);
            }
            else
            {
                if (userDirectoryExists)
                {
                    register.makeDirectory(txtUsername.Text);
                    register.makeListDirectory(txtUsername.Text);
                    register.savePassword(txtUsername.Text, txtPassword.Password);
                    MessageBox.Show("Success");
                    LoginWindow login = new LoginWindow();
                    login.Show();
                    Close();
                }
                else
                {
                    Background = new SolidColorBrush(Colors.Red);
                    MessageBox.Show("Username already taken!");
                    Background = new SolidColorBrush(Colors.DarkSlateBlue);
                }
            }
        }
    }
}
