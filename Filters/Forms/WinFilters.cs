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
using System.Reflection;
using System.Windows.Forms;
using static WinFilters.Action.CheckFilter;
using WinFilters.Data;
using WinFilters.Initialization;
using Core.Data;
using Core.DriverCore;
using Core.Filtering;
using Core.Filtering.Core;
using Core.Graphing;
using Core.Targeting;
using ZedGraph;

namespace WinFilters
{
    public partial class WinFilters : Form
    {
        FrequencyRange frequencies = new FrequencyRange();

        Targets targets = new Targets(); // Using this gives us predefined graphing values

        Drivers drivers = new Drivers(); // Need driver objects for the setup of targets

        public WinFilters()
        {
            Targets targets = new Targets();
            FilterSPL.Initialize();

            InitializeComponent();

            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            string assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            this.Text = string.Format("{0} Version {1}.{2}.{3} Revision {4} - Windows Acoustic Filter Graphing Tool",
                                      assemblyName, version.Major, version.Minor, version.Build, version.Revision);

            systemGraph.GraphPane.Title.Text = "Magnitude and Phase";
            InitGraph.SetZedgraphFormat(systemGraph);

            // Remove the default option from all graphs
            systemGraph.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(WinFilters.ContextMenuRemoveDefault);

            // Add the save options to the specific graphs
            systemGraph.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(WinFilters.ContextMenuSaveLPFilterWfr);

            // Add the save options to the specific graphs
            systemGraph.ContextMenuBuilder += new ZedGraphControl.ContextMenuBuilderEventHandler(WinFilters.ContextMenuSaveHPFilterTwtr);

            ExportSPL.Instance.nominal = Convert.ToDouble(splNumeric.Value);

            Refresh(); // Refresh to paint the graph components

            // Create the frequency range array for use throughout the program.
//            frequencies.InitFrequencyRange();
        }

        bool startupLock = false;

        private void WinFilters_Load(object sender, EventArgs e)
        {
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            // Set up the ToolTip text for the Button and Checkbox.
            InitTooltips(toolTip1);

            startupLock = true;

            ClearTargetTypes();
            AddInitialTargetTypes();

            // Initialize the drivers as required
            targets.woofer1.frequencyLP = Convert.ToDouble(trgtFreqLP.Value);
            targets.tweeter.frequencyHP = Convert.ToDouble(trgtFreqHP.Value);
            targets.woofer1.orderLP = Convert.ToInt32(trgtOrderLP.Text);
            targets.tweeter.orderHP = Convert.ToInt32(trgtOrderHP.Text);
            drivers.woofer1.target = targets.woofer1;
            drivers.tweeter.target = targets.tweeter;
            drivers.woofer1.SetFullAcousticTargetType();
            drivers.tweeter.SetFullAcousticTargetType();

            trgtTypeLP.Text = AcousticTargets.FilterName.LinkwitzRiley.ToString();
            trgtTypeHP.Text = AcousticTargets.FilterName.LinkwitzRiley.ToString();
            CalculateAll();
            startupLock = false;
        }
  
        partial void InitTooltips(ToolTip toolTipMain);

        #region Setup

        private void ClearTargetTypes()
        {
            trgtTypeHP.Items.Clear();
            trgtTypeLP.Items.Clear();
        }

        /// <summary>
        /// Used for driver target ComboBox, but is generic and could be used for others.
        /// </summary>
        /// <param name="box">ComboBox to fill (should be cleared to start)</param>
        /// <param name="values">Array of strings to fill the box</param>
        private void UpdateFilterComboBoxList(ComboBox box, string[] values)
        {
            box.Items.AddRange(values);
        }

        // This may go away. It was originally used when the order selection updated the list with valid filter types
        private void AddInitialTargetTypes()
        {
            UpdateFilterComboBoxList(trgtTypeHP, Target.orders[AcousticTargets.Order.fourth]);
            UpdateFilterComboBoxList(trgtTypeLP, Target.orders[AcousticTargets.Order.fourth]);
            targets.tweeter.nameHP = AcousticTargets.FilterName.LinkwitzRiley;
            targets.woofer1.nameLP = AcousticTargets.FilterName.LinkwitzRiley;
            targets.tweeter.targetMag = 0.0;
            targets.woofer1.targetMag = 0.0;
            drivers.woofer1.target = targets.woofer1;
            drivers.tweeter.target = targets.tweeter;
        } 
        #endregion

        /// <summary>
        /// Used only to initialize at startup
        /// </summary>
        private void CalculateAll()
        {
            double FcLP = 1000;
            double FcHP = 1000;
            double splAtFcLP = 1;
            double phaseAtFcLP = 0;
            double splAtFcHP = 1;
            double phaseAtFcHP = 0;
            double factoredFcLP;
            double factoredFcHP;
            double[,] groupDelay;
            double max;

            // Lowpass section
            if (IsFilterConsistentWithOrder(trgtOrderLP, trgtTypeLP))
            {
                FcLP = Convert.ToDouble(trgtFreqLP.Value);
                Filters.Calculate(drivers.woofer1, FrequencyRange.dynamicFreqs
                    , FcLP, out splAtFcLP, out phaseAtFcLP
                    , FcHP, out splAtFcHP, out phaseAtFcHP
                    , out factoredFcLP, out factoredFcHP);
                FilterSPL.Instance.lowpass = drivers.woofer1.target.splInterpolated;
                Curves.AddTargetComplexCurves(systemGraph, drivers.woofer1
                    , Color.Blue, "LowpassSPL", "LowpassSPL", "LowpassPhase"
                    , FcLP, splAtFcLP, phaseAtFcLP
                    , FcHP, splAtFcHP, phaseAtFcHP);
                besselFcPhaseLP.Text = phaseAtFcLP.ToString();
                UpdateFactorLP(FcLP);
                // Lowpass delay
                groupDelay = GroupDelay.Calculate(drivers.woofer1.target.splInterpolated, out max);
                Curves.AddGroupDelayCurves(systemGraph, groupDelay, Color.Violet, "LowpassDelay", "LowpassDelay", curveIsVisible : true, yMax : max);
            }
            // Highpass section
            if (IsFilterConsistentWithOrder(trgtOrderHP, trgtTypeHP))
            {
                FcHP = Convert.ToDouble(trgtFreqHP.Value);
                Filters.Calculate(drivers.tweeter, FrequencyRange.dynamicFreqs
                                  , FcLP, out splAtFcLP, out phaseAtFcLP
                                  , FcHP, out splAtFcHP, out phaseAtFcHP
                                  , out factoredFcLP, out factoredFcHP
                                  , hpIsInverted);
                //FilterSPL.Instance.lowpass = drivers.tweeter.target.splInterpolated;
                FilterSPL.Instance.highpass = drivers.tweeter.target.splInterpolated;
                Curves.AddTargetComplexCurves(systemGraph, drivers.tweeter
                    , Color.Red, "HighpassSPL", "HighpassSPL", "HighpassPhase"
                    , FcLP, splAtFcLP, phaseAtFcLP
                    , FcHP, splAtFcHP, phaseAtFcHP);
                besselFcPhaseHP.Text = phaseAtFcHP.ToString();
                UpdateFactorHP(FcHP);
                // Highpass delay
                groupDelay = GroupDelay.Calculate(drivers.tweeter.target.splInterpolated, out max);
                Curves.AddGroupDelayCurves(systemGraph, groupDelay, Color.Orange, "HighpassDelay", "HighpassDelay", curveIsVisible : true, yMax: max);
            }
            // System sum section
            double[,] sum = (new FilterCore().SumResponses(drivers.woofer1.target.splInterpolated, drivers.tweeter.target.splInterpolated));
            Curves.AddSummedComplexCurves(systemGraph, sum
                , Color.Black, "SummedSPL", "SummedSPL", "SummedPhase"
                , FcLP, splAtFcLP, phaseAtFcLP
                , FcHP, splAtFcHP, phaseAtFcHP);
            // System delay
            groupDelay = GroupDelay.Calculate(sum, out max);
            Curves.AddGroupDelayCurves(systemGraph, groupDelay, Color.Green, "SystemDelay", "SystemDelay", curveIsVisible : true, yMax: max);
            FilterSPL.Instance.lowpass = drivers.woofer1.target.splInterpolated;
            FilterSPL.Instance.highpass = drivers.tweeter.target.splInterpolated;
        }

        #region Target Orders

        private void trgtOrderLP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startupLock) { return; }
            ComboBox cBox = sender as ComboBox;
            targets.woofer1.orderLP = Convert.ToInt32(cBox.Text);
            drivers.woofer1.target = targets.woofer1;
            drivers.woofer1.target.section = FilterCore.Section.LP;
            drivers.woofer1.SetFullAcousticTargetType();

            if (IsFilterConsistentWithOrder(trgtOrderLP, trgtTypeLP))
            {
                double FcLP = Convert.ToDouble(trgtFreqLP.Value);
                double FcHP = 0.0;
                ProcessAction(drivers.woofer1, FcLP, FcHP, Color.Blue, "LowpassSPL", "LowpassPhase");
            }
        }

        private void trgtOrderHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startupLock) { return; }
            ComboBox cBox = sender as ComboBox;
            targets.tweeter.orderHP = Convert.ToInt32(cBox.Text);
            drivers.tweeter.target = targets.tweeter;
            drivers.tweeter.target.section = FilterCore.Section.HP;
            drivers.tweeter.SetFullAcousticTargetType();

            if (IsFilterConsistentWithOrder(trgtOrderHP, trgtTypeHP))
            {
                double FcLP = 0.0;
                double FcHP = Convert.ToDouble(trgtFreqHP.Value);
                ProcessAction(drivers.tweeter, FcLP, FcHP, Color.Red, "HighpassSPL", "HighpassPhase");
            }
        } 
        #endregion

        #region Target Filter Types

        private void trgtTypeLP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startupLock) { return; }
            ComboBox cBox = sender as ComboBox;
            targets.woofer1.nameLP = Target.names[cBox.Text]; // Get it from the dictionary
            targets.woofer1.orderLP = Convert.ToInt32(trgtOrderLP.Text);

            drivers.woofer1.target = targets.woofer1;
            drivers.woofer1.target.section = FilterCore.Section.LP;
            drivers.woofer1.SetFullAcousticTargetType();

            if (IsFilterConsistentWithOrder(trgtOrderLP, trgtTypeLP))
            {
                double FcLP = Convert.ToDouble(trgtFreqLP.Value);
                double FcHP = 0;
                ProcessAction(drivers.woofer1, FcLP, FcHP, Color.Blue, "LowpassSPL", "LowpassPhase");
            }
        }

        private void trgtTypeHP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (startupLock) { return; }
            ComboBox cBox = sender as ComboBox;
            targets.tweeter.nameHP = Target.names[cBox.Text]; // Get it from the dictionary
            targets.tweeter.orderHP = Convert.ToInt32(trgtOrderHP.Text);

            drivers.tweeter.target = targets.tweeter;
            drivers.tweeter.target.section = FilterCore.Section.HP;
            drivers.tweeter.SetFullAcousticTargetType();

            if (IsFilterConsistentWithOrder(trgtOrderHP, trgtTypeHP))
            {
                double FcLP = 0;
                double FcHP = Convert.ToDouble(trgtFreqHP.Value);
                ProcessAction(drivers.tweeter, FcLP, FcHP, Color.Red, "HighpassSPL", "HighpassPhase");
            }
        } 
        #endregion

        #region Target Frequencies

        private void trgtFreqLP_ValueChanged(object sender, EventArgs e)
        {
            if (startupLock) { return; }
            NumericUpDown numeric = sender as NumericUpDown;
            targets.woofer1.frequencyLP = Convert.ToDouble(numeric.Value);
            drivers.woofer1.target = targets.woofer1;

            double FcLP = targets.woofer1.frequencyLP;
            double FcHP = 0;
            ProcessAction(drivers.woofer1, FcLP, FcHP, Color.Blue, "LowpassSPL", "LowpassPhase");
        }

        private void trgtFreqHP_ValueChanged(object sender, EventArgs e)
        {
            if (startupLock) { return; }
            NumericUpDown numeric = sender as NumericUpDown;
            targets.tweeter.frequencyHP = Convert.ToDouble(numeric.Value);
            drivers.tweeter.target = targets.tweeter;

            double FcLP = 0;
            double FcHP = targets.tweeter.frequencyHP;
            ProcessAction(drivers.tweeter, FcLP, FcHP, Color.Red, "HighpassSPL", "HighpassPhase");
        } 
        #endregion

        #region Filter Factor Group Boxes

        private void BesselFactorGroupBoxes()
        {
            if (trgtTypeLP.Text.Contains("Bessel") || trgtTypeHP.Text.Contains("Bessel"))
            {
                magGroupBox.Visible = true;
                fcGroupBox.Visible = true;
                factorGroupBox.Visible = true;
            }
            else
            {
                magGroupBox.Visible = false;
                fcGroupBox.Visible = false;
                factorGroupBox.Visible = false;
            }
        }

        private void UpdateFactorLP(double factoredFc)
        {

            if (targets.woofer1.nameLP.ToString().Contains("Bessel"))
            {
                besselFcFactorLP.Text = Convert.ToString(factoredFc / drivers.woofer1.target.frequencyLP);
            }
            else
            {
                besselFcFactorLP.Text = "1";
            }
            actualFcLP.Text = Convert.ToInt32(factoredFc).ToString();
        }

        private void UpdateFactorHP(double factoredFc)
        {

            if (targets.tweeter.nameHP.ToString().Contains("Bessel"))
            {
                besselFcFactorHP.Text = Convert.ToString(factoredFc / drivers.tweeter.target.frequencyHP);
            }
            else
            {
                besselFcFactorHP.Text = "1";
            }
            actualFcHP.Text = Convert.ToInt32(factoredFc).ToString();
        } 
        #endregion

        #region Toggle Buttons

        static bool hpIsInverted = false;
        private void invertHighpass_Click(object sender, EventArgs e)
        {
            hpIsInverted = !hpIsInverted;
            if (hpIsInverted)
            {
                invertHighpass.BackColor = Color.Red;
                invertHighpass.Text = "Tweeter -";
            }
            else
            {
                invertHighpass.BackColor = Color.LightGreen;
                invertHighpass.Text = "Tweeter +";
            }
            double FcLP = 0.0;
            double FcHP = Convert.ToDouble(trgtFreqHP.Value);
            ProcessAction(drivers.tweeter, FcLP, FcHP, Color.Red, "HighpassSPL", "HighpassPhase");
        }

        private void ToggleSum(Button thisButton, ref bool thisBoolean, string id)
        {
            thisBoolean = !thisBoolean;
            ToggleBase.Curve(systemGraph, id, thisBoolean);
            if (thisBoolean == true)
            {
                thisButton.BackColor = Color.LightGreen;
            }
            else
            {
                thisButton.BackColor = Color.LightGray;
            }
        }

        static bool splToggleSum = true;
        private void toggleSum_Click(object sender, EventArgs e)
        {
            ToggleSum(toggleSum, ref splToggleSum, "SummedSPL");
        }

        bool phaseToggleSum = true;
        private void toggleSumPhase_Click(object sender, EventArgs e)
        {
            ToggleSum(toggleSumPhase, ref phaseToggleSum, "SummedPhase");
        }

        bool delayToggleSum = true;
        private void toggleSumDelay_Click(object sender, EventArgs e)
        {
            ToggleSum(toggleSumDelay, ref delayToggleSum, "SystemDelay");
        }


        private void ToggleButtonColor(Button thisButton, ref bool thisBoolean)
        {
            thisBoolean = !thisBoolean;
            if (thisBoolean == true)
            {
                thisButton.BackColor = Color.LightGreen;
            }
            else
            {
                thisButton.BackColor = Color.LightGray;
            }
        }

        bool low = true, high = true; // Used for overall curve display (lowpass/highpass combined)
        bool colorTogglePhase = true; // i.e. Green or On
        bool colorToggleDelay = true; // i.e. Green or On

        private void toggleLowPass_Click(object sender, EventArgs e)
        {
            ToggleButtonColor(toggleLowPass, ref low);
            
            ToggleBase.Curve(systemGraph, "LowpassSPL", low);
            ToggleBase.Curve(systemGraph, "LowpassPhase", low && colorTogglePhase);
            ToggleBase.Curve(systemGraph, "LowpassDelay", low && colorToggleDelay);
        }
        
        private void toggleHighPass_Click(object sender, EventArgs e)
        {
            ToggleButtonColor(toggleHighPass, ref high);

            ToggleBase.Curve(systemGraph, "HighpassSPL", high);
            ToggleBase.Curve(systemGraph, "HighpassPhase", high && colorTogglePhase);
            ToggleBase.Curve(systemGraph, "HighpassDelay", high && colorToggleDelay);
        }
        
        private void togglePhase_Click(object sender, EventArgs e)
        {
            ToggleButtonColor(togglePhase, ref colorTogglePhase);
            
            ToggleBase.Curve(systemGraph, "LowpassPhase", low && colorTogglePhase);
            ToggleBase.Curve(systemGraph, "HighpassPhase", high && colorTogglePhase);
        }
        
        private void toggleDelay_Click(object sender, EventArgs e)
        {
            ToggleButtonColor(toggleDelay, ref colorToggleDelay);
            
            ToggleBase.Curve(systemGraph, "LowpassDelay", low && colorToggleDelay);
            ToggleBase.Curve(systemGraph, "HighpassDelay", high && colorToggleDelay);
        }
        #endregion

        #region Radio Buttons

        double offsetTime = 0;
        double offsetMm = 0;
        private void offsetMetric_ValueChanged(object sender, EventArgs e)
        {
            offsetMm = Convert.ToDouble(offsetMetric.Value);
            double offset = offsetMm / 25.4; // Metric to English
            offsetEnglish.Text = offset.ToString("F2");
            if ((lpIsOffset == false) && (hpIsOffset == false)) { return; }
            if (lpIsOffset)
            {
                offsetMm = -offsetMm;
            }
            ProcessRadioButton();
            offsetTime = (offsetMm / 344000.0);
        }

        bool lpIsOffset = false;
        private void radioButtonLP_CheckedChanged(object sender, EventArgs e)
        {
            lpIsOffset = true;
            hpIsOffset = false;
            offsetMm = Convert.ToDouble(offsetMetric.Value);
            offsetMm = -offsetMm;
            ProcessRadioButton();
        }

        bool hpIsOffset = false;
        private void radioButtonHP_CheckedChanged(object sender, EventArgs e)
        {
            hpIsOffset = true;
            lpIsOffset = false;
            offsetMm = Convert.ToDouble(offsetMetric.Value);
            ProcessRadioButton();
        }

        private void radioButtonReset_CheckedChanged(object sender, EventArgs e)
        {
            lpIsOffset = false;
            hpIsOffset = false;
            ProcessRadioButton();
        }

        private void ProcessRadioButton()
        {
            double FcLP = Convert.ToDouble(trgtFreqLP.Value);
            double FcHP = Convert.ToDouble(trgtFreqHP.Value);
            ProcessAction(drivers.woofer1, FcLP, FcHP, Color.Blue, "LowpassSPL", "LowpassPhase");
            ProcessAction(drivers.tweeter, FcLP, FcHP, Color.Red, "HighpassSPL", "HighpassPhase");
        }
        #endregion

        private void splLabel_Click(object sender, EventArgs e)
        {

        }

        private void buttonExportLP_Click(object sender, EventArgs e)
        {
            File_IO. Filter.ExportCurveToFile("LowPass");
        }

        private void buttonExportHP_Click(object sender, EventArgs e)
        {
            File_IO.Filter.ExportCurveToFile("HighPass");
        }

        private void splNumeric_ValueChanged(object sender, EventArgs e)
        {
            var spl = sender as NumericUpDown;
            ExportSPL.Instance.nominal = Convert.ToDouble(spl.Value);
        }

    }
}
