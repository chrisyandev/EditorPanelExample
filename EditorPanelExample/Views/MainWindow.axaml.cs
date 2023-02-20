using Avalonia.Controls;
using EditorPanelExample.Models;
using EditorPanelExample.ViewModels;

namespace EditorPanelExample.Views
{
    public partial class MainWindow : Window, IMainWindow
    {
        private Button _addComponentButton;

        public MainWindow()
        {
            InitializeComponent();

            _addComponentButton = this.FindControl<Button>("addComponentButton");
        }

        protected override void OnDataContextEndUpdate()
        {
            base.OnDataContextEndUpdate();

            (DataContext as MainWindowViewModel).MainWindowView = this;
        }

        public void HideAddComponentFlyout()
        {
            _addComponentButton.Flyout.Hide();
        }
    }
}
