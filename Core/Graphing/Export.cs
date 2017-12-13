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

using System.Linq;
using System.Windows.Forms;
using System.IO;
using Core.Data;

namespace Core.Graphing
{
    /// <summary>
    /// Graph is the class used to control driver assignment to a curve and to manipulate its display.
    /// </summary>
    public static class Export
    {        
        public static void CurveToFile(double[,] data, string fileType, double nominal = 0.0)
        {
            if (null == data) { MessageBox.Show("Error - No data was available for this curve."); return; }

            SaveFileDialog saveFileDialog = new SaveFileDialog();

            // Set filter options and filter index.
            switch (fileType)
            {
                case ("frd"):
                    {
                        saveFileDialog.Filter = "Text Files (.FRD)|*.frd|All Files (*.*)|*.*";
                        break;
                    }
                case ("zma"):
                    {
                        saveFileDialog.Filter = "Text Files (.ZMA)|*.zma|All Files (*.*)|*.*";
                        break;
                    }
                default:
                    {
                        MessageBox.Show("Ooops. No option case to handle null fileType.");
                        return;
                    }
            }
            saveFileDialog.FilterIndex = 1;
            saveFileDialog.CheckPathExists = true;

            DialogResult result = saveFileDialog.ShowDialog();

            int i = 0;
            if (result == DialogResult.OK)
            {
                using (StreamWriter objWriter = new StreamWriter(saveFileDialog.FileName))
                {
                    int count = FrequencyRange.dynamicFreqs.Count();

                    for (i = 0; i < count; i++)
                    {
                        // Do not want to WriteLine the last line
                        if (i < count)
                        {
                            objWriter.WriteLine
                            (
                                data[i, 0].ToString("F6") + " " +
                                (data[i, 1] + nominal).ToString("F6") + " " +
                                data[i, 2].ToString("F6")
                            );
                        }
                        else
                        {
                            objWriter.Write
                            (
                                data[i, 0].ToString("F6") + " " +
                                (data[i, 1] + nominal).ToString("F6") + " " +
                                data[i, 2].ToString("F6")
                            );
                        }
                    } 
                }
                //objWriter.Close();
                //objWriter.Dispose();
                MessageBox.Show("Saved " + i + " lines");
            }
        }

    }
}
