using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Password_Generator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cmbPasswordType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Показываем соответствующие опции в зависимости от выбранного типа пароля
            stackRandomPassword.Visibility = Visibility.Collapsed;
            stackMemorablePassword.Visibility = Visibility.Collapsed;
            stackPinCode.Visibility = Visibility.Collapsed;

            if (cmbPasswordType.SelectedItem == null)
                return;

            switch (((ComboBoxItem)cmbPasswordType.SelectedItem).Content.ToString())
            {
                case "Random Password":
                    stackRandomPassword.Visibility = Visibility.Visible;
                    break;
                case "Memorable Password":
                    stackMemorablePassword.Visibility = Visibility.Visible;
                    break;
                case "PIN Code":
                    stackPinCode.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btnGenerateRandomPassword_Click(object sender, RoutedEventArgs e)
        {
            int length = (int)sliderLength.Value;
            bool includeNumbers = chkIncludeNumbers.IsChecked == true;
            bool includeSpecialChars = chkIncludeSpecialChars.IsChecked == true;

            txtRandomPassword.Text = GenerateRandomPassword(length, includeNumbers, includeSpecialChars);
        }

        private void btnGenerateMemorablePassword_Click(object sender, RoutedEventArgs e)
        {
            int length = (int)sliderMemorableLength.Value;
            bool capitalizeFirstLetter = chkCapitalizeFirstLetter.IsChecked == true;
            bool fullWords = chkFullWords.IsChecked == true;

            txtMemorablePassword.Text = GenerateMemorablePassword(length, capitalizeFirstLetter, fullWords);
        }

        private void btnGeneratePin_Click(object sender, RoutedEventArgs e)
        {
            int length = (int)sliderPinLength.Value;

            txtPin.Text = GeneratePinCode(length);
        }

        private string GenerateRandomPassword(int length, bool includeNumbers, bool includeSpecialChars)
        {
            string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            if (includeNumbers)
                chars += "0123456789";
            if (includeSpecialChars)
                chars += "!@#$%^&*()_+-=[]{}|;:',./<>?";

            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private string GenerateMemorablePassword(int length, bool capitalizeFirstLetter, bool fullWords)
        {
            string[] words = { "apple", "banana", "cherry", "date", "elderberry", "fig", "grape", "honeydew", "kiwi", "lemon" };

            Random random = new Random();
            string password = "";
            for (int i = 0; i < length; i++)
            {
                string word = fullWords ? words[random.Next(words.Length)] : words[random.Next(words.Length)].Substring(0, random.Next(3) + 1);
                if (capitalizeFirstLetter)
                    word = char.ToUpper(word[0]) + word.Substring(1);
                password += word + "-";
            }
            return password.TrimEnd('-');
        }

        private string GeneratePinCode(int length)
        {
            string pin = "";
            Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                pin += random.Next(10);
            }
            return pin;
        }
    }
}
