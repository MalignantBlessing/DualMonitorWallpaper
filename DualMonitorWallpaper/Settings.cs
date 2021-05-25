using System;
using System.Drawing;
using System.Windows.Forms;

namespace DualMonitorWallpaper
{
    public partial class Settings : Form
    {
        #region Variable Declarations
        // I need to acquire the main form so that I can disable and enable it
        Form mainForm;

        PictureBox pbCurrentButton = new PictureBox();

        Point lastLocation;

        // Values to position the form directly in the centre of the main form
        int locX = 270;
        int locY = 125;
        #endregion

        // The constructor needs to pass the main form as a parameter
        public Settings(Form frm)
        {
            InitializeComponent();

            mainForm = frm;
            mainForm.Enabled = false;

            SetGUI();
            SetComboBoxValues();
        }

        #region Form & Control Events
        // Runs when the form is opened
        private void Settings_Load(object sender, EventArgs e)
        {
            locX += DMW.ActiveForm.Location.X;
            locY += DMW.ActiveForm.Location.Y;
            this.Location = new Point(locX, locY);
        }

        // Runs when the form is closing
        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainForm.Enabled = true;
        }

        // Saves the user's settings, then closes the form
        private void pbSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.leftWidth = int.Parse(cbLeftWidth.SelectedItem.ToString());
            Properties.Settings.Default.leftHeight = int.Parse(cbLeftHeight.SelectedItem.ToString());
            Properties.Settings.Default.rightWidth = int.Parse(cbRightWidth.SelectedItem.ToString());
            Properties.Settings.Default.rightHeight = int.Parse(cbRightHeight.SelectedItem.ToString());
            Properties.Settings.Default.Save();

            ActiveForm.Close();
        }

        // Closes the form, triggering the closing event
        private void pbExit_Click(object sender, EventArgs e)
        {
            ActiveForm.Close();
        }

        // The following events essentially link the height and width, meaning that the user is constrained
        // to a 16:9 aspect ratio (this just makes it easier for me and less prone to errors)
        private void cbLeftWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLeftWidth.SelectedIndex)
            {
                case 0:
                    cbLeftHeight.SelectedIndex = 0;
                    break;
                case 1:
                    cbLeftHeight.SelectedIndex = 1;
                    break;
                case 2:
                    cbLeftHeight.SelectedIndex = 2;
                    break;
            }
        }
        private void cbLeftHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbLeftHeight.SelectedIndex)
            {
                case 0:
                    cbLeftWidth.SelectedIndex = 0;
                    break;
                case 1:
                    cbLeftWidth.SelectedIndex = 1;
                    break;
                case 2:
                    cbLeftWidth.SelectedIndex = 2;
                    break;
            }
        }
        private void cbRightWidth_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbRightWidth.SelectedIndex)
            {
                case 0:
                    cbRightHeight.SelectedIndex = 0;
                    break;
                case 1:
                    cbRightHeight.SelectedIndex = 1;
                    break;
                case 2:
                    cbRightHeight.SelectedIndex = 2;
                    break;
            }
        }
        private void cbRightHeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbRightHeight.SelectedIndex)
            {
                case 0:
                    cbRightWidth.SelectedIndex = 0;
                    break;
                case 1:
                    cbRightWidth.SelectedIndex = 1;
                    break;
                case 2:
                    cbRightWidth.SelectedIndex = 2;
                    break;
            }
        }
        #endregion

        #region User-Defined Functions
        // Sets up the initial state of the form
        void SetGUI()
        {
            this.BackgroundImage = Resources.bgSettings;
            pbSave.Image = Resources.pbSettingsSave;
            pbExit.Image = Resources.pbSettingsExit;

            ActiveControl = pbFormBorder;
        }

        // Sets the combo box values to be the same as the saved values
        void SetComboBoxValues()
        {
            switch (Properties.Settings.Default.leftWidth)
            {
                case 1920:
                    cbLeftWidth.SelectedIndex = 0;
                    break;
                case 2560:
                    cbLeftWidth.SelectedIndex = 1;
                    break;
                case 3840:
                    cbLeftWidth.SelectedIndex = 2;
                    break;
            }
            switch (Properties.Settings.Default.leftHeight)
            {
                case 1080:
                    cbLeftHeight.SelectedIndex = 0;
                    break;
                case 1440:
                    cbLeftHeight.SelectedIndex = 1;
                    break;
                case 2160:
                    cbLeftHeight.SelectedIndex = 2;
                    break;
            }
            switch (Properties.Settings.Default.rightWidth)
            {
                case 1920:
                    cbRightWidth.SelectedIndex = 0;
                    break;
                case 2560:
                    cbRightWidth.SelectedIndex = 1;
                    break;
                case 3840:
                    cbRightWidth.SelectedIndex = 2;
                    break;
            }
            switch (Properties.Settings.Default.rightHeight)
            {
                case 1080:
                    cbRightHeight.SelectedIndex = 0;
                    break;
                case 1440:
                    cbRightHeight.SelectedIndex = 1;
                    break;
                case 2160:
                    cbRightHeight.SelectedIndex = 2;
                    break;
            }
        }
        #endregion

        #region Mouse events
        // These mouse events handle what image the button the mouse is interacting with is set to
        private void pbButtons_MouseEnter(object sender, EventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbSave":
                    pbSave.Image = Resources.pbSettingsSaveSelected;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbSettingsExitSelected;
                    break;
            }
        }
        private void pbButtons_MouseLeave(object sender, EventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbSave":
                    pbSave.Image = Resources.pbSettingsSave;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbSettingsExit;
                    break;
            }
        }
        private void pbButtons_MouseDown(object sender, MouseEventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbSave":
                    pbSave.Image = Resources.pbSettingsSaveClick;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbSettingsExitClick;
                    break;
            }
        }
        private void pbButtons_MouseUp(object sender, MouseEventArgs e)
        {
            pbCurrentButton = (PictureBox)sender;

            switch (pbCurrentButton.Name.ToString())
            {
                case "pbSave":
                    pbSave.Image = Resources.pbSettingsSaveSelected;
                    break;
                case "pbExit":
                    pbExit.Image = Resources.pbSettingsExitSelected;
                    break;
            }
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