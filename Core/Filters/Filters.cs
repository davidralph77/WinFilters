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
using System.Numerics;
using System.Windows.Forms;
using Core.DriverCore;
using Core.Filtering.Core;
using Core.Filtering.SPL;
using Core.Filtering.Phase;
using Core.Targeting;


namespace Core.Filtering
{
    /// <summary>
    /// The Filters class contains the methods that calculate all of the various target curves.
    /// The available targets are Butterworth, Bessel and Linkiwitz-Riley.
    /// The arguments passed in included items such as "Section" (HP, BP, LP), "Order" and "Gain".
    /// </summary>
    [Serializable]
    //public sealed class Filters : FilterBase
    public sealed class Filters
    {
        public Filters() { }

        /* 
         * Bilinear Transform
         * 
                    b0 + b1*z^-1 + b2*z^-2
            H(z) = ------------------------
                    a0 + a1*z^-1 + a2*z^-2
         */

        /// <summary>
        /// Overload for the case where the output values aren't needed by the caller.
        /// Only the driver target data will be updated.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="frequencyRange"></param>
        /// <param name="FcLP"></param>
        /// <param name="FcHP"></param>
        public static void Calculate(DriverCore.DriverCore driver, double[] frequencyRange, double FcLP, double FcHP)
        {
            double splAtFcLP;
            double phaseAtFcLP;
            double splAtFcHP;
            double phaseAtFcHP;
            double factoredFcLP;
            double factoredFcHP;
            Filters.Calculate(driver, frequencyRange
                    , FcLP, out splAtFcLP, out phaseAtFcLP
                    , FcHP, out splAtFcHP, out phaseAtFcHP
                    , out factoredFcLP, out factoredFcHP);
        }

        /// <summary>
        /// Calculates the response for a theoretical filter. These are used for design acoustic filter targets, not electrical.
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="frequencyRange"></param>
        /// <param name="phaseAtFc"></param>
        /// <param name="hpIsInverted"></param>
        /// <returns>besselFactoredFc, but only if it is for a Bessel.</returns>
        public static void Calculate(DriverCore.DriverCore driver, double[] frequencyRange
                                       , double FcLP, out double splAtFcLP, out double phaseAtFcLP
                                       , double FcHP, out double splAtFcHP, out double phaseAtFcHP
                                       , out double factoredFcLP, out double factoredFcHP
                                       , bool hpIsInverted = false, double offsetMm = 0.0, bool lpIsOffset = false, bool hpIsOffset = false)
        {
            double gain = 1; // Epsilon - This allows change in gain later
            double minInterpFreq = frequencyRange[0];
            int rangeUpperLimit = frequencyRange.Count() - 1; // Count of the frequency specified range
            double maxInterpFreq = frequencyRange[rangeUpperLimit]; // Upper frequency limit of the interpolations to be made
            double interpFreq; // Used for iterations
            double[,] interpMagPhaseLP = new double[frequencyRange.Count(), 3]; // LP mag and phase.
            double[,] interpMagPhaseHP = new double[frequencyRange.Count(), 3]; // HP mag and phase.
            double[,] interpMagPhase   = new double[frequencyRange.Count(), 3]; // Array to hold interp mag and phase.
            double gainLP = 1.0;
            double gainHP = 1.0;
            double gainAtFcLP = 0.0;
            double gainAtFcHP = 0.0;
            double phaseLP = 0.0;
            double phaseHP = 0.0;

            // These are calculated on each loop, but it was needed to be able to locate the specific set of two
            // frequencies between which the Fc is located.
            splAtFcLP = 0.0;
            splAtFcHP = 0.0;
            phaseAtFcLP = 0.0;
            phaseAtFcHP = 0.0;
            factoredFcLP = 1.0;
            factoredFcHP = 1.0;

            for (int i = 0; i < rangeUpperLimit; i++)
            {
                interpFreq = frequencyRange[i]; // Interpolation frequency points to use from the range specified.
                interpMagPhase[i, 0] = interpFreq;
                interpMagPhase[i, 1] = 1; // SPL default
                interpMagPhase[i, 2] = 0; // Phase default

                interpMagPhaseLP[i, 0] = interpMagPhase[i, 0];
                interpMagPhaseLP[i, 1] = interpMagPhase[i, 1];
                interpMagPhaseLP[i, 2] = interpMagPhase[i, 2];
                interpMagPhaseHP[i, 0] = interpMagPhase[i, 0];
                interpMagPhaseHP[i, 1] = interpMagPhase[i, 1];
                interpMagPhaseHP[i, 2] = interpMagPhase[i, 2];

                double s;
                double besselFactor = 1.0; // Multiply the Fc (divide into s)
                // Add offset to LP or HP based on user input
                double offsetDelay = (2 * Math.PI * interpMagPhase[i, 0] * (offsetMm / 344000));
                double offsetDelayAtFcLP = (2 * Math.PI * FcLP * (offsetMm / 344000));
                double offsetDelayAtFcHP = (2 * Math.PI * FcHP * (offsetMm / 344000));
                double offsetLP     = 0, offsetHP     = 0;
                double offsetLpAtFc = 0, offsetHpAtFc = 0;
                if (lpIsOffset) { offsetLP = offsetDelay; offsetLpAtFc = offsetDelayAtFcLP; }
                if (hpIsOffset) { offsetHP = offsetDelay; offsetHpAtFc = offsetDelayAtFcHP; }

                driver.target.splInterpolated[i, 1] = 0;
                driver.target.splInterpolated[i, 2] = 0;

                if ((driver.target.section == FilterCore.Section.LP) || (driver.target.section == FilterCore.Section.BP))
                {
                    s = interpFreq / driver.target.frequencyLP;

                    switch (driver.target.nameLP)
                    {
                        case AcousticTargets.FilterName.Butterworth:
                            {
                                factoredFcLP = driver.target.frequencyLP;
                                gainLP = FilterSPL.ButterworthSPL(driver.target, driver.target.nameLP, driver.target.orderLP, gain, s);
                                phaseLP = FilterPhase.ButterworthPhase(driver.target.orderLP, s) + offsetLP;
                                phaseLP = FilterCore.RotatePhase(phaseLP);
                                phaseAtFcLP = FilterPhase.ButterworthPhase(driver.target.orderLP, s = 1) + offsetLpAtFc;
                                phaseAtFcLP = (FilterCore.RotatePhase(phaseAtFcLP) * 180.0) / Math.PI;
                                gainAtFcLP = FilterSPL.ButterworthSPL(driver.target, driver.target.nameLP, driver.target.orderLP, gain, s = 1);
                                break;
                            }
                        case AcousticTargets.FilterName.LinkwitzRiley:
                            {
                                if (driver.target.fullTypeLP == null) { return; } // Handle odd-order values not permissible for L-R

                                factoredFcLP = driver.target.frequencyLP;
                                gainLP = Math.Pow(FilterSPL.ButterworthSPL(driver.target, driver.target.nameLP, driver.target.orderLP, gain, s), 2);
                                phaseLP = FilterPhase.LinkwitzRileyPhase(driver.target.orderLP, s) + offsetLP;
                                phaseLP = FilterCore.RotatePhase(phaseLP);
                                phaseAtFcLP = FilterPhase.LinkwitzRileyPhase(driver.target.orderLP, s = 1) + offsetLpAtFc;
                                phaseAtFcLP = FilterCore.RotatePhase(phaseAtFcLP) * 180 / Math.PI;
                                gainAtFcLP = Math.Pow(FilterSPL.ButterworthSPL(driver.target, driver.target.nameLP, driver.target.orderLP, gain, s = 1), 2);
                                break;
                            }
                        case AcousticTargets.FilterName.Bessel:
                            {
                                factoredFcLP = driver.target.frequencyLP;
                                gainLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s);
                                phaseLP = FilterPhase.BesselPhase(driver.target.orderLP, s) + offsetLP;
                                phaseLP = FilterCore.RotatePhase(phaseLP);
                                phaseAtFcLP = FilterPhase.BesselPhase(driver.target.orderLP, s = 1) + offsetLpAtFc;
                                phaseAtFcLP = FilterCore.RotatePhase(phaseAtFcLP) * 180 / Math.PI;
                                gainAtFcLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s = 1);
                                break;
                            }
                        case AcousticTargets.FilterName.BesselPhaseMatch:
                            {
                                switch (driver.target.orderLP)
                                {
                                    case 2: 
                                        { besselFactor = 0.58;  break; }
                                    case 3:
                                        { besselFactor = 1.0;   break; }
                                    case 4: 
                                        { besselFactor = 0.31;  break; }
                                }
                                s /= besselFactor;
                                factoredFcLP = driver.target.frequencyLP * besselFactor;
                                gainLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s);
                                phaseLP = FilterPhase.BesselPhase(driver.target.orderLP, s) + offsetLP;
                                phaseLP = FilterCore.RotatePhase(phaseLP);
                                phaseAtFcLP = FilterPhase.BesselPhase(driver.target.orderLP, s = 1 / besselFactor) + offsetLpAtFc;
                                phaseAtFcLP = FilterCore.RotatePhase(phaseAtFcLP) * 180 / Math.PI;
                                gainAtFcLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s = 1 / besselFactor);
                                break;
                            }
                        case AcousticTargets.FilterName.BesselFlattest:
                            {
                                switch (driver.target.orderLP)
                                {
                                    case 2:
                                        { besselFactor = 0.51;  break; }
                                    case 3:
                                        { besselFactor = 1.0;   break; }
                                    case 4:
                                        { besselFactor = 0.40;  break; }
                                }
                                s /= besselFactor;
                                factoredFcLP = driver.target.frequencyLP * besselFactor;
                                gainLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s);
                                phaseLP = FilterPhase.BesselPhase(driver.target.orderLP, s) + offsetLP;
                                phaseLP = FilterCore.RotatePhase(phaseLP);
                                phaseAtFcLP = FilterPhase.BesselPhase(driver.target.orderLP, s = 1 / besselFactor) + offsetLpAtFc;
                                phaseAtFcLP = FilterCore.RotatePhase(phaseAtFcLP) * 180 / Math.PI;
                                gainAtFcLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s = 1 / besselFactor);
                                break;
                            }
                        case AcousticTargets.FilterName.Bessel3db:
                            {
                                switch (driver.target.orderLP)
                                {
                                    case 2:
                                        { besselFactor = 0.73;  break; }
                                    case 3:
                                        { besselFactor = 1.0;   break; }
                                    case 4:
                                        { besselFactor = 0.48;  break; }
                                }
                                s /= besselFactor;
                                factoredFcLP = driver.target.frequencyLP * besselFactor;
                                gainLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s);
                                phaseLP = FilterPhase.BesselPhase(driver.target.orderLP, s) + offsetLP;
                                phaseLP = FilterCore.RotatePhase(phaseLP);
                                phaseAtFcLP = FilterPhase.BesselPhase(driver.target.orderLP, s = 1 / besselFactor) + offsetLpAtFc;
                                phaseAtFcLP = FilterCore.RotatePhase(phaseAtFcLP) * 180 / Math.PI;
                                gainAtFcLP = FilterSPL.BesselSPL(driver.target.orderLP, gain, s = 1 / besselFactor);
                                break;
                            }
                        case AcousticTargets.FilterName.None:
                            {
                                break;
                            }
                        default: { MessageBox.Show("Section.LP (.typeLP) is not correct."); break; }
                    }
                }

                if ((driver.target.section == FilterCore.Section.HP) || (driver.target.section == FilterCore.Section.BP))
                {
                    s = interpFreq / driver.target.frequencyHP; // f/Fc

                    switch (driver.target.nameHP)
                    {
                        case AcousticTargets.FilterName.Butterworth:
                            {
                                factoredFcHP = driver.target.frequencyHP;
                                gainHP = FilterSPL.ButterworthSPL(driver.target, driver.target.nameHP, driver.target.orderHP, gain, 1 / s);
                                phaseHP = FilterPhase.ButterworthPhase(driver.target.orderHP, s, true) + offsetHP;
                                offsetHpAtFc *= factoredFcHP; // Determine the offset using the factored Fc.
                                phaseAtFcHP = FilterPhase.ButterworthPhase(driver.target.orderHP, s = 1, true) + offsetHpAtFc;
                                if (hpIsInverted) { phaseHP =FilterCore.InvertPhase(phaseHP); phaseAtFcHP = FilterCore.InvertPhase(phaseAtFcHP) * 180 / Math.PI; }
                                else              { phaseHP =FilterCore.RotatePhase(phaseHP); phaseAtFcHP = FilterCore.RotatePhase(phaseAtFcHP) * 180 / Math.PI; }
                                gainAtFcHP = FilterSPL.ButterworthSPL(driver.target, driver.target.nameHP, driver.target.orderHP, gain, s = 1);
                                break;
                            }
                        case AcousticTargets.FilterName.LinkwitzRiley:
                            {
                                if (driver.target.fullTypeHP == null) { return; } // Handle odd-order values not permissible for L-R

                                factoredFcHP = driver.target.frequencyHP;
                                if (null == driver.target.fullTypeHP) { return; } // Handle odd-order values not permissible for L-R
                                gainHP = Math.Pow(FilterSPL.ButterworthSPL(driver.target, driver.target.nameHP, driver.target.orderHP, gain, 1 / s), 2); 
                                phaseHP = FilterPhase.LinkwitzRileyPhase(driver.target.orderHP, s, true) + offsetHP;
                                offsetHpAtFc *= factoredFcHP; // Determine the offset using the factored Fc.
                                phaseAtFcHP = FilterPhase.LinkwitzRileyPhase(driver.target.orderHP, s = 1, true) + offsetHpAtFc;
                                if (hpIsInverted) { phaseHP =FilterCore.InvertPhase(phaseHP); phaseAtFcHP = FilterCore.InvertPhase(phaseAtFcHP) * 180 / Math.PI; }
                                else              { phaseHP =FilterCore.RotatePhase(phaseHP); phaseAtFcHP = FilterCore.RotatePhase(phaseAtFcHP) * 180 / Math.PI; }
                                gainAtFcHP = Math.Pow(FilterSPL.ButterworthSPL(driver.target, driver.target.nameHP, driver.target.orderHP, gain, s = 1), 2);
                                break;
                            }
                        case AcousticTargets.FilterName.Bessel:
                            {
                                factoredFcHP = driver.target.frequencyHP;
                                gainHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, 1 / s);
                                phaseHP = FilterPhase.BesselPhase(driver.target.orderHP, s, true) + offsetHP;
                                offsetHpAtFc *= factoredFcHP; // Determine the offset using the factored Fc.
                                phaseAtFcHP = FilterPhase.BesselPhase(driver.target.orderHP, s = 1, true) + offsetHpAtFc;
                                if (hpIsInverted) { phaseHP =FilterCore.InvertPhase(phaseHP); phaseAtFcHP = FilterCore.InvertPhase(phaseAtFcHP) * 180 / Math.PI; }
                                else              { phaseHP =FilterCore.RotatePhase(phaseHP); phaseAtFcHP = FilterCore.RotatePhase(phaseAtFcHP) * 180 / Math.PI; }
                                gainAtFcHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, s = 1);
                                break;
                            }
                        case AcousticTargets.FilterName.BesselPhaseMatch:
                            {
                                switch (driver.target.orderHP)
                                {
                                    case 2:
                                        { besselFactor = 1 / 0.58;  break; }
                                    case 3:
                                        { besselFactor = 1.0;       break; }
                                    case 4:
                                        { besselFactor = 1 / 0.31;  break; }
                                }
                                s /= besselFactor;
                                factoredFcHP = driver.target.frequencyHP * besselFactor;
                                gainHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, 1 / s);
                                phaseHP = FilterPhase.BesselPhase(driver.target.orderHP, s, true) + offsetHP;
                                offsetHpAtFc *= factoredFcHP; // Determine the offset using the factored Fc.
                                phaseAtFcHP = FilterPhase.BesselPhase(driver.target.orderHP, s = 1 / besselFactor, true) + offsetHpAtFc;
                                if (hpIsInverted) { phaseHP =FilterCore.InvertPhase(phaseHP); phaseAtFcHP = FilterCore.InvertPhase(phaseAtFcHP) * 180 / Math.PI; }
                                else              { phaseHP =FilterCore.RotatePhase(phaseHP); phaseAtFcHP = FilterCore.RotatePhase(phaseAtFcHP) * 180 / Math.PI; }
                                gainAtFcHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, s = 1 * besselFactor);
                                break;
                            }
                        case AcousticTargets.FilterName.BesselFlattest:
                            {
                                switch (driver.target.orderHP)
                                {
                                    case 2:
                                        { besselFactor = 1 / 0.51;  break; }
                                    case 3:
                                        { besselFactor = 1.0;       break; }
                                    case 4:
                                        { besselFactor = 1 / 0.40;  break; }
                                }
                                s /= besselFactor;
                                factoredFcHP = driver.target.frequencyHP * besselFactor;
                                gainHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, 1 / s);
                                phaseHP = FilterPhase.BesselPhase(driver.target.orderHP, s, true) + offsetHP;
                                offsetHpAtFc *= factoredFcHP; // Determine the offset using the factored Fc.
                                phaseAtFcHP = FilterPhase.BesselPhase(driver.target.orderHP, s = 1 / besselFactor, true) + offsetHpAtFc;
                                if (hpIsInverted) { phaseHP =FilterCore.InvertPhase(phaseHP); phaseAtFcHP = FilterCore.InvertPhase(phaseAtFcHP) * 180 / Math.PI; }
                                else              { phaseHP =FilterCore.RotatePhase(phaseHP); phaseAtFcHP = FilterCore.RotatePhase(phaseAtFcHP) * 180 / Math.PI; }
                                gainAtFcHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, s = 1 * besselFactor);
                                break;
                            }
                        case AcousticTargets.FilterName.Bessel3db:
                            {
                                switch (driver.target.orderHP)
                                {
                                    case 2:
                                        { besselFactor = 1 / 0.73;  break; }
                                    case 3:
                                        { besselFactor = 1.0;       break; }
                                    case 4:
                                        { besselFactor = 1 / 0.48;  break; }
                                }
                                s /= besselFactor;
                                factoredFcHP = driver.target.frequencyHP * besselFactor;
                                gainHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, 1 / s);
                                phaseHP = FilterPhase.BesselPhase(driver.target.orderHP, s, true) + offsetHP;
                                offsetHpAtFc *= factoredFcHP; // Determine the offset using the factored Fc.
                                phaseAtFcHP = FilterPhase.BesselPhase(driver.target.orderHP, s = 1 / besselFactor, true) + offsetHpAtFc;
                                if (hpIsInverted) { phaseHP =FilterCore.InvertPhase(phaseHP); phaseAtFcHP = FilterCore.InvertPhase(phaseAtFcHP) * 180 / Math.PI; }
                                else              { phaseHP =FilterCore.RotatePhase(phaseHP); phaseAtFcHP = FilterCore.RotatePhase(phaseAtFcHP) * 180 / Math.PI; }
                                gainAtFcHP = FilterSPL.BesselSPL(driver.target.orderHP, gain, s = 1 * besselFactor);
                                break;
                            }
                        case AcousticTargets.FilterName.None:
                            {
                                break;
                            }
                        default: { MessageBox.Show("Section.HP (.typeHP) is not correct. Must be null."); break; }
                    }
                }

                double factorLP = 0.0;
                double factorFcLP = 0.0;
                Complex complexLP = 0.0;

                double factorHP = 0.0;
                double factorFcHP = 0.0;
                Complex complexHP = 0.0;

                interpMagPhase[i, 0] = interpFreq;

                switch (driver.target.section)
                {
                    case FilterCore.Section.LP:
                        {
                            factorLP = 20 * Math.Log10(gainLP);
                            interpMagPhaseLP[i, 1] = driver.target.targetMag + factorLP; // Subtract filter factorLP (in db) from maximum gain (sensitivity)
                            interpMagPhaseLP[i, 2] = phaseLP;
                            complexLP = Complex.FromPolarCoordinates(factorLP, phaseLP);

                            factorFcLP = 20 * Math.Log10(gainAtFcLP);
                            splAtFcLP = driver.target.targetMag + factorFcLP;
                            
                            interpMagPhase[i, 1] = interpMagPhaseLP[i, 1];
                            interpMagPhase[i, 2] = interpMagPhaseLP[i, 2] * 180 / Math.PI;
                            break;
                        }
                    case FilterCore.Section.HP:
                        {
                            factorHP = 20 * Math.Log10(gainHP);
                            interpMagPhaseHP[i, 1] = driver.target.targetMag + factorHP; // Subtract filter factorHP (in db) from maximum gain (sensitivity)
                            interpMagPhaseHP[i, 2] = phaseHP;
                            complexHP = Complex.FromPolarCoordinates(factorHP, phaseHP);

                            factorFcHP = 20 * Math.Log10(gainAtFcHP);
                            splAtFcHP = driver.target.targetMag + factorFcHP;
                            
                            interpMagPhase[i, 1] = interpMagPhaseHP[i, 1];
                            interpMagPhase[i, 2] = interpMagPhaseHP[i, 2] * 180 / Math.PI;
                            break;
                        }
                    case FilterCore.Section.BP:
                        {
                            // Update the out arguments
                            factorFcLP = 20 * Math.Log10(gainAtFcLP);
                            factorFcHP = 20 * Math.Log10(gainAtFcHP);
                            splAtFcLP = driver.target.targetMag + factorFcLP;
                            splAtFcHP = driver.target.targetMag + factorFcHP;

                            // Calculate the bandpass
                            //Complex target        = Complex.FromPolarCoordinates(driver.target.targetMag, 0.0);
                            Complex complexGainLP = Complex.FromPolarCoordinates(gainLP, phaseLP);
                            Complex complexGainHP = Complex.FromPolarCoordinates(gainHP, phaseHP);

                            Complex complexGainSum  = complexGainLP * complexGainHP;
                            double totalGainDb = driver.target.targetMag + (20 * Math.Log10(complexGainSum.Magnitude));

                            interpMagPhase[i, 1] = totalGainDb;
                            interpMagPhase[i, 2] = complexGainSum.Phase * 180 / Math.PI;
                            break;
                        }
                }
            }

            driver.target.splInterpolated = interpMagPhase;

            return;
        }

    }
}
