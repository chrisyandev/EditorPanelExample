using Avalonia.Controls;
using DynamicData;
using EditorPanelExample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace EditorPanelExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<IMyComponent> Components { get; set; }

        public MainWindowViewModel()
        {
            Components = new ObservableCollection<IMyComponent>();

            Components.Add(new Material("ExampleMaterial.mat"));

            MaterialList materialList = new MaterialList();
            materialList.Add(new Material("ExampleMaterial.mat"));
            materialList.Add(new Material("ExampleMaterial1.mat"));
            materialList.Add(new Material("ExampleMaterial2.mat"));
            materialList.Add(new Material("ExampleMaterial3.mat"));
            Components.Add(materialList);

            Components.Add(new Transform(24, 30, 55));
        }

    }
}
