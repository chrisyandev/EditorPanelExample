using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using SkiaSharp;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EditorPanelExample.Views.Components
{
    public partial class TransformControl : UserControl
    {
        private TextBox _textBox_X;
        private TextBox _textBox_Y;
        private TextBox _textBox_Z;

        public TransformControl()
        {
            InitializeComponent();

            _textBox_X = this.FindControl<TextBox>("textBox_X");
            _textBox_Y = this.FindControl<TextBox>("textBox_Y");
            _textBox_Z = this.FindControl<TextBox>("textBox_Z");

            _textBox_X.AddHandler(TextInputEvent, TextBox_X_PreviewTextInput, RoutingStrategies.Tunnel);
            _textBox_X.PastingFromClipboard += TextBox_X_PastingFromClipboard;

            _textBox_Y.AddHandler(TextInputEvent, TextBox_Y_PreviewTextInput, RoutingStrategies.Tunnel);
            _textBox_Y.PastingFromClipboard += TextBox_Y_PastingFromClipboard;

            _textBox_Z.AddHandler(TextInputEvent, TextBox_Z_PreviewTextInput, RoutingStrategies.Tunnel);
            _textBox_Z.PastingFromClipboard += TextBox_Z_PastingFromClipboard;

            // Note: If valid string is pasted to overwrite existing string
            // that contains '-' or '.', ex: -23.45 pasted to overwrite -19.78,
            // will not paste due to the way clipboard data is validated
        }

        private static bool IsInputAllowed(string text)
        {
            Regex regex = new(@"[0-9\.\-]");
            return regex.IsMatch(text);
        }

        private static bool IsFloat(string text)
        {
            Regex regex = new(@"^[+-]?([0-9]+([.][0-9]*)?|[.][0-9]+)$");
            return regex.IsMatch(text);
        }

        private void TextBox_X_PreviewTextInput(object sender, TextInputEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;

            if (!IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, e.Text)))
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }

        private async void TextBox_X_PastingFromClipboard(object sender, RoutedEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            string text = await Application.Current.Clipboard.GetTextAsync();
            
            if (!IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, text)))
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }

        private void TextBox_Y_PreviewTextInput(object sender, TextInputEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;

            if (!IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, e.Text)))
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }

        private async void TextBox_Y_PastingFromClipboard(object sender, RoutedEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            string text = await Application.Current.Clipboard.GetTextAsync();

            if (!IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, text)))
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }

        private void TextBox_Z_PreviewTextInput(object sender, TextInputEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;

            if (!IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, e.Text)))
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }

        private async void TextBox_Z_PastingFromClipboard(object sender, RoutedEventArgs e)
        {
            TextBox senderTextBox = sender as TextBox;
            string text = await Application.Current.Clipboard.GetTextAsync();

            if (!IsFloat(senderTextBox.Text.Insert(senderTextBox.CaretIndex, text)))
            {
                e.Handled = true;
                return;
            }
            e.Handled = false;
        }
    }
}
