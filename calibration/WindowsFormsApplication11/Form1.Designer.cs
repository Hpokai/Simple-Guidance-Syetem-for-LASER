namespace WindowsFormsApplication11
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.gb_OffsetSetting = new System.Windows.Forms.GroupBox();
            this.num_OffsetL = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button10 = new System.Windows.Forms.Button();
            this.num_OffsetU = new System.Windows.Forms.NumericUpDown();
            this.num_OffsetZ = new System.Windows.Forms.NumericUpDown();
            this.num_OffsetY = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.num_OffsetX = new System.Windows.Forms.NumericUpDown();
            this.cogRecordDisplay1 = new Cognex.VisionPro.CogRecordDisplay();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tx_ServerMessage = new System.Windows.Forms.RichTextBox();
            this.tcpipControl11 = new TCPICPC.TCPIPControl1();
            this.button2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.gb_OffsetSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetL)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetU)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetZ)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).BeginInit();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.47584F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.52415F));
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.gb_OffsetSetting, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.cogRecordDisplay1, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button2, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.92576F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 77.07423F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(683, 483);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 3;
            this.numericUpDown1.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numericUpDown1.Location = new System.Drawing.Point(176, 460);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(90, 22);
            this.numericUpDown1.TabIndex = 22;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(2, 2);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 28);
            this.button1.TabIndex = 13;
            this.button1.Text = "連線";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // gb_OffsetSetting
            // 
            this.gb_OffsetSetting.Controls.Add(this.num_OffsetL);
            this.gb_OffsetSetting.Controls.Add(this.label5);
            this.gb_OffsetSetting.Controls.Add(this.button3);
            this.gb_OffsetSetting.Controls.Add(this.button11);
            this.gb_OffsetSetting.Controls.Add(this.label1);
            this.gb_OffsetSetting.Controls.Add(this.button10);
            this.gb_OffsetSetting.Controls.Add(this.num_OffsetU);
            this.gb_OffsetSetting.Controls.Add(this.num_OffsetZ);
            this.gb_OffsetSetting.Controls.Add(this.num_OffsetY);
            this.gb_OffsetSetting.Controls.Add(this.label4);
            this.gb_OffsetSetting.Controls.Add(this.label3);
            this.gb_OffsetSetting.Controls.Add(this.label2);
            this.gb_OffsetSetting.Controls.Add(this.num_OffsetX);
            this.gb_OffsetSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gb_OffsetSetting.Location = new System.Drawing.Point(176, 2);
            this.gb_OffsetSetting.Margin = new System.Windows.Forms.Padding(2);
            this.gb_OffsetSetting.Name = "gb_OffsetSetting";
            this.gb_OffsetSetting.Padding = new System.Windows.Forms.Padding(2);
            this.gb_OffsetSetting.Size = new System.Drawing.Size(505, 101);
            this.gb_OffsetSetting.TabIndex = 20;
            this.gb_OffsetSetting.TabStop = false;
            this.gb_OffsetSetting.Text = "組裝位置補償設定";
            // 
            // num_OffsetL
            // 
            this.num_OffsetL.DecimalPlaces = 3;
            this.num_OffsetL.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.num_OffsetL.Location = new System.Drawing.Point(240, 75);
            this.num_OffsetL.Margin = new System.Windows.Forms.Padding(2);
            this.num_OffsetL.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.num_OffsetL.Name = "num_OffsetL";
            this.num_OffsetL.Size = new System.Drawing.Size(90, 22);
            this.num_OffsetL.TabIndex = 15;
            this.num_OffsetL.ValueChanged += new System.EventHandler(this.num_OffsetL_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 77);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "L_OffsetX";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(404, 15);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(50, 28);
            this.button3.TabIndex = 13;
            this.button3.Text = "讀取";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button11
            // 
            this.button11.Location = new System.Drawing.Point(350, 45);
            this.button11.Margin = new System.Windows.Forms.Padding(2);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(50, 28);
            this.button11.TabIndex = 12;
            this.button11.Text = "重設";
            this.button11.UseVisualStyleBackColor = true;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 26);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "OffsetX";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(350, 15);
            this.button10.Margin = new System.Windows.Forms.Padding(2);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(50, 28);
            this.button10.TabIndex = 11;
            this.button10.Text = "保存";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // num_OffsetU
            // 
            this.num_OffsetU.DecimalPlaces = 3;
            this.num_OffsetU.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.num_OffsetU.Location = new System.Drawing.Point(240, 45);
            this.num_OffsetU.Margin = new System.Windows.Forms.Padding(2);
            this.num_OffsetU.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.num_OffsetU.Name = "num_OffsetU";
            this.num_OffsetU.Size = new System.Drawing.Size(90, 22);
            this.num_OffsetU.TabIndex = 10;
            this.num_OffsetU.ValueChanged += new System.EventHandler(this.num_OffsetU_ValueChanged);
            // 
            // num_OffsetZ
            // 
            this.num_OffsetZ.DecimalPlaces = 3;
            this.num_OffsetZ.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.num_OffsetZ.Location = new System.Drawing.Point(240, 22);
            this.num_OffsetZ.Margin = new System.Windows.Forms.Padding(2);
            this.num_OffsetZ.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.num_OffsetZ.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.num_OffsetZ.Name = "num_OffsetZ";
            this.num_OffsetZ.Size = new System.Drawing.Size(90, 22);
            this.num_OffsetZ.TabIndex = 9;
            // 
            // num_OffsetY
            // 
            this.num_OffsetY.DecimalPlaces = 3;
            this.num_OffsetY.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.num_OffsetY.Location = new System.Drawing.Point(66, 45);
            this.num_OffsetY.Margin = new System.Windows.Forms.Padding(2);
            this.num_OffsetY.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.num_OffsetY.Name = "num_OffsetY";
            this.num_OffsetY.Size = new System.Drawing.Size(90, 22);
            this.num_OffsetY.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 47);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "OffsetU";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(189, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "OffsetZ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 49);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "OffsetY";
            // 
            // num_OffsetX
            // 
            this.num_OffsetX.DecimalPlaces = 3;
            this.num_OffsetX.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.num_OffsetX.Location = new System.Drawing.Point(66, 22);
            this.num_OffsetX.Margin = new System.Windows.Forms.Padding(2);
            this.num_OffsetX.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.num_OffsetX.Name = "num_OffsetX";
            this.num_OffsetX.Size = new System.Drawing.Size(90, 22);
            this.num_OffsetX.TabIndex = 0;
            // 
            // cogRecordDisplay1
            // 
            this.cogRecordDisplay1.ColorMapLowerClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapLowerRoiLimit = 0D;
            this.cogRecordDisplay1.ColorMapPredefined = Cognex.VisionPro.Display.CogDisplayColorMapPredefinedConstants.None;
            this.cogRecordDisplay1.ColorMapUpperClipColor = System.Drawing.Color.Black;
            this.cogRecordDisplay1.ColorMapUpperRoiLimit = 1D;
            this.cogRecordDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cogRecordDisplay1.Location = new System.Drawing.Point(177, 108);
            this.cogRecordDisplay1.MouseWheelMode = Cognex.VisionPro.Display.CogDisplayMouseWheelModeConstants.Zoom1;
            this.cogRecordDisplay1.MouseWheelSensitivity = 1D;
            this.cogRecordDisplay1.Name = "cogRecordDisplay1";
            this.cogRecordDisplay1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cogRecordDisplay1.OcxState")));
            this.cogRecordDisplay1.Size = new System.Drawing.Size(503, 347);
            this.cogRecordDisplay1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tableLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 108);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(168, 347);
            this.panel1.TabIndex = 21;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tcpipControl11, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(168, 347);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tx_ServerMessage);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 110);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(162, 260);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server接收訊息";
            // 
            // tx_ServerMessage
            // 
            this.tx_ServerMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tx_ServerMessage.Location = new System.Drawing.Point(3, 18);
            this.tx_ServerMessage.Name = "tx_ServerMessage";
            this.tx_ServerMessage.Size = new System.Drawing.Size(156, 239);
            this.tx_ServerMessage.TabIndex = 5;
            this.tx_ServerMessage.Text = "";
            // 
            // tcpipControl11
            // 
            this.tcpipControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcpipControl11.Location = new System.Drawing.Point(3, 3);
            this.tcpipControl11.Name = "tcpipControl11";
            this.tcpipControl11.Size = new System.Drawing.Size(162, 101);
            this.tcpipControl11.TabIndex = 23;
            this.tcpipControl11.ReadLines += new TCPICPC.TCPIP.ReadCLCRComplete(this.tcpipControl11_ReadLines);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(3, 461);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 19);
            this.button2.TabIndex = 6;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 483);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.gb_OffsetSetting.ResumeLayout(false);
            this.gb_OffsetSetting.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetU)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetZ)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.num_OffsetX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cogRecordDisplay1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private Cognex.VisionPro.CogRecordDisplay cogRecordDisplay1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox gb_OffsetSetting;
        private System.Windows.Forms.Button button11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button10;
        private System.Windows.Forms.NumericUpDown num_OffsetU;
        private System.Windows.Forms.NumericUpDown num_OffsetZ;
        private System.Windows.Forms.NumericUpDown num_OffsetY;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown num_OffsetX;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox tx_ServerMessage;
        private TCPICPC.TCPIPControl1 tcpipControl11;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.NumericUpDown num_OffsetL;
        private System.Windows.Forms.Label label5;

    }
}

