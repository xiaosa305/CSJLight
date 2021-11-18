
namespace LightController.MyForm.LightList
{
    partial class LightsEditForm
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
            this.cancelButton = new Sunny.UI.UIButton();
            this.enterButton = new Sunny.UI.UIButton();
            this.startNUD = new System.Windows.Forms.NumericUpDown();
            this.oldAddrLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameLabel2 = new System.Windows.Forms.Label();
            this.addrLabel = new System.Windows.Forms.Label();
            this.addrLabel2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startNUD)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.enterButton);
            this.panel1.Controls.Add(this.startNUD);
            this.panel1.Controls.Add(this.oldAddrLabel);
            this.panel1.Controls.Add(this.nameLabel);
            this.panel1.Controls.Add(this.nameLabel2);
            this.panel1.Controls.Add(this.addrLabel);
            this.panel1.Controls.Add(this.addrLabel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(240, 165);
            this.panel1.TabIndex = 0;
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(140, 122);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 19;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.enterButton.Location = new System.Drawing.Point(49, 122);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(54, 20);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 20;
            this.enterButton.Text = "确定";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // startNUD
            // 
            this.startNUD.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.startNUD.ForeColor = System.Drawing.Color.Black;
            this.startNUD.Location = new System.Drawing.Point(148, 82);
            this.startNUD.Margin = new System.Windows.Forms.Padding(2);
            this.startNUD.Maximum = new decimal(new int[] {
            512,
            0,
            0,
            0});
            this.startNUD.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.startNUD.Name = "startNUD";
            this.startNUD.Size = new System.Drawing.Size(50, 20);
            this.startNUD.TabIndex = 16;
            this.startNUD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.startNUD.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // oldAddrLabel
            // 
            this.oldAddrLabel.AutoSize = true;
            this.oldAddrLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.oldAddrLabel.ForeColor = System.Drawing.Color.White;
            this.oldAddrLabel.Location = new System.Drawing.Point(26, 54);
            this.oldAddrLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.oldAddrLabel.Name = "oldAddrLabel";
            this.oldAddrLabel.Size = new System.Drawing.Size(77, 11);
            this.oldAddrLabel.TabIndex = 13;
            this.oldAddrLabel.Text = "原灯具地址：";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.nameLabel.ForeColor = System.Drawing.Color.White;
            this.nameLabel.Location = new System.Drawing.Point(121, 21);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(41, 11);
            this.nameLabel.TabIndex = 17;
            this.nameLabel.Text = "label3";
            // 
            // nameLabel2
            // 
            this.nameLabel2.AutoSize = true;
            this.nameLabel2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.nameLabel2.ForeColor = System.Drawing.Color.White;
            this.nameLabel2.Location = new System.Drawing.Point(26, 21);
            this.nameLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.nameLabel2.Name = "nameLabel2";
            this.nameLabel2.Size = new System.Drawing.Size(41, 11);
            this.nameLabel2.TabIndex = 14;
            this.nameLabel2.Text = "灯具：";
            // 
            // addrLabel
            // 
            this.addrLabel.AutoSize = true;
            this.addrLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.addrLabel.ForeColor = System.Drawing.Color.White;
            this.addrLabel.Location = new System.Drawing.Point(148, 54);
            this.addrLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addrLabel.Name = "addrLabel";
            this.addrLabel.Size = new System.Drawing.Size(41, 11);
            this.addrLabel.TabIndex = 18;
            this.addrLabel.Text = "label3";
            // 
            // addrLabel2
            // 
            this.addrLabel2.AutoSize = true;
            this.addrLabel2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.addrLabel2.ForeColor = System.Drawing.Color.White;
            this.addrLabel2.Location = new System.Drawing.Point(26, 87);
            this.addrLabel2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.addrLabel2.Name = "addrLabel2";
            this.addrLabel2.Size = new System.Drawing.Size(77, 11);
            this.addrLabel2.TabIndex = 15;
            this.addrLabel2.Text = "新起始地址：";
            // 
            // LightsEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(240, 200);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LightsEditForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "修改灯具地址";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.LightsEditForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.startNUD)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown startNUD;
        private System.Windows.Forms.Label oldAddrLabel;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label nameLabel2;
        private System.Windows.Forms.Label addrLabel;
        private System.Windows.Forms.Label addrLabel2;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton enterButton;
    }
}