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
    public class MaterialViewModel : ViewModelBase
    {
        private Material _material;

        public MaterialViewModel(Material material)
        {
            _material = material;
        }

        public Material Material
        { 
            get => _material;
            set => this.RaiseAndSetIfChanged(ref _material, value);
        }

        public void ClearMaterial()
        {
            Material = new Material("");
        }
    }
}
