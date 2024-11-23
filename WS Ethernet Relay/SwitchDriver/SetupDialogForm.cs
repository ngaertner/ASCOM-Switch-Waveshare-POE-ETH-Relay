using ASCOM.Utilities;
using System;
using System.ComponentModel;
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

            // Initialise current values of user settings from the ASCOM Profile
            InitUI();
        }

        private void CmdOK_Click(object sender, EventArgs e) // OK button event handler
        {
            // Place any validation constraint checks here and update the state variables with results from the dialogue

            tl.Enabled = chkTrace.Checked;
            SwitchHardware.debugState = checkBoxDebug.Checked;

            // Update the COM port variable if one has been selected
            /*        if (comboBoxComPort.SelectedItem is null) // No COM port selected
                    {
                        tl.LogMessage("Setup OK", $"New configuration values - COM Port: Not selected");
                    }
                    else if (comboBoxComPort.SelectedItem.ToString() == NO_PORTS_MESSAGE)
                    {
                        tl.LogMessage("Setup OK", $"New configuration values - NO COM ports detected on this PC.");
                    }
                    else // A valid COM port has been selected
                    {
            */


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
            catch (Exception ex)
            {
                MessageBox.Show("Please enter valid Port Number", "Port is invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            SwitchHardware.deviceType     = comboDevice.SelectedIndex;
            SwitchHardware.deviceProtocol = comboProtocol.SelectedIndex;

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


            SwitchHardware.switchModes[0] = comboInput1Mode.SelectedIndex;
            SwitchHardware.switchModes[1] = comboInput2Mode.SelectedIndex;
            SwitchHardware.switchModes[2] = comboInput3Mode.SelectedIndex;
            SwitchHardware.switchModes[3] = comboInput4Mode.SelectedIndex;
            SwitchHardware.switchModes[4] = comboInput5Mode.SelectedIndex;
            SwitchHardware.switchModes[5] = comboInput6Mode.SelectedIndex;
            SwitchHardware.switchModes[6] = comboInput7Mode.SelectedIndex;
            SwitchHardware.switchModes[7] = comboInput8Mode.SelectedIndex;

            SwitchHardware.switchToggle[0] = checkBoxRelay1Toggle.Checked;
            SwitchHardware.switchToggle[1] = checkBoxRelay2Toggle.Checked;
            SwitchHardware.switchToggle[2] = checkBoxRelay3Toggle.Checked;
            SwitchHardware.switchToggle[3] = checkBoxRelay4Toggle.Checked;
            SwitchHardware.switchToggle[4] = checkBoxRelay5Toggle.Checked;
            SwitchHardware.switchToggle[5] = checkBoxRelay6Toggle.Checked;
            SwitchHardware.switchToggle[6] = checkBoxRelay7Toggle.Checked;
            SwitchHardware.switchToggle[7] = checkBoxRelay8Toggle.Checked;


            //            SwitchHardware.comPort   = (string)comboBoxComPort.SelectedItem;
            tl.LogMessage("Setup OK", $"New configuration values - IP: {textBoxIP.Text} Port: {textBoxPort.Text}");
            //            tl.LogMessage("Setup OK", $"New configuration values - COM Port: {comboBoxComPort.SelectedItem}");
            //       }

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

            /*
            // set the list of COM ports to those that are currently available
            comboBoxComPort.Items.Clear(); // Clear any existing entries
            using (Serial serial = new Serial()) // User the Se5rial component to get an extended list of COM ports
            {
                comboBoxComPort.Items.AddRange(serial.AvailableCOMPorts);
            }

            // If no ports are found include a message to this effect
            if (comboBoxComPort.Items.Count == 0)
            {
                comboBoxComPort.Items.Add(NO_PORTS_MESSAGE);
                comboBoxComPort.SelectedItem = NO_PORTS_MESSAGE;
            }

             select the current port if possible
            if (comboBoxComPort.Items.Contains(SwitchHardware.comPort))
            {
                comboBoxComPort.SelectedItem = SwitchHardware.comPort;
            }
            tl.LogMessage("InitUI", $"Set UI controls to Trace: {chkTrace.Checked}, COM Port: {comboBoxComPort.SelectedItem}");
            */


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
            catch (Exception ex)
            {
                MessageBox.Show("Unable to open link that was clicked.");
            }
        }

        private void VisitLink()
        {
            // Change the color of the link text by setting LinkVisited
            // to true.
            linkLabel1.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start(linkLabel1.Text);
        }

        private void comboDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboDevice.SelectedIndex == 1)  {
                panelInput.Enabled = true;
            } else
            {
                panelInput.Enabled = false;
            }

        }

        /*
private void textBoxPort_KeyPress(object sender, KeyPressEventArgs e)
{
if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
(e.KeyChar != '.'))
{
e.Handled = true;
}

// only allow one decimal point
if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
{
e.Handled = true;
}
}

private void textBoxIP_KeyPress(object sender, KeyPressEventArgs e)
{
if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
(e.KeyChar != '.'))
{
e.Handled = true;
}

// only allow one decimal point
if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
{
e.Handled = true;
}
}
*/
    }

}