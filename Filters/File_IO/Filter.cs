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
