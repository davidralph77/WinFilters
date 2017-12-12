using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WinFilters.Data
{
    sealed class FilterSPL
    {
        private static FilterSPL instance;

        public bool isActive = false;

        public double[,] lowpass;
        public double[,] highpass;

        private FilterSPL() { }

        public static FilterSPL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FilterSPL();
                }
                return instance;
            }
        }

        public static void Initialize()
        {
            FilterSPL.Instance.isActive = true;
        }
    }

    sealed class ExportSPL
    {
        private static ExportSPL instance;

        public double nominal;

        private ExportSPL() { }

        public static ExportSPL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ExportSPL();
                }
                return instance;
            }
        }
    }
}
