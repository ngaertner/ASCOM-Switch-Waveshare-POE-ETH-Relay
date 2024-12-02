//
// ASCOM Switch hardware class for Waveshare Modbus POE ETH Relay
//
// Description:	 Driver for Waveshare Modbus POE ETH Relay
//
// Implements:	ASCOM Switch interface version: ISwitchV2
// Author:		Nico Gärtner - ascom@astronico.de
//

using ASCOM.Astrometry.AstroUtils;
using ASCOM.DeviceInterface;
using ASCOM.Utilities;
using ASCOM.Utilities.Interfaces;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Net;
using System.Windows.Forms;



namespace ASCOM.Waveshare_Modbus_POE_ETH_Relay.Switch
{

    /// <summary>
    /// ASCOM Switch hardware class for WaveshareModbus POE ETH Relay.
    /// </summary>t
    [HardwareClass()] // Class attribute flag this as a device hardware class that needs to be disposed by the local server when it exits.
    internal static class SwitchHardware
    {
        // Constants used for Profile persistence

        internal static WaveShareRelay Relay;

        internal const int DeviceTypeETH8CH = 0;
        internal const int DeviceTypeETH8CHB = 1;
        //internal const int DeviceTypeETH30CH = 2;

        internal const int ProtocolTypeModBusRTU = 0;
        internal const int ProtocolTypeModBusTCP = 1;

        internal const string IpAddressProfileName = "IP Address";
        internal const string IpAddressDefault = "192.168.1.200";
        internal const string IpPortProfileName = "Port";
        internal const string IpPortDefault = "4196";

        internal const string DeviceTypeProfileName = "Device Type";
        internal static short deviceType;
        internal const int DeviceTypeDefault = DeviceTypeETH8CHB;
        internal const string DeviceProtocolProfileName = "Device Protocol";
        internal const int DeviceProtocolDefault = ProtocolTypeModBusRTU;

        internal static short deviceProtocol;

        internal const string DebugStateProfileName = "Debug";


        internal const int SwitchModeNormal = WaveShareRelay.SwitchModeNormal;
        internal const int SwitchModeLinkage = WaveShareRelay.SwitchModeLinkage;
        internal const int SwitchModeToggle = WaveShareRelay.SwitchModeToggle;
        internal const int SwitchModeJump = WaveShareRelay.SwitchModeJump;
        internal const int SwitchModeNoChange = WaveShareRelay.SwitchModeNoChange;
        internal const int SwitchModeInvalid = WaveShareRelay.SwitchModeInvalid;

        internal const string TraceStateProfileName = "Trace Level";
        internal const string TraceStateDefault = "true";
        internal const string DebugStateDefault = "false";
        internal const string Relay1NameProfileName = "Relay 1 Name";
        internal const string Relay1NameDefault = "Relay 1";
        internal const string Relay2NameProfileName = "Relay 2 Name";
        internal const string Relay2NameDefault = "Relay 2";
        internal const string Relay3NameProfileName = "Relay 3 Name";
        internal const string Relay3NameDefault = "Relay 3";
        internal const string Relay4NameProfileName = "Relay 4 Name";
        internal const string Relay4NameDefault = "Relay 4";
        internal const string Relay5NameProfileName = "Relay 5 Name";
        internal const string Relay5NameDefault = "Relay 5";
        internal const string Relay6NameProfileName = "Relay 6 Name";
        internal const string Relay6NameDefault = "Relay 6";
        internal const string Relay7NameProfileName = "Relay 7 Name";
        internal const string Relay7NameDefault = "Relay 7";
        internal const string Relay8NameProfileName = "Relay 8 Name";
        internal const string Relay8NameDefault = "Relay 8";

        internal const string Input1NameProfileName = "Input 1 Name";
        internal const string Input1NameDefault = "Input 1";
        internal const string Input2NameProfileName = "Input 2 Name";
        internal const string Input2NameDefault = "Input 2";
        internal const string Input3NameProfileName = "Input 3 Name";
        internal const string Input3NameDefault = "Input 3";
        internal const string Input4NameProfileName = "Input 4 Name";
        internal const string Input4NameDefault = "Input 4";
        internal const string Input5NameProfileName = "Input 5 Name";
        internal const string Input5NameDefault = "Input 5";
        internal const string Input6NameProfileName = "Input 6 Name";
        internal const string Input6NameDefault = "Input 6";
        internal const string Input7NameProfileName = "Input 7 Name";
        internal const string Input7NameDefault = "Input 7";
        internal const string Input8NameProfileName = "Input 8 Name";
        internal const string Input8NameDefault = "Input 8";

        internal const string Input1ModeProfileName = "Input 1 Mode";
        internal const string Input2ModeProfileName = "Input 2 Mode";
        internal const string Input3ModeProfileName = "Input 3 Mode";
        internal const string Input4ModeProfileName = "Input 4 Mode";
        internal const string Input5ModeProfileName = "Input 5 Mode";
        internal const string Input6ModeProfileName = "Input 6 Mode";
        internal const string Input7ModeProfileName = "Input 7 Mode";
        internal const string Input8ModeProfileName = "Input 8 Mode";

        internal const string Relay1VisibleProfileName = "Relay 1 visible";
        internal const string Relay2VisibleProfileName = "Relay 2 visible";
        internal const string Relay3VisibleProfileName = "Relay 3 visible";
        internal const string Relay4VisibleProfileName = "Relay 4 visible";
        internal const string Relay5VisibleProfileName = "Relay 5 visible";
        internal const string Relay6VisibleProfileName = "Relay 6 visible";
        internal const string Relay7VisibleProfileName = "Relay 7 visible";
        internal const string Relay8VisibleProfileName = "Relay 8 visible";


        internal const string Relay1ToggleProfileName = "Relay 1 toggle";
        internal const string Relay2ToggleProfileName = "Relay 2 toggle";
        internal const string Relay3ToggleProfileName = "Relay 3 toggle";
        internal const string Relay4ToggleProfileName = "Relay 4 toggle";
        internal const string Relay5ToggleProfileName = "Relay 5 toggle";
        internal const string Relay6ToggleProfileName = "Relay 6 toggle";
        internal const string Relay7ToggleProfileName = "Relay 7 toggle";
        internal const string Relay8ToggleProfileName = "Relay 8 toggle";

        internal const string Input1VisibleProfileName = "Input 1 visible";
        internal const string Input2VisibleProfileName = "Input 2 visible";
        internal const string Input3VisibleProfileName = "Input 3 visible";
        internal const string Input4VisibleProfileName = "Input 4 visible";
        internal const string Input5VisibleProfileName = "Input 5 visible";
        internal const string Input6VisibleProfileName = "Input 6 visible";
        internal const string Input7VisibleProfileName = "Input 7 visible";
        internal const string Input8VisibleProfileName = "Input 8 visible";

        private static string DriverProgId = ""; // ASCOM DeviceID (COM ProgID) for this driver, the value is set by the driver's class initialiser.
        private static string DriverDescription = ""; // The value is set by the driver's class initialiser.
                                                      //    internal static string comPort; // COM port name (if required)
        internal static string ipAddress; // IP name (if required)
        internal static int ipPort; // port number (if required)
        private static bool connectedState; // Local server's connected state
        private static bool runOnce = false; // Flag to enable "one-off" activities only to run once.
        internal static Util utilities; // ASCOM Utilities object for use as required
        internal static AstroUtils astroUtilities; // ASCOM AstroUtilities object for use as required
        internal static TraceLogger tl; // Local server's trace logger object for diagnostic log with information that you specify
        internal static bool debugState;

        /// <summary>
        /// Initializes a new instance of the device Hardware class.
        /// </summary>
        static SwitchHardware()
        {
            try
            {
                // Create the hardware trace logger in the static initialiser.
                // All other initialisation should go in the InitialiseHardware method.
                tl = new TraceLogger("", "Waveshare_Modbus_POE_ETH_Relay.Hardware");

                // DriverProgId has to be set here because it used by ReadProfile to get the TraceState flag.
                DriverProgId = Switch.DriverProgId; // Get this device's ProgID so that it can be used to read the Profile configuration values

                // ReadProfile has to go here before anything is written to the log because it loads the TraceLogger enable / disable state.
                ReadProfile(); // Read device configuration from the ASCOM Profile store, including the trace state

                LogMessage("SwitchHardware", $"Static initialiser completed.");
            }
            catch (Exception ex)
            {
                try { LogMessage("SwitchHardware", $"Initialisation exception: {ex}"); } catch { }
                MessageBox.Show($"{ex.Message}", "Exception creating ASCOM.Waveshare_Modbus_POE_ETH_Relay.Switch", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        /// <summary>
        /// Place device initialisation code here that delivers the selected ASCOM <see cref="Devices."/>
        /// </summary>
        /// <remarks>Called every time a new instance of the driver is created.</remarks>
        internal static void InitialiseHardware()
        {
            // This method will be called every time a new ASCOM client loads your driver
            LogMessage("InitialiseHardware", $"Start.");

            // Make sure that "one off" activities are only undertaken once
            if (runOnce == false)
            {
                LogMessage("InitialiseHardware", $"Starting one-off initialisation.");

                DriverDescription = Switch.DriverDescription; // Get this device's Chooser description

                LogMessage("InitialiseHardware", $"ProgID: {DriverProgId}, Description: {DriverDescription}");

                connectedState = false; // Initialise connected to false
                utilities = new Util(); //Initialise ASCOM Utilities object
                astroUtilities = new AstroUtils(); // Initialise ASCOM Astronomy Utilities object

                LogMessage("InitialiseHardware", "Completed basic initialisation");

                // Add your own "one off" device initialisation here e.g. validating existence of hardware and setting up communications

                LogMessage("InitialiseHardware", $"One-off initialisation complete.");
                runOnce = true; // Set the flag to ensure that this code is not run again
            }
        }

        // PUBLIC COM INTERFACE ISwitchV2 IMPLEMENTATION

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialogue form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public static void SetupDialog()
        {
            // Don't permit the setup dialogue if already connected
            if (IsConnected)
                Connected = false;
            //MessageBox.Show("Blab la Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm(tl))
            {
                var result = F.ShowDialog();
                if (result == DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        /// <summary>Returns the list of custom action names supported by this driver.</summary>
        /// <value>An ArrayList of strings (SafeArray collection) containing the names of supported actions.</value>
        public static ArrayList SupportedActions
        {
            get
            {
                LogMessage("SupportedActions Get", "Returning empty ArrayList");
                var ActionList = new ArrayList();
                ActionList.Add("Test");
                return ActionList;
                //return new ArrayList();
            }
        }

        /// <summary>Invokes the specified device-specific custom action.</summary>
        /// <param name="ActionName">A well known name agreed by interested parties that represents the action to be carried out.</param>
        /// <param name="ActionParameters">List of required parameters or an <see cref="String.Empty">Empty String</see> if none are required.</param>
        /// <returns>A string response. The meaning of returned strings is set by the driver author.
        /// <para>Suppose filter wheels start to appear with automatic wheel changers; new actions could be <c>QueryWheels</c> and <c>SelectWheel</c>. The former returning a formatted list
        /// of wheel names and the second taking a wheel name and making the change, returning appropriate values to indicate success or failure.</para>
        /// </returns>
        public static string Action(string actionName, string actionParameters)
        {
            LogMessage("Action", $"Action {actionName}, parameters {actionParameters} is not implemented");
            throw new ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and does not wait for a response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param> 
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        public static void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // TODO The optional CommandBlind method should either be implemented OR throw a MethodNotImplementedException
            // If implemented, CommandBlind must send the supplied command to the mount and return immediately without waiting for a response

            throw new MethodNotImplementedException($"CommandBlind - Command:{command}, Raw: {raw}.");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a boolean response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the interpreted boolean response received from the device.
        /// </returns>
        public static bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            // TODO The optional CommandBool method should either be implemented OR throw a MethodNotImplementedException
            // If implemented, CommandBool must send the supplied command to the mount, wait for a response and parse this to return a True or False value

            throw new MethodNotImplementedException($"CommandBool - Command:{command}, Raw: {raw}.");
        }

        /// <summary>
        /// Transmits an arbitrary string to the device and waits for a string response.
        /// Optionally, protocol framing characters may be added to the string before transmission.
        /// </summary>
        /// <param name="Command">The literal command string to be transmitted.</param>
        /// <param name="Raw">
        /// if set to <c>true</c> the string is transmitted 'as-is'.
        /// If set to <c>false</c> then protocol framing characters may be added prior to transmission.
        /// </param>
        /// <returns>
        /// Returns the string response received from the device.
        /// </returns>
        public static string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // TODO The optional CommandString method should either be implemented OR throw a MethodNotImplementedException
            // If implemented, CommandString must send the supplied command to the mount and wait for a response before returning this to the client

            throw new MethodNotImplementedException($"CommandString - Command:{command}, Raw: {raw}.");
        }

        /// <summary>
        /// Deterministically release both managed and unmanaged resources that are used by this class.
        /// </summary>
        /// <remarks>
        /// 
        /// Do not call this method from the Dispose method in your driver class.
        ///
        /// This is because this hardware class is decorated with the <see cref="HardwareClassAttribute"/> attribute and this Dispose() method will be called 
        /// automatically by the  local server executable when it is irretrievably shutting down. This gives you the opportunity to release managed and unmanaged 
        /// resources in a timely fashion and avoid any time delay between local server close down and garbage collection by the .NET runtime.
        ///
        /// For the same reason, do not call the SharedResources.Dispose() method from this method. Any resources used in the static shared resources class
        /// itself should be released in the SharedResources.Dispose() method as usual. The SharedResources.Dispose() method will be called automatically 
        /// by the local server just before it shuts down.
        /// 
        /// </remarks>
        public static void Dispose()
        {
            try { LogMessage("Dispose", $"Disposing of assets and closing down."); } catch { }

            try
            {
                // Clean up the trace logger and utility objects
                if (tl != null)
                {
                    tl.Enabled = false;
                    tl.Dispose();
                }
                tl = null;
            }
            catch { }

            try
            {
                if (utilities != null) utilities.Dispose();
                utilities = null;
            }
            catch { }

            try
            {
                if (astroUtilities != null) astroUtilities.Dispose();
                astroUtilities = null;
            }
            catch { }


            try
            {
                if (Relay != null) Relay.Dispose();
                Relay = null;
            }
            catch { }

        }

        /// <summary>
        /// Set True to connect to the device hardware. Set False to disconnect from the device hardware.
        /// You can also read the property to check whether it is connected. This reports the current hardware state.
        /// </summary>
        /// <value><c>true</c> if connected to the hardware; otherwise, <c>false</c>.</value>
        public static bool Connected
        {
            get
            {
                LogMessage("Connected", $"Get {IsConnected}");
                return IsConnected;
            }
            set
            {
                LogMessage("Connected", $"Set {value}");
                /*                if (value == IsConnected)
                                    return;
                */
                if (value)
                {

                    LogMessage("Connected Set", $"Connecting to ip {ipAddress}:{ipPort}");

                    numSwitch = MaxSwitch;

                    Relay = new WaveShareRelay(deviceType, deviceProtocol);

                    if (Relay.Connect(ipAddress, ipPort))
                    {
                        connectedState = true;
                    }
                    else
                    {
                        Connected = false;
                        LogMessage("Connected Set", $"Disconnecting to ip {ipAddress}:{ipPort}");
                        throw new DriverException("Failed to connect server.");
                    }


                    for (short id = 0; id < numSwitch; id++)
                    {
                        var intId = switchVisibleMapping[id];

                        if (switchIsInput[intId])
                        {
                            continue;
                        }

                        if (switchModes[intId] == SwitchModeNoChange)
                        {
                            //don't change switch mode - instead fetch current mode
                            switchModes[intId] = Relay.GetRelayMode(intId);
                            if (switchModes[intId] == SwitchModeInvalid)
                            {
                                throw new DriverException("Failed to get relay mode.");
                            }
                        }
                        else
                        {
                            //switch mode
                            if (Relay.SetRelayMode(intId, switchModes[intId]) == false)
                            {
                                Connected = false;
                                throw new DriverException("Failed to set relay mode.");
                            }
                        }

                    }


                    // initialize switch status
                    if (Relay.RefreshStates(true) == false)
                    {
                        Connected = false;
                        throw new DriverException("Failed to read relay status from server.");
                    }

                    try
                    {
                        for (short i = 0; i < numSwitch; i++)
                        {
                            GetSwitch(i);
                        }
                    }
                    catch
                    {
                        Connected = false;
                        throw new DriverException("Failed to read relay status from server.");
                    }

                }
                else
                {
                    LogMessage("Connected Set", $"Disconnecting ip {ipAddress}:{ipPort}");
                    if (Relay != null)
                    {
                        Relay.Disconnect();
                        Relay.Dispose();
                        Relay = null;
                    }
                    connectedState = false;
                }
            }
        }

        /// <summary>
        /// Returns a description of the device, such as manufacturer and model number. Any ASCII characters may be used.
        /// </summary>
        /// <value>The description.</value>
        public static string Description
        {
            // TODO customise this device description if required
            get
            {
                LogMessage("Description Get", DriverDescription);
                return DriverDescription;
            }
        }

        /// <summary>
        /// Descriptive and version information about this ASCOM driver.
        /// </summary>
        public static string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverInfo = $"Information about the driver itself. Version: {version.Major}.{version.Minor}";
                LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        /// <summary>
        /// A string containing only the major and minor version of the driver formatted as 'm.n'.
        /// </summary>
        public static string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = $"{version.Major}.{version.Minor}";
                LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        /// <summary>
        /// The interface version number that this device supports.
        /// </summary>
        public static short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                LogMessage("InterfaceVersion Get", "2");
                return Convert.ToInt16("2");
            }
        }

        /// <summary>
        /// The short name of the driver, for display purposes
        /// </summary>
        public static string Name
        {
            get
            {
                string name = "Waveshare Modbus POE ETH Relay";
                LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region ISwitchV2 Implementation

        private static short numSwitch = 8;

        internal static string portOneName = "one";
        internal static string portTwoName = "two";
        internal static string portThreeName = "three";
        internal static string portFourName = "four";
        internal static string portFiveName = "five";
        internal static string portSixName = "six";
        internal static string portSevenName = "seven";
        internal static string portEightName = "eight";

        internal static string inputOneName = "input one";
        internal static string inputTwoName = "input two";
        internal static string inputThreeName = "input three";
        internal static string inputFourName = "input four";
        internal static string inputFiveName = "input five";
        internal static string inputSixName = "input six";
        internal static string inputSevenName = "input seven";
        internal static string inputEightName = "input eight";

        internal static string[] switchNames = new string[] { portOneName, portTwoName, portThreeName, portFourName, portFiveName, portSixName, portSevenName, portEightName, inputOneName, inputTwoName, inputThreeName, inputFourName, inputFiveName, inputSixName, inputSevenName, inputEightName };

        internal static short[] switchModes = new short[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        internal static short[] switchHardwareMapping = new short[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        internal static short[] switchVisibleMapping = new short[] { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        internal static Boolean[] switchVisible = new Boolean[] { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };
        internal static Boolean[] switchWritable = new Boolean[] { true, true, true, true, true, true, true, true, false, false, false, false, false, false, false, false };
        internal static Boolean[] switchIsInput = new Boolean[] { false, false, false, false, false, false, false, false, true, true, true, true, true, true, true, true };
        internal static Boolean[] switchToggle = new Boolean[] { false, false, false, false, false, false, false, false };

        /// <summary>
        /// The number of switches managed by this driver
        /// </summary>
        /// <returns>The number of devices managed by this driver.</returns>
        internal static short MaxSwitch
        {
            get
            {
                LogMessage("MaxSwitch Get", numSwitch.ToString());

                // initialize array with -1 pointers
                for (short i = 0; i < switchVisibleMapping.Length; i++)
                {
                    switchVisibleMapping[i] = -1;
                }
                for (short i = 0; i < switchHardwareMapping.Length; i++)
                {
                    switchHardwareMapping[i] = -1;
                }

                // map visible switch IDs to actual switch IDs in the device / config
                short visibleSwitches = 0;
                for (short i = 0; i < switchVisible.Length; i++)
                {

                    if (switchVisible[i])
                    {

                        switchVisibleMapping[visibleSwitches] = i;
                        switchHardwareMapping[i] = visibleSwitches;
                        visibleSwitches++;
                    }

                }

                //MessageBox.Show(visibleSwitches.ToString());
                //MessageBox.Show(switchVisibleMapping.ToString());
                return visibleSwitches;
            }
        }

        /// <summary>
        /// Return the name of switch device n.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The name of the device</returns>
        internal static string GetSwitchName(short id)
        {

            Validate("GetSwitchName", id);

            var int_id = switchVisibleMapping[id];
            return switchNames[int_id];
        }

        /// <summary>
        /// Set a switch device name to a specified value.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <param name="name">The name of the device</param>
        internal static void SetSwitchName(short id, string name)
        {

            Validate("SetSwitchName", id);

            var int_id = switchVisibleMapping[id];
            switchNames[int_id] = name;
        }

        /// <summary>
        /// Gets the description of the specified switch device. This is to allow a fuller description of
        /// the device to be returned, for example for a tool tip.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>
        /// String giving the device description.
        /// </returns>
        internal static string GetSwitchDescription(short id)
        {

            Validate("GetSwitchDescription", id);

            var intId = switchVisibleMapping[id];

            string desc = "";

            string modeDesc = "";

            string toggleDesc = "";

            if (switchIsInput[intId] == false)
            {
                desc = $"Relay {intId + 1}";

                switch (switchModes[intId])
                {
                    case SwitchModeNormal:
                        modeDesc = " - Normal Mode";
                        break;
                    case SwitchModeJump:
                        modeDesc = $" - Jumps on Input {intId + 1} Signal";
                        break;
                    case SwitchModeToggle:
                        modeDesc = $" - Toggles on Input {intId + 1} Signal";
                        break;
                    case SwitchModeLinkage:
                        modeDesc = $" - Linked to Input {intId + 1} (Readonly)";
                        break;
                    default:
                        //modeDesc = " - Normal Mode";
                        break;

                }
                if (switchToggle[intId])
                    toggleDesc = " with automatic back toggle";
            }
            else
            {
                desc = $"Digital Input {intId - 8 + 1}";

                switch (switchModes[intId - 8])
                {
                    case SwitchModeNormal:
                        //                        modeDesc = " - Normal Mode";
                        break;
                    case SwitchModeJump:
                        modeDesc = $"  - Toggles Relay {intId - 8 + 1} to Input change";
                        break;
                    case SwitchModeToggle:
                        modeDesc = $" - Toggles Relay {intId - 8 + 1} on Input status";
                        break;
                    case SwitchModeLinkage:
                        modeDesc = $" - Links Relay {intId - 8 + 1} to Input Status";
                        break;
                    default:
                        //modeDesc = "Normal Mode";
                        break;

                }

            }

            return desc + modeDesc + toggleDesc;

        }

        /// <summary>
        /// Reports if the specified switch device can be written to, default true.
        /// This is false if the device cannot be written to, for example a limit switch or a sensor.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>
        /// <c>true</c> if the device can be written to, otherwise <c>false</c>.
        /// </returns>
        internal static bool CanWrite(short id)
        {
            Validate("CanWrite", id);

            var intId = switchVisibleMapping[id];

            bool writable = switchWritable[intId];

            LogMessage("CanWrite", $"CanWrite({id}): {writable}");
            return writable;
        }

        #region Boolean switch members

        /// <summary>
        /// Return the state of switch device id as a boolean
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>True or false</returns>
        internal static bool GetSwitch(short id, bool forceRefresh = false)
        {

            bool state;

            Validate("GetSwitch", id);

            if (IsConnected == false)
            {
                return false;
            }

            var intId = switchVisibleMapping[id];

            if (Relay.RefreshStates() == false)
            {
                throw new DriverException($"Switch could not be read: {id}");
            }

            if (switchIsInput[intId] == false)
            {
                state = Relay.GetRelayState(intId);
            }
            else
            {
                //this is a bit hacky... better approach required
                state = Relay.GetInputState(Convert.ToInt16(intId - 8));
            }

            return state;
        }

        /// <summary>
        /// Sets a switch controller device to the specified state, true or false.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <param name="state">The required control state</param>
        internal static void SetSwitch(short id, bool state)
        {

            Validate("SetSwitch", id);

            if (IsConnected == false)
            {
                return;
            }

            if (GetSwitch(id, true) == state)
            {
                return;
            }

            var intId = switchVisibleMapping[id];

            if (switchModes[intId] == SwitchModeLinkage)
            {
                throw new DriverException($"Relay is linked to input: {intId + 1}");
            }

            if (Relay.SetRelayState(intId, state, switchToggle[intId]) == false)
            {
                if (Relay.RefreshStates() == false)
                {
                    throw new DriverException($"Switch could not be read: {id}");
                }
            }

        }

        #endregion

        #region Analogue members

        /// <summary>
        /// Returns the maximum value for this switch device, this must be greater than <see cref="MinSwitchValue"/>.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The maximum value to which this device can be set or which a read only sensor will return.</returns>
        internal static double MaxSwitchValue(short id)
        {
            Validate("MaxSwitchValue", id);
            return 1;
        }

        /// <summary>
        /// Returns the minimum value for this switch device, this must be less than <see cref="MaxSwitchValue"/>
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The minimum value to which this device can be set or which a read only sensor will return.</returns>
        internal static double MinSwitchValue(short id)
        {
            Validate("MinSwitchValue", id);
            return 0;
        }

        /// <summary>
        /// Returns the step size that this device supports (the difference between successive values of the device).
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The step size for this device.</returns>
        internal static double SwitchStep(short id)
        {

            Validate("SwitchStep", id);
            return 1;
        }

        /// <summary>
        /// Returns the value for switch device id as a double
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <returns>The value for this switch, this is expected to be between <see cref="MinSwitchValue"/> and
        /// <see cref="MaxSwitchValue"/>.</returns>
        internal static double GetSwitchValue(short id)
        {
            Validate("GetSwitchValue", id);
            LogMessage("GetSwitchValue", $"GetSwitchValue({id}) - not implemented");
            throw new MethodNotImplementedException("GetSwitchValue");
        }

        /// <summary>
        /// Set the value for this device as a double.
        /// </summary>
        /// <param name="id">The device number (0 to <see cref="MaxSwitch"/> - 1)</param>
        /// <param name="value">The value to be set, between <see cref="MinSwitchValue"/> and <see cref="MaxSwitchValue"/></param>
        internal static void SetSwitchValue(short id, double value)
        {
            Validate("SetSwitchValue", id, value);
            if (!CanWrite(id))
            {
                LogMessage("SetSwitchValue", $"SetSwitchValue({id}) - Cannot write");
                throw new ASCOM.MethodNotImplementedException($"SetSwitchValue({id}) - Cannot write");
            }
            LogMessage("SetSwitchValue", $"SetSwitchValue({id}) = {value} - not implemented");
            throw new MethodNotImplementedException("SetSwitchValue");
        }

        #endregion

        #endregion

        #region Private methods

        /// <summary>
        /// Checks that the switch id is in range and throws an InvalidValueException if it isn't
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="id">The id.</param>
        private static void Validate(string message, short id)
        {
            if (id < 0 || id >= numSwitch)
            {
                LogMessage(message, string.Format("Switch {0} not available, range is 0 to {1}", id, numSwitch - 1));
                throw new InvalidValueException(message, id.ToString(), string.Format("0 to {0}", numSwitch - 1));
            }
        }

        /// <summary>
        /// Checks that the switch id and value are in range and throws an
        /// InvalidValueException if they are not.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="id">The id.</param>
        /// <param name="value">The value.</param>
        private static void Validate(string message, short id, double value)
        {
            Validate(message, id);
            var min = MinSwitchValue(id);
            var max = MaxSwitchValue(id);
            if (value < min || value > max)
            {
                LogMessage(message, string.Format("Value {1} for Switch {0} is out of the allowed range {2} to {3}", id, value, min, max));
                throw new InvalidValueException(message, value.ToString(), string.Format("Switch({0}) range {1} to {2}", id, min, max));
            }
        }

        #endregion

        #region Private properties and methods
        // Useful methods that can be used as required to help with driver development

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private static bool IsConnected
        {
            get
            {

                if (Relay != null)
                {
                    connectedState = Relay.Connected();
                }
                else
                {
                    connectedState = false;
                }


                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private static void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal static void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Switch";
                tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, TraceStateProfileName, string.Empty, TraceStateDefault));
                debugState = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, DebugStateProfileName, string.Empty, DebugStateDefault));
                ipPort = Convert.ToInt32(driverProfile.GetValue(DriverProgId, IpPortProfileName, string.Empty, IpPortDefault));
                ipAddress = driverProfile.GetValue(DriverProgId, IpAddressProfileName, string.Empty, IpAddressDefault);
                deviceType = Convert.ToInt16(driverProfile.GetValue(DriverProgId, DeviceTypeProfileName, string.Empty, DeviceTypeDefault.ToString()));
                deviceProtocol = Convert.ToInt16(driverProfile.GetValue(DriverProgId, DeviceProtocolProfileName, string.Empty, DeviceProtocolDefault.ToString()));

                switchNames[0] = driverProfile.GetValue(DriverProgId, Relay1NameProfileName, string.Empty, Relay1NameDefault);
                switchNames[1] = driverProfile.GetValue(DriverProgId, Relay2NameProfileName, string.Empty, Relay2NameDefault);
                switchNames[2] = driverProfile.GetValue(DriverProgId, Relay3NameProfileName, string.Empty, Relay3NameDefault);
                switchNames[3] = driverProfile.GetValue(DriverProgId, Relay4NameProfileName, string.Empty, Relay4NameDefault);
                switchNames[4] = driverProfile.GetValue(DriverProgId, Relay5NameProfileName, string.Empty, Relay5NameDefault);
                switchNames[5] = driverProfile.GetValue(DriverProgId, Relay6NameProfileName, string.Empty, Relay6NameDefault);
                switchNames[6] = driverProfile.GetValue(DriverProgId, Relay7NameProfileName, string.Empty, Relay7NameDefault);
                switchNames[7] = driverProfile.GetValue(DriverProgId, Relay8NameProfileName, string.Empty, Relay8NameDefault);

                switchNames[8] = driverProfile.GetValue(DriverProgId, Input1NameProfileName, string.Empty, Input1NameDefault);
                switchNames[9] = driverProfile.GetValue(DriverProgId, Input2NameProfileName, string.Empty, Input2NameDefault);
                switchNames[10] = driverProfile.GetValue(DriverProgId, Input3NameProfileName, string.Empty, Input3NameDefault);
                switchNames[11] = driverProfile.GetValue(DriverProgId, Input4NameProfileName, string.Empty, Input4NameDefault);
                switchNames[12] = driverProfile.GetValue(DriverProgId, Input5NameProfileName, string.Empty, Input5NameDefault);
                switchNames[13] = driverProfile.GetValue(DriverProgId, Input6NameProfileName, string.Empty, Input6NameDefault);
                switchNames[14] = driverProfile.GetValue(DriverProgId, Input7NameProfileName, string.Empty, Input7NameDefault);
                switchNames[15] = driverProfile.GetValue(DriverProgId, Input8NameProfileName, string.Empty, Input8NameDefault);

                switchVisible[0] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay1VisibleProfileName, string.Empty, "true"));
                switchVisible[1] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay2VisibleProfileName, string.Empty, "true"));
                switchVisible[2] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay3VisibleProfileName, string.Empty, "true"));
                switchVisible[3] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay4VisibleProfileName, string.Empty, "true"));
                switchVisible[4] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay5VisibleProfileName, string.Empty, "true"));
                switchVisible[5] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay6VisibleProfileName, string.Empty, "true"));
                switchVisible[6] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay7VisibleProfileName, string.Empty, "true"));
                switchVisible[7] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay8VisibleProfileName, string.Empty, "true"));

                switchVisible[8] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input1VisibleProfileName, string.Empty, "true"));
                switchVisible[9] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input2VisibleProfileName, string.Empty, "true"));
                switchVisible[10] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input3VisibleProfileName, string.Empty, "true"));
                switchVisible[11] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input4VisibleProfileName, string.Empty, "true"));
                switchVisible[12] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input5VisibleProfileName, string.Empty, "true"));
                switchVisible[13] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input6VisibleProfileName, string.Empty, "true"));
                switchVisible[14] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input7VisibleProfileName, string.Empty, "true"));
                switchVisible[15] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Input8VisibleProfileName, string.Empty, "true"));

                switchToggle[0] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay1ToggleProfileName, string.Empty, "false"));
                switchToggle[1] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay2ToggleProfileName, string.Empty, "false"));
                switchToggle[2] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay3ToggleProfileName, string.Empty, "false"));
                switchToggle[3] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay4ToggleProfileName, string.Empty, "false"));
                switchToggle[4] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay5ToggleProfileName, string.Empty, "false"));
                switchToggle[5] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay6ToggleProfileName, string.Empty, "false"));
                switchToggle[6] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay7ToggleProfileName, string.Empty, "false"));
                switchToggle[7] = Convert.ToBoolean(driverProfile.GetValue(DriverProgId, Relay8ToggleProfileName, string.Empty, "false"));

                switchModes[0] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input1ModeProfileName, string.Empty, "0"));
                switchModes[1] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input2ModeProfileName, string.Empty, "0"));
                switchModes[2] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input3ModeProfileName, string.Empty, "0"));
                switchModes[3] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input4ModeProfileName, string.Empty, "0"));
                switchModes[4] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input5ModeProfileName, string.Empty, "0"));
                switchModes[5] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input6ModeProfileName, string.Empty, "0"));
                switchModes[6] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input7ModeProfileName, string.Empty, "0"));
                switchModes[7] = Convert.ToInt16(driverProfile.GetValue(DriverProgId, Input8ModeProfileName, string.Empty, "0"));

            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal static void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Switch";
                driverProfile.WriteValue(DriverProgId, TraceStateProfileName, tl.Enabled.ToString());
                driverProfile.WriteValue(DriverProgId, DebugStateProfileName, debugState.ToString());
                driverProfile.WriteValue(DriverProgId, IpAddressProfileName, ipAddress.ToString());
                driverProfile.WriteValue(DriverProgId, IpPortProfileName, ipPort.ToString());
                driverProfile.WriteValue(DriverProgId, DeviceTypeProfileName, deviceType.ToString());
                driverProfile.WriteValue(DriverProgId, DeviceProtocolProfileName, deviceProtocol.ToString());

                driverProfile.WriteValue(DriverProgId, Relay1NameProfileName, switchNames[0]);
                driverProfile.WriteValue(DriverProgId, Relay2NameProfileName, switchNames[1]);
                driverProfile.WriteValue(DriverProgId, Relay3NameProfileName, switchNames[2]);
                driverProfile.WriteValue(DriverProgId, Relay4NameProfileName, switchNames[3]);
                driverProfile.WriteValue(DriverProgId, Relay5NameProfileName, switchNames[4]);
                driverProfile.WriteValue(DriverProgId, Relay6NameProfileName, switchNames[5]);
                driverProfile.WriteValue(DriverProgId, Relay7NameProfileName, switchNames[6]);
                driverProfile.WriteValue(DriverProgId, Relay8NameProfileName, switchNames[7]);

                driverProfile.WriteValue(DriverProgId, Input1NameProfileName, switchNames[8]);
                driverProfile.WriteValue(DriverProgId, Input2NameProfileName, switchNames[9]);
                driverProfile.WriteValue(DriverProgId, Input3NameProfileName, switchNames[10]);
                driverProfile.WriteValue(DriverProgId, Input4NameProfileName, switchNames[11]);
                driverProfile.WriteValue(DriverProgId, Input5NameProfileName, switchNames[12]);
                driverProfile.WriteValue(DriverProgId, Input6NameProfileName, switchNames[13]);
                driverProfile.WriteValue(DriverProgId, Input7NameProfileName, switchNames[14]);
                driverProfile.WriteValue(DriverProgId, Input8NameProfileName, switchNames[15]);

                driverProfile.WriteValue(DriverProgId, Relay1ToggleProfileName, Convert.ToString(switchToggle[0]));
                driverProfile.WriteValue(DriverProgId, Relay2ToggleProfileName, Convert.ToString(switchToggle[1]));
                driverProfile.WriteValue(DriverProgId, Relay3ToggleProfileName, Convert.ToString(switchToggle[2]));
                driverProfile.WriteValue(DriverProgId, Relay4ToggleProfileName, Convert.ToString(switchToggle[3]));
                driverProfile.WriteValue(DriverProgId, Relay5ToggleProfileName, Convert.ToString(switchToggle[4]));
                driverProfile.WriteValue(DriverProgId, Relay6ToggleProfileName, Convert.ToString(switchToggle[5]));
                driverProfile.WriteValue(DriverProgId, Relay7ToggleProfileName, Convert.ToString(switchToggle[6]));
                driverProfile.WriteValue(DriverProgId, Relay8ToggleProfileName, Convert.ToString(switchToggle[7]));

                driverProfile.WriteValue(DriverProgId, Relay1VisibleProfileName, Convert.ToString(switchVisible[0]));
                driverProfile.WriteValue(DriverProgId, Relay2VisibleProfileName, Convert.ToString(switchVisible[1]));
                driverProfile.WriteValue(DriverProgId, Relay3VisibleProfileName, Convert.ToString(switchVisible[2]));
                driverProfile.WriteValue(DriverProgId, Relay4VisibleProfileName, Convert.ToString(switchVisible[3]));
                driverProfile.WriteValue(DriverProgId, Relay5VisibleProfileName, Convert.ToString(switchVisible[4]));
                driverProfile.WriteValue(DriverProgId, Relay6VisibleProfileName, Convert.ToString(switchVisible[5]));
                driverProfile.WriteValue(DriverProgId, Relay7VisibleProfileName, Convert.ToString(switchVisible[6]));
                driverProfile.WriteValue(DriverProgId, Relay8VisibleProfileName, Convert.ToString(switchVisible[7]));

                driverProfile.WriteValue(DriverProgId, Input1VisibleProfileName, Convert.ToString(switchVisible[8]));
                driverProfile.WriteValue(DriverProgId, Input2VisibleProfileName, Convert.ToString(switchVisible[9]));
                driverProfile.WriteValue(DriverProgId, Input3VisibleProfileName, Convert.ToString(switchVisible[10]));
                driverProfile.WriteValue(DriverProgId, Input4VisibleProfileName, Convert.ToString(switchVisible[11]));
                driverProfile.WriteValue(DriverProgId, Input5VisibleProfileName, Convert.ToString(switchVisible[12]));
                driverProfile.WriteValue(DriverProgId, Input6VisibleProfileName, Convert.ToString(switchVisible[13]));
                driverProfile.WriteValue(DriverProgId, Input7VisibleProfileName, Convert.ToString(switchVisible[14]));
                driverProfile.WriteValue(DriverProgId, Input8VisibleProfileName, Convert.ToString(switchVisible[15]));

                driverProfile.WriteValue(DriverProgId, Input1ModeProfileName, Convert.ToString(switchModes[0]));
                driverProfile.WriteValue(DriverProgId, Input2ModeProfileName, Convert.ToString(switchModes[1]));
                driverProfile.WriteValue(DriverProgId, Input3ModeProfileName, Convert.ToString(switchModes[2]));
                driverProfile.WriteValue(DriverProgId, Input4ModeProfileName, Convert.ToString(switchModes[3]));
                driverProfile.WriteValue(DriverProgId, Input5ModeProfileName, Convert.ToString(switchModes[4]));
                driverProfile.WriteValue(DriverProgId, Input6ModeProfileName, Convert.ToString(switchModes[5]));
                driverProfile.WriteValue(DriverProgId, Input7ModeProfileName, Convert.ToString(switchModes[6]));
                driverProfile.WriteValue(DriverProgId, Input8ModeProfileName, Convert.ToString(switchModes[7]));

                driverProfile.WriteValue(DriverProgId, Relay1VisibleProfileName, Convert.ToString(switchVisible[0]));
                driverProfile.WriteValue(DriverProgId, Relay2VisibleProfileName, Convert.ToString(switchVisible[1]));
                driverProfile.WriteValue(DriverProgId, Relay3VisibleProfileName, Convert.ToString(switchVisible[2]));
                driverProfile.WriteValue(DriverProgId, Relay4VisibleProfileName, Convert.ToString(switchVisible[3]));
                driverProfile.WriteValue(DriverProgId, Relay5VisibleProfileName, Convert.ToString(switchVisible[4]));
                driverProfile.WriteValue(DriverProgId, Relay6VisibleProfileName, Convert.ToString(switchVisible[5]));
                driverProfile.WriteValue(DriverProgId, Relay7VisibleProfileName, Convert.ToString(switchVisible[6]));
                driverProfile.WriteValue(DriverProgId, Relay8VisibleProfileName, Convert.ToString(switchVisible[7]));

                driverProfile.WriteValue(DriverProgId, Input1VisibleProfileName, Convert.ToString(switchVisible[8]));
                driverProfile.WriteValue(DriverProgId, Input2VisibleProfileName, Convert.ToString(switchVisible[9]));
                driverProfile.WriteValue(DriverProgId, Input3VisibleProfileName, Convert.ToString(switchVisible[10]));
                driverProfile.WriteValue(DriverProgId, Input4VisibleProfileName, Convert.ToString(switchVisible[11]));
                driverProfile.WriteValue(DriverProgId, Input5VisibleProfileName, Convert.ToString(switchVisible[12]));
                driverProfile.WriteValue(DriverProgId, Input6VisibleProfileName, Convert.ToString(switchVisible[13]));
                driverProfile.WriteValue(DriverProgId, Input7VisibleProfileName, Convert.ToString(switchVisible[14]));
                driverProfile.WriteValue(DriverProgId, Input8VisibleProfileName, Convert.ToString(switchVisible[15]));

            }
        }

        /// <summary>
        /// Log helper function that takes identifier and message strings
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        internal static void LogMessage(string identifier, string message)
        {
            tl.LogMessageCrLf(identifier, message);
        }

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            LogMessage(identifier, msg);
        }
        #endregion
    }
}

static class testAbs
{
    public static bool GetBit(this byte b, int bitNumber)
    {
        var bit = (b & (1 << bitNumber - 1)) != 0;
        return bit;
    }
}