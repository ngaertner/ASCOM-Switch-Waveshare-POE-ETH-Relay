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
            if (IPAddress.TryParse(textBoxIP.Text, out ipAddress))
            {
                //valid ip
            }
            else
            {
                return;
                //is not valid ip
            }

            SwitchHardware.IpAddress = (string)textBoxIP.Text;
            SwitchHardware.IpPort = Convert.ToInt32(textBoxPort.Text);

            SwitchHardware.switchNames[0] = textBoxRelay1.Text;
            SwitchHardware.switchNames[1] = textBoxRelay2.Text;
            SwitchHardware.switchNames[2] = textBoxRelay3.Text;
            SwitchHardware.switchNames[3] = textBoxRelay4.Text;
            SwitchHardware.switchNames[4] = textBoxRelay5.Text;
            SwitchHardware.switchNames[5] = textBoxRelay6.Text;
            SwitchHardware.switchNames[6] = textBoxRelay7.Text;
            SwitchHardware.switchNames[7] = textBoxRelay8.Text;

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


            textBoxIP.Text = SwitchHardware.IpAddress;
            textBoxPort.Text = Convert.ToString(SwitchHardware.IpPort);

            textBoxRelay1.Text = SwitchHardware.switchNames[0];
            textBoxRelay2.Text = SwitchHardware.switchNames[1];
            textBoxRelay3.Text = SwitchHardware.switchNames[2];
            textBoxRelay4.Text = SwitchHardware.switchNames[3];
            textBoxRelay5.Text = SwitchHardware.switchNames[4];
            textBoxRelay6.Text = SwitchHardware.switchNames[5];
            textBoxRelay7.Text = SwitchHardware.switchNames[6];
            textBoxRelay8.Text = SwitchHardware.switchNames[7];

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