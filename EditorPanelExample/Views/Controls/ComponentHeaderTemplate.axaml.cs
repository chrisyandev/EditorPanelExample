using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using EditorPanelExample.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Windows.Input;

namespace EditorPanelExample.Views.Controls
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
            _componentTitleButton.AddHandler(DragDrop.DragOverEvent, DragOver);
            #endregion
        }

        private async void DoDrag(object sender, PointerPressedEventArgs e)
        {
            Debug.WriteLine("Drag Start");

            var dragData = new DataObject();

            if (sender is ComponentTitleButton button)
            {
                dragData.Set("SourceComponent", button.DataContext);
            }

            var result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link);
            Debug.WriteLine(result);
        }

        private void DragOver(object? sender, DragEventArgs e)
        {
            e.DragEffects = DragDropEffects.Move;
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
            }
        }
    }
}
