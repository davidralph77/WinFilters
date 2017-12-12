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
