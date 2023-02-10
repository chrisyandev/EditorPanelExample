﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Models
{
    public class Transform : Component
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Transform(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}