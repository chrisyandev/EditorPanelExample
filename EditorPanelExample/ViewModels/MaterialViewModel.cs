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

        public List<string> ContextMenuItems { get; } = new List<string>()
        {
            "Remove Component",
            "Move Up",
            "Move Down"
        };

        public override ICommand RemoveComponentCommand { get; set; }

        public override ICommand MoveUpCommand { get; set; }

        public override ICommand MoveDownCommand { get; set; }

        public override bool IsCollapsed 
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

        public override void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }
    }
}
