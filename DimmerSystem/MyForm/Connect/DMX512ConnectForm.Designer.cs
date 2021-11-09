
namespace LightController.MyForm.Connect
{
    partial class DMX512ConnectForm
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
            this.portConnectButton = new Sunny.UI.UIButton();
            this.portRefreshButton = new Sunny.UI.UIButton();
            this.portComboBox = new Sunny.UI.UIComboBox();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            this.myStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.portConnectButton);
            this.panel1.Controls.Add(this.portRefreshButton);
            this.panel1.Controls.Add(this.portComboBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(220, 101);
            this.panel1.TabIndex = 0;
            // 
            // portConnectButton
            // 
            this.portConnectButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.portConnectButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.portConnectButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.portConnectButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.portConnectButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.portConnectButton.Font = new System.Drawing.Font("黑体", 8.25F);
            this.portConnectButton.Location = new System.Drawing.Point(129, 61);
            this.portConnectButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.portConnectButton.Name = "portConnectButton";
            this.portConnectButton.Radius = 10;
            this.portConnectButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.portConnectButton.Size = new System.Drawing.Size(60, 20);
            this.portConnectButton.Style = Sunny.UI.UIStyle.Custom;
            this.portConnectButton.StyleCustomMode = true;
            this.portConnectButton.TabIndex = 33;
            this.portConnectButton.Text = "设备连接";
            this.portConnectButton.Click += new System.EventHandler(this.portConnectButton_Click);
            // 
            // portRefreshButton
            // 
            this.portRefreshButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.portRefreshButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.portRefreshButton.Font = new System.Drawing.Font("黑体", 8F);
            this.portRefreshButton.Location = new System.Drawing.Point(34, 61);
            this.portRefreshButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.portRefreshButton.Name = "portRefreshButton";
            this.portRefreshButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.portRefreshButton.Size = new System.Drawing.Size(60, 20);
            this.portRefreshButton.Style = Sunny.UI.UIStyle.Custom;
            this.portRefreshButton.TabIndex = 32;
            this.portRefreshButton.Text = "刷新串口";
            this.portRefreshButton.Click += new System.EventHandler(this.portRefreshButton_Click);
            // 
            // portComboBox
            // 
            this.portComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.portComboBox.DataSource = null;
            this.portComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.portComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.portComboBox.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.portComboBox.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.portComboBox.ForeColor = System.Drawing.Color.White;
            this.portComboBox.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.portComboBox.Location = new System.Drawing.Point(20, 20);
            this.portComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.portComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.portComboBox.Name = "portComboBox";
            this.portComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.portComboBox.Radius = 4;
            this.portComboBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.portComboBox.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.portComboBox.Size = new System.Drawing.Size(183, 20);
            this.portComboBox.Style = Sunny.UI.UIStyle.Custom;
            this.portComboBox.TabIndex = 6;
            this.portComboBox.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
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
            this.myStatusStrip.TabIndex = 33;
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myStatusLabel.ForeColor = System.Drawing.Color.White;
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(0, 19);
            // 
            // DMX512ConnectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(220, 160);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.myStatusStrip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DMX512ConnectForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "《DMX512调试线》直连灯具";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("黑体", 10F);
            this.Load += new System.EventHandler(this.DMX512ConnectForm_Load);
            this.panel1.ResumeLayout(false);
            this.myStatusStrip.ResumeLayout(false);
            this.myStatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UIComboBox portComboBox;
        private Sunny.UI.UIButton portRefreshButton;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private Sunny.UI.UIButton portConnectButton;
    }
}