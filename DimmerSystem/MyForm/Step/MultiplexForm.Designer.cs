
namespace LightController.MyForm.Step
{
    partial class MultiplexForm
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
            this.endNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.allStepButton = new Sunny.UI.UIButton();
            this.startNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.stepLabel = new System.Windows.Forms.Label();
            this.timesLabel = new System.Windows.Forms.Label();
            this.timesNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.enterButton = new Sunny.UI.UIButton();
            this.cancelButton = new Sunny.UI.UIButton();
            this.noticeLabel = new System.Windows.Forms.Label();
            this.allCheckBox = new System.Windows.Forms.CheckBox();
            this.lightsListView = new System.Windows.Forms.ListView();
            this.checkBox = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lightType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lightAddr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timesNumericUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.lightsListView);
            this.mainPanel.Controls.Add(this.allCheckBox);
            this.mainPanel.Controls.Add(this.noticeLabel);
            this.mainPanel.Controls.Add(this.endNumericUpDown);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.cancelButton);
            this.mainPanel.Controls.Add(this.enterButton);
            this.mainPanel.Controls.Add(this.allStepButton);
            this.mainPanel.Controls.Add(this.timesNumericUpDown);
            this.mainPanel.Controls.Add(this.startNumericUpDown);
            this.mainPanel.Controls.Add(this.timesLabel);
            this.mainPanel.Controls.Add(this.stepLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(500, 385);
            this.mainPanel.TabIndex = 0;
            // 
            // endNumericUpDown
            // 
            this.endNumericUpDown.BackColor = System.Drawing.SystemColors.Window;
            this.endNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.endNumericUpDown.ForeColor = System.Drawing.SystemColors.MenuText;
            this.endNumericUpDown.Location = new System.Drawing.Point(423, 198);
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
            this.endNumericUpDown.TabIndex = 52;
            this.endNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.endNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.endNumericUpDown.ValueChanged += new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(387, 203);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(11, 11);
            this.label1.TabIndex = 51;
            this.label1.Text = "-";
            // 
            // allStepButton
            // 
            this.allStepButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.allStepButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.allStepButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.allStepButton.Location = new System.Drawing.Point(425, 161);
            this.allStepButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.allStepButton.Name = "allStepButton";
            this.allStepButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.allStepButton.Size = new System.Drawing.Size(54, 20);
            this.allStepButton.Style = Sunny.UI.UIStyle.Custom;
            this.allStepButton.TabIndex = 50;
            this.allStepButton.Text = "全选";
            this.allStepButton.Click += new System.EventHandler(this.allStepButton_Click);
            // 
            // startNumericUpDown
            // 
            this.startNumericUpDown.BackColor = System.Drawing.SystemColors.Window;
            this.startNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.startNumericUpDown.ForeColor = System.Drawing.SystemColors.MenuText;
            this.startNumericUpDown.Location = new System.Drawing.Point(307, 198);
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
            this.startNumericUpDown.TabIndex = 49;
            this.startNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startNumericUpDown.ValueChanged += new System.EventHandler(this.startEndNumericUpDown_ValueChanged);
            // 
            // stepLabel
            // 
            this.stepLabel.AutoSize = true;
            this.stepLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.stepLabel.ForeColor = System.Drawing.Color.White;
            this.stepLabel.Location = new System.Drawing.Point(304, 166);
            this.stepLabel.Name = "stepLabel";
            this.stepLabel.Size = new System.Drawing.Size(113, 11);
            this.stepLabel.TabIndex = 48;
            this.stepLabel.Text = "请选择被复用的步数";
            // 
            // timesLabel
            // 
            this.timesLabel.AutoSize = true;
            this.timesLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.timesLabel.ForeColor = System.Drawing.Color.White;
            this.timesLabel.Location = new System.Drawing.Point(306, 249);
            this.timesLabel.Name = "timesLabel";
            this.timesLabel.Size = new System.Drawing.Size(101, 11);
            this.timesLabel.TabIndex = 48;
            this.timesLabel.Text = "请设置复用的次数";
            // 
            // timesNumericUpDown
            // 
            this.timesNumericUpDown.BackColor = System.Drawing.SystemColors.Window;
            this.timesNumericUpDown.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.timesNumericUpDown.ForeColor = System.Drawing.SystemColors.MenuText;
            this.timesNumericUpDown.Location = new System.Drawing.Point(423, 245);
            this.timesNumericUpDown.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.timesNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timesNumericUpDown.Name = "timesNumericUpDown";
            this.timesNumericUpDown.Size = new System.Drawing.Size(56, 20);
            this.timesNumericUpDown.TabIndex = 49;
            this.timesNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timesNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timesNumericUpDown.ValueChanged += new System.EventHandler(this.timesNumericUpDown_ValueChanged);
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.enterButton.Location = new System.Drawing.Point(308, 328);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(54, 20);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 50;
            this.enterButton.Text = "复用";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(422, 328);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 50;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // noticeLabel
            // 
            this.noticeLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.noticeLabel.ForeColor = System.Drawing.Color.White;
            this.noticeLabel.Location = new System.Drawing.Point(304, 56);
            this.noticeLabel.Name = "noticeLabel";
            this.noticeLabel.Size = new System.Drawing.Size(175, 48);
            this.noticeLabel.TabIndex = 53;
            this.noticeLabel.Text = "提示：未被选中的灯具仍会添加相应步数，但并非复用步数，而是采用该灯具的最后一步进行填充。";
            // 
            // allCheckBox
            // 
            this.allCheckBox.AutoSize = true;
            this.allCheckBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.allCheckBox.ForeColor = System.Drawing.Color.White;
            this.allCheckBox.Location = new System.Drawing.Point(306, 28);
            this.allCheckBox.Name = "allCheckBox";
            this.allCheckBox.Size = new System.Drawing.Size(72, 15);
            this.allCheckBox.TabIndex = 54;
            this.allCheckBox.Text = "灯具全选";
            this.allCheckBox.UseVisualStyleBackColor = true;
            this.allCheckBox.CheckedChanged += new System.EventHandler(this.allCheckBox_CheckedChanged);
            // 
            // lightsListView
            // 
            this.lightsListView.CheckBoxes = true;
            this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.checkBox,
            this.lightType,
            this.lightAddr});
            this.lightsListView.Dock = System.Windows.Forms.DockStyle.Left;
            this.lightsListView.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lightsListView.FullRowSelect = true;
            this.lightsListView.GridLines = true;
            this.lightsListView.HideSelection = false;
            this.lightsListView.Location = new System.Drawing.Point(0, 0);
            this.lightsListView.MultiSelect = false;
            this.lightsListView.Name = "lightsListView";
            this.lightsListView.Size = new System.Drawing.Size(281, 383);
            this.lightsListView.TabIndex = 55;
            this.lightsListView.UseCompatibleStateImageBehavior = false;
            this.lightsListView.View = System.Windows.Forms.View.Details;
            // 
            // checkBox
            // 
            this.checkBox.Text = "";
            this.checkBox.Width = 30;
            // 
            // lightType
            // 
            this.lightType.Text = "型号";
            this.lightType.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightType.Width = 150;
            // 
            // lightAddr
            // 
            this.lightAddr.Text = "通道地址";
            this.lightAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lightAddr.Width = 72;
            // 
            // MultiplexForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(500, 420);
            this.Controls.Add(this.mainPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MultiplexForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ShowRadius = false;
            this.ShowShadow = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "多步复用";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.MultiplexForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.endNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.startNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timesNumericUpDown)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.NumericUpDown endNumericUpDown;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton enterButton;
        private Sunny.UI.UIButton allStepButton;
        private System.Windows.Forms.NumericUpDown timesNumericUpDown;
        private System.Windows.Forms.NumericUpDown startNumericUpDown;
        private System.Windows.Forms.Label timesLabel;
        private System.Windows.Forms.Label stepLabel;
        private System.Windows.Forms.Label noticeLabel;
        private System.Windows.Forms.CheckBox allCheckBox;
        private System.Windows.Forms.ListView lightsListView;
        private System.Windows.Forms.ColumnHeader checkBox;
        private System.Windows.Forms.ColumnHeader lightType;
        private System.Windows.Forms.ColumnHeader lightAddr;
    }
}