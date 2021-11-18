
namespace LightController.MyForm.LightList
{
    partial class LightsAddForm
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
            this.cancelButton = new Sunny.UI.UIButton();
            this.enterButton = new Sunny.UI.UIButton();
            this.nameTypeLabel = new System.Windows.Forms.Label();
            this.startAddrNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.lightCountNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startAddrNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightCountNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.enterButton);
            this.panel1.Controls.Add(this.nameTypeLabel);
            this.panel1.Controls.Add(this.startAddrNumericUpDown);
            this.panel1.Controls.Add(this.lightCountNumericUpDown);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 165);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(138, 124);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 17;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.enterButton.Location = new System.Drawing.Point(47, 124);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(54, 20);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 18;
            this.enterButton.Text = "确定";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // nameTypeLabel
            // 
            this.nameTypeLabel.AutoSize = true;
            this.nameTypeLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.nameTypeLabel.ForeColor = System.Drawing.Color.White;
            this.nameTypeLabel.Location = new System.Drawing.Point(136, 24);
            this.nameTypeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameTypeLabel.Name = "nameTypeLabel";
            this.nameTypeLabel.Size = new System.Drawing.Size(41, 11);
            this.nameTypeLabel.TabIndex = 13;
            this.nameTypeLabel.Tag = "999";
            this.nameTypeLabel.Text = "灯具名";
            // 
            // startAddrNumericUpDown
            // 
            this.startAddrNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.startAddrNumericUpDown.ForeColor = System.Drawing.Color.Black;
            this.startAddrNumericUpDown.Location = new System.Drawing.Point(138, 54);
            this.startAddrNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.startAddrNumericUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.startAddrNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startAddrNumericUpDown.Name = "startAddrNumericUpDown";
            this.startAddrNumericUpDown.Size = new System.Drawing.Size(62, 20);
            this.startAddrNumericUpDown.TabIndex = 11;
            this.startAddrNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startAddrNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lightCountNumericUpDown
            // 
            this.lightCountNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lightCountNumericUpDown.ForeColor = System.Drawing.Color.Black;
            this.lightCountNumericUpDown.Location = new System.Drawing.Point(138, 86);
            this.lightCountNumericUpDown.Margin = new System.Windows.Forms.Padding(2);
            this.lightCountNumericUpDown.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.lightCountNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lightCountNumericUpDown.Name = "lightCountNumericUpDown";
            this.lightCountNumericUpDown.Size = new System.Drawing.Size(62, 20);
            this.lightCountNumericUpDown.TabIndex = 12;
            this.lightCountNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightCountNumericUpDown.Value = new decimal(new int[] {
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
            this.label2.Location = new System.Drawing.Point(19, 56);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 11);
            this.label2.TabIndex = 8;
            this.label2.Text = "起始灯具地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(19, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 11);
            this.label3.TabIndex = 9;
            this.label3.Text = "添加灯具名称：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 88);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 11);
            this.label1.TabIndex = 10;
            this.label1.Text = "添加灯具数量：";
            // 
            // LightsAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(240, 200);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LightsAddForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "添加灯具";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.LightsAddForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startAddrNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lightCountNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label nameTypeLabel;
        private System.Windows.Forms.NumericUpDown startAddrNumericUpDown;
        private System.Windows.Forms.NumericUpDown lightCountNumericUpDown;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton enterButton;
    }
}