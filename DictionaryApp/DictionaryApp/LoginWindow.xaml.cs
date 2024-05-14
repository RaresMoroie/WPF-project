using System.IO;
using System.Windows;

namespace DictionaryApp
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            if (CheckCredentials(username, password))
            {
                DialogResult = true;
            }
            else
            {
                txtMessage.Text = "Incorrect username or password.";
            }
        }

        private bool CheckCredentials(string username, string password)
        {
            if (!File.Exists("admin.txt"))
                return false;

            string[] lines = File.ReadAllLines("admin.txt");
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 2 && parts[0].Trim() == username && parts[1].Trim() == password)
                    return true;
            }
            return false;
        }
    }
}
