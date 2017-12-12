using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFilters
{
    partial class WinFilters : Form 
    {
        partial void InitTooltips(ToolTip toolTip)
        {
            // Set up the delays for the ToolTip.
            toolTip.AutomaticDelay = 5000;
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 200;
            // Force the ToolTip text to be displayed whether or not the form is active.
            //toolTip.ShowAlways = true;
            //toolTip.UseFading = true;

            // Set up the ToolTip text
            toolTip.SetToolTip(systemGraph, "Right click for a full context menu.\nScroll wheel changes both scales.\nClick & Drag mouse to show an area to zoom.");
        }
    }
}
