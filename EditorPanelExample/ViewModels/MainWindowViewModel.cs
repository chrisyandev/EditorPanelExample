using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using DynamicData;
using EditorPanelExample.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace EditorPanelExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
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

            AvailableComponents = new List<string>(_componentNameToTypeMap.Keys);

            AddComponentFlyoutSelection = new SelectionModel<string>(AvailableComponents);
            AddComponentFlyoutSelection.SelectionChanged += AddComponentFlyoutSelectionChanged;


            // ===== Mock Data =====
            ViewModels.Add(new MaterialViewModel(new Material("ExampleMaterial.mat")));

            MaterialList materialList = new MaterialList();
            materialList.Add(new Material("ExampleMaterial.mat"));
            materialList.Add(new Material("ExampleMaterial1.mat"));
            materialList.Add(new Material("ExampleMaterial2.mat"));
            materialList.Add(new Material("ExampleMaterial3.mat"));
            ViewModels.Add(new MaterialListViewModel(materialList));

            ViewModels.Add(new TransformViewModel(new Transform(24, 30, 55)));

            ViewModels.Add(new AnimatorViewModel(new Animator("KnightController.controller", "knightAvatar")));
            // =====================
        }

        public ObservableCollection<ViewModelBase> ViewModels { get; }

        public List<string> AvailableComponents { get; }

        public SelectionModel<string> AddComponentFlyoutSelection { get; }

        private void AddComponent(string componentName)
        {
            Type viewModelType = _componentNameToTypeMap[componentName];
            ViewModelBase newVM = Activator.CreateInstance(viewModelType) as ViewModelBase;
            ViewModels.Add(newVM);
        }

        private void AddComponentFlyoutSelectionChanged(object sender, SelectionModelSelectionChangedEventArgs e)
        {
            string selectedName = e.SelectedItems.FirstOrDefault() as string;

            Debug.WriteLine($"Adding {selectedName}");

            if (selectedName != null && _componentNameToTypeMap.ContainsKey(selectedName))
            {
                AddComponent(selectedName);
            }
            else
            {
                Debug.WriteLine("Could not create component");
            }
        }
    }
}
