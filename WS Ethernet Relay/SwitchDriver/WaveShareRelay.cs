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

//namespace ASCOM.LocalServer.Server.SwitchDriver
//{
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

    internal static byte[] commandRelayStatus = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x01, 0x00, 0x00, 0x00, 0x08 }; // 01 01 00 00 00 08 3D CC
    internal static byte[] commandInputStatus = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x02, 0x00, 0x00, 0x00, 0x08 }; // 01 02 00 00 00 08 79 CC
    internal static byte[] commandSwitchOnRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x00, 0x00, 0xFF, 0x00 };
    internal static byte[] commandSwitchOffRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x00, 0x00, 0x00, 0x00 };
    internal static byte[] commandFlashOnRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x02, 0x00, 0x00, 0x03 }; // 01 05 02 00 00 05 8D B0
    internal static byte[] commandFlashOffRelay = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x05, 0x04, 0x00, 0x00, 0x03 }; // 01 05 04 00 00 05 8D B0
    internal static byte[] commandModeNormal = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x00 }; //01 06 10 00 00 00 4C CA
    internal static byte[] commandModeLinkage = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x01 }; //01 06 10 00 00 01 4C CA
    internal static byte[] commandModeToggle = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x02 }; //01 06 10 00 00 02 4C CA
    internal static byte[] commandModeJump = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x06, 0x01, 0x06, 0x10, 0x00, 0x00, 0x03 }; //01 06 10 00 00 03 4C CA

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
            soc.Close();
            connected = false;
        }

        connected = true;

        lastRefreshTime = DateTime.MinValue;


        return connected;

    }

    internal void Disconnect()
    {
        if (soc != null && soc.Connected)
        {

            if (asyncSocketHandle != null)
            {
                soc.EndConnect(asyncSocketHandle);
            }
            soc.Close();
            soc.Shutdown(SocketShutdown.Both);
            soc.Disconnect(true);
        }

        soc.Dispose();
        soc = null;
        connected = false;
    }

    internal bool Connected()
    {

        if (soc == null)
            connected = false;
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

        byte[] command;
        if (flash)
        {
            if (state)
            {
                command = commandFlashOnRelay;

            }
            else
            {
                command = commandFlashOffRelay;
            }
        }
        else
        {
            if (state)
            {
                command = commandSwitchOnRelay;
            }
            else
            {
                command = commandSwitchOffRelay;
            }
        }

        //modify 10th byte of the command to set the requested switch number
        command[9] = relayAddresses[id];

        if (SendCommand(command) == null)
        {
            return false;
        }

        RefreshStates(true);
        return true;
    }

    internal bool SetRelayMode(short id, short mode)
    {
        byte[] command;

        switch (mode)
        {
            case SwitchModeNormal:
                command = commandModeNormal;
                relayReadonly[id] = false;
                break;
            case SwitchModeLinkage:
                command = commandModeLinkage;
                relayReadonly[id] = true;
                break;
            case SwitchModeJump:
                command = commandModeJump;
                relayReadonly[id] = false;
                break;
            case SwitchModeToggle:
                command = commandModeToggle;
                relayReadonly[id] = false;
                break;
            default:
                command = commandModeNormal;
                relayReadonly[id] = false;
                break;
        }

        //modify 10th byte of the command to set the requested switch number
        command[9] = relayAddresses[id];

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

    internal bool RefreshStates(bool ForceRefresh = false)
    {
        byte[] buffer;
        
        TimeSpan refreshDiffMilliseconds = DateTime.Now - lastRefreshTime;
        
        try
        {
            // initialize correct switch state with the actual device state
            if (refreshDiffMilliseconds.TotalMilliseconds > 200 || ForceRefresh == true)
            {


                lastRefreshTime = DateTime.Now;

                buffer = SendCommand(commandRelayStatus);

                if (buffer == null)
                {
                    return false;
                }

                for (int n = 0; n < GetRelayCount(); n++)
                {
                    relayStates[n] = testAbs.GetBit(buffer[9], n + 1);
                }

                if (deviceType == DeviceTypeETH8CHB)
                {
                    buffer = SendCommand(commandInputStatus);
                    if (buffer == null)
                    {
                        return false;
                    }

                    for (int n = 0; n < GetInputCount(); n++)
                    {
                        inputStates[n] = testAbs.GetBit(buffer[9], n + 1);
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


    internal byte[] SendCommand(byte[] Command)
    {
        if (Connected()) {

        
        var buffer = new byte[1024];
        try
        {
            soc.ReceiveTimeout = 1000;
            soc.Send(Command);
            soc.Receive(buffer);
            return buffer;

            }
            catch
        {
            MessageBox.Show("FATAL: Socket Send Error!");
            return null; }

        } else { return null; }

    }

}
