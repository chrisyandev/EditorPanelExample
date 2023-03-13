using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using EditorPanelExample.Models;

namespace EditorPanelExample.ViewModels
{
    public class LightViewModel : MyComponentBase
    {
        private Light _light;

        public LightViewModel()
        {
            _light = new Light();
            SetupComponent();
        }

        public LightViewModel(Light light)
        {
            _light = light;
            SetupComponent();
        }

        private void SetupComponent()
        {
            Title = "Light";
        }
    }
}