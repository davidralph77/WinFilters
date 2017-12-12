using ZedGraph;


namespace WinFilters.Initialization
{
    class InitGraph
    {
        GraphObjList Y3AxisList = new GraphObjList();

        public static double groupDelayScaleMax = 18;

        public static void SetZedgraphFormat(ZedGraphControl myZed)
        {
            myZed.GraphPane.Title.Text = "Magnitude and Phase";
            InitSPLAxes(myZed);

            Line.Default.IsSmooth = true;
            Line.Default.SmoothTension = 1; // 1 should be maximum. IsSmooth must be true.

            Y2Axis.Default.IsVisible = true;
            Y2Axis.Default.IsZeroLine = true;

            //Add a third Y axis
            //new Y3Axis = new ZedGraph.YAxis("Group Delay (microseconds");

            myZed.AxisChange();
        }

        /// <summary>
        /// InitSPLAxes is called for each graph during intialization. It sets up all of the properties for 
        /// an SPL graph for axis titles and, more importantly, curve plotting characteristics.
        /// No data is returned.
        /// </summary>
        /// <param name="myZGC"></param>
        private static void InitSPLAxes(ZedGraphControl myZGC)
        {
            // Axis Types

            myZGC.GraphPane.XAxis.Type = AxisType.Log;
            myZGC.GraphPane.YAxis.Type = AxisType.Linear;

            myZGC.GraphPane.Legend.Position = LegendPos.BottomFlushLeft;

            // Axis Grid Settings

            myZGC.GraphPane.XAxis.Scale.IsUseTenPower = false;
            myZGC.GraphPane.XAxis.MajorGrid.IsVisible = true;
            myZGC.GraphPane.XAxis.MajorGrid.DashOff = 0;
            myZGC.GraphPane.XAxis.MinorGrid.IsVisible = true;
            myZGC.GraphPane.XAxis.MinorGrid.DashOff = 1;

            // Axis Text

            myZGC.GraphPane.XAxis.Title.Text = "Frequency (Hz)";
            myZGC.GraphPane.YAxis.Title.Text = "Gain (db)";
            myZGC.GraphPane.Y2Axis.Title.Text = "Phase (Deg)";
            myZGC.GraphPane.YAxis.Title.FontSpec.IsBold = false;
            myZGC.GraphPane.Y2Axis.Title.FontSpec.IsBold = false;
            myZGC.GraphPane.Y2Axis.Title.IsVisible = true;

            // X Axis (Frequency)

            myZGC.GraphPane.XAxis.IsVisible = true;
            myZGC.GraphPane.XAxis.IsAxisSegmentVisible = true;
            myZGC.GraphPane.XAxis.MajorGrid.IsVisible = true;
            myZGC.GraphPane.XAxis.Scale.IsVisible = true; //***
            myZGC.GraphPane.XAxis.Scale.Min = 10;
            myZGC.GraphPane.XAxis.Scale.Max = 20000;
            myZGC.GraphPane.XAxis.Scale.MaxAuto = false;
            //myZGC.GraphPane.XAxis.Scale.BaseTic = 100; // This screws up a log axis!!!!
            //myZGC.GraphPane.XAxis.Scale.AlignH = Center;
            myZGC.GraphPane.XAxis.Title.IsVisible = true;
            myZGC.GraphPane.XAxis.Title.IsOmitMag = true; //Controls display (or not) of "e" power

            // Y Axis (SPL)

            myZGC.GraphPane.YAxis.Scale.Min = -55.0;
            myZGC.GraphPane.YAxis.Scale.Max = 5.0;
            myZGC.GraphPane.YAxis.Scale.MajorStep = 5.0;
            myZGC.GraphPane.YAxis.Scale.MinorStep = 1;
            myZGC.GraphPane.YAxis.MajorTic.IsOpposite = false;
            myZGC.GraphPane.YAxis.MinorTic.IsOpposite = false;
            myZGC.GraphPane.YAxis.MajorGrid.IsVisible = true;
            myZGC.GraphPane.YAxis.MajorGrid.DashOff = 0;

            // Y2 Axis (Phase)

            myZGC.GraphPane.Y2Axis.IsVisible = true;
            myZGC.GraphPane.Y2Axis.Scale.IsVisible = true;
            myZGC.GraphPane.Y2Axis.MajorTic.IsOpposite = false;
            myZGC.GraphPane.Y2Axis.MinorTic.IsOpposite = false;
            myZGC.GraphPane.Y2Axis.Scale.Min = -180;
            myZGC.GraphPane.Y2Axis.Scale.Max = 180;
            myZGC.GraphPane.Y2Axis.Scale.MajorStep = 60;
            myZGC.GraphPane.Y2Axis.Scale.MinorStep = 30;
            
            // Y2 Axis (Group Delay)

            myZGC.GraphPane.AddY2Axis("Group Delay");
            int y3Index = myZGC.GraphPane.Y2AxisList.IndexOf("Group Delay");
            myZGC.GraphPane.Y2AxisList[y3Index].Title.Text = "Group Delay (msec)";

            myZGC.GraphPane.Y2AxisList[y3Index].Title.FontSpec.IsBold = false;
            myZGC.GraphPane.Y2AxisList[y3Index].IsVisible = true;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.IsVisible = true;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.MajorStep = 1;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.Min = 0;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.Max = groupDelayScaleMax;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.MaxAuto = true;
            //myZGC.GraphPane.Y2AxisList[y3Index].Scale.MinAuto = true;

            myZGC.GraphPane.Y2AxisList[y3Index].Scale.FormatAuto = true;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.IsUseTenPower = true;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.MagAuto = false;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.MajorStepAuto = true;

            // Manual setting of graph options not appearing in the designer listing

            myZGC.GraphPane.IsFontsScaled = false;
            myZGC.GraphPane.Title.FontSpec.Size = 12.0f;

            // Most of the following were affected by IsFontScaled set false

            myZGC.GraphPane.TitleGap = 0.8f;
            myZGC.GraphPane.Margin.Top = 6.0f;

            myZGC.GraphPane.Legend.FontSpec.Size = 9.0f;

            myZGC.GraphPane.XAxis.Title.FontSpec.Size = 12.0f;
            myZGC.GraphPane.YAxis.Title.FontSpec.Size = 12.0f;
            myZGC.GraphPane.Y2Axis.Title.FontSpec.Size = 12.0f;
            myZGC.GraphPane.Y2AxisList[y3Index].Title.FontSpec.Size = 12.0f;

            myZGC.GraphPane.XAxis.Scale.LabelGap = 0.05f;
            myZGC.GraphPane.YAxis.Scale.LabelGap = 0.05f;
            myZGC.GraphPane.Y2Axis.Scale.LabelGap = 0.05f;

            myZGC.GraphPane.XAxis.Scale.FontSpec.Size = 12.0f;
            myZGC.GraphPane.YAxis.Scale.FontSpec.Size = 12.0f;
            myZGC.GraphPane.Y2Axis.Scale.FontSpec.Size = 12.0f;
            myZGC.GraphPane.Y2AxisList[y3Index].Scale.FontSpec.Size = 12.0f;

            myZGC.GraphPane.XAxis.Scale.LabelGap = 0.1f;
            myZGC.GraphPane.YAxis.Scale.LabelGap = 0.1f;
            myZGC.GraphPane.Y2Axis.Scale.LabelGap = 0.1f;
        }

    }
}
