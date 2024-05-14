using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DictionaryApp
{
    public partial class MiniGameWindow : Window
    {
        private List<(string Description, string Word)> wordDescriptions;

        public MiniGameWindow(List<(string Description, string Word)> descriptions)
        {
            InitializeComponent();
            wordDescriptions = descriptions;
            SetDescriptions();
        }

        private void SetDescriptions()
        {
            Description1.Text = wordDescriptions[0].Description;
            Description2.Text = wordDescriptions[1].Description;
            Description3.Text = wordDescriptions[2].Description;
            Description4.Text = wordDescriptions[3].Description;
            Description5.Text = wordDescriptions[4].Description;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string guess1 = guessTextBox1.Text.Trim();
            string guess2 = guessTextBox2.Text.Trim();
            string guess3 = guessTextBox3.Text.Trim();
            string guess4 = guessTextBox4.Text.Trim();
            string guess5 = guessTextBox5.Text.Trim();

            string correctWords = string.Join(", ", wordDescriptions.Select(desc => desc.Word));

            string feedback = $"Correct words: {correctWords}\n\n";

            int correctGuesses = 0;

            if (guess1.Equals(wordDescriptions[0].Word, StringComparison.OrdinalIgnoreCase))
                correctGuesses++;
            if (guess2.Equals(wordDescriptions[1].Word, StringComparison.OrdinalIgnoreCase))
                correctGuesses++;
            if (guess3.Equals(wordDescriptions[2].Word, StringComparison.OrdinalIgnoreCase))
                correctGuesses++;
            if (guess4.Equals(wordDescriptions[3].Word, StringComparison.OrdinalIgnoreCase))
                correctGuesses++;
            if (guess5.Equals(wordDescriptions[4].Word, StringComparison.OrdinalIgnoreCase))
                correctGuesses++;

            feedback += $"You guessed {correctGuesses} out of 5 words correctly.";

            MessageBox.Show(feedback, "Mini Game Results", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
