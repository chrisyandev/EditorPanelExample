using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using EditorPanelExample.Models;
using EditorPanelExample.ViewModels.Dialogs;
using EditorPanelExample.Views.Dialogs;
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
            SetupNewMaterialDialog();
        }

        public MaterialListViewModel(MaterialList materialList)
        {
            Materials = new ObservableCollection<Material>(materialList);
            SetupNewMaterialDialog();
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

        public void RemoveMaterial(Material material)
        {
            Materials.Remove(material);
        }

        public override void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }

        #region Add New Material
        public ICommand AddMaterialCommand { get; private set; }

        public Interaction<NewMaterialViewModel, string> ShowNewMaterialDialog { get; private set; }

        private void SetupNewMaterialDialog()
        {
            ShowNewMaterialDialog = new Interaction<NewMaterialViewModel, string>();

            AddMaterialCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                NewMaterialViewModel vm = new NewMaterialViewModel();

                string result = await ShowNewMaterialDialog.Handle(vm);

                Debug.WriteLine($"MaterialListVM result: {result}");

                if ( result != null && result.Trim() != string.Empty)
                {
                    Material newMaterial = new(result);
                    Materials.Add(newMaterial);

                    Debug.WriteLine($"Added new material: {newMaterial.Name}");
                }
            });
        }
        #endregion
    }
}
