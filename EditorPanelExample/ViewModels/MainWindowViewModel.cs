using Avalonia.Controls;
using EditorPanelExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EditorPanelExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<EditorPanel> EditorPanels { get; set; }

        public MainWindowViewModel()
        {
            EditorPanels = new List<EditorPanel>();

            EditorPanels.Add(new EditorPanel(new Material("ExampleMaterial.mat")));

            MaterialList materialList = new MaterialList();
            materialList.Materials.Add(new Material("ExampleMaterial.mat"));
            materialList.Materials.Add(new Material("ExampleMaterial1.mat"));
            materialList.Materials.Add(new Material("ExampleMaterial2.mat"));
            materialList.Materials.Add(new Material("ExampleMaterial3.mat"));
            EditorPanels.Add(new EditorPanel(materialList));

            EditorPanels.Add(new EditorPanel(new Transform(24, 30, 55)));
        }
    }
}
