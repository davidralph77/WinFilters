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
using Core.Targeting;


namespace Core.Filtering.SPL
{
    /// <summary>
    /// The Filters class contains the methods that calculate all of the various target curves.
    /// The available targets are Butterworth, Bessel and Linkiwitz-Riley.
    /// The arguments passed in included items such as "Section" (HP, BP, LP), "Order" and "Gain".
    /// </summary>
    public class FilterSPL
    {
        public FilterSPL() { }

        /* 
         * Bilinear Transform
         * 
                    b0 + b1*z^-1 + b2*z^-2
            H(z) = ------------------------
                    a0 + a1*z^-1 + a2*z^-2
         */

        public static double BesselSPL(int order, double gain, double s)
        {
            double magnitude = 1.0;
            double numerator = 1.0, denominator = 1.0;

            switch (order)
            {
                case 1: 
                    {
                        //numerator = 3;
                        //denominator = Math.Sqrt(Math.Pow(s, 2) + 1);
                        break; 
                    }
                case 2:
                    {
                        numerator = 3;
                        denominator = Math.Sqrt(Math.Pow(s, 4) + (3 * Math.Pow(s, 2)) + 9);
                        break;
                    }
                case 3:
                    {
                        numerator = 15; 
                        denominator = Math.Sqrt(Math.Pow(s, 6) + 6 * Math.Pow(s, 4) + (45 * Math.Pow(s, 2)) + 225);
                        break;
                    }
                case 4:
                    {
                        numerator = 105;
                        denominator = Math.Sqrt(Math.Pow(s, 8) + 10 * Math.Pow(s, 6) + 135 * Math.Pow(s, 4) + (1575 * Math.Pow(s, 2)) + 11025);
                        break;
                    }
                #region Unused orders
                /*
                case 5:
                    { 
                        numerator = 945; 
                        denominator = Math.Sqrt(Math.Pow(s, 10) + 15 * Math.Pow(s, 8) + 315 * Math.Pow(s, 6) + 6300 * Math.Pow(s, 4) + 99225 * Math.Pow(s, 2) + 893025);
                        break;
                    }
                case 6:
                    {
                        numerator = 10395;
                        denominator = Math.Sqrt(Math.Pow(s, 12) + 21 * Math.Pow(s, 10) + 630 * Math.Pow(s, 8) + 18900 * Math.Pow(s, 6) + 496125 * Math.Pow(s, 4) + (9823275 * Math.Pow(s, 2)) + 108056025);
                        break;
                    }
*/

                #endregion
                default: { break; }
            }
            magnitude = numerator / denominator;
            return magnitude;
        }

        /// <summary>
        /// The Butterworth method calculates the value for a discrete point for the order indicated and at the 
        /// specified frequency point. The Butterworth filter equation is the generic form for any nth order.
        /// Lowpass:
        ///               1 / sqrt( 1 + gain(2pi*f/2pi*Fc)^2n )
        /// Highpass:
        ///             (f/Fc)^2 / sqrt( 1 + gain(2pi*f/2pi*Fc)^2n )
        /// </summary>
        public static double ButterworthSPL(Target target, AcousticTargets.FilterName name, double order, double gain, double s)
        {
            if (name == AcousticTargets.FilterName.LinkwitzRiley)
            {
                order /= 2; // Since an LR is a BW^2 we have to half the specified order before calculating
            }
            double power = 2 * order;
            double magnitude = 1 / (Math.Sqrt(1 + (Math.Pow(s, power))));
            return magnitude;
        }

        #region Unused Generic

        //public static double FilterByOddQ(Filter.Section section, Target target, double gain, double Q, double f)
        //{
        //    double magnitude;
        //    int order;
        //    double Fc;
        //    if (section == Filter.Section.LP)
        //    {
        //        order = target.orderLP;
        //        Fc = target.frequencyLP;
        //    }
        //    else if (section == Filter.Section.HP)
        //    {
        //        order = target.orderHP;
        //        Fc = target.frequencyHP;
        //    }
        //    else { return 0.0; }

        //    double s = f / Fc;
        //    Complex plus = (Q * (2 * s + Math.Sqrt(4 - Math.Pow(1 / Q, 2))));
        //    Complex minus = (Q * (2 * s - Math.Sqrt(4 - Math.Pow(1 / Q, 2))));


        //    if (section == Filter.Section.LP)
        //    {
        //        magnitude = Math.Atan2(plus.Imaginary, plus.Magnitude) - Math.Atan2(minus.Imaginary, minus.Magnitude);
        //    }
        //    else
        //    {
        //        magnitude = -Math.Atan2(plus.Imaginary, plus.Magnitude) + Math.Atan2(minus.Imaginary, minus.Magnitude);
        //    }

        //    return magnitude;
        //}

        #endregion
        
    }
}
