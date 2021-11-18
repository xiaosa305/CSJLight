
namespace LightController.MyForm.Step
{
    partial class MaterialSaveForm
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
            this.mainPanel = new System.Windows.Forms.Panel();
            this.mNameTextBox = new Sunny.UI.UITextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cancelButton = new Sunny.UI.UIButton();
            this.saveButton = new Sunny.UI.UIButton();
            this.endNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lightRadioButton = new System.Windows.Forms.RadioButton();
            this.genericRadioButton = new System.Windows.Forms.RadioButton();
            this.tempRadioButton = new System.Windows.Forms.RadioButton();
            this.lightLabel = new System.Windows.Forms.Label();
            this.modeLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.allStepButton = new Sunny.UI.UIButton();
            this.startNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.selectAllCheckBox = new System.Windows.Forms.CheckBox();
            this.noticeLabel = new System.Windows.Forms.Label();
            this.tdFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.tdCBDemo = new System.Windows.Forms.CheckBox();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).BeginInit();
            this.panel1.SuspendLayout();
            this.tdFLP.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.mNameTextBox);
            this.mainPanel.Controls.Add(this.label3);
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Controls.Add(this.endNumericUpDown);
            this.mainPanel.Controls.Add(this.lightRadioButton);
            this.mainPanel.Controls.Add(this.genericRadioButton);
            this.mainPanel.Controls.Add(this.tempRadioButton);
            this.mainPanel.Controls.Add(this.lightLabel);
            this.mainPanel.Controls.Add(this.modeLabel);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.allStepButton);
            this.mainPanel.Controls.Add(this.startNumericUpDown);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Controls.Add(this.panel1);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(500, 385);
            this.mainPanel.TabIndex = 0;
            // 
            // mNameTextBox
            // 
            this.mNameTextBox.ButtonSymbol = 61761;
            this.mNameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.mNameTextBox.Enabled = false;
            this.mNameTextBox.FillColor = System.Drawing.Color.White;
            this.mNameTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.mNameTextBox.Location = new System.Drawing.Point(346, 295);
            this.mNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mNameTextBox.Maximum = 2147483647D;
            this.mNameTextBox.Minimum = -2147483648D;
            this.mNameTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.mNameTextBox.Name = "mNameTextBox";
            this.mNameTextBox.Padding = new System.Windows.Forms.Padding(5);
            this.mNameTextBox.Size = new System.Drawing.Size(126, 20);
            this.mNameTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.mNameTextBox.TabIndex = 60;
            this.mNameTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.mNameTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mNameTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(298, 302);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 11);
            this.label3.TabIndex = 59;
            this.label3.Text = "素材名";
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(403, 335);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(64, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 58;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.saveButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.saveButton.Location = new System.Drawing.Point(304, 334);
            this.saveButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.saveButton.Name = "saveButton";
            this.saveButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.saveButton.Size = new System.Drawing.Size(64, 20);
            this.saveButton.Style = Sunny.UI.UIStyle.Custom;
            this.saveButton.TabIndex = 58;
            this.saveButton.Text = "保存";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // endNumericUpDown
            // 
            this.endNumericUpDown.BackColor = System.Drawing.SystemColors.Window;
            this.endNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.endNumericUpDown.ForeColor = System.Drawing.SystemColors.MenuText;
            this.endNumericUpDown.Location = new System.Drawing.Point(417, 157);
            this.endNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.endNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.endNumericUpDown.Name = "endNumericUpDown";
            this.endNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.endNumericUpDown.TabIndex = 57;
            this.endNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.endNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lightRadioButton
            // 
            this.lightRadioButton.AutoSize = true;
            this.lightRadioButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lightRadioButton.ForeColor = System.Drawing.Color.White;
            this.lightRadioButton.Location = new System.Drawing.Point(396, 259);
            this.lightRadioButton.Name = "lightRadioButton";
            this.lightRadioButton.Size = new System.Drawing.Size(71, 15);
            this.lightRadioButton.TabIndex = 54;
            this.lightRadioButton.Text = "单灯素材";
            this.lightRadioButton.UseVisualStyleBackColor = true;
            this.lightRadioButton.CheckedChanged += new System.EventHandler(this.tempRadioButton_CheckedChanged);
            // 
            // genericRadioButton
            // 
            this.genericRadioButton.AutoSize = true;
            this.genericRadioButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.genericRadioButton.ForeColor = System.Drawing.Color.White;
            this.genericRadioButton.Location = new System.Drawing.Point(300, 259);
            this.genericRadioButton.Name = "genericRadioButton";
            this.genericRadioButton.Size = new System.Drawing.Size(71, 15);
            this.genericRadioButton.TabIndex = 55;
            this.genericRadioButton.Text = "通用素材";
            this.genericRadioButton.UseVisualStyleBackColor = true;
            this.genericRadioButton.CheckedChanged += new System.EventHandler(this.tempRadioButton_CheckedChanged);
            // 
            // tempRadioButton
            // 
            this.tempRadioButton.AutoSize = true;
            this.tempRadioButton.Checked = true;
            this.tempRadioButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.tempRadioButton.ForeColor = System.Drawing.Color.White;
            this.tempRadioButton.Location = new System.Drawing.Point(300, 231);
            this.tempRadioButton.Name = "tempRadioButton";
            this.tempRadioButton.Size = new System.Drawing.Size(71, 15);
            this.tempRadioButton.TabIndex = 56;
            this.tempRadioButton.TabStop = true;
            this.tempRadioButton.Text = "临时素材";
            this.tempRadioButton.UseVisualStyleBackColor = true;
            this.tempRadioButton.CheckedChanged += new System.EventHandler(this.tempRadioButton_CheckedChanged);
            // 
            // lightLabel
            // 
            this.lightLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.lightLabel.ForeColor = System.Drawing.Color.White;
            this.lightLabel.Location = new System.Drawing.Point(296, 52);
            this.lightLabel.Name = "lightLabel";
            this.lightLabel.Size = new System.Drawing.Size(182, 37);
            this.lightLabel.TabIndex = 53;
            this.lightLabel.Text = "灯具：";
            // 
            // modeLabel
            // 
            this.modeLabel.AutoSize = true;
            this.modeLabel.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold);
            this.modeLabel.ForeColor = System.Drawing.Color.White;
            this.modeLabel.Location = new System.Drawing.Point(295, 19);
            this.modeLabel.Name = "modeLabel";
            this.modeLabel.Size = new System.Drawing.Size(82, 14);
            this.modeLabel.TabIndex = 53;
            this.modeLabel.Text = "当前模式：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(381, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 11);
            this.label1.TabIndex = 52;
            this.label1.Text = "-";
            // 
            // allStepButton
            // 
            this.allStepButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.allStepButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.allStepButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.allStepButton.Location = new System.Drawing.Point(419, 120);
            this.allStepButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.allStepButton.Name = "allStepButton";
            this.allStepButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.allStepButton.Size = new System.Drawing.Size(54, 20);
            this.allStepButton.Style = Sunny.UI.UIStyle.Custom;
            this.allStepButton.TabIndex = 51;
            this.allStepButton.Text = "全选";
            this.allStepButton.Click += new System.EventHandler(this.allStepButton_Click);
            // 
            // startNumericUpDown
            // 
            this.startNumericUpDown.BackColor = System.Drawing.SystemColors.Window;
            this.startNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.startNumericUpDown.ForeColor = System.Drawing.SystemColors.MenuText;
            this.startNumericUpDown.Location = new System.Drawing.Point(301, 157);
            this.startNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.startNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startNumericUpDown.Name = "startNumericUpDown";
            this.startNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.startNumericUpDown.TabIndex = 50;
            this.startNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(298, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 11);
            this.label2.TabIndex = 49;
            this.label2.Text = "请选择要保存的步数";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.selectAllCheckBox);
            this.panel1.Controls.Add(this.noticeLabel);
            this.panel1.Controls.Add(this.tdFLP);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 383);
            this.panel1.TabIndex = 48;
            // 
            // selectAllCheckBox
            // 
            this.selectAllCheckBox.AutoSize = true;
            this.selectAllCheckBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.selectAllCheckBox.ForeColor = System.Drawing.Color.White;
            this.selectAllCheckBox.Location = new System.Drawing.Point(186, 19);
            this.selectAllCheckBox.Name = "selectAllCheckBox";
            this.selectAllCheckBox.Size = new System.Drawing.Size(48, 15);
            this.selectAllCheckBox.TabIndex = 38;
            this.selectAllCheckBox.Text = "全选";
            this.selectAllCheckBox.UseVisualStyleBackColor = true;
            this.selectAllCheckBox.CheckedChanged += new System.EventHandler(this.selectAllCheckBox_CheckedChanged);
            // 
            // noticeLabel
            // 
            this.noticeLabel.AutoSize = true;
            this.noticeLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.noticeLabel.ForeColor = System.Drawing.Color.White;
            this.noticeLabel.Location = new System.Drawing.Point(16, 19);
            this.noticeLabel.Name = "noticeLabel";
            this.noticeLabel.Size = new System.Drawing.Size(113, 11);
            this.noticeLabel.TabIndex = 37;
            this.noticeLabel.Text = "请勾选要保存的通道";
            // 
            // tdFLP
            // 
            this.tdFLP.AutoScroll = true;
            this.tdFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tdFLP.Controls.Add(this.tdCBDemo);
            this.tdFLP.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tdFLP.Location = new System.Drawing.Point(0, 52);
            this.tdFLP.Name = "tdFLP";
            this.tdFLP.Size = new System.Drawing.Size(278, 331);
            this.tdFLP.TabIndex = 0;
            // 
            // tdCBDemo
            // 
            this.tdCBDemo.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.tdCBDemo.ForeColor = System.Drawing.Color.White;
            this.tdCBDemo.Location = new System.Drawing.Point(10, 5);
            this.tdCBDemo.Margin = new System.Windows.Forms.Padding(10, 5, 5, 5);
            this.tdCBDemo.Name = "tdCBDemo";
            this.tdCBDemo.Size = new System.Drawing.Size(111, 24);
            this.tdCBDemo.TabIndex = 0;
            this.tdCBDemo.Text = "通道名Demo";
            this.tdCBDemo.UseVisualStyleBackColor = true;
            this.tdCBDemo.Visible = false;
            // 
            // MaterialSaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(500, 420);
            this.Controls.Add(this.mainPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MaterialSaveForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "保存素材";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Load += new System.EventHandler(this.MaterialForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tdFLP.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.NumericUpDown endNumericUpDown;
        private System.Windows.Forms.RadioButton lightRadioButton;
        private System.Windows.Forms.RadioButton genericRadioButton;
        private System.Windows.Forms.RadioButton tempRadioButton;
        private System.Windows.Forms.Label modeLabel;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIButton allStepButton;
        private System.Windows.Forms.NumericUpDown startNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.CheckBox selectAllCheckBox;
        private System.Windows.Forms.Label noticeLabel;
        private System.Windows.Forms.FlowLayoutPanel tdFLP;
        private System.Windows.Forms.CheckBox tdCBDemo;
        private System.Windows.Forms.Label lightLabel;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton saveButton;
        private System.Windows.Forms.Label label3;
        private Sunny.UI.UITextBox mNameTextBox;
    }
}