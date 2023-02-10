using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditorPanelExample.Models;
using ReactiveUI;

namespace EditorPanelExample.ViewModels
{
    public class TransformViewModel : ViewModelBase, ICollapsible
    {
        private bool _isCollapsed;

        private Transform _transform;

        public bool IsCollapsed
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
        }

        public TransformViewModel(Transform transform)
        {
            _transform = transform;
        }

        public Transform Transform
        {
            get => _transform;
            set => this.RaiseAndSetIfChanged(ref _transform, value);
        }

        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }

        public void ClearTransform()
        {
            Transform = new Transform(0, 0, 0);
        }
    }
}
