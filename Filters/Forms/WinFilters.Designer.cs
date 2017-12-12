namespace WinFilters
{
    partial class WinFilters
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.systemGraph = new ZedGraph.ZedGraphControl();
            this.togglePhase = new System.Windows.Forms.Button();
            this.invertHighpass = new System.Windows.Forms.Button();
            this.toggleDelay = new System.Windows.Forms.Button();
            this.toggleSum = new System.Windows.Forms.Button();
            this.toggleSumPhase = new System.Windows.Forms.Button();
            this.lowpassGroupBox = new System.Windows.Forms.GroupBox();
            this.trgtTypeLP = new System.Windows.Forms.ComboBox();
            this.trgtFreqLP = new System.Windows.Forms.NumericUpDown();
            this.trgtOrderLP = new System.Windows.Forms.ComboBox();
            this.highpassGroupBox = new System.Windows.Forms.GroupBox();
            this.trgtTypeHP = new System.Windows.Forms.ComboBox();
            this.trgtFreqHP = new System.Windows.Forms.NumericUpDown();
            this.trgtOrderHP = new System.Windows.Forms.ComboBox();
            this.fcGroupBox = new System.Windows.Forms.GroupBox();
            this.besselFcPhaseHP = new System.Windows.Forms.TextBox();
            this.besselFcPhaseLP = new System.Windows.Forms.TextBox();
            this.magGroupBox = new System.Windows.Forms.GroupBox();
            this.actualFcHP = new System.Windows.Forms.TextBox();
            this.actualFcLP = new System.Windows.Forms.TextBox();
            this.factorGroupBox = new System.Windows.Forms.GroupBox();
            this.besselFcFactorHP = new System.Windows.Forms.TextBox();
            this.besselFcFactorLP = new System.Windows.Forms.TextBox();
            this.toggleHighPass = new System.Windows.Forms.Button();
            this.toggleLowPass = new System.Windows.Forms.Button();
            this.offsetGroupBox = new System.Windows.Forms.GroupBox();
            this.offsetEnglish = new System.Windows.Forms.TextBox();
            this.offsetMetric = new System.Windows.Forms.NumericUpDown();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.radioButtonLP = new System.Windows.Forms.RadioButton();
            this.radioButtonHP = new System.Windows.Forms.RadioButton();
            this.radioButtonReset = new System.Windows.Forms.RadioButton();
            this.toggleSumDelay = new System.Windows.Forms.Button();
            this.splNumeric = new System.Windows.Forms.NumericUpDown();
            this.splLabel = new System.Windows.Forms.Label();
            this.exportGroupBox = new System.Windows.Forms.GroupBox();
            this.buttonExportLP = new System.Windows.Forms.Button();
            this.buttonExportHP = new System.Windows.Forms.Button();
            this.offsetSelectGroupBox = new System.Windows.Forms.GroupBox();
            this.lowpassGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trgtFreqLP)).BeginInit();
            this.highpassGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trgtFreqHP)).BeginInit();
            this.fcGroupBox.SuspendLayout();
            this.magGroupBox.SuspendLayout();
            this.factorGroupBox.SuspendLayout();
            this.offsetGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.offsetMetric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splNumeric)).BeginInit();
            this.exportGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // systemGraph
            // 
            this.systemGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.systemGraph.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.systemGraph.BackColor = System.Drawing.SystemColors.Control;
            this.systemGraph.CausesValidation = false;
            this.systemGraph.IsEnableSelection = true;
            this.systemGraph.IsShowHScrollBar = true;
            this.systemGraph.IsShowVScrollBar = true;
            this.systemGraph.Location = new System.Drawing.Point(0, 89);
            this.systemGraph.Name = "systemGraph";
            this.systemGraph.PointValueFormat = "N";
            this.systemGraph.ScrollGrace = 0D;
            this.systemGraph.ScrollMaxX = 30000D;
            this.systemGraph.ScrollMaxY = 10D;
            this.systemGraph.ScrollMaxY2 = 180D;
            this.systemGraph.ScrollMinX = 10D;
            this.systemGraph.ScrollMinY = -100D;
            this.systemGraph.ScrollMinY2 = -180D;
            this.systemGraph.Size = new System.Drawing.Size(928, 453);
            this.systemGraph.TabIndex = 421;
            this.systemGraph.TabStop = false;
            this.systemGraph.Tag = "systemGraph";
            // 
            // togglePhase
            // 
            this.togglePhase.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.togglePhase.BackColor = System.Drawing.Color.LightGreen;
            this.togglePhase.Location = new System.Drawing.Point(804, 91);
            this.togglePhase.Name = "togglePhase";
            this.togglePhase.Size = new System.Drawing.Size(52, 23);
            this.togglePhase.TabIndex = 423;
            this.togglePhase.Text = "Phase";
            this.togglePhase.UseVisualStyleBackColor = false;
            this.togglePhase.Click += new System.EventHandler(this.togglePhase_Click);
            // 
            // invertHighpass
            // 
            this.invertHighpass.BackColor = System.Drawing.Color.LightGreen;
            this.invertHighpass.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.invertHighpass.Location = new System.Drawing.Point(3, 91);
            this.invertHighpass.Margin = new System.Windows.Forms.Padding(0);
            this.invertHighpass.Name = "invertHighpass";
            this.invertHighpass.Size = new System.Drawing.Size(69, 23);
            this.invertHighpass.TabIndex = 431;
            this.invertHighpass.TabStop = false;
            this.invertHighpass.Text = "Tweeter +";
            this.invertHighpass.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.invertHighpass.UseVisualStyleBackColor = false;
            this.invertHighpass.Click += new System.EventHandler(this.invertHighpass_Click);
            // 
            // toggleDelay
            // 
            this.toggleDelay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleDelay.BackColor = System.Drawing.Color.LightGreen;
            this.toggleDelay.Location = new System.Drawing.Point(858, 91);
            this.toggleDelay.Name = "toggleDelay";
            this.toggleDelay.Size = new System.Drawing.Size(52, 23);
            this.toggleDelay.TabIndex = 473;
            this.toggleDelay.Text = "Delay";
            this.toggleDelay.UseVisualStyleBackColor = false;
            this.toggleDelay.Click += new System.EventHandler(this.toggleDelay_Click);
            // 
            // toggleSum
            // 
            this.toggleSum.BackColor = System.Drawing.Color.LightGreen;
            this.toggleSum.Location = new System.Drawing.Point(75, 91);
            this.toggleSum.Name = "toggleSum";
            this.toggleSum.Size = new System.Drawing.Size(70, 23);
            this.toggleSum.TabIndex = 474;
            this.toggleSum.Text = "Sum SPL";
            this.toggleSum.UseVisualStyleBackColor = false;
            this.toggleSum.Click += new System.EventHandler(this.toggleSum_Click);
            // 
            // toggleSumPhase
            // 
            this.toggleSumPhase.BackColor = System.Drawing.Color.LightGreen;
            this.toggleSumPhase.Location = new System.Drawing.Point(148, 91);
            this.toggleSumPhase.Name = "toggleSumPhase";
            this.toggleSumPhase.Size = new System.Drawing.Size(70, 23);
            this.toggleSumPhase.TabIndex = 475;
            this.toggleSumPhase.Text = "Sum Phase";
            this.toggleSumPhase.UseVisualStyleBackColor = false;
            this.toggleSumPhase.Click += new System.EventHandler(this.toggleSumPhase_Click);
            // 
            // lowpassGroupBox
            // 
            this.lowpassGroupBox.Controls.Add(this.trgtTypeLP);
            this.lowpassGroupBox.Controls.Add(this.trgtFreqLP);
            this.lowpassGroupBox.Controls.Add(this.trgtOrderLP);
            this.lowpassGroupBox.Location = new System.Drawing.Point(3, 2);
            this.lowpassGroupBox.Name = "lowpassGroupBox";
            this.lowpassGroupBox.Size = new System.Drawing.Size(249, 41);
            this.lowpassGroupBox.TabIndex = 477;
            this.lowpassGroupBox.TabStop = false;
            this.lowpassGroupBox.Text = "Target Low Pass Response";
            // 
            // trgtTypeLP
            // 
            this.trgtTypeLP.BackColor = System.Drawing.Color.Purple;
            this.trgtTypeLP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trgtTypeLP.ForeColor = System.Drawing.Color.White;
            this.trgtTypeLP.FormattingEnabled = true;
            this.trgtTypeLP.Items.AddRange(new object[] {
            "No High Pass Target",
            "First Order Butterworth",
            "Second Order Bessel 3db",
            "Second Order Bessel Phase Match",
            "Second Order Butterworth",
            "Second Order Linkwitz-Riley",
            "Third Order Bessel 3db",
            "Third Order Bessel Phase Match",
            "Third Order Butterworth",
            "Fourth Order Bessel 3db",
            "Fourth Order Bessel Phase Match",
            "Fourth Order Butterworth",
            "Fourth Order Linkwitz-Riley",
            "Fifth Order Bessel 3db",
            "Fifth Order Butterworth",
            "Sixth Order Bessel 3db",
            "Sixth Order Butterworth",
            "Sixth Order Linkwitz-Riley",
            "Eighth Order Linkwitz-Riley",
            "Imported Target Active"});
            this.trgtTypeLP.Location = new System.Drawing.Point(43, 16);
            this.trgtTypeLP.Margin = new System.Windows.Forms.Padding(0);
            this.trgtTypeLP.MaxDropDownItems = 20;
            this.trgtTypeLP.Name = "trgtTypeLP";
            this.trgtTypeLP.Size = new System.Drawing.Size(144, 21);
            this.trgtTypeLP.TabIndex = 425;
            this.trgtTypeLP.Text = "LinkwitzRiley";
            this.trgtTypeLP.SelectedIndexChanged += new System.EventHandler(this.trgtTypeLP_SelectedIndexChanged);
            // 
            // trgtFreqLP
            // 
            this.trgtFreqLP.BackColor = System.Drawing.Color.Black;
            this.trgtFreqLP.Cursor = System.Windows.Forms.Cursors.Default;
            this.trgtFreqLP.Dock = System.Windows.Forms.DockStyle.Right;
            this.trgtFreqLP.ForeColor = System.Drawing.Color.White;
            this.trgtFreqLP.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.trgtFreqLP.Location = new System.Drawing.Point(187, 16);
            this.trgtFreqLP.Margin = new System.Windows.Forms.Padding(0);
            this.trgtFreqLP.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.trgtFreqLP.Name = "trgtFreqLP";
            this.trgtFreqLP.Size = new System.Drawing.Size(59, 20);
            this.trgtFreqLP.TabIndex = 424;
            this.trgtFreqLP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.trgtFreqLP.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.trgtFreqLP.ValueChanged += new System.EventHandler(this.trgtFreqLP_ValueChanged);
            // 
            // trgtOrderLP
            // 
            this.trgtOrderLP.BackColor = System.Drawing.Color.Purple;
            this.trgtOrderLP.Dock = System.Windows.Forms.DockStyle.Left;
            this.trgtOrderLP.ForeColor = System.Drawing.Color.White;
            this.trgtOrderLP.FormattingEnabled = true;
            this.trgtOrderLP.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "6",
            "8"});
            this.trgtOrderLP.Location = new System.Drawing.Point(3, 16);
            this.trgtOrderLP.Margin = new System.Windows.Forms.Padding(0);
            this.trgtOrderLP.MaxDropDownItems = 20;
            this.trgtOrderLP.Name = "trgtOrderLP";
            this.trgtOrderLP.Size = new System.Drawing.Size(40, 21);
            this.trgtOrderLP.TabIndex = 423;
            this.trgtOrderLP.Text = "4";
            this.trgtOrderLP.SelectedIndexChanged += new System.EventHandler(this.trgtOrderLP_SelectedIndexChanged);
            // 
            // highpassGroupBox
            // 
            this.highpassGroupBox.Controls.Add(this.trgtTypeHP);
            this.highpassGroupBox.Controls.Add(this.trgtFreqHP);
            this.highpassGroupBox.Controls.Add(this.trgtOrderHP);
            this.highpassGroupBox.Location = new System.Drawing.Point(3, 42);
            this.highpassGroupBox.Name = "highpassGroupBox";
            this.highpassGroupBox.Size = new System.Drawing.Size(249, 41);
            this.highpassGroupBox.TabIndex = 478;
            this.highpassGroupBox.TabStop = false;
            this.highpassGroupBox.Text = "Target High Pass Response";
            // 
            // trgtTypeHP
            // 
            this.trgtTypeHP.BackColor = System.Drawing.Color.Purple;
            this.trgtTypeHP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trgtTypeHP.ForeColor = System.Drawing.Color.White;
            this.trgtTypeHP.FormattingEnabled = true;
            this.trgtTypeHP.Items.AddRange(new object[] {
            "No Low Pass Target",
            "First Order Butterworth",
            "Second Order Bessel 3db",
            "Second Order Bessel Phase Match",
            "Second Order Butterworth",
            "Second Order Linkwitz-Riley",
            "Third Order Bessel 3db",
            "Third Order Bessel Phase Match",
            "Third Order Butterworth",
            "Fourth Order Bessel 3db",
            "Fourth Order Bessel Phase Match",
            "Fourth Order Butterworth",
            "Fourth Order Linkwitz-Riley",
            "Fifth Order Bessel 3db",
            "Fifth Order Butterworth",
            "Sixth Order Bessel 3db",
            "Sixth Order Butterworth",
            "Sixth Order Linkwitz-Riley",
            "Eighth Order Linkwitz-Riley",
            "Imported Target Active"});
            this.trgtTypeHP.Location = new System.Drawing.Point(43, 16);
            this.trgtTypeHP.Margin = new System.Windows.Forms.Padding(0);
            this.trgtTypeHP.MaxDropDownItems = 20;
            this.trgtTypeHP.Name = "trgtTypeHP";
            this.trgtTypeHP.Size = new System.Drawing.Size(144, 21);
            this.trgtTypeHP.TabIndex = 426;
            this.trgtTypeHP.Text = "LinkwitzRiley";
            this.trgtTypeHP.SelectedIndexChanged += new System.EventHandler(this.trgtTypeHP_SelectedIndexChanged);
            // 
            // trgtFreqHP
            // 
            this.trgtFreqHP.BackColor = System.Drawing.Color.Black;
            this.trgtFreqHP.Cursor = System.Windows.Forms.Cursors.Default;
            this.trgtFreqHP.Dock = System.Windows.Forms.DockStyle.Right;
            this.trgtFreqHP.ForeColor = System.Drawing.Color.White;
            this.trgtFreqHP.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.trgtFreqHP.Location = new System.Drawing.Point(187, 16);
            this.trgtFreqHP.Margin = new System.Windows.Forms.Padding(0);
            this.trgtFreqHP.Maximum = new decimal(new int[] {
            20000,
            0,
            0,
            0});
            this.trgtFreqHP.Name = "trgtFreqHP";
            this.trgtFreqHP.Size = new System.Drawing.Size(59, 20);
            this.trgtFreqHP.TabIndex = 425;
            this.trgtFreqHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.trgtFreqHP.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.trgtFreqHP.ValueChanged += new System.EventHandler(this.trgtFreqHP_ValueChanged);
            // 
            // trgtOrderHP
            // 
            this.trgtOrderHP.BackColor = System.Drawing.Color.Purple;
            this.trgtOrderHP.Dock = System.Windows.Forms.DockStyle.Left;
            this.trgtOrderHP.ForeColor = System.Drawing.Color.White;
            this.trgtOrderHP.FormattingEnabled = true;
            this.trgtOrderHP.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "6",
            "8"});
            this.trgtOrderHP.Location = new System.Drawing.Point(3, 16);
            this.trgtOrderHP.Margin = new System.Windows.Forms.Padding(0);
            this.trgtOrderHP.MaxDropDownItems = 20;
            this.trgtOrderHP.Name = "trgtOrderHP";
            this.trgtOrderHP.Size = new System.Drawing.Size(40, 21);
            this.trgtOrderHP.TabIndex = 424;
            this.trgtOrderHP.Text = "4";
            this.trgtOrderHP.SelectedIndexChanged += new System.EventHandler(this.trgtOrderHP_SelectedIndexChanged);
            // 
            // fcGroupBox
            // 
            this.fcGroupBox.Controls.Add(this.besselFcPhaseHP);
            this.fcGroupBox.Controls.Add(this.besselFcPhaseLP);
            this.fcGroupBox.Location = new System.Drawing.Point(406, 2);
            this.fcGroupBox.Name = "fcGroupBox";
            this.fcGroupBox.Size = new System.Drawing.Size(68, 81);
            this.fcGroupBox.TabIndex = 479;
            this.fcGroupBox.TabStop = false;
            this.fcGroupBox.Text = "Fc Phase";
            // 
            // besselFcPhaseHP
            // 
            this.besselFcPhaseHP.BackColor = System.Drawing.Color.Black;
            this.besselFcPhaseHP.ForeColor = System.Drawing.Color.White;
            this.besselFcPhaseHP.Location = new System.Drawing.Point(12, 55);
            this.besselFcPhaseHP.MaxLength = 5;
            this.besselFcPhaseHP.Name = "besselFcPhaseHP";
            this.besselFcPhaseHP.ReadOnly = true;
            this.besselFcPhaseHP.Size = new System.Drawing.Size(43, 20);
            this.besselFcPhaseHP.TabIndex = 1;
            this.besselFcPhaseHP.Text = "1000";
            this.besselFcPhaseHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.besselFcPhaseHP.WordWrap = false;
            // 
            // besselFcPhaseLP
            // 
            this.besselFcPhaseLP.BackColor = System.Drawing.Color.Black;
            this.besselFcPhaseLP.ForeColor = System.Drawing.Color.White;
            this.besselFcPhaseLP.Location = new System.Drawing.Point(12, 17);
            this.besselFcPhaseLP.MaxLength = 5;
            this.besselFcPhaseLP.Name = "besselFcPhaseLP";
            this.besselFcPhaseLP.ReadOnly = true;
            this.besselFcPhaseLP.Size = new System.Drawing.Size(43, 20);
            this.besselFcPhaseLP.TabIndex = 0;
            this.besselFcPhaseLP.Text = "1000";
            this.besselFcPhaseLP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.besselFcPhaseLP.WordWrap = false;
            // 
            // magGroupBox
            // 
            this.magGroupBox.Controls.Add(this.actualFcHP);
            this.magGroupBox.Controls.Add(this.actualFcLP);
            this.magGroupBox.Location = new System.Drawing.Point(332, 2);
            this.magGroupBox.Name = "magGroupBox";
            this.magGroupBox.Size = new System.Drawing.Size(68, 81);
            this.magGroupBox.TabIndex = 480;
            this.magGroupBox.TabStop = false;
            this.magGroupBox.Text = "Actual Fc";
            // 
            // actualFcHP
            // 
            this.actualFcHP.BackColor = System.Drawing.Color.Black;
            this.actualFcHP.ForeColor = System.Drawing.Color.White;
            this.actualFcHP.Location = new System.Drawing.Point(12, 55);
            this.actualFcHP.MaxLength = 5;
            this.actualFcHP.Name = "actualFcHP";
            this.actualFcHP.ReadOnly = true;
            this.actualFcHP.Size = new System.Drawing.Size(43, 20);
            this.actualFcHP.TabIndex = 1;
            this.actualFcHP.Text = "1000";
            this.actualFcHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.actualFcHP.WordWrap = false;
            // 
            // actualFcLP
            // 
            this.actualFcLP.BackColor = System.Drawing.Color.Black;
            this.actualFcLP.ForeColor = System.Drawing.Color.White;
            this.actualFcLP.Location = new System.Drawing.Point(12, 17);
            this.actualFcLP.MaxLength = 5;
            this.actualFcLP.Name = "actualFcLP";
            this.actualFcLP.ReadOnly = true;
            this.actualFcLP.Size = new System.Drawing.Size(43, 20);
            this.actualFcLP.TabIndex = 0;
            this.actualFcLP.Text = "1000";
            this.actualFcLP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.actualFcLP.WordWrap = false;
            // 
            // factorGroupBox
            // 
            this.factorGroupBox.Controls.Add(this.besselFcFactorHP);
            this.factorGroupBox.Controls.Add(this.besselFcFactorLP);
            this.factorGroupBox.Location = new System.Drawing.Point(258, 2);
            this.factorGroupBox.Name = "factorGroupBox";
            this.factorGroupBox.Size = new System.Drawing.Size(68, 81);
            this.factorGroupBox.TabIndex = 480;
            this.factorGroupBox.TabStop = false;
            this.factorGroupBox.Text = "Fc Factor";
            // 
            // besselFcFactorHP
            // 
            this.besselFcFactorHP.BackColor = System.Drawing.Color.Black;
            this.besselFcFactorHP.ForeColor = System.Drawing.Color.White;
            this.besselFcFactorHP.Location = new System.Drawing.Point(12, 55);
            this.besselFcFactorHP.MaxLength = 4;
            this.besselFcFactorHP.Name = "besselFcFactorHP";
            this.besselFcFactorHP.ReadOnly = true;
            this.besselFcFactorHP.Size = new System.Drawing.Size(43, 20);
            this.besselFcFactorHP.TabIndex = 1;
            this.besselFcFactorHP.Text = "1000";
            this.besselFcFactorHP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.besselFcFactorHP.WordWrap = false;
            // 
            // besselFcFactorLP
            // 
            this.besselFcFactorLP.BackColor = System.Drawing.Color.Black;
            this.besselFcFactorLP.ForeColor = System.Drawing.Color.White;
            this.besselFcFactorLP.Location = new System.Drawing.Point(12, 17);
            this.besselFcFactorLP.MaxLength = 4;
            this.besselFcFactorLP.Name = "besselFcFactorLP";
            this.besselFcFactorLP.ReadOnly = true;
            this.besselFcFactorLP.Size = new System.Drawing.Size(43, 20);
            this.besselFcFactorLP.TabIndex = 0;
            this.besselFcFactorLP.Text = "1000";
            this.besselFcFactorLP.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.besselFcFactorLP.WordWrap = false;
            // 
            // toggleHighPass
            // 
            this.toggleHighPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleHighPass.BackColor = System.Drawing.Color.LightGreen;
            this.toggleHighPass.Location = new System.Drawing.Point(750, 91);
            this.toggleHighPass.Name = "toggleHighPass";
            this.toggleHighPass.Size = new System.Drawing.Size(52, 23);
            this.toggleHighPass.TabIndex = 481;
            this.toggleHighPass.Text = "HiPass";
            this.toggleHighPass.UseVisualStyleBackColor = false;
            this.toggleHighPass.Click += new System.EventHandler(this.toggleHighPass_Click);
            // 
            // toggleLowPass
            // 
            this.toggleLowPass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleLowPass.BackColor = System.Drawing.Color.LightGreen;
            this.toggleLowPass.Location = new System.Drawing.Point(695, 91);
            this.toggleLowPass.Name = "toggleLowPass";
            this.toggleLowPass.Size = new System.Drawing.Size(52, 23);
            this.toggleLowPass.TabIndex = 482;
            this.toggleLowPass.Text = "LoPass";
            this.toggleLowPass.UseVisualStyleBackColor = false;
            this.toggleLowPass.Click += new System.EventHandler(this.toggleLowPass_Click);
            // 
            // offsetGroupBox
            // 
            this.offsetGroupBox.Controls.Add(this.offsetEnglish);
            this.offsetGroupBox.Controls.Add(this.offsetMetric);
            this.offsetGroupBox.Controls.Add(this.textBox1);
            this.offsetGroupBox.Controls.Add(this.textBox2);
            this.offsetGroupBox.Location = new System.Drawing.Point(480, 2);
            this.offsetGroupBox.Name = "offsetGroupBox";
            this.offsetGroupBox.Size = new System.Drawing.Size(88, 81);
            this.offsetGroupBox.TabIndex = 483;
            this.offsetGroupBox.TabStop = false;
            this.offsetGroupBox.Text = "Offset";
            // 
            // offsetEnglish
            // 
            this.offsetEnglish.BackColor = System.Drawing.Color.Black;
            this.offsetEnglish.ForeColor = System.Drawing.Color.White;
            this.offsetEnglish.Location = new System.Drawing.Point(11, 55);
            this.offsetEnglish.MaxLength = 5;
            this.offsetEnglish.Name = "offsetEnglish";
            this.offsetEnglish.ReadOnly = true;
            this.offsetEnglish.Size = new System.Drawing.Size(40, 20);
            this.offsetEnglish.TabIndex = 484;
            this.offsetEnglish.Text = "0";
            this.offsetEnglish.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.offsetEnglish.WordWrap = false;
            // 
            // offsetMetric
            // 
            this.offsetMetric.BackColor = System.Drawing.Color.Black;
            this.offsetMetric.Cursor = System.Windows.Forms.Cursors.Default;
            this.offsetMetric.DecimalPlaces = 1;
            this.offsetMetric.ForeColor = System.Drawing.Color.White;
            this.offsetMetric.Location = new System.Drawing.Point(3, 16);
            this.offsetMetric.Margin = new System.Windows.Forms.Padding(0);
            this.offsetMetric.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.offsetMetric.Name = "offsetMetric";
            this.offsetMetric.Size = new System.Drawing.Size(48, 20);
            this.offsetMetric.TabIndex = 484;
            this.offsetMetric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.offsetMetric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.offsetMetric.ValueChanged += new System.EventHandler(this.offsetMetric_ValueChanged);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(54, 55);
            this.textBox1.MaxLength = 5;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(27, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "inch";
            this.textBox1.WordWrap = false;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.White;
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(54, 16);
            this.textBox2.MaxLength = 5;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(27, 20);
            this.textBox2.TabIndex = 0;
            this.textBox2.TabStop = false;
            this.textBox2.Text = "mm";
            this.textBox2.WordWrap = false;
            // 
            // radioButtonLP
            // 
            this.radioButtonLP.AutoSize = true;
            this.radioButtonLP.Location = new System.Drawing.Point(579, 16);
            this.radioButtonLP.Name = "radioButtonLP";
            this.radioButtonLP.Size = new System.Drawing.Size(167, 17);
            this.radioButtonLP.TabIndex = 484;
            this.radioButtonLP.TabStop = true;
            this.radioButtonLP.Text = "Offset LP (Add Excess-Phase)";
            this.radioButtonLP.UseVisualStyleBackColor = true;
            this.radioButtonLP.CheckedChanged += new System.EventHandler(this.radioButtonLP_CheckedChanged);
            // 
            // radioButtonHP
            // 
            this.radioButtonHP.AutoSize = true;
            this.radioButtonHP.Location = new System.Drawing.Point(579, 58);
            this.radioButtonHP.Name = "radioButtonHP";
            this.radioButtonHP.Size = new System.Drawing.Size(174, 17);
            this.radioButtonHP.TabIndex = 485;
            this.radioButtonHP.TabStop = true;
            this.radioButtonHP.Text = "Offset HP (Remove Exc-Phase)";
            this.radioButtonHP.UseVisualStyleBackColor = true;
            this.radioButtonHP.CheckedChanged += new System.EventHandler(this.radioButtonHP_CheckedChanged);
            // 
            // radioButtonReset
            // 
            this.radioButtonReset.AutoSize = true;
            this.radioButtonReset.Location = new System.Drawing.Point(579, 37);
            this.radioButtonReset.Name = "radioButtonReset";
            this.radioButtonReset.Size = new System.Drawing.Size(149, 17);
            this.radioButtonReset.TabIndex = 486;
            this.radioButtonReset.TabStop = true;
            this.radioButtonReset.Text = "None (Coincident Centers)";
            this.radioButtonReset.UseVisualStyleBackColor = true;
            this.radioButtonReset.CheckedChanged += new System.EventHandler(this.radioButtonReset_CheckedChanged);
            // 
            // toggleSumDelay
            // 
            this.toggleSumDelay.BackColor = System.Drawing.Color.LightGreen;
            this.toggleSumDelay.Location = new System.Drawing.Point(221, 91);
            this.toggleSumDelay.Name = "toggleSumDelay";
            this.toggleSumDelay.Size = new System.Drawing.Size(70, 23);
            this.toggleSumDelay.TabIndex = 487;
            this.toggleSumDelay.Text = "Sum Delay";
            this.toggleSumDelay.UseVisualStyleBackColor = false;
            this.toggleSumDelay.Click += new System.EventHandler(this.toggleSumDelay_Click);
            // 
            // splNumeric
            // 
            this.splNumeric.BackColor = System.Drawing.Color.Black;
            this.splNumeric.Cursor = System.Windows.Forms.Cursors.Default;
            this.splNumeric.DecimalPlaces = 1;
            this.splNumeric.ForeColor = System.Drawing.Color.White;
            this.splNumeric.Location = new System.Drawing.Point(99, 21);
            this.splNumeric.Margin = new System.Windows.Forms.Padding(0);
            this.splNumeric.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.splNumeric.Name = "splNumeric";
            this.splNumeric.Size = new System.Drawing.Size(48, 20);
            this.splNumeric.TabIndex = 488;
            this.splNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.splNumeric.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.splNumeric.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.splNumeric.ValueChanged += new System.EventHandler(this.splNumeric_ValueChanged);
            // 
            // splLabel
            // 
            this.splLabel.AutoSize = true;
            this.splLabel.BackColor = System.Drawing.Color.White;
            this.splLabel.Location = new System.Drawing.Point(16, 24);
            this.splLabel.Name = "splLabel";
            this.splLabel.Size = new System.Drawing.Size(80, 13);
            this.splLabel.TabIndex = 489;
            this.splLabel.Text = "Reference SPL";
            this.splLabel.Click += new System.EventHandler(this.splLabel_Click);
            // 
            // exportGroupBox
            // 
            this.exportGroupBox.Controls.Add(this.buttonExportHP);
            this.exportGroupBox.Controls.Add(this.buttonExportLP);
            this.exportGroupBox.Controls.Add(this.splLabel);
            this.exportGroupBox.Controls.Add(this.splNumeric);
            this.exportGroupBox.Location = new System.Drawing.Point(753, 2);
            this.exportGroupBox.Name = "exportGroupBox";
            this.exportGroupBox.Size = new System.Drawing.Size(163, 81);
            this.exportGroupBox.TabIndex = 490;
            this.exportGroupBox.TabStop = false;
            this.exportGroupBox.Text = "Export Target to File";
            // 
            // buttonExportLP
            // 
            this.buttonExportLP.Location = new System.Drawing.Point(7, 51);
            this.buttonExportLP.Name = "buttonExportLP";
            this.buttonExportLP.Size = new System.Drawing.Size(75, 23);
            this.buttonExportLP.TabIndex = 490;
            this.buttonExportLP.Text = "Lowpass";
            this.buttonExportLP.UseVisualStyleBackColor = true;
            this.buttonExportLP.Click += new System.EventHandler(this.buttonExportLP_Click);
            // 
            // buttonExportHP
            // 
            this.buttonExportHP.Location = new System.Drawing.Point(82, 52);
            this.buttonExportHP.Name = "buttonExportHP";
            this.buttonExportHP.Size = new System.Drawing.Size(75, 23);
            this.buttonExportHP.TabIndex = 491;
            this.buttonExportHP.Text = "Highpass";
            this.buttonExportHP.UseVisualStyleBackColor = true;
            this.buttonExportHP.Click += new System.EventHandler(this.buttonExportHP_Click);
            // 
            // offsetSelectGroupBox
            // 
            this.offsetSelectGroupBox.Location = new System.Drawing.Point(575, 2);
            this.offsetSelectGroupBox.Name = "offsetSelectGroupBox";
            this.offsetSelectGroupBox.Size = new System.Drawing.Size(172, 81);
            this.offsetSelectGroupBox.TabIndex = 491;
            this.offsetSelectGroupBox.TabStop = false;
            this.offsetSelectGroupBox.Text = "Filter to Offset (Excess Phase)";
            // 
            // WinFilters
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(928, 542);
            this.Controls.Add(this.exportGroupBox);
            this.Controls.Add(this.toggleSumDelay);
            this.Controls.Add(this.radioButtonReset);
            this.Controls.Add(this.radioButtonHP);
            this.Controls.Add(this.radioButtonLP);
            this.Controls.Add(this.offsetGroupBox);
            this.Controls.Add(this.toggleLowPass);
            this.Controls.Add(this.toggleHighPass);
            this.Controls.Add(this.factorGroupBox);
            this.Controls.Add(this.magGroupBox);
            this.Controls.Add(this.fcGroupBox);
            this.Controls.Add(this.highpassGroupBox);
            this.Controls.Add(this.lowpassGroupBox);
            this.Controls.Add(this.toggleSumPhase);
            this.Controls.Add(this.toggleSum);
            this.Controls.Add(this.toggleDelay);
            this.Controls.Add(this.invertHighpass);
            this.Controls.Add(this.togglePhase);
            this.Controls.Add(this.systemGraph);
            this.Controls.Add(this.offsetSelectGroupBox);
            this.HelpButton = true;
            this.Name = "WinFilters";
            this.Text = "WinFilters";
            this.Load += new System.EventHandler(this.WinFilters_Load);
            this.lowpassGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trgtFreqLP)).EndInit();
            this.highpassGroupBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trgtFreqHP)).EndInit();
            this.fcGroupBox.ResumeLayout(false);
            this.fcGroupBox.PerformLayout();
            this.magGroupBox.ResumeLayout(false);
            this.magGroupBox.PerformLayout();
            this.factorGroupBox.ResumeLayout(false);
            this.factorGroupBox.PerformLayout();
            this.offsetGroupBox.ResumeLayout(false);
            this.offsetGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.offsetMetric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splNumeric)).EndInit();
            this.exportGroupBox.ResumeLayout(false);
            this.exportGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ZedGraph.ZedGraphControl systemGraph;
        private System.Windows.Forms.Button togglePhase;
        private System.Windows.Forms.Button invertHighpass;
        private System.Windows.Forms.Button toggleDelay;
        private System.Windows.Forms.Button toggleSum;
        private System.Windows.Forms.Button toggleSumPhase;
        private System.Windows.Forms.GroupBox lowpassGroupBox;
        private System.Windows.Forms.ComboBox trgtTypeLP;
        private System.Windows.Forms.NumericUpDown trgtFreqLP;
        private System.Windows.Forms.ComboBox trgtOrderLP;
        private System.Windows.Forms.GroupBox highpassGroupBox;
        private System.Windows.Forms.ComboBox trgtTypeHP;
        private System.Windows.Forms.NumericUpDown trgtFreqHP;
        private System.Windows.Forms.ComboBox trgtOrderHP;
        private System.Windows.Forms.GroupBox fcGroupBox;
        private System.Windows.Forms.TextBox besselFcPhaseLP;
        private System.Windows.Forms.TextBox besselFcPhaseHP;
        private System.Windows.Forms.GroupBox magGroupBox;
        private System.Windows.Forms.TextBox actualFcHP;
        private System.Windows.Forms.TextBox actualFcLP;
        private System.Windows.Forms.GroupBox factorGroupBox;
        private System.Windows.Forms.TextBox besselFcFactorHP;
        private System.Windows.Forms.TextBox besselFcFactorLP;
        private System.Windows.Forms.Button toggleHighPass;
        private System.Windows.Forms.Button toggleLowPass;
        private System.Windows.Forms.GroupBox offsetGroupBox;
        private System.Windows.Forms.NumericUpDown offsetMetric;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox offsetEnglish;
        private System.Windows.Forms.RadioButton radioButtonLP;
        private System.Windows.Forms.RadioButton radioButtonHP;
        private System.Windows.Forms.RadioButton radioButtonReset;
        private System.Windows.Forms.Button toggleSumDelay;
        private System.Windows.Forms.NumericUpDown splNumeric;
        private System.Windows.Forms.Label splLabel;
        private System.Windows.Forms.GroupBox exportGroupBox;
        private System.Windows.Forms.Button buttonExportHP;
        private System.Windows.Forms.Button buttonExportLP;
        private System.Windows.Forms.GroupBox offsetSelectGroupBox;
    }
}

