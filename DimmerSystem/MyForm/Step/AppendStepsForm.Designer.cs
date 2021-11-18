
namespace LightController.MyForm.Step
{
    partial class AppendStepsForm
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
            this.cancelButton = new Sunny.UI.UIButton();
            this.appendButton = new Sunny.UI.UIButton();
            this.stepCountNUD = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepCountNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Controls.Add(this.appendButton);
            this.mainPanel.Controls.Add(this.stepCountNUD);
            this.mainPanel.Controls.Add(this.label2);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(200, 85);
            this.mainPanel.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("黑体", 8F);
            this.cancelButton.Location = new System.Drawing.Point(117, 48);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 29;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // appendButton
            // 
            this.appendButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.appendButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.appendButton.Font = new System.Drawing.Font("黑体", 8F);
            this.appendButton.Location = new System.Drawing.Point(33, 48);
            this.appendButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.appendButton.Name = "appendButton";
            this.appendButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.appendButton.Size = new System.Drawing.Size(54, 20);
            this.appendButton.Style = Sunny.UI.UIStyle.Custom;
            this.appendButton.TabIndex = 30;
            this.appendButton.Text = "追加步";
            this.appendButton.Click += new System.EventHandler(this.appendButton_Click);
            // 
            // stepCountNUD
            // 
            this.stepCountNUD.BackColor = System.Drawing.SystemColors.Window;
            this.stepCountNUD.Font = new System.Drawing.Font("黑体", 8F);
            this.stepCountNUD.ForeColor = System.Drawing.SystemColors.MenuText;
            this.stepCountNUD.Location = new System.Drawing.Point(117, 15);
            this.stepCountNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.stepCountNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.stepCountNUD.Name = "stepCountNUD";
            this.stepCountNUD.Size = new System.Drawing.Size(56, 20);
            this.stepCountNUD.TabIndex = 28;
            this.stepCountNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stepCountNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(27, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 11);
            this.label2.TabIndex = 27;
            this.label2.Text = "追加的步数";
            // 
            // AppendStepsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(200, 120);
            this.Controls.Add(this.mainPanel);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AppendStepsForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "追加步数";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("黑体", 10F);
            this.Load += new System.EventHandler(this.AppendStepsForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepCountNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.NumericUpDown stepCountNUD;
        private System.Windows.Forms.Label label2;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton appendButton;
    }
}