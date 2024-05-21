using System;
using System.Windows;

namespace BruteForcePasswordReset
{
    public partial class MainWindow : Window
    {
        private PasswordManager passwordManager;
        private BruteForceCracker bruteForceCracker;

        public MainWindow()
        {
            InitializeComponent();
            passwordManager = new PasswordManager();
            bruteForceCracker = new BruteForceCracker();
        }

        private void CreatePassword_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordTextBox.Text;
            if (!string.IsNullOrEmpty(password))
            {
                passwordManager.CreatePassword(password);
                MessageBox.Show("Password created and encrypted successfully.");
            }
            else
            {
                MessageBox.Show("Please enter a password.");
            }
        }

        private void StartBruteForce_Click(object sender, RoutedEventArgs e)
        {
            int maxThreads = 1;
            if (int.TryParse(ThreadsTextBox.Text, out int result))
            {
                maxThreads = result;
            }
            bruteForceCracker.StartBruteForce(maxThreads, passwordManager.EncryptedPassword);
            ProgressTextBlock.Text = bruteForceCracker.Results;
        }
    }
}
