using System.Drawing;

namespace Core.Graphing
{
    /// <summary>
    /// Base class with properties for graph display
    /// </summary>
    public class BaseCurve
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
