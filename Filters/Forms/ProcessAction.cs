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
using System.Windows.Forms;
using WinFilters.Data;
using Core.Data;
using Core.DriverCore;
using Core.Filtering;
using Core.Filtering.Core;
using Core.Graphing;

namespace WinFilters
{
    public partial class WinFilters : Form
    {

        private void ProcessAction(DriverCore driver, double FcLP, double FcHP, Color color, string splText, string phaseText)
        {
            double splAtFcLP;
            double phaseAtFcLP;
            double splAtFcHP;
            double phaseAtFcHP;
            double factoredFcLP;
            double factoredFcHP;

            Filters.Calculate(driver, FrequencyRange.dynamicFreqs
                            , FcLP, out splAtFcLP, out phaseAtFcLP
                            , FcHP, out splAtFcHP, out phaseAtFcHP
                            , out factoredFcLP, out factoredFcHP
                            , hpIsInverted, offsetMm, lpIsOffset, hpIsOffset);
            Curves.AddTargetComplexCurves(systemGraph, driver
                , color, splText, splText, phaseText
                , FcLP, splAtFcLP, phaseAtFcLP
                , FcHP, splAtFcHP, phaseAtFcHP);

            if (driver.target.section == FilterCore.Section.LP)
            {
                UpdateFactorLP(factoredFcLP);
                besselFcPhaseLP.Text = phaseAtFcLP.ToString("F0");
                FilterSPL.Instance.lowpass = driver.splInterpolated; // For graph export option
            }
            if (driver.target.section == FilterCore.Section.HP)
            {
                UpdateFactorHP(factoredFcHP);
                besselFcPhaseHP.Text = phaseAtFcHP.ToString("F0");
                FilterSPL.Instance.highpass = driver.splInterpolated; // For graph export option
            }
            Action.Update.SystemResponse(systemGraph, drivers, phaseToggleSum);
        }
    }
}
