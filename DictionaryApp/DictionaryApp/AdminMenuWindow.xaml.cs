using System.Collections.Generic;
using System.IO;
using System.Windows;

namespace DictionaryApp
{
    public partial class AdminMenuWindow : Window
    {
        private const string DictionaryFilePath = "dictionary.txt";

        public AdminMenuWindow()
        {
            InitializeComponent();
            LoadWords();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            string selectedWord = (string)lstWords.SelectedItem;
            if (selectedWord != null)
            {
                string[] lines = File.ReadAllLines(DictionaryFilePath);
                using (StreamWriter writer = new StreamWriter(DictionaryFilePath))
                {
                    foreach (string line in lines)
                    {
                        if (!line.StartsWith(selectedWord))
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
                MessageBox.Show("Word deleted successfully.");
                LoadWords();
            }
            else
            {
                MessageBox.Show("Please select a word to delete.");
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string category = txtCategory.Text.Trim();
            string word = txtWord.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (!string.IsNullOrEmpty(category) && !string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(description))
            {
                bool wordExists = false;
                string newEntry = $"{word}:{category}:{description}";
                List<string> modifiedLines = new List<string>();

                if (File.Exists(DictionaryFilePath))
                {
                    string[] lines = File.ReadAllLines(DictionaryFilePath);
                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length >= 2)
                        {
                            if (parts[0].Trim() == word && !wordExists)
                            {
                                modifiedLines.Add(newEntry);
                                wordExists = true;
                            }
                            else
                            {
                                modifiedLines.Add(line);
                            }
                        }
                    }
                    if (!wordExists)
                    {
                        modifiedLines.Add(newEntry);
                    }

                    File.WriteAllLines(DictionaryFilePath, modifiedLines);
                    MessageBox.Show(wordExists ? "Word modified successfully." : "Word added successfully.");
                    LoadWords();
                    ClearInputFields();
                }
                else
                {
                    File.WriteAllText(DictionaryFilePath, newEntry + "\n");
                    MessageBox.Show("Word added successfully.");
                    LoadWords();
                    ClearInputFields();
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields to add a new word.");
            }
/*            DialogResult = true;
            AdminMenuWindow adminMenuWindow = new AdminMenuWindow();
            adminMenuWindow.Show();*/
        }


        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoadWords()
        {
            lstWords.Items.Clear();
            if (File.Exists(DictionaryFilePath))
            {
                string[] lines = File.ReadAllLines(DictionaryFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(':');
                    if (parts.Length >= 2)
                    {
                        lstWords.Items.Add(parts[0].Trim());
                    }
                }
            }
        }

        private void ClearInputFields()
        {
            txtCategory.Text = "";
            txtWord.Text = "";
            txtDescription.Text = "";
        }
    }
}
