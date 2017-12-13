/* 
 Copyright <2017> <David L Ralph>

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies 
of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS 
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER 
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using Core.Data;
using Core.Filtering.Core;
using ZedGraph;

namespace Core.Graphing
{
    public static class Curves
    {
        /// <summary>
        /// Driver SPL/Phase Curves. 
        /// These were designed because of the desire to have a graph point precisely at the Fc.
        /// </summary>
        public static void AddDriverComplexCurves(ZedGraphControl myZGC, DriverCore.DriverCore driver
                                            , Color nextColor, string curveName, string primaryTag, string secondaryTag
                                            , double FcLP, double splAtFcLP, double phaseAtFcLP
                                            , double FcHP, double splAtFcHP, double phaseAtFcHP
                                            , bool curveIsVisible = true)
        {
            if (null == driver.splInterpolated) { return; }

            double minInterpFreq = FrequencyRange.dynamicFreqs[0];

            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            bool magIsVisible = true;

            double x = new double(); // Holder for frequency data
            double y = new double(); // Holder for magnitude data
            double y2 = new double();// Holder for phase data or group delay data

            // First remove the old SPL and Phase curves
            int curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(primaryTag);
            if (curveIndex != -1)
            {
                magIsVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }
            curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(secondaryTag);
            if (curveIndex != -1)
            {
                curveIsVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }
            myZGC.AxisChange(); // Trigger the driver graph update
            
            // Build the curve from data, effectively each frequency point.
            for (int i = 0; i < (FrequencyRange.dynamicFreqs.Count() - 1); i++)
            {
                //x = FrequencyRange.dynamicFreqs[i];  // Interpolation frequency point
                x = driver.splInterpolated[i, 0];  // Interpolation frequency point
                y = driver.splInterpolated[i, 1];  // Magnitude (SPL or impedance)
                y2 = driver.splInterpolated[i, 2]; // Phase (SPL or impedance) OR Group Delay
                // Don't want any points below 10Hz (or whatever the low limit becomes)
                if (x < minInterpFreq) { continue; }
                if (y != 0)
                {
                    list1.Add(x, y);
                }
                if (y2 != 0)
                {
                    list2.Add(x, y2);
                }

                if (primaryTag.Contains("Sum")) { continue; } // No filter sections, skip the rest

                // Add the points precisely at Fc, LP and/or HP (both for BP)
                if (i >= FrequencyRange.dynamicFreqs.Count() - 1) { continue; } // Don't want to crash on the last pass

                if ((driver.target.section == FilterCore.Section.LP) || (driver.target.section == FilterCore.Section.BP))
                {
                    if ((driver.splInterpolated[i, 0] < FcLP) && (driver.splInterpolated[i + 1, 0] > FcLP))
                    {
                        list1.Add(FcLP, splAtFcLP);
                        list2.Add(FcLP, phaseAtFcLP);
                    }
                }
                if ((driver.target.section == FilterCore.Section.HP) || (driver.target.section == FilterCore.Section.BP))
                {
                    if ((driver.splInterpolated[i, 0] < FcHP) && (driver.splInterpolated[i + 1, 0] > FcHP) && !(primaryTag.Contains("Sum")))
                    {
                        list1.Add(FcHP, splAtFcHP);
                        list2.Add(FcHP, phaseAtFcHP);
                    }
                }
            }

            {
                // Add the magnitude curve to the specified graph.
                Color color = nextColor;
                LineItem curve1 = myZGC.GraphPane.AddCurve(primaryTag, list1, color, SymbolType.None);
                curve1.Label.Text = curveName;
                curve1.IsSelectable = true;
                curve1.Line.Width = 1.6F;
                curve1.Symbol.Fill = new Fill(Color.White);
                curve1.Symbol.Size = 5;
                curve1.Tag = primaryTag;
                curve1.Line.IsSmooth = true;
                curve1.Line.IsAntiAlias = true;
                curve1.Line.SmoothTension = 0.0F;
                curve1.IsVisible = magIsVisible;
                curve1.Label.IsVisible = true;

                // Add the phase curve to the specified graph if optional is present
                //if (secondaryTag != String.Empty)
                {
                    LineItem curve2 = myZGC.GraphPane.AddCurve(secondaryTag, list2, color, SymbolType.None);
                    curve2.Label.IsVisible = false;
                    curve2.IsSelectable = true;
                    curve2.IsY2Axis = true;
                    curve2.Line.Width = 1.0F;
                    curve2.Line.IsSmooth = true;
                    curve2.Line.IsAntiAlias = true;
                    curve2.Line.SmoothTension = 0.0F;
                    curve2.Line.Style = DashStyle.Dash;
                    curve2.Symbol.Fill = new Fill(Color.White);
                    curve2.Symbol.Size = 5;
                    curve2.Tag = secondaryTag;
                    //curve2.Line.SmoothTension = 0.1F;
                    curve2.IsVisible = curveIsVisible;
                    curve2.Label.IsVisible = true;
                }
            }
        }

        /// <summary>
        /// Sum SPL/Phase Curves
        /// </summary>
        public static void AddSummedComplexCurves(ZedGraphControl myZGC, double[,] splInterpolated
                                            , Color nextColor, string curveName, string primaryTag, string secondaryTag
                                            , double FcLP, double splAtFcLP, double phaseAtFcLP
                                            , double FcHP, double splAtFcHP, double phaseAtFcHP
                                            , bool curveIsVisible = true)
        {
            if (null == splInterpolated) { return; }

            double minInterpFreq = FrequencyRange.dynamicFreqs[0];

            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            bool magIsVisible = true;

            double x = new double(); // Holder for frequency data
            double y = new double(); // Holder for magnitude data
            double y2 = new double();// Holder for phase data or group delay data

            // First remove the old SPL and Phase curves
            int curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(primaryTag);
            if (curveIndex != -1)
            {
                magIsVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }
            curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(secondaryTag);
            if (curveIndex != -1)
            {
                curveIsVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }
            myZGC.AxisChange(); // Trigger the driver graph update
            
            // Build the curve from data, effectively each frequency point.
            for (int i = 0; i < (FrequencyRange.dynamicFreqs.Count() - 1); i++)
            {
                x = splInterpolated[i, 0];  // Interpolation frequency point
                y = splInterpolated[i, 1];  // Magnitude (SPL or impedance)
                y2 = splInterpolated[i, 2]; // Phase (SPL or impedance) OR Group Delay
                // Don't want any points below 10Hz (or whatever the low limit becomes)
                if (x < minInterpFreq) { continue; }
                if (y != 0)
                {
                    list1.Add(x, y);
                }
                if (y2 != 0)
                {
                    list2.Add(x, y2);
                }

                if (primaryTag.Contains("Sum")) { continue; } // No filter sections, skip the rest

                // Add the points precisely at Fc, LP and/or HP (both for BP)
                if (i >= FrequencyRange.dynamicFreqs.Count() - 1) { continue; } // Don't want to crash on the last pass
            }

            {
                // Add the magnitude curve to the specified graph.
                Color color = nextColor;
                LineItem curve1 = myZGC.GraphPane.AddCurve(primaryTag, list1, color, SymbolType.None);
                curve1.Label.Text = curveName;
                curve1.IsSelectable = true;
                curve1.Line.Width = 1.6F;
                curve1.Symbol.Fill = new Fill(Color.White);
                curve1.Symbol.Size = 5;
                curve1.Tag = primaryTag;
                curve1.Line.IsSmooth = true;
                curve1.Line.IsAntiAlias = true;
                curve1.Line.SmoothTension = 0.0F;
                curve1.IsVisible = magIsVisible;
                curve1.Label.IsVisible = true;

                // Add the phase curve to the specified graph if optional is present
                //if (secondaryTag != String.Empty)
                {
                    LineItem curve2 = myZGC.GraphPane.AddCurve(secondaryTag, list2, color, SymbolType.None);
                    curve2.Label.IsVisible = false;
                    curve2.IsSelectable = true;
                    curve2.IsY2Axis = true;
                    curve2.Line.Width = 1.0F;
                    curve2.Line.IsSmooth = true;
                    curve2.Line.IsAntiAlias = true;
                    curve2.Line.SmoothTension = 0.0F;
                    curve2.Line.Style = DashStyle.Dash;
                    curve2.Symbol.Fill = new Fill(Color.White);
                    curve2.Symbol.Size = 5;
                    curve2.Tag = secondaryTag;
                    //curve2.Line.SmoothTension = 0.1F;
                    curve2.IsVisible = curveIsVisible;
                    curve2.Label.IsVisible = true;
                }
            }
        }
        
        /// <summary>
        ///  Driver Target Curves
        /// </summary>
        public static void AddTargetComplexCurves(ZedGraphControl myZGC, DriverCore.DriverCore driver
                                   , Color nextColor, string curveName, string primaryTag, string secondaryTag
                                   , double FcLP, double splAtFcLP, double phaseAtFcLP
                                   , double FcHP, double splAtFcHP, double phaseAtFcHP
                                   , bool curveIsVisible = true)
        {
            if (null == driver.target.splInterpolated) { return; }

            double minInterpFreq = FrequencyRange.dynamicFreqs[0];

            PointPairList list1 = new PointPairList();
            PointPairList list2 = new PointPairList();
            bool magIsVisible = true;

            double x = new double(); // Holder for frequency data
            double y = new double(); // Holder for magnitude data
            double y2 = new double();// Holder for phase data or group delay data

            // First remove the old SPL and Phase curves
            int curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(primaryTag);
            if (curveIndex != -1)
            {
                magIsVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }
            curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(secondaryTag);
            if (curveIndex != -1)
            {
                curveIsVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }
            myZGC.AxisChange(); // Trigger the driver graph update
            
            // Build the curve from data, effectively each frequency point.
            for (int i = 0; i < (FrequencyRange.dynamicFreqs.Count() - 1); i++)
            {
                //x = FrequencyRange.dynamicFreqs[i];  // Interpolation frequency point
                x = driver.target.splInterpolated[i, 0];  // Interpolation frequency point
                y = driver.target.splInterpolated[i, 1];  // Magnitude (SPL or impedance)
                y2 = driver.target.splInterpolated[i, 2]; // Phase (SPL or impedance) OR Group Delay
                // Don't want any points below 10Hz (or whatever the low limit becomes)
                if (x < minInterpFreq) { continue; }
                if (y != 0)
                {
                    list1.Add(x, y);
                }
                if (y2 != 0)
                {
                    list2.Add(x, y2);
                }

                if (primaryTag.Contains("Sum")) { continue; } // No filter sections, skip the rest

                // Add the points precisely at Fc, LP and/or HP (both for BP)
                if (i >= FrequencyRange.dynamicFreqs.Count() - 1) { continue; } // Don't want to crash on the last pass

                if ((driver.target.section == FilterCore.Section.LP) || (driver.target.section == FilterCore.Section.BP))
                {
                    if ((driver.target.splInterpolated[i, 0] < FcLP) && (driver.target.splInterpolated[i + 1, 0] > FcLP))
                    {
                        list1.Add(FcLP, splAtFcLP);
                        list2.Add(FcLP, phaseAtFcLP);
                    }
                }
                if ((driver.target.section == FilterCore.Section.HP) || (driver.target.section == FilterCore.Section.BP))
                {
                    if ((driver.target.splInterpolated[i, 0] < FcHP) && (driver.target.splInterpolated[i + 1, 0] > FcHP) && !(primaryTag.Contains("Sum")))
                    {
                        list1.Add(FcHP, splAtFcHP);
                        list2.Add(FcHP, phaseAtFcHP);
                    }
                }
            }
            
            // Add the magnitude curve to the specified graph.
            Color color = nextColor;
            LineItem curve1 = myZGC.GraphPane.AddCurve(primaryTag, list1, color, SymbolType.None);
            curve1.Label.Text = curveName;
            curve1.IsSelectable = true;
            curve1.Line.Width = 1.6F;
            curve1.Symbol.Fill = new Fill(Color.White);
            curve1.Symbol.Size = 5;
            curve1.Tag = primaryTag;
            curve1.Line.IsSmooth = true;
            curve1.Line.IsAntiAlias = true;
            curve1.Line.SmoothTension = 0.0F;
            curve1.IsVisible = magIsVisible;
            curve1.Label.IsVisible = true;

            // Add the phase curve to the specified graph.
            LineItem curve2 = myZGC.GraphPane.AddCurve(secondaryTag, list2, color, SymbolType.None);
            curve2.Label.IsVisible = false;
            curve2.IsSelectable = true;
            curve2.IsY2Axis = true;
            curve2.Line.Width = 1.0F;
            curve2.Line.IsSmooth = true;
            curve2.Line.IsAntiAlias = true;
            curve2.Line.SmoothTension = 0.0F;
            curve2.Line.Style = DashStyle.Dash;
            curve2.Symbol.Fill = new Fill(Color.White);
            curve2.Symbol.Size = 5;
            curve2.Tag = secondaryTag;
            //curve2.Line.SmoothTension = 0.1F;
            curve2.IsVisible = curveIsVisible;
            curve2.Label.IsVisible = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myZGC">Zedgraph name</param>
        /// <param name="plotData">Complex data (SPL, phase or group delay)</param>
        /// <param name="nextColor">Color rotation</param>
        /// <param name="curveName">Curve name for display</param>
        /// <param name="primaryTag">Curve tag for update access</param>
        /// <param name="curveIsVisible">Toggles curve on/off</param>
        /// <param name="yMax">Maximum value to use for the display (used only for group delay) - optional</param>
        public static void AddGroupDelayCurves(ZedGraphControl myZGC, double[,] plotData
                                            , Color nextColor, string curveName, string primaryTag
                                            , bool curveIsVisible = true
                                            , double yMax = 20)
        {
            if (null == plotData) { return; }

            double minInterpFreq = FrequencyRange.dynamicFreqs[0];
            PointPairList list3 = new PointPairList();

            double x = new double(); // Holder for frequency data
            double y2 = new double();// Holder for phase data or group delay data

            int curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(primaryTag); // Primary tag field must be used for the delay
            if (curveIndex != -1)
            {
                curveIsVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }

            myZGC.AxisChange(); // Trigger the driver graph update


            double xMin = 999, xMax = 0;
            // Build the curve from data, effectively each frequency point.
            for (int i = 0; i < (FrequencyRange.dynamicFreqs.Count() - 1); i++)
            {
                x = plotData[i, 0];  // Interpolation frequency point
                y2 = plotData[i, 2]; // Phase (SPL or impedance) OR Group Delay
                // Don't want any points below 10Hz (or whatever the low limit becomes)
                if (x < minInterpFreq) { continue; }
                if (y2 != 0)
                {
                    list3.Add(x, y2);
                    x = (x < xMin) ? xMin = x : x;
                    x = (x > xMax) ? xMax = x : x;
                }
            }

            // Add the group delay curve to the specified graph.
            Color color = nextColor;
            //yMax = 14.653654631696744; // Calculated value for the peak of the 1K Butterworth 8th order as a reference.
            PointPairList normalizedList = ScaleToY2(list3, (360.0 / yMax));

            //double peak = 0;
            for (int i = 0; i < normalizedList.Count; i++)
            {
                // First find the maximum element value
                //if (list3[i].Y > peak) { peak = (int)(list3[i].Y) + 1; }
                normalizedList[i].Y -= 180.0;
            }
            //myZGC.GraphPane.Y2AxisList[1].Scale.Max = peak;
            myZGC.GraphPane.Y2AxisList[1].Scale.Max = yMax;

            LineItem curve3 = myZGC.GraphPane.AddCurve(primaryTag, normalizedList, color, SymbolType.None);
            curve3.Label.Text = curveName;
            curve3.IsSelectable = true;
            curve3.Line.Width = 1.5F;
            curve3.Symbol.Fill = new Fill(Color.White);
            curve3.Symbol.Size = 5;
            curve3.Tag = primaryTag;
            curve3.Line.IsSmooth = true;
            curve3.Line.IsAntiAlias = true;
            curve3.Line.SmoothTension = 0.0F;
            curve3.IsVisible = curveIsVisible;
            curve3.Label.IsVisible = true;

            curve3.IsY2Axis = true;
            curve3.YAxisIndex = 2; // Make it track to the Y2 scale *
            //curve3.GetRange(out xMin, out xMax, out yMin, out yMax, false, false, myZGC.GraphPane);

            myZGC.AxisChange();
            myZGC.Refresh(); // Refresh to paint the graph components
        }

        private static PointPairList ScaleToY2(PointPairList list, double ratio)
        {
            PointPairList newList = new PointPairList();
            for (int i = 0; i <= list.Count() - 1; i++)
            {
                newList.Add(list[i].X, list[i].Y * ratio);
            }
            return newList;
        }

        public static void AddFcLines(ZedGraphControl myZGC, double Fc, Color color, string fcTag, bool fcTagIsVisible = true)
        {
            PointPairList list1 = new PointPairList();
            bool isVisible = true;

            double minInterpFreq = FrequencyRange.dynamicFreqs[0];

            // First remove the old Fc line
            int curveIndex;
            curveIndex = myZGC.GraphPane.CurveList.IndexOfTag(fcTag);
            if (curveIndex != -1)
            {
                isVisible = myZGC.GraphPane.CurveList[curveIndex].IsVisible;
                myZGC.GraphPane.CurveList.RemoveAt(curveIndex);
            }
            myZGC.AxisChange(); // Trigger the driver graph update

           
            if (Fc < 0) { return; } // do nothing

            // Add the magnitude curve to the specified graph.
            LineItem curve1 = myZGC.GraphPane.AddCurve(fcTag, list1, color, SymbolType.None);
            curve1.Label.Text = "";
            curve1.IsSelectable = true;
            curve1.Line.Width = 1F;
            curve1.Symbol.Fill = new Fill(Color.White);
            curve1.Symbol.Size = 5;
            curve1.Tag = fcTag;
            curve1.Line.IsSmooth = true;
            curve1.Line.SmoothTension = 0.1F;// Changed due to anomolie with magnitude data equal zero
            curve1.IsVisible = isVisible;
            curve1.Label.IsVisible = false;

            curve1.Line.DashOn  = 5;
            curve1.Line.DashOff = 5;
            
            int i = myZGC.GraphPane.AddYAxis("");
            myZGC.GraphPane.YAxisList[i].Color = Color.Gray;
            myZGC.GraphPane.YAxisList[i].Scale.IsVisible = false;
            myZGC.GraphPane.YAxisList[i].MajorTic.IsAllTics = false;
            myZGC.GraphPane.YAxisList[i].MinorTic.IsAllTics = false;
            myZGC.GraphPane.YAxisList[i].Cross = Fc;


            myZGC.AxisChange();
            myZGC.Refresh(); // Refresh to paint the graph components
        }
    }
}
