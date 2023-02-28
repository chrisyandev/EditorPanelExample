using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Input;
using Avalonia.Interactivity;
using EditorPanelExample.ViewModels;
using ReactiveUI;
using System.Collections.Generic;
using System.Diagnostics;
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

            _componentTitleButton = e.NameScope.Find<ComponentTitleButton>("componentTitleButton");
            Debug.WriteLine(_componentTitleButton);

            _componentTitleButton.ComponentTitleButtonMouseDown += PointerPressedHandler;
            _componentTitleButton.ComponentTitleButtonMouseUp += PointerReleasedHandler;
        }

        private ComponentTitleButton _componentTitleButton;

        private void PointerMovedHandler(object sender, PointerEventArgs e)
        {
            Debug.WriteLine("pointer moved");
        }

        private void PointerPressedHandler(object sender, PointerPressedEventArgs e)
        {
            Debug.WriteLine("pointer pressed");
            _componentTitleButton.PointerMoved += PointerMovedHandler;
        }

        private void PointerReleasedHandler(object sender, PointerReleasedEventArgs e)
        {
            Debug.WriteLine("pointer released");
            _componentTitleButton.PointerMoved -= PointerMovedHandler;
        }
    }
}
