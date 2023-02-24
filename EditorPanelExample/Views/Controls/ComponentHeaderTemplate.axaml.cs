using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using System.Diagnostics;

namespace EditorPanelExample.Views.Controls
{
    public class ComponentHeaderTemplate : TemplatedControl
    {
        public static readonly StyledProperty<string> MyTitleProperty = 
            AvaloniaProperty.Register<ComponentHeaderTemplate, string>(nameof(MyTitle), $"<{nameof(MyTitle)}>");

        public static readonly StyledProperty<string> MyTooltipProperty =
            AvaloniaProperty.Register<ComponentHeaderTemplate, string>(nameof(MyTooltip), $"<{nameof(MyTooltip)}>");

        public static readonly StyledProperty<object> MyContextMenuItemsProperty =
            AvaloniaProperty.Register<ComponentHeaderTemplate, object>(nameof(MyContextMenuItems), $"<{nameof(MyContextMenuItems)}>");

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

        public object MyContextMenuItems
        {
            get
            {
                Debug.WriteLine(GetValue(MyContextMenuItemsProperty));
                return GetValue(MyContextMenuItemsProperty);
            } 
            set => SetValue(MyContextMenuItemsProperty, value);
        }
    }
}
