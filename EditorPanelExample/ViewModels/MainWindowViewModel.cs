using Avalonia.Controls;
using DynamicData;
using EditorPanelExample.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace EditorPanelExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _selectedComponentToAdd = null;

        private Dictionary<string, Type> _componentNameToTypeMap = new Dictionary<string, Type>()
        {
            ["Material"] = typeof(MaterialViewModel),
            ["Material List"] = typeof(MaterialListViewModel),
            ["Transform"] = typeof(TransformViewModel),
            ["Animator"] = typeof(AnimatorViewModel)
        };

        public MainWindowViewModel()
        {
            ViewModels = new ObservableCollection<ViewModelBase>();
            AvailableComponents = new ObservableCollection<string>(_componentNameToTypeMap.Keys);

            // Mock Data
            ViewModels.Add(new MaterialViewModel(new Material("ExampleMaterial.mat")));

            MaterialList materialList = new MaterialList();
            materialList.Add(new Material("ExampleMaterial.mat"));
            materialList.Add(new Material("ExampleMaterial1.mat"));
            materialList.Add(new Material("ExampleMaterial2.mat"));
            materialList.Add(new Material("ExampleMaterial3.mat"));
            ViewModels.Add(new MaterialListViewModel(materialList));

            ViewModels.Add(new TransformViewModel(new Transform(24, 30, 55)));

            ViewModels.Add(new AnimatorViewModel(new Animator("KnightController.controller", "knightAvatar")));
        }

        public ObservableCollection<ViewModelBase> ViewModels { get; }

        public ObservableCollection<string> AvailableComponents { get; }

        public string SelectedComponentToAdd
        {
            get => _selectedComponentToAdd;
            set
            {
                AddComponent(value);
            }
        }

        private void AddComponent(string componentName)
        {
            Type viewModelType = _componentNameToTypeMap[componentName];
            ViewModelBase newVM = Activator.CreateInstance(viewModelType) as ViewModelBase;
            ViewModels.Add(newVM);
        }
    }
}
