using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditorPanelExample.Models
{
    public class EditorPanel : Draggable, Collapsible
    {
        public Component Component { get; set; }

        public EditorPanel(Component component)
        {
            Component = component;
        }

        public void Drag()
        {
            throw new NotImplementedException();
        }

        public void Collapse()
        {
            throw new NotImplementedException();
        }
    }


}
