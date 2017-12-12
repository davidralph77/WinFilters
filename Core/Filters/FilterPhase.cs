using System;


namespace Core.Filtering.Phase
{
    /// <summary>
    /// The Filters class contains the methods that calculate all of the various target curves.
    /// The available targets are Butterworth, Bessel and Linkiwitz-Riley.
    /// The arguments passed in included items such as "Section" (HP, BP, LP), "Order" and "Gain".
    /// </summary>
    public class FilterPhase
    {
        public FilterPhase() { }

        public static double ButterworthPhase(int order, double s, bool HP = false)
        {
            double phase = 0.0;

            switch (order)
            {
                case 1:
                    {
                        phase = -Math.Atan2(s, 1);
                        if (HP) { phase += Math.PI / 2; }
                        break;
                    }
                case 2:
                    {
                        phase = -Math.Atan2(Math.Sqrt(2) * s, 1 - Math.Pow(s, 2));
                        if (HP) { phase += Math.PI; }
                        break;
                    }
                case 3:
                    {
                        phase = -Math.Atan2((2 * s) - Math.Pow(s, 3), 1 + (-2 * Math.Pow(s, 2)));
                        if (HP) { phase +=  (3.0 * (Math.PI / 2.0)); }
                        break;
                    }
                case 4:
                    {
                        phase = -Math.Atan2(2.613126 * (s - Math.Pow(s, 3)), 1 + (-3.4142136 * Math.Pow(s, 2)) + Math.Pow(s, 4));
                        break;
                    }
                case 5:
                    {
                        phase = -Math.Atan2((3.236068 * s) + (-5.236068 * Math.Pow(s, 3)) + Math.Pow(s, 5),
                                            1 + (-5.236068 * Math.Pow(s, 2)) + (3.236068 * Math.Pow(s, 4)));
                        if (HP) { phase += Math.PI / 2; }
                        break;
                    }
                case 6:
                    {
                        // phase = -Math.Atan2((3.7338 * s) + (-8.7866 * Math.Pow(s, 3)) + (3.7338 * Math.Pow(s, 5)),
                        //                    1 + (-7.2130 * Math.Pow(s, 2)) + (7.2130 * Math.Pow(s, 4)) - Math.Pow(s, 6));
                        phase = -Math.Atan2((3.8637 * s) + (-9.1416 * Math.Pow(s, 3)) + (3.8637 * Math.Pow(s, 5)),
                                            1 + (-7.4641 * Math.Pow(s, 2)) + (7.4641 * Math.Pow(s, 4)) - Math.Pow(s, 6));
                        if (HP) { phase += Math.PI; }
                        break;
                    }
                case 7:
                    {
                        break;
                    }
                case 8:
                    {
                        //phase = -Math.Atan2((5.1048 * s) + (-21.7438 * Math.Pow(s, 3)) + (21.7438 * Math.Pow(s, 5)) + (-5.1048 * Math.Pow(s, 7)),
                        //                     1 + (-13.0745 * Math.Pow(s, 2)) + (25.568 * Math.Pow(s, 4)) + (-13.0745 * Math.Pow(s, 6)) + Math.Pow(s, 8));
                        phase = -Math.Atan2((5.1258 * s) + (-21.8462 * Math.Pow(s, 3)) + (21.8462 * Math.Pow(s, 5)) + (-5.1258 * Math.Pow(s, 7)),
                                             1 + (-13.1371 * Math.Pow(s, 2)) + (25.6884 * Math.Pow(s, 4)) + (-13.1371 * Math.Pow(s, 6)) + Math.Pow(s, 8));
                        break;
                    }
                default: { break; }
            }
            return phase;
        }

        public static double BesselPhase(int order, double s, bool HP = false)
        {
            double phase = 0.0;

            switch (order)
            {
                case 1:
                    {
                        phase = -Math.Atan2(s, 1);
                        break;
                    }
                case 2:
                    {
                        if (HP)
                        {
                            phase = Math.Atan2((3 / s), 3 - (1 / Math.Pow(s, 2)));
                        }
                        else
                        {
                            phase = -Math.Atan2(1 * s, (-((1.0 / 3.0) * Math.Pow(s, 2)) + 1));
                        }
                        break;
                    }
                case 3:
                    {
                        phase = -Math.Atan2((-Math.Pow(s, 3)) + (15 * s), (-6 * Math.Pow(s, 2)) + 15);
                        break;
                    }
                case 4:
                    {
                        if (HP)
                        {
                            phase = Math.Atan2((105 / s) - (10 / Math.Pow(s, 3)), 105 - (45 / Math.Pow(s, 2)) + (1 / Math.Pow(s, 4)));
                        }
                        else
                        {
                            phase = -Math.Atan2((-10 * Math.Pow(s, 3)) + (105 * s), (Math.Pow(s, 4) - (45 * Math.Pow(s, 2)) + 105));
                        }
                        break;
                    }
                case 5:
                    {
                        //phase = -Math.Atan2(Math.Pow(s, 5) - 105 * Math.Pow(s, 3) + (945 * s), ((15 * Math.Pow(s, 4)) - (420 * Math.Pow(s, 2)) + 945));
                        break;
                    }
                case 6:
                    {
                        break;
                    }
                case 7:
                    {
                        break;
                    }
                case 8:
                    {
                        break;
                    }
                default: { break; }
            }
            return phase;
        }

        public static double LinkwitzRileyPhase(int order, double s, bool HP = false)
        {
            double phase = 0.0;

            switch (order)
            {
                case 2:
                    {
                        phase = -Math.Atan2(2 * s, 1 - Math.Pow(s, 2));
                        break;
                    }
                case 4:
                    {
                        //phase = -Math.Atan2((-4 * Math.Pow(s, 3)) + (4 * s), Math.Pow(s, 4) + (-6 * Math.Pow(s, 2)) + 1);
                        phase = -Math.Atan2((-2.82 * Math.Pow(s, 3)) + (2.82 * s), Math.Pow(s, 4) + (-3.9981 * Math.Pow(s, 2)) + 1);
                        break;
                    }
                case 6:
                    {
                        //phase = -Math.Atan2((6 * Math.Pow(s, 5)) + (-20 * Math.Pow(s, 3)) + (6 * s), (-Math.Pow(s, 6)) + (15 * Math.Pow(s, 4)) + (-15 * Math.Pow(s, 2)) + 1);
                        phase = -Math.Atan2((4 * Math.Pow(s, 5)) + (-10 * Math.Pow(s, 3)) + (4 * s), (-Math.Pow(s, 6)) + (8 * Math.Pow(s, 4)) + (-8 * Math.Pow(s, 2)) + 1);
                        break;
                    }
                case 8:
                    {
                        phase = -Math.Atan2((-5.2 * Math.Pow(s, 7)) + (22.8 * Math.Pow(s, 5)) + (-22.8 * Math.Pow(s, 3)) + (5.2 * s),
                                             Math.Pow(s, 8) + (-13.53 * Math.Pow(s, 6)) + (26.98 * Math.Pow(s, 4)) + (-13.53 * Math.Pow(s, 2)) + 1);
                        break;
                    }
                default: { break; }
            }
            if (HP && (order % 4 != 0)) { phase += Math.PI; }
            return phase;
        }

        //public static double MixedBP(Target target, double gain, double f)
        //{
        //    int orderHP = target.orderHP;
        //    int orderLP = target.orderLP;
        //    double FcHP = target.frequencyHP;
        //    double FcLP = target.frequencyLP;

        //    double ratioHP = f / FcHP;
        //    double ratioLP = f / FcLP;
        //    double powerHP = 2 * orderHP;
        //    double powerLP = 2 * orderLP;
        //    double sHP = f / FcHP;
        //    double sLP = f / FcLP;
        //    double bwHP = 0, bwLP = 0, bw, phiHP, phiLP;
        //    Complex cHP, cLP, cBW, real;
        //    double realHP, realLP, imaginaryHP, imaginaryLP;


        //    bwHP = 1 / (Math.Sqrt(1 + (Math.Pow(1 / sHP, powerHP)))); // HP

        //    // Magnitude contribution of each leg (no phase data).

        //    // Phase of each leg.
        //    phiHP = (- 1 / Math.Atan(sHP));
        //    phiLP = (- 1 / Math.Atan(sLP));

        //    // Real and reactive parts of the magnitude.
        //    realHP = bwHP * Math.Cos(phiHP);
        //    realLP = bwLP * Math.Cos(phiLP);
        //    imaginaryHP = bwHP * Math.Sin(phiHP);
        //    imaginaryLP = bwLP * Math.Sin(phiLP);

        //    cHP = new Complex(realHP, imaginaryHP); // Real and Imaginary components
        //    cLP = new Complex(realLP, imaginaryLP); // Real and Imaginary components

        //    if ((cHP != null) && (cHP != null)) 
        //        { cBW = Complex.Multiply(cHP, cLP);} // Combine both legs 
        //    else if (cHP != null) 
        //        { cBW = cHP; }
        //    else 
        //        { cBW = cLP; }

        //    // Get the new magnitude.
        //    real = Complex.Abs(cBW);
        //    bw = real.Magnitude;
        //    return bw;
        //}


    }
}
