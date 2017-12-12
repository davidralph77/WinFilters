using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZedGraph;
using System.IO;
using System.Collections;
using System.Numerics;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;


namespace Core.Graphing
{
    class GraphFiles
    {
        public GraphFiles() { }

        public static ColorSymbolRotator splColor = new ColorSymbolRotator();

        public static Color currentColor = Color.Black;

        /// <summary>
        /// PopulateSPL is used to add/update a graph for the SPL magnitude and phase curves of a driver.
        /// The system graph is updated as well.
        /// This one is the for interactive user query.
        /// </summary>
        /// <param name="systemZGC"></param>
        /// <param name="dialog"></param>
        public static bool PopulateSPL(ZedGraphControl systemZGC, OpenFileDialog dialog, double[] frequencies)
        {
            switch (dialog.ShowDialog())
            {
                default: return false;

                case DialogResult.OK:
                    {
                        if (dialog.CheckFileExists == true)
                        {
                            if (AutoPopulateSPL(systemZGC, dialog.FileName, frequencies))
                            { return true; }
                            else
                            { return false; }
                        }
                        else // File not exist
                        {
                            MessageBox.Show("File not found, cannot continue.", dialog.FileName);
                            return false;
                        }
                    }
                case DialogResult.Cancel:
                    {
                        return false;
                    }
            }
        }

        /// <summary>
        /// AutoPopulateSPL is used to add/update a graph for the SPL magnitude and phase curves of a driver.
        /// The system graph is updated as well.
        /// This one uses the text in the filename box to auto-populate the driver files on session file read.
        /// </summary>
        /// <param name="systemZGC"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static bool AutoPopulateSPL(ZedGraphControl systemZGC, string filename, double[] frequencies)
        {
            Zed zedObject = new Zed(); // Created only for access to the methods
            //ColorSymbolRotator splColor = new ColorSymbolRotator();

            if (!File.Exists(filename)) { return false; }
            zedObject.ImportSPLData(filename, frequencies); // Read the data, convert to double and interpolate
            zedObject.filename = filename;

            if (zedObject.splInterpolated == null)
            {
                MessageBox.Show("No valid data found, cannot continue.\nTry another file.", filename);
                return false; // Can't go further, but don't need to stop. User can try another file.
            }

            systemZGC.ZoomOutAll(systemZGC.GraphPane);

            // Invoke the method to add the curves to the graph. 
            Curves.AddComplexCurves(systemZGC, zedObject.splInterpolated, currentColor, filename, "Mag", "Phase", false);
            currentColor = splColor.NextColor;
            return true;
        }

    }
}
