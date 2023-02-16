using Avalonia.Controls;
using DynamicData;
using EditorPanelExample.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace EditorPanelExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ViewModelBase> ViewModels { get; set; }

        public MainWindowViewModel()
        {
            ViewModels = new ObservableCollection<ViewModelBase>();

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

    }
}
