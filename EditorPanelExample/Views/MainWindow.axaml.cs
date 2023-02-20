using Avalonia.Controls;
using Avalonia.Controls.Selection;
using EditorPanelExample.Models;
using EditorPanelExample.ViewModels;

namespace EditorPanelExample.Views
{
    public partial class MainWindow : Window
    {
        private Button _addComponentButton;
        private ListBox _addComponentListBox;

        public MainWindow()
        {
            InitializeComponent();

            _addComponentButton = this.FindControl<Button>("addComponentButton");
            _addComponentListBox = this.FindControl<ListBox>("addComponentListBox");
            _addComponentListBox.SelectionChanged += AddComponentListBoxSelectionChanged;
        }

        private void AddComponentListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAddComponentFlyout();
        }

        public void HideAddComponentFlyout()
        {
            _addComponentButton.Flyout.Hide();
        }
    }
}
