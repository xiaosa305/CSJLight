
namespace LightController.MyForm.Project
{
    partial class GroupForm
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
            this.lightsListView = new System.Windows.Forms.ListView();
            this.lightNo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.copyAllCheckBox = new Sunny.UI.UICheckBox();
            this.enterButton = new Sunny.UI.UIButton();
            this.cancelButton = new Sunny.UI.UIButton();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.linePanel = new System.Windows.Forms.Panel();
            this.myStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lightsListView
            // 
            this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lightNo,
            this.lightType,
            this.lightAddr});
            this.lightsListView.Dock = System.Windows.Forms.DockStyle.Top;
            this.lightsListView.Font = new System.Drawing.Font("黑体", 8F);
            this.lightsListView.FullRowSelect = true;
            this.lightsListView.GridLines = true;
            this.lightsListView.HideSelection = false;
            this.lightsListView.Location = new System.Drawing.Point(0, 35);
            this.lightsListView.Name = "lightsListView";
            this.lightsListView.Size = new System.Drawing.Size(280, 253);
            this.lightsListView.TabIndex = 17;
            this.lightsListView.UseCompatibleStateImageBehavior = false;
            this.lightsListView.View = System.Windows.Forms.View.Details;
            this.lightsListView.SelectedIndexChanged += new System.EventHandler(this.lightsListView_SelectedIndexChanged);
            // 
            // lightNo
            // 
            this.lightNo.Text = "序号";
            this.lightNo.Width = 55;
            // 
            // lightType
            // 
            this.lightType.Text = "型号";
            this.lightType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightType.Width = 128;
            // 
            // lightAddr
            // 
            this.lightAddr.Text = "通道地址";
            this.lightAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightAddr.Width = 87;
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.White;
            this.nameTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nameTextBox.Font = new System.Drawing.Font("黑体", 8F);
            this.nameTextBox.Location = new System.Drawing.Point(95, 334);
            this.nameTextBox.MaxLength = 20;
            this.nameTextBox.Multiline = true;
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(143, 20);
            this.nameTextBox.TabIndex = 19;
            this.nameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("黑体", 8F);
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Location = new System.Drawing.Point(48, 339);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(41, 11);
            this.nameLabel.TabIndex = 18;
            this.nameLabel.Text = "编组名";
            // 
            // copyAllCheckBox
            // 
            this.copyAllCheckBox.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.copyAllCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.copyAllCheckBox.Font = new System.Drawing.Font("黑体", 8F);
            this.copyAllCheckBox.ForeColor = System.Drawing.Color.White;
            this.copyAllCheckBox.Location = new System.Drawing.Point(47, 304);
            this.copyAllCheckBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.copyAllCheckBox.Name = "copyAllCheckBox";
            this.copyAllCheckBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.copyAllCheckBox.Size = new System.Drawing.Size(147, 18);
            this.copyAllCheckBox.Style = Sunny.UI.UIStyle.Custom;
            this.copyAllCheckBox.TabIndex = 20;
            this.copyAllCheckBox.Text = "统一设为组长数据";
            this.copyAllCheckBox.CheckedChanged += new System.EventHandler(this.copyAllCheckBox_CheckedChanged);
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("黑体", 8F);
            this.enterButton.Location = new System.Drawing.Point(50, 370);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(70, 25);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 21;
            this.enterButton.Text = "进入编组";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("黑体", 8F);
            this.cancelButton.Location = new System.Drawing.Point(168, 370);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(70, 25);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 22;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // myStatusStrip
            // 
            this.myStatusStrip.AutoSize = false;
            this.myStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
            this.myStatusStrip.Location = new System.Drawing.Point(0, 416);
            this.myStatusStrip.Name = "myStatusStrip";
            this.myStatusStrip.ShowItemToolTips = true;
            this.myStatusStrip.Size = new System.Drawing.Size(280, 24);
            this.myStatusStrip.SizingGrip = false;
            this.myStatusStrip.TabIndex = 38;
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myStatusLabel.ForeColor = System.Drawing.Color.White;
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(0, 19);
            // 
            // linePanel
            // 
            this.linePanel.BackColor = System.Drawing.Color.Gray;
            this.linePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.linePanel.Location = new System.Drawing.Point(0, 415);
            this.linePanel.Name = "linePanel";
            this.linePanel.Size = new System.Drawing.Size(280, 1);
            this.linePanel.TabIndex = 39;
            // 
            // GroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(280, 440);
            this.Controls.Add(this.linePanel);
            this.Controls.Add(this.myStatusStrip);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.copyAllCheckBox);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.lightsListView);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "灯具编组";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("黑体", 10F);
            this.Load += new System.EventHandler(this.GroupForm_Load);
            this.myStatusStrip.ResumeLayout(false);
            this.myStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lightsListView;
        private System.Windows.Forms.ColumnHeader lightNo;
        private System.Windows.Forms.ColumnHeader lightType;
        private System.Windows.Forms.ColumnHeader lightAddr;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label nameLabel;
        private Sunny.UI.UICheckBox copyAllCheckBox;
        private Sunny.UI.UIButton enterButton;
        private Sunny.UI.UIButton cancelButton;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private System.Windows.Forms.Panel linePanel;
    }
}