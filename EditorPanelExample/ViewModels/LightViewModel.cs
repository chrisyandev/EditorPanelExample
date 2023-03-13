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
    public class LightViewModel : MyComponentBase
    {
        private Light _light;
        private string _selectedType;

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

        public List<string> Types { get; set; }

        public string SelectedType
        {
            get => _selectedType;
            set
            {
                _selectedType = value;
                _light.CurrentType = value;
                this.RaisePropertyChanged(nameof(SelectedType));

                Debug.WriteLine(_light.CurrentType);
            }
        }

        public float Intensity
        {
            get => _light.Intensity;
            set
            {
                if (value == _light.Intensity) { return; }
                _light.Intensity = value;
                this.RaisePropertyChanged(nameof(Intensity));

                Debug.WriteLine(_light.Intensity);
            }
        }

        public float ShadowStrength
        {
            get => _light.Shadow.Strength;
            set
            {
                if (value == _light.Shadow.Strength) { return; }
                _light.Shadow.Strength = value;
                this.RaisePropertyChanged(nameof(ShadowStrength));

                Debug.WriteLine(_light.Shadow.Strength);
            }
        }

        private void SetupComponent()
        {
            Title = "Light";

            Types = new List<string>(
                typeof(Light.Type).GetFields()
                .Select(field => field.GetValue(null) as string));
            SelectedType = _light.CurrentType;


        }
    }
}