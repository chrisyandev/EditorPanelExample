using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EditorPanelExample.Models;

namespace EditorPanelExample.ViewModels
{
    public class TransformViewModel : ViewModelBase
    {
        private readonly Transform _transform;

        public TransformViewModel(Transform transform)
        {
            _transform = transform;
        }

        public float X => _transform.X;
        public float Y => _transform.Y;
        public float Z => _transform.Z;

    }
}
