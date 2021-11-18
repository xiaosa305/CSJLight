
namespace LightController.MyForm.Project
{
    partial class DownloadForm
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
            this.processLabel = new Sunny.UI.UILabel();
            this.myProcessBar = new Sunny.UI.UIProcessBar();
            this.enterButton = new Sunny.UI.UIButton();
            this.pathLabel = new System.Windows.Forms.Label();
            this.downloadButton = new Sunny.UI.UIButton();
            this.exportedCheckBox = new Sunny.UI.UICheckBox();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.linePanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.myStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.processLabel);
            this.panel1.Controls.Add(this.myProcessBar);
            this.panel1.Controls.Add(this.enterButton);
            this.panel1.Controls.Add(this.pathLabel);
            this.panel1.Controls.Add(this.downloadButton);
            this.panel1.Controls.Add(this.exportedCheckBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(380, 140);
            this.panel1.TabIndex = 0;
            // 
            // processLabel
            // 
            this.processLabel.AutoSize = true;
            this.processLabel.Font = new System.Drawing.Font("黑体", 8F);
            this.processLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(183)))), ((int)(((byte)(183)))), ((int)(((byte)(183)))));
            this.processLabel.Location = new System.Drawing.Point(29, 62);
            this.processLabel.Name = "processLabel";
            this.processLabel.Size = new System.Drawing.Size(65, 11);
            this.processLabel.Style = Sunny.UI.UIStyle.Custom;
            this.processLabel.TabIndex = 27;
            this.processLabel.Text = "下载进度：";
            this.processLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myProcessBar
            // 
            this.myProcessBar.BackColor = System.Drawing.Color.Transparent;
            this.myProcessBar.DecimalCount = 0;
            this.myProcessBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myProcessBar.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myProcessBar.ForeColor = System.Drawing.Color.White;
            this.myProcessBar.Location = new System.Drawing.Point(114, 58);
            this.myProcessBar.MinimumSize = new System.Drawing.Size(70, 5);
            this.myProcessBar.Name = "myProcessBar";
            this.myProcessBar.Radius = 10;
            this.myProcessBar.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.myProcessBar.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.myProcessBar.Size = new System.Drawing.Size(244, 18);
            this.myProcessBar.Style = Sunny.UI.UIStyle.Custom;
            this.myProcessBar.TabIndex = 26;
            this.myProcessBar.Text = "40%";
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("黑体", 8F);
            this.enterButton.Location = new System.Drawing.Point(23, 97);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(82, 25);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 24;
            this.enterButton.Text = "选择已有工程";
            this.enterButton.Click += new System.EventHandler(this.dirChooseButton_Click);
            // 
            // pathLabel
            // 
            this.pathLabel.Font = new System.Drawing.Font("黑体", 8F);
            this.pathLabel.ForeColor = System.Drawing.Color.White;
            this.pathLabel.Location = new System.Drawing.Point(112, 97);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(246, 25);
            this.pathLabel.TabIndex = 23;
            this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.pathLabel.TextChanged += new System.EventHandler(this.pathLabel_TextChanged);
            // 
            // downloadButton
            // 
            this.downloadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.downloadButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.downloadButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.downloadButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.downloadButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.downloadButton.Font = new System.Drawing.Font("黑体", 10F);
            this.downloadButton.Location = new System.Drawing.Point(278, 14);
            this.downloadButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Radius = 10;
            this.downloadButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.downloadButton.Size = new System.Drawing.Size(80, 34);
            this.downloadButton.Style = Sunny.UI.UIStyle.Custom;
            this.downloadButton.TabIndex = 22;
            this.downloadButton.Text = "下载工程";
            this.downloadButton.Click += new System.EventHandler(this.downloadButton_Click);
            // 
            // exportedCheckBox
            // 
            this.exportedCheckBox.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.exportedCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.exportedCheckBox.Font = new System.Drawing.Font("黑体", 8F);
            this.exportedCheckBox.ForeColor = System.Drawing.Color.White;
            this.exportedCheckBox.Location = new System.Drawing.Point(23, 19);
            this.exportedCheckBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.exportedCheckBox.Name = "exportedCheckBox";
            this.exportedCheckBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.exportedCheckBox.Size = new System.Drawing.Size(99, 18);
            this.exportedCheckBox.Style = Sunny.UI.UIStyle.Custom;
            this.exportedCheckBox.TabIndex = 21;
            this.exportedCheckBox.Text = "下载已有工程";
            this.exportedCheckBox.CheckedChanged += new System.EventHandler(this.exportedCheckBox_CheckedChanged);
            // 
            // myStatusStrip
            // 
            this.myStatusStrip.AutoSize = false;
            this.myStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
            this.myStatusStrip.Location = new System.Drawing.Point(0, 176);
            this.myStatusStrip.Name = "myStatusStrip";
            this.myStatusStrip.ShowItemToolTips = true;
            this.myStatusStrip.Size = new System.Drawing.Size(380, 24);
            this.myStatusStrip.SizingGrip = false;
            this.myStatusStrip.TabIndex = 39;
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myStatusLabel.ForeColor = System.Drawing.Color.White;
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(0, 19);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.ShowNewFolderButton = false;
            // 
            // linePanel
            // 
            this.linePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.linePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linePanel.Location = new System.Drawing.Point(0, 175);
            this.linePanel.Name = "linePanel";
            this.linePanel.Size = new System.Drawing.Size(380, 1);
            this.linePanel.TabIndex = 40;
            // 
            // DownloadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(380, 200);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.linePanel);
            this.Controls.Add(this.myStatusStrip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "工程下载";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("黑体", 10F);
            this.Load += new System.EventHandler(this.DownloadForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.myStatusStrip.ResumeLayout(false);
            this.myStatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UICheckBox exportedCheckBox;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private Sunny.UI.UIButton downloadButton;
        private Sunny.UI.UIButton enterButton;
        private System.Windows.Forms.Label pathLabel;
        private Sunny.UI.UILabel processLabel;
        private Sunny.UI.UIProcessBar myProcessBar;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Panel linePanel;
    }
}