
namespace LightController.MyForm.Project
{
    partial class CopySceneForm
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
            this.toButton = new Sunny.UI.UIButton();
            this.sceneComboBox = new Sunny.UI.UIComboBox();
            this.sceneLabel = new Sunny.UI.UILabel();
            this.fromButton = new Sunny.UI.UIButton();
            this.soundCheckBox = new Sunny.UI.UICheckBox();
            this.normalCheckBox = new Sunny.UI.UICheckBox();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.normalCheckBox);
            this.mainPanel.Controls.Add(this.soundCheckBox);
            this.mainPanel.Controls.Add(this.fromButton);
            this.mainPanel.Controls.Add(this.toButton);
            this.mainPanel.Controls.Add(this.sceneComboBox);
            this.mainPanel.Controls.Add(this.sceneLabel);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(320, 185);
            this.mainPanel.TabIndex = 0;
            // 
            // toButton
            // 
            this.toButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.toButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.toButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.toButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.toButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.toButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.toButton.Font = new System.Drawing.Font("黑体", 8.25F);
            this.toButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.toButton.Location = new System.Drawing.Point(38, 99);
            this.toButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.toButton.Name = "toButton";
            this.toButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.toButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.toButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.toButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.toButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.toButton.Size = new System.Drawing.Size(240, 20);
            this.toButton.Style = Sunny.UI.UIStyle.Custom;
            this.toButton.TabIndex = 8;
            this.toButton.Text = "从当前场景复制到指定场景";
            this.toButton.Click += new System.EventHandler(this.toButton_Click);
            // 
            // sceneComboBox
            // 
            this.sceneComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.sceneComboBox.DataSource = null;
            this.sceneComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.sceneComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.sceneComboBox.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.sceneComboBox.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sceneComboBox.ForeColor = System.Drawing.Color.White;
            this.sceneComboBox.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.sceneComboBox.Location = new System.Drawing.Point(95, 27);
            this.sceneComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sceneComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.sceneComboBox.Name = "sceneComboBox";
            this.sceneComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.sceneComboBox.Radius = 4;
            this.sceneComboBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.sceneComboBox.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.sceneComboBox.Size = new System.Drawing.Size(183, 20);
            this.sceneComboBox.Style = Sunny.UI.UIStyle.Custom;
            this.sceneComboBox.TabIndex = 9;
            this.sceneComboBox.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // sceneLabel
            // 
            this.sceneLabel.AutoSize = true;
            this.sceneLabel.Font = new System.Drawing.Font("黑体", 9F);
            this.sceneLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.sceneLabel.Location = new System.Drawing.Point(36, 31);
            this.sceneLabel.Name = "sceneLabel";
            this.sceneLabel.Size = new System.Drawing.Size(53, 12);
            this.sceneLabel.Style = Sunny.UI.UIStyle.Custom;
            this.sceneLabel.TabIndex = 10;
            this.sceneLabel.Text = "指定场景";
            this.sceneLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fromButton
            // 
            this.fromButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fromButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.fromButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.fromButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.fromButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.fromButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.fromButton.Font = new System.Drawing.Font("黑体", 8.25F);
            this.fromButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.fromButton.Location = new System.Drawing.Point(38, 138);
            this.fromButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.fromButton.Name = "fromButton";
            this.fromButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fromButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fromButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fromButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fromButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fromButton.Size = new System.Drawing.Size(240, 20);
            this.fromButton.Style = Sunny.UI.UIStyle.Custom;
            this.fromButton.TabIndex = 8;
            this.fromButton.Text = "从指定场景复制到当前场景";
            this.fromButton.Click += new System.EventHandler(this.fromButton_Click);
            // 
            // soundCheckBox
            // 
            this.soundCheckBox.CheckBoxColor = System.Drawing.Color.Snow;
            this.soundCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.soundCheckBox.Font = new System.Drawing.Font("黑体", 9F);
            this.soundCheckBox.ForeColor = System.Drawing.Color.White;
            this.soundCheckBox.Location = new System.Drawing.Point(166, 65);
            this.soundCheckBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.soundCheckBox.Name = "soundCheckBox";
            this.soundCheckBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.soundCheckBox.Size = new System.Drawing.Size(74, 18);
            this.soundCheckBox.Style = Sunny.UI.UIStyle.Custom;
            this.soundCheckBox.TabIndex = 13;
            this.soundCheckBox.Text = "音频模式";
            this.soundCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.soundCheckBox.CheckedChanged += new System.EventHandler(this.modeCheckBox_CheckedChanged);
            // 
            // normalCheckBox
            // 
            this.normalCheckBox.CheckBoxColor = System.Drawing.Color.Snow;
            this.normalCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.normalCheckBox.Font = new System.Drawing.Font("黑体", 9F);
            this.normalCheckBox.ForeColor = System.Drawing.Color.White;
            this.normalCheckBox.Location = new System.Drawing.Point(54, 65);
            this.normalCheckBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.normalCheckBox.Name = "normalCheckBox";
            this.normalCheckBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.normalCheckBox.Size = new System.Drawing.Size(74, 18);
            this.normalCheckBox.Style = Sunny.UI.UIStyle.Custom;
            this.normalCheckBox.TabIndex = 13;
            this.normalCheckBox.Text = "常规模式";
            this.normalCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.normalCheckBox.CheckedChanged += new System.EventHandler(this.modeCheckBox_CheckedChanged);
            // 
            // CopySceneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(320, 220);
            this.Controls.Add(this.mainPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CopySceneForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ShowRadius = false;
            this.ShowShadow = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "复用场景";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("黑体", 10F);
            this.Load += new System.EventHandler(this.CopySceneForm_Load);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private Sunny.UI.UIButton fromButton;
        private Sunny.UI.UIButton toButton;
        private Sunny.UI.UIComboBox sceneComboBox;
        private Sunny.UI.UILabel sceneLabel;
        private Sunny.UI.UICheckBox normalCheckBox;
        private Sunny.UI.UICheckBox soundCheckBox;
    }
}