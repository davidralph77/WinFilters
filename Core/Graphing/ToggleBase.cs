using System;
using ZedGraph;
using System.Text.RegularExpressions;

namespace Core.Graphing
{
    public class ToggleBase
    {
        /// <summary>
        /// To be used if multiple curves may be involved. 
        /// This prevents alternating visible/invisible curves simultaneously.
        /// </summary>
        /// <param name="theZGC"></param>
        /// <param name="pattern"></param>
        /// <param name="isVisible"></param>
        public static void Curve(ZedGraphControl theZGC, String pattern, bool isVisible)
        {
            if (theZGC.GraphPane.CurveList.HasData())
            {
                for (int i = 0; i < theZGC.GraphPane.CurveList.Count; i++)
                {
                    // Operate only on those with the specified pattern in the tag
                    string text = theZGC.GraphPane.CurveList[i].Tag.ToString();
                    if (Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase))
                    {
                        if (isVisible)
                        {
                            theZGC.GraphPane.CurveList[i].IsVisible = true;
                            theZGC.GraphPane.CurveList[i].Label.IsVisible = true;
                        }
                        else
                        {
                            theZGC.GraphPane.CurveList[i].IsVisible = false;
                            theZGC.GraphPane.CurveList[i].Label.IsVisible = false;
                        }
                    }
                }
                theZGC.Refresh();
            }
        }

        /// <summary>
        /// ToggleLegend makes the legend area of the specified ZGC visible/invisible
        /// </summary>
        /// <param name="theZGC"></param>
        /// <param name="isVisible"></param>
        /// <returns></returns>
        public static bool Legend(ZedGraphControl theZGC, bool isVisible)
        {
            if (theZGC.GraphPane.CurveList.HasData())
            {
                if (isVisible)
                {
                    theZGC.GraphPane.Legend.IsVisible = false;
                }
                else
                {
                    theZGC.GraphPane.Legend.IsVisible = true;
                }
            }
            theZGC.Refresh();
            return theZGC.GraphPane.Legend.IsVisible;
        }

        /// <summary>
        /// Used to remove curves of a driver that is not part of the crossover type.
        /// Primarily for the "second" mid and/or woofer.
        /// </summary>
        /// <param name="theZGC"></param>
        /// <param name="pattern"></param>
        public static void DeleteCurve(ZedGraphControl theZGC, String pattern)
        {
            if (theZGC.GraphPane.CurveList.HasData())
            {
                for (int i = 0; i < theZGC.GraphPane.CurveList.Count; i++)
                {
                    // Operate only on those with the specified pattern in the tag
                    string text = theZGC.GraphPane.CurveList[i].Tag.ToString();
                    if (Regex.IsMatch(text, pattern, RegexOptions.IgnoreCase))
                    {
                        theZGC.GraphPane.CurveList[i].Clear();
                        theZGC.GraphPane.CurveList[i].Label.IsVisible = false;
                    }
                }
                theZGC.Refresh();
            }
        }
    }
}
