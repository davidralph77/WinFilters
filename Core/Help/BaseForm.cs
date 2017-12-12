using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Reflection;

namespace WinPCD.Forms.Help
{
    public partial class BaseForm : Form
    {
        public BaseForm()
        {
            InitializeComponent();
            // Create the ToolTip and associate with the Form container.
            ToolTip toolTip1 = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTip1.AutomaticDelay = 5000;
            toolTip1.AutoPopDelay = 5000;
            toolTip1.InitialDelay = 1000;
            toolTip1.ReshowDelay = 200;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = true;

            toolTip1.SetToolTip(helpRichTextBox, "Use <ctrl><scrollwheel> to change font size.");

            PreviewBtn.Click += PreviewBtn_Click;
            PrintBtn.Click += PrintBtn_Click;
            MS.Items.Add(PreviewBtn);
            MS.Items.Add(PrintBtn);
            MS.Parent = this;
        }

        private MenuStrip MS = new MenuStrip();
        private ToolStripButton PreviewBtn = new ToolStripButton("Preview");
        private ToolStripButton PrintBtn = new ToolStripButton("Print");
        protected void PreviewBtn_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog PPDlg = new PrintPreviewDialog();
            PPDlg.Document = InitPrint(helpRichTextBox.Text);
            PPDlg.ShowDialog();
        }

        protected void PrintBtn_Click(object sender, EventArgs e)
        {
            InitPrint(helpRichTextBox.Text).Print();
        }

        string TextToPrint;
        public PrintDocument InitPrint(string TextToPrint)
        {
            DialogResult result = fontDialog1.ShowDialog();
            // See if OK was pressed.
            if (result == DialogResult.OK)
            {
                // Get Font.
                Font font = fontDialog1.Font;
                // Set TextBox properties.
                this.helpRichTextBox.Font = font;
            }

            this.TextToPrint = TextToPrint;
            PrintDocument PD = new PrintDocument();
            PD.OriginAtMargins = true;
            PD.PrintPage += PrintPage;
            return PD;
        }

        protected virtual void PrintPage(object sender, PrintPageEventArgs e)
        {
            int Chars = 0;
            int Lines = 0;
            Font Font = new Font("Courier new", 10);
            Rectangle R = new Rectangle(Point.Empty, e.MarginBounds.Size);
            StringFormat SF = StringFormat.GenericTypographic;
            e.Graphics.MeasureString(TextToPrint, Font, R.Size, SF, out Chars, out Lines);
            e.Graphics.DrawString(TextToPrint, Font, Brushes.Black, R, SF);
            TextToPrint = TextToPrint.Substring(Chars);
            e.HasMorePages = (TextToPrint.Length > 0);
        }
        protected virtual void BaseForm_Load(object sender, EventArgs e)
        {
            try
            {
                //From the assembly where this code lives!
                var type = this.GetType().Assembly.GetManifestResourceNames();

                //or from the entry point to the application - there is a difference!
                var resourceList = Assembly.GetExecutingAssembly().GetManifestResourceNames();

                Assembly assembly = Assembly.GetExecutingAssembly();
                string name = assembly.GetName().Name;
                Stream creditStream = assembly.GetManifestResourceStream(name + ".Instructions.WinPCD_Instructions.rtf");
                if (creditStream != null)
                {
                    helpRichTextBox.LoadFile(creditStream, RichTextBoxStreamType.RichText);
                }
                else
                {
                    MessageBox.Show("Cannot open the help file");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Exception: BaseForm - Cannot open the help file");
                //throw;
            }
        }
    }
}
