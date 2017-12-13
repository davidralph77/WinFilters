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

using System.IO;
using System.Linq;
using System.Windows.Forms;
using Core.Data;
using Core.Filtering.Core;
using WinFilters.Data;

namespace WinFilters.File_IO
{
    class Filter
    {
        public static void ExportCurveToFile(string curveName, double factor = 90.0)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { FilterIndex = 1, CheckPathExists = true, FileName = curveName, DefaultExt = "frd" };

            DialogResult result = saveFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                int i = 0;
                using (StreamWriter objWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    double[,] data = null;

                    switch (curveName)
                    {
                        case "LowPass" : { data = FilterSPL.Instance.lowpass;  break; }
                        case "HighPass": { data = FilterSPL.Instance.highpass; break; }
                        default:
                            {
                                MessageBox.Show("Error - Curve name not in case list.");
                                return;
                            }
                    }

                    objWriter.WriteLine("* Frequency    Magnitude    Phase");

                    for (i = 0; i < FrequencyRange.dynamicFreqs.Count(); i++)
                    {
                        var wrappedPhase = FilterCore.WrapPhaseDegrees(data[i, 2]);

                        objWriter.WriteLine
                        (
                            data[i, 0] + " " +
                            (data[i, 1] + 90).ToString() + " " +
                            data[i, 2].ToString()
                        );
                    }
                }
                MessageBox.Show("Saved " + i + " lines");
            }
        }
    }
}
