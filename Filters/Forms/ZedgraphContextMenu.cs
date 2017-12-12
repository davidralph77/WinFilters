using System;
using System.Drawing;
using System.Windows.Forms;
using WinFilters.Data;
using Core.Graphing;
using ZedGraph;

namespace WinFilters
{
    public partial class WinFilters : Form
    {
        //public event EventHandler MenuEvent;
        static double[,] data = null;

        public static void ContextMenuRemoveDefault(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        //public static void ContextMenuRemoveDefault(ZedGraphControl control, ContextMenuStrip menuStrip)
        {
            foreach (ToolStripMenuItem item in menuStrip.Items)
            {
                if ((string)item.Tag == "set_default")
                {
                    // remove the menu item
                    menuStrip.Items.Remove(item);
                    // or, just disable the item with this
                    //item.Enabled = false; 
                    break;
                }
            }
        }

        /*
         * To edit the context menu, first subscribe to the ContextMenuBuilder event. 
         * You can do this in the Forms designer in Visual Studio by 
         *     right clicking on the ZedGraphControl in the form, 
         *     select "properties", 
         *     click the little yellow "lightning bolt" to get a list of events, 
         *     click in the empty box to the right of ContextMenuBuilder, and 
         *     hit enter. 
         * This will add ContextMenuEventHandler template to your code. 
         * 
         * If you are not using the designer, you can just add the following event to your code at the same point where you first construct the graph (typically in the Form_Load method):
         *   zedGraphControl1.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler( MyContextMenuBuilder );
         */

        public static void ContextMenuSaveSystemSPL(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            // create a new menu item
            ToolStripMenuItem item = new ToolStripMenuItem();
            // This is the user-defined Tag so you can find this menu item later if necessary
            item.Name = "systemSPL";
            item.Tag = "systemSPL";
            // This is the text that will show up in the menu
            item.Text = "Save system SPL to file";
            // Add a handler that will respond when that menu item is selected
            item.Click += new System.EventHandler(SaveGraphCurve);
            // Add the menu item to the menu
            menuStrip.Items.Add(item);
        }

        public static void ContextMenuSaveHPFilterTwtr(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            // create a new menu item
            ToolStripMenuItem item = new ToolStripMenuItem();
            // This is the user-defined Tag so you can find this menu item later if necessary
            item.Name = "outHPFltrRspToFRDTwtr";
            item.Tag = control;
            // This is the text that will show up in the menu
            item.Text = "Save highpass filter response to file";
            // Add a handler that will respond when that menu item is selected
            //item.Click += new System.EventHandler(GraphFiles.ExportCurveToFile);
            item.Click += new System.EventHandler(SaveGraphCurve); // Generic method
            // Add the menu item to the menu
            menuStrip.Items.Add(item);
        }

        public static void ContextMenuSaveLPFilterWfr(ZedGraphControl control, ContextMenuStrip menuStrip, Point mousePt, ZedGraphControl.ContextMenuObjectState objState)
        {
            // create a new menu item
            ToolStripMenuItem item = new ToolStripMenuItem();
            // This is the user-defined Tag so you can find this menu item later if necessary
            item.Name = "outLPFltrRspToFRDWfr";
            item.Tag = control;
            // This is the text that will show up in the menu
            item.Text = "Save lowpass filter response to file";
            // Add a handler that will respond when that menu item is selected
            item.Click += new System.EventHandler(SaveGraphCurve); // Generic method
            // Add the menu item to the menu
            menuStrip.Items.Add(item);
        }


        /// <summary>
        /// This is the event handler for the Zedgraph context menu print options.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void SaveGraphCurve(object sender, EventArgs e)
        {
            ToolStripDropDownItem description = sender as ToolStripDropDownItem;
            string name = description.Name;

            // Provided as unused by Zedgraph. As used here, it contains the Zedraph control object.
            ZedGraphControl control = (ZedGraphControl)description.Tag;

            string fileType;
            switch (name)
            {
                case "outHPFltrRspToFRDTwtr":
                    {
                        fileType = "frd";
                        data = FilterSPL.Instance.highpass;
                        break;
                    }
                case "outLPFltrRspToFRDWfr":
                    {
                        fileType = "frd";
                        data = FilterSPL.Instance.lowpass;
                        break;
                    }
                case "outSystemResponse":
                    {
                        fileType = "frd";
                        MessageBox.Show("Not yet implemented for this selection.");
                        break;
                    }
                default:
                    {
                        fileType = null;
                        MessageBox.Show("Not yet implemented for this selection (or it is a code error).");
                        return;
                    }
            }
            //Export.CurveToFile(name, fileType, control);
            Export.CurveToFile(data, fileType, ExportSPL.Instance.nominal);
        }

    }
}
