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

            #region Creating Commands
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

            ReactiveCommand<Tuple<MyComponentBase, MyComponentBase>, string> getDragDirectionCommand
                = ReactiveCommand.Create<Tuple<MyComponentBase, MyComponentBase>, string>(_ =>
                {
                    (MyComponentBase target, MyComponentBase source) = _;

                    int indexOfSource = ViewModels.IndexOf(source);
                    int indexOfTarget = ViewModels.IndexOf(target);

                    if (indexOfSource < indexOfTarget)
                    {
                        return "down";
                    }
                    else if (indexOfSource > indexOfTarget)
                    {
                        return "up";
                    }
                    else
                    {
                        return "none";
                    }
                });
            GetDragDirectionCommand = getDragDirectionCommand;

            ReactiveCommand<Unit, Unit> collapseAllCommand
                = ReactiveCommand.Create(() =>
                {
                    foreach (MyComponentBase component in ViewModels)
                    {
                        component.IsCollapsed = true;
                    }
                });
            CollapseAllCommand = collapseAllCommand;

            ReactiveCommand<Unit, Unit> expandAllCommand
                = ReactiveCommand.Create(() =>
                {
                    foreach (MyComponentBase component in ViewModels)
                    {
                        component.IsCollapsed = false;
                    }
                });
            ExpandAllCommand = expandAllCommand;
            #endregion


            #region Mock Data
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
                SetContextMenuSelectedCommand(component);
            }
            #endregion
        }

        public ObservableCollection<MyComponentBase> ViewModels { get; }

        #region Add Component Logic
        public List<string> AvailableComponents { get; }

        private string _selectedComponentName;

        public string SelectedComponentName
        {
            get => _selectedComponentName;
            set
            {
                if (value == _selectedComponentName) return;
                _selectedComponentName = value;
                this.RaisePropertyChanged(nameof(SelectedComponentName));


                if (_selectedComponentName != null && _componentNameToTypeMap.ContainsKey(_selectedComponentName))
                {
                    AddComponent(_selectedComponentName);
                }
                else
                {
                    // just for skipping debug message
                    if (_selectedComponentName == null) return;
                    Debug.WriteLine("Could not create component");
                }
            }
        }

        private void AddComponent(string componentName)
        {
            Type viewModelType = _componentNameToTypeMap[componentName];
            MyComponentBase newComponent = Activator.CreateInstance(viewModelType) as MyComponentBase;
            ViewModels.Add(newComponent);

            SetContextMenuSelectedCommand(newComponent);

            Debug.WriteLine($"Added {componentName}");
        }
        #endregion

        private void SetContextMenuSelectedCommand(MyComponentBase component)
        {
            ReactiveCommand<string, Unit> contextMenuSelectedCommand =
                ReactiveCommand.Create<string>(selected =>
                {
                    int currentIndex = ViewModels.IndexOf(component);

                    switch (selected)
                    {
                        case "remove component":
                            ViewModels.Remove(component);
                            break;
                        case "move up":
                            int previousIndex = currentIndex - 1;
                            if (previousIndex >= 0)
                            {
                                MyComponentBase temp = ViewModels[previousIndex];
                                ViewModels[previousIndex] = ViewModels[currentIndex];
                                ViewModels[currentIndex] = temp;
                            }
                            break;
                        case "move down":
                            int nextIndex = currentIndex + 1;
                            if (nextIndex < ViewModels.Count)
                            {
                                MyComponentBase temp = ViewModels[nextIndex];
                                ViewModels[nextIndex] = ViewModels[currentIndex];
                                ViewModels[currentIndex] = temp;
                            }
                            break;
                        default:
                            break;
                    }
                });
            component.ContextMenuSelectedCommand = contextMenuSelectedCommand;
        }

        public ICommand InsertComponentCommand { get; set; }

        public ReactiveCommand<Tuple<MyComponentBase, MyComponentBase>, string> GetDragDirectionCommand { get; set; }

        public ICommand CollapseAllCommand { get; set; }

        public ICommand ExpandAllCommand { get; set; }
    }
}
