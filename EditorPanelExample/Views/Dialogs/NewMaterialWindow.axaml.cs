using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using EditorPanelExample.ViewModels.Dialogs;
using EditorPanelExample.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using System;

namespace EditorPanelExample.Views.Dialogs
{
    public partial class NewMaterialWindow : ReactiveWindow<NewMaterialViewModel>
    {
        private Button _addButton;
        private Button _cancelButton;

        public NewMaterialWindow()
        {
            InitializeComponent();

            _addButton = this.FindControl<Button>("addButton");
            _cancelButton = this.FindControl<Button>("cancelButton");

            //this.WhenActivated(d => d(ViewModel!.AddCommand.Subscribe(Close)));
        }
    }
}
