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

using System;
using System.Drawing;
using Core.Filtering.Core;

namespace Core.Targeting
{
    /// <summary>
    /// Target class. Includes all basic properties of a target, initially created for reading target files.
    /// When classical targets are calculated, they should be used to populate these where appropriate. Maybe. 
    /// Since there should be no need of interpolation, direct population of the "interpolated data" may be used.
    /// </summary>
    [Serializable]
    public abstract class TargetBase
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TargetBase() { }

        /// <summary>
        /// Driver name entered by the user
        /// </summary>
        public string name;

        /// <summary>
        /// Standard T/S parameters for a driver
        /// </summary>
        //public TS_Params paramsTS;

        /// <summary>
        /// Title that is displayed on the graph for SPL Magnitude
        /// </summary>
        public string title1;

        /// <summary>
        /// Title that is displayed on the graph for SPL Phase
        /// </summary>
        public string title2;

        /// <summary>
        /// Used to identify the magnitude line displayed on the graph
        /// </summary>
        public string splMagTag;

        /// <summary>
        /// Used to identify the phase line displayed on the graph
        /// </summary>
        public string splPhaseTag;

        /// <summary>
        /// Specifies the color to use for the SPL magnitude curve
        /// </summary>
        public Color splMagColor;

        /// <summary>
        /// Specifies the color to use for the SPL phase curve
        /// </summary>
        public Color splPhaseColor;

        /// <summary>
        /// Used to identify the magnitude line displayed on the graph
        /// </summary>
        public string impMagTag;

        /// <summary>
        /// Used to identify the phase line displayed on the graph
        /// </summary>
        public string impPhaseTag;

        /// <summary>
        /// Specifies the color to use for the Impedance magnitude curve
        /// </summary>
        public Color impMagColor;

        /// <summary>
        /// Specifies the color to use for the Impedance phase curve
        /// </summary>
        public Color impPhaseColor;

        /// <summary>
        /// Used to include/exclude the target curves on the graph for display.
        /// </summary>
        public bool isVisible = false;

        public int orderHP;
        public int orderLP;

        public AcousticTargets.FilterName nameHP;
        public AcousticTargets.FilterName nameLP;

        /// <summary>
        /// Crossover type, e.g. Linkwitz-Riley, Butterworth, ...
        /// </summary>
        public string fullTypeHP;
        public string fullTypeLP;

        /// <summary>
        /// HP, BP, LP
        /// </summary>
        public FilterCore.Section section;

        public double targetMag;

        public double frequencyLP;
        public double frequencyHP;

        //public double[,] groupDelayTarget;
        //public string tagGroupDelayTarget;
        //public Color colorGroupDelayTarget;
    }
}
