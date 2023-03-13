using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EditorPanelExample.ViewModels
{
    public abstract class MyComponentBase : ViewModelBase
    {
        private bool _isCollapsed;

        public string Title { get; set; } = "<ComponentTitle>";

        public bool IsCollapsed
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
        }

        public List<string> ContextMenuItems { get; } = new List<string>()
        {
            "Remove Component",
            "Move Up",
            "Move Down"
        };

        public ICommand ContextMenuSelectedCommand { get; set; }

        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }
    }
}
