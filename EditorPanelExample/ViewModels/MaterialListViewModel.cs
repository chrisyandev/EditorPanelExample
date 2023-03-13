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
        public MaterialListViewModel()
        {
            Materials = new ObservableCollection<Material>();
            SetupComponent();
        }

        public MaterialListViewModel(MaterialList materialList)
        {
            Materials = new ObservableCollection<Material>(materialList);
            SetupComponent();
        }

        public ObservableCollection<Material> Materials { get; set; }

        private void SetupComponent()
        {
            Title = "Material List";
            SetupNewMaterialDialog();
        }

        public void RemoveMaterial(Material material)
        {
            Materials.Remove(material);
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
