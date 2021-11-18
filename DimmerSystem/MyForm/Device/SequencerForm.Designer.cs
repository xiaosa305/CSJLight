
namespace LightController.MyForm.Device
{
    partial class SequencerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequencerForm));
            this.mainPanel = new System.Windows.Forms.Panel();
            this.relayFLP = new System.Windows.Forms.FlowLayoutPanel();
            this.relayPanelDemo = new System.Windows.Forms.Panel();
            this.relayButtonDemo = new Sunny.UI.UIImageButton();
            this.relayTextBoxDemo = new Sunny.UI.UITextBox();
            this.loadButton = new Sunny.UI.UIButton();
            this.saveButton = new Sunny.UI.UIButton();
            this.readButton = new Sunny.UI.UIButton();
            this.openCheckBox = new Sunny.UI.UICheckBox();
            this.writeButton = new Sunny.UI.UIButton();
            this.uiLabel2 = new Sunny.UI.UILabel();
            this.uiLabel1 = new Sunny.UI.UILabel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.closeButton = new Sunny.UI.UIButton();
            this.uiButton5 = new Sunny.UI.UIButton();
            this.panel4 = new System.Windows.Forms.Panel();
            this.myStatusStrip = new System.Windows.Forms.StatusStrip();
            this.myStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.myToolTip = new Sunny.UI.UIToolTip(this.components);
            this.lbinOpenDialog = new System.Windows.Forms.OpenFileDialog();
            this.lbinSaveDialog = new System.Windows.Forms.SaveFileDialog();
            this.timeNUDDemo = new System.Windows.Forms.NumericUpDown();
            this.maskPanel = new LightController.Ast.Form.MaskPanel();
            this.mainPanel.SuspendLayout();
            this.relayFLP.SuspendLayout();
            this.relayPanelDemo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.relayButtonDemo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.myStatusStrip.SuspendLayout();
            this.labelPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeNUDDemo)).BeginInit();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainPanel.Controls.Add(this.relayFLP);
            this.mainPanel.Controls.Add(this.loadButton);
            this.mainPanel.Controls.Add(this.saveButton);
            this.mainPanel.Controls.Add(this.readButton);
            this.mainPanel.Controls.Add(this.openCheckBox);
            this.mainPanel.Controls.Add(this.writeButton);
            this.mainPanel.Controls.Add(this.uiLabel2);
            this.mainPanel.Controls.Add(this.uiLabel1);
            this.mainPanel.Controls.Add(this.pictureBox2);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Controls.Add(this.closeButton);
            this.mainPanel.Controls.Add(this.uiButton5);
            this.mainPanel.Controls.Add(this.panel4);
            this.mainPanel.Controls.Add(this.myStatusStrip);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.Location = new System.Drawing.Point(0, 35);
            this.mainPanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(650, 345);
            this.mainPanel.TabIndex = 0;
            // 
            // relayFLP
            // 
            this.relayFLP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.relayFLP.Controls.Add(this.relayPanelDemo);
            this.relayFLP.Controls.Add(this.labelPanel);
            this.relayFLP.Controls.Add(this.timeNUDDemo);
            this.relayFLP.Location = new System.Drawing.Point(74, 66);
            this.relayFLP.Name = "relayFLP";
            this.relayFLP.Size = new System.Drawing.Size(500, 140);
            this.relayFLP.TabIndex = 45;
            // 
            // relayPanelDemo
            // 
            this.relayPanelDemo.Controls.Add(this.relayButtonDemo);
            this.relayPanelDemo.Controls.Add(this.relayTextBoxDemo);
            this.relayPanelDemo.Location = new System.Drawing.Point(0, 0);
            this.relayPanelDemo.Margin = new System.Windows.Forms.Padding(0);
            this.relayPanelDemo.Name = "relayPanelDemo";
            this.relayPanelDemo.Size = new System.Drawing.Size(71, 100);
            this.relayPanelDemo.TabIndex = 21;
            this.relayPanelDemo.Visible = false;
            // 
            // relayButtonDemo
            // 
            this.relayButtonDemo.BackColor = System.Drawing.Color.Transparent;
            this.relayButtonDemo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.relayButtonDemo.Font = new System.Drawing.Font("微软雅黑", 7F);
            this.relayButtonDemo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.relayButtonDemo.Image = global::LightController.Properties.Resources.开关关;
            this.relayButtonDemo.ImageDisabled = ((System.Drawing.Image)(resources.GetObject("relayButtonDemo.ImageDisabled")));
            this.relayButtonDemo.ImageHover = global::LightController.Properties.Resources.开关经过;
            this.relayButtonDemo.ImageOffset = new System.Drawing.Point(2, 0);
            this.relayButtonDemo.ImageSelected = global::LightController.Properties.Resources.开关开;
            this.relayButtonDemo.Location = new System.Drawing.Point(19, 13);
            this.relayButtonDemo.Name = "relayButtonDemo";
            this.relayButtonDemo.Size = new System.Drawing.Size(30, 50);
            this.relayButtonDemo.TabIndex = 9;
            this.relayButtonDemo.TabStop = false;
            this.relayButtonDemo.Text = "开关1";
            this.relayButtonDemo.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.relayButtonDemo.WaitOnLoad = true;
            // 
            // relayTextBoxDemo
            // 
            this.relayTextBoxDemo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.relayTextBoxDemo.ButtonSymbol = 61761;
            this.relayTextBoxDemo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.relayTextBoxDemo.FillColor = System.Drawing.Color.White;
            this.relayTextBoxDemo.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.relayTextBoxDemo.Location = new System.Drawing.Point(9, 74);
            this.relayTextBoxDemo.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.relayTextBoxDemo.Maximum = 2147483647D;
            this.relayTextBoxDemo.Minimum = -2147483648D;
            this.relayTextBoxDemo.MinimumSize = new System.Drawing.Size(1, 1);
            this.relayTextBoxDemo.Name = "relayTextBoxDemo";
            this.relayTextBoxDemo.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.relayTextBoxDemo.Radius = 4;
            this.relayTextBoxDemo.Size = new System.Drawing.Size(50, 20);
            this.relayTextBoxDemo.Style = Sunny.UI.UIStyle.Custom;
            this.relayTextBoxDemo.TabIndex = 19;
            this.relayTextBoxDemo.Text = "摇头灯";
            this.relayTextBoxDemo.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.relayTextBoxDemo.Watermark = "";
            // 
            // loadButton
            // 
            this.loadButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.loadButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.loadButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.loadButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.loadButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.loadButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.loadButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.loadButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.loadButton.Location = new System.Drawing.Point(243, 287);
            this.loadButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.loadButton.Name = "loadButton";
            this.loadButton.Radius = 4;
            this.loadButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.loadButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.loadButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.loadButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.loadButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.loadButton.Size = new System.Drawing.Size(60, 20);
            this.loadButton.Style = Sunny.UI.UIStyle.Custom;
            this.loadButton.TabIndex = 42;
            this.loadButton.Text = "打开配置";
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.saveButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.saveButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.saveButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.saveButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.saveButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.saveButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.saveButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.saveButton.Location = new System.Drawing.Point(326, 287);
            this.saveButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Radius = 4;
            this.saveButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.saveButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.saveButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.saveButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.saveButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.saveButton.Size = new System.Drawing.Size(60, 20);
            this.saveButton.Style = Sunny.UI.UIStyle.Custom;
            this.saveButton.TabIndex = 43;
            this.saveButton.Text = "保存配置";
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // readButton
            // 
            this.readButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.readButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.readButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.readButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.readButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.readButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.readButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.readButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.readButton.Location = new System.Drawing.Point(419, 287);
            this.readButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.readButton.Name = "readButton";
            this.readButton.Radius = 4;
            this.readButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.readButton.Size = new System.Drawing.Size(60, 20);
            this.readButton.Style = Sunny.UI.UIStyle.Custom;
            this.readButton.TabIndex = 44;
            this.readButton.Text = "回读配置";
            this.readButton.Click += new System.EventHandler(this.readButton_Click);
            // 
            // openCheckBox
            // 
            this.openCheckBox.CheckBoxColor = System.Drawing.Color.FromArgb(((int)(((byte)(140)))), ((int)(((byte)(140)))), ((int)(((byte)(140)))));
            this.openCheckBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.openCheckBox.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.openCheckBox.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.openCheckBox.Location = new System.Drawing.Point(75, 284);
            this.openCheckBox.MinimumSize = new System.Drawing.Size(1, 1);
            this.openCheckBox.Name = "openCheckBox";
            this.openCheckBox.Padding = new System.Windows.Forms.Padding(22, 0, 0, 0);
            this.openCheckBox.Size = new System.Drawing.Size(87, 27);
            this.openCheckBox.Style = Sunny.UI.UIStyle.Custom;
            this.openCheckBox.TabIndex = 41;
            this.openCheckBox.Text = "启用时序器";
            // 
            // writeButton
            // 
            this.writeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.writeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.writeButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.writeButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.writeButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.writeButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.writeButton.Location = new System.Drawing.Point(504, 287);
            this.writeButton.MinimumSize = new System.Drawing.Size(1, 1);
            this.writeButton.Name = "writeButton";
            this.writeButton.Radius = 4;
            this.writeButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.writeButton.Size = new System.Drawing.Size(60, 20);
            this.writeButton.Style = Sunny.UI.UIStyle.Custom;
            this.writeButton.StyleCustomMode = true;
            this.writeButton.TabIndex = 40;
            this.writeButton.Text = "写入设备";
            this.writeButton.Click += new System.EventHandler(this.writeButton_Click);
            // 
            // uiLabel2
            // 
            this.uiLabel2.AutoSize = true;
            this.uiLabel2.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel2.Location = new System.Drawing.Point(280, 245);
            this.uiLabel2.Name = "uiLabel2";
            this.uiLabel2.Size = new System.Drawing.Size(89, 11);
            this.uiLabel2.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel2.TabIndex = 38;
            this.uiLabel2.Text = "继电器关闭顺序";
            this.uiLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // uiLabel1
            // 
            this.uiLabel1.AutoSize = true;
            this.uiLabel1.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiLabel1.Location = new System.Drawing.Point(280, 19);
            this.uiLabel1.Name = "uiLabel1";
            this.uiLabel1.Size = new System.Drawing.Size(89, 11);
            this.uiLabel1.Style = Sunny.UI.UIStyle.Custom;
            this.uiLabel1.TabIndex = 39;
            this.uiLabel1.Text = "继电器开启顺序";
            this.uiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox2
            // 
            this.pictureBox2.ErrorImage = global::LightController.Properties.Resources.继电器开启顺序;
            this.pictureBox2.Image = global::LightController.Properties.Resources.继电器开启顺序;
            this.pictureBox2.Location = new System.Drawing.Point(171, 229);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(302, 12);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 36;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = global::LightController.Properties.Resources.继电器关闭顺序;
            this.pictureBox1.Image = global::LightController.Properties.Resources.继电器关闭顺序;
            this.pictureBox1.Location = new System.Drawing.Point(173, 35);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 11);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 37;
            this.pictureBox1.TabStop = false;
            // 
            // closeButton
            // 
            this.closeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeButton.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.closeButton.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.closeButton.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.closeButton.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.closeButton.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.closeButton.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.closeButton.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.closeButton.Location = new System.Drawing.Point(593, 117);
            this.closeButton.MinimumSize = new System.Drawing.Size(1, 2);
            this.closeButton.Name = "closeButton";
            this.closeButton.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.closeButton.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.closeButton.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.closeButton.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.closeButton.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.closeButton.Size = new System.Drawing.Size(36, 36);
            this.closeButton.Style = Sunny.UI.UIStyle.Custom;
            this.closeButton.TabIndex = 34;
            this.closeButton.Text = "关台\n模拟";
            this.closeButton.TipsText = "Hello";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // uiButton5
            // 
            this.uiButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.uiButton5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.uiButton5.FillDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.uiButton5.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(69)))), ((int)(((byte)(79)))));
            this.uiButton5.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(37)))), ((int)(((byte)(48)))));
            this.uiButton5.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.uiButton5.Font = new System.Drawing.Font("微软雅黑", 8.25F);
            this.uiButton5.ForeDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(83)))), ((int)(((byte)(91)))));
            this.uiButton5.Location = new System.Drawing.Point(19, 117);
            this.uiButton5.MinimumSize = new System.Drawing.Size(1, 2);
            this.uiButton5.Name = "uiButton5";
            this.uiButton5.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiButton5.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiButton5.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiButton5.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiButton5.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.uiButton5.Size = new System.Drawing.Size(36, 36);
            this.uiButton5.Style = Sunny.UI.UIStyle.Custom;
            this.uiButton5.TabIndex = 35;
            this.uiButton5.Text = "开台\n模拟";
            this.uiButton5.TipsText = "Hello";
            this.uiButton5.Click += new System.EventHandler(this.openButton_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(76)))), ((int)(((byte)(88)))));
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 326);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(648, 1);
            this.panel4.TabIndex = 31;
            // 
            // myStatusStrip
            // 
            this.myStatusStrip.AutoSize = false;
            this.myStatusStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.myStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.myStatusLabel});
            this.myStatusStrip.Location = new System.Drawing.Point(0, 327);
            this.myStatusStrip.Name = "myStatusStrip";
            this.myStatusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 7, 0);
            this.myStatusStrip.ShowItemToolTips = true;
            this.myStatusStrip.Size = new System.Drawing.Size(648, 16);
            this.myStatusStrip.SizingGrip = false;
            this.myStatusStrip.TabIndex = 32;
            // 
            // myStatusLabel
            // 
            this.myStatusLabel.Font = new System.Drawing.Font("微软雅黑", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myStatusLabel.ForeColor = System.Drawing.Color.White;
            this.myStatusLabel.Name = "myStatusLabel";
            this.myStatusLabel.Size = new System.Drawing.Size(125, 11);
            this.myStatusLabel.Text = "成功回读时序器配置。";
            // 
            // labelPanel
            // 
            this.labelPanel.Controls.Add(this.label5);
            this.labelPanel.Location = new System.Drawing.Point(71, 0);
            this.labelPanel.Margin = new System.Windows.Forms.Padding(0);
            this.labelPanel.Name = "labelPanel";
            this.labelPanel.Size = new System.Drawing.Size(50, 30);
            this.labelPanel.TabIndex = 22;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(5, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 11);
            this.label5.TabIndex = 0;
            this.label5.Text = "时延(S)";
            // 
            // myToolTip
            // 
            this.myToolTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.myToolTip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.myToolTip.OwnerDraw = true;
            // 
            // lbinOpenDialog
            // 
            this.lbinOpenDialog.Filter = "lbin配置文件|*.lbin";
            // 
            // lbinSaveDialog
            // 
            this.lbinSaveDialog.Filter = "lbin配置文件|*.lbin";
            // 
            // timeNUDDemo
            // 
            this.timeNUDDemo.Font = new System.Drawing.Font("微软雅黑", 8F);
            this.timeNUDDemo.Location = new System.Drawing.Point(123, 5);
            this.timeNUDDemo.Margin = new System.Windows.Forms.Padding(2, 5, 27, 3);
            this.timeNUDDemo.Maximum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.timeNUDDemo.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timeNUDDemo.Name = "timeNUDDemo";
            this.timeNUDDemo.Size = new System.Drawing.Size(44, 20);
            this.timeNUDDemo.TabIndex = 2;
            this.timeNUDDemo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.timeNUDDemo.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timeNUDDemo.Visible = false;
            // 
            // maskPanel
            // 
            this.maskPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.maskPanel.Location = new System.Drawing.Point(0, 35);
            this.maskPanel.Name = "maskPanel";
            this.maskPanel.Size = new System.Drawing.Size(650, 345);
            this.maskPanel.TabIndex = 1;
            this.maskPanel.Visible = false;
            // 
            // SequencerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ClientSize = new System.Drawing.Size(650, 380);
            this.Controls.Add(this.maskPanel);
            this.Controls.Add(this.mainPanel);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1792, 867);
            this.MinimizeBox = false;
            this.Name = "SequencerForm";
            this.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.ShowRadius = false;
            this.ShowShadow = true;
            this.Style = Sunny.UI.UIStyle.Custom;
            this.Text = "时序器配置";
            this.TitleColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(58)))));
            this.TitleFont = new System.Drawing.Font("微软雅黑", 10F);
            this.Load += new System.EventHandler(this.SequencerForm_Load);
            this.Shown += new System.EventHandler(this.SequencerForm_Shown);
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.relayFLP.ResumeLayout(false);
            this.relayPanelDemo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.relayButtonDemo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.myStatusStrip.ResumeLayout(false);
            this.myStatusStrip.PerformLayout();
            this.labelPanel.ResumeLayout(false);
            this.labelPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timeNUDDemo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mainPanel;
        private Sunny.UI.UIButton loadButton;
        private Sunny.UI.UIButton saveButton;
        private Sunny.UI.UIButton readButton;
        private Sunny.UI.UICheckBox openCheckBox;
        private Sunny.UI.UIButton writeButton;
        private Sunny.UI.UILabel uiLabel2;
        private Sunny.UI.UILabel uiLabel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private Sunny.UI.UIButton closeButton;
        private Sunny.UI.UIButton uiButton5;
        private Sunny.UI.UITextBox relayTextBoxDemo;
        private Sunny.UI.UIImageButton relayButtonDemo;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.StatusStrip myStatusStrip;
        private System.Windows.Forms.ToolStripStatusLabel myStatusLabel;
        private System.Windows.Forms.Panel relayPanelDemo;
        private System.Windows.Forms.FlowLayoutPanel relayFLP;
        private System.Windows.Forms.Panel labelPanel;
        private System.Windows.Forms.Label label5;
        private Sunny.UI.UIToolTip myToolTip;
        private System.Windows.Forms.OpenFileDialog lbinOpenDialog;
        private System.Windows.Forms.SaveFileDialog lbinSaveDialog;
        private System.Windows.Forms.NumericUpDown timeNUDDemo;
        private Ast.Form.MaskPanel maskPanel;
    }
}