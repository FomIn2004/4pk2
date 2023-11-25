using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Win32;
using Microsoft.Win32;
using System;
using System.IO;

namespace WpfApp2
{
    
    public partial class MainWindow : Window
    {
        // Создание коллекции для хранения разделов
        private ObservableCollection<string> sections;
        public MainWindow()
        {
            InitializeComponent();
            sections = new ObservableCollection<string>();
            SectionsList.ItemsSource = sections;
        }
        private void AddSectionButton_Click(object sender, RoutedEventArgs e)
        {
            string newSectionName = SectionNameTextBox.Text.Trim();

            if (!string.IsNullOrEmpty(newSectionName))
            {
                sections.Add(newSectionName);
                SectionNameTextBox.Clear(); 
            }
            else
            {
                MessageBox.Show("Введите название раздела.");
            }
        }
        private void ChangeFontFamily(string fontFamily)
        {
            if (NoteContent.Selection.IsEmpty)
                NoteContent.FontFamily = new System.Windows.Media.FontFamily(fontFamily);
            else
                NoteContent.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new System.Windows.Media.FontFamily(fontFamily));
        }

        private void ChangeFontSize(double fontSize)
        {
            if (NoteContent.Selection.IsEmpty)
                NoteContent.FontSize = fontSize;
            else
                NoteContent.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
        }

        private void FontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontComboBox.SelectedItem != null)
            {
                string selectedFont = (FontComboBox.SelectedItem as ComboBoxItem).Content.ToString();
                ChangeFontFamily(selectedFont);
            }
        }

        private void SizeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SizeComboBox.SelectedItem != null)
            {
                double selectedSize = double.Parse((SizeComboBox.SelectedItem as ComboBoxItem).Content.ToString());
                ChangeFontSize(selectedSize);
            }
        }
        private void SaveToFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр для выбора файлов

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = saveFileDialog.FileName;

                    TextRange textRange = new TextRange(NoteContent.Document.ContentStart, NoteContent.Document.ContentEnd);
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        textRange.Save(fileStream, DataFormats.Text);
                    }

                    MessageBox.Show("Файл успешно сохранен!", "Сохранение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка сохранения файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"; // Фильтр для выбора файлов

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string filePath = openFileDialog.FileName;

                    if (File.Exists(filePath))
                    {
                        string fileContent = File.ReadAllText(filePath);
                        NoteContent.Document.Blocks.Clear();
                        NoteContent.AppendText(fileContent); // NoteContent - ваш RichTextBox или другой элемент для отображения содержимого файла

                        MessageBox.Show("Файл успешно открыт!", "Открытие файла", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Выбранный файл не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка открытия файла: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void ChangeFontButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedFont = "Arial"; 
            ChangeFontFamily(selectedFont);
        }

        private void ChangeSizeButton_Click(object sender, RoutedEventArgs e)
        {
            double selectedSize = 14; 
            ChangeFontSize(selectedSize);
        }
        private void ApplyFormattingToSelection(TextDecorationCollection textDecorations, FontStyle fontStyle, FontWeight fontWeight)
        {
            if (!NoteContent.Selection.IsEmpty)
            {
                TextRange selection = new TextRange(NoteContent.Selection.Start, NoteContent.Selection.End);

                selection.ApplyPropertyValue(TextElement.FontStyleProperty, fontStyle);
                selection.ApplyPropertyValue(TextElement.FontWeightProperty, fontWeight);
                selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormattingToSelection(null, FontStyles.Normal, FontWeights.Bold);
        }

        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormattingToSelection(null, FontStyles.Italic, FontWeights.Normal);
        }

        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFormattingToSelection(TextDecorations.Underline, FontStyles.Normal, FontWeights.Normal);
        }


    }
}
