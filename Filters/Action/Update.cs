using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using WinFilters.Data;
using WinFilters.Initialization;
using Core.Data;
using Core.DriverCore;
using Core.Filtering;
using Core.Filtering.Core;
using Core.Graphing;
using ZedGraph;

namespace WinFilters.Action
{
    class Update
    {
        public static void SystemResponse(ZedGraphControl systemGraph, Drivers drivers, bool phaseToggleSum)
        {
            double FcLP = 0.0;
            double FcHP = 0.0;
            double phaseAtFcLP = 0;
            double phaseAtFcHP = 0;
            double splAtFcLP = 0;
            double splAtFcHP = 0;
            double[,] groupDelayLP, groupDelayHP, groupDelaySum;
            double maxLP, maxHP, maxSum;

            double[,] sum = (new FilterCore().SumResponses(drivers.woofer1.target.splInterpolated, drivers.tweeter.target.splInterpolated));
            Curves.AddSummedComplexCurves(systemGraph, sum
                , Color.Black, "SummedSPL", "SummedSPL", "SummedPhase"
                , FcLP, splAtFcLP, phaseAtFcLP
                , FcHP, splAtFcHP, phaseAtFcHP
                , phaseToggleSum);
            // Lowpass delay
            groupDelayLP = GroupDelay.Calculate(drivers.woofer1.target.splInterpolated, out maxLP);
            // Highpass delay
            groupDelayHP = GroupDelay.Calculate(drivers.tweeter.target.splInterpolated, out maxHP);
            // System delay
            groupDelaySum = GroupDelay.Calculate(sum, out maxSum);

            var max = (maxLP > maxHP) ? maxLP : maxHP;
            Curves.AddGroupDelayCurves(systemGraph, groupDelayLP, Color.Violet, "LowpassDelay", "LowpassDelay", curveIsVisible: true, yMax: max);
            Curves.AddGroupDelayCurves(systemGraph, groupDelayHP, Color.Orange, "HighpassDelay", "HighpassDelay", curveIsVisible: true, yMax: max);
            Curves.AddGroupDelayCurves(systemGraph, groupDelaySum, Color.Green, "SystemDelay", "SystemDelay", phaseToggleSum, yMax: max);

            // Data used for graph export
            FilterSPL.Instance.lowpass = drivers.woofer1.target.splInterpolated;
            FilterSPL.Instance.highpass = drivers.tweeter.target.splInterpolated;
        }
    }
}
