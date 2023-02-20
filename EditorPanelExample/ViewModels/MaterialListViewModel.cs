using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditorPanelExample.Models;
using ReactiveUI;

namespace EditorPanelExample.ViewModels
{
    public class MaterialListViewModel : ViewModelBase, IMyCollapsible
    {
        private bool _isCollapsed;

        public ObservableCollection<Material> Materials { get; set; }

        public MaterialListViewModel()
        {
            Materials = new ObservableCollection<Material>();
        }

        public MaterialListViewModel(MaterialList materialList)
        {
            Materials = new ObservableCollection<Material>(materialList);
        }

        public string Description { get; } = "Placeholder for description of Material List";

        public bool IsCollapsed
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
        }

        public void AddMaterial()
        {
            Materials.Add(new Material(""));
        }

        public void RemoveMaterial(Material material)
        {
            Materials.Remove(material);
        }

        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }
    }
}
