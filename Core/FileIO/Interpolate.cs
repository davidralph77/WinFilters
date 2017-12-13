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
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace Core.FileIO
{
    /// <summary>
    /// The Interpolate class has methods for sorting a hash table and interpolating it. These hash tables are
    /// magnitude and phase data.
    /// </summary>
    public static class Interpolate
    {
        /// <summary>
        /// InterpolateData takes hash tables for magnitude and phase as input and returns a double array of
        /// interpolated data. It will exclude data outside of the range specified for the range data. This
        /// range data may at some point be made user controllable. It's a simple interpolation algorithm.
        /// All it does is loop through each interpolation point and try to find A sample point on each side.
        /// If that condition occurs, then the interpolation is made on the slope of the line defined by those
        /// two sample points.
        /// </summary>
        /// <param name="magData"></param> magdata is likely to be from at sting with limited decimal range (maybe 2 characters)
        /// <param name="phaseData"></param> phaseData is likely to be from at sting with limited decimal range (maybe 2 characters)
        /// <returns></returns>
        public static double[,] InterpolateData(Hashtable magData, Hashtable phaseData, double[] frequencies)
        {
            double[,] bogusArray;
            bogusArray = null;
            double[,] fileData = new double[frequencies.Count(), 3];

            if ((magData.Count == 0) || (phaseData.Count == 0))
            {
                return bogusArray; // Returning null array to short-circuit.
            }

            double tmpSrcFreqLo;
            double tmpSrcFreqHi;
            double tmpSrcMagLo;
            double tmpSrcMagHi;
            double tmpSrcPhiLo;
            double tmpSrcPhiHi;
            double freqDiff;
            double magDiff;
            double phiDiff;

            double currentMag, currentPhi, lowerCof, upperCof;
            double[] interpTargetPoints;
            double interpFreq; // This is the currently targeted interpolation frequency.

            // Get a sorted array of the hash keys. Since both hashes have identical keys, only one sort is needed.
            double[] sortedKeys = sortHash(magData); // Gets a sorted array of the keys (frequency).
            double frequency;

            // We now have a sorted array of the key values. These should be used to sequentially access the values
            // that are to be interpolated. Both key (x-axis) and value (y-axis) are to be interpolated.
            // The interpolation points are taken from the FrequencyRange class. Ultimately we should put them into
            // a hash to return to the originall caller.
            interpTargetPoints = frequencies;
            Hashtable interpHashMag = new Hashtable(); // This will be returned for all later processing/design.
            Hashtable interpHashPhase = new Hashtable(); // This will be returned for all later processing/design.
            double[,] interpMagPhase = new double[frequencies.Count(), 3]; // Array to hold interp mag and phase.

            // We only use the mag data for other aspects as well, such as the key, although we are interpolating 
            // for both magnitude and phase data simultaneously.
            // Both hashes must be the same size and by default should be.

            //Initialize the temporary variables
            double minInterpFreq = frequencies[0];
            int rangeUpperLimit  = frequencies.Count() - 1; // Count of the frequency specified range
            double maxInterpFreq = frequencies[rangeUpperLimit]; // Upper frequency limit of the interpolations to be made

            int lowFrequencyCount = 0; // More reliable than assigning to upper frequency when looping through.

            tmpSrcFreqLo = sortedKeys[lowFrequencyCount];
            tmpSrcFreqHi = sortedKeys[lowFrequencyCount + 1];

            tmpSrcMagLo = Convert.ToDouble(magData[lowFrequencyCount]);
            tmpSrcMagHi = Convert.ToDouble(magData[lowFrequencyCount + 1]);

            tmpSrcPhiLo = Convert.ToDouble(phaseData[lowFrequencyCount]); // Lower frequency to be considered for the interpolation (magnitude)
            tmpSrcPhiHi = Convert.ToDouble(phaseData[lowFrequencyCount + 1]); // Upper frequency to be considered for the interpolation (magnitude)

            int ifCount1 = 0;
            int elseifCount = 0;
            int elseCount = 0;
            int sortedKeysCount = 0;

            // Added to check if these are identical frequency points. Main section can't handle identicals.
            // There's no need to interpolate if identical anyway.
            bool identical = true;
            for (int j = 0; j < sortedKeys.Count()-1; j++)
            {
                frequency = sortedKeys[j];
                if (Math.Abs(sortedKeys[j] - frequencies[j]) > 0.0001) 
                { 
                    identical = false; break; // Find one mis-match, must interpolate. 
                }
                fileData[j, 0] = frequencies[j]; // Use the defined value, not the imported one.
                fileData[j, 1] = Convert.ToDouble(magData[sortedKeys[j]]);
                fileData[j, 2] = Convert.ToDouble(phaseData[sortedKeys[j]]);
            }

            if (identical)
            {
                return interpMagPhase = fileData; // No need to go further.
            }
            
            // First, let's populate the full range to prevent holes in the final interpolated array (graphing purposes)
            for (int i = 0; i < rangeUpperLimit; i++)
            {
                interpFreq = frequencies[i]; // Interpolation frequency points to use from the range specified.
                interpMagPhase[i, 0] = interpFreq;
                interpMagPhase[i, 1] = 0;
                interpMagPhase[i, 2] = 0;
            }

            //if (Convert.ToDouble(magData.Count) > rangeUpperLimit)
            if ((sortedKeys[sortedKeys.Count() - 1]) > maxInterpFreq)
            {
                // Data above the upper limit is available. Add one more point to the limit so that the upper limit interpolation point will be handled.
                rangeUpperLimit++;
            }

            // Loop through each interpolation frequency (full double data). Find measurement points that fall in bewteen interpolation points.
            // Any points that are not between interpolation points are not used.
            for (int interpFreqCount = 0; interpFreqCount < rangeUpperLimit; interpFreqCount++)
            {
                interpFreq = frequencies[interpFreqCount]; // Interpolation frequency points to use from the range specified.

                if ((interpFreq > maxInterpFreq) || (interpFreq > sortedKeys[sortedKeys.Length - 1]))
                {
                    // The current dynamic range frequency is either:
                    //      > the maximum interpolation frequency specified (more data in the file than needed for the range)
                    //      > the maximum frequency in the data points in the file data (truncated or limited)
                    ifCount1++;
                    sortedKeysCount++;
                    break;
                }

                else if (interpFreq <= tmpSrcFreqLo)  // This means that the current interpolation frequency is not yet above the lowest sampled data point.
                {
                    elseifCount++;
                    continue;
                }

                else if (interpFreq > tmpSrcFreqHi)
                {
                    // The interpolation fruquency step is larger than the lo-hi diff. It stepped above the upper frequency.
                    // This means that there is no interpolation to do, there is no sampled data in between interpolation frequencies.
                    // Move to the next sample low data point higher than the current interpolation point. 
                    while (interpFreq > tmpSrcFreqHi)
                    {
                        lowFrequencyCount++;
                        tmpSrcFreqLo = sortedKeys[lowFrequencyCount];
                        tmpSrcFreqHi = sortedKeys[lowFrequencyCount + 1];
                    }
                    // Calculate the interpolations
                    freqDiff = tmpSrcFreqHi - tmpSrcFreqLo;

                    tmpSrcMagLo = Convert.ToDouble(magData[sortedKeys[lowFrequencyCount]]);
                    tmpSrcMagHi = Convert.ToDouble(magData[sortedKeys[lowFrequencyCount + 1]]);
                    tmpSrcPhiLo = Convert.ToDouble(phaseData[sortedKeys[lowFrequencyCount]]);
                    tmpSrcPhiHi = Convert.ToDouble(phaseData[sortedKeys[lowFrequencyCount + 1]]);
                    magDiff = tmpSrcMagHi - tmpSrcMagLo;
                    phiDiff = tmpSrcPhiHi - tmpSrcPhiLo;
                    lowerCof = Math.Abs((tmpSrcFreqHi - interpFreq) / freqDiff);
                    upperCof = Math.Abs((interpFreq - tmpSrcFreqLo) / freqDiff);
                    currentMag = (lowerCof * tmpSrcMagLo) + (upperCof * tmpSrcMagHi);
                    currentPhi = (lowerCof * tmpSrcPhiLo) + (upperCof * tmpSrcPhiHi);

                    // Update the final data structures
                    if (interpFreqCount <= rangeUpperLimit)
                    {
                        interpMagPhase[interpFreqCount, 0] = interpFreq;
                        interpMagPhase[interpFreqCount, 1] = currentMag;
                        interpMagPhase[interpFreqCount, 2] = currentPhi;
                    }

                    // Go to the next interpolation point. The idea is that the next interpolation point will be between the lo and hi found here.
                    continue;
                }

                else if ((interpFreq > tmpSrcFreqLo) && (interpFreq < tmpSrcFreqHi))// Interpolation frequency is between the points, we can do the interpolation.
                {
                    elseCount++;

                    // Calculate the interpolations
                    freqDiff = tmpSrcFreqHi - tmpSrcFreqLo;

                    tmpSrcMagLo = Convert.ToDouble(magData[sortedKeys[lowFrequencyCount]]);
                    tmpSrcMagHi = Convert.ToDouble(magData[sortedKeys[lowFrequencyCount + 1]]);
                    tmpSrcPhiLo = Convert.ToDouble(phaseData[sortedKeys[lowFrequencyCount]]);
                    tmpSrcPhiHi = Convert.ToDouble(phaseData[sortedKeys[lowFrequencyCount + 1]]);
                    magDiff = tmpSrcMagHi - tmpSrcMagLo;
                    phiDiff = tmpSrcPhiHi - tmpSrcPhiLo;
                    lowerCof = Math.Abs((tmpSrcFreqHi - interpFreq) / freqDiff);
                    upperCof = Math.Abs((interpFreq - tmpSrcFreqLo) / freqDiff);
                    currentMag = (lowerCof * tmpSrcMagLo) + (upperCof * tmpSrcMagHi);
                    currentPhi = (lowerCof * tmpSrcPhiLo) + (upperCof * tmpSrcPhiHi);

                    // Update the final data structures
                    if (interpFreq <= maxInterpFreq)
                    {
                        interpMagPhase[interpFreqCount, 0] = interpFreq;
                        interpMagPhase[interpFreqCount, 1] = currentMag;
                        interpMagPhase[interpFreqCount, 2] = currentPhi;
                    }
                }
                else
                {
                    // We should never be here. The handling should always be in one of the earlier situations.
                    MessageBox.Show("Oops. We should not be here.\nThe interpolation failed on this pass.");
                }
            }
            return interpMagPhase;
        }

        /// <summary>
        /// This sorting method returns a a sorted array that can be used to extract hash table data
        /// in a sorted manner. The array returned is a sort of the keys in the hash.
        /// </summary>
        /// <param name="hashData"></param>
        /// <returns></returns>
        private static double[] sortHash(Hashtable hashData)
        {
            double[] sortingArray = new double[hashData.Count]; // Length of the hash table sets the array size.

            // Copy the keys from the hash into an array. This should be the frequencies that must be in ascending order.
            // This sorted array will then be used to create an array of values sorted by the keys from the hash.
            // This array will be used for the interpolation. 
            int j = 0;
            foreach (double key in hashData.Keys)
            {
                sortingArray[j] = key; // Add to the array to sort (a list of keys into the hashtable)
                j++;
            }

            // Sort the array
            Array.Sort(sortingArray);
            return sortingArray;
        }
    }
}
