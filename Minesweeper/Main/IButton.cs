using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Main
{
    public class IButton : Button
    {
        public int x { get; set; }
        public int y { get; set; }
        public bool IsRightClicked = false;
    }
}
