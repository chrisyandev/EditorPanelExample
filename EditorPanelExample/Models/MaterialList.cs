﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Models
{
    public class MaterialList : Component
    {
        public List<Material> Materials { get; set; }
        public bool Expanded { get; set; } = true;
    }
}
