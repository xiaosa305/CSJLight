
namespace LightController.MyForm.Step
{
    partial class DetailSingleForm
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
            this.saComboBox = new Sunny.UI.UIComboBox();
            this.unifyComboBox = new Sunny.UI.UIComboBox();
            this.unifyBottomButton = new Sunny.UI.UIButton();
            this.unifyTopButton = new Sunny.UI.UIButton();
            this.unifyValueButton = new Sunny.UI.UIButton();
            this.stepPanelDemo = new System.Windows.Forms.Panel();
            this.topBottomButtonDemo = new Sunny.UI.UIButton();
            this.stepLabelDemo = new System.Windows.Forms.Label();
            this.stepNUDDemo = new System.Windows.Forms.NumericUpDown();
            this.unifyNUD = new System.Windows.Forms.NumericUpDown();
            this.stepFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainPanel.SuspendLayout();
            this.stepPanelDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unifyNUD)).BeginInit();
            this.myStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.saComboBox);
            this.mainPanel.Controls.Add(this.unifyComboBox);
            this.mainPanel.Controls.Add(this.unifyBottomButton);
            this.mainPanel.Controls.Add(this.unifyTopButton);
            this.mainPanel.Controls.Add(this.unifyValueButton);
            this.mainPanel.Controls.Add(this.stepPanelDemo);
            this.mainPanel.Controls.Add(this.unifyNUD);
            this.mainPanel.Controls.Add(this.stepFLP);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1000, 534);
            this.mainPanel.TabIndex = 0;
            // 
            // saComboBox
            // 
            this.saComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.saComboBox.DataSource = null;
            this.saComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.saComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.saComboBox.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.saComboBox.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.saComboBox.ForeColor = System.Drawing.Color.White;
            this.saComboBox.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.saComboBox.Location = new System.Drawing.Point(29, 81);
            this.saComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.saComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.saComboBox.Name = "saComboBox";
            this.saComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.saComboBox.Radius = 4;
            this.saComboBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.saComboBox.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.saComboBox.Size = new System.Drawing.Size(125, 20);
            this.saComboBox.Style = Sunny.UI.UIStyle.Custom;
            this.saComboBox.TabIndex = 7;
            this.saComboBox.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            this.saComboBox.Visible = false;
            // 
            // unifyComboBox
            // 
            this.unifyComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.unifyComboBox.DataSource = null;
            this.unifyComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.unifyComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.unifyComboBox.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.unifyComboBox.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.unifyComboBox.ForeColor = System.Drawing.Color.White;
            this.unifyComboBox.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.unifyComboBox.Items.AddRange(new object[] {
            "全步",
            "奇数步",
            "偶数步"});
            this.unifyComboBox.Location = new System.Drawing.Point(29, 19);
            this.unifyComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.unifyComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.unifyComboBox.Name = "unifyComboBox";
            this.unifyComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.unifyComboBox.Radius = 4;
            this.unifyComboBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyComboBox.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.unifyComboBox.Size = new System.Drawing.Size(63, 20);
            this.unifyComboBox.Style = Sunny.UI.UIStyle.Custom;
            this.unifyComboBox.TabIndex = 7;
            this.unifyComboBox.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // unifyBottomButton
            // 
            this.unifyBottomButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.unifyBottomButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.unifyBottomButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.unifyBottomButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.unifyBottomButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.unifyBottomButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.unifyBottomButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.unifyBottomButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.unifyBottomButton.Location = new System.Drawing.Point(124, 19);
            this.unifyBottomButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.unifyBottomButton.Name = "unifyBottomButton";
            this.unifyBottomButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyBottomButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyBottomButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyBottomButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyBottomButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyBottomButton.Size = new System.Drawing.Size(30, 20);
            this.unifyBottomButton.Style = Sunny.UI.UIStyle.Custom;
            this.unifyBottomButton.TabIndex = 6;
            this.unifyBottomButton.Text = "↓";
            this.unifyBottomButton.Click += new System.EventHandler(this.unifyValueButton_Click);
            // 
            // unifyTopButton
            // 
            this.unifyTopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.unifyTopButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.unifyTopButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.unifyTopButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.unifyTopButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.unifyTopButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.unifyTopButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.unifyTopButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.unifyTopButton.Location = new System.Drawing.Point(93, 19);
            this.unifyTopButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.unifyTopButton.Name = "unifyTopButton";
            this.unifyTopButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyTopButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyTopButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyTopButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyTopButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyTopButton.Size = new System.Drawing.Size(30, 20);
            this.unifyTopButton.Style = Sunny.UI.UIStyle.Custom;
            this.unifyTopButton.TabIndex = 6;
            this.unifyTopButton.Text = "↑";
            this.unifyTopButton.Click += new System.EventHandler(this.unifyValueButton_Click);
            // 
            // unifyValueButton
            // 
            this.unifyValueButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.unifyValueButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.unifyValueButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.unifyValueButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.unifyValueButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.unifyValueButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.unifyValueButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.unifyValueButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.unifyValueButton.Location = new System.Drawing.Point(93, 50);
            this.unifyValueButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.unifyValueButton.Name = "unifyValueButton";
            this.unifyValueButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyValueButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyValueButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyValueButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyValueButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.unifyValueButton.Size = new System.Drawing.Size(61, 20);
            this.unifyValueButton.Style = Sunny.UI.UIStyle.Custom;
            this.unifyValueButton.TabIndex = 6;
            this.unifyValueButton.Text = "设值";
            this.unifyValueButton.Click += new System.EventHandler(this.unifyValueButton_Click);
            // 
            // stepPanelDemo
            // 
            this.stepPanelDemo.BackColor = System.Drawing.Color.Transparent;
            this.stepPanelDemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stepPanelDemo.Controls.Add(this.topBottomButtonDemo);
            this.stepPanelDemo.Controls.Add(this.stepLabelDemo);
            this.stepPanelDemo.Controls.Add(this.stepNUDDemo);
            this.stepPanelDemo.Location = new System.Drawing.Point(29, 253);
            this.stepPanelDemo.Margin = new System.Windows.Forms.Padding(1, 1, 0, 0);
            this.stepPanelDemo.Name = "stepPanelDemo";
            this.stepPanelDemo.Size = new System.Drawing.Size(40, 85);
            this.stepPanelDemo.TabIndex = 2;
            this.stepPanelDemo.Visible = false;
            // 
            // topBottomButtonDemo
            // 
            this.topBottomButtonDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.topBottomButtonDemo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.topBottomButtonDemo.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.topBottomButtonDemo.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.topBottomButtonDemo.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.topBottomButtonDemo.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.topBottomButtonDemo.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.topBottomButtonDemo.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.topBottomButtonDemo.Location = new System.Drawing.Point(5, 27);
            this.topBottomButtonDemo.MinimumSize = new System.Drawing.Size(1, 2);
            this.topBottomButtonDemo.Name = "topBottomButtonDemo";
            this.topBottomButtonDemo.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.topBottomButtonDemo.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.topBottomButtonDemo.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.topBottomButtonDemo.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.topBottomButtonDemo.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.topBottomButtonDemo.Size = new System.Drawing.Size(30, 20);
            this.topBottomButtonDemo.Style = Sunny.UI.UIStyle.Custom;
            this.topBottomButtonDemo.TabIndex = 6;
            this.topBottomButtonDemo.Text = "↑↓";
            // 
            // stepLabelDemo
            // 
            this.stepLabelDemo.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.stepLabelDemo.ForeColor = System.Drawing.Color.White;
            this.stepLabelDemo.Location = new System.Drawing.Point(5, 8);
            this.stepLabelDemo.Name = "stepLabelDemo";
            this.stepLabelDemo.Size = new System.Drawing.Size(33, 16);
            this.stepLabelDemo.TabIndex = 2;
            this.stepLabelDemo.Text = "1000";
            // 
            // stepNUDDemo
            // 
            this.stepNUDDemo.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.stepNUDDemo.Location = new System.Drawing.Point(1, 55);
            this.stepNUDDemo.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.stepNUDDemo.Name = "stepNUDDemo";
            this.stepNUDDemo.Size = new System.Drawing.Size(38, 20);
            this.stepNUDDemo.TabIndex = 1;
            this.stepNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.stepNUDDemo.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.stepNUDDemo.ValueChanged += new System.EventHandler(this.StepNUD_ValueChanged);
            // 
            // unifyNUD
            // 
            this.unifyNUD.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.unifyNUD.Location = new System.Drawing.Point(29, 50);
            this.unifyNUD.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.unifyNUD.Name = "unifyNUD";
            this.unifyNUD.Size = new System.Drawing.Size(54, 20);
            this.unifyNUD.TabIndex = 1;
            this.unifyNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.unifyNUD.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // stepFLP
            // 
            this.stepFLP.AutoScroll = true;
            this.stepFLP.Dock = System.Windows.Forms.DockStyle.Right;
            this.stepFLP.Location = new System.Drawing.Point(178, 0);
            this.stepFLP.Margin = new System.Windows.Forms.Padding(0);
            this.stepFLP.Name = "stepFLP";
            this.stepFLP.Size = new System.Drawing.Size(820, 532);
            this.stepFLP.TabIndex = 0;
            // 
            // myStatusStrip
            // 
            this.myStatusStrip.AutoSize = false;
            this.myStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
            this.myStatusStrip.Location = new System.Drawing.Point(0, 569);
            this.myStatusStrip.Name = "myStatusStrip";
            this.myStatusStrip.ShowItemToolTips = true;
            this.myStatusStrip.Size = new System.Drawing.Size(1000, 31);
            this.myStatusStrip.SizingGrip = false;
            this.myStatusStrip.TabIndex = 6;
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myStatusLabel.ForeColor = System.Drawing.Color.White;
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(0, 26);
            // 
            // DetailSingleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.myStatusStrip);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailSingleForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ShowRadius = false;
            this.ShowShadow = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "单通道多步联调";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.DetailSingleForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.stepPanelDemo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.stepNUDDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unifyNUD)).EndInit();
            this.myStatusStrip.ResumeLayout(false);
            this.myStatusStrip.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private System.Windows.Forms.FlowLayoutPanel stepFLP;
        private System.Windows.Forms.Panel stepPanelDemo;
        private System.Windows.Forms.Label stepLabelDemo;
        private System.Windows.Forms.NumericUpDown stepNUDDemo;
        private Sunny.UI.UIButton topBottomButtonDemo;
        private Sunny.UI.UIButton unifyValueButton;
        private System.Windows.Forms.NumericUpDown unifyNUD;
        private Sunny.UI.UIButton unifyBottomButton;
        private Sunny.UI.UIButton unifyTopButton;
        private Sunny.UI.UIComboBox unifyComboBox;
        private Sunny.UI.UIComboBox saComboBox;
    }
}