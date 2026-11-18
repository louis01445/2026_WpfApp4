using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
 
namespace _2026_WpfApp4
{
    /// <summary>
    /// Window1.xaml 的互動邏輯
    /// </summary>
    public partial class Window1 : Window
    {
        Color fontColor = Colors.Black;

        public Window1()
        {
            InitializeComponent();
            FontColorPicker.SelectedColor = fontColor;
            foreach (FontFamily font in Fonts.SystemFontFamilies)
            {
                FontFamilyCombobox.Items.Add(font);
            }
            FontFamilyCombobox.SelectedIndex = 1;

            // 移除 ItemsSource，改為手動添加字體大小
            foreach (double size in new List<double>()
                { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 })
            {
                FontsizeCombobox.Items.Add(size);
            }
            FontsizeCombobox.SelectedIndex = 4;
        }

        private void SaveCommands_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*",
                DefaultExt = ".rtf",
                AddExtension = true

            };
            if(saveFileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(MainRichTextbox.Document.ContentStart, MainRichTextbox.Document.ContentEnd);
                FileStream filestream = new FileStream(saveFileDialog.FileName, FileMode.Create);
                range.Save(filestream, DataFormats.Rtf);
                filestream.Close();
            }
        }

        private void OpenCommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf|All files (*.*)|*.*",
                DefaultExt = ".rtf",
            };
            if (openFileDialog.ShowDialog() == true)
            {
                TextRange range = new TextRange(MainRichTextbox.Document.ContentStart, MainRichTextbox.Document.ContentEnd);
                FileStream filestream = new FileStream(openFileDialog.FileName, FileMode.OpenOrCreate);
                range.Load(filestream, DataFormats.Rtf);
                filestream.Close();
            }
        }

        private void NewCommands_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Window1 window = new Window1();
            window.Show();
        }

        private void FontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            fontColor = (Color)e.NewValue;
            SolidColorBrush fontBrush = new SolidColorBrush(fontColor);
            MainRichTextbox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, fontBrush);
        }

        private void MainRIchTextBox_(object sender, RoutedEventArgs e)
        {
            var propety_bold = MainRichTextbox.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            Boldbutton.IsChecked = (propety_bold != DependencyProperty.UnsetValue) && (propety_bold.Equals(FontWeights.Bold));

            var propety_italic = MainRichTextbox.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            Italicbutton.IsChecked = (propety_italic != DependencyProperty.UnsetValue) && (propety_italic.Equals(FontStyles.Italic));

            var propety_underline = MainRichTextbox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            Underlinebutton.IsChecked = (propety_underline != DependencyProperty.UnsetValue) && (propety_underline.Equals(TextDecorations.Underline));

            var propety_fontfamily = MainRichTextbox.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            FontFamilyCombobox.SelectedItem = propety_fontfamily;

            var property_fontsize = MainRichTextbox.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            FontsizeCombobox.SelectedItem = property_fontsize;

            var propety_fontcolor = MainRichTextbox.Selection.GetPropertyValue(TextElement.ForegroundProperty);
            FontColorPicker.SelectedColor = ((SolidColorBrush)propety_fontcolor).Color;


        }

        private void Trashbutton_Click(object sender, RoutedEventArgs e)
        {
            MainRichTextbox.Document.Blocks.Clear();
        }

        private void FontFamilyCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(FontFamilyCombobox.SelectedItem != null)
            {
                FontFamily selectedFontFamily = (FontFamily)FontFamilyCombobox.SelectedItem;
                MainRichTextbox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, selectedFontFamily);
            }
        }

        private void FontsizeCombobox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(FontsizeCombobox.SelectedItem != null)
            {
                double selectedFontSize = (double)FontsizeCombobox.SelectedItem;
                MainRichTextbox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, selectedFontSize);
            }
        }
    }
}