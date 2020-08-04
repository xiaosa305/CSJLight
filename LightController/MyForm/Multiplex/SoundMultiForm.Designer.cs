namespace LightController.MyForm.Multiplex
{
	partial class SoundMultiForm
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
			this.sqLiteCommand1 = new System.Data.SQLite.SQLiteCommand();
			this.SuspendLayout();
			// 
			// sqLiteCommand1
			// 
			this.sqLiteCommand1.CommandText = null;
			// 
			// SoundMultiForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(928, 498);
			this.Name = "SoundMultiForm";
			this.Text = "多步联调(音频模式)";
			this.Load += new System.EventHandler(this.SoundMultiForm_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Data.SQLite.SQLiteCommand sqLiteCommand1;
	}
}