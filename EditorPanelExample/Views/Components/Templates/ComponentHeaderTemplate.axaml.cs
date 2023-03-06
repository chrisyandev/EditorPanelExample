using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.VisualTree;
using EditorPanelExample.ViewModels;
using EditorPanelExample.Views.Components.Common;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Windows.Input;

namespace EditorPanelExample.Views.Components.Templates
{
    public class ComponentHeaderTemplate : TemplatedControl
    {
        public static readonly StyledProperty<string> MyTitleProperty =
            AvaloniaProperty.Register<ComponentHeaderTemplate, string>(nameof(MyTitle), $"<{nameof(MyTitle)}>");

        public static readonly StyledProperty<string> MyTooltipProperty =
            AvaloniaProperty.Register<ComponentHeaderTemplate, string>(nameof(MyTooltip), $"<{nameof(MyTooltip)}>");

        public string MyTitle
        {
            get => GetValue(MyTitleProperty);
            set => SetValue(MyTitleProperty, value);
        }

        public string MyTooltip
        {
            get => GetValue(MyTooltipProperty);
            set => SetValue(MyTooltipProperty, value);
        }

        #region Context Menu Properties
        public static readonly DirectProperty<ComponentHeaderTemplate, ICommand> OpenContextMenuCommandProperty =
            AvaloniaProperty.RegisterDirect<ComponentHeaderTemplate, ICommand>(
                nameof(OpenContextMenuCommand),
                o => o.OpenContextMenuCommand);

        private ICommand _openContextMenuCommand;

        public ICommand OpenContextMenuCommand
        {
            get => _openContextMenuCommand;
            private set => SetAndRaise(OpenContextMenuCommandProperty, ref _openContextMenuCommand, value);
        }
        #endregion

        #region Drag Component Properties
        private ComponentTitleButton _componentTitleButton;

        public static readonly StyledProperty<ICommand> MyInsertComponentCommandProperty =
            AvaloniaProperty.Register<ComponentHeaderTemplate, ICommand>(nameof(MyInsertComponentCommand));

        public ICommand MyInsertComponentCommand
        {
            get => GetValue(MyInsertComponentCommandProperty);
            set => SetValue(MyInsertComponentCommandProperty, value);
        }

        public static readonly StyledProperty<ReactiveCommand<Tuple<MyComponentBase, MyComponentBase>, string>> MyGetDragDirectionCommandProperty =
            AvaloniaProperty.Register<ComponentHeaderTemplate, ReactiveCommand<Tuple<MyComponentBase, MyComponentBase>, string>>(nameof(MyGetDragDirectionCommand));

        public ReactiveCommand<Tuple<MyComponentBase, MyComponentBase>, string> MyGetDragDirectionCommand
        {
            get => GetValue(MyGetDragDirectionCommandProperty);
            set => SetValue(MyGetDragDirectionCommandProperty, value);
        }
        #endregion

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);

            #region Set Up Context Menu Button
            ContextMenu componentContextMenu = e.NameScope.Find<ContextMenu>("componentContextMenu");
            Button contextMenuButton = e.NameScope.Find<Button>("contextMenuButton");

            ICommand openContextMenuCommand =
                ReactiveCommand.Create<Unit>(_ =>
                {
                    if (componentContextMenu != null && contextMenuButton != null)
                    {
                        componentContextMenu.PlacementTarget = contextMenuButton;
                        componentContextMenu.Open();
                    }
                });
            OpenContextMenuCommand = openContextMenuCommand;
            #endregion

            #region Set Up Drag Component
            _componentTitleButton = e.NameScope.Find<ComponentTitleButton>("componentTitleButton");
            _componentTitleButton.ComponentTitleButtonMouseDown += DoDrag;
            _componentTitleButton.AddHandler(DragDrop.DropEvent, Drop);
            _componentTitleButton.AddHandler(DragDrop.DragEnterEvent, DragEnter);
            #endregion
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            Debug.WriteLine("Drag Enter");

            if (sender is ComponentTitleButton enteredButton)
            {
                Border currentBorder = enteredButton.FindAncestorOfType<Border>();
                Border lastBorder = (e.Data.Get("LastBorder") as Border[])[0];

                if (currentBorder == lastBorder)
                {
                    string currentDragDirection = (e.Data.Get("DragDirection") as string[])[0];
                    PaintBorder(currentBorder, Brushes.LightBlue, currentDragDirection);
                }
                else
                {
                    MyComponentBase targetComponent = enteredButton.DataContext as MyComponentBase;
                    MyComponentBase sourceComponent = e.Data.Get("SourceComponent") as MyComponentBase;

                    MyGetDragDirectionCommand.Execute(Tuple.Create(targetComponent, sourceComponent)).Subscribe(dragDirection =>
                    {
                        PaintBorder(currentBorder, Brushes.LightBlue, dragDirection);

                        lastBorder.BorderThickness = new Thickness(0, 0, 0, 0);
                        (e.Data.Get("LastBorder") as Border[])[0] = currentBorder;
                        (e.Data.Get("DragDirection") as string[])[0] = dragDirection;
                    });
                }
            }
        }

        private async void DoDrag(object sender, PointerPressedEventArgs e)
        {
            Debug.WriteLine("Drag Start");

            DataObject dragData = new DataObject();

            if (sender is ComponentTitleButton button)
            {
                dragData.Set("SourceComponent", button.DataContext);
                // Use array for data that needs to be modified during drag
                dragData.Set("LastBorder", new Border[] { button.FindAncestorOfType<Border>() });
                dragData.Set("DragDirection", new string[] { "none" });
            }

            DragDropEffects result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);

            if (result == DragDropEffects.None)
            {
                Border lastBorder = (dragData.Get("LastBorder") as Border[])[0];
                lastBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        private void Drop(object? sender, DragEventArgs e)
        {
            Debug.WriteLine("Drag End");

            e.DragEffects = DragDropEffects.Move;

            if (sender is ComponentTitleButton button)
            {
                MyComponentBase targetComponent = button.DataContext as MyComponentBase;
                Debug.WriteLine($"Target: {targetComponent}");

                MyComponentBase sourceComponent = e.Data.Get("SourceComponent") as MyComponentBase;
                Debug.WriteLine($"Source: {sourceComponent}");

                MyInsertComponentCommand.Execute(Tuple.Create(targetComponent, sourceComponent));

                Border lastBorder = (e.Data.Get("LastBorder") as Border[])[0];
                lastBorder.BorderThickness = new Thickness(0, 0, 0, 0);
            }
        }

        private void PaintBorder(Border border, ISolidColorBrush brush, string dragDirection)
        {
            border.BorderBrush = brush;

            if (dragDirection == "up")
            {
                border.BorderThickness = new Thickness(0, 2, 0, 0);
            }
            else if (dragDirection == "down")
            {
                border.BorderThickness = new Thickness(0, 0, 0, 2);
            }
            else
            {
                border.BorderThickness = new Thickness(0, 2, 0, 0);
            }
        }
    }
}
