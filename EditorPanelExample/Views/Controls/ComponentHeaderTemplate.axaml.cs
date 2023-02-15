using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

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
    }
}
