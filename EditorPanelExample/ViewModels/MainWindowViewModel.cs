using Avalonia.Controls;
using DynamicData;
using EditorPanelExample.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EditorPanelExample.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public List<IComponent> Components { get; set; }

        public MainWindowViewModel()
        {
            Components = new List<IComponent>();

            Components.Add(new Material("ExampleMaterial.mat"));

            MaterialList materialList = new MaterialList();
            materialList.Add(new Material("ExampleMaterial.mat"));
            materialList.Add(new Material("ExampleMaterial1.mat"));
            materialList.Add(new Material("ExampleMaterial2.mat"));
            materialList.Add(new Material("ExampleMaterial3.mat"));
            Components.Add(materialList);

            Components.Add(new Transform(24, 30, 55));

            /*            EditorPanels = new List<EditorPanel>();

                        EditorPanels.Add(new EditorPanel(new Material("ExampleMaterial.mat")));*/

            /*            MaterialList materialList = new MaterialList();
                        materialList.Materials.Add(new Material("ExampleMaterial.mat"));
                        materialList.Materials.Add(new Material("ExampleMaterial1.mat"));
                        materialList.Materials.Add(new Material("ExampleMaterial2.mat"));
                        materialList.Materials.Add(new Material("ExampleMaterial3.mat"));
                        EditorPanels.Add(new EditorPanel(materialList));

                        EditorPanels.Add);*/
        }
    }
}
