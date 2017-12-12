using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using Core.Filtering.Core;
using Core.Targeting;
using Core.FileIO;

namespace Core.DriverCore
{
    /// <summary>
    /// Driver class. Includes all basic properties of any driver.
    /// </summary>
    public class DriverCore
    {
        public DriverCore() { }

        public enum DriverType { Tweeter, Midrange, Woofer };

        public bool isActive = false;

        public bool isInverted = false;

        #region BasicDriver

        /// <summary>
        /// driverType is a string version of the type.
        /// </summary>
        public DriverType type; // Changed accessibility to fix conflict

        /// <summary>
        /// Indicates if an impedance file has been imported.
        /// </summary>
        public bool initializedZ = false;

        /// <summary>
        /// Indicates if an SPL file has been imported.
        /// </summary>
        public bool initializedSPL = false;

        public string filenameSPL;
        public string filenameIMP;

        #endregion

        #region MeasurementFileControlsAndFields

        /// <summary>
        /// SPL profile for a driver. Imported from a measurement file.
        /// </summary>
        public double[,] splRaw;
        public Hashtable splMagHash;
        public Hashtable splPhaseHash;
        public Hashtable splComplex;

        /// <summary>
        /// Impedance profile for a driver. Imported from a measurement file
        /// </summary>
        public double[,] impRaw;
        public Hashtable impMagHash;
        public Hashtable impPhaseHash;
        public Hashtable impComplex;

        public double splAdjust; // Manual value set by the user if needed.

        /// <summary>
        /// SPL profile for a driver. Interpolated from a measurement file.
        /// </summary>
        public double[,] splInterpolated;
        public double[,] splInterpolatedInvertedPhase;

        /// <summary>
        /// Impedance profile for a driver.  Interpolated from a measurement file.
        /// </summary>
        public double[,] impInterpolated;

        public double[,] groupDelay;

        #endregion
        
        #region Titles

        /// <summary>
        /// Title that is displayed on the graph for SPL Magnitude.
        /// </summary>
        public string titleSPLMag;

        /// <summary>
        /// Title that is displayed on the graph for SPL Phase.
        /// </summary>
        public string titleSPLPhase;

        /// <summary>
        /// Title that is displayed on the graph for SPL Magnitude.
        /// </summary>
        public string titleEqualizedSPLMag;

        /// <summary>
        /// Title that is displayed on the graph for SPL Phase.
        /// </summary>
        public string titleEqualizedSPLPhase;

        /// <summary>
        /// Title that is displayed on the graph for Impedance Magnitude.
        /// </summary>
        public string titleIMPMag;

        /// <summary>
        /// Title that is displayed on the graph for Impedance Phase.
        /// </summary>
        public string titleIMPPhase;

        /// <summary>
        /// Title that is displayed on the graph for Impedance Magnitude.
        /// </summary>
        public string titleEqualizedIMPMag;

        /// <summary>
        /// Title that is displayed on the graph for Impedance Phase.
        /// </summary>
        public string titleEqualizedIMPPhase;

        public string titleCompensatedIMPMag;

        public string titleCompensatedIMPPhase;

        /// <summary>
        /// Title that is displayed on the graph for SPL Magnitude.
        /// </summary>
        public string titleSPLMagRaw;

        /// <summary>
        /// Title that is displayed on the graph for SPL Phase.
        /// </summary>
        public string titleSPLPhaseRaw;

        /// <summary>
        /// Title that is displayed on the graph for Impedance Magnitude.
        /// </summary>
        public string titleIMPMagRaw;

        /// <summary>
        /// Title that is displayed on the graph for Impedance Phase.
        /// </summary>
        public string titleIMPPhaseRaw;

        public string titleCircuitOnlyIMPMag;

        public string titleCircuitOnlyIMPPhase;

        public string titleCircuitTransferFunctionMag;

        public string titleCircuitTransferFunctionPhase;

        public string titleCircuitTransferFunctionMagSys;

        public string titleGroupDelay;

        public string titleSPLReference;

        #endregion        
        
        #region Tags

        /// <summary>
        /// Identifies the magnitude line displayed on the graph.
        /// </summary>
        public string tagSPLMag;

        /// <summary>
        /// Identifies the phase line displayed on the graph.
        /// </summary>
        public string tagSPLPhase;

        /// <summary>
        /// Identifies the magnitude line displayed on the graph.
        /// </summary>
        public string tagEqualizedSPLMag;

        /// <summary>
        /// Identifies the phase line displayed on the graph.
        /// </summary>
        public string tagEqualizedSPLPhase;

        /// <summary>
        /// Identifies the magnitude line displayed on the graph.
        /// </summary>
        public string tagImpMag;

        /// <summary>
        /// Identifies the phase line displayed on the graph.
        /// </summary>
        public string tagImpPhase;

        /// <summary>
        /// Identifies the magnitude line displayed on the graph.
        /// </summary>
        public string tagEqualizedImpMag;

        /// <summary>
        /// Identifies the phase line displayed on the graph.
        /// </summary>
        public string tagEqualizedImpPhase;

        public string tagCompensatedImpMag;

        public string tagCompensatedImpPhase;

        public string tagCircuitOnlyImpMag;

        public string tagCircuitOnlyImpPhase;

        public string tagCircuitTransferFunctionMag;

        public string tagCircuitTransferFunctionPhase;

        /// <summary>
        /// Identifies the magnitude line displayed on the graph.
        /// </summary>
        public string tagSPLMagRaw;

        /// <summary>
        /// Identifies the phase line displayed on the graph.
        /// </summary>
        public string tagSPLPhaseRaw;

        /// <summary>
        /// Specifies the color to use for the SPL magnitude curve.
        /// </summary>
        public Color colorSPLMagRaw;

        /// <summary>
        /// Specifies the color to use for the SPL phase curve.
        /// </summary>
        public Color colorSPLPhaseRaw;

        /// <summary>
        /// Identifies the magnitude line displayed on the graph.
        /// </summary>
        public string tagImpMagRaw;

        /// <summary>
        /// Identifies the phase line displayed on the graph.
        /// </summary>
        public string tagImpPhaseRaw;

        public string tagSPLReference;

        #endregion

        #region Colors

        /// <summary>
        /// Specifies the color to use for the SPL magnitude curve.
        /// </summary>
        public Color colorSPLMag;

        /// <summary>
        /// Specifies the color to use for the SPL phase curve.
        /// </summary>
        public Color colorSPLPhase;

        /// <summary>
        /// Specifies the color to use for the SPL magnitude curve.
        /// </summary>
        public Color colorEqualizedSPLMag;

        /// <summary>
        /// Specifies the color to use for the SPL phase curve.
        /// </summary>
        public Color colorEqualizedSPLPhase;

        public Color colorCompensatedImpMag;

        public Color colorCompensatedImpPhase;

        /// <summary>
        /// Specifies the color to use for the SPL magnitude curve.
        /// </summary>
        public Color colorSPLMagSys;

        /// <summary>
        /// Specifies the color to use for the SPL phase curve.
        /// </summary>
        public Color colorSPLPhaseSys;

        /// <summary>
        /// Specifies the color to use for the SPL magnitude curve.
        /// </summary>
        public Color colorEqualizedSPLMagSys;

        /// <summary>
        /// Specifies the color to use for the SPL phase curve.
        /// </summary>
        public Color colorEqualizedSPLPhaseSys;

        /// <summary>
        /// Specifies the color to use for the Impedance magnitude curve.
        /// </summary>
        public Color colorImpMag;

        /// <summary>
        /// Specifies the color to use for the Impedance phase curve.
        /// </summary>
        public Color colorImpPhase;

        /// <summary>
        /// Specifies the color to use for the Impedance magnitude curve.
        /// </summary>
        public Color colorEqualizedImpMag;

        /// <summary>
        /// Specifies the color to use for the Impedance phase curve.
        /// </summary>
        public Color colorEqualizedImpPhase;

        /// <summary>
        /// Specifies the color to use for the Impedance magnitude curve.
        /// </summary>
        public Color colorEqualizedImpMagSys;

        /// <summary>
        /// Specifies the color to use for the Impedance phase curve.
        /// </summary>
        public Color colorEqualizedImpPhaseSys;

        public Color colorCircuitOnlyImpMag;

        public Color colorCircuitOnlyImpPhase;

        public Color colorCircuitTransferFunctionMag;

        public Color colorCircuitTransferFunctionPhase;

        public Color colorCircuitTransferFunctionMagSys;

        public Color colorCircuitTransferFunctionPhaseSys;

        /// <summary>
        /// Specifies the color to use for the Impedance magnitude curve.
        /// </summary>
        public Color colorImpMagRaw;

        /// <summary>
        /// Specifies the color to use for the Impedance phase curve.
        /// </summary>
        public Color colorImpPhaseRaw;

        public Color colorGroupDelay;

        public Color colorSPLReference;

        #endregion

        #region ZedGraph

        /// <summary>
        /// A LineWidth...Mag... property is used to assign a value for Curve.Line.Width in a graph.
        /// The value must be a float that ends in "F", such as 1.8F or 2F.
        /// </summary>
        public float lineWidthSPLInterpMag;
        public float lineWidthSPLInterpMagSys;
        public float lineWidthSPLEqMag;
        public float lineWidthSPLEqMagSys;
        public float lineWidthSPLSumMag;
        public float lineWidthIMPInterpMag;
        public float lineWidthIMPEqMag;
        public float lineWidthIMPCompMag;

        /// <summary>
        /// LineWidth...Phase... property is used to assign a value for Curve.Line.Width in a graph.
        /// The value must be a float that ends in "F", such as 1.8F or 2F.
        /// </summary>
        public float lineWidthSPLInterpPhase;
        public float lineWidthSPLInterpPhaseSys;
        public float lineWidthSPLEqPhase;
        public float lineWidthSPLEqPhaseSys;
        public float lineWidthSPLSumPhase;
        public float lineWidthIMPInterpPhase;
        public float lineWidthIMPEqPhase;
        public float lineWidthIMPCompPhase;

        public float lineWidthSPLReference;

        public bool  lineIsSmooth;
        public bool  lineIsAntiAlias;
        public float lineSmoothTension;

        #endregion

        #region Targets
        public Target target = new Target();

        /// <summary>
        /// Self explanatory.
        /// </summary>
        /// <param name="sectionToUpdate"></param>
        public void SetAcousticTargetOrder(Filtering.Core.FilterCore.Section sectionToUpdate)
        {
            if (target.section == FilterCore.Section.HP)
            {
                if (target.fullTypeHP.Contains("first")) { target.orderHP = 1; }
                else if (target.fullTypeHP.Contains("second")) { target.orderHP = 2; }
                else if (target.fullTypeHP.Contains("third")) { target.orderHP = 3; }
                else if (target.fullTypeHP.Contains("fourth")) { target.orderHP = 4; }
                else if (target.fullTypeHP.Contains("fifth")) { target.orderHP = 5; }
                else if (target.fullTypeHP.Contains("sixth")) { target.orderHP = 6; }
                else if (target.fullTypeHP.Contains("seventh")) { target.orderHP = 7; }
                else if (target.fullTypeHP.Contains("eighth")) { target.orderHP = 8; }
                else { target.orderHP = 0; }
            }
            else if (target.section == FilterCore.Section.LP)
            {
                if (target.fullTypeLP.Contains("first")) { target.orderLP = 1; }
                else if (target.fullTypeLP.Contains("second")) { target.orderLP = 2; }
                else if (target.fullTypeLP.Contains("third")) { target.orderLP = 3; }
                else if (target.fullTypeLP.Contains("fourth")) { target.orderLP = 4; }
                else if (target.fullTypeLP.Contains("fifth")) { target.orderLP = 5; }
                else if (target.fullTypeLP.Contains("sixth")) { target.orderLP = 6; }
                else if (target.fullTypeLP.Contains("seventh")) { target.orderLP = 7; }
                else if (target.fullTypeLP.Contains("eighth")) { target.orderLP = 8; }
                else { target.orderLP = 0; }
            }
            else if (target.section == FilterCore.Section.BP)
            {
                if (sectionToUpdate == FilterCore.Section.HP)
                {
                    if (target.fullTypeHP.Contains("first")) { target.orderHP = 1; }
                    else if (target.fullTypeHP.Contains("second")) { target.orderHP = 2; }
                    else if (target.fullTypeHP.Contains("third")) { target.orderHP = 3; }
                    else if (target.fullTypeHP.Contains("fourth")) { target.orderHP = 4; }
                    else if (target.fullTypeHP.Contains("fifth")) { target.orderHP = 5; }
                    else if (target.fullTypeHP.Contains("sixth")) { target.orderHP = 6; }
                    else if (target.fullTypeHP.Contains("seventh")) { target.orderHP = 7; }
                    else if (target.fullTypeHP.Contains("eighth")) { target.orderHP = 8; }
                    else { target.orderHP = 0; }
                }
                if (sectionToUpdate == FilterCore.Section.LP)
                {
                    if (target.fullTypeLP.Contains("first")) { target.orderLP = 1; }
                    else if (target.fullTypeLP.Contains("second")) { target.orderLP = 2; }
                    else if (target.fullTypeLP.Contains("third")) { target.orderLP = 3; }
                    else if (target.fullTypeLP.Contains("fourth")) { target.orderLP = 4; }
                    else if (target.fullTypeLP.Contains("fifth")) { target.orderLP = 5; }
                    else if (target.fullTypeLP.Contains("sixth")) { target.orderLP = 6; }
                    else if (target.fullTypeLP.Contains("seventh")) { target.orderLP = 7; }
                    else if (target.fullTypeLP.Contains("eighth")) { target.orderLP = 8; }
                    else { target.orderLP = 0; }
                }
            }
            else
            {
                MessageBox.Show("Oops. There was no case for this FilterCore.\nCan't do anything.");
            }
        }

        /// <summary>
        /// Butterworth, Bessel or Linkwitz-Riley
        /// </summary>
        public void SetAcousticTargetType()
        {
            if (this.target.section == FilterCore.Section.HP || target.section == FilterCore.Section.BP)
            {
                // We've got to break up the targetType into consitutuent parts. Not pretty, but there's no easy way.
                if (target.fullTypeHP.Contains("Butterworth")) { target.nameHP = AcousticTargets.FilterName.Butterworth; }
                else if (target.fullTypeHP.Contains("Linkwitz")) { target.nameHP = AcousticTargets.FilterName.LinkwitzRiley; }
                else if (target.fullTypeHP.Contains("Bessel")) { target.nameHP = AcousticTargets.FilterName.Bessel; }
                else if (target.fullTypeHP.Contains("Bessel_PM")) { target.nameHP = AcousticTargets.FilterName.BesselPhaseMatch; }

                else { target.nameHP = AcousticTargets.FilterName.None; }
            }

            if (this.target.section == FilterCore.Section.LP || target.section == FilterCore.Section.BP)
            {
                if (target.fullTypeLP.Contains("Butterworth")) { target.nameLP = AcousticTargets.FilterName.Butterworth; }
                else if (target.fullTypeLP.Contains("Linkwitz")) { target.nameLP = AcousticTargets.FilterName.LinkwitzRiley; }
                else if (target.fullTypeLP.Contains("Bessel")) { target.nameLP = AcousticTargets.FilterName.Bessel; }
                else if (target.fullTypeLP.Contains("Bessel_PM")) { target.nameLP = AcousticTargets.FilterName.BesselPhaseMatch; }

                else { target.nameLP = AcousticTargets.FilterName.None; }
            }
        }

        AcousticTargets targets = new AcousticTargets();

        /// <summary>
        /// New; replaces method used with old target scheme.
        /// Need to build the old combination type/order property since it is still used in the business code.
        /// </summary>
        /// <param name="targetType"></param>
        public void SetFullAcousticTargetType()
        {
            switch (this.type)
            {
                case DriverType.Tweeter:
                    {
                        string tKey = target.nameHP.ToString() + target.orderHP.ToString();
                        targets.FullFilter.TryGetValue(tKey, out target.fullTypeHP);
                        break;
                    }
                case DriverType.Midrange:
                    {
                        if (this.target.section == FilterCore.Section.BP)
                        {
                            string tKey = target.nameHP.ToString() + target.orderHP.ToString();
                            targets.FullFilter.TryGetValue(tKey, out target.fullTypeHP);

                            tKey = target.nameLP.ToString() + target.orderLP.ToString();
                            targets.FullFilter.TryGetValue(tKey, out target.fullTypeLP);
                        }
                        break;
                    }
                case DriverType.Woofer:
                    {
                        string tKey = target.nameLP.ToString() + target.orderLP.ToString();
                        targets.FullFilter.TryGetValue(tKey, out target.fullTypeLP);
                        break;
                    }
            }
        }

        #endregion

        /// TODO Group delay calculation - on hold

        /// <summary>
        /// Group delay is the slope at discrete points. Plotting this on the existing phase scale requires 
        /// finding the peak value, normalizing to that, then re-factoring so that the peak value coincides with +180.
        /// This will work for the existing dual vertical scales. A third scale could be added, but this will change
        /// the layout of the graphs. The actual scale isn't as important as seeing the curve itself.
        /// </summary>
        /// <param name="complexSPL"></param>
        /// <returns></returns>
        /// 
        //public void CalculateGroupDelay()
        //{
        //    double[,] groupDelay = new double[FrequencyRange.dynamicFreqs.Count() - 1, 2];

        //    double freq1 = 0, freq2 = 0, phase1 = 0, phase2 = 0, slope = 0;

        //    for (int i = 1; i < FrequencyRange.dynamicFreqs.Count() - 2; i++)
        //    {
        //        freq1 = 0; phase1 = 0; phase2 = 0;

        //        freq1 = target.splInterpolated[i, 0];
        //        freq2 = target.splInterpolated[i + 1, 0];

        //        phase1 = FilterCore.WrapPhaseDegrees(target.splInterpolated[i, 2]);
        //        phase2 = FilterCore.WrapPhaseDegrees(target.splInterpolated[i + 1, 2]);

        //        slope = ((phase2 - phase1) / (freq2 - freq1));

        //        groupDelay[i, 0] = freq1 + ((freq2 - freq1) / 2);
        //        groupDelay[i, 1] = slope;
        //    }

        //    this.groupDelay = groupDelay;
        //}

        #region Spatial

        /// <summary>
        /// Point coordinates to be used for 3-D spatial reference.
        /// For use with minimum-phase (MP) files with generated Hilbert-Bode phase.
        /// </summary>
        public SpatialCore.Spatial center;

        public double diameter;

        public double distance;

        private double RotatePhase(double phase)
        {
            //lock (_locker)
            {
                // Prevent infinite loop
                if ((phase >= -180.0) && (phase <= 180.0)) { return phase; }

                else
                {
                    if (!(phase < 180.0)) { phase -= 360.0; } // Greater than 180, subtract 360
                    if ((phase < -180.0)) { phase += 360.0; } // Less than -180, add 360

                    return RotatePhase(phase);
                }
            }
        }

        #endregion

        #region FileReadMethods

        /// <summary>
        /// Reads driver impedance files in text format and converts it 
        /// as an array of doubles to the caller.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private double[,] ImportRawMeasurement(string filename, double[] frequencies)
        {
            int rejectCount = 0;
            int columnCount = 0;
            int commentLines = 0;
            int duplicateFrequencies = 0;
            int lessThanZero = 0;

            double frequency, magnitude, phase;

            Hashtable magHash = new Hashtable();
            Hashtable phaseHash = new Hashtable();

            List<double> magList = new List<double>();
            List<double> phaseList = new List<double>();

            // Prepare to open the stream and read it into the Session Data array.
            string text_line = "";
            StreamReader objReader = new StreamReader(filename);
            var i = 0;

            // Read all lines and process them line-by-line.
            string[] substring = new string[3];// Placeholder for split results
            do
            {
                var raw_line = objReader.ReadLine();
                text_line = raw_line.Trim();
                if (objReader.EndOfStream)
                {
                }

                // Skip empty lines or lines with any form of comment (any char not an "E" or +/-)
                if (text_line == "")
                {
                    //objReader.Close();
                    continue;
                }
                // Skip anything that is not a data line type (partial check here)
                if (!(Regex.Matches(text_line, @"[0-9E.+-]+").Count > 0) &&
                    !(Regex.Matches(text_line, @"\\s{2,}").Count > 0))
                {
                    commentLines++;
                    continue;
                }
                // Skip anything that is specifically a comment line
                if (Regex.Matches(text_line, @"[*#!]+").Count > 0)
                {
                    commentLines++;
                    continue;
                }
                // Skip anything that is a comment line. Set to 2 because it could be in scientific notation.
                // This is a redundant check left in (in case the above somehow misses some possible combinations).
                if (Regex.Matches(text_line, @"[a-zA-Z]{2,}").Count > 0)
                {
                    commentLines++;
                    continue;
                }
                
                substring.Initialize();

                // Determine if this is a CSV or not. The trim command should have left only the data and two separators.
                string input = text_line;
                string pattern;
                int count;
                count = Regex.Matches(text_line, @"[\s]+").Count;
                if (Regex.Matches(text_line, @",").Count == 2)
                {
                    pattern = @",";
                }
                else if (Regex.Matches(text_line, @"[\s]+").Count == 2) // White space seems to include before and after the line !
                {
                    pattern = @"[\s]+";
                }
                else if (Regex.Matches(text_line, @"\t").Count == 2)
                {
                    pattern = @"\t";
                }
                else
                {
                    var myMessage = "Failure to find column separator for a data column in file\n" + filename;
                    MessageBox.Show(myMessage);
                    break; // break out of DO to close the file and exit
                }

                substring = Regex.Split(input, pattern);    // Split on pattern. Should be 3 entries.

                // Check for error
                if (substring[1] == null || substring[2] == null)
                {
                    var myMessage = "Failure due to data not found for a column.\nData for the line is " + input + "\nFilename is \n" + filename;
                    MessageBox.Show(myMessage);
                    break; // break out of DO to close the file and exit
                }
                var length = substring.GetLength(0);
                if (!(substring.GetLength(0) == 3))
                {
                    var myMessage = "Failure due to number of data columns not exactly three.\n" + filename;
                    MessageBox.Show(myMessage);
                    break; // break out of DO to close the file and exit
                }

                // Need to ensure that the decimal places do not exceed the range for a double

                Regex rPos = new Regex(@"E+");
                Regex rNeg = new Regex(@"E-");
                if ((rPos.Matches(input).Count >= 1) || (rNeg.Matches(input).Count >= 1))
                {
                    // Evidently the file uses values expressed in scientific notation.
                }
                if ((rPos.Match(substring[0]).Success) || (rNeg.Match(substring[0]).Success))
                {
                    substring[0] = Convert.ToString(Convert.ToDouble(substring[0]));
                }
                if ((rPos.Match(substring[1]).Success) || (rNeg.Match(substring[1]).Success))
                {
                    substring[1] = Convert.ToString(Convert.ToDouble(substring[1]));
                }
                if ((rPos.Match(substring[2]).Success) || (rNeg.Match(substring[2]).Success))
                {
                    substring[2] = Convert.ToString(Convert.ToDouble(substring[2]));
                }

                if (substring.Length < 3) // Item 4 may be a string null terminator or other irrelevant data
                {
                    columnCount++;
                    //MessageBox.Show("Did not find three data columns (is phase missing?). Invalid measurement file.");
                    continue;
                }
                // Handle case of leading space(s) before first column. Shift all left one. NOTE: THIS IS PROBABLY NOW REDUNDANT DUE TO THE TRIM ABOVE.
                if ((substring.Length >= 4) && (substring[0] == ""))
                {
                    substring[0] = substring[1];
                    substring[1] = substring[2];
                    substring[2] = substring[3];
                }
                // Reject any magnitude less than zero. Continue to the next one.
                if (Convert.ToDouble(substring[1]) < 0)
                {
                    rejectCount++;
                    lessThanZero++;
                    continue;
                }
                // Reject any lines with frequency less than the lower limit of the interpolation range
                if (Convert.ToDouble(substring[0]) < frequencies[0])
                {
                    rejectCount++;
                    continue;
                }
                // Convert each value from a string to a double.
                // If the value is out of range of a double, flag it and go no further.
                try
                {
                    // Build the hashes that will be used later. Must leave as strings.
                    frequency = Convert.ToDouble(substring[0]);
                    magnitude = Convert.ToDouble(substring[1]);
                    phase = Convert.ToDouble(substring[2]);
                    // Skip any lines with duplicate frequencies
                    if (magHash.ContainsKey(frequency))
                    {
                        duplicateFrequencies++;
                        continue; // Skip this one
                    }
                    magHash.Add(frequency, magnitude);
                    phaseHash.Add(frequency, RotatePhase(phase));
                    magList.Add(magnitude);
                    phaseList.Add(phase);
                }
                catch (OverflowException)
                {
                    var myMessage = "Failure of conversion from string to double (OverflowException). Cancelling file import.\nTry another file or correct this one." + filename;
                    MessageBox.Show(myMessage, "Data Conversion Error");
                    break; // break out of DO to close the file and exit
                }
                i++;
            } while (objReader.Peek() != -1);

            objReader.Close();
            MessageBox.Show("Converted " + i + " lines from string to double and interpolated to internal range.\nRejected "
                + rejectCount + " lines (data type or range issue) with " + lessThanZero + " less than zero.\nRejected "
                + commentLines + " comment lines.\nFound "
                + columnCount + " lines with less than 3 columns.\n"
                + duplicateFrequencies + " lines with duplicate frequencies.", filename);

            if ((magHash.Count == 0) || (phaseHash.Count == 0))
            {
                MessageBox.Show("Either magnitude or phase data absent or invalid.\nMagnitude count = " + magHash.Count + "\nPhase count = " + phaseHash.Count, filename);
            }

            return Interpolate.InterpolateData(magHash, phaseHash, frequencies);
        }

        /// <summary>
        /// Read the raw SPL file and split each data line into constituent parts.
        /// </summary>
        /// <param name="filename">Raw SPL measurement file</param>
        public void ImportSPLData(string filename, double[] frequencies)
        {
            splInterpolated = ImportRawMeasurement(filename, frequencies);
            // Rotating phase by 180 degrees for use if the driver connection is inverted.
            // This is done here so that it is done once rather than on every calcution of a crossover.
            // TODO: Need to distinquish raw from eq
            //InvertPhase(); 
            return;
        }

        /// <summary>
        /// Read the raw impedance file and split each data line into constituent parts.
        /// </summary>
        /// <param name="filename">Raw impedance measurement file</param>
        public void ImportImpData(string filename, double[] frequencies)
        {
            impInterpolated = ImportRawMeasurement(filename, frequencies);
            return;
        }
        #endregion

        public virtual void Initialize()
        {
            initializedSPL = false;
            initializedZ = false;
            filenameSPL = null;
            impComplex = null;
            impInterpolated = null;
            impMagHash = null;
            impPhaseHash = null;
            impRaw = null;
            splComplex = null;
            splInterpolated = null;
            splInterpolatedInvertedPhase = null;
            splMagHash = null;
            splPhaseHash = null;
            splRaw = null;
            splAdjust = 0;
            /// TODO: Use this when GroupDelay is changed.
            //groupDelayEqualizedSPL = null;

            isInverted = false;

            center.xAxis = 0.0;
            center.yAxis = 0.0;
            center.zAxis = 0.0;

            diameter = 0.0;
            distance = 0.0;
        }
        
    }
}
