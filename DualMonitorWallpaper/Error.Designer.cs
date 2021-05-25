
namespace DualMonitorWallpaper
{
    partial class Error
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Error));
            this.pbFormBorder = new System.Windows.Forms.PictureBox();
            this.pbGotIt = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFormBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGotIt)).BeginInit();
            this.SuspendLayout();
            // 
            // pbFormBorder
            // 
            this.pbFormBorder.BackColor = System.Drawing.Color.Transparent;
            this.pbFormBorder.Location = new System.Drawing.Point(0, 0);
            this.pbFormBorder.Name = "pbFormBorder";
            this.pbFormBorder.Size = new System.Drawing.Size(326, 31);
            this.pbFormBorder.TabIndex = 0;
            this.pbFormBorder.TabStop = false;
            this.pbFormBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFormBorder_MouseDown);
            this.pbFormBorder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbFormBorder_MouseUp);
            // 
            // pbGotIt
            // 
            this.pbGotIt.BackColor = System.Drawing.Color.Transparent;
            this.pbGotIt.Location = new System.Drawing.Point(116, 129);
            this.pbGotIt.Name = "pbGotIt";
            this.pbGotIt.Size = new System.Drawing.Size(86, 23);
            this.pbGotIt.TabIndex = 1;
            this.pbGotIt.TabStop = false;
            this.pbGotIt.Click += new System.EventHandler(this.pbGotIt_Click);
            this.pbGotIt.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbGotIt_MouseDown);
            this.pbGotIt.MouseEnter += new System.EventHandler(this.pbGotIt_MouseEnter);
            this.pbGotIt.MouseLeave += new System.EventHandler(this.pbGotIt_MouseLeave);
            this.pbGotIt.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbGotIt_MouseUp);
            // 
            // Error
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 164);
            this.Controls.Add(this.pbGotIt);
            this.Controls.Add(this.pbFormBorder);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Error";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Error_FormClosing);
            this.Load += new System.EventHandler(this.Error_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFormBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGotIt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbFormBorder;
        private System.Windows.Forms.PictureBox pbGotIt;
    }
}