using ASCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Configuration;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Linq.Expressions;
using System.Threading;

internal class WaveShareRelay
{

    internal Socket soc;
    internal bool connected;

    internal short deviceType;
    internal short deviceProtocol;
    internal DateTime lastRefreshTime;

    internal const short SwitchModeNormal = 0;
    internal const short SwitchModeLinkage = 1;
    internal const short SwitchModeToggle = 2;
    internal const short SwitchModeJump = 3;
    internal const short SwitchModeNoChange = 4;
    internal const short SwitchModeInvalid = -1;

    internal static byte[] commandRTURelayStatus = new byte[] { 0x01, 0x01, 0x00, 0x00, 0x00, 0x08 }; // 01 01 00 00 00 08 3D CC
    internal static byte[] commandRTUInputStatus = new byte[] { 0x01, 0x02, 0x00, 0x00, 0x00, 0x08 }; // 01 01 00 00 00 08 3D CC
    internal static byte[] commandRTUSwitchOnRelay = new byte[] { 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00 }; //01 05 00 00 FF 00 8C 3A
    internal static byte[] commandRTUSwitchOffRelay = new byte[] { 0x01, 0x05, 0x00, 0x00, 0x00, 0x00 }; //01 05 00 00 FF 00 8C 3A
    internal static byte[] commandRTUFlashOnRelay = new byte[] { 0x01, 0x05, 0x02, 0x00, 0x00, 0x05 }; // 01 05 02 00 00 05 8D B0
    internal static byte[] commandRTUFlashOffRelay = new byte[] { 0x01, 0x05, 0x04, 0x00, 0x00, 0x05 }; // 01 05 04 00 00 05 8D B0

    internal static byte[] commandRTUModeNormal = new byte[] { 0x01, 0x06, 0x10, 0x00, 0x00, 0x00 }; //01 06 10 00 00 00 4C CA
    internal static byte[] commandRTUModeLinkage = new byte[] { 0x01, 0x06, 0x10, 0x00, 0x00, 0x01 }; //01 06 10 00 00 01 4C CA
    internal static byte[] commandRTUModeToggle = new byte[] { 0x01, 0x06, 0x10, 0x00, 0x00, 0x02 }; //01 06 10 00 00 02 4C CA
    internal static byte[] commandRTUModeJump = new byte[] { 0x01, 0x06, 0x10, 0x00, 0x00, 0x03 }; //01 06 10 00 00 03 4C CA
    internal static byte[] commandRTUReadMode = new byte[] { 0x01, 0x03, 0x10, 0x00, 0x00, 0x01 }; //01 03 10 00 00 01 80 CA


    internal static byte[] commandRelayStatus = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, 0x00, 0x00, 0x00, 0x08 }; // 01 01 00 00 00 08 3D CC
    internal static byte[] commandInputStatus = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x02, 0x00, 0x00, 0x00, 0x08 }; // 01 02 00 00 00 08 79 CC
    internal static byte[] commandSwitchOnRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00 }; //01 05 00 00 FF 00 8C 3A
    internal static byte[] commandSwitchOffRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x00, 0x00, 0x00, 0x00 }; //01 05 00 00 00 00 8C 3A
    internal static byte[] commandFlashOnRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x02, 0x00, 0x00, 0x03 }; // 01 05 02 00 00 05 8D B0
    internal static byte[] commandFlashOffRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x04, 0x00, 0x00, 0x03 }; // 01 05 04 00 00 05 8D B0
    internal static byte[] commandModeNormal = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x00 }; //01 06 10 00 00 00 4C CA
    internal static byte[] commandModeLinkage = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x01 }; //01 06 10 00 00 01 4C CA
    internal static byte[] commandModeToggle = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x02 }; //01 06 10 00 00 02 4C CA
    internal static byte[] commandModeJump = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x03 }; //01 06 10 00 00 03 4C CA
    internal static byte[] commandReadMode = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x03, 0x10, 0x00, 0x00, 0x01 }; //01 03 10 00 00 01 80 CA

    internal static byte[] relayAddresses = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };
    internal static byte[] inputAddresses = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 };

    internal bool[] relayStates = new bool[8];
    internal bool[] relayReadonly = new bool[8];
    internal bool[] inputStates = new bool[8];

    internal const short DeviceTypeETH8CH = 0;
    internal const short DeviceTypeETH8CHB = 1;
    internal const short DeviceTypeETH30CH = 2;

    internal const short ProtocolTypeTCP = 0;
    internal const short ProtocolTypeModBusTCP = 1;

    internal IAsyncResult asyncSocketHandle = null;



    public WaveShareRelay(short DeviceType, short DeviceProtocol)
    {
        deviceProtocol = DeviceProtocol;
        deviceType = DeviceType;
    }

    public void Dispose()
    {
        if (Connected())
        {
            Disconnect();
        }
        if (soc != null)
        {
            soc.Dispose();
            soc = null;
        }
    }

    internal bool Connect(string ipAddress, int ipPort)
    {

        if (connected)
        {
            Disconnect();
        }

        soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        IAsyncResult asyncSocketHandle = soc.BeginConnect(ipAddress, ipPort, null, null);
        bool _ = asyncSocketHandle.AsyncWaitHandle.WaitOne(1000, true);



        if (soc.Connected)
        {
            soc.EndConnect(asyncSocketHandle);
        }
        else
        {
            // NOTE, MUST CLOSE THE SOCKET
            if (asyncSocketHandle != null)
            {
                soc.EndConnect(asyncSocketHandle);
            }
            soc.Close();
            connected = false;
        }

        connected = true;

        lastRefreshTime = DateTime.MinValue;

        if (connected)
        {
            RefreshStates();
        }

        return connected;

    }

    internal void Disconnect()
    {
        if (soc != null)
        {

            if (soc.Connected)
            {

                if (asyncSocketHandle != null)
                {
                    soc.EndConnect(asyncSocketHandle);
                }
                soc.Shutdown(SocketShutdown.Both);
                soc.Close();
            }
        }
        connected = false;
    }

    internal bool Connected()
    {

        if (soc == null)
        {
            connected = false;
        }
        else
        {
            if (connected && soc.Connected)
            {
                bool part1 = soc.Poll(1000, SelectMode.SelectRead);
                bool part2 = (soc.Available == 0);
                if (part1 && part2)
                    connected = false;
                else
                    connected = true;
            }
        }

        return connected;
    }


    internal bool SetRelayState(short id, bool state, bool flash = false)
    {


        if (!connected)
        {
            return false;
        }

        byte[] command;
        if (flash)
        {
            if (state)
            {
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandFlashOnRelay;
                }
                else
                {
                    command = commandRTUFlashOnRelay;
                }

            }
            else
            {
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandFlashOffRelay;
                }
                else
                {
                    command = commandRTUFlashOffRelay;
                }
            }
        }
        else
        {
            if (state)
            {
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandSwitchOnRelay;
                }
                else
                {
                    command = commandRTUSwitchOnRelay;
                }
            }
            else
            {
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandSwitchOffRelay;
                }
                else
                {
                    command = commandRTUSwitchOffRelay;
                }
            }
        }



        if (deviceProtocol == ProtocolTypeModBusTCP)
        {
            //modify 10th byte of the command to set the requested switch number
            command[9] = relayAddresses[id];
        }
        else
        {
            //modify 4th byte of the command to set the requested switch number
            command[3] = relayAddresses[id];
            command = prepareRTUCommand(command);
        }

        if (SendCommand(command) == null)
        {
            return false;
        }

        RefreshStates(true);
        return true;
    }

    internal short GetRelayMode(short id)
    {
        if (!connected)
        {
            return -1;
        }

        byte[] command;


        if (deviceProtocol == ProtocolTypeModBusTCP)
        {
            command = commandReadMode;
            command[9] = relayAddresses[id];
        }
        else
        {
            command = commandRTUReadMode;
            command[3] = relayAddresses[id];
            command = prepareRTUCommand(command);
        }

        byte[] buffer = SendCommand(command);
        if (buffer == null) return -1;

        switch (buffer[4])
        {
            case 0x00:
                return SwitchModeNormal;
            case 0x01:
                return SwitchModeLinkage;
            case 0x02:
                return SwitchModeToggle;
            case 0x03:
                return SwitchModeJump;
            default:
                return SwitchModeInvalid;
        }


    }

    internal bool SetRelayMode(short id, short mode)
    {

        if (!connected)
        {
            return false;
        }

        byte[] command;

        switch (mode)
        {
            case SwitchModeNormal:
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandModeNormal;
                }
                else
                {
                    command = commandRTUModeNormal;
                }
                relayReadonly[id] = false;
                break;
            case SwitchModeLinkage:
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandModeLinkage;
                }
                else
                {
                    command = commandRTUModeLinkage;
                }
                relayReadonly[id] = true;
                break;
            case SwitchModeJump:
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandModeJump;
                }
                else
                {
                    command = commandRTUModeJump;
                }
                relayReadonly[id] = false;
                break;
            case SwitchModeToggle:
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandModeToggle;
                }
                else
                {
                    command = commandRTUModeToggle;
                }
                relayReadonly[id] = false;
                break;
            default:
                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    command = commandModeNormal;
                }
                else
                {
                    command = commandRTUModeNormal;
                }
                relayReadonly[id] = false;
                break;
        }

        //modify 10th byte of the command to set the requested switch number
        if (deviceProtocol == ProtocolTypeModBusTCP)
        {
            command[9] = relayAddresses[id];
        }
        else
        {
            command[3] = relayAddresses[id];
            command = prepareRTUCommand(command);
        }

        if (SendCommand(command) == null) return false;

        return true;

    }

    internal bool GetRelayState(short Id)
    {
        RefreshStates();
        return relayStates[Id];
    }

    internal bool GetInputState(short Id)
    {
        RefreshStates();
        return inputStates[Id];
    }

    internal byte[] prepareRTUCommand(byte[] command)
    {
        //var crc = Crc16.ComputeChecksum(command);
        var crc = ModRTU_CRC(command, command.Length);
        byte[] crcByte = new byte[2];
        crcByte[0] = (byte)crc;
        crcByte[1] = (byte)(crc >> 8);

        IEnumerable<byte> rv = command.Concat(crcByte);
        return rv.ToArray();
    }

    internal bool RefreshStates(bool ForceRefresh = false)
    {

        if (!connected)
        {
            return false;
        }

        byte[] buffer;

        TimeSpan refreshDiffMilliseconds = DateTime.Now - lastRefreshTime;

        try
        {
            // initialize correct switch state with the actual device state
            if (refreshDiffMilliseconds.TotalMilliseconds > 200 || ForceRefresh == true)
            {

                lastRefreshTime = DateTime.Now;

                if (deviceProtocol == ProtocolTypeModBusTCP)
                {
                    buffer = SendCommand(commandRelayStatus);
                }
                else
                {
                    buffer = SendCommand(commandRTURelayStatus, true);
                }


                if (buffer == null)
                {
                    return false;
                }

                for (int n = 0; n < GetRelayCount(); n++)
                {

                    if (deviceProtocol == ProtocolTypeModBusTCP)
                    {
                        relayStates[n] = testAbs.GetBit(buffer[9], n + 1);
                    }
                    else
                    {
                        relayStates[n] = testAbs.GetBit(buffer[3], n + 1);
                    }

                }

                if (deviceType == DeviceTypeETH8CHB)
                {

                    if (deviceProtocol == ProtocolTypeModBusTCP)
                    {
                        buffer = SendCommand(commandInputStatus);
                    }
                    else
                    {
                        buffer = SendCommand(commandRTUInputStatus, true);
                    }

                    if (buffer == null)
                    {
                        return false;
                    }

                    for (int n = 0; n < GetInputCount(); n++)
                    {
                        if (deviceProtocol == ProtocolTypeModBusTCP)
                        {
                            inputStates[n] = testAbs.GetBit(buffer[9], n + 1);
                        }
                        else
                        {
                            inputStates[n] = testAbs.GetBit(buffer[3], n + 1);
                        }
                    }

                }

                string toDisplay = string.Join(Environment.NewLine, relayStates);


            }
        }
        catch
        {

            return false;

        };
        return true;

    }


    internal short GetInputCount()
    {
        if (deviceType == DeviceTypeETH8CHB)
        {
            return 8;
        }
        else
        {
            return 0;
        }
    }

    internal short GetRelayCount()
    {
        return 8;
    }

    static readonly object _lock = new object();

    internal byte[] SendCommand(byte[] Command, bool AddRTUChecksum = false, bool AutomaticRetry = true, int Timeout = 150 )
    {
        bool exceptionOccurred = false;
        bool retry = false;
        string exceptionMessage = "";
        var buffer = new byte[1024];
        if (Connected())
        {

            lock (_lock) // make it thread safe..
            {
                if (AddRTUChecksum)
                {
                    Command = prepareRTUCommand(Command);
                }

                try
                {
                    soc.ReceiveTimeout = Timeout;
                    soc.Send(Command);
                    soc.Receive(buffer);
                }
                catch (Exception ex)
                {

                    if (AutomaticRetry)
                    {
                        //retry same command once more
                        retry = true;
                    }
                    else
                    {
                        exceptionMessage = ex.Message;
                        exceptionOccurred = true;
                    }
                }
            }
            if (retry)
            {
                return SendCommand(Command, false, false, 500); //retry with a higher timeout
            }
            
            if (exceptionOccurred)
            {
                //show message outside of lock block
                MessageBox.Show($"FATAL: Socket Send Error - command: {BitConverter.ToString(Command).Replace(" - ", "")} \n {exceptionMessage}");
            } else
            {
                return buffer;
            }
        }
        return null;        
    }

    UInt16 ModRTU_CRC(byte[] buf, int len)
    {
        UInt16 crc = 0xFFFF;

        for (int pos = 0; pos < len; pos++)
        {
            crc ^= (UInt16)buf[pos];          // XOR byte into least sig. byte of crc

            for (int i = 8; i != 0; i--)      // Loop over each bit
            {
                if ((crc & 0x0001) != 0)        // If the LSB is set
                {
                    crc >>= 1;                    // Shift right and XOR 0xA001
                    crc ^= 0xA001;
                }
                else                            // Else LSB is not set
                {
                    crc >>= 1;                    // Just shift right
                }
            }
        }
        // Note, this number has low and high bytes swapped, so use it accordingly (or swap bytes)
        return crc;
    }

}

