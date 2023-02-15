using EditorPanelExample.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.ViewModels
{
    public class AnimatorViewModel : ViewModelBase, ICollapsible
    {
        private bool _isCollapsed;

        private string _controller;
        private string _avatar;
        private bool _applyRootMotion;
        private UpdateMode _updateMode;
        private string _selectedUpdateMode;
        private CullingMode _cullingMode;
        private string _selectedCullingMode;

        public AnimatorViewModel(Animator animator)
        {
            _controller = animator.Controller;
            _avatar = animator.Avatar;
            _applyRootMotion = animator.ApplyRootMotion;
            _updateMode = animator.UpdateMode;
            _cullingMode = animator.CullMode;

            UpdateModes = new(Enum.GetNames<UpdateMode>());
            SelectedUpdateMode = UpdateModes.FirstOrDefault();

            CullingModes = new(Enum.GetNames<CullingMode>());
            SelectedCullingMode = CullingModes.FirstOrDefault();
        }

        public string Description { get; } = "Placeholder for description of Animator";

        public bool IsCollapsed
        {
            get => _isCollapsed;
            set => this.RaiseAndSetIfChanged(ref _isCollapsed, value);
        }

        public string Controller
        {
            get => _controller;
            set => this.RaiseAndSetIfChanged(ref _controller, value);
        }

        public string Avatar
        {
            get => _avatar;
            set => this.RaiseAndSetIfChanged(ref _avatar, value);
        }

        public bool ApplyRootMotion
        {
            get => _applyRootMotion;
            set => this.RaiseAndSetIfChanged(ref _applyRootMotion, value);
        }

        public ObservableCollection<string> UpdateModes { get; set; }

        public string SelectedUpdateMode
        {
            get => _selectedUpdateMode;
            set => this.RaiseAndSetIfChanged(ref _selectedUpdateMode, value);
        }

        public ObservableCollection<string> CullingModes { get; set; }

        public string SelectedCullingMode
        {
            get => _selectedCullingMode;
            set => this.RaiseAndSetIfChanged(ref _selectedCullingMode, value);
        }

        public void ToggleCollapse()
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

        public void ToggleApplyRootMotion()
        {
            ApplyRootMotion = !ApplyRootMotion;
        }
    }
}
