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
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Core.Targeting;

namespace WinFilters.Action
{
    class CheckFilter
    {
        /// <summary>
        /// Intended for use with the Order ComboBox. If Linkwitz-Riley filter, Order must be even.
        /// This checks the current selection, despite the list being updated for the order selected.
        /// </summary>
        /// <param name="targetOrder"></param>
        /// <param name="targetFilter"></param>
        /// <returns></returns>
        public static bool IsFilterConsistentWithOrder(ComboBox targetOrder, ComboBox targetFilter)
        {
            int order = (Convert.ToInt32(targetOrder.Text));
            // Exclude these
            if (((targetFilter.Text.Contains("No Target"))) ||
                ((targetFilter.Text.Contains(AcousticTargets.FilterName.LinkwitzRiley.ToString())) && !(order % 2 == 0)) ||
                ((targetFilter.Text.Contains(AcousticTargets.FilterName.Bessel.ToString())) && ((order < 2) || (order > 4))) ||
                ((targetFilter.Text.Contains(AcousticTargets.FilterName.BesselFlattest.ToString())) && ((order < 2) || (order > 4))) ||
                ((targetFilter.Text.Contains(AcousticTargets.FilterName.Bessel3db.ToString())) && ((order < 2) || (order > 4))) ||
                ((targetFilter.Text.Contains(AcousticTargets.FilterName.Butterworth.ToString())) && (order > 8)) ||
                ((targetFilter.Text.Contains(AcousticTargets.FilterName.BesselPhaseMatch.ToString())) && ((order < 2) || (order > 4))))
            {
                //flash the targetFilter ComboBox here
                var stopWatch = new Stopwatch();
                stopWatch.Start();

                Color original = targetFilter.BackColor;
                bool toggle = true;
                while (stopWatch.Elapsed < TimeSpan.FromSeconds(1.5))
                {
                    if (toggle)
                    {
                        targetFilter.BackColor = Color.Red;
                        toggle = false;
                    }
                    else
                    {
                        targetFilter.BackColor = original;
                        toggle = true;
                    }
                    targetFilter.Update();
                    Thread.Sleep(250);
                }
                stopWatch.Stop();
                targetFilter.BackColor = original;
                targetFilter.Update();
                return false;
            }
            else { return true; }
        }
    }
}
