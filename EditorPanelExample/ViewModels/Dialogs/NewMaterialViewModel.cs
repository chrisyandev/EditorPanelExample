using EditorPanelExample.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.ViewModels.Dialogs
{
    public class NewMaterialViewModel : ViewModelBase
    {
        private string _newMaterial;

        public NewMaterialViewModel()
        {
            AddCommand = ReactiveCommand.Create(() =>
            {
                Debug.WriteLine("Add Command");
                return NewMaterial;
            });
        }

        public string NewMaterial
        {
            get => _newMaterial;
            set
            {
                if (value == _newMaterial) { return; }
                _newMaterial = value;
                this.RaisePropertyChanged(nameof(NewMaterial));

                Debug.WriteLine(_newMaterial);
            }
        }

        public ReactiveCommand<Unit, string> AddCommand { get; }
    }
}
