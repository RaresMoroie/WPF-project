using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DictionaryApp
{
    public partial class MainWindow : Window
    {
        private List<(string Category, string Word, string Definition, string Image)> dictionaryEntries = new List<(string Category, string Word, string Definition, string Image)>();
        
        public MainWindow()
        {
            InitializeComponent();
            LoadDictionaryFromFile("dictionary.txt");
            resultsListBox.SelectionChanged += ResultsListBox_SelectionChanged;

            PopulateCategoryComboBox();
        }

        private void PopulateCategoryComboBox()
        {
            var categories = dictionaryEntries.Select(entry => entry.Category).Distinct().ToList();
            categories.Insert(0, "All");
            categoryComboBox.ItemsSource = categories;
            categoryComboBox.SelectedIndex = 0;
        }

        private void LoadDictionaryFromFile(string filename)
        {
            dictionaryEntries.Clear();
            string[] lines = File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 3)
                {
                    string word = parts[0].Trim();
                    string category = parts[1].Trim();
                    string definition = parts[2].Trim();
                    string image = "Images/" + parts[0].Trim() + ".jpg";
                    dictionaryEntries.Add((category, word, definition, image));
                }
            }
        }

        private void SearchBox_TextChanged(object sender, RoutedEventArgs e)
        {
            string searchText = searchBox.Text.ToLower();
            string selectedCategory = categoryComboBox.SelectedItem as string;

            var filteredResults = dictionaryEntries;

            if (selectedCategory != "All")
            {
                filteredResults = filteredResults.Where(entry => entry.Category.Equals(selectedCategory, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            var results = filteredResults.Where(entry => entry.Word.ToLower().Contains(searchText)).OrderBy(entry => entry.Word).ToList();
            resultsListBox.ItemsSource = results.Select(entry => $"{entry.Word}");
        }

        private void ResultsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (resultsListBox.SelectedItem != null)
            {
                string selectedItem = resultsListBox.SelectedItem.ToString();
                string[] parts = selectedItem.Split(new char[] { ':' }, 2);
                string selectedWord = parts[0].Trim();
                var result = dictionaryEntries.FirstOrDefault(entry => entry.Word.Equals(selectedWord, StringComparison.OrdinalIgnoreCase));
                if (result != default)
                {
                    string selectedCategory = result.Category;
                    string selectedDefinition = result.Definition;
                    string selectedImage = result.Image;
                    if (!File.Exists(selectedImage))
                    {
                        selectedImage = "Images/default.jpg";
                    }
                    WordDetailsWindow wordDetailsWindow = new WordDetailsWindow(selectedCategory, selectedWord, selectedDefinition, selectedImage);
                    wordDetailsWindow.Show();
                }
            }
        }

        private void MiniGameButton_Click(object sender, RoutedEventArgs e)
        {
            List<(string Description, string Word)> randomDescriptions = GetRandomWordDescriptions(5);
            MiniGameWindow miniGameWindow = new MiniGameWindow(randomDescriptions);
            miniGameWindow.ShowDialog();
        }

        private List<(string Description, string Word)> GetRandomWordDescriptions(int count)
        {
            Random random = new Random();
            var randomEntries = dictionaryEntries.OrderBy(x => random.Next()).Take(count);

            return randomEntries.Select(entry => (entry.Definition, entry.Word)).ToList();
        }

        private void BtnAdminLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                AdminMenuWindow adminMenuWindow = new AdminMenuWindow();
                adminMenuWindow.Show();
/*                if (adminMenuWindow.ShowDialog() == true)
                {
                    LoadDictionaryFromFile("dictionary.txt");
                }*/
            }
            LoadDictionaryFromFile("dictionary.txt");
        }
    }
}

