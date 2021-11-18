
namespace LightController.MyForm.LightList
{
    partial class LightsForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.noteicLabel = new System.Windows.Forms.Label();
            this.cancelButton = new Sunny.UI.UIButton();
            this.deleteButton = new Sunny.UI.UIButton();
            this.enterButton = new Sunny.UI.UIButton();
            this.addButton = new Sunny.UI.UIButton();
            this.libTreeView = new System.Windows.Forms.TreeView();
            this.lightsListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.noteicLabel);
            this.panel1.Controls.Add(this.cancelButton);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.enterButton);
            this.panel1.Controls.Add(this.addButton);
            this.panel1.Controls.Add(this.libTreeView);
            this.panel1.Controls.Add(this.lightsListView);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 35);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(700, 465);
            this.panel1.TabIndex = 0;
            // 
            // noteicLabel
            // 
            this.noteicLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.noteicLabel.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.noteicLabel.ForeColor = System.Drawing.Color.White;
            this.noteicLabel.Location = new System.Drawing.Point(246, 181);
            this.noteicLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.noteicLabel.Name = "noteicLabel";
            this.noteicLabel.Size = new System.Drawing.Size(139, 35);
            this.noteicLabel.TabIndex = 17;
            this.noteicLabel.Text = "提示：双击右侧灯具，可修改其通道地址。\r\n";
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(288, 395);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 15;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deleteButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.deleteButton.Location = new System.Drawing.Point(288, 108);
            this.deleteButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.deleteButton.Size = new System.Drawing.Size(54, 20);
            this.deleteButton.Style = Sunny.UI.UIStyle.Custom;
            this.deleteButton.TabIndex = 15;
            this.deleteButton.Text = "<-删除";
            this.deleteButton.Click += new System.EventHandler(this.deleteLightButton_Click);
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.enterButton.Location = new System.Drawing.Point(288, 354);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(54, 20);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 16;
            this.enterButton.Text = "确定";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // addButton
            // 
            this.addButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.addButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.addButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.addButton.Location = new System.Drawing.Point(288, 67);
            this.addButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.addButton.Name = "addButton";
            this.addButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.addButton.Size = new System.Drawing.Size(54, 20);
            this.addButton.Style = Sunny.UI.UIStyle.Custom;
            this.addButton.TabIndex = 16;
            this.addButton.Text = "添加->";
            this.addButton.Click += new System.EventHandler(this.addLightButton_Click);
            // 
            // libTreeView
            // 
            this.libTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.libTreeView.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.libTreeView.Location = new System.Drawing.Point(0, 0);
            this.libTreeView.Name = "libTreeView";
            this.libTreeView.Size = new System.Drawing.Size(238, 463);
            this.libTreeView.TabIndex = 14;
            this.libTreeView.DoubleClick += new System.EventHandler(this.addLightButton_Click);
            // 
            // lightsListView
            // 
            this.lightsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lightsListView.Dock = System.Windows.Forms.DockStyle.Right;
            this.lightsListView.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.lightsListView.FullRowSelect = true;
            this.lightsListView.GridLines = true;
            this.lightsListView.HideSelection = false;
            this.lightsListView.Location = new System.Drawing.Point(390, 0);
            this.lightsListView.Name = "lightsListView";
            this.lightsListView.Size = new System.Drawing.Size(308, 463);
            this.lightsListView.TabIndex = 13;
            this.lightsListView.UseCompatibleStateImageBehavior = false;
            this.lightsListView.View = System.Windows.Forms.View.Details;
            this.lightsListView.DoubleClick += new System.EventHandler(this.lightsListView_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "厂商名";
            this.columnHeader1.Width = 83;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "型号";
            this.columnHeader2.Width = 98;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "通道地址";
            this.columnHeader3.Width = 100;
            // 
            // myToolTip
            // 
            this.myToolTip.AutoPopDelay = 10000;
            this.myToolTip.InitialDelay = 500;
            this.myToolTip.ReshowDelay = 100;
            // 
            // LightsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.ClientSize = new System.Drawing.Size(700, 500);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LightsForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "编辑工程灯具列表";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.LightsForm_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView lightsListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.TreeView libTreeView;
        private Sunny.UI.UIButton deleteButton;
        private Sunny.UI.UIButton addButton;
        private System.Windows.Forms.Label noteicLabel;
        private Sunny.UI.UIButton cancelButton;
        private Sunny.UI.UIButton enterButton;
        private System.Windows.Forms.ToolTip myToolTip;
    }
}