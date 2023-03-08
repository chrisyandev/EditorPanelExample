using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using EditorPanelExample.Models;
using ReactiveUI;

namespace EditorPanelExample.ViewModels
{
    public class MaterialListViewModel : MyComponentBase
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

        public string Title { get; } = "Material List";

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

        public void AddMaterial()
        {
            Materials.Add(new Material(""));
        }

        public void RemoveMaterial(Material material)
        {
            Materials.Remove(material);
        }

        public override void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }
    }
}
