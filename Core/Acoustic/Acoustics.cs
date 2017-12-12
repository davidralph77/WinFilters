using System;

namespace Core.Acoustic

{
    public class Acoustics
    {

        public static double ConvertDBToAbsolute(double db)
        {
            if (db <= 0) { return 0; };

            double absolutePressure;
            double refPressure = 1E-12; // w/m^2
            
            absolutePressure = refPressure * Math.Pow(10, db / 20); // ref * 10^(db/10)
            return absolutePressure;
        }

        public static double ConvertAbsoluteToDB(double abs)
        {
            if (abs <= 0) { return 0; };

            double db = 0;
            double refPressure = 1E-12; // w/m^2

            db = 20 * Math.Log10(abs / refPressure); // 10log(abs/ref);
            return db;
        }
    }
}
