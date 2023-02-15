using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace EditorPanelExample.Views.Controls
{
    public class ComponentHeaderTemplate : TemplatedControl
    {
        public static readonly StyledProperty<string> TitleProperty = 
            AvaloniaProperty.Register<ComponentHeaderTemplate, string>(nameof(Title), $"<{nameof(Title)}>");

        public string Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
    }
}
