using System;
using System.Collections;
using System.Linq;
using System.Threading;

namespace adevices
{
    /// <summary>
    /// Relay Class
    /// </summary>
    public class Relay
    {
        public static string        SetRelaySync = "SetRelaySync";
        public string               SerialId { set; get; }
        private static readonly int MAX_PORT_SIZE = 8;
        public ArrayList            m_Ports = new ArrayList();
        public static ArrayList     Relays = new ArrayList();
        public static bool          LoadingRelays = false;
        private FTDI                m_Relay = null;
        public static int           RelaysOpened = 0;
        public string               RelayDescription { set; get; }
        public static string        relayLockerStatus = "";


        public Relay(string serialNumberId, string newDescription)
        {
            this.SerialId = serialNumberId;
            this.RelayDescription = newDescription;
            for (short i = 1; i <= MAX_PORT_SIZE; i++)
            {
                RelayPort tmp = new RelayPort();
                tmp.PortNumber = i;
                m_Ports.Add(tmp);
            }
            UpdatePortsStatus();
        }

        /// <summary>
        /// getPortByIndex
        /// </summary>
        /// <param name="portIndex"></param>
        /// <returns></returns>
        public RelayPort getPortByIndex(short portIndex)
        {
            foreach (RelayPort tmp in m_Ports)
            {
                if (tmp.PortNumber == portIndex)
                    return tmp;
            }
            return null;
        }

        /// <summary>
        /// Update ports status
        /// </summary>
        public bool UpdatePortsStatus()
        {
            try
            {
                int[] bits = Relay.GetRelayPortStatus(this.SerialId);
                short iterator = 0;
                if (bits != null)
                {
                    foreach (int bit in bits)
                    {
                        iterator++;
                        RelayPort tmp = this.getPortByIndex(iterator);
                        tmp.PortStatus = (bit == 1) ? true : false;

                    }
                }
            }
            catch (Exception ex)
            {
                Common.AddToLog("[UpdatePortsStatus][" + this.SerialId + "]" + ex.Message);
                return false;
            }
            return true;
        }
        

        private void OpenRelay()
        {
            this.m_Relay = Relay.OpenRelay(this.SerialId);
        }

        private void CloseRelay()
        {
            if (this.m_Relay != null)
            {
                Relay.CloseRelay(this.m_Relay);
            }
            else
            {
                Common.AddToLog("[CloseRelay] Relay object is null");
            }
        }

        private static string openRelayLocker = "openRelayLocker";
        public static ArrayList openedRelays = new ArrayList();

        /// <summary>
        /// Open Relay function
        /// </summary>
        /// <param name="relaySerial">Relay serial number</param>
        /// <returns></returns>
        public static FTDI OpenRelay(string relaySerial)
        {
            if (relaySerial == null || relaySerial.Equals(""))
            {
                Common.AddToErrors("[CRITICAL][OpenRelay] bad serial number !!!");
                Thread.Sleep(3);
                return null;
            }
            FTDI.FT_STATUS ftStatus;
            FTDI newRelay = new FTDI();
            try
            {
                bool locked = false;
                int count = 0;
                while(!locked)
                {
                    lock (openRelayLocker)
                    {
                        if (!openedRelays.Contains(relaySerial))
                        {
                            openedRelays.Add(relaySerial);
                            locked = true;
                        }
                    }
                    if (!locked)
                    {
                        count++;
                        Thread.Sleep(10);
                        if (count >= 100)
                        {
                            
                            lock (openRelayLocker)
                            {

                                if (openedRelays.Contains(relaySerial))
                                {
                                    openedRelays.Remove(relaySerial);
                                    Relay.RelaysOpened--;
                                }
                            }
                        }
                    }
                }
                if (newRelay.OpenBySerialNumber(relaySerial) != FTDI.FT_STATUS.FT_OK)
                {
                    Console.WriteLine("relay.OpenBySerialNumber() failed.");
                }

                ftStatus = newRelay.SetBaudRate(921600);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {

                    Common.AddToErrors("Failed to set baudrate (error " + ftStatus + ")");
                    throw new Exception("Failed to set baudrate (error " + ftStatus + ")");
                }

                ftStatus = newRelay.SetBitMode(255, 4);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    Common.AddToErrors("Failed to set bit mode (error " + ftStatus + ")" + "Device opened:" + Relay.RelaysOpened);
                    throw new Exception("Failed to set bit mode (error " + ftStatus + ")");
                }
                Relay.RelaysOpened++;
            }
            catch (Exception failedToOpenRelay)
            {
                Console.WriteLine(failedToOpenRelay.Message);
                return null;
            }
            return newRelay;
        }

        /// <summary>
        /// CloseRelay
        /// </summary>
        /// <param name="relay"></param>
        public static void CloseRelay(FTDI relay)
        {
            try
            {

                string deviceSerialNum = "";
                relay.GetSerialNumber(out deviceSerialNum);
                //Console.WriteLine(" <-- CloseRelay: " + deviceSerialNum);

                bool pass = false;
                int counter = 0;
                while (!pass && counter<20)
                {
                    try
                    {
                        if (relay.Close() != FTDI.FT_STATUS.FT_OK)
                        {
                            Common.AddToLog("[CloseRelay] relay.Close() failed.");
                            Thread.Sleep(2);
                            counter++;
                        }
                        else
                        {
                            pass = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        Common.AddToLog("ERROR:----------------------[CloseRelay] relay.Close() failed." + ex.Message);
                    }
                }

                lock (openRelayLocker)
                {
                    Relay.RelaysOpened--;
                    lock (openRelayLocker)
                    {
                        if (openedRelays.Contains(deviceSerialNum))
                        {
                            openedRelays.Remove(deviceSerialNum);
                        }
                    }
                }
                    
 
            }
            catch (Exception ex)
            {
                Common.AddToLog("[CloseRelay]" + ex.Message);
                Common.AddToErrors("[CloseRelay]" + ex.Message);
            }
        }

        /// <summary>
        /// calculates a number to a power of another power
        /// </summary>
        /// <param name="baseNum"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        private static int Power(int baseNum, int exp)
        {
            int ret = 0;
            try
            {
                //handle to the power of zero and 1;
                if (exp == 0)
                    return 1;
                if (exp == 1)
                    return baseNum;
                //higher numbers
                ret = baseNum;
                for (int i = 1; i < exp; i++)
                {
                    ret = ret * baseNum;
                }
            }
            catch (Exception ex)
            {
                Common.AddToLog("[Power]" + ex.Message);
                Common.AddToErrors("[Power]" + ex.Message);
            }

            return ret;
        }

        /// <summary>
        /// GetRelayStatus
        /// </summary>
        /// <param name="relay"></param>
        /// <returns></returns>
        public static uint GetRelayStatus(FTDI relay)
        {
            byte relayStatus = 0;
            try
            {
                relay.GetPinStates(ref relayStatus);
            }
            catch (Exception relayNotOpen)
            {
                Console.WriteLine(relayNotOpen.Message);
                return 260;
            }
            return (uint)relayStatus;
        }

        /// <summary>
        /// CheckIfRelayExistInDbById
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckIfRelayExistInDbById(string id)
        {
            foreach (Relay tmp in Relays)
            {
                if (tmp.SerialId == id)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// getRelayById
        /// </summary>
        /// <param name="relayId"></param>
        /// <returns></returns>
        public static Relay getRelayById(string relayId)
        {
            foreach (Relay tmp in Relay.Relays)
            {
                if (tmp.SerialId == relayId)
                {
                    return tmp;
                }
            }
            return null;
        }


        public static bool GetBit(byte b, int bitNumber)
        {
            return (b & (1 << bitNumber)) != 0;
        }

        /// <summary>
        /// handles complete atomic setting of a relay
        /// </summary>
        /// <param name="relaySerial"></param>
        /// <param name="relayPort"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public static bool SetRelayPort(string relaySerial, int relayPort, bool status)
        {

            if (relayPort == -1)
            {
                return true;
            }
            byte[] sentBytes = new byte[2];
            uint receivedBytes = 0;
            FTDI activeRelay = null;
            const int MAX_OPEN_TRYS = 10;
            int counter = 0;
            if (relaySerial == null || relaySerial.Equals(""))
            {
                Common.AddToErrors("Cannot open relay " + relaySerial + " Port=" + relayPort);
                return false;
            }
            lock (SetRelaySync)
            {
                //Common.AddToLog("Openning relay  " + relaySerial + " Port=" + relayPort);
                activeRelay = OpenRelay(relaySerial);
                while(activeRelay == null && counter <= MAX_OPEN_TRYS)
                {
                    activeRelay = OpenRelay(relaySerial);
                    Thread.Sleep(100);
                    counter++;
                }
                if (activeRelay == null)
                {
                    //Common.AddToErrors("Cannot open relay " + relaySerial + " Port=" + relayPort);
                    return false;
                }
                lock (relayLockerStatus)
                {
                    try
                    {
                        //get current relay status
                        byte currentStatus = (byte)GetRelayStatus(activeRelay);

                        // calculate the exact commands to be sent
                        int command = Power(2, relayPort - 1);
                        //XOR between current status and requested bit
                        sentBytes[0] = (byte)(command ^ currentStatus);

                        Relay tmpRelay = Relay.getRelayById(relaySerial);

                        for (short i = 1; i <= 8; i++)
                        {
                            RelayPort tmp = tmpRelay.getPortByIndex(i);
                            tmp.PortStatus = (GetBit(sentBytes[0],i-1)) ? true : false;
                            
                        }

                        FTDI.FT_STATUS ftdiStatus = activeRelay.Write(sentBytes, 1, ref receivedBytes);

                        if (ftdiStatus == FTDI.FT_STATUS.FT_OK)
                            return true;
                        else
                        {
                            Common.AddToLog("Bad status: " + ftdiStatus);
                            Common.AddToErrors("Bad status: " + ftdiStatus);
                            return false;
                        }
                    }
                    catch (Exception relayNotOpen)
                    {
                        Common.AddToLog("Bad status: " + relayNotOpen.Message);
                        Common.AddToErrors("Bad status: " + relayNotOpen.Message);
                    }
                    finally
                    {
                        CloseRelay(activeRelay);
                    }
                }
            }
            return true ;
        }

        /// <summary>
        /// GetRelayPortStatus
        /// </summary>
        /// <param name="relayId"></param>
        /// <returns></returns>
        private static int[] GetRelayPortStatus(string relayId)
        {
            int[] bits = null;
            try
            {
                uint status;
                lock (relayLockerStatus)
                {
                    FTDI relay = OpenRelay(relayId);
                    status = GetRelayStatus(relay);
                    CloseRelay(relay);
                }
                string s = Convert.ToString(status, 2); //Convert to binary in a string

                bits = s.PadLeft(8, '0') // Add 0's from left
                             .Select(c => int.Parse(c.ToString())) // convert each char to int
                             .ToArray(); // Convert IEnumerable from select to Array
                Array.Reverse(bits);
            }
            catch (Exception)
            {
            }
            return bits;
        }

        public static string[] serialsArray = null;

        /// <summary>
        /// Load relays existed in PC
        /// </summary>
        public static void LoadRelaysThread()
        {
            try
            {
                ArrayList serials               = new ArrayList();
                FTDI relay                      = new FTDI();
                FTDI.FT_DEVICE_INFO_NODE[] list = new FTDI.FT_DEVICE_INFO_NODE[10];
                relay.GetDeviceList(list);
                foreach (FTDI.FT_DEVICE_INFO_NODE tmp in list)
                {
                    if (tmp != null)
                    {
                        serials.Add(tmp.SerialNumber);
                        if (tmp.SerialNumber != "" && !Relay.CheckIfRelayExistInDbById(tmp.SerialNumber)){
                            Relay.Relays.Add(new Relay(tmp.SerialNumber,tmp.Description));
                        }

                    }
                }

                serialsArray = new string[serials.Count];
                int i = 0;
                foreach (string serial in serials)
                {
                    serialsArray[i] = serial;
                    i++;
                }
                Relay.LoadingRelays = false;
            }
            catch (Exception ex)
            {
                Common.AddToErrors("[LoadRelaysThread]" + ex.Message);
            }
            finally
            {
                Relay.LoadingRelays = false;
            }
        }
    }
}
