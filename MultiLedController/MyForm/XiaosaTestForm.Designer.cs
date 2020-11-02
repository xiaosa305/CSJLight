namespace MultiLedController.MyForm
{
    partial class XiaosaTestForm
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
            this.Start = new System.Windows.Forms.Button();
            this.Stop = new System.Windows.Forms.Button();
            this.StartRecord = new System.Windows.Forms.Button();
            this.StopDebug = new System.Windows.Forms.Button();
            this.StartDebug = new System.Windows.Forms.Button();
            this.StopRecord = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.debugcount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.recordcount = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(84, 45);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(75, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "开始接收";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // Stop
            // 
            this.Stop.Location = new System.Drawing.Point(84, 110);
            this.Stop.Name = "Stop";
            this.Stop.Size = new System.Drawing.Size(75, 23);
            this.Stop.TabIndex = 1;
            this.Stop.Text = "停止接收";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.Click += new System.EventHandler(this.Stop_Click);
            // 
            // StartRecord
            // 
            this.StartRecord.Location = new System.Drawing.Point(84, 320);
            this.StartRecord.Name = "StartRecord";
            this.StartRecord.Size = new System.Drawing.Size(75, 23);
            this.StartRecord.TabIndex = 2;
            this.StartRecord.Text = "开始录制";
            this.StartRecord.UseVisualStyleBackColor = true;
            this.StartRecord.Click += new System.EventHandler(this.StartRecord_Click);
            // 
            // StopDebug
            // 
            this.StopDebug.Location = new System.Drawing.Point(84, 255);
            this.StopDebug.Name = "StopDebug";
            this.StopDebug.Size = new System.Drawing.Size(75, 23);
            this.StopDebug.TabIndex = 3;
            this.StopDebug.Text = "停止调试";
            this.StopDebug.UseVisualStyleBackColor = true;
            this.StopDebug.Click += new System.EventHandler(this.StopDebug_Click);
            // 
            // StartDebug
            // 
            this.StartDebug.Location = new System.Drawing.Point(84, 182);
            this.StartDebug.Name = "StartDebug";
            this.StartDebug.Size = new System.Drawing.Size(75, 23);
            this.StartDebug.TabIndex = 4;
            this.StartDebug.Text = "开始调试";
            this.StartDebug.UseVisualStyleBackColor = true;
            this.StartDebug.Click += new System.EventHandler(this.StartDebug_Click);
            // 
            // StopRecord
            // 
            this.StopRecord.Location = new System.Drawing.Point(84, 383);
            this.StopRecord.Name = "StopRecord";
            this.StopRecord.Size = new System.Drawing.Size(75, 23);
            this.StopRecord.TabIndex = 5;
            this.StopRecord.Text = "停止录制";
            this.StopRecord.UseVisualStyleBackColor = true;
            this.StopRecord.Click += new System.EventHandler(this.StopRecord_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(479, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "调试帧数：";
            // 
            // debugcount
            // 
            this.debugcount.AutoSize = true;
            this.debugcount.Location = new System.Drawing.Point(555, 56);
            this.debugcount.Name = "debugcount";
            this.debugcount.Size = new System.Drawing.Size(11, 12);
            this.debugcount.TabIndex = 7;
            this.debugcount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 115);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "录制帧数：";
            // 
            // recordcount
            // 
            this.recordcount.AutoSize = true;
            this.recordcount.Location = new System.Drawing.Point(555, 115);
            this.recordcount.Name = "recordcount";
            this.recordcount.Size = new System.Drawing.Size(11, 12);
            this.recordcount.TabIndex = 9;
            this.recordcount.Text = "0";
            // 
            // XiaosaTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.recordcount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.debugcount);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.StopRecord);
            this.Controls.Add(this.StartDebug);
            this.Controls.Add(this.StopDebug);
            this.Controls.Add(this.StartRecord);
            this.Controls.Add(this.Stop);
            this.Controls.Add(this.Start);
            this.Name = "XiaosaTestForm";
            this.Text = "XiaosaTestForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Stop;
        private System.Windows.Forms.Button StartRecord;
        private System.Windows.Forms.Button StopDebug;
        private System.Windows.Forms.Button StartDebug;
        private System.Windows.Forms.Button StopRecord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label debugcount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label recordcount;
    }
}