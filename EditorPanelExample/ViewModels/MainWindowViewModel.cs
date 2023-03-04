using Avalonia.Controls;
using Avalonia.Controls.Selection;
using Avalonia.Input;
using DynamicData;
using EditorPanelExample.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
            ViewModels = new ObservableCollection<MyComponentBase>();

            AvailableComponents = new List<string>(_componentNameToTypeMap.Keys);

            AddComponentFlyoutSelection = new SelectionModel<string>(AvailableComponents);
            AddComponentFlyoutSelection.SelectionChanged += AddComponentFlyoutSelectionChanged;

            ReactiveCommand<Tuple<MyComponentBase, MyComponentBase>, Unit> insertComponentCommand
                = ReactiveCommand.Create<Tuple<MyComponentBase, MyComponentBase>>(_ =>
                {
                    (MyComponentBase target, MyComponentBase source) = _;

                    if (target != source)
                    {
                        string dragDirection = ViewModels.IndexOf(source) < ViewModels.IndexOf(target) ? "down" : "up";

                        // Must remove before inserting
                        ViewModels.Remove(source);

                        if (dragDirection == "down")
                        {
                            ViewModels.Insert(ViewModels.IndexOf(target) + 1, source);
                        }
                        else if (dragDirection == "up")
                        {
                            ViewModels.Insert(ViewModels.IndexOf(target), source);
                        }
                    }
                });
            InsertComponentCommand = insertComponentCommand;


            // ===== Mock Data =====
            ViewModels.Add(new MaterialViewModel(new Material("ExampleMaterial.mat")));

            MaterialList materialList = new MaterialList
            {
                new Material("ExampleMaterial.mat"),
                new Material("ExampleMaterial1.mat"),
                new Material("ExampleMaterial2.mat"),
                new Material("ExampleMaterial3.mat")
            };
            ViewModels.Add(new MaterialListViewModel(materialList));

            ViewModels.Add(new TransformViewModel(new Transform(24, 30, 55)));

            ViewModels.Add(new AnimatorViewModel(new Animator("KnightController.controller", "knightAvatar")));

            foreach (MyComponentBase component in ViewModels)
            {
                SetRemoveComponentCommand(component);
                SetMoveUpCommand(component);
                SetMoveDownCommand(component);
            }
            // =====================
        }

        public ObservableCollection<MyComponentBase> ViewModels { get; }

        public List<string> AvailableComponents { get; }

        public SelectionModel<string> AddComponentFlyoutSelection { get; }

        private void AddComponent(string componentName)
        {
            Type viewModelType = _componentNameToTypeMap[componentName];
            MyComponentBase newComponent = Activator.CreateInstance(viewModelType) as MyComponentBase;
            ViewModels.Add(newComponent);

            SetRemoveComponentCommand(newComponent);
            SetMoveUpCommand(newComponent);
            SetMoveDownCommand(newComponent);
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

        private void SetRemoveComponentCommand(MyComponentBase component)
        {
            ReactiveCommand<MyComponentBase, Unit> removeComponentCommand =
                ReactiveCommand.Create<MyComponentBase>(_ => ViewModels.Remove(component));
            component.RemoveComponentCommand = removeComponentCommand;
        }

        private void SetMoveUpCommand(MyComponentBase component)
        {
            ReactiveCommand<MyComponentBase, Unit> moveUpCommand =
                ReactiveCommand.Create<MyComponentBase>(_ =>
                {
                    int currentIndex = ViewModels.IndexOf(component);
                    int previousIndex = currentIndex - 1;
                    if (previousIndex >= 0)
                    {
                        MyComponentBase temp = ViewModels[previousIndex];
                        ViewModels[previousIndex] = ViewModels[currentIndex];
                        ViewModels[currentIndex] = temp;
                    }
                });
            component.MoveUpCommand = moveUpCommand;
        }

        private void SetMoveDownCommand(MyComponentBase component)
        {
            ReactiveCommand<MyComponentBase, Unit> moveDownCommand =
                ReactiveCommand.Create<MyComponentBase>(_ =>
                {
                    int currentIndex = ViewModels.IndexOf(component);
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < ViewModels.Count)
                    {
                        MyComponentBase temp = ViewModels[nextIndex];
                        ViewModels[nextIndex] = ViewModels[currentIndex];
                        ViewModels[currentIndex] = temp;
                    }
                });
            component.MoveDownCommand = moveDownCommand;
        }

        public ICommand InsertComponentCommand { get; set; }
    }
}
