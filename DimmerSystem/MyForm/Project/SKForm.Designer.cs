
namespace LightController.MyForm.Project
{
    partial class SKForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.jgtNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.sceneStepTimeNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.mSceneLKTextBox = new Sunny.UI.UITextBox();
            this.cancelButton = new Sunny.UI.UIButton();
            this.saveButton = new Sunny.UI.UIButton();
            this.sceneLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sceneStepTimeNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.jgtNumericUpDown);
            this.panel1.Controls.Add(this.sceneStepTimeNumericUpDown);
            this.panel1.Controls.Add(this.mSceneLKTextBox);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.saveButton);
            this.panel1.Controls.Add(this.sceneLabel);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 225);
            this.panel1.TabIndex = 0;
            // 
            // jgtNumericUpDown
            // 
            this.jgtNumericUpDown.BackColor = System.Drawing.SystemColors.Window;
            this.jgtNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.jgtNumericUpDown.ForeColor = System.Drawing.SystemColors.MenuText;
            this.jgtNumericUpDown.Location = new System.Drawing.Point(147, 74);
            this.jgtNumericUpDown.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.jgtNumericUpDown.Name = "jgtNumericUpDown";
            this.jgtNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.jgtNumericUpDown.TabIndex = 25;
            this.jgtNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.jgtNumericUpDown.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            // 
            // sceneStepTimeNumericUpDown
            // 
            this.sceneStepTimeNumericUpDown.BackColor = System.Drawing.SystemColors.Window;
            this.sceneStepTimeNumericUpDown.DecimalPlaces = 2;
            this.sceneStepTimeNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.sceneStepTimeNumericUpDown.ForeColor = System.Drawing.SystemColors.MenuText;
            this.sceneStepTimeNumericUpDown.Increment = new decimal(new int[] {
            4,
            0,
            0,
            131072});
            this.sceneStepTimeNumericUpDown.Location = new System.Drawing.Point(147, 48);
            this.sceneStepTimeNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.sceneStepTimeNumericUpDown.Name = "sceneStepTimeNumericUpDown";
            this.sceneStepTimeNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.sceneStepTimeNumericUpDown.TabIndex = 26;
            this.sceneStepTimeNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sceneStepTimeNumericUpDown.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
            this.sceneStepTimeNumericUpDown.ValueChanged += new System.EventHandler(this.sceneStepTimeNumericUpDown_ValueChanged);
            // 
            // mSceneLKTextBox
            // 
            this.mSceneLKTextBox.ButtonSymbol = 61761;
            this.mSceneLKTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.mSceneLKTextBox.FillColor = System.Drawing.Color.White;
            this.mSceneLKTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.mSceneLKTextBox.Location = new System.Drawing.Point(18, 142);
            this.mSceneLKTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.mSceneLKTextBox.Maximum = 2147483647D;
            this.mSceneLKTextBox.MaxLength = 20;
            this.mSceneLKTextBox.Minimum = -2147483648D;
            this.mSceneLKTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.mSceneLKTextBox.Name = "mSceneLKTextBox";
            this.mSceneLKTextBox.Padding = new System.Windows.Forms.Padding(5);
            this.mSceneLKTextBox.Size = new System.Drawing.Size(185, 20);
            this.mSceneLKTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.mSceneLKTextBox.TabIndex = 24;
            this.mSceneLKTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.mSceneLKTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mSceneLKTextBox_KeyPress);
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(124, 182);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 22;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelSkinButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.saveButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.saveButton.Location = new System.Drawing.Point(40, 182);
            this.saveButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.saveButton.Name = "saveButton";
            this.saveButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.saveButton.Size = new System.Drawing.Size(54, 20);
            this.saveButton.Style = Sunny.UI.UIStyle.Custom;
            this.saveButton.TabIndex = 23;
            this.saveButton.Text = "保存设置";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // sceneLabel
            // 
            this.sceneLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.sceneLabel.ForeColor = System.Drawing.Color.White;
            this.sceneLabel.Location = new System.Drawing.Point(75, 20);
            this.sceneLabel.Name = "sceneLabel";
            this.sceneLabel.Size = new System.Drawing.Size(128, 11);
            this.sceneLabel.TabIndex = 17;
            this.sceneLabel.Text = "激情(01)";
            this.sceneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(16, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 11);
            this.label5.TabIndex = 18;
            this.label5.Text = "音频链表：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(16, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 11);
            this.label1.TabIndex = 19;
            this.label1.Text = "场景名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(16, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 11);
            this.label3.TabIndex = 20;
            this.label3.Text = "叠加后间隔时间(ms)：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 11);
            this.label2.TabIndex = 21;
            this.label2.Text = "音频步时间(s)：";
            // 
            // SKForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(220, 260);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SKForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "音频场景设置";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.SKForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.jgtNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sceneStepTimeNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown jgtNumericUpDown;
        private System.Windows.Forms.NumericUpDown sceneStepTimeNumericUpDown;
        private Sunny.UI.UITextBox mSceneLKTextBox;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton saveButton;
        private System.Windows.Forms.Label sceneLabel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}