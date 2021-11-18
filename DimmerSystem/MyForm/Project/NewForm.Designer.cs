
namespace LightController.MyForm.Project
{
    partial class NewForm
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
            this.projectNameTextBox = new Sunny.UI.UITextBox();
            this.sceneComboBox = new Sunny.UI.UIComboBox();
            this.cancelButton = new Sunny.UI.UIButton();
            this.enterButton = new Sunny.UI.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.projectNameTextBox);
            this.panel1.Controls.Add(this.sceneComboBox);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.enterButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 145);
            this.panel1.TabIndex = 0;
            // 
            // projectNameTextBox
            // 
            this.projectNameTextBox.ButtonSymbol = 61761;
            this.projectNameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.projectNameTextBox.FillColor = System.Drawing.Color.White;
            this.projectNameTextBox.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.projectNameTextBox.Location = new System.Drawing.Point(94, 23);
            this.projectNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.projectNameTextBox.Maximum = 2147483647D;
            this.projectNameTextBox.Minimum = -2147483648D;
            this.projectNameTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.projectNameTextBox.Name = "projectNameTextBox";
            this.projectNameTextBox.Padding = new System.Windows.Forms.Padding(5);
            this.projectNameTextBox.Size = new System.Drawing.Size(157, 20);
            this.projectNameTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.projectNameTextBox.TabIndex = 11;
            this.projectNameTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // sceneComboBox
            // 
            this.sceneComboBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.sceneComboBox.DataSource = null;
            this.sceneComboBox.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.sceneComboBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.sceneComboBox.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.sceneComboBox.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sceneComboBox.ForeColor = System.Drawing.Color.White;
            this.sceneComboBox.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.sceneComboBox.Location = new System.Drawing.Point(94, 58);
            this.sceneComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sceneComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.sceneComboBox.Name = "sceneComboBox";
            this.sceneComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.sceneComboBox.Radius = 4;
            this.sceneComboBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.sceneComboBox.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.sceneComboBox.Size = new System.Drawing.Size(157, 20);
            this.sceneComboBox.Style = Sunny.UI.UIStyle.Custom;
            this.sceneComboBox.TabIndex = 10;
            this.sceneComboBox.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(164, 100);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 8;
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
            this.enterButton.TabIndex = 9;
            this.enterButton.Text = "确定";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(22, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 11);
            this.label1.TabIndex = 7;
            this.label1.Text = "工程名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 11);
            this.label2.TabIndex = 7;
            this.label2.Text = "初始场景：";
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(270, 180);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.HelpButton = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1536, 800);
            this.MinimizeBox = false;
            this.Name = "NewForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "新建工程";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.NewForm_HelpButtonClicked);
            this.Load += new System.EventHandler(this.NewForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UITextBox projectNameTextBox;
        private Sunny.UI.UIComboBox sceneComboBox;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton enterButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}