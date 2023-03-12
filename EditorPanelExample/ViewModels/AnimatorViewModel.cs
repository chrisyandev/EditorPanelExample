using EditorPanelExample.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EditorPanelExample.ViewModels
{
    public class AnimatorViewModel : MyComponentBase
    {
        private bool _isCollapsed;

        private Animator _animator;
        private string _selectedUpdateMode;
        private string _selectedCullingMode;

        public AnimatorViewModel()
        {
            _animator = new Animator();
            InitializeThisComponent();
        }

        public AnimatorViewModel(Animator animator)
        {
            _animator = animator;
            InitializeThisComponent();
        }

        public string Title { get; } = "Animator";

        public List<string> ContextMenuItems { get; } = new List<string>()
        {
            "Remove Component",
            "Move Up",
            "Move Down"
        };

        public override ICommand ContextMenuSelectedCommand { get; set; }

        public override bool IsCollapsed
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
        }

        public string Controller
        {
            get => _animator.Controller;
            set
            {
                if (value == _animator.Controller) { return; }
                _animator.Controller = value;
                this.RaisePropertyChanged(nameof(Controller));

                Debug.WriteLine(_animator.Controller);
            }
        }

        public string Avatar
        {
            get => _animator.Avatar;
            set
            {
                if (value == _animator.Avatar) { return; }
                _animator.Avatar = value;
                this.RaisePropertyChanged(nameof(Avatar));

                Debug.WriteLine(_animator.Avatar);
            }
        }

        public bool ApplyRootMotion
        {
            get => _animator.ApplyRootMotion;
            set
            {
                if (value == _animator.ApplyRootMotion) { return; }
                _animator.ApplyRootMotion = value;
                this.RaisePropertyChanged(nameof(ApplyRootMotion));

                Debug.WriteLine(_animator.ApplyRootMotion);
            }
        }

        public ObservableCollection<string> UpdateModes { get; set; }

        public string SelectedUpdateMode
        {
            get => _selectedUpdateMode;
            set
            {
                _selectedUpdateMode = value;

                string updateModeNoSpaces = string.Concat(value.Split(' '));
                bool enumParsed = Enum.TryParse(updateModeNoSpaces, true, out UpdateMode result);
                if (!enumParsed || result == _animator.CurrentUpdateMode) { return; }
                _animator.CurrentUpdateMode = result;
                this.RaisePropertyChanged(nameof(SelectedUpdateMode));

                Debug.WriteLine(_animator.CurrentUpdateMode);
            }
        }

        public ObservableCollection<string> CullingModes { get; set; }

        public string SelectedCullingMode
        {
            get => _selectedCullingMode;
            set
            {
                _selectedCullingMode = value;

                string cullingModeNoSpaces = string.Concat(value.Split(' '));
                bool enumParsed = Enum.TryParse(cullingModeNoSpaces, true, out CullingMode result);
                if (!enumParsed || result == _animator.CurrentCullingMode) { return; }
                _animator.CurrentCullingMode = result;
                this.RaisePropertyChanged(nameof(SelectedCullingMode));

                Debug.WriteLine(_animator.CurrentCullingMode);
            }
        }

        private void InitializeThisComponent()
        {
            UpdateModes = new(
                Enum.GetNames<UpdateMode>()
                .Select(
                    name => string.Join(' ', new Regex(@"(?=[A-Z])").Split(name))
                ));
            SelectedUpdateMode = UpdateModes.FirstOrDefault();

            CullingModes = new(
                Enum.GetNames<CullingMode>()
                .Select(
                    name => string.Join(' ', new Regex(@"(?=[A-Z])").Split(name))
                ));
            SelectedCullingMode = CullingModes.FirstOrDefault();
        }

        public override void ToggleCollapse()
        {
            IsCollapsed = !IsCollapsed;
        }

        public void ClearController()
        {
            Controller = "";
        }

        public void ClearAvatar()
        {
            Avatar = "";
        }
    }
}
