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
