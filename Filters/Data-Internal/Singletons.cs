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
