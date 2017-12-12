using System;
using System.Linq;
using System.Numerics;
using Core.Data;

namespace Core.Filtering.Core
{
    public class FilterCore
    {
        /// <summary>
        /// Indicates a crossover section type, HP/BP/LP.
        /// </summary>
        public enum Section { HP, BP, LP };

        public static double WrapPhaseDegrees(double phase)
        {
            if ((phase >= -180.0) && (phase <= 180.0)) { return phase; }

            while (!(phase >= -180.0) && (phase <= 180.0))
            {
                if ((phase >= 180.0)) { phase -= 2 * 180.0; } // Greater than 180, subtract 360
                if ((phase < -180.0)) { phase += 2 * 180.0; } // Less than -180, add 360
            }
            return phase;
        }

        // TODO: Determine the algorithm needed
        public static double UnWrapPhaseDegrees(double phase)
        {
            if ((phase >= -180.0) && (phase <= 180.0)) { return phase; }

            while (!(phase >= -180.0) && (phase <= 180.0))
            {
                if (!(phase < 180.0)) { phase -= 2 * 180.0; } // Greater than 180, subtract 360
                if ((phase < -180.0)) { phase += 2 * 180.0; } // Less than -180, add 360
            }
            return phase;
        }

        private double ConvertDBToAbsolute(double db)
        {
            double absolutePressure;
            double refPressure = 1E-12; // w/m^2

            //if (db == 0) { return 0; };
            absolutePressure = refPressure * Math.Pow(10, db / 20); // ref * 10^(db/10)
            return absolutePressure;
        }

        private double ConvertAbsoluteToDB(double abs)
        {
            double mydb;
            double refPressure = 1E-12; // w/m^2

            mydb = 20 * Math.Log10(abs / refPressure); // 10log(abs/ref);
            return mydb;
        }

        public static double InvertPhase(double phase)
        {
            while ((phase < -Math.PI) || (phase >= Math.PI))
            {
                phase = RotatePhase(phase);
            }
            
            if ( (phase < 0) ) 
                { phase += Math.PI; }
            else 
                { phase -= Math.PI; }

            return phase;
        }

        public static double RotatePhase(double phase)
        {
            if ((phase >= -Math.PI) && (phase <= Math.PI)) { return phase; }

            while ((phase < -Math.PI) || (phase >= Math.PI))
            {
                if ((phase >= Math.PI)) { phase -= 2.0 * Math.PI; } // Greater than 180, subtract 360
                if ((phase < -Math.PI)) { phase += 2.0 * Math.PI; } // Less than -180, add 360
            }
            return phase;
        }

        public double[,] SumResponses(double[,] filter1, double[,] filter2)
        {
            int count = FrequencyRange.dynamicFreqs.Count();
            double absMag1, absMag2, mag, phase;
            Complex complexAbs1, complexAbs2, complexSumDB;
            double[] systemSPL = new double[count];
            Complex[] complexSum = new Complex[count];
            double[,] doubleSum = new double[count, 3];
            double wrappedPhase = 0;

            for (int i = 0; i < count; i++)
            {
                // Convert db to absolute, create complex using absolute, sum the drivers
                if (double.IsNegativeInfinity(filter1[i, 1]))
                {
                    complexAbs1 = Complex.Zero;
                }
                else
                {
                    absMag1 = ConvertDBToAbsolute(filter1[i, 1]);
                    wrappedPhase = filter1[i, 2] * (Math.PI / 180.0);
                    complexAbs1 = Complex.FromPolarCoordinates(absMag1, wrappedPhase);
                }

                if (double.IsNegativeInfinity(filter2[i, 1]))
                {
                    complexAbs2 = Complex.Zero;
                }
                else
                {
                    absMag2 = ConvertDBToAbsolute(filter2[i, 1]);
                    wrappedPhase = filter2[i, 2] * (Math.PI / 180.0);
                    complexAbs2 = Complex.FromPolarCoordinates(absMag2, wrappedPhase);
                }

                complexSumDB = complexAbs1 + complexAbs2;

//                if (complexSumDB.Magnitude <= 0.0) { continue; } // Skip this one.
                mag = ConvertAbsoluteToDB(complexSumDB.Magnitude);
                phase = complexSumDB.Phase;
                //complexSum[i] = Complex.FromPolarCoordinates(mag, phase);
                doubleSum[i, 0] = FrequencyRange.dynamicFreqs[i];
                doubleSum[i, 1] = mag;
                doubleSum[i, 2] = phase * (180.0 / Math.PI);
            }
            return doubleSum;
        }
        
    }
}
