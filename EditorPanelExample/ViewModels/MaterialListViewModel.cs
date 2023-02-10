using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditorPanelExample.Models;

namespace EditorPanelExample.ViewModels
{
    public class MaterialListViewModel : ViewModelBase
    {
        public ObservableCollection<Material> Materials { get; set; }

        public MaterialListViewModel(MaterialList materialList)
        {
            Materials = new ObservableCollection<Material>(materialList);
        }

        public void AddMaterial()
        {
            Materials.Add(new Material(""));
        }
    }
}
