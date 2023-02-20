using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using EditorPanelExample.Models;
using EditorPanelExample.ViewModels;

namespace EditorPanelExample.Views
{
    public partial class MainWindow : Window
    {
        private ScrollViewer _mainScrollViewer;
        private Button _addComponentButton;
        private ListBox _addComponentListBox;

        public MainWindow()
        {
            InitializeComponent();

            _mainScrollViewer = this.FindControl<ScrollViewer>("mainScrollViewer");
            _addComponentButton = this.FindControl<Button>("addComponentButton");
            _addComponentListBox = this.FindControl<ListBox>("addComponentListBox");

            _addComponentListBox.PointerReleased += AddComponentListBoxPointerReleased;
        }

        private void AddComponentListBoxPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            HideAddComponentFlyout();
            _addComponentListBox.UnselectAll();
            ScrollToBottom();
        }

        public void HideAddComponentFlyout()
        {
            _addComponentButton.Flyout.Hide();
        }

        public void ScrollToBottom()
        {
            _mainScrollViewer.ScrollToEnd();
        }
    }
}
