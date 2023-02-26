using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EditorPanelExample.ViewModels
{
    public abstract class MyComponentBase : ViewModelBase, IMyCollapsible, IMyRemovable
    {
        public abstract bool IsCollapsed { get; set; }
        public abstract ICommand RemoveComponentCommand { get; set; }
        public abstract ICommand MoveUpCommand { get; set; }
        public abstract ICommand MoveDownCommand { get; set; }

        public abstract void ToggleCollapse();
    }
}
