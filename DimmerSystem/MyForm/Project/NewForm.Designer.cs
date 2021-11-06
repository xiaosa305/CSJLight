
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
            this.uiTextBox1 = new Sunny.UI.UITextBox();
            this.uiComboBox3 = new Sunny.UI.UIComboBox();
            this.uiButton6 = new Sunny.UI.UIButton();
            this.uiButton5 = new Sunny.UI.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.uiTextBox1);
            this.panel1.Controls.Add(this.uiComboBox3);
            this.panel1.Controls.Add(this.uiButton6);
            this.panel1.Controls.Add(this.uiButton5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(270, 145);
            this.panel1.TabIndex = 0;
            // 
            // uiTextBox1
            // 
            this.uiTextBox1.ButtonSymbol = 61761;
            this.uiTextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.uiTextBox1.FillColor = System.Drawing.Color.White;
            this.uiTextBox1.Font = new System.Drawing.Font("黑体", 8F);
            this.uiTextBox1.Location = new System.Drawing.Point(94, 23);
            this.uiTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiTextBox1.Maximum = 2147483647D;
            this.uiTextBox1.Minimum = -2147483648D;
            this.uiTextBox1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiTextBox1.Name = "uiTextBox1";
            this.uiTextBox1.Padding = new System.Windows.Forms.Padding(5);
            this.uiTextBox1.Size = new System.Drawing.Size(157, 20);
            this.uiTextBox1.Style = Sunny.UI.UIStyle.Custom;
            this.uiTextBox1.TabIndex = 11;
            this.uiTextBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiComboBox3
            // 
            this.uiComboBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.uiComboBox3.DataSource = null;
            this.uiComboBox3.DropDownStyle = Sunny.UI.UIDropDownStyle.DropDownList;
            this.uiComboBox3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.uiComboBox3.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.uiComboBox3.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiComboBox3.ForeColor = System.Drawing.Color.White;
            this.uiComboBox3.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.uiComboBox3.Items.AddRange(new object[] {
            "激情（01）",
            "柔和（02）"});
            this.uiComboBox3.Location = new System.Drawing.Point(94, 58);
            this.uiComboBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.uiComboBox3.MinimumSize = new System.Drawing.Size(63, 0);
            this.uiComboBox3.Name = "uiComboBox3";
            this.uiComboBox3.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.uiComboBox3.Radius = 4;
            this.uiComboBox3.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiComboBox3.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.uiComboBox3.Size = new System.Drawing.Size(157, 20);
            this.uiComboBox3.Style = Sunny.UI.UIStyle.Custom;
            this.uiComboBox3.TabIndex = 10;
            this.uiComboBox3.Text = "激情（01）";
            this.uiComboBox3.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // uiButton6
            // 
            this.uiButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton6.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.uiButton6.Font = new System.Drawing.Font("黑体", 8F);
            this.uiButton6.Location = new System.Drawing.Point(164, 100);
            this.uiButton6.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton6.Name = "uiButton6";
            this.uiButton6.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.uiButton6.Size = new System.Drawing.Size(54, 20);
            this.uiButton6.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton6.TabIndex = 8;
            this.uiButton6.Text = "取消";
            // 
            // uiButton5
            // 
            this.uiButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.uiButton5.Font = new System.Drawing.Font("黑体", 8F);
            this.uiButton5.Location = new System.Drawing.Point(55, 100);
            this.uiButton5.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiButton5.Name = "uiButton5";
            this.uiButton5.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.uiButton5.Size = new System.Drawing.Size(54, 20);
            this.uiButton5.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton5.TabIndex = 9;
            this.uiButton5.Text = "确定";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 8F);
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
            this.label2.Font = new System.Drawing.Font("黑体", 8F);
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
            this.Font = new System.Drawing.Font("黑体", 9F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1536, 800);
            this.MinimizeBox = false;
            this.Name = "NewForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "新建工程";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("黑体", 10F);
            this.Load += new System.EventHandler(this.NewForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Sunny.UI.UITextBox uiTextBox1;
        private Sunny.UI.UIComboBox uiComboBox3;
        private Sunny.UI.UIButton uiButton6;
        private Sunny.UI.UIButton uiButton5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}