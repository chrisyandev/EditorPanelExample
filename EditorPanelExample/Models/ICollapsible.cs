﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Models
{
    internal interface ICollapsible
    {
        bool IsCollapsed { get; set; }

        void ToggleCollapse();
    }
}