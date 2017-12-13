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

using System.Drawing;

namespace Core.Graphing
{
    /// <summary>
    /// Base class with properties for graph display
    /// </summary>
    public class BaseGraph
    {
        /// <summary>
        /// Name to be displayed in the legend (may be unneeded)
        /// </summary>
        public string name;

        /// <summary>
        /// Magnitude complex data. [frequency, magnitude]
        /// </summary>
        public double[,] magInterpolated;

        /// <summary>
        ///  Phase complex data
        /// </summary>
        public double[,] phaseInterpolated;

        /// <summary>
        /// Name to be displayed in the legend
        /// </summary>
        public string title1;

        /// <summary>
        /// Tag used for identifying curve object in zedgraph
        /// </summary>
        public string magTag;

        /// <summary>
        /// Color of the magnitude curve
        /// </summary>
        public Color magColor;

        /// <summary>
        /// Tag used for identifying curve object in zedgraph (if there is one)
        /// </summary>
        public string phaseTag;

        /// <summary>
        /// Color of the phase curve (if there is one)
        /// </summary>
        public Color phaseColor;

        public bool isVisible = false;
    }
}
