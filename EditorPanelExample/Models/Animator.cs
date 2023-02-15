using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Models
{
    public enum UpdateMode
    { 
        Normal,
        AnimatePhysics,
        UnscaledTime
    }

    public enum CullingMode
    {
        AlwaysAnimate,
        CullUpdateTransforms,
        CullCompletely
    }

    public class Animator
    {
        public string Controller { get; set; }
        public string Avatar { get; set; }
        public bool ApplyRootMotion { get; set; }
        public UpdateMode UpdateMode { get; set; }
        public CullingMode CullMode { get; set; }

        public Animator(string controller, string avatar, bool applyRootMotion,
            UpdateMode updateMode = UpdateMode.Normal, CullingMode cullMode = CullingMode.CullUpdateTransforms)
        {
            Controller = controller;
            Avatar = avatar;
            ApplyRootMotion = applyRootMotion;
            UpdateMode = updateMode;
            CullMode = cullMode;
        }
    }
}
