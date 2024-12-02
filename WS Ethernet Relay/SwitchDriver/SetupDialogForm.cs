using ASCOM.Utilities;
using System;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ASCOM.Waveshare_Modbus_POE_ETH_Relay.Switch
{
    [ComVisible(false)] // Form not registered for COM!
    partial class SetupDialogForm : Form
    {
        const string NO_PORTS_MESSAGE = "No COM ports found";
        TraceLogger tl; // Holder for a reference to the driver's trace logger

        public SetupDialogForm(TraceLogger tlDriver)
        {
            InitializeComponent();

            // Save the provided trace logger for use within the setup dialogue
            tl = tlDriver;

            //Read saved profile
            SwitchHardware.ReadProfile();

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void CmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here and update the state variables with results from the dialogue

            tl.Enabled = chkTrace.Checked;
            SwitchHardware.debugState = checkBoxDebug.Checked;

            IPAddress ipAddress;
            if (IPAddress.TryParse(textBoxIP.Text, out ipAddress) == false)
            {
                MessageBox.Show("Please enter valid IP Addess in format n.n.n.n", "IP could not be validated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SwitchHardware.ipAddress = (string)textBoxIP.Text;
            try
            {
                SwitchHardware.ipPort = Convert.ToInt32(textBoxPort.Text);
                if (SwitchHardware.ipPort < 1)
                {
                    throw new ArgumentException();
                }

            }
            catch
            {
                MessageBox.Show("Please enter valid Port Number", "Port is invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
            if ( textBoxRelay1.Text == "" ||
                 textBoxRelay2.Text == "" || 
                 textBoxRelay3.Text == "" ||
                 textBoxRelay4.Text == "" ||
                 textBoxRelay5.Text == "" ||
                 textBoxRelay6.Text == "" ||
                 textBoxRelay7.Text == "" ||
                 textBoxRelay8.Text == "" ||
                 textBoxInput1.Text == "" ||
                 textBoxInput2.Text == "" ||
                 textBoxInput3.Text == "" ||
                 textBoxInput4.Text == "" ||
                 textBoxInput5.Text == "" ||
                 textBoxInput6.Text == "" ||
                 textBoxInput7.Text == "" ||
                 textBoxInput8.Text == "" 
                 )
            {
                MessageBox.Show("Please provide a valid name - a relay / switch name must not be empty!", "Relay / Input Name is empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }



            SwitchHardware.deviceType     = Convert.ToInt16(comboDevice.SelectedIndex);
            SwitchHardware.deviceProtocol = Convert.ToInt16(comboProtocol.SelectedIndex);

            SwitchHardware.switchNames[0] = textBoxRelay1.Text;
            SwitchHardware.switchNames[1] = textBoxRelay2.Text;
            SwitchHardware.switchNames[2] = textBoxRelay3.Text;
            SwitchHardware.switchNames[3] = textBoxRelay4.Text;
            SwitchHardware.switchNames[4] = textBoxRelay5.Text;
            SwitchHardware.switchNames[5] = textBoxRelay6.Text;
            SwitchHardware.switchNames[6] = textBoxRelay7.Text;
            SwitchHardware.switchNames[7] = textBoxRelay8.Text;

            SwitchHardware.switchNames[8]  = textBoxInput1.Text;
            SwitchHardware.switchNames[9]  = textBoxInput2.Text;
            SwitchHardware.switchNames[10] = textBoxInput3.Text;
            SwitchHardware.switchNames[11] = textBoxInput4.Text;
            SwitchHardware.switchNames[12] = textBoxInput5.Text;
            SwitchHardware.switchNames[13] = textBoxInput6.Text;
            SwitchHardware.switchNames[14] = textBoxInput7.Text;
            SwitchHardware.switchNames[15] = textBoxInput8.Text;

            SwitchHardware.switchVisible[0] = checkBoxRelay1Visible.Checked;
            SwitchHardware.switchVisible[1] = checkBoxRelay2Visible.Checked;
            SwitchHardware.switchVisible[2] = checkBoxRelay3Visible.Checked;
            SwitchHardware.switchVisible[3] = checkBoxRelay4Visible.Checked;
            SwitchHardware.switchVisible[4] = checkBoxRelay5Visible.Checked;
            SwitchHardware.switchVisible[5] = checkBoxRelay6Visible.Checked;
            SwitchHardware.switchVisible[6] = checkBoxRelay7Visible.Checked;
            SwitchHardware.switchVisible[7] = checkBoxRelay8Visible.Checked;
            
            SwitchHardware.switchVisible[8]  = checkBoxInput1Visible.Checked;
            SwitchHardware.switchVisible[9]  = checkBoxInput2Visible.Checked;
            SwitchHardware.switchVisible[10] = checkBoxInput3Visible.Checked;
            SwitchHardware.switchVisible[11] = checkBoxInput4Visible.Checked;
            SwitchHardware.switchVisible[12] = checkBoxInput5Visible.Checked;
            SwitchHardware.switchVisible[13] = checkBoxInput6Visible.Checked;
            SwitchHardware.switchVisible[14] = checkBoxInput7Visible.Checked;
            SwitchHardware.switchVisible[15] = checkBoxInput8Visible.Checked;


            SwitchHardware.switchModes[0] = Convert.ToInt16(comboInput1Mode.SelectedIndex);
            SwitchHardware.switchModes[1] = Convert.ToInt16(comboInput2Mode.SelectedIndex);
            SwitchHardware.switchModes[2] = Convert.ToInt16(comboInput3Mode.SelectedIndex);
            SwitchHardware.switchModes[3] = Convert.ToInt16(comboInput4Mode.SelectedIndex);
            SwitchHardware.switchModes[4] = Convert.ToInt16(comboInput5Mode.SelectedIndex);
            SwitchHardware.switchModes[5] = Convert.ToInt16(comboInput6Mode.SelectedIndex);
            SwitchHardware.switchModes[6] = Convert.ToInt16(comboInput7Mode.SelectedIndex);
            SwitchHardware.switchModes[7] = Convert.ToInt16(comboInput8Mode.SelectedIndex);

            SwitchHardware.switchToggle[0] = checkBoxRelay1Toggle.Checked;
            SwitchHardware.switchToggle[1] = checkBoxRelay2Toggle.Checked;
            SwitchHardware.switchToggle[2] = checkBoxRelay3Toggle.Checked;
            SwitchHardware.switchToggle[3] = checkBoxRelay4Toggle.Checked;
            SwitchHardware.switchToggle[4] = checkBoxRelay5Toggle.Checked;
            SwitchHardware.switchToggle[5] = checkBoxRelay6Toggle.Checked;
            SwitchHardware.switchToggle[6] = checkBoxRelay7Toggle.Checked;
            SwitchHardware.switchToggle[7] = checkBoxRelay8Toggle.Checked;


            tl.LogMessage("Setup OK", $"New configuration values - IP: {textBoxIP.Text} Port: {textBoxPort.Text}");

            this.DialogResult = DialogResult.OK;
        }

        private void CmdCancel_Click(object sender, EventArgs e) // Cancel button event handler
        {
            Close();
        }

        private void BrowseToAscom(object sender, EventArgs e) // Click on ASCOM logo event handler
        {
            try
            {
                System.Diagnostics.Process.Start("https://ascom-standards.org/");
            }
            catch (Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    MessageBox.Show(noBrowser.Message);
            }
            catch (Exception other)
            {
                MessageBox.Show(other.Message);
            }
        }

        private void InitUI()
        {

            // Set the trace checkbox
            chkTrace.Checked = tl.Enabled;

            checkBoxDebug.Checked = SwitchHardware.debugState;


            textBoxIP.Text = SwitchHardware.ipAddress;
            textBoxPort.Text = Convert.ToString(SwitchHardware.ipPort);

            textBoxRelay1.Text = SwitchHardware.switchNames[0];
            textBoxRelay2.Text = SwitchHardware.switchNames[1];
            textBoxRelay3.Text = SwitchHardware.switchNames[2];
            textBoxRelay4.Text = SwitchHardware.switchNames[3];
            textBoxRelay5.Text = SwitchHardware.switchNames[4];
            textBoxRelay6.Text = SwitchHardware.switchNames[5];
            textBoxRelay7.Text = SwitchHardware.switchNames[6];
            textBoxRelay8.Text = SwitchHardware.switchNames[7];

            textBoxInput1.Text = SwitchHardware.switchNames[8];
            textBoxInput2.Text = SwitchHardware.switchNames[9];
            textBoxInput3.Text = SwitchHardware.switchNames[10];
            textBoxInput4.Text = SwitchHardware.switchNames[11];
            textBoxInput5.Text = SwitchHardware.switchNames[12];
            textBoxInput6.Text = SwitchHardware.switchNames[13];
            textBoxInput7.Text = SwitchHardware.switchNames[14];
            textBoxInput8.Text = SwitchHardware.switchNames[15];

            checkBoxRelay1Toggle.Checked = SwitchHardware.switchToggle[0];
            checkBoxRelay2Toggle.Checked = SwitchHardware.switchToggle[1];
            checkBoxRelay3Toggle.Checked = SwitchHardware.switchToggle[2];
            checkBoxRelay4Toggle.Checked = SwitchHardware.switchToggle[3];
            checkBoxRelay5Toggle.Checked = SwitchHardware.switchToggle[4];
            checkBoxRelay6Toggle.Checked = SwitchHardware.switchToggle[5];
            checkBoxRelay7Toggle.Checked = SwitchHardware.switchToggle[6];
            checkBoxRelay8Toggle.Checked = SwitchHardware.switchToggle[7];

            checkBoxRelay1Visible.Checked = SwitchHardware.switchVisible[0];
            checkBoxRelay2Visible.Checked = SwitchHardware.switchVisible[1];
            checkBoxRelay3Visible.Checked = SwitchHardware.switchVisible[2];
            checkBoxRelay4Visible.Checked = SwitchHardware.switchVisible[3];
            checkBoxRelay5Visible.Checked = SwitchHardware.switchVisible[4];
            checkBoxRelay6Visible.Checked = SwitchHardware.switchVisible[5];
            checkBoxRelay7Visible.Checked = SwitchHardware.switchVisible[6];
            checkBoxRelay8Visible.Checked = SwitchHardware.switchVisible[7];

            checkBoxInput1Visible.Checked = SwitchHardware.switchVisible[8];
            checkBoxInput2Visible.Checked = SwitchHardware.switchVisible[9];
            checkBoxInput3Visible.Checked = SwitchHardware.switchVisible[10];
            checkBoxInput4Visible.Checked = SwitchHardware.switchVisible[11];
            checkBoxInput5Visible.Checked = SwitchHardware.switchVisible[12];
            checkBoxInput6Visible.Checked = SwitchHardware.switchVisible[13];
            checkBoxInput7Visible.Checked = SwitchHardware.switchVisible[14];
            checkBoxInput8Visible.Checked = SwitchHardware.switchVisible[15];


            comboDevice.SelectedIndex = SwitchHardware.deviceType;
            comboProtocol.SelectedIndex = SwitchHardware.deviceProtocol;

            comboInput1Mode.SelectedIndex = SwitchHardware.switchModes[0];
            comboInput2Mode.SelectedIndex = SwitchHardware.switchModes[1];
            comboInput3Mode.SelectedIndex = SwitchHardware.switchModes[2];
            comboInput4Mode.SelectedIndex = SwitchHardware.switchModes[3];
            comboInput5Mode.SelectedIndex = SwitchHardware.switchModes[4];
            comboInput6Mode.SelectedIndex = SwitchHardware.switchModes[5];
            comboInput7Mode.SelectedIndex = SwitchHardware.switchModes[6];
            comboInput8Mode.SelectedIndex = SwitchHardware.switchModes[7];


            tl.LogMessage("InitUI", $"Set UI controls to Trace: {chkTrace.Checked}, IP: {textBoxIP.Text}, Port: {textBoxPort.Text}");
        }

        private void SetupDialogForm_Load(object sender, EventArgs e)
        {
            // Bring the setup dialogue to the front of the screen
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            else
            {
                TopMost = true;
                Focus();
                BringToFront();
                TopMost = false;
            }
        }


        private void textBoxPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                VisitLink();
            }
            catch
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        private void VisitLink()
        {
            linkURL.LinkVisited = true;
            System.Diagnostics.Process.Start(linkURL.Text);
        }

        private void comboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDevice.SelectedIndex == SwitchHardware.DeviceTypeETH8CHB)  {
                panelInput.Enabled = true;
            } else
            {
                panelInput.Enabled = false;
            }

            SwitchToggle();

        }

        private void comboInput1Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }

        private void comboInput2Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }

        private void comboInput3Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }

        private void comboInput4Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }

        private void comboInput5Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }

        private void comboInput6Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }

        private void comboInput7Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }

        private void comboInput8Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            SwitchToggle();
        }


        private void SwitchToggle( )
        {

            if (comboDevice.SelectedIndex != SwitchHardware.DeviceTypeETH8CHB)
            {
                checkBoxRelay1Toggle.Enabled = true;
                checkBoxRelay2Toggle.Enabled = true;
                checkBoxRelay3Toggle.Enabled = true;
                checkBoxRelay4Toggle.Enabled = true;
                checkBoxRelay5Toggle.Enabled = true;
                checkBoxRelay6Toggle.Enabled = true;
                checkBoxRelay7Toggle.Enabled = true;
                checkBoxRelay8Toggle.Enabled = true;
                return;

            }


            if (comboInput1Mode.SelectedIndex > 0)
            {
                checkBoxRelay3Toggle.Enabled = false;
                checkBoxRelay3Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay3Toggle.Enabled = true;
                checkBoxRelay3Toggle.Checked = SwitchHardware.switchToggle[2];
            }
            if (comboInput2Mode.SelectedIndex > 0)
            {
                checkBoxRelay2Toggle.Enabled = false;
                checkBoxRelay2Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay2Toggle.Enabled = true;
                checkBoxRelay2Toggle.Checked = SwitchHardware.switchToggle[1];
            }
            if (comboInput3Mode.SelectedIndex > 0)
            {
                checkBoxRelay3Toggle.Enabled = false;
                checkBoxRelay3Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay3Toggle.Enabled = true;
                checkBoxRelay3Toggle.Checked = SwitchHardware.switchToggle[2];
            }
            if (comboInput4Mode.SelectedIndex > 0)
            {
                checkBoxRelay4Toggle.Enabled = false;
                checkBoxRelay4Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay4Toggle.Enabled = true;
                checkBoxRelay4Toggle.Checked = SwitchHardware.switchToggle[3];
            }
            if (comboInput5Mode.SelectedIndex > 0)
            {
                checkBoxRelay5Toggle.Enabled = false;
                checkBoxRelay5Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay5Toggle.Enabled = true;
                checkBoxRelay5Toggle.Checked = SwitchHardware.switchToggle[4];
            }
            if (comboInput6Mode.SelectedIndex > 0)
            {
                checkBoxRelay6Toggle.Enabled = false;
                checkBoxRelay6Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay6Toggle.Enabled = true;
                checkBoxRelay6Toggle.Checked = SwitchHardware.switchToggle[5];
            }
            if (comboInput7Mode.SelectedIndex > 0)
            {
                checkBoxRelay7Toggle.Enabled = false;
                checkBoxRelay7Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay7Toggle.Enabled = true;
                checkBoxRelay7Toggle.Checked = SwitchHardware.switchToggle[6];
            }
            if (comboInput8Mode.SelectedIndex > 0)
            {
                checkBoxRelay8Toggle.Enabled = false;
                checkBoxRelay8Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay8Toggle.Enabled = true;
                checkBoxRelay8Toggle.Checked = SwitchHardware.switchToggle[7];
            }

            if (comboInput8Mode.SelectedIndex > 0)
            {
                checkBoxRelay8Toggle.Enabled = false;
                checkBoxRelay8Toggle.Checked = false;

            }
            else
            {
                checkBoxRelay8Toggle.Enabled = true;
                checkBoxRelay8Toggle.Checked = SwitchHardware.switchToggle[7];
            }

        }


    }

}