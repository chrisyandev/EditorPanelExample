using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
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
        }
    }
}
