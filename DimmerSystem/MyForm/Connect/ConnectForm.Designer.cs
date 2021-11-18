
namespace LightController.MyForm.Connect
{
    partial class ConnectForm
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
            this.deviceConnectButton = new Sunny.UI.UIButton();
            this.deviceRestartButton = new Sunny.UI.UIButton();
            this.deviceRefreshButton = new Sunny.UI.UIButton();
            this.deviceComboBox = new Sunny.UI.UIComboBox();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.myStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.deviceConnectButton);
            this.panel1.Controls.Add(this.deviceRestartButton);
            this.panel1.Controls.Add(this.deviceRefreshButton);
            this.panel1.Controls.Add(this.deviceComboBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 101);
            this.panel1.TabIndex = 0;
            // 
            // deviceConnectButton
            // 
            this.deviceConnectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deviceConnectButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.deviceConnectButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.deviceConnectButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.deviceConnectButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.deviceConnectButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.deviceConnectButton.Location = new System.Drawing.Point(130, 60);
            this.deviceConnectButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.deviceConnectButton.Name = "deviceConnectButton";
            this.deviceConnectButton.Radius = 10;
            this.deviceConnectButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.deviceConnectButton.Size = new System.Drawing.Size(60, 20);
            this.deviceConnectButton.Style = Sunny.UI.UIStyle.Custom;
            this.deviceConnectButton.StyleCustomMode = true;
            this.deviceConnectButton.TabIndex = 36;
            this.deviceConnectButton.Text = "连接灯具";
            this.deviceConnectButton.Click += new System.EventHandler(this.deviceConnectButton_Click);
            // 
            // deviceRestartButton
            // 
            this.deviceRestartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deviceRestartButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deviceRestartButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.deviceRestartButton.Location = new System.Drawing.Point(228, 19);
            this.deviceRestartButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.deviceRestartButton.Name = "deviceRestartButton";
            this.deviceRestartButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.deviceRestartButton.Size = new System.Drawing.Size(60, 61);
            this.deviceRestartButton.Style = Sunny.UI.UIStyle.Custom;
            this.deviceRestartButton.TabIndex = 35;
            this.deviceRestartButton.Text = "设备重启";
            this.deviceRestartButton.Click += new System.EventHandler(this.deviceRestartButton_Click);
            // 
            // deviceRefreshButton
            // 
            this.deviceRefreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deviceRefreshButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deviceRefreshButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.deviceRefreshButton.Location = new System.Drawing.Point(35, 60);
            this.deviceRefreshButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.deviceRefreshButton.Name = "deviceRefreshButton";
            this.deviceRefreshButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.deviceRefreshButton.Size = new System.Drawing.Size(60, 20);
            this.deviceRefreshButton.Style = Sunny.UI.UIStyle.Custom;
            this.deviceRefreshButton.TabIndex = 35;
            this.deviceRefreshButton.Text = "刷新列表";
            this.deviceRefreshButton.Click += new System.EventHandler(this.deviceRefreshButton_Click);
            // 
            // deviceComboBox
            // 
            this.deviceComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deviceComboBox.DataSource = null;
            this.deviceComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.deviceComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deviceComboBox.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.deviceComboBox.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.deviceComboBox.ForeColor = System.Drawing.Color.White;
            this.deviceComboBox.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.deviceComboBox.Location = new System.Drawing.Point(21, 19);
            this.deviceComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deviceComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.deviceComboBox.Name = "deviceComboBox";
            this.deviceComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.deviceComboBox.Radius = 4;
            this.deviceComboBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.deviceComboBox.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.deviceComboBox.Size = new System.Drawing.Size(183, 20);
            this.deviceComboBox.Style = Sunny.UI.UIStyle.Custom;
            this.deviceComboBox.TabIndex = 34;
            this.deviceComboBox.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // myStatusStrip
            // 
            this.myStatusStrip.AutoSize = false;
            this.myStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
            this.myStatusStrip.Location = new System.Drawing.Point(0, 136);
            this.myStatusStrip.Name = "myStatusStrip";
            this.myStatusStrip.ShowItemToolTips = true;
            this.myStatusStrip.Size = new System.Drawing.Size(220, 24);
            this.myStatusStrip.SizingGrip = false;
            this.myStatusStrip.TabIndex = 37;
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myStatusLabel.ForeColor = System.Drawing.Color.White;
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(0, 19);
            // 
            // ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(220, 160);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.myStatusStrip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConnectForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "设备连接";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 8F);
            this.Load += new System.EventHandler(this.ConnectForm_Load);
            this.Shown += new System.EventHandler(this.ConnectForm_Shown);
            this.panel1.ResumeLayout(false);
            this.myStatusStrip.ResumeLayout(false);
            this.myStatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UIButton deviceConnectButton;
        private Sunny.UI.UIButton deviceRefreshButton;
        private Sunny.UI.UIComboBox deviceComboBox;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private Sunny.UI.UIButton deviceRestartButton;
    }
}