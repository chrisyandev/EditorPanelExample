using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace EditorPanelExample.Views.Controls
{
    public class TextBoxClearButtonInsideTemplate : TemplatedControl
    {
        public static readonly StyledProperty<string> MyTextProperty =
            AvaloniaProperty.Register<TextBoxClearButtonInsideTemplate, string>(nameof(MyText), $"<{nameof(MyText)}>", false, BindingMode.TwoWay);

        public static readonly StyledProperty<ICommand> MyClearCommandProperty =
            AvaloniaProperty.Register<TextBoxClearButtonInsideTemplate, ICommand>(nameof(MyClearCommand));


        public string MyText
        {
            get => GetValue(MyTextProperty);
            set => SetValue(MyTextProperty, value);
        }

        public ICommand MyClearCommand
        {
            get => GetValue(MyClearCommandProperty);
            set => SetValue(MyClearCommandProperty, value);
        }
    }
}
