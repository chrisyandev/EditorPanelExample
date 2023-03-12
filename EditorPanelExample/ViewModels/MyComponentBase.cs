using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EditorPanelExample.ViewModels
{
    public abstract class MyComponentBase : ViewModelBase
    {
        public abstract bool IsCollapsed { get; set; }
        public abstract ICommand ContextMenuSelectedCommand { get; set; }

        public abstract void ToggleCollapse();
    }
}
