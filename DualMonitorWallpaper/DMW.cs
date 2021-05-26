using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace DualMonitorWallpaper
{
    public partial class DMW : Form
    {
        #region SetDesktopWallpaper Library Initialization - NOT MY CODE!
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SystemParametersInfo(uint uiAction, uint uiParam, String pvParam, uint fWinIni);

        private const uint SPI_SETDESKWALLPAPER = 0x14;
        private const uint SPIF_UPDATEINIFILE = 0x1;
        private const uint SPIF_SENDWININICHANGE = 0x2;
        #endregion

        #region Variable Declarations
        // Enables easier implementation of mouse events
        PictureBox pbCurrentButton = new PictureBox();

        Point lastLocation;
        Point formLocation;

        Image leftImage;
        Image rightImage;

        bool selectedMonitorLeft;
        bool previewMode;

        int leftWidth;
        int leftHeight;
        int rightWidth;
        int rightHeight;
        int maxWidth;
        int maxHeight;

        double offsetX;
        double offsetY;

        string monitor;
        string wallpaperSavePath;
        #endregion
        
        public DMW()
        {
            InitializeComponent();
            SetGUI();
            RetrieveResolutions();
            SetResolutionLabels();
        }

        #region Form & Control Events
        // Runs when the program is opened
        private void DMW_Load(object sender, EventArgs e)
        {
            this.Location = Properties.Settings.Default.location;
        }

        // Runs when the program is closing
        private void DMW_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.location = this.Location;
            Properties.Settings.Default.Save();
        }

        // Calls the above function
        private void pbExit_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        // This is used when the user is previewing the wallpaper
        private void DMW_KeyDown(object sender, KeyEventArgs e)
        {
            // If it's in preview mode then this will exit it.
            if (e.KeyCode == Keys.Escape)
            {
                if (previewMode)
                {
                    ExitPreview();
                }
            }
        }

        // This function is called whenever the settings or error forms have been closed
        // It validates the monitor resolutions and, if they differ, then the corresponding image is removed to prevent issues
        private void DMW_EnabledChanged(object sender, EventArgs e)
        {
            if (this.Enabled == true)
            {
                if (leftWidth != Properties.Settings.Default.leftWidth)
                {
                    pbLeftMonitor.Image = null;
                }
                if (rightWidth != Properties.Settings.Default.rightWidth)
                {
                    pbRightMonitor.Image = null;
                }

                SetResolutionLabels();
            }
        }

        // Either sets the monitor preview to be active, or exits preview mode
        private void pbLeftMonitor_Click(object sender, EventArgs e)
        {
            if (!previewMode)
            {
                monitor = "left";
                pbBgLeftMonitor.Image = Resources.pbBorder;
                pbBgRightMonitor.Image = null;
                selectedMonitorLeft = true;
            }
            else
            {
                ExitPreview();
            }
        }

        // Same as the above
        private void pbRightMonitor_Click(object sender, EventArgs e)
        {
            if (!previewMode)
            {
                monitor = "right";
                pbBgRightMonitor.Image = Resources.pbBorder;
                pbBgLeftMonitor.Image = null;
                selectedMonitorLeft = false;
            }
            else
            {
                ExitPreview();
            }
        }

        // Runs when the user browses for an image, it opens a file dialog control so that the user can select the image they want
        private void pbBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            // Set the dialog title and set the file type filters.
            dialog.Title = "Please select the image for your " + monitor + " monitor.";
            dialog.Filter = "Image files|*.bmp;*.jpg;*.jpeg;*.png;*.tif;*.tiff";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Creates a bitmap of the selected image and sets values accordingly
                Bitmap img = new Bitmap(dialog.FileName);

                leftImage = img;
                rightImage = img;
                pbImagePreview.Image = img;

                txtImageLocation.Text = dialog.FileName;
            }
        }

        // Runs when the user tries to add the image they have selected
        private void pbAddImage_Click(object sender, EventArgs e)
        {
            RetrieveResolutions();

            if (selectedMonitorLeft && leftImage != null)
            {
                // Ensures that the selected image is the correct size (the same or smaller than the monitors resolution)
                // If it isn't the correct size, then display an error message
                if (leftImage.Width <= leftWidth && leftImage.Height <= leftHeight)
                {
                    pbLeftMonitor.Image = pbImagePreview.Image;
                }
                else
                {
                    Error error = new Error(this, 2, true);
                    error.Show();
                }
            }
            else if (!selectedMonitorLeft && rightImage != null)
            {
                // Same here
                if (rightImage.Width <= rightWidth && rightImage.Height <= rightHeight)
                {
                    pbRightMonitor.Image = pbImagePreview.Image;
                }
                else
                {
                    Error error = new Error(this, 2, false);
                    error.Show();
                }
            }
            // If no image was previously selected, then display an error message
            else if (leftImage == null)
            {
                Error error = new Error(this, 1, true);
                error.Show();
            }
        }

        // Runs when the user removes an image from one of the monitor previews
        private void pbRemoveImage_Click(object sender, EventArgs e)
        {
            if (selectedMonitorLeft && pbLeftMonitor.Image != null)
            {
                pbLeftMonitor.Image = null;
            }
            else if (!selectedMonitorLeft && pbRightMonitor.Image != null)
            {
                pbRightMonitor.Image = null;
            }
        }

        // Runs when the user enters preview mode
        private void pbPreviewWallpaper_Click(object sender, EventArgs e)
        {
            EnterPreview();
        }

        // Runs when the user saves their selected wallpaper
        private void pbSaveWallpaper_Click(object sender, EventArgs e)
        {
            // Check if the images exist, then proceed - if they don't, then display an error message
            if (pbLeftMonitor.Image != null && pbRightMonitor.Image != null)
            {
                SaveWallpaper(true);
            }
            else
            {
                Error error = new Error(this, 4, true);
                error.Show();
            }
        }

        // Runs when the user sets their selected wallpaper
        private void pbSetWallpaper_Click(object sender, EventArgs e)
        {
            // Assume the save was successful, if it isn't then it will be set to false
            bool saveSuccessful = true;

            // Same as the previous function
            if (pbLeftMonitor.Image != null && pbRightMonitor.Image != null)
            {
                SaveWallpaper(false);
            }
            else
            {
                saveSuccessful = false;

                Error error = new Error(this, 4, true);
                error.Show();
            }

            // If the save was successful, set the desktop wallpaper to the selected wallpaper
            if (saveSuccessful)
            {
                SetWallpaper(wallpaperSavePath);
            }
        }

        // Prevents user from selecting text within the textbox.
        private void txtImageLocation_Enter(object sender, EventArgs e)
        {
            ActiveControl = pbBgImagePreview;
        }

        // Runs when the user opens the settings menu
        private void pbSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(this);
            settings.Show();
        }
        #endregion

        #region User-Defined Functions
        // Sets up the initial state of the program
        void SetGUI()
        {
            this.BackgroundImage = Resources.bgMain;
            pbPreviewWallpaper.Image = Resources.pbPreviewWallpaper;
            pbBrowse.Image = Resources.pbBrowse;
            pbAddImage.Image = Resources.pbAddImage;
            pbRemoveImage.Image = Resources.pbRemoveImage;
            pbSaveWallpaper.Image = Resources.pbSaveWallpaper;
            pbSetWallpaper.Image = Resources.pbSetWallpaper;
            pbSettings.Image = Resources.pbSettings;
            pbExit.Image = Resources.pbExit;

            // Initially setting the selected monitor to the left one.
            pbBgLeftMonitor.Image = Resources.pbBorder;
            monitor = "left";
            selectedMonitorLeft = true;
        }

        // Retrieves the saved resolutions and finds the max width to be used when creating the wallpaper
        void RetrieveResolutions()
        {
            leftWidth = Properties.Settings.Default.leftWidth;
            leftHeight = Properties.Settings.Default.leftHeight;
            rightWidth = Properties.Settings.Default.rightWidth;
            rightHeight = Properties.Settings.Default.rightHeight;
            maxWidth = leftWidth + rightWidth;
            if (leftHeight > rightHeight)
            {
                maxHeight = leftHeight;
            }
            else
            {
                maxHeight = rightHeight;
            }
        }

        // Enables you to see a preview of the wallpaper depending on which montior previews have an image.
        void EnterPreview()
        {
            RetrieveResolutions();

            // Essentially just changes the size of the form and pictureboxes so that they mimic desktop wallpaper
            // Setting the form invisible just makes the transition cleaner (this is also done in the ExitPreview procedure)
            // This block of if else statements checks which monitor previews have an image with them, then displays them accordingly
            if (pbLeftMonitor.Image != null && pbRightMonitor.Image != null)
            {
                this.Visible = false;

                formLocation = this.Location;
                this.Location = new Point(0, 0);
                this.Size = new Size(maxWidth, maxHeight);

                pbLeftMonitor.SizeMode = PictureBoxSizeMode.CenterImage;
                pbRightMonitor.SizeMode = PictureBoxSizeMode.CenterImage;

                pbLeftMonitor.Location = new Point(0, 0);
                pbLeftMonitor.Size = new Size(leftWidth, leftHeight);

                pbRightMonitor.Location = new Point(leftWidth, 0);
                pbRightMonitor.Size = new Size(rightWidth, rightHeight);

                pbLeftMonitor.BringToFront();
                pbRightMonitor.BringToFront();

                this.Visible = true;

                previewMode = true;
            }
            else if (pbLeftMonitor.Image != null && pbRightMonitor.Image == null)
            {
                this.Visible = false;

                formLocation = this.Location;
                this.Location = new Point(0, 0);
                this.Size = new Size(leftWidth, leftHeight);

                pbLeftMonitor.SizeMode = PictureBoxSizeMode.CenterImage;

                pbLeftMonitor.Location = new Point(0, 0);
                pbLeftMonitor.Size = new Size(leftWidth, leftHeight);

                pbLeftMonitor.BringToFront();

                this.Visible = true;

                previewMode = true;
            }
            else if (pbLeftMonitor.Image == null && pbRightMonitor.Image != null)
            {
                this.Visible = false;

                formLocation = this.Location;
                this.Location = new Point(leftWidth, 0);
                this.Size = new Size(rightWidth, rightHeight);

                pbRightMonitor.SizeMode = PictureBoxSizeMode.CenterImage;

                pbRightMonitor.Location = new Point(0, 0);
                pbRightMonitor.Size = new Size(rightWidth, rightHeight);

                pbRightMonitor.BringToFront();

                this.Visible = true;

                previewMode = true;
            }
            // If neither monitor preview had an image, then display an error message
            else
            {
                Error error = new Error(this, 3, true);
                error.Show();
            }
        }

        // Puts all the controls back to how they originally were when the preview is exited
        void ExitPreview()
        {
            this.Visible = false;

            pbLeftMonitor.SizeMode = PictureBoxSizeMode.Zoom;
            pbRightMonitor.SizeMode = PictureBoxSizeMode.Zoom;

            pbLeftMonitor.Location = new Point(13, 43);
            pbLeftMonitor.Size = new Size(375, 211);

            pbRightMonitor.Location = new Point(398, 43);
            pbRightMonitor.Size = new Size(375, 211);

            pbLeftMonitor.SendToBack();
            pbRightMonitor.SendToBack();

            pbBgLeftMonitor.SendToBack();
            pbBgRightMonitor.SendToBack();

            this.Size = new Size(786, 460);
            this.Location = formLocation;

            this.Visible = true;

            previewMode = false;
        }

        // Saves the wallpaper to a file in the same directory as the program
        void SaveWallpaper(bool isJustSave)
        {
            RetrieveResolutions();

            leftImage = pbLeftMonitor.Image;
            rightImage = pbRightMonitor.Image;

            Bitmap finalImage = new Bitmap(maxWidth, maxHeight);
            Graphics wallpaper = Graphics.FromImage(finalImage);

            // If the left image size is exactly the same as the left monitor resolution, no alterations need to be made
            if (leftWidth == leftImage.Width && leftHeight == leftImage.Height)
            {
                wallpaper.DrawImage(leftImage,
                             new Rectangle(0, 0, leftWidth, leftHeight),
                             new Rectangle(0, 0, leftWidth, leftHeight),
                             GraphicsUnit.Pixel);
            }
            // If it isn't exactly the same, then centre the image
            else
            {
                // Check for the difference between the monitor resolution and the left image
                // If the difference is even then the image can simply be centred, however, if it isn't, then it must be
                // moved one pixel to the right/down
                offsetX = ((double)leftWidth - (double)leftImage.Width) / 2;
                offsetY = ((double)leftHeight - (double)leftImage.Height) / 2;

                if (!IsOffsetEven(offsetX))
                {
                    offsetX += 0.5;
                }
                if (!IsOffsetEven(offsetY))
                {
                    offsetY += 0.5;
                }

                wallpaper.DrawImage(leftImage,
                                 new Rectangle((int)offsetX, (int)offsetY, leftWidth, leftHeight),
                                 new Rectangle(0, 0, leftWidth, leftHeight),
                                 GraphicsUnit.Pixel);
            }
            // Same here, only for the right image instead
            if (rightWidth == rightImage.Width && rightHeight == rightImage.Height)
            {
                wallpaper.DrawImage(rightImage,
                             new Rectangle(leftWidth, 0, rightWidth, rightHeight),
                             new Rectangle(0, 0, rightWidth, rightHeight),
                             GraphicsUnit.Pixel);
            }
            else
            {
                offsetX = ((double)rightWidth - (double)rightImage.Width) / 2;
                offsetY = ((double)rightHeight - (double)rightImage.Height) / 2;

                if (!IsOffsetEven(offsetX))
                {
                    offsetX += 0.5;
                }
                if (!IsOffsetEven(offsetY))
                {
                    offsetY += 0.5;
                }

                wallpaper.DrawImage(rightImage,
                                 new Rectangle(leftWidth + (int)offsetX, (int)offsetY, rightWidth, rightHeight),
                                 new Rectangle(0, 0, rightWidth, rightHeight),
                                 GraphicsUnit.Pixel);
            }

            wallpaper.Save();

            // If the function was called via the save button, save the file with the 'Saved' prefix, else save it
            // with the 'Set' prefix
            if (isJustSave)
            {
                wallpaperSavePath = Application.StartupPath + "\\SavedWallpaper.bmp";
                finalImage.Save(wallpaperSavePath, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            else
            {
                wallpaperSavePath = Application.StartupPath + "\\SetWallpaper.bmp";
                finalImage.Save(wallpaperSavePath, System.Drawing.Imaging.ImageFormat.Bmp);
            }
        }

        // Parity check to properly allign the image
        bool IsOffsetEven(double num)
        {
            if (num % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Set the pseudo-labels underneath each monitor preview to the appropriate resolution
        void SetResolutionLabels()
        {
            if (Properties.Settings.Default.leftWidth == 1920)
            {
                pbLeftMonitorSize.Image = Resources.pbMonitorSize1920;
            }
            else if (Properties.Settings.Default.leftWidth == 2560)
            {
                pbLeftMonitorSize.Image = Resources.pbMonitorSize2560;
            }
            else
            {
                pbLeftMonitorSize.Image = Resources.pbMonitorSize3840;
            }
            if (Properties.Settings.Default.rightWidth == 1920)
            {
                pbRightMonitorSize.Image = Resources.pbMonitorSize1920;
            }
            else if (Properties.Settings.Default.rightWidth == 2560)
            {
                pbRightMonitorSize.Image = Resources.pbMonitorSize2560;
            }
            else
            {
                pbRightMonitorSize.Image = Resources.pbMonitorSize3840;
            }
        }
        #endregion

        #region Mouse Events
        // These mouse events handle what image the button the mouse is interacting with is set to
        private void pbButtons_MouseEnter(object sender, EventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbPreviewWallpaper":
                    pbPreviewWallpaper.Image = Resources.pbPreviewWallpaperSelected;
                    break;
                case "pbBrowse":
                    pbBrowse.Image = Resources.pbBrowseSelected;
                    break;
                case "pbAddImage":
                    pbAddImage.Image = Resources.pbAddImageSelected;
                    break;
                case "pbRemoveImage":
                    pbRemoveImage.Image = Resources.pbRemoveImageSelected;
                    break;
                case "pbSaveWallpaper":
                    pbSaveWallpaper.Image = Resources.pbSaveWallpaperSelected;
                    break;
                case "pbSetWallpaper":
                    pbSetWallpaper.Image = Resources.pbSetWallpaperSelected;
                    break;
                case "pbSettings":
                    pbSettings.Image = Resources.pbSettingsSelected;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbExitSelected;
                    break;
            }
        }
        private void pbButtons_MouseLeave(object sender, EventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbPreviewWallpaper":
                    pbPreviewWallpaper.Image = Resources.pbPreviewWallpaper;
                    break;
                case "pbBrowse":
                    pbBrowse.Image = Resources.pbBrowse;
                    break;
                case "pbAddImage":
                    pbAddImage.Image = Resources.pbAddImage;
                    break;
                case "pbRemoveImage":
                    pbRemoveImage.Image = Resources.pbRemoveImage;
                    break;
                case "pbSaveWallpaper":
                    pbSaveWallpaper.Image = Resources.pbSaveWallpaper;
                    break;
                case "pbSetWallpaper":
                    pbSetWallpaper.Image = Resources.pbSetWallpaper;
                    break;
                case "pbSettings":
                    pbSettings.Image = Resources.pbSettings;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbExit;
                    break;
            }
        }
        private void pbButtons_MouseDown(object sender, MouseEventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbPreviewWallpaper":
                    pbPreviewWallpaper.Image = Resources.pbPreviewWallpaperClick;
                    break;
                case "pbBrowse":
                    pbBrowse.Image = Resources.pbBrowseClick;
                    break;
                case "pbAddImage":
                    pbAddImage.Image = Resources.pbAddImageClick;
                    break;
                case "pbRemoveImage":
                    pbRemoveImage.Image = Resources.pbRemoveImageClick;
                    break;
                case "pbSaveWallpaper":
                    pbSaveWallpaper.Image = Resources.pbSaveWallpaperClick;
                    break;
                case "pbSetWallpaper":
                    pbSetWallpaper.Image = Resources.pbSetWallpaperClick;
                    break;
                case "pbSettings":
                    pbSettings.Image = Resources.pbSettingsClick;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbExitClick;
                    break;
            }
        }
        private void pbButtons_MouseUp(object sender, MouseEventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbPreviewWallpaper":
                    pbPreviewWallpaper.Image = Resources.pbPreviewWallpaperSelected;
                    break;
                case "pbBrowse":
                    pbBrowse.Image = Resources.pbBrowse;
                    break;
                case "pbAddImage":
                    pbAddImage.Image = Resources.pbAddImageSelected;
                    break;
                case "pbRemoveImage":
                    pbRemoveImage.Image = Resources.pbRemoveImageSelected;
                    break;
                case "pbSaveWallpaper":
                    pbSaveWallpaper.Image = Resources.pbSaveWallpaperSelected;
                    break;
                case "pbSetWallpaper":
                    pbSetWallpaper.Image = Resources.pbSetWallpaperSelected;
                    break;
                case "pbSettings":
                    pbSettings.Image = Resources.pbSettingsSelected;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbExitSelected;
                    break;
            }
        }
        #endregion

        #region Border Events
        // This enables the user to freely move the form
        private void pbFormBorder_MouseDown(object sender, MouseEventArgs e)
        {
            lastLocation = e.Location;
            pbFormBorder.MouseMove += pbFormBorder_MouseMove;
        }
        private void pbFormBorder_MouseUp(object sender, MouseEventArgs e)
        {
            pbFormBorder.MouseMove -= pbFormBorder_MouseMove;
        }
        private void pbFormBorder_MouseMove(object sender, MouseEventArgs e)
        {
            this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
            this.Update();
        }
        #endregion

        #region SetWallpaper Function - NOT MY CODE!
        // Sets the desktop wallpaper to the created wallpaper.
        private void SetWallpaper(string file_name)
        {
            try
            {
                uint flags = 0;

                flags = SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE;

                if (!SystemParametersInfo(SPI_SETDESKWALLPAPER,
                    0, file_name, flags))
                {
                    MessageBox.Show("SystemParametersInfo failed.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error displaying picture " +
                    file_name + ".\n" + ex.Message,
                    "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }
        #endregion
    }
}
