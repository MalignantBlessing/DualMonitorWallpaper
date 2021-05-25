
namespace DualMonitorWallpaper
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.cbLeftWidth = new System.Windows.Forms.ComboBox();
            this.cbLeftHeight = new System.Windows.Forms.ComboBox();
            this.cbRightWidth = new System.Windows.Forms.ComboBox();
            this.cbRightHeight = new System.Windows.Forms.ComboBox();
            this.pbFormBorder = new System.Windows.Forms.PictureBox();
            this.pbSave = new System.Windows.Forms.PictureBox();
            this.pbExit = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbFormBorder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).BeginInit();
            this.SuspendLayout();
            // 
            // cbLeftWidth
            // 
            this.cbLeftWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLeftWidth.FormattingEnabled = true;
            this.cbLeftWidth.Items.AddRange(new object[] {
            "1920",
            "2560",
            "3840"});
            this.cbLeftWidth.Location = new System.Drawing.Point(31, 74);
            this.cbLeftWidth.Name = "cbLeftWidth";
            this.cbLeftWidth.Size = new System.Drawing.Size(76, 23);
            this.cbLeftWidth.TabIndex = 0;
            this.cbLeftWidth.SelectedIndexChanged += new System.EventHandler(this.cbLeftWidth_SelectedIndexChanged);
            // 
            // cbLeftHeight
            // 
            this.cbLeftHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLeftHeight.FormattingEnabled = true;
            this.cbLeftHeight.Items.AddRange(new object[] {
            "1080",
            "1440",
            "2160"});
            this.cbLeftHeight.Location = new System.Drawing.Point(31, 121);
            this.cbLeftHeight.Name = "cbLeftHeight";
            this.cbLeftHeight.Size = new System.Drawing.Size(76, 23);
            this.cbLeftHeight.TabIndex = 1;
            this.cbLeftHeight.SelectedIndexChanged += new System.EventHandler(this.cbLeftHeight_SelectedIndexChanged);
            // 
            // cbRightWidth
            // 
            this.cbRightWidth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRightWidth.FormattingEnabled = true;
            this.cbRightWidth.Items.AddRange(new object[] {
            "1920",
            "2560",
            "3840"});
            this.cbRightWidth.Location = new System.Drawing.Point(139, 74);
            this.cbRightWidth.Name = "cbRightWidth";
            this.cbRightWidth.Size = new System.Drawing.Size(76, 23);
            this.cbRightWidth.TabIndex = 2;
            this.cbRightWidth.SelectedIndexChanged += new System.EventHandler(this.cbRightWidth_SelectedIndexChanged);
            // 
            // cbRightHeight
            // 
            this.cbRightHeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRightHeight.FormattingEnabled = true;
            this.cbRightHeight.Items.AddRange(new object[] {
            "1080",
            "1440",
            "2160"});
            this.cbRightHeight.Location = new System.Drawing.Point(139, 121);
            this.cbRightHeight.Name = "cbRightHeight";
            this.cbRightHeight.Size = new System.Drawing.Size(76, 23);
            this.cbRightHeight.TabIndex = 3;
            this.cbRightHeight.SelectedIndexChanged += new System.EventHandler(this.cbRightHeight_SelectedIndexChanged);
            // 
            // pbFormBorder
            // 
            this.pbFormBorder.BackColor = System.Drawing.Color.Transparent;
            this.pbFormBorder.Location = new System.Drawing.Point(0, 0);
            this.pbFormBorder.Name = "pbFormBorder";
            this.pbFormBorder.Size = new System.Drawing.Size(246, 31);
            this.pbFormBorder.TabIndex = 4;
            this.pbFormBorder.TabStop = false;
            this.pbFormBorder.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbFormBorder_MouseDown);
            this.pbFormBorder.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbFormBorder_MouseUp);
            // 
            // pbSave
            // 
            this.pbSave.BackColor = System.Drawing.Color.Transparent;
            this.pbSave.Location = new System.Drawing.Point(31, 174);
            this.pbSave.Name = "pbSave";
            this.pbSave.Size = new System.Drawing.Size(76, 23);
            this.pbSave.TabIndex = 5;
            this.pbSave.TabStop = false;
            this.pbSave.Click += new System.EventHandler(this.pbSave_Click);
            this.pbSave.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbButtons_MouseDown);
            this.pbSave.MouseEnter += new System.EventHandler(this.pbButtons_MouseEnter);
            this.pbSave.MouseLeave += new System.EventHandler(this.pbButtons_MouseLeave);
            this.pbSave.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbButtons_MouseUp);
            // 
            // pbExit
            // 
            this.pbExit.BackColor = System.Drawing.Color.Transparent;
            this.pbExit.Location = new System.Drawing.Point(139, 174);
            this.pbExit.Name = "pbExit";
            this.pbExit.Size = new System.Drawing.Size(76, 23);
            this.pbExit.TabIndex = 6;
            this.pbExit.TabStop = false;
            this.pbExit.Click += new System.EventHandler(this.pbExit_Click);
            this.pbExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pbButtons_MouseDown);
            this.pbExit.MouseEnter += new System.EventHandler(this.pbButtons_MouseEnter);
            this.pbExit.MouseLeave += new System.EventHandler(this.pbButtons_MouseLeave);
            this.pbExit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pbButtons_MouseUp);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 210);
            this.Controls.Add(this.pbExit);
            this.Controls.Add(this.pbSave);
            this.Controls.Add(this.pbFormBorder);
            this.Controls.Add(this.cbRightHeight);
            this.Controls.Add(this.cbRightWidth);
            this.Controls.Add(this.cbLeftHeight);
            this.Controls.Add(this.cbLeftWidth);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.Load += new System.EventHandler(this.Settings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbFormBorder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbExit)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLeftWidth;
        private System.Windows.Forms.ComboBox cbLeftHeight;
        private System.Windows.Forms.ComboBox cbRightWidth;
        private System.Windows.Forms.ComboBox cbRightHeight;
        private System.Windows.Forms.PictureBox pbFormBorder;
        private System.Windows.Forms.PictureBox pbSave;
        private System.Windows.Forms.PictureBox pbExit;
    }
}