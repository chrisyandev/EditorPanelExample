using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Styling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Views.Components.Common
{
    public sealed class AddComponentListBox : ListBox, IStyleable
    {
        Type IStyleable.StyleKey => typeof(ListBox);

        public event EventHandler MyItemSelected;

        private PointerPressedEventArgs _pointerPressedEventArgs;

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            _pointerPressedEventArgs = e;
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            // ListBox item is selected on pointer pressed by default. We want to select item on pointer released.
            base.OnPointerPressed(_pointerPressedEventArgs);

            MyItemSelected.Invoke(this, EventArgs.Empty);

            base.OnPointerReleased(e);
        }
    }
}
