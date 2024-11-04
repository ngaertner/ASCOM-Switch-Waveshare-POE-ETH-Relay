namespace ASCOM.Waveshare_Modbus_POE_ETH_Relay.Switch
{
    internal partial class SetupDialogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetupDialogForm));
            this.cmdOK = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.picASCOM = new System.Windows.Forms.PictureBox();
            this.chkTrace = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxRelay1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxRelay2 = new System.Windows.Forms.TextBox();
            this.textBoxRelay3 = new System.Windows.Forms.TextBox();
            this.textBoxRelay4 = new System.Windows.Forms.TextBox();
            this.textBoxRelay5 = new System.Windows.Forms.TextBox();
            this.textBoxRelay6 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxRelay7 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxRelay8 = new System.Windows.Forms.TextBox();
            this.checkBoxRelay1Visible = new System.Windows.Forms.CheckBox();
            this.checkBoxRelay2Visible = new System.Windows.Forms.CheckBox();
            this.checkBoxRelay3Visible = new System.Windows.Forms.CheckBox();
            this.checkBoxRelay4Visible = new System.Windows.Forms.CheckBox();
            this.checkBoxRelay5Visible = new System.Windows.Forms.CheckBox();
            this.checkBoxRelay6Visible = new System.Windows.Forms.CheckBox();
            this.checkBoxRelay7Visible = new System.Windows.Forms.CheckBox();
            this.checkBoxRelay8Visible = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).BeginInit();
            this.SuspendLayout();
            // 
            // cmdOK
            // 
            this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdOK.Location = new System.Drawing.Point(308, 321);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(59, 24);
            this.cmdOK.TabIndex = 0;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.CmdOK_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdCancel.Location = new System.Drawing.Point(308, 351);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(59, 25);
            this.cmdCancel.TabIndex = 1;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // picASCOM
            // 
            this.picASCOM.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picASCOM.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picASCOM.Image = ((System.Drawing.Image)(resources.GetObject("picASCOM.Image")));
            this.picASCOM.Location = new System.Drawing.Point(319, 9);
            this.picASCOM.Name = "picASCOM";
            this.picASCOM.Size = new System.Drawing.Size(48, 56);
            this.picASCOM.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picASCOM.TabIndex = 3;
            this.picASCOM.TabStop = false;
            this.picASCOM.Click += new System.EventHandler(this.BrowseToAscom);
            this.picASCOM.DoubleClick += new System.EventHandler(this.BrowseToAscom);
            // 
            // chkTrace
            // 
            this.chkTrace.AutoSize = true;
            this.chkTrace.Location = new System.Drawing.Point(71, 95);
            this.chkTrace.Name = "chkTrace";
            this.chkTrace.Size = new System.Drawing.Size(69, 17);
            this.chkTrace.TabIndex = 3;
            this.chkTrace.Text = "Trace on";
            this.chkTrace.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "IP";
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(71, 43);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(139, 20);
            this.textBoxIP.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Port";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(71, 69);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(64, 20);
            this.textBoxPort.TabIndex = 2;
            this.textBoxPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxPort_KeyPress);
            // 
            // textBoxRelay1
            // 
            this.textBoxRelay1.Location = new System.Drawing.Point(71, 129);
            this.textBoxRelay1.Name = "textBoxRelay1";
            this.textBoxRelay1.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Switch 1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "Switch 2";
            // 
            // textBoxRelay2
            // 
            this.textBoxRelay2.Location = new System.Drawing.Point(71, 155);
            this.textBoxRelay2.Name = "textBoxRelay2";
            this.textBoxRelay2.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay2.TabIndex = 6;
            // 
            // textBoxRelay3
            // 
            this.textBoxRelay3.Location = new System.Drawing.Point(71, 181);
            this.textBoxRelay3.Name = "textBoxRelay3";
            this.textBoxRelay3.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay3.TabIndex = 8;
            // 
            // textBoxRelay4
            // 
            this.textBoxRelay4.Location = new System.Drawing.Point(71, 207);
            this.textBoxRelay4.Name = "textBoxRelay4";
            this.textBoxRelay4.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay4.TabIndex = 10;
            // 
            // textBoxRelay5
            // 
            this.textBoxRelay5.Location = new System.Drawing.Point(71, 233);
            this.textBoxRelay5.Name = "textBoxRelay5";
            this.textBoxRelay5.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay5.TabIndex = 12;
            // 
            // textBoxRelay6
            // 
            this.textBoxRelay6.Location = new System.Drawing.Point(71, 259);
            this.textBoxRelay6.Name = "textBoxRelay6";
            this.textBoxRelay6.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay6.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 184);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "Switch 3";
            // 
            // textBoxRelay7
            // 
            this.textBoxRelay7.Location = new System.Drawing.Point(71, 285);
            this.textBoxRelay7.Name = "textBoxRelay7";
            this.textBoxRelay7.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay7.TabIndex = 17;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Switch 4";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(16, 236);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Switch 5";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(16, 262);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Switch 6";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(16, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 14;
            this.label9.Text = "Switch 7";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(16, 314);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Switch 8";
            // 
            // textBoxRelay8
            // 
            this.textBoxRelay8.Location = new System.Drawing.Point(71, 311);
            this.textBoxRelay8.Name = "textBoxRelay8";
            this.textBoxRelay8.Size = new System.Drawing.Size(139, 20);
            this.textBoxRelay8.TabIndex = 19;
            // 
            // checkBoxRelay1Visible
            // 
            this.checkBoxRelay1Visible.AutoSize = true;
            this.checkBoxRelay1Visible.Checked = true;
            this.checkBoxRelay1Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay1Visible.Enabled = false;
            this.checkBoxRelay1Visible.Location = new System.Drawing.Point(216, 131);
            this.checkBoxRelay1Visible.Name = "checkBoxRelay1Visible";
            this.checkBoxRelay1Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay1Visible.TabIndex = 5;
            this.checkBoxRelay1Visible.Text = "visible";
            this.checkBoxRelay1Visible.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelay2Visible
            // 
            this.checkBoxRelay2Visible.AutoSize = true;
            this.checkBoxRelay2Visible.Checked = true;
            this.checkBoxRelay2Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay2Visible.Enabled = false;
            this.checkBoxRelay2Visible.Location = new System.Drawing.Point(216, 157);
            this.checkBoxRelay2Visible.Name = "checkBoxRelay2Visible";
            this.checkBoxRelay2Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay2Visible.TabIndex = 7;
            this.checkBoxRelay2Visible.Text = "visible";
            this.checkBoxRelay2Visible.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelay3Visible
            // 
            this.checkBoxRelay3Visible.AutoSize = true;
            this.checkBoxRelay3Visible.Checked = true;
            this.checkBoxRelay3Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay3Visible.Enabled = false;
            this.checkBoxRelay3Visible.Location = new System.Drawing.Point(216, 180);
            this.checkBoxRelay3Visible.Name = "checkBoxRelay3Visible";
            this.checkBoxRelay3Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay3Visible.TabIndex = 9;
            this.checkBoxRelay3Visible.Text = "visible";
            this.checkBoxRelay3Visible.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelay4Visible
            // 
            this.checkBoxRelay4Visible.AutoSize = true;
            this.checkBoxRelay4Visible.Checked = true;
            this.checkBoxRelay4Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay4Visible.Enabled = false;
            this.checkBoxRelay4Visible.Location = new System.Drawing.Point(216, 206);
            this.checkBoxRelay4Visible.Name = "checkBoxRelay4Visible";
            this.checkBoxRelay4Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay4Visible.TabIndex = 11;
            this.checkBoxRelay4Visible.Text = "visible";
            this.checkBoxRelay4Visible.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelay5Visible
            // 
            this.checkBoxRelay5Visible.AutoSize = true;
            this.checkBoxRelay5Visible.Checked = true;
            this.checkBoxRelay5Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay5Visible.Enabled = false;
            this.checkBoxRelay5Visible.Location = new System.Drawing.Point(216, 232);
            this.checkBoxRelay5Visible.Name = "checkBoxRelay5Visible";
            this.checkBoxRelay5Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay5Visible.TabIndex = 13;
            this.checkBoxRelay5Visible.Text = "visible";
            this.checkBoxRelay5Visible.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelay6Visible
            // 
            this.checkBoxRelay6Visible.AutoSize = true;
            this.checkBoxRelay6Visible.Checked = true;
            this.checkBoxRelay6Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay6Visible.Enabled = false;
            this.checkBoxRelay6Visible.Location = new System.Drawing.Point(216, 258);
            this.checkBoxRelay6Visible.Name = "checkBoxRelay6Visible";
            this.checkBoxRelay6Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay6Visible.TabIndex = 15;
            this.checkBoxRelay6Visible.Text = "visible";
            this.checkBoxRelay6Visible.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelay7Visible
            // 
            this.checkBoxRelay7Visible.AutoSize = true;
            this.checkBoxRelay7Visible.Checked = true;
            this.checkBoxRelay7Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay7Visible.Enabled = false;
            this.checkBoxRelay7Visible.Location = new System.Drawing.Point(216, 287);
            this.checkBoxRelay7Visible.Name = "checkBoxRelay7Visible";
            this.checkBoxRelay7Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay7Visible.TabIndex = 18;
            this.checkBoxRelay7Visible.Text = "visible";
            this.checkBoxRelay7Visible.UseVisualStyleBackColor = true;
            // 
            // checkBoxRelay8Visible
            // 
            this.checkBoxRelay8Visible.AutoSize = true;
            this.checkBoxRelay8Visible.Checked = true;
            this.checkBoxRelay8Visible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxRelay8Visible.Enabled = false;
            this.checkBoxRelay8Visible.Location = new System.Drawing.Point(216, 312);
            this.checkBoxRelay8Visible.Name = "checkBoxRelay8Visible";
            this.checkBoxRelay8Visible.Size = new System.Drawing.Size(55, 17);
            this.checkBoxRelay8Visible.TabIndex = 20;
            this.checkBoxRelay8Visible.Text = "visible";
            this.checkBoxRelay8Visible.UseVisualStyleBackColor = true;
            // 
            // SetupDialogForm
            // 
            this.AcceptButton = this.cmdOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 384);
            this.Controls.Add(this.checkBoxRelay8Visible);
            this.Controls.Add(this.checkBoxRelay7Visible);
            this.Controls.Add(this.checkBoxRelay6Visible);
            this.Controls.Add(this.checkBoxRelay5Visible);
            this.Controls.Add(this.checkBoxRelay4Visible);
            this.Controls.Add(this.checkBoxRelay3Visible);
            this.Controls.Add(this.checkBoxRelay2Visible);
            this.Controls.Add(this.checkBoxRelay1Visible);
            this.Controls.Add(this.textBoxRelay8);
            this.Controls.Add(this.textBoxRelay7);
            this.Controls.Add(this.textBoxRelay6);
            this.Controls.Add(this.textBoxRelay5);
            this.Controls.Add(this.textBoxRelay4);
            this.Controls.Add(this.textBoxRelay3);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxRelay2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxRelay1);
            this.Controls.Add(this.textBoxPort);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxIP);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.chkTrace);
            this.Controls.Add(this.picASCOM);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetupDialogForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WaveshareModbus POE ETH Relay Setup";
            this.Load += new System.EventHandler(this.SetupDialogForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picASCOM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cmdOK;
    private System.Windows.Forms.Button cmdCancel;
    private System.Windows.Forms.PictureBox picASCOM;
    private System.Windows.Forms.CheckBox chkTrace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxIP;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxPort;
        private System.Windows.Forms.TextBox textBoxRelay1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxRelay2;
        private System.Windows.Forms.TextBox textBoxRelay3;
        private System.Windows.Forms.TextBox textBoxRelay4;
        private System.Windows.Forms.TextBox textBoxRelay5;
        private System.Windows.Forms.TextBox textBoxRelay6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxRelay7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxRelay8;
        private System.Windows.Forms.CheckBox checkBoxRelay1Visible;
        private System.Windows.Forms.CheckBox checkBoxRelay2Visible;
        private System.Windows.Forms.CheckBox checkBoxRelay3Visible;
        private System.Windows.Forms.CheckBox checkBoxRelay4Visible;
        private System.Windows.Forms.CheckBox checkBoxRelay5Visible;
        private System.Windows.Forms.CheckBox checkBoxRelay6Visible;
        private System.Windows.Forms.CheckBox checkBoxRelay7Visible;
        private System.Windows.Forms.CheckBox checkBoxRelay8Visible;
    }
}