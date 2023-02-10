using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Models
{
    public class Material : IComponent
    {
        public string MaterialName { get; set; }

        public Material(string materialName)
        {
            MaterialName = materialName;
        }
    }
}
