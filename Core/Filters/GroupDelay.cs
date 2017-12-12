using System.Linq;
using System.Numerics;
using System.Drawing;
using Core.Data;

namespace Core.Filtering
{
    public class GroupDelay
    {
        public string name;
        public string title;
        public string magTag;
        public Color magColor;
        public bool isVisible = true;

        //private double[,] _discretePoints;
        public double[,] normalizedDelay { get { return _normalizedDelay; } }

        private double[,] groupDelay       = new double[FrequencyRange.dynamicFreqs.Count() - 1, 2];
        private double[,] _normalizedDelay = new double[FrequencyRange.dynamicFreqs.Count() - 1, 2];

        public GroupDelay()
        {
            // These are defaults. They should be modified for anything other than the system response.
            name = "Group Delay";
            title = "Group Delay";
            magTag = "Group Delay";
            magColor = Color.Black;
        }

        /// <summary>
        /// This unwrap only works for calculated phase. Measured phase has peaks/dips that will fail with this method.
        /// </summary>
        /// <param name="phaseN"></param>
        /// <param name="phaseNMinus1"></param>
        /// <returns></returns>
        private static double UnwrapPhaseDegrees(double phaseN, double phaseNMinus1)
        {
            {
                while ((phaseN > phaseNMinus1))
                {
                    phaseN -= 360;
                }
            }
            return phaseN;
        }

        private static double WrapPhaseDegrees(double phase)
        {
            if ((phase >= -180.0) && (phase <= 180.0)) 
            { 
                return phase + 180; 
            }
            
            while ((phase < -180.0) || (phase > 180.0))
                {
                if (!(phase < 180.0)) { phase -= 2 * 180.0; }
                if ((phase < -180.0)) { phase += 2 * 180.0; }
            }
            return phase + 180; // Handles case of negative phase values since divide operation will nullify this.
        }

        /// <summary>
        /// Group delay is the slope at discrete points. Plotting this on the existing phase scale requires 
        /// finding the peak value, normalizing to that, then re-factoring so that the peak value coincides with +180.
        /// This will work for the existing dual vertical scales. A third scale could be added, but this will change
        /// the layout of the graphs. The actual scale isn't as important as seeing the curve itself.
        /// </summary>
        /// <param name="complexSPL"></param>
        /// <returns></returns>
        public static double[,] Calculate(double[,] originalSPL, out double max)
        {
            max = 0;
            double freq0 = 0, freqN = 0, freq2 = 0, phase0 = 0, phaseN = 0, phase2 = 0, slope = 0;

            double[,] unwrappedSPL = originalSPL;
            double unwrappedPhase0 = 0;
            double unwrappedPhaseN = 0;
            double unwrappedPhase2 = 0;
            double[,] groupDelay = new double[FrequencyRange.dynamicFreqs.Count() - 1, 3]; // Third value added for plotting method that needs to see an array of 3

            unwrappedPhase0    = originalSPL[1, 2];
            unwrappedSPL[1, 2] = originalSPL[1, 2]; 

            // All in between
            for (int i = 2; i < FrequencyRange.dynamicFreqs.Count() - 2; i++ )
            {
                phase0 = unwrappedSPL[i - 1, 2];
                phaseN = unwrappedSPL[i    , 2];
                phase2 = unwrappedSPL[i + 1, 2];

                unwrappedPhaseN = UnwrapPhaseDegrees(phaseN, phase0         );
                unwrappedPhase2 = UnwrapPhaseDegrees(phase2, unwrappedPhaseN);

                freq0 = FrequencyRange.dynamicFreqs[i - 1];
                freqN = FrequencyRange.dynamicFreqs[i    ];
                freq2 = FrequencyRange.dynamicFreqs[i + 1];
                
                unwrappedSPL[i    , 2] = unwrappedPhaseN;
                unwrappedSPL[i + 1, 2] = unwrappedPhase2;

                phase0 = unwrappedSPL[i - 1, 2];
                phaseN = unwrappedPhaseN;
                phase2 = unwrappedPhase2;

                slope = -(  ((phaseN - phase0) / (freqN - freq0))
                          + ((phase2 - phaseN) / (freq2 - freqN)) ) / 720.0;

                groupDelay[i, 0] = freqN;
                //groupDelay[i, 1] = groupDelay[i - 1, 1] + (slope);
                //groupDelay[i, 2] = slope; // Sec
                groupDelay[i, 2] = slope * 1000; // msec
                max = (max < groupDelay[i, 2]) ? groupDelay[i, 2] : max;
            }
            return groupDelay;
        }

        /// <summary>
        /// Group delay is the slope at discrete points. Plotting this on the existing phase scale requires 
        /// finding the peak value, normalizing to that, then re-factoring so that the peak value coincides with +180.
        /// This will work for the existing dual vertical scales. A third scale could be added, but this will change
        /// the layout of the graphs. The actual scale isn't as important as seeing the curve itself.
        /// </summary>
        /// <param name="complexSPL"></param>
        /// <returns></returns>
        public void Calculate(Complex[] complexSPL)
        {
            // Multiple points are used for smoothing. Rapidly changing localized phase makes the result almost useless.
            double freqN  = 0; //, phaseN = 0, freq2 = 0, phase2 = 0;
            double slope = 0;
            bool firstFound = false;
            //bool lastFound  = false;
            int first = 0;
            int last = FrequencyRange.dynamicFreqs.Count() - 2;

            int smoothingValue = 10; // Smoothing points above and below the center frequency

            // All in between
            for (int i = smoothingValue / 2; i < FrequencyRange.dynamicFreqs.Count() - (smoothingValue / 2); i++)
            {
                // Any data should never be precisely 0.0, first point with non-zero for lowest point used for smoothing is start of data.
                if (complexSPL[i - (smoothingValue / 2)].Phase == 0.0) { continue; }

                if (!firstFound) { first = i; firstFound = true; continue; }

                // First precise zero after start of data should be the end of data.
                //if (complexSPL[i].Phase == 0.0) { last = i - smoothingValue; lastFound = true; break; }
                if (complexSPL[i].Phase == 0.0) { last = i - smoothingValue; break; }

                freqN = FrequencyRange.dynamicFreqs[i];
                slope = Smoothing(complexSPL, i, smoothingValue); // i = center frequency reference

                groupDelay[i, 0] = freqN;
                groupDelay[i, 1] = slope;
            }

            /*
            // First and last values
            //slope = -((phase2 - phase1) / (freq2 - freq1)) / 360.0;

            freqN = FrequencyRange.dynamicFreqs[first];
            freq2 = FrequencyRange.dynamicFreqs[first + 1];
            phaseN = UnwrapPhaseDegrees(complexSPL[first].Phase);
            phase2 = UnwrapPhaseDegrees(complexSPL[first + 1].Phase);
            //slope = -((phase2 - phaseN) / (freq2 - freqN)) / 360.0;
            slope = -((phase2 - phaseN) / (freq2 - freqN));
            groupDelay[first, 0] = freqN;
            groupDelay[first, 1] = slope;

            freqN = FrequencyRange.dynamicFreqs[last];
            freq2 = FrequencyRange.dynamicFreqs[last + 1];
            phaseN = UnwrapPhaseDegrees(complexSPL[last].Phase);
            phase2 = UnwrapPhaseDegrees(complexSPL[last + 1].Phase);
            //slope = -((phase2 - phaseN) / (freq2 - freqN)) / 360.0;
            slope = -((phase2 - phaseN) / (freq2 - freqN));
            groupDelay[last, 0] = freqN;
            groupDelay[last, 1] = slope;
            */

            //groupDelay[0, 0] = freq1 + ((freq2 - freq1) / 2);
            //groupDelay[FrequencyRange.dynamicFreqs.Count()-1, 0] = freq1 + ((freq2 - freq1) / 2);

            NormalizeToPeakValue();
        }

        /// <summary>
        /// Smoothing used to reduce the influence of wildly fluctuating localized phase values.
        /// </summary>
        /// <param name="complexSPL"></param>
        /// <param name="centerPoint"></param>
        /// <param name="smoothingValue"></param>
        /// <returns></returns>
        private static double Smoothing(Complex[] complexSPL, int centerPoint, int smoothingValue)
        {
            double freqN = 0;
            double phaseN = 0;
            double slope = 0;

            freqN = FrequencyRange.dynamicFreqs[centerPoint];
            phaseN = WrapPhaseDegrees(complexSPL[centerPoint].Phase);

            // Calculate slope about the center frequency N
            for (int i = 0; i < (smoothingValue / 2); i++)
            {
                slope += (complexSPL[centerPoint + (i + 1)].Phase - complexSPL[centerPoint + i].Phase) / (FrequencyRange.dynamicFreqs[centerPoint + (i + 1)] - FrequencyRange.dynamicFreqs[centerPoint + i]);
                slope += (complexSPL[centerPoint - i].Phase - complexSPL[centerPoint - (i + 1)].Phase) / (FrequencyRange.dynamicFreqs[centerPoint - i] - FrequencyRange.dynamicFreqs[centerPoint - (i + 1)]);
            }
            slope /= (smoothingValue);

            /* slope = +((phase1 - phase0) / (freq1 - freq0))
                    + ((phaseN - phase1) / (freqN - freq1))
                    + ((phase2 - phaseN) / (freq2 - freqN))
                    + ((phase3 - phase2) / (freq3 - freq2))
                    / 4;*/

            return slope;
        }

        private void NormalizeToPeakValue()
        {
            // Find the peak
            double peak = 0;
            double value = 0;
            for (int i = 1; i < FrequencyRange.dynamicFreqs.Count() - 2; i++)
            {
                value = groupDelay[i, 1];
                if (value > peak) { peak = value; }
            }
            if (peak <= 0.0) { return; }

            // Normalize to the peak, then normalize peak to 180 to fit graph scale
            for (int i= 0; i < groupDelay.GetLength(0) - 1; i++)
            {
                normalizedDelay[i, 0] = groupDelay[i, 0];              // Frequency point
                normalizedDelay[i, 1] = 180 * groupDelay[i, 1] / peak;
            }
        }
    }
}
