using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Views.Components.Common
{
    public sealed class ComponentTitleButton : Button, IStyleable
    {
        Type IStyleable.StyleKey => typeof(Button);

        public event EventHandler<PointerPressedEventArgs> ComponentTitleButtonMouseDown;
        public event EventHandler<PointerReleasedEventArgs> ComponentTitleButtonMouseUp;

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            if (e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
                ComponentTitleButtonMouseDown?.Invoke(this, e);
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);

            if (e.InitialPressMouseButton == MouseButton.Left)
                ComponentTitleButtonMouseUp?.Invoke(this, e);
        }
    }
}
