using System;
using System.Collections.Generic;
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
        private readonly Material _material;

        public MaterialViewModel(Material material)
        {
            _material = material;
        }

        public string MaterialName => _material.MaterialName;

    }
}
