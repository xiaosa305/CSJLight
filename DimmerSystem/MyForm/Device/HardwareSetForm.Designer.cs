
namespace LightController.MyForm.Device
{
    partial class HardwareSetForm
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.uiTabControl2 = new Sunny.UI.UITabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.fileOpenButton = new Sunny.UI.UIButton();
            this.uiLabel8 = new Sunny.UI.UILabel();
            this.myProcessBar = new Sunny.UI.UIProcessBar();
            this.pathLabel = new Sunny.UI.UILabel();
            this.updateButton = new Sunny.UI.UIButton();
            this.gatewayTextBox = new Sunny.UI.UITextBox();
            this.uiLabel3 = new Sunny.UI.UILabel();
            this.uiLabel4 = new Sunny.UI.UILabel();
            this.macTextBox = new Sunny.UI.UITextBox();
            this.IPTextBox = new Sunny.UI.UITextBox();
            this.writeButton = new Sunny.UI.UIButton();
            this.uiLabel5 = new Sunny.UI.UILabel();
            this.macCheckBox = new Sunny.UI.UICheckBox();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.readButton = new Sunny.UI.UIButton();
            this.deviceNameTextBox = new Sunny.UI.UITextBox();
            this.hidePanel = new System.Windows.Forms.Panel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.protocolTextBox = new Sunny.UI.UITextBox();
            this.uiLabel6 = new Sunny.UI.UILabel();
            this.firmwarePanel = new System.Windows.Forms.Panel();
            this.hardwarePanel = new System.Windows.Forms.Panel();
            this.netmaskTextBox = new Sunny.UI.UITextBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.maskPanel = new LightController.Ast.Form.MaskPanel();
            this.myStatusStrip.SuspendLayout();
            this.uiTabControl2.SuspendLayout();
            this.firmwarePanel.SuspendLayout();
            this.hardwarePanel.SuspendLayout();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "*.xbin(自定义二进制文件)|*.xbin";
            // 
            // myStatusStrip
            // 
            this.myStatusStrip.AutoSize = false;
            this.myStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
            this.myStatusStrip.Location = new System.Drawing.Point(0, 421);
            this.myStatusStrip.Name = "myStatusStrip";
            this.myStatusStrip.ShowItemToolTips = true;
            this.myStatusStrip.Size = new System.Drawing.Size(488, 24);
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
            // uiTabControl2
            // 
            this.uiTabControl2.Controls.Add(this.tabPage3);
            this.uiTabControl2.Controls.Add(this.tabPage4);
            this.uiTabControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.uiTabControl2.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.uiTabControl2.Font = new System.Drawing.Font("黑体", 9F);
            this.uiTabControl2.ItemSize = new System.Drawing.Size(100, 25);
            this.uiTabControl2.Location = new System.Drawing.Point(0, 0);
            this.uiTabControl2.MainPage = "";
            this.uiTabControl2.MenuStyle = Sunny.UI.UIMenuStyle.Custom;
            this.uiTabControl2.Name = "uiTabControl2";
            this.uiTabControl2.SelectedIndex = 0;
            this.uiTabControl2.Size = new System.Drawing.Size(488, 23);
            this.uiTabControl2.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.uiTabControl2.Style = Sunny.UI.UIStyle.Custom;
            this.uiTabControl2.TabBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.uiTabControl2.TabIndex = 39;
            this.uiTabControl2.TabSelectedForeColor = System.Drawing.Color.White;
            this.uiTabControl2.TabSelectedHighColor = System.Drawing.Color.Transparent;
            this.uiTabControl2.SelectedIndexChanged += new System.EventHandler(this.uiTabControl2_SelectedIndexChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.tabPage3.Location = new System.Drawing.Point(0, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(488, 0);
            this.tabPage3.TabIndex = 0;
            this.tabPage3.Text = "硬件配置";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.tabPage4.Location = new System.Drawing.Point(0, 25);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(488, 0);
            this.tabPage4.TabIndex = 1;
            this.tabPage4.Text = "固件升级";
            // 
            // fileOpenButton
            // 
            this.fileOpenButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.fileOpenButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.fileOpenButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.fileOpenButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.fileOpenButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.fileOpenButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.fileOpenButton.Font = new System.Drawing.Font("黑体", 8.25F);
            this.fileOpenButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.fileOpenButton.Location = new System.Drawing.Point(30, 28);
            this.fileOpenButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.fileOpenButton.Name = "fileOpenButton";
            this.fileOpenButton.Radius = 10;
            this.fileOpenButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fileOpenButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fileOpenButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fileOpenButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fileOpenButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.fileOpenButton.Size = new System.Drawing.Size(80, 30);
            this.fileOpenButton.Style = Sunny.UI.UIStyle.Custom;
            this.fileOpenButton.TabIndex = 31;
            this.fileOpenButton.Text = "选择升级文件";
            this.fileOpenButton.Click += new System.EventHandler(this.fileOpenButton_Click);
            // 
            // uiLabel8
            // 
            this.uiLabel8.AutoSize = true;
            this.uiLabel8.Font = new System.Drawing.Font("黑体", 8F);
            this.uiLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel8.Location = new System.Drawing.Point(33, 91);
            this.uiLabel8.Name = "uiLabel8";
            this.uiLabel8.Size = new System.Drawing.Size(65, 11);
            this.uiLabel8.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel8.TabIndex = 30;
            this.uiLabel8.Text = "升级进度：";
            this.uiLabel8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // myProcessBar
            // 
            this.myProcessBar.BackColor = System.Drawing.Color.Transparent;
            this.myProcessBar.DecimalCount = 0;
            this.myProcessBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myProcessBar.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myProcessBar.ForeColor = System.Drawing.Color.White;
            this.myProcessBar.Location = new System.Drawing.Point(133, 88);
            this.myProcessBar.MinimumSize = new System.Drawing.Size(70, 5);
            this.myProcessBar.Name = "myProcessBar";
            this.myProcessBar.Radius = 10;
            this.myProcessBar.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.myProcessBar.RectSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.myProcessBar.Size = new System.Drawing.Size(300, 18);
            this.myProcessBar.Style = Sunny.UI.UIStyle.Custom;
            this.myProcessBar.TabIndex = 29;
            this.myProcessBar.Text = "40%";
            // 
            // pathLabel
            // 
            this.pathLabel.Font = new System.Drawing.Font("黑体", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pathLabel.ForeColor = System.Drawing.Color.White;
            this.pathLabel.Location = new System.Drawing.Point(128, 28);
            this.pathLabel.Name = "pathLabel";
            this.pathLabel.Size = new System.Drawing.Size(322, 30);
            this.pathLabel.Style = Sunny.UI.UIStyle.Custom;
            this.pathLabel.TabIndex = 28;
            this.pathLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // updateButton
            // 
            this.updateButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.updateButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.updateButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.updateButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.updateButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.updateButton.Font = new System.Drawing.Font("黑体", 8.25F);
            this.updateButton.Location = new System.Drawing.Point(357, 145);
            this.updateButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.updateButton.Name = "updateButton";
            this.updateButton.Radius = 10;
            this.updateButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.updateButton.Size = new System.Drawing.Size(80, 20);
            this.updateButton.Style = Sunny.UI.UIStyle.Custom;
            this.updateButton.StyleCustomMode = true;
            this.updateButton.TabIndex = 27;
            this.updateButton.Text = "升级";
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // gatewayTextBox
            // 
            this.gatewayTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.gatewayTextBox.ButtonSymbol = 61761;
            this.gatewayTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.gatewayTextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.gatewayTextBox.Font = new System.Drawing.Font("黑体", 8F);
            this.gatewayTextBox.ForeColor = System.Drawing.Color.White;
            this.gatewayTextBox.Location = new System.Drawing.Point(93, 86);
            this.gatewayTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.gatewayTextBox.Maximum = 2147483647D;
            this.gatewayTextBox.Minimum = -2147483648D;
            this.gatewayTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.gatewayTextBox.Name = "gatewayTextBox";
            this.gatewayTextBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gatewayTextBox.Radius = 4;
            this.gatewayTextBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.gatewayTextBox.Size = new System.Drawing.Size(120, 23);
            this.gatewayTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.gatewayTextBox.TabIndex = 18;
            this.gatewayTextBox.Text = "192.168.2.1";
            this.gatewayTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.gatewayTextBox.Watermark = "";
            // 
            // uiLabel3
            // 
            this.uiLabel3.AutoSize = true;
            this.uiLabel3.Font = new System.Drawing.Font("黑体", 8F);
            this.uiLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel3.Location = new System.Drawing.Point(28, 94);
            this.uiLabel3.Name = "uiLabel3";
            this.uiLabel3.Size = new System.Drawing.Size(41, 11);
            this.uiLabel3.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel3.TabIndex = 17;
            this.uiLabel3.Text = "网关：";
            this.uiLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel4
            // 
            this.uiLabel4.AutoSize = true;
            this.uiLabel4.Font = new System.Drawing.Font("黑体", 8F);
            this.uiLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel4.Location = new System.Drawing.Point(28, 127);
            this.uiLabel4.Name = "uiLabel4";
            this.uiLabel4.Size = new System.Drawing.Size(59, 11);
            this.uiLabel4.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel4.TabIndex = 17;
            this.uiLabel4.Text = "Mac地址：";
            this.uiLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // macTextBox
            // 
            this.macTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.macTextBox.ButtonSymbol = 61761;
            this.macTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.macTextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.macTextBox.Font = new System.Drawing.Font("黑体", 8F);
            this.macTextBox.ForeColor = System.Drawing.Color.White;
            this.macTextBox.Location = new System.Drawing.Point(93, 120);
            this.macTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.macTextBox.Maximum = 2147483647D;
            this.macTextBox.Minimum = -2147483648D;
            this.macTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.macTextBox.Name = "macTextBox";
            this.macTextBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.macTextBox.Radius = 4;
            this.macTextBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.macTextBox.Size = new System.Drawing.Size(120, 23);
            this.macTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.macTextBox.TabIndex = 18;
            this.macTextBox.Text = "00-00-00-00-00-00";
            this.macTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.macTextBox.Watermark = "";
            // 
            // IPTextBox
            // 
            this.IPTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.IPTextBox.ButtonSymbol = 61761;
            this.IPTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.IPTextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.IPTextBox.Font = new System.Drawing.Font("黑体", 8F);
            this.IPTextBox.ForeColor = System.Drawing.Color.White;
            this.IPTextBox.Location = new System.Drawing.Point(93, 52);
            this.IPTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.IPTextBox.Maximum = 2147483647D;
            this.IPTextBox.Minimum = -2147483648D;
            this.IPTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.IPTextBox.Name = "IPTextBox";
            this.IPTextBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.IPTextBox.Radius = 4;
            this.IPTextBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.IPTextBox.Size = new System.Drawing.Size(120, 23);
            this.IPTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.IPTextBox.TabIndex = 18;
            this.IPTextBox.Text = "192.168.2.10";
            this.IPTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.IPTextBox.Watermark = "";
            // 
            // writeButton
            // 
            this.writeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.writeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.writeButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.writeButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.writeButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.writeButton.Font = new System.Drawing.Font("黑体", 8.25F);
            this.writeButton.Location = new System.Drawing.Point(356, 165);
            this.writeButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.writeButton.Name = "writeButton";
            this.writeButton.Radius = 10;
            this.writeButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.writeButton.Size = new System.Drawing.Size(80, 20);
            this.writeButton.Style = Sunny.UI.UIStyle.Custom;
            this.writeButton.StyleCustomMode = true;
            this.writeButton.TabIndex = 20;
            this.writeButton.Text = "写入设备";
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // uiLabel5
            // 
            this.uiLabel5.AutoSize = true;
            this.uiLabel5.Font = new System.Drawing.Font("黑体", 8F);
            this.uiLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel5.Location = new System.Drawing.Point(256, 58);
            this.uiLabel5.Name = "uiLabel5";
            this.uiLabel5.Size = new System.Drawing.Size(65, 11);
            this.uiLabel5.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel5.TabIndex = 17;
            this.uiLabel5.Text = "子网掩码：";
            this.uiLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // macCheckBox
            // 
            this.macCheckBox.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.macCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.macCheckBox.Font = new System.Drawing.Font("黑体", 8.25F);
            this.macCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.macCheckBox.Location = new System.Drawing.Point(258, 119);
            this.macCheckBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.macCheckBox.Name = "macCheckBox";
            this.macCheckBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.macCheckBox.Size = new System.Drawing.Size(129, 27);
            this.macCheckBox.Style = Sunny.UI.UIStyle.Custom;
            this.macCheckBox.TabIndex = 23;
            this.macCheckBox.Text = "自动获取MAC地址";
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.Font = new System.Drawing.Font("黑体", 8F);
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel2.Location = new System.Drawing.Point(28, 58);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(53, 11);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.TabIndex = 17;
            this.uiLabel2.Text = "IP地址：";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // readButton
            // 
            this.readButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.readButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.readButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.readButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.readButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.readButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.readButton.Font = new System.Drawing.Font("黑体", 8.25F);
            this.readButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.readButton.Location = new System.Drawing.Point(256, 165);
            this.readButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.readButton.Name = "readButton";
            this.readButton.Radius = 10;
            this.readButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.Size = new System.Drawing.Size(80, 20);
            this.readButton.Style = Sunny.UI.UIStyle.Custom;
            this.readButton.TabIndex = 24;
            this.readButton.Text = "从设备回读";
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // deviceNameTextBox
            // 
            this.deviceNameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deviceNameTextBox.ButtonSymbol = 61761;
            this.deviceNameTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.deviceNameTextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.deviceNameTextBox.Font = new System.Drawing.Font("黑体", 8F);
            this.deviceNameTextBox.ForeColor = System.Drawing.Color.White;
            this.deviceNameTextBox.Location = new System.Drawing.Point(93, 18);
            this.deviceNameTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.deviceNameTextBox.Maximum = 2147483647D;
            this.deviceNameTextBox.Minimum = -2147483648D;
            this.deviceNameTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.deviceNameTextBox.Name = "deviceNameTextBox";
            this.deviceNameTextBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.deviceNameTextBox.Radius = 4;
            this.deviceNameTextBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.deviceNameTextBox.Size = new System.Drawing.Size(120, 23);
            this.deviceNameTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.deviceNameTextBox.TabIndex = 18;
            this.deviceNameTextBox.Text = "910出厂配置";
            this.deviceNameTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.deviceNameTextBox.Watermark = "";
            // 
            // hidePanel
            // 
            this.hidePanel.Location = new System.Drawing.Point(245, 13);
            this.hidePanel.Name = "hidePanel";
            this.hidePanel.Size = new System.Drawing.Size(221, 31);
            this.hidePanel.TabIndex = 27;
            this.hidePanel.DoubleClick += new System.EventHandler(this.hidePanel_DoubleClick);
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("黑体", 8F);
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel1.Location = new System.Drawing.Point(28, 24);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(53, 11);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 17;
            this.uiLabel1.Text = "设备名：";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // protocolTextBox
            // 
            this.protocolTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.protocolTextBox.ButtonSymbol = 61761;
            this.protocolTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.protocolTextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.protocolTextBox.Font = new System.Drawing.Font("黑体", 8F);
            this.protocolTextBox.ForeColor = System.Drawing.Color.White;
            this.protocolTextBox.Location = new System.Drawing.Point(324, 18);
            this.protocolTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.protocolTextBox.Maximum = 2147483647D;
            this.protocolTextBox.Minimum = -2147483648D;
            this.protocolTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.protocolTextBox.Name = "protocolTextBox";
            this.protocolTextBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.protocolTextBox.Radius = 4;
            this.protocolTextBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.protocolTextBox.Size = new System.Drawing.Size(120, 23);
            this.protocolTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.protocolTextBox.TabIndex = 25;
            this.protocolTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.protocolTextBox.Watermark = "";
            // 
            // uiLabel6
            // 
            this.uiLabel6.AutoSize = true;
            this.uiLabel6.Font = new System.Drawing.Font("黑体", 8F);
            this.uiLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel6.Location = new System.Drawing.Point(256, 24);
            this.uiLabel6.Name = "uiLabel6";
            this.uiLabel6.Size = new System.Drawing.Size(41, 11);
            this.uiLabel6.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel6.TabIndex = 26;
            this.uiLabel6.Text = "协议：";
            this.uiLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // firmwarePanel
            // 
            this.firmwarePanel.Controls.Add(this.fileOpenButton);
            this.firmwarePanel.Controls.Add(this.uiLabel8);
            this.firmwarePanel.Controls.Add(this.updateButton);
            this.firmwarePanel.Controls.Add(this.myProcessBar);
            this.firmwarePanel.Controls.Add(this.pathLabel);
            this.firmwarePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.firmwarePanel.Location = new System.Drawing.Point(0, 221);
            this.firmwarePanel.Name = "firmwarePanel";
            this.firmwarePanel.Size = new System.Drawing.Size(488, 200);
            this.firmwarePanel.TabIndex = 40;
            // 
            // hardwarePanel
            // 
            this.hardwarePanel.Controls.Add(this.hidePanel);
            this.hardwarePanel.Controls.Add(this.netmaskTextBox);
            this.hardwarePanel.Controls.Add(this.uiLabel6);
            this.hardwarePanel.Controls.Add(this.uiLabel1);
            this.hardwarePanel.Controls.Add(this.writeButton);
            this.hardwarePanel.Controls.Add(this.IPTextBox);
            this.hardwarePanel.Controls.Add(this.protocolTextBox);
            this.hardwarePanel.Controls.Add(this.uiLabel5);
            this.hardwarePanel.Controls.Add(this.macTextBox);
            this.hardwarePanel.Controls.Add(this.macCheckBox);
            this.hardwarePanel.Controls.Add(this.gatewayTextBox);
            this.hardwarePanel.Controls.Add(this.uiLabel2);
            this.hardwarePanel.Controls.Add(this.deviceNameTextBox);
            this.hardwarePanel.Controls.Add(this.uiLabel4);
            this.hardwarePanel.Controls.Add(this.uiLabel3);
            this.hardwarePanel.Controls.Add(this.readButton);
            this.hardwarePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.hardwarePanel.Location = new System.Drawing.Point(0, 23);
            this.hardwarePanel.Name = "hardwarePanel";
            this.hardwarePanel.Size = new System.Drawing.Size(488, 200);
            this.hardwarePanel.TabIndex = 41;
            // 
            // netmaskTextBox
            // 
            this.netmaskTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.netmaskTextBox.ButtonSymbol = 61761;
            this.netmaskTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.netmaskTextBox.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.netmaskTextBox.Font = new System.Drawing.Font("黑体", 8F);
            this.netmaskTextBox.ForeColor = System.Drawing.Color.White;
            this.netmaskTextBox.Location = new System.Drawing.Point(324, 52);
            this.netmaskTextBox.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.netmaskTextBox.Maximum = 2147483647D;
            this.netmaskTextBox.Minimum = -2147483648D;
            this.netmaskTextBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.netmaskTextBox.Name = "netmaskTextBox";
            this.netmaskTextBox.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.netmaskTextBox.Radius = 4;
            this.netmaskTextBox.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.netmaskTextBox.Size = new System.Drawing.Size(120, 23);
            this.netmaskTextBox.Style = Sunny.UI.UIStyle.Custom;
            this.netmaskTextBox.TabIndex = 28;
            this.netmaskTextBox.Text = "255.255.255.0";
            this.netmaskTextBox.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.netmaskTextBox.Watermark = "";
            // 
            // mainPanel
            // 
            this.mainPanel.Controls.Add(this.hardwarePanel);
            this.mainPanel.Controls.Add(this.firmwarePanel);
            this.mainPanel.Controls.Add(this.uiTabControl2);
            this.mainPanel.Controls.Add(this.myStatusStrip);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(488, 445);
            this.mainPanel.TabIndex = 29;
            // 
            // maskPanel
            // 
            this.maskPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskPanel.Location = new System.Drawing.Point(0, 35);
            this.maskPanel.Name = "maskPanel";
            this.maskPanel.Size = new System.Drawing.Size(488, 445);
            this.maskPanel.TabIndex = 29;
            this.maskPanel.Visible = false;
            // 
            // HardwareSetForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(488, 480);
            this.Controls.Add(this.maskPanel);
            this.Controls.Add(this.mainPanel);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HardwareSetForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ShowRadius = false;
            this.ShowShadow = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "网络配置";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("黑体", 10F);
            this.Load += new System.EventHandler(this.HardwareSet_Load);
            this.Shown += new System.EventHandler(this.HardwareSetForm_Shown);
            this.myStatusStrip.ResumeLayout(false);
            this.myStatusStrip.PerformLayout();
            this.uiTabControl2.ResumeLayout(false);
            this.firmwarePanel.ResumeLayout(false);
            this.firmwarePanel.PerformLayout();
            this.hardwarePanel.ResumeLayout(false);
            this.hardwarePanel.PerformLayout();
            this.mainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private Sunny.UI.UITabControl uiTabControl2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private Sunny.UI.UIButton fileOpenButton;
        private Sunny.UI.UILabel uiLabel8;
        private Sunny.UI.UIProcessBar myProcessBar;
        private Sunny.UI.UILabel pathLabel;
        private Sunny.UI.UIButton updateButton;
        private Sunny.UI.UITextBox gatewayTextBox;
        private Sunny.UI.UILabel uiLabel3;
        private Sunny.UI.UILabel uiLabel4;
        private Sunny.UI.UITextBox macTextBox;
        private Sunny.UI.UITextBox IPTextBox;
        private Sunny.UI.UIButton writeButton;
        private Sunny.UI.UILabel uiLabel5;
        private Sunny.UI.UICheckBox macCheckBox;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UIButton readButton;
        private Sunny.UI.UITextBox deviceNameTextBox;
        private System.Windows.Forms.Panel hidePanel;
        private Sunny.UI.UILabel uiLabel1;
        private Sunny.UI.UITextBox protocolTextBox;
        private Sunny.UI.UILabel uiLabel6;
        private System.Windows.Forms.Panel firmwarePanel;
        private System.Windows.Forms.Panel hardwarePanel;
        private Sunny.UI.UITextBox netmaskTextBox;
        private System.Windows.Forms.Panel mainPanel;
        private Ast.Form.MaskPanel maskPanel;
    }
}