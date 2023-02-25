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
    public class TransformViewModel : ViewModelBase, IMyCollapsible
    {
        private bool _isCollapsed;

        private Transform _transform;

        public TransformViewModel()
        {
            _transform = new Transform();
        }

        public TransformViewModel(Transform transform)
        {
            _transform = transform;
        }

        public string Description { get; } = "Placeholder for description of Transform";

        public List<string> ContextMenuItems { get; } = new List<string>()
        {
            "Remove Component",
            "Move Up",
            "Move Down"
        };

        public bool IsCollapsed
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
        }

        public float X
        {
            get => _transform.X;
            set
            {
                if (value == _transform.X) { return; }
                _transform.X = value;
                this.RaisePropertyChanged(nameof(X));

                Debug.WriteLine(_transform.X);
            }
        }

        public float Y
        {
            get => _transform.Y;
            set
            {
                if (value == _transform.Y) { return; }
                _transform.Y = value;
                this.RaisePropertyChanged(nameof(Y));

                Debug.WriteLine(_transform.Y);
            }
        }

        public float Z
        {
            get => _transform.Z;
            set
            {
                if (value == _transform.Z) { return; }
                _transform.Z = value;
                this.RaisePropertyChanged(nameof(Z));

                Debug.WriteLine(_transform.Z);
            }
        }

        public void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }

        public void ClearTransform()
        {
            X = 0;
            Y = 0;
            Z = 0;
        }
    }
}
