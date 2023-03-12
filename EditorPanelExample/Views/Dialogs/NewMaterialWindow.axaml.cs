using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using EditorPanelExample.ViewModels.Dialogs;
using EditorPanelExample.ViewModels;
using ReactiveUI;
using System.Threading.Tasks;
using Avalonia.Interactivity;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace EditorPanelExample.Views.Dialogs
{
    public partial class NewMaterialWindow : ReactiveWindow<NewMaterialViewModel>
    {
        public NewMaterialWindow()
        {
            InitializeComponent();

            this.WhenActivated(d => d(ViewModel!.AddCommand.Subscribe(CloseIfInputValid)));
        }

        private void CloseIfInputValid(object dialogResult)
        {
            if (dialogResult is string input && input.Trim() != string.Empty)
            {
                Close(dialogResult);
            }
            else
            {
                ViewModel!.InvalidInputMessage = new List<string> { "Invalid input" };
            }
        }

        public void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
