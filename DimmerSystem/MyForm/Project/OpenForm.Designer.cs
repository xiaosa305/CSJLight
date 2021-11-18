
namespace LightController.MyForm.Project
{
    partial class OpenForm
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
            this.projectTreeView = new System.Windows.Forms.TreeView();
            this.changeWorkspaceButton = new Sunny.UI.UIButton();
            this.sortLastTimeButton = new Sunny.UI.UIButton();
            this.sortCreateTimeButton = new Sunny.UI.UIButton();
            this.sortNameButton = new Sunny.UI.UIButton();
            this.label1 = new System.Windows.Forms.Label();
            this.enterButton = new Sunny.UI.UIButton();
            this.deleteButton = new Sunny.UI.UIButton();
            this.cancelButton = new Sunny.UI.UIButton();
            this.label2 = new System.Windows.Forms.Label();
            this.sceneComboBox = new Sunny.UI.UIComboBox();
            this.wsFolderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.myContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renameButton = new System.Windows.Forms.ToolStripMenuItem();
            this.copyButton = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.myContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.projectTreeView);
            this.panel1.Controls.Add(this.changeWorkspaceButton);
            this.panel1.Controls.Add(this.sortLastTimeButton);
            this.panel1.Controls.Add(this.sortCreateTimeButton);
            this.panel1.Controls.Add(this.sortNameButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(280, 228);
            this.panel1.TabIndex = 1;
            // 
            // projectTreeView
            // 
            this.projectTreeView.Dock = System.Windows.Forms.DockStyle.Left;
            this.projectTreeView.Location = new System.Drawing.Point(0, 0);
            this.projectTreeView.Name = "projectTreeView";
            this.projectTreeView.Size = new System.Drawing.Size(200, 228);
            this.projectTreeView.TabIndex = 3;
            this.projectTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectTreeView_NodeMouseClick);
            this.projectTreeView.DoubleClick += new System.EventHandler(this.projectTreeView_DoubleClick);
            this.projectTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.projectTreeView_MouseDown);
            // 
            // changeWorkspaceButton
            // 
            this.changeWorkspaceButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.changeWorkspaceButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.changeWorkspaceButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.changeWorkspaceButton.Location = new System.Drawing.Point(211, 181);
            this.changeWorkspaceButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.changeWorkspaceButton.Name = "changeWorkspaceButton";
            this.changeWorkspaceButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.changeWorkspaceButton.Size = new System.Drawing.Size(57, 32);
            this.changeWorkspaceButton.Style = Sunny.UI.UIStyle.Custom;
            this.changeWorkspaceButton.TabIndex = 2;
            this.changeWorkspaceButton.Text = "  更改  \n工作目录";
            this.changeWorkspaceButton.Click += new System.EventHandler(this.changeWorkspaceButton_Click);
            // 
            // sortLastTimeButton
            // 
            this.sortLastTimeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sortLastTimeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.sortLastTimeButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.sortLastTimeButton.Location = new System.Drawing.Point(211, 135);
            this.sortLastTimeButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.sortLastTimeButton.Name = "sortLastTimeButton";
            this.sortLastTimeButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.sortLastTimeButton.Size = new System.Drawing.Size(57, 32);
            this.sortLastTimeButton.Style = Sunny.UI.UIStyle.Custom;
            this.sortLastTimeButton.TabIndex = 2;
            this.sortLastTimeButton.Text = "保存时间";
            this.sortLastTimeButton.Click += new System.EventHandler(this.sortLastTimeButton_Click);
            // 
            // sortCreateTimeButton
            // 
            this.sortCreateTimeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sortCreateTimeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.sortCreateTimeButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.sortCreateTimeButton.Location = new System.Drawing.Point(211, 89);
            this.sortCreateTimeButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.sortCreateTimeButton.Name = "sortCreateTimeButton";
            this.sortCreateTimeButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.sortCreateTimeButton.Size = new System.Drawing.Size(57, 32);
            this.sortCreateTimeButton.Style = Sunny.UI.UIStyle.Custom;
            this.sortCreateTimeButton.TabIndex = 2;
            this.sortCreateTimeButton.Text = "创建时间";
            this.sortCreateTimeButton.Click += new System.EventHandler(this.sortCreateTimeButton_Click);
            // 
            // sortNameButton
            // 
            this.sortNameButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.sortNameButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.sortNameButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.sortNameButton.Location = new System.Drawing.Point(211, 43);
            this.sortNameButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.sortNameButton.Name = "sortNameButton";
            this.sortNameButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.sortNameButton.Size = new System.Drawing.Size(57, 32);
            this.sortNameButton.Style = Sunny.UI.UIStyle.Custom;
            this.sortNameButton.TabIndex = 2;
            this.sortNameButton.Text = "文件名";
            this.sortNameButton.Click += new System.EventHandler(this.sortNameButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(209, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 11);
            this.label1.TabIndex = 1;
            this.label1.Text = "排序方法：";
            // 
            // enterButton
            // 
            this.enterButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.enterButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.enterButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.enterButton.Location = new System.Drawing.Point(22, 306);
            this.enterButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.enterButton.Name = "enterButton";
            this.enterButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.enterButton.Size = new System.Drawing.Size(54, 20);
            this.enterButton.Style = Sunny.UI.UIStyle.Custom;
            this.enterButton.TabIndex = 2;
            this.enterButton.Text = "确定";
            this.enterButton.Click += new System.EventHandler(this.enterButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.deleteButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deleteButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.deleteButton.Location = new System.Drawing.Point(109, 306);
            this.deleteButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.deleteButton.Size = new System.Drawing.Size(54, 20);
            this.deleteButton.Style = Sunny.UI.UIStyle.Custom;
            this.deleteButton.TabIndex = 2;
            this.deleteButton.Text = "删除";
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cancelButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.cancelButton.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.cancelButton.Location = new System.Drawing.Point(196, 306);
            this.cancelButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.cancelButton.Size = new System.Drawing.Size(54, 20);
            this.cancelButton.Style = Sunny.UI.UIStyle.Custom;
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "取消";
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(22, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 11);
            this.label2.TabIndex = 1;
            this.label2.Text = "初始场景：";
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
            this.sceneComboBox.Location = new System.Drawing.Point(88, 273);
            this.sceneComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.sceneComboBox.MinimumSize = new System.Drawing.Size(63, 0);
            this.sceneComboBox.Name = "sceneComboBox";
            this.sceneComboBox.Padding = new System.Windows.Forms.Padding(0, 0, 30, 2);
            this.sceneComboBox.Radius = 4;
            this.sceneComboBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.sceneComboBox.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.sceneComboBox.Size = new System.Drawing.Size(165, 20);
            this.sceneComboBox.Style = Sunny.UI.UIStyle.Custom;
            this.sceneComboBox.TabIndex = 6;
            this.sceneComboBox.TextAlignment = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // myContextMenuStrip
            // 
            this.myContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renameButton,
            this.copyButton});
            this.myContextMenuStrip.Name = "contextMenuStrip1";
            this.myContextMenuStrip.Size = new System.Drawing.Size(137, 48);
            // 
            // renameButton
            // 
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(136, 22);
            this.renameButton.Text = "工程重命名";
            this.renameButton.Click += new System.EventHandler(this.renameToolStripMenuItem_Click);
            // 
            // copyButton
            // 
            this.copyButton.Name = "copyButton";
            this.copyButton.Size = new System.Drawing.Size(136, 22);
            this.copyButton.Text = "工程复制";
            this.copyButton.Click += new System.EventHandler(this.copyProjectToolStripMenuItem_Click);
            // 
            // OpenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(280, 340);
            this.Controls.Add(this.sceneComboBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.enterButton);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1536, 800);
            this.MinimizeBox = false;
            this.Name = "OpenForm";
            this.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "打开工程";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.TitleHeight = 32;
            this.Load += new System.EventHandler(this.OpenForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.myContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private Sunny.UI.UIButton changeWorkspaceButton;
        private Sunny.UI.UIButton sortLastTimeButton;
        private Sunny.UI.UIButton sortCreateTimeButton;
        private Sunny.UI.UIButton sortNameButton;
        private Sunny.UI.UIButton enterButton;
        private Sunny.UI.UIButton deleteButton;
        private Sunny.UI.UIButton cancelButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TreeView projectTreeView;
        private Sunny.UI.UIComboBox sceneComboBox;
        private System.Windows.Forms.FolderBrowserDialog wsFolderBrowserDialog;
        private System.Windows.Forms.ContextMenuStrip myContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem renameButton;
        private System.Windows.Forms.ToolStripMenuItem copyButton;
    }
}