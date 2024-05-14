using System;
using System.Windows;
using System.Windows.Media.Imaging;

namespace DictionaryApp
{
    public partial class WordDetailsWindow : Window
    {
        public WordDetailsWindow(string category, string word, string definition, string imagePath)
        {
            InitializeComponent();
            categoryTextBlock.Text = category;
            wordTextBlock.Text = word;
            definitionTextBlock.Text = definition;
            if (!string.IsNullOrEmpty(imagePath))
            {
                wordImage.Source = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
