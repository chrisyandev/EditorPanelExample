using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EditorPanelExample.Models;
using ReactiveUI;

namespace EditorPanelExample.ViewModels
{
    public class MaterialViewModel : MyComponentBase
    {
        private Material _material;

        public MaterialViewModel()
        {
            _material = new Material();
            SetupComponent();
        }

        public MaterialViewModel(Material material)
        {
            _material = material;
            SetupComponent();
        }

        public string Material
        {
            get => _material.Name;
            set
            {
                if (value == _material.Name) { return; }
                _material.Name = value;
                this.RaisePropertyChanged(nameof(Material));

                Debug.WriteLine(_material.Name);
            }
        }

        private void SetupComponent()
        {
            Title = "Material";
        }

        public void ClearMaterial()
        {
            Material = "";
        }
    }
}
