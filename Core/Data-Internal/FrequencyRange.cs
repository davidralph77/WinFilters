﻿using System;
using System.Linq;

/*
 The frequency points are calculated from the values specified in FrequencyRange.cs and are generated by calling
 the SetFrequencies method. Initially these have been set to match the original Excel version of the PCD 7.0, but 
 provision is (planned) to be made to allow for a user selectable range. This will, of course, be with the
 understanding that the more sample points generated, the longer all calculations will take. Given the power of
 current PCs, it's hoped that this will be moot to any reasonable number of points. Due to the issue of rounding
 error, the last position is set by the upper range entered. All values in between are calculated.

Steps	    506
Min Freq	10
Max Freq	20000
Span	    2000
Increm	    1.015165133
 */

namespace Core.Data
{
    /// <summary>
    /// FrequencyRange is used to create and initialize the frequency points to be used for calculations.
    /// All imported measurements will eventually be interpolated to this range.
    /// </summary>
    public class FrequencyRange
    {
        // These will need to be changed to have a different range
        int workingSamplePoints = 506;
        int groupDelaySamplePoints;
        int freqMin = 10;
        int freqMax = 20000;

        //public FrequencyRange frequencies;
        
        double[,] groupDelay;

        // The freq array has hard-coded values taken from PCD v6.20
        static double[] freqPCD = { 10.00,10.15,10.31,10.46,10.62,10.78,10.95,11.11,11.28,11.45,11.62,11.80,11.98,12.16,12.35,12.53,12.72,12.92,13.11,13.31,13.51,13.72,13.93,14.14,14.35,14.57,14.79,15.01,15.24,15.47,15.71,15.95,16.19,16.43,16.68,16.93,17.19,17.45,17.72,17.99,18.26,18.54,18.82,19.10,19.39,19.69,19.98,20.29,20.59,20.91,21.22,21.55,21.87,22.20,22.54,22.88,23.23,23.58,23.94,24.30,24.67,25.05,25.43,25.81,26.20,26.60,27.00,27.41,27.83,28.25,28.68,29.11,29.56,30.00,30.46,30.92,31.39,31.87,32.35,32.84,33.34,33.84,34.36,34.88,35.41,35.94,36.49,37.04,37.60,38.17,38.75,39.34,39.94,40.54,41.16,41.78,42.42,43.06,43.71,44.37,45.05,45.73,46.42,47.13,47.84,48.57,49.30,50.05,50.81,51.58,52.36,53.16,53.96,54.78,55.61,56.46,57.31,58.18,59.06,59.96,60.87,61.79,62.73,63.68,64.65,65.63,66.62,67.63,68.66,69.70,70.76,71.83,72.92,74.03,75.15,76.29,77.44,78.62,79.81,81.02,82.25,83.50,84.76,86.05,87.35,88.68,90.02,91.39,92.77,94.18,95.61,97.06,98.53,100.03,101.54,103.08,104.65,106.23,107.84,109.48,111.14,112.83,114.54,116.27,118.04,119.83,121.64,123.49,125.36,127.26,129.19,131.15,133.14,135.16,137.21,139.29,141.40,143.55,145.72,147.93,150.18,152.45,154.77,157.11,159.50,161.92,164.37,166.86,169.39,171.96,174.57,177.22,179.91,182.63,185.40,188.22,191.07,193.97,196.91,199.89,202.93,206.00,209.13,212.30,215.52,218.79,222.11,225.47,228.89,232.36,235.89,239.47,243.10,246.78,250.53,254.32,258.18,262.10,266.07,270.11,274.20,278.36,282.58,286.87,291.22,295.64,300.12,304.67,309.29,313.98,318.74,323.58,328.48,333.46,338.52,343.66,348.87,354.16,359.53,364.98,370.52,376.13,381.84,387.63,393.51,399.48,405.53,411.68,417.93,424.26,430.70,437.23,443.86,450.59,457.43,464.36,471.40,478.55,485.81,493.18,500.66,508.25,515.96,523.78,531.72,539.79,547.97,556.28,564.72,573.28,581.98,590.80,599.76,608.86,618.09,627.47,636.98,646.64,656.45,666.40,676.51,686.77,697.18,707.76,718.49,729.39,740.45,751.68,763.08,774.65,786.40,798.32,810.43,822.72,835.19,847.86,860.72,873.77,887.02,900.47,914.13,927.99,942.07,956.35,970.86,985.58,1000.53,1015.70,1031.10,1046.74,1062.61,1078.73,1095.09,1111.69,1128.55,1145.67,1163.04,1180.68,1198.58,1216.76,1235.21,1253.94,1272.96,1292.27,1311.86,1331.76,1351.95,1372.46,1393.27,1414.40,1435.85,1457.62,1479.73,1502.17,1524.95,1548.08,1571.55,1595.39,1619.58,1644.14,1669.07,1694.39,1720.08,1746.17,1772.65,1799.53,1826.82,1854.52,1882.65,1911.20,1940.18,1969.61,1999.47,2029.80,2060.58,2091.83,2123.55,2155.76,2188.45,2221.64,2255.33,2289.53,2324.25,2359.50,2395.28,2431.60,2468.48,2505.92,2543.92,2582.50,2621.66,2661.42,2701.78,2742.75,2784.35,2826.57,2869.44,2912.95,2957.13,3001.97,3047.50,3093.71,3140.63,3188.26,3236.61,3285.69,3335.52,3386.10,3437.45,3489.58,3542.50,3596.23,3650.76,3706.13,3762.33,3819.39,3877.31,3936.11,3995.80,4056.40,4117.91,4180.36,4243.76,4308.12,4373.45,4439.77,4507.10,4575.45,4644.84,4715.28,4786.79,4859.38,4933.07,5007.88,5083.83,5160.93,5239.19,5318.65,5399.30,5481.18,5564.31,5648.69,5734.35,5821.32,5909.60,5999.22,6090.20,6182.55,6276.31,6371.49,6468.12,6566.21,6665.79,6766.87,6869.49,6973.67,7079.43,7186.79,7295.78,7406.42,7518.74,7632.76,7748.51,7866.02,7985.31,8106.41,8229.34,8354.14,8480.83,8609.45,8740.01,8872.55,9007.11,9143.70,9282.37,9423.13,9566.04,9711.11,9858.38,10007.88,10159.65,10313.72,10470.13,10628.91,10790.10,10953.74,11119.85,11288.48,11459.68,11633.46,11809.89,11988.99,12170.80,12355.37,12542.74,12732.95,12926.05,13122.08,13321.08,13523.09,13728.17,13936.36,14147.71,14362.26,14580.06,14801.17,15025.63,15253.50,15484.82,15719.65,15958.04,16200.05,16445.72,16695.13,16948.31,17205.33,17466.25,17731.13,18000.03,18273.00,18550.11,18831.43,19117.01,19406.92,19701.23,20000.00 };

        static double[] _dynamicFreqs;
        static double[] _groupDelayFreqs;

        /// <summary>
        /// dynamicFreqs is the publicly accessible form of the frequency list array.
        /// </summary>
        public static double[] dynamicFreqs
        { 
            get { return _dynamicFreqs; }
            private set { _dynamicFreqs = value; }
        }

        public static double[] groupDelayFreqs
        {
            get { return _groupDelayFreqs; }
            private set { _groupDelayFreqs = value; }
        }
        
        public FrequencyRange() 
        { 
            // Create the frequency range array for use throughout the program.
            SetDefaultFrequencyRange();
            groupDelaySamplePoints = workingSamplePoints - 1;
        }

        /// <summary>
        /// The frequency range and incremental multiplier are based on the following equation:
        /// x* y^n = z where:               Initially ==> 10 * (y^505) = 20000
        ///     x = start frequency (10)
        ///     y = incremental multiplier 
        ///     n = number of sample points - 1 (initial point x is not counted for the increment)
        ///         Also is the nth root of the range or span
        ///     z = end frequency (20000)
        /// therefore:
        ///     n = 505root(20000/10)
        ///
        /// InitFrequencyRange initalizes the sample point range to match the original PCD v7.0 for now.
        /// Later it may be called if a user-selectable range is implemented.
        /// </summary>
        /// <returns>freqs (double)</returns>
        public void SetDefaultFrequencyRange()
        {
            int points = workingSamplePoints;
            double increment = Math.Pow(freqMax / freqMin, 1.0 / (points - 1));
            double[] freqs = new double[points];
            freqs[0] = freqMin;
            for (int i = 1; i < (points - 1); i++)
            {
                freqs[i] = freqs[i - 1] * increment;
            }
            freqs[points - 1] = freqMax;
            _dynamicFreqs = freqs;
            return;
        }

        private void SetGroupDelayFrequencies()
        {
            groupDelay = new double[FrequencyRange.dynamicFreqs.Count() - 1, 2];

            double freq1 = 0, freq2 = 0, phase1 = 0, phase2 = 0, slope = 0;
            
            for (int i = 0; i < _dynamicFreqs.Count() - 1; i++ )
            {
                freq1 = 0; phase1 = 0; phase2 = 0;

                freq1 = _dynamicFreqs[i];
                freq2 = _dynamicFreqs[i + 1];

                slope = -((phase2 - phase1) / (freq2 - freq1)) / 360.0;

                groupDelay[i, 0] = freq1 + ((freq2 - freq1) / 2);
                groupDelay[i, 1] = 0; // Default flat group delay
            }

        }

        /// <summary>
        /// SetFrequencies is set up so that at some point in the future the number of sample points 
        /// will be user selectable.
        /// </summary>
        /// <param name="freqMin"></param>
        /// <param name="freqMax"></param>
        /// <param name="points"></param>
        /// <returns></returns>
        //public double[] SetNewFrequencyRange(int freqMin, int freqMax, int points)
        public void SetNewFrequencyRange(int freqMin, int freqMax, int points)
        {
            double increment = Math.Pow(freqMax / freqMin, 1.0 / (points - 1));
            double[] myFreqs = new double[points + 1]; // Add one extra to allow for interpolation to the last point if data exists above it.
            myFreqs[0] = freqMin;
            for (int i = 1; i < (points -1); i++)
            {
                myFreqs[i] = myFreqs[i-1] * increment;
            }
            myFreqs[points - 2] = freqMax; // This leaves the last slot empty (? not sure what I meant when I wrote this)
            _dynamicFreqs = myFreqs;
            return;
        }
    }
}
