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
