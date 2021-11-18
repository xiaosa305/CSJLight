
namespace LightController.MyForm.Project
{
    partial class RenameOrCopyForm
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
            this.projectNameLabel = new System.Windows.Forms.Label();
            this.cancelButton = new Sunny.UI.UIButton();
            this.enterButton = new Sunny.UI.UIButton();
            this.projectNameTextBox = new Sunny.UI.UITextBox();
            this.newLabel = new System.Windows.Forms.Label();
            this.oldLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // projectNameLabel
            // 
            this.projectNameLabel.AutoSize = true;
            this.projectNameLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this.projectNameLabel.ForeColor = System.Drawing.Color.White;
            this.projectNameLabel.Location = new System.Drawing.Point(87, 27);
            this.projectNameLabel.Name = "projectNameLabel";
            this.projectNameLabel.Size = new System.Drawing.Size(57, 12);
            this.projectNameLabel.TabIndex = 14;
            this.projectNameLabel.Text = "原工程名";
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(166, 100);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 18;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.enterButton.Location = new System.Drawing.Point(55, 100);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(54, 20);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 19;
            this.enterButton.Text = "确定";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.ButtonSymbol = 61761;
            this.projectNameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.projectNameTextBox.FillColor = System.Drawing.Color.White;
            this.projectNameTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.projectNameTextBox.Location = new System.Drawing.Point(87, 58);
            this.projectNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.projectNameTextBox.Maximum = 2147483647D;
            this.projectNameTextBox.Minimum = -2147483648D;
            this.projectNameTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Padding = new System.Windows.Forms.Padding(5);
            this.projectNameTextBox.Size = new System.Drawing.Size(157, 20);
            this.projectNameTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.projectNameTextBox.TabIndex = 21;
            this.projectNameTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // newLabel
            // 
            this.newLabel.AutoSize = true;
            this.newLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.newLabel.ForeColor = System.Drawing.Color.White;
            this.newLabel.Location = new System.Drawing.Point(22, 63);
            this.newLabel.Name = "newLabel";
            this.newLabel.Size = new System.Drawing.Size(65, 11);
            this.newLabel.TabIndex = 20;
            this.newLabel.Text = "新工程名：";
            // 
            // oldLabel
            // 
            this.oldLabel.AutoSize = true;
            this.oldLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.oldLabel.ForeColor = System.Drawing.Color.White;
            this.oldLabel.Location = new System.Drawing.Point(22, 28);
            this.oldLabel.Name = "oldLabel";
            this.oldLabel.Size = new System.Drawing.Size(65, 11);
            this.oldLabel.TabIndex = 20;
            this.oldLabel.Text = "原工程名：";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.oldLabel);
            this.panel1.Controls.Add(this.projectNameTextBox);
            this.panel1.Controls.Add(this.projectNameLabel);
            this.panel1.Controls.Add(this.enterButton);
            this.panel1.Controls.Add(this.newLabel);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 145);
            this.panel1.TabIndex = 22;
            // 
            // RenameOrCopyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(270, 180);
            this.Controls.Add(this.panel1);
            this.Name = "RenameOrCopyForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "工程重命名";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.RenameOrCopyForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label projectNameLabel;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton enterButton;
        private Sunny.UI.UITextBox projectNameTextBox;
        private System.Windows.Forms.Label newLabel;
        private System.Windows.Forms.Label oldLabel;
        private System.Windows.Forms.Panel panel1;
    }
}