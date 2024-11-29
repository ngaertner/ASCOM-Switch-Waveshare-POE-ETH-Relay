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
        
        


        public WaveShareRelay(short DeviceType, short DeviceProtocol )
        {
            deviceProtocol = DeviceProtocol;
            deviceType = DeviceType;
        }

    internal bool Connect(string ipAddress, int ipPort)
        {
            soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IAsyncResult result = soc.BeginConnect(ipAddress, ipPort, null, null);
            bool success = result.AsyncWaitHandle.WaitOne(1000, true);

            if (soc.Connected)
            {
                soc.EndConnect(result);
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
            if (soc != null && soc.Connected) {

            soc.Close();

            soc.Shutdown(SocketShutdown.Both);
            soc.Disconnect(true);

        }
        connected = false;
        }

        internal  bool Connected()
        {
             return connected;
        }

        internal  void SetRelayState(short Id)
        {

        }

        internal void SetRelayMode(short Id, short Mode)
        {
        byte[] command;

        switch (Mode)
        {
            case SwitchModeNormal:
                command = commandModeNormal;
                relayReadonly[Id] = false;
                break;
            case SwitchModeLinkage:
                command = commandModeLinkage;
                relayReadonly[Id] = true;
                break;
            case SwitchModeJump:
                command = commandModeJump;
                relayReadonly[Id] = false;
                break;
            case SwitchModeToggle:
                command = commandModeToggle;
                relayReadonly[Id] = false;
                break;
            default:
                command = commandModeNormal;
                relayReadonly[Id] = false;
                break;
        }

        //modify 10th byte of the command to set the requested switch number
        command[9] = relayAddresses[Id];

        SendCommand(command);

    }

    internal bool GetRelayState(short Id)
        {
            return relayStates[Id];
        }

        internal bool GetInputState(short Id)
        {
            return inputStates[Id];        
        }

        internal void RefreshStates(bool ForceRefresh = false)
        {
            TimeSpan refreshDiffMilliseconds = DateTime.Now - lastRefreshTime;

        try
        {
            // initialize correct switch state with the actual device state
            if (refreshDiffMilliseconds.TotalMilliseconds > 500 || ForceRefresh == true)
            {

                lastRefreshTime = DateTime.Now;

                var buffer = SendCommand(commandRelayStatus);

                for (int n = 0; n < GetRelayCount() - 1; n++)
                {
                      relayStates[n] = testAbs.GetBit(buffer[9], n + 1);
                }

                if (deviceType == DeviceTypeETH8CHB)
                {
                    soc.Send(commandInputStatus);
                    soc.Receive(buffer);

                    for (int n = 0; n < GetInputCount() -1; n++)
                    {
                        inputStates[n] = testAbs.GetBit(buffer[9], n + 1);
                    }

                }

            }
        }
        catch
        {

//            throw new DriverException($"Switch could not be read: {id}");

        };

    }


    internal short GetInputCount()
        {
            return 8;
        }

        internal short GetRelayCount()
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


        internal byte[] SendCommand(byte[] Command )
        {
            soc.ReceiveTimeout = 1000;
            soc.Send(Command);
            var buffer = new byte[12];
            soc.Receive(buffer);
            return buffer;
    }


    }
//}