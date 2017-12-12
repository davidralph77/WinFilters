using System;
using System.Collections.Generic;
using System.Linq;
using Core.Data;

namespace Core.Targeting
{
    /// <summary>
    /// Target class. Includes all basic properties of a target, initially created for reading target files.
    /// When classical targets are calculated, they should be used to populate these where appropriate. Maybe. 
    /// Since there should be no need of interpolation, direct population of the "interpolated data" may be used.
    /// </summary>
    [Serializable]
    public class Target : TargetBase
    {
        /// <summary>
        /// SPL profile for a driver. Interpolated from a measurement file.
        /// </summary>
        public double[,] splInterpolated = new double[FrequencyRange.dynamicFreqs.Count(), 3];

        public Target() {  }

        // This is used to populate the filter ScrollBox when the user selects a filter order number. 
        // This is needed because some filters (i.e. L-R) have even order filter implementations only
        // and not all Bessel filters are being implemented for every order.
        public static Dictionary<AcousticTargets.Order, string[]> orders = new Dictionary<AcousticTargets.Order, string[]>
        {
             { AcousticTargets.Order.first,  new string[] {                                                     AcousticTargets.FilterName.Butterworth.ToString()                                                                                                      } },
             { AcousticTargets.Order.second, new string[] {AcousticTargets.FilterName.LinkwitzRiley.ToString(), AcousticTargets.FilterName.Butterworth.ToString(), AcousticTargets.FilterName.Bessel.ToString(), AcousticTargets.FilterName.Bessel3db.ToString(), AcousticTargets.FilterName.BesselPhaseMatch.ToString(), AcousticTargets.FilterName.BesselFlattest.ToString()} },
             { AcousticTargets.Order.third,  new string[] {                                                     AcousticTargets.FilterName.Butterworth.ToString(), AcousticTargets.FilterName.Bessel.ToString(), AcousticTargets.FilterName.Bessel3db.ToString(), AcousticTargets.FilterName.BesselPhaseMatch.ToString(), AcousticTargets.FilterName.BesselFlattest.ToString()} },
             { AcousticTargets.Order.fourth, new string[] {AcousticTargets.FilterName.LinkwitzRiley.ToString(), AcousticTargets.FilterName.Butterworth.ToString(), AcousticTargets.FilterName.Bessel.ToString(), AcousticTargets.FilterName.Bessel3db.ToString(), AcousticTargets.FilterName.BesselPhaseMatch.ToString(), AcousticTargets.FilterName.BesselFlattest.ToString()} },
             { AcousticTargets.Order.fifth,  new string[] {                                                     AcousticTargets.FilterName.Butterworth.ToString(), AcousticTargets.FilterName.Bessel.ToString(), AcousticTargets.FilterName.BesselPhaseMatch.ToString()} },
             { AcousticTargets.Order.sixth,  new string[] {AcousticTargets.FilterName.LinkwitzRiley.ToString(), AcousticTargets.FilterName.Butterworth.ToString(), AcousticTargets.FilterName.Bessel.ToString(), AcousticTargets.FilterName.BesselPhaseMatch.ToString()} },
             { AcousticTargets.Order.seventh,new string[] {                                                     AcousticTargets.FilterName.Butterworth.ToString(), AcousticTargets.FilterName.Bessel.ToString(), AcousticTargets.FilterName.BesselPhaseMatch.ToString()} },
             { AcousticTargets.Order.eighth, new string[] {AcousticTargets.FilterName.LinkwitzRiley.ToString(), AcousticTargets.FilterName.Butterworth.ToString(), AcousticTargets.FilterName.Bessel.ToString(), AcousticTargets.FilterName.BesselPhaseMatch.ToString()} },
        };

        // This takes the string from the form and finds the AcousticTargets.FilterName version that is used elsewhere in the program.
        public static Dictionary<string, AcousticTargets.FilterName> names = new Dictionary<string, AcousticTargets.FilterName>
        {
             { AcousticTargets.FilterName.LinkwitzRiley.ToString()   , AcousticTargets.FilterName.LinkwitzRiley    },
             { AcousticTargets.FilterName.Butterworth.ToString()     , AcousticTargets.FilterName.Butterworth      }, 
             { AcousticTargets.FilterName.Bessel.ToString()          , AcousticTargets.FilterName.Bessel           },
             { AcousticTargets.FilterName.Bessel3db.ToString()       , AcousticTargets.FilterName.Bessel3db        },
             { AcousticTargets.FilterName.BesselPhaseMatch.ToString(), AcousticTargets.FilterName.BesselPhaseMatch },
             { AcousticTargets.FilterName.BesselFlattest.ToString()  , AcousticTargets.FilterName.BesselFlattest   },
        };
        #region unused

        /*
         * public void ImportSPLData(string filename, double[] frequencies)
        {
            int rejectCount = 0;
            int columnCount = 0;
            int commentLines = 0;

            double frequency, magnitude, phase;

            Hashtable magHash = new Hashtable();
            Hashtable phaseHash = new Hashtable();

            // Prepare to open the stream and read it into the Session Data array.
            string text_line = "";
            StreamReader objReader = new StreamReader(filename);
            var i = 0;

            // Read all lines and process them line-by-line.
            string[] substring = new string[3];// Placeholder for split results
            do
            {
                text_line = objReader.ReadLine();
                if (objReader.EndOfStream)
                {
                }

                // Skip empty lines or lines with any form of comment (any char not an "E" or +/-)
                if (text_line == "")
                {
                    //objReader.Close();
                    continue;
                }

                // Parse each line and extract the constituent parts
                for (int j = 0; j < substring.Length; j++) { substring[j] = ""; } // Reset the array
                string input = text_line;
                string pattern = "\\s{1,}";
                substring = Regex.Split(input, pattern);    // Split on space. Should be 3 entries.

                // Skip anything that is a comment line
                if (Regex.Matches(input, @"[a-zA-Z]{2,}").Count > 0)
                {
                    commentLines++;
                    continue;
                }

                if (substring.Length < 3) // Item 4 may be a string null terminator or other irrelevant data
                {
                    columnCount++;
                    //MessageBox.Show("Did not find three data columns (is phase missing?). Invalid measurement file.");
                    continue;
                }

                // Handle case of leading space(s) before first column. Shift all left one.
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
                    magHash.Add(frequency, magnitude);
                    phaseHash.Add(frequency, phase);
                }
                catch (OverflowException)
                {
                    MessageBox.Show("Failure of conversion from string to double. Cancelling file import.\nTry another file or correct this one.");
                    //objReader.Close();
                    break; // break out of DO to close the file and exits
                }
                i++;
            } while (objReader.Peek() != -1);

            objReader.Close();
            MessageBox.Show("Converted " + i + " lines from string to double and interpolated to internal range.\nRejected " + rejectCount + " lines (data type issue).\nRejected " + commentLines + " comment lines.\nFound " + columnCount + " lines with less than 3 columns.");

            //double[,] myData;     // Array data for return

            if ((magHash.Count == 0) || (phaseHash.Count == 0))
            {
                MessageBox.Show("Either magnitude or phase data absent or invalid.\nMagnitude count = " + magHash.Count + "\nPhase count = " + phaseHash.Count);
            }

            splInterpolated = Interpolate.InterpolateData(magHash, phaseHash, frequencies);

            return;
        }
        */ 
        #endregion
    }
}
