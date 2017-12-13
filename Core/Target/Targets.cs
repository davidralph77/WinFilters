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
using Core.Filtering.Core;

namespace Core.Targeting
{
    /// <summary>
    /// Target class. Includes all Target objects used throughout the project.
    /// </summary>
    public sealed class Targets
    {
        public Targets() { }

        #region Drivers

        // All drivers are instantiated here exclusively.

        /// <summary>
        /// Tweeter target object
        /// </summary>
        public Target tweeter = new Target()
        {
            splMagTag = "targetTwtrSPL",
            splPhaseTag = "targetTwtrPhase",
            splMagColor = Color.Gray,
            splPhaseColor = Color.LightGray,
            title1 = "Tgt SPL",
            title2 = "Tgt Phase",
            fullTypeHP = AcousticTargets.no_High_Pass_Target,
            fullTypeLP = AcousticTargets.no_Low_Pass_Target,
            orderHP = 1,
            orderLP = 1,
            section = FilterCore.Section.HP,
            frequencyHP = 2000,
            targetMag = 90
        };

        /// <summary>
        /// Midrange target object #1
        /// </summary>
        public Target midrange1 = new Target()
        {
            splMagTag = "targetMid1SPL",
            splPhaseTag = "targetMid1Phase",
            splMagColor = Color.Gray,
            splPhaseColor = Color.LightGray,
            impMagColor = Color.Cyan,
            impPhaseColor = Color.LightCyan,
            title1 = "Tgt SPL",
            title2 = "Tgt Phase",
            fullTypeHP = AcousticTargets.no_High_Pass_Target,
            fullTypeLP = AcousticTargets.no_Low_Pass_Target,
            orderHP = 1,
            orderLP = 1,
            section = FilterCore.Section.BP,
            frequencyHP = 350,
            frequencyLP = 2000,
            targetMag = 90
        };

        /// <summary>
        /// Midrange target object #2
        /// </summary>
        public Target midrange2 = new Target()
        {
            splMagTag = "targetMid2SPL",
            splPhaseTag = "targetMid2Phase",
            splMagColor = Color.Gray,
            splPhaseColor = Color.LightGray,
            impMagColor = Color.Cyan,
            impPhaseColor = Color.LightCyan,
            title1 = "Tgt SPL",
            title2 = "Tgt Phase",
            fullTypeHP = AcousticTargets.no_High_Pass_Target,
            fullTypeLP = AcousticTargets.no_Low_Pass_Target,
            orderHP = 1,
            orderLP = 1,
            section = FilterCore.Section.BP,
            frequencyHP = 350,
            frequencyLP = 2000,
            targetMag = 90
        };

        /// <summary>
        /// Woofer target object #1
        /// </summary>
        public Target woofer1 = new Target()
        {
            splMagTag = "targetWfr1SPL",
            splPhaseTag = "targetWfr1Phase",
            splMagColor = Color.Gray,
            splPhaseColor = Color.LightGray,
            impMagColor = Color.Cyan,
            impPhaseColor = Color.LightCyan,
            title1 = "Tgt SPL",
            title2 = "Tgt Phase",
            fullTypeHP = AcousticTargets.no_High_Pass_Target,
            fullTypeLP = AcousticTargets.no_Low_Pass_Target,
            orderHP = 1,
            orderLP = 1,
            section = FilterCore.Section.LP,
            frequencyLP = 350,
            targetMag = 90
        };

        /// <summary>
        /// Woofer target object #2
        /// </summary>
        public Target woofer2 = new Target()
        {
            splMagTag = "targetWfr2SPL",
            splPhaseTag = "targetWfr2Phase",
            splMagColor = Color.Gray,
            splPhaseColor = Color.LightGray,
            impMagColor = Color.Cyan,
            impPhaseColor = Color.LightCyan,
            title1 = "Tgt SPL",
            title2 = "Tgt Phase",
            fullTypeHP = AcousticTargets.no_High_Pass_Target,
            fullTypeLP = AcousticTargets.no_Low_Pass_Target,
            orderHP = 1,
            orderLP = 1,
            section = FilterCore.Section.LP,
            frequencyLP = 350,
            targetMag = 90
        }; 
        #endregion
         
    }
}
