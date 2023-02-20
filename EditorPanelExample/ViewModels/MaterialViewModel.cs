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
    public class MaterialViewModel : ViewModelBase, IMyCollapsible
    {
        private bool _isCollapsed;

        private Material _material;

        public MaterialViewModel()
        {
            _material = new Material();
        }

        public MaterialViewModel(Material material)
        {
            _material = material;
        }

        public string Description { get; } = "Placeholder for description of Material";

        public bool IsCollapsed 
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
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

        public void ClearMaterial()
        {
            Material = "";
        }

        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }
    }
}
