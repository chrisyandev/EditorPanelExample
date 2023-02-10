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
    public class MaterialViewModel : ViewModelBase, ICollapsible
    {
        private bool _isCollapsed;

        private Material _material;

        public MaterialViewModel(Material material)
        {
            _material = material;
        }

        public bool IsCollapsed 
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
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

        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }
    }
}
