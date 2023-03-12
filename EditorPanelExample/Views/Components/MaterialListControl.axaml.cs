using Avalonia.Controls;
using Avalonia.VisualTree;
using EditorPanelExample.ViewModels;
using EditorPanelExample.ViewModels.Dialogs;
using EditorPanelExample.Views.Dialogs;
using ReactiveUI;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EditorPanelExample.Views.Components
{
    public partial class MaterialListControl : UserControl
    {
        private Window _parentWindow;

        public MaterialListControl()
        {
            InitializeComponent();
        }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _parentWindow = this.FindAncestorOfType<Window>();

            (DataContext as MaterialListViewModel).ShowNewMaterialDialog.RegisterHandler(DoShowNewMaterialDialog);
        }

        private async Task DoShowNewMaterialDialog(InteractionContext<NewMaterialViewModel, string> interaction)
        {
            NewMaterialWindow dialogWindow = new NewMaterialWindow
            {
                DataContext = interaction.Input
            };

            string result = await dialogWindow.ShowDialog<string>(_parentWindow);

            interaction.SetOutput(result);
        }
    }
}
