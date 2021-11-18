
namespace LightController.MyForm.Step
{
    partial class DeleteStepsForm
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
            this.deleteButton = new Sunny.UI.UIButton();
            this.startNUD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.endNUD = new System.Windows.Forms.NumericUpDown();
            this.allButton = new Sunny.UI.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startNUD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.endNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.allButton);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.endNUD);
            this.panel1.Controls.Add(this.startNUD);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 125);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(118, 87);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 33;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deleteButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.deleteButton.Location = new System.Drawing.Point(30, 87);
            this.deleteButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.deleteButton.Size = new System.Drawing.Size(54, 20);
            this.deleteButton.Style = Sunny.UI.UIStyle.Custom;
            this.deleteButton.TabIndex = 34;
            this.deleteButton.Text = "删除步";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // startNUD
            // 
            this.startNUD.BackColor = System.Drawing.SystemColors.Window;
            this.startNUD.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.startNUD.ForeColor = System.Drawing.SystemColors.MenuText;
            this.startNUD.Location = new System.Drawing.Point(28, 47);
            this.startNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.startNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startNUD.Name = "startNUD";
            this.startNUD.Size = new System.Drawing.Size(56, 20);
            this.startNUD.TabIndex = 32;
            this.startNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startNUD.Value = new decimal(new int[] {
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
            this.label2.Location = new System.Drawing.Point(26, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 11);
            this.label2.TabIndex = 31;
            this.label2.Text = "请选择删除步数：";
            // 
            // endNUD
            // 
            this.endNUD.BackColor = System.Drawing.SystemColors.Window;
            this.endNUD.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.endNUD.ForeColor = System.Drawing.SystemColors.MenuText;
            this.endNUD.Location = new System.Drawing.Point(115, 47);
            this.endNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.endNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.endNUD.Name = "endNUD";
            this.endNUD.Size = new System.Drawing.Size(56, 20);
            this.endNUD.TabIndex = 32;
            this.endNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.endNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // allButton
            // 
            this.allButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.allButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.allButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.allButton.Location = new System.Drawing.Point(118, 14);
            this.allButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.allButton.Name = "allButton";
            this.allButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.allButton.Size = new System.Drawing.Size(54, 20);
            this.allButton.Style = Sunny.UI.UIStyle.Custom;
            this.allButton.TabIndex = 34;
            this.allButton.Text = "全选";
            this.allButton.Click += new System.EventHandler(this.allStepButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(94, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 11);
            this.label1.TabIndex = 35;
            this.label1.Text = "-";
            // 
            // DeleteStepsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(200, 160);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteStepsForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "删除步数";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.DeleteStepsForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startNUD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.endNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton allButton;
        private Sunny.UI.UIButton deleteButton;
        private System.Windows.Forms.NumericUpDown endNUD;
        private System.Windows.Forms.NumericUpDown startNUD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}