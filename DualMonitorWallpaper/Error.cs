using System;
using System.Drawing;
using System.Windows.Forms;

namespace DualMonitorWallpaper
{
    public partial class Error : Form
    {
        #region Variable Declarations
        // I need to acquire the main form so that I can disable and enable it
        Form mainForm;

        Point lastLocation;

        // Values to position the form directly in the centre of the main form
        int locX = 230;
        int locY = 148;
        #endregion

        // The constructor needs to pass the main form as a parameter, as well as an errorType value code which error message to display
        // and a isLeft bool to check if the monitor preview which gave the error was the left or right one (this is so I can display
        // the correct resolution to the user
        public Error(Form frm, int errorType, bool isLeft)
        {
            InitializeComponent();

            mainForm = frm;
            mainForm.Enabled = false;

            SetGUI(errorType, isLeft);
        }

        #region Form & Control Events
        // Runs when the form is opened
        private void Error_Load(object sender, EventArgs e)
        {
            locX += DMW.ActiveForm.Location.X;
            locY += DMW.ActiveForm.Location.Y;
            this.Location = new Point(locX, locY);
        }

        // Runs when the form is closing
        private void Error_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Enabled = true;
        }

        // Closes the form, triggering the event above
        private void pbGotIt_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }
        #endregion

        #region User-Defined Functions
        // Set the GUI based on the type of error
        void SetGUI(int errorType, bool isLeft)
        {
            if (errorType == 1)
            {
                NoImageSelectedError();
            }
            else if (errorType == 2)
            {
                if (isLeft)
                {
                    ImageSizeError(true);
                }
                else
                {
                    ImageSizeError(false);
                }
            }
            else if (errorType == 3)
            {
                PreviewError();
            }
            else
            {
                SaveSetWallpaperError();
            }

            pbGotIt.Image = Resources.pbGotIt;
        }

        void NoImageSelectedError()
        {
            this.BackgroundImage = Resources.bgErrorNoImageSelected;
        }

        void ImageSizeError(bool isLeft)
        {
            if (isLeft)
            {
                if (Properties.Settings.Default.leftWidth == 1920)
                {
                    this.BackgroundImage = Resources.bgErrorAddImage1920;
                }
                else if (Properties.Settings.Default.leftWidth == 2560)
                {
                    this.BackgroundImage = Resources.bgErrorAddImage2560;
                }
                else
                {
                    this.BackgroundImage = Resources.bgErrorAddImage3840;
                }
            }
            else
            {
                if (Properties.Settings.Default.rightWidth == 1920)
                {
                    this.BackgroundImage = Resources.bgErrorAddImage1920;
                }
                else if (Properties.Settings.Default.rightWidth == 2560)
                {
                    this.BackgroundImage = Resources.bgErrorAddImage2560;
                }
                else
                {
                    this.BackgroundImage = Resources.bgErrorAddImage3840;
                }
            }
        }

        void PreviewError()
        {
            this.BackgroundImage = Resources.bgErrorPreview;
        }

        void SaveSetWallpaperError()
        {
            this.BackgroundImage = Resources.bgErrorSaveSet;
        }
        #endregion

        #region Mouse events
        // I only have a single pseudo-button within this form so I only need that button's events
        private void pbGotIt_MouseEnter(object sender, EventArgs e)
        {
            pbGotIt.Image = Resources.pbGotItSelected;
        }
        private void pbGotIt_MouseLeave(object sender, EventArgs e)
        {
            pbGotIt.Image = Resources.pbGotIt;
        }
        private void pbGotIt_MouseDown(object sender, MouseEventArgs e)
        {
            pbGotIt.Image = Resources.pbGotItClick;
        }
        private void pbGotIt_MouseUp(object sender, MouseEventArgs e)
        {
            pbGotIt.Image = Resources.pbGotItSelected;
        }
        #endregion

        #region Border events
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
    }
}
