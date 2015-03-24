using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace adevices
{


    public class FTDI
    {
        private const int FT_COM_PORT_NOT_ASSIGNED = -1;
        private const uint FT_DEFAULT_BAUD_RATE = 0x2580;
        private const uint FT_DEFAULT_DEADMAN_TIMEOUT = 0x1388;
        private const uint FT_DEFAULT_DEVICE_ID = 0x4036001;
        private const uint FT_DEFAULT_IN_TRANSFER_SIZE = 0x1000;
        private const byte FT_DEFAULT_LATENCY = 0x10;
        private const uint FT_DEFAULT_OUT_TRANSFER_SIZE = 0x1000;
        private const uint FT_OPEN_BY_DESCRIPTION = 2;
        private const uint FT_OPEN_BY_LOCATION = 4;
        private const uint FT_OPEN_BY_SERIAL_NUMBER = 1;
        private IntPtr ftHandle = IntPtr.Zero;
        private IntPtr hFTD2XXDLL = IntPtr.Zero;
        private IntPtr pFT_Close = IntPtr.Zero;
        private IntPtr pFT_ClrDtr = IntPtr.Zero;
        private IntPtr pFT_ClrRts = IntPtr.Zero;
        private IntPtr pFT_CreateDeviceInfoList = IntPtr.Zero;
        private IntPtr pFT_CyclePort = IntPtr.Zero;
        private IntPtr pFT_EE_Program = IntPtr.Zero;
        private IntPtr pFT_EE_Read = IntPtr.Zero;
        private IntPtr pFT_EE_UARead = IntPtr.Zero;
        private IntPtr pFT_EE_UASize = IntPtr.Zero;
        private IntPtr pFT_EE_UAWrite = IntPtr.Zero;
        private IntPtr pFT_EraseEE = IntPtr.Zero;
        private IntPtr pFT_GetBitMode = IntPtr.Zero;
        private IntPtr pFT_GetComPortNumber = IntPtr.Zero;
        private IntPtr pFT_GetDeviceInfo = IntPtr.Zero;
        private IntPtr pFT_GetDeviceInfoDetail = IntPtr.Zero;
        private IntPtr pFT_GetDriverVersion = IntPtr.Zero;
        private IntPtr pFT_GetLatencyTimer = IntPtr.Zero;
        private IntPtr pFT_GetLibraryVersion = IntPtr.Zero;
        private IntPtr pFT_GetModemStatus = IntPtr.Zero;
        private IntPtr pFT_GetQueueStatus = IntPtr.Zero;
        private IntPtr pFT_GetStatus = IntPtr.Zero;
        private IntPtr pFT_Open = IntPtr.Zero;
        private IntPtr pFT_OpenEx = IntPtr.Zero;
        private IntPtr pFT_Purge = IntPtr.Zero;
        private IntPtr pFT_Read = IntPtr.Zero;
        private IntPtr pFT_ReadEE = IntPtr.Zero;
        private IntPtr pFT_Reload = IntPtr.Zero;
        private IntPtr pFT_Rescan = IntPtr.Zero;
        private IntPtr pFT_ResetDevice = IntPtr.Zero;
        private IntPtr pFT_ResetPort = IntPtr.Zero;
        private IntPtr pFT_RestartInTask = IntPtr.Zero;
        private IntPtr pFT_SetBaudRate = IntPtr.Zero;
        private IntPtr pFT_SetBitMode = IntPtr.Zero;
        private IntPtr pFT_SetBreakOff = IntPtr.Zero;
        private IntPtr pFT_SetBreakOn = IntPtr.Zero;
        private IntPtr pFT_SetChars = IntPtr.Zero;
        private IntPtr pFT_SetDataCharacteristics = IntPtr.Zero;
        private IntPtr pFT_SetDeadmanTimeout = IntPtr.Zero;
        private IntPtr pFT_SetDtr = IntPtr.Zero;
        private IntPtr pFT_SetEventNotification = IntPtr.Zero;
        private IntPtr pFT_SetFlowControl = IntPtr.Zero;
        private IntPtr pFT_SetLatencyTimer = IntPtr.Zero;
        private IntPtr pFT_SetResetPipeRetryCount = IntPtr.Zero;
        private IntPtr pFT_SetRts = IntPtr.Zero;
        private IntPtr pFT_SetTimeouts = IntPtr.Zero;
        private IntPtr pFT_SetUSBParameters = IntPtr.Zero;
        private IntPtr pFT_StopInTask = IntPtr.Zero;
        private IntPtr pFT_Write = IntPtr.Zero;
        private IntPtr pFT_WriteEE = IntPtr.Zero;

        public FTDI()
        {
            if (this.hFTD2XXDLL == IntPtr.Zero)
            {
                this.hFTD2XXDLL = LoadLibrary("FTD2XX.DLL");
                if (this.hFTD2XXDLL == IntPtr.Zero)
                {
                    MessageBox.Show("Attempting to load FTD2XX.DLL from:\n" + Path.GetDirectoryName(base.GetType().Assembly.Location));
                    this.hFTD2XXDLL = LoadLibrary(Path.GetDirectoryName(base.GetType().Assembly.Location) + @"\FTD2XX.DLL");
                }
            }
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                this.pFT_CreateDeviceInfoList = GetProcAddress(this.hFTD2XXDLL, "FT_CreateDeviceInfoList");
                this.pFT_GetDeviceInfoDetail = GetProcAddress(this.hFTD2XXDLL, "FT_GetDeviceInfoDetail");
                this.pFT_Open = GetProcAddress(this.hFTD2XXDLL, "FT_Open");
                this.pFT_OpenEx = GetProcAddress(this.hFTD2XXDLL, "FT_OpenEx");
                this.pFT_Close = GetProcAddress(this.hFTD2XXDLL, "FT_Close");
                this.pFT_Read = GetProcAddress(this.hFTD2XXDLL, "FT_Read");
                this.pFT_Write = GetProcAddress(this.hFTD2XXDLL, "FT_Write");
                this.pFT_GetQueueStatus = GetProcAddress(this.hFTD2XXDLL, "FT_GetQueueStatus");
                this.pFT_GetModemStatus = GetProcAddress(this.hFTD2XXDLL, "FT_GetModemStatus");
                this.pFT_GetStatus = GetProcAddress(this.hFTD2XXDLL, "FT_GetStatus");
                this.pFT_SetBaudRate = GetProcAddress(this.hFTD2XXDLL, "FT_SetBaudRate");
                this.pFT_SetDataCharacteristics = GetProcAddress(this.hFTD2XXDLL, "FT_SetDataCharacteristics");
                this.pFT_SetFlowControl = GetProcAddress(this.hFTD2XXDLL, "FT_SetFlowControl");
                this.pFT_SetDtr = GetProcAddress(this.hFTD2XXDLL, "FT_SetDtr");
                this.pFT_ClrDtr = GetProcAddress(this.hFTD2XXDLL, "FT_ClrDtr");
                this.pFT_SetRts = GetProcAddress(this.hFTD2XXDLL, "FT_SetRts");
                this.pFT_ClrRts = GetProcAddress(this.hFTD2XXDLL, "FT_ClrRts");
                this.pFT_ResetDevice = GetProcAddress(this.hFTD2XXDLL, "FT_ResetDevice");
                this.pFT_ResetPort = GetProcAddress(this.hFTD2XXDLL, "FT_ResetPort");
                this.pFT_CyclePort = GetProcAddress(this.hFTD2XXDLL, "FT_CyclePort");
                this.pFT_Rescan = GetProcAddress(this.hFTD2XXDLL, "FT_Rescan");
                this.pFT_Reload = GetProcAddress(this.hFTD2XXDLL, "FT_Reload");
                this.pFT_Purge = GetProcAddress(this.hFTD2XXDLL, "FT_Purge");
                this.pFT_SetTimeouts = GetProcAddress(this.hFTD2XXDLL, "FT_SetTimeouts");
                this.pFT_SetBreakOn = GetProcAddress(this.hFTD2XXDLL, "FT_SetBreakOn");
                this.pFT_SetBreakOff = GetProcAddress(this.hFTD2XXDLL, "FT_SetBreakOff");
                this.pFT_GetDeviceInfo = GetProcAddress(this.hFTD2XXDLL, "FT_GetDeviceInfo");
                this.pFT_SetResetPipeRetryCount = GetProcAddress(this.hFTD2XXDLL, "FT_SetResetPipeRetryCount");
                this.pFT_StopInTask = GetProcAddress(this.hFTD2XXDLL, "FT_StopInTask");
                this.pFT_RestartInTask = GetProcAddress(this.hFTD2XXDLL, "FT_RestartInTask");
                this.pFT_GetDriverVersion = GetProcAddress(this.hFTD2XXDLL, "FT_GetDriverVersion");
                this.pFT_GetLibraryVersion = GetProcAddress(this.hFTD2XXDLL, "FT_GetLibraryVersion");
                this.pFT_SetDeadmanTimeout = GetProcAddress(this.hFTD2XXDLL, "FT_SetDeadmanTimeout");
                this.pFT_SetChars = GetProcAddress(this.hFTD2XXDLL, "FT_SetChars");
                this.pFT_SetEventNotification = GetProcAddress(this.hFTD2XXDLL, "FT_SetEventNotification");
                this.pFT_GetComPortNumber = GetProcAddress(this.hFTD2XXDLL, "FT_GetComPortNumber");
                this.pFT_SetLatencyTimer = GetProcAddress(this.hFTD2XXDLL, "FT_SetLatencyTimer");
                this.pFT_GetLatencyTimer = GetProcAddress(this.hFTD2XXDLL, "FT_GetLatencyTimer");
                this.pFT_SetBitMode = GetProcAddress(this.hFTD2XXDLL, "FT_SetBitMode");
                this.pFT_GetBitMode = GetProcAddress(this.hFTD2XXDLL, "FT_GetBitMode");
                this.pFT_SetUSBParameters = GetProcAddress(this.hFTD2XXDLL, "FT_SetUSBParameters");
                this.pFT_ReadEE = GetProcAddress(this.hFTD2XXDLL, "FT_ReadEE");
                this.pFT_WriteEE = GetProcAddress(this.hFTD2XXDLL, "FT_WriteEE");
                this.pFT_EraseEE = GetProcAddress(this.hFTD2XXDLL, "FT_EraseEE");
                this.pFT_EE_UASize = GetProcAddress(this.hFTD2XXDLL, "FT_EE_UASize");
                this.pFT_EE_UARead = GetProcAddress(this.hFTD2XXDLL, "FT_EE_UARead");
                this.pFT_EE_UAWrite = GetProcAddress(this.hFTD2XXDLL, "FT_EE_UAWrite");
                this.pFT_EE_Read = GetProcAddress(this.hFTD2XXDLL, "FT_EE_Read");
                this.pFT_EE_Program = GetProcAddress(this.hFTD2XXDLL, "FT_EE_Program");
            }
            else
            {
                MessageBox.Show("Failed to load FTD2XX.DLL.  Are the FTDI drivers installed?");
            }
        }

        public FT_STATUS Close()
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Close != IntPtr.Zero)
                {
                    tFT_Close delegateForFunctionPointer = (tFT_Close)Marshal.GetDelegateForFunctionPointer(this.pFT_Close, typeof(tFT_Close));
                    ft_status = delegateForFunctionPointer(this.ftHandle);
                    if (ft_status == FT_STATUS.FT_OK)
                    {
                        this.ftHandle = IntPtr.Zero;
                    }
                    return ft_status;
                }
                if (this.pFT_Close == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Close.");
                }
            }
            return ft_status;
        }

        public FT_STATUS CyclePort()
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((this.pFT_CyclePort != IntPtr.Zero) & (this.pFT_Close != IntPtr.Zero))
                {
                    tFT_CyclePort delegateForFunctionPointer = (tFT_CyclePort)Marshal.GetDelegateForFunctionPointer(this.pFT_CyclePort, typeof(tFT_CyclePort));
                    tFT_Close close = (tFT_Close)Marshal.GetDelegateForFunctionPointer(this.pFT_Close, typeof(tFT_Close));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle);
                        if (ft_status != FT_STATUS.FT_OK)
                        {
                            return ft_status;
                        }
                        ft_status = close(this.ftHandle);
                        if (ft_status == FT_STATUS.FT_OK)
                        {
                            this.ftHandle = IntPtr.Zero;
                        }
                    }
                    return ft_status;
                }
                if (this.pFT_CyclePort == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_CyclePort.");
                }
                if (this.pFT_Close == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Close.");
                }
            }
            return ft_status;
        }

        public FT_STATUS EEReadUserArea(byte[] UserAreaDataBuffer, ref uint numBytesRead)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((this.pFT_EE_UASize != IntPtr.Zero) & (this.pFT_EE_UARead != IntPtr.Zero))
                {
                    tFT_EE_UASize delegateForFunctionPointer = (tFT_EE_UASize)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_UASize, typeof(tFT_EE_UASize));
                    tFT_EE_UARead read = (tFT_EE_UARead)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_UARead, typeof(tFT_EE_UARead));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        uint dwSize = 0;
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref dwSize);
                        if (UserAreaDataBuffer.Length >= dwSize)
                        {
                            ft_status = read(this.ftHandle, UserAreaDataBuffer, UserAreaDataBuffer.Length, ref numBytesRead);
                        }
                    }
                    return ft_status;
                }
                if (this.pFT_EE_UASize == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_UASize.");
                }
                if (this.pFT_EE_UARead == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_UARead.");
                }
            }
            return ft_status;
        }

        public FT_STATUS EEUserAreaSize(ref uint UASize)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_UASize != IntPtr.Zero)
                {
                    tFT_EE_UASize delegateForFunctionPointer = (tFT_EE_UASize)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_UASize, typeof(tFT_EE_UASize));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref UASize);
                    }
                    return ft_status;
                }
                if (this.pFT_EE_UASize == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_UASize.");
                }
            }
            return ft_status;
        }

        public FT_STATUS EEWriteUserArea(byte[] UserAreaDataBuffer)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((this.pFT_EE_UASize != IntPtr.Zero) & (this.pFT_EE_UAWrite != IntPtr.Zero))
                {
                    tFT_EE_UASize delegateForFunctionPointer = (tFT_EE_UASize)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_UASize, typeof(tFT_EE_UASize));
                    tFT_EE_UAWrite write = (tFT_EE_UAWrite)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_UAWrite, typeof(tFT_EE_UAWrite));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        uint dwSize = 0;
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref dwSize);
                        if (UserAreaDataBuffer.Length <= dwSize)
                        {
                            ft_status = write(this.ftHandle, UserAreaDataBuffer, UserAreaDataBuffer.Length);
                        }
                    }
                    return ft_status;
                }
                if (this.pFT_EE_UASize == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_UASize.");
                }
                if (this.pFT_EE_UAWrite == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_UAWrite.");
                }
            }
            return ft_status;
        }

        public FT_STATUS EraseEEPROM()
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EraseEE != IntPtr.Zero)
                {
                    tFT_EraseEE delegateForFunctionPointer = (tFT_EraseEE)Marshal.GetDelegateForFunctionPointer(this.pFT_EraseEE, typeof(tFT_EraseEE));
                    if (!(this.ftHandle != IntPtr.Zero))
                    {
                        return ftStatus;
                    }
                    FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                    this.GetDeviceType(ref deviceType);
                    if (deviceType == FT_DEVICE.FT_DEVICE_232R)
                    {
                        ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                        this.ErrorHandler(ftStatus, ftErrorCondition);
                    }
                    return delegateForFunctionPointer(this.ftHandle);
                }
                if (this.pFT_EraseEE == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EraseEE.");
                }
            }
            return ftStatus;
        }

        private void ErrorHandler(FT_STATUS ftStatus, FT_ERROR ftErrorCondition)
        {
            switch (ftStatus)
            {
                case FT_STATUS.FT_INVALID_HANDLE:
                    throw new FT_EXCEPTION("Invalid handle for FTDI device.");

                case FT_STATUS.FT_DEVICE_NOT_FOUND:
                    throw new FT_EXCEPTION("FTDI device not found.");

                case FT_STATUS.FT_DEVICE_NOT_OPENED:
                    throw new FT_EXCEPTION("FTDI device not opened.");

                case FT_STATUS.FT_IO_ERROR:
                    throw new FT_EXCEPTION("FTDI device IO error.");

                case FT_STATUS.FT_INSUFFICIENT_RESOURCES:
                    throw new FT_EXCEPTION("Insufficient resources.");

                case FT_STATUS.FT_INVALID_PARAMETER:
                    throw new FT_EXCEPTION("Invalid parameter for FTD2XX function call.");

                case FT_STATUS.FT_INVALID_BAUD_RATE:
                    throw new FT_EXCEPTION("Invalid Baud rate for FTDI device.");

                case FT_STATUS.FT_DEVICE_NOT_OPENED_FOR_ERASE:
                    throw new FT_EXCEPTION("FTDI device not opened for erase.");

                case FT_STATUS.FT_DEVICE_NOT_OPENED_FOR_WRITE:
                    throw new FT_EXCEPTION("FTDI device not opened for write.");

                case FT_STATUS.FT_FAILED_TO_WRITE_DEVICE:
                    throw new FT_EXCEPTION("Failed to write to FTDI device.");

                case FT_STATUS.FT_EEPROM_READ_FAILED:
                    throw new FT_EXCEPTION("Failed to read FTDI device EEPROM.");

                case FT_STATUS.FT_EEPROM_WRITE_FAILED:
                    throw new FT_EXCEPTION("Failed to write FTDI device EEPROM.");

                case FT_STATUS.FT_EEPROM_ERASE_FAILED:
                    throw new FT_EXCEPTION("Failed to erase FTDI device EEPROM.");

                case FT_STATUS.FT_EEPROM_NOT_PRESENT:
                    throw new FT_EXCEPTION("No EEPROM fitted to FTDI device.");

                case FT_STATUS.FT_EEPROM_NOT_PROGRAMMED:
                    throw new FT_EXCEPTION("FTDI device EEPROM not programmed.");

                case FT_STATUS.FT_INVALID_ARGS:
                    throw new FT_EXCEPTION("Invalid arguments for FTD2XX function call.");

                case FT_STATUS.FT_OTHER_ERROR:
                    throw new FT_EXCEPTION("An unexpected error has occurred when trying to communicate with the FTDI device.");
            }
            switch (ftErrorCondition)
            {
                case FT_ERROR.FT_INCORRECT_DEVICE:
                    throw new FT_EXCEPTION("The current device type does not match the EEPROM structure.");

                case FT_ERROR.FT_INVALID_BITMODE:
                    throw new FT_EXCEPTION("The requested bit mode is not valid for the current device.");

                case FT_ERROR.FT_BUFFER_SIZE:
                    throw new FT_EXCEPTION("The supplied buffer is not big enough.");

                case FT_ERROR.FT_NO_ERROR:
                    return;
            }
        }

        ~FTDI()
        {
            FreeLibrary(this.hFTD2XXDLL);
            this.hFTD2XXDLL = IntPtr.Zero;
        }

        [DllImport("kernel32.dll")]
        private static extern bool FreeLibrary(IntPtr hModule);
        public FT_STATUS GetCOMPort(out string ComPortName)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            ComPortName = string.Empty;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetComPortNumber != IntPtr.Zero)
                {
                    tFT_GetComPortNumber delegateForFunctionPointer = (tFT_GetComPortNumber)Marshal.GetDelegateForFunctionPointer(this.pFT_GetComPortNumber, typeof(tFT_GetComPortNumber));
                    int dwComPortNumber = -1;
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref dwComPortNumber);
                    }
                    if (dwComPortNumber == -1)
                    {
                        ComPortName = string.Empty;
                        return ft_status;
                    }
                    ComPortName = "COM" + dwComPortNumber.ToString();
                    return ft_status;
                }
                if (this.pFT_GetComPortNumber == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetComPortNumber.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetDescription(out string Description)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            Description = string.Empty;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetDeviceInfo != IntPtr.Zero)
                {
                    tFT_GetDeviceInfo delegateForFunctionPointer = (tFT_GetDeviceInfo)Marshal.GetDelegateForFunctionPointer(this.pFT_GetDeviceInfo, typeof(tFT_GetDeviceInfo));
                    uint lpdwID = 0;
                    FT_DEVICE pftType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                    byte[] pcSerialNumber = new byte[0x10];
                    byte[] pcDescription = new byte[0x40];
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref pftType, ref lpdwID, pcSerialNumber, pcDescription, IntPtr.Zero);
                        Description = Encoding.ASCII.GetString(pcDescription);
                        Description = Description.Substring(0, Description.IndexOf("\0"));
                    }
                    return ft_status;
                }
                if (this.pFT_GetDeviceInfo == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetDeviceID(ref uint DeviceID)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetDeviceInfo != IntPtr.Zero)
                {
                    tFT_GetDeviceInfo delegateForFunctionPointer = (tFT_GetDeviceInfo)Marshal.GetDelegateForFunctionPointer(this.pFT_GetDeviceInfo, typeof(tFT_GetDeviceInfo));
                    FT_DEVICE pftType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                    byte[] pcSerialNumber = new byte[0x10];
                    byte[] pcDescription = new byte[0x40];
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref pftType, ref DeviceID, pcSerialNumber, pcDescription, IntPtr.Zero);
                    }
                    return ft_status;
                }
                if (this.pFT_GetDeviceInfo == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetDeviceList(FT_DEVICE_INFO_NODE[] devicelist)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((this.pFT_CreateDeviceInfoList != IntPtr.Zero) & (this.pFT_GetDeviceInfoDetail != IntPtr.Zero))
                {
                    uint numdevs = 0;
                    tFT_CreateDeviceInfoList delegateForFunctionPointer = (tFT_CreateDeviceInfoList)Marshal.GetDelegateForFunctionPointer(this.pFT_CreateDeviceInfoList, typeof(tFT_CreateDeviceInfoList));
                    tFT_GetDeviceInfoDetail detail = (tFT_GetDeviceInfoDetail)Marshal.GetDelegateForFunctionPointer(this.pFT_GetDeviceInfoDetail, typeof(tFT_GetDeviceInfoDetail));
                    ftStatus = delegateForFunctionPointer(ref numdevs);
                    byte[] serialnumber = new byte[0x10];
                    byte[] description = new byte[0x40];
                    if (numdevs > 0)
                    {
                        if (devicelist.Length < numdevs)
                        {
                            ftErrorCondition = FT_ERROR.FT_BUFFER_SIZE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        for (uint i = 0; i < numdevs; i++)
                        {
                            devicelist[i] = new FT_DEVICE_INFO_NODE();
                            ftStatus = detail(i, ref devicelist[i].Flags, ref devicelist[i].Type, ref devicelist[i].ID, ref devicelist[i].LocId, serialnumber, description, ref devicelist[i].ftHandle);
                            devicelist[i].SerialNumber = Encoding.ASCII.GetString(serialnumber);
                            devicelist[i].Description = Encoding.ASCII.GetString(description);
                            devicelist[i].SerialNumber = devicelist[i].SerialNumber.Substring(0, devicelist[i].SerialNumber.IndexOf("\0"));
                            devicelist[i].Description = devicelist[i].Description.Substring(0, devicelist[i].Description.IndexOf("\0"));
                        }
                    }
                    return ftStatus;
                }
                if (this.pFT_CreateDeviceInfoList == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_CreateDeviceInfoList.");
                }
                if (this.pFT_GetDeviceInfoDetail == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetDeviceInfoListDetail.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS GetDeviceType(ref FT_DEVICE DeviceType)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetDeviceInfo != IntPtr.Zero)
                {
                    tFT_GetDeviceInfo delegateForFunctionPointer = (tFT_GetDeviceInfo)Marshal.GetDelegateForFunctionPointer(this.pFT_GetDeviceInfo, typeof(tFT_GetDeviceInfo));
                    uint lpdwID = 0;
                    byte[] pcSerialNumber = new byte[0x10];
                    byte[] pcDescription = new byte[0x40];
                    DeviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref DeviceType, ref lpdwID, pcSerialNumber, pcDescription, IntPtr.Zero);
                    }
                    return ft_status;
                }
                if (this.pFT_GetDeviceInfo == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetDriverVersion(ref uint DriverVersion)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetDriverVersion != IntPtr.Zero)
                {
                    tFT_GetDriverVersion delegateForFunctionPointer = (tFT_GetDriverVersion)Marshal.GetDelegateForFunctionPointer(this.pFT_GetDriverVersion, typeof(tFT_GetDriverVersion));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref DriverVersion);
                    }
                    return ft_status;
                }
                if (this.pFT_GetDriverVersion == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetDriverVersion.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetEventType(ref uint EventType)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetStatus != IntPtr.Zero)
                {
                    tFT_GetStatus delegateForFunctionPointer = (tFT_GetStatus)Marshal.GetDelegateForFunctionPointer(this.pFT_GetStatus, typeof(tFT_GetStatus));
                    uint lpdwAmountInRxQueue = 0;
                    uint lpdwAmountInTxQueue = 0;
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref lpdwAmountInRxQueue, ref lpdwAmountInTxQueue, ref EventType);
                    }
                    return ft_status;
                }
                if (this.pFT_GetStatus == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetStatus.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetLatency(ref byte Latency)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetLatencyTimer != IntPtr.Zero)
                {
                    tFT_GetLatencyTimer delegateForFunctionPointer = (tFT_GetLatencyTimer)Marshal.GetDelegateForFunctionPointer(this.pFT_GetLatencyTimer, typeof(tFT_GetLatencyTimer));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref Latency);
                    }
                    return ft_status;
                }
                if (this.pFT_GetLatencyTimer == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetLatencyTimer.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetLibraryVersion(ref uint LibraryVersion)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetLibraryVersion != IntPtr.Zero)
                {
                    tFT_GetLibraryVersion delegateForFunctionPointer = (tFT_GetLibraryVersion)Marshal.GetDelegateForFunctionPointer(this.pFT_GetLibraryVersion, typeof(tFT_GetLibraryVersion));
                    return delegateForFunctionPointer(ref LibraryVersion);
                }
                if (this.pFT_GetLibraryVersion == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetLibraryVersion.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetLineStatus(ref byte LineStatus)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetModemStatus != IntPtr.Zero)
                {
                    tFT_GetModemStatus delegateForFunctionPointer = (tFT_GetModemStatus)Marshal.GetDelegateForFunctionPointer(this.pFT_GetModemStatus, typeof(tFT_GetModemStatus));
                    uint lpdwModemStatus = 0;
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref lpdwModemStatus);
                    }
                    LineStatus = Convert.ToByte((uint)((lpdwModemStatus >> 8) & 0xff));
                    return ft_status;
                }
                if (this.pFT_GetModemStatus == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetModemStatus.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetModemStatus(ref byte ModemStatus)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetModemStatus != IntPtr.Zero)
                {
                    tFT_GetModemStatus delegateForFunctionPointer = (tFT_GetModemStatus)Marshal.GetDelegateForFunctionPointer(this.pFT_GetModemStatus, typeof(tFT_GetModemStatus));
                    uint lpdwModemStatus = 0;
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref lpdwModemStatus);
                    }
                    ModemStatus = Convert.ToByte((uint)(lpdwModemStatus & 0xff));
                    return ft_status;
                }
                if (this.pFT_GetModemStatus == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetModemStatus.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetNumberOfDevices(ref uint devcount)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_CreateDeviceInfoList != IntPtr.Zero)
                {
                    tFT_CreateDeviceInfoList delegateForFunctionPointer = (tFT_CreateDeviceInfoList)Marshal.GetDelegateForFunctionPointer(this.pFT_CreateDeviceInfoList, typeof(tFT_CreateDeviceInfoList));
                    return delegateForFunctionPointer(ref devcount);
                }
                MessageBox.Show("Failed to load function FT_CreateDeviceInfoList.");
            }
            return ft_status;
        }

        public FT_STATUS GetPinStates(ref byte BitMode)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetBitMode != IntPtr.Zero)
                {
                    tFT_GetBitMode delegateForFunctionPointer = (tFT_GetBitMode)Marshal.GetDelegateForFunctionPointer(this.pFT_GetBitMode, typeof(tFT_GetBitMode));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref BitMode);
                    }
                    return ft_status;
                }
                if (this.pFT_GetBitMode == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetBitMode.");
                }
            }
            return ft_status;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);
        public FT_STATUS GetRxBytesAvailable(ref uint RxQueue)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetQueueStatus != IntPtr.Zero)
                {
                    tFT_GetQueueStatus delegateForFunctionPointer = (tFT_GetQueueStatus)Marshal.GetDelegateForFunctionPointer(this.pFT_GetQueueStatus, typeof(tFT_GetQueueStatus));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref RxQueue);
                    }
                    return ft_status;
                }
                if (this.pFT_GetQueueStatus == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetQueueStatus.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetSerialNumber(out string SerialNumber)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            SerialNumber = string.Empty;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetDeviceInfo != IntPtr.Zero)
                {
                    tFT_GetDeviceInfo delegateForFunctionPointer = (tFT_GetDeviceInfo)Marshal.GetDelegateForFunctionPointer(this.pFT_GetDeviceInfo, typeof(tFT_GetDeviceInfo));
                    uint lpdwID = 0;
                    FT_DEVICE pftType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                    byte[] pcSerialNumber = new byte[0x10];
                    byte[] pcDescription = new byte[0x40];
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref pftType, ref lpdwID, pcSerialNumber, pcDescription, IntPtr.Zero);
                        SerialNumber = Encoding.ASCII.GetString(pcSerialNumber);
                        SerialNumber = SerialNumber.Substring(0, SerialNumber.IndexOf("\0"));
                    }
                    return ft_status;
                }
                if (this.pFT_GetDeviceInfo == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetDeviceInfo.");
                }
            }
            return ft_status;
        }

        public FT_STATUS GetTxBytesWaiting(ref uint TxQueue)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_GetStatus != IntPtr.Zero)
                {
                    tFT_GetStatus delegateForFunctionPointer = (tFT_GetStatus)Marshal.GetDelegateForFunctionPointer(this.pFT_GetStatus, typeof(tFT_GetStatus));
                    uint lpdwAmountInRxQueue = 0;
                    uint lpdwEventStatus = 0;
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ref lpdwAmountInRxQueue, ref TxQueue, ref lpdwEventStatus);
                    }
                    return ft_status;
                }
                if (this.pFT_GetStatus == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_GetStatus.");
                }
            }
            return ft_status;
        }

        public FT_STATUS InTransferSize(uint InTransferSize)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetUSBParameters != IntPtr.Zero)
                {
                    tFT_SetUSBParameters delegateForFunctionPointer = (tFT_SetUSBParameters)Marshal.GetDelegateForFunctionPointer(this.pFT_SetUSBParameters, typeof(tFT_SetUSBParameters));
                    uint dwOutTransferSize = InTransferSize;
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, InTransferSize, dwOutTransferSize);
                    }
                    return ft_status;
                }
                if (this.pFT_SetUSBParameters == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetUSBParameters.");
                }
            }
            return ft_status;
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr LoadLibrary(string dllToLoad);
        public FT_STATUS OpenByDescription(string description)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((((this.pFT_OpenEx != IntPtr.Zero) & (this.pFT_SetDataCharacteristics != IntPtr.Zero)) & (this.pFT_SetFlowControl != IntPtr.Zero)) & (this.pFT_SetBaudRate != IntPtr.Zero))
                {
                    tFT_OpenEx delegateForFunctionPointer = (tFT_OpenEx)Marshal.GetDelegateForFunctionPointer(this.pFT_OpenEx, typeof(tFT_OpenEx));
                    tFT_SetDataCharacteristics characteristics = (tFT_SetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(this.pFT_SetDataCharacteristics, typeof(tFT_SetDataCharacteristics));
                    tFT_SetFlowControl control = (tFT_SetFlowControl)Marshal.GetDelegateForFunctionPointer(this.pFT_SetFlowControl, typeof(tFT_SetFlowControl));
                    tFT_SetBaudRate rate = (tFT_SetBaudRate)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBaudRate, typeof(tFT_SetBaudRate));
                    ft_status = delegateForFunctionPointer(description, 2, ref this.ftHandle);
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        byte uWordLength = 8;
                        byte uStopBits = 0;
                        byte uParity = 0;
                        ft_status = characteristics(this.ftHandle, uWordLength, uStopBits, uParity);
                        ushort usFlowControl = 0;
                        byte uXon = 0x11;
                        byte uXoff = 0x13;
                        ft_status = control(this.ftHandle, usFlowControl, uXon, uXoff);
                        uint dwBaudRate = 0x2580;
                        ft_status = rate(this.ftHandle, dwBaudRate);
                    }
                    return ft_status;
                }
                if (this.pFT_OpenEx == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_OpenEx.");
                }
                if (this.pFT_SetDataCharacteristics == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
                }
                if (this.pFT_SetFlowControl == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetFlowControl.");
                }
                if (this.pFT_SetBaudRate == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBaudRate.");
                }
            }
            return ft_status;
        }

        public FT_STATUS OpenByIndex(uint index)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((((this.pFT_Open != IntPtr.Zero) & (this.pFT_SetDataCharacteristics != IntPtr.Zero)) & (this.pFT_SetFlowControl != IntPtr.Zero)) & (this.pFT_SetBaudRate != IntPtr.Zero))
                {
                    tFT_Open delegateForFunctionPointer = (tFT_Open)Marshal.GetDelegateForFunctionPointer(this.pFT_Open, typeof(tFT_Open));
                    tFT_SetDataCharacteristics characteristics = (tFT_SetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(this.pFT_SetDataCharacteristics, typeof(tFT_SetDataCharacteristics));
                    tFT_SetFlowControl control = (tFT_SetFlowControl)Marshal.GetDelegateForFunctionPointer(this.pFT_SetFlowControl, typeof(tFT_SetFlowControl));
                    tFT_SetBaudRate rate = (tFT_SetBaudRate)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBaudRate, typeof(tFT_SetBaudRate));
                    ft_status = delegateForFunctionPointer(index, ref this.ftHandle);
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        byte uWordLength = 8;
                        byte uStopBits = 0;
                        byte uParity = 0;
                        ft_status = characteristics(this.ftHandle, uWordLength, uStopBits, uParity);
                        ushort usFlowControl = 0;
                        byte uXon = 0x11;
                        byte uXoff = 0x13;
                        ft_status = control(this.ftHandle, usFlowControl, uXon, uXoff);
                        uint dwBaudRate = 0x2580;
                        ft_status = rate(this.ftHandle, dwBaudRate);
                    }
                    return ft_status;
                }
                if (this.pFT_Open == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Open.");
                }
                if (this.pFT_SetDataCharacteristics == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
                }
                if (this.pFT_SetFlowControl == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetFlowControl.");
                }
                if (this.pFT_SetBaudRate == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBaudRate.");
                }
            }
            return ft_status;
        }

        public FT_STATUS OpenByLocation(uint location)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((((this.pFT_OpenEx != IntPtr.Zero) & (this.pFT_SetDataCharacteristics != IntPtr.Zero)) & (this.pFT_SetFlowControl != IntPtr.Zero)) & (this.pFT_SetBaudRate != IntPtr.Zero))
                {
                    tFT_OpenExLoc delegateForFunctionPointer = (tFT_OpenExLoc)Marshal.GetDelegateForFunctionPointer(this.pFT_OpenEx, typeof(tFT_OpenExLoc));
                    tFT_SetDataCharacteristics characteristics = (tFT_SetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(this.pFT_SetDataCharacteristics, typeof(tFT_SetDataCharacteristics));
                    tFT_SetFlowControl control = (tFT_SetFlowControl)Marshal.GetDelegateForFunctionPointer(this.pFT_SetFlowControl, typeof(tFT_SetFlowControl));
                    tFT_SetBaudRate rate = (tFT_SetBaudRate)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBaudRate, typeof(tFT_SetBaudRate));
                    ft_status = delegateForFunctionPointer(location, 4, ref this.ftHandle);
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        byte uWordLength = 8;
                        byte uStopBits = 0;
                        byte uParity = 0;
                        ft_status = characteristics(this.ftHandle, uWordLength, uStopBits, uParity);
                        ushort usFlowControl = 0;
                        byte uXon = 0x11;
                        byte uXoff = 0x13;
                        ft_status = control(this.ftHandle, usFlowControl, uXon, uXoff);
                        uint dwBaudRate = 0x2580;
                        ft_status = rate(this.ftHandle, dwBaudRate);
                    }
                    return ft_status;
                }
                if (this.pFT_OpenEx == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_OpenEx.");
                }
                if (this.pFT_SetDataCharacteristics == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
                }
                if (this.pFT_SetFlowControl == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetFlowControl.");
                }
                if (this.pFT_SetBaudRate == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBaudRate.");
                }
            }
            return ft_status;
        }

        public FT_STATUS OpenBySerialNumber(string serialnumber)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((((this.pFT_OpenEx != IntPtr.Zero) & (this.pFT_SetDataCharacteristics != IntPtr.Zero)) & (this.pFT_SetFlowControl != IntPtr.Zero)) & (this.pFT_SetBaudRate != IntPtr.Zero))
                {
                    tFT_OpenEx delegateForFunctionPointer = (tFT_OpenEx)Marshal.GetDelegateForFunctionPointer(this.pFT_OpenEx, typeof(tFT_OpenEx));
                    tFT_SetDataCharacteristics characteristics = (tFT_SetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(this.pFT_SetDataCharacteristics, typeof(tFT_SetDataCharacteristics));
                    tFT_SetFlowControl control = (tFT_SetFlowControl)Marshal.GetDelegateForFunctionPointer(this.pFT_SetFlowControl, typeof(tFT_SetFlowControl));
                    tFT_SetBaudRate rate = (tFT_SetBaudRate)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBaudRate, typeof(tFT_SetBaudRate));
                    ft_status = delegateForFunctionPointer(serialnumber, 1, ref this.ftHandle);
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        byte uWordLength = 8;
                        byte uStopBits = 0;
                        byte uParity = 0;
                        ft_status = characteristics(this.ftHandle, uWordLength, uStopBits, uParity);
                        ushort usFlowControl = 0;
                        byte uXon = 0x11;
                        byte uXoff = 0x13;
                        ft_status = control(this.ftHandle, usFlowControl, uXon, uXoff);
                        uint dwBaudRate = 0x2580;
                        ft_status = rate(this.ftHandle, dwBaudRate);
                    }
                    return ft_status;
                }
                if (this.pFT_OpenEx == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_OpenEx.");
                }
                if (this.pFT_SetDataCharacteristics == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
                }
                if (this.pFT_SetFlowControl == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetFlowControl.");
                }
                if (this.pFT_SetBaudRate == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBaudRate.");
                }
            }
            return ft_status;
        }

        public FT_STATUS Purge(uint purgemask)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Purge != IntPtr.Zero)
                {
                    tFT_Purge delegateForFunctionPointer = (tFT_Purge)Marshal.GetDelegateForFunctionPointer(this.pFT_Purge, typeof(tFT_Purge));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, purgemask);
                    }
                    return ft_status;
                }
                if (this.pFT_Purge == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Purge.");
                }
            }
            return ft_status;
        }

        public FT_STATUS Read(byte[] dataBuffer, uint numBytesToRead, ref uint numBytesRead)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Read != IntPtr.Zero)
                {
                    tFT_Read delegateForFunctionPointer = (tFT_Read)Marshal.GetDelegateForFunctionPointer(this.pFT_Read, typeof(tFT_Read));
                    if (dataBuffer.Length < numBytesToRead)
                    {
                        numBytesToRead = (uint)dataBuffer.Length;
                    }
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, dataBuffer, numBytesToRead, ref numBytesRead);
                    }
                    return ft_status;
                }
                if (this.pFT_Read == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Read.");
                }
            }
            return ft_status;
        }

        public FT_STATUS Read(out string dataBuffer, uint numBytesToRead, ref uint numBytesRead)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            dataBuffer = string.Empty;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Read != IntPtr.Zero)
                {
                    tFT_Read delegateForFunctionPointer = (tFT_Read)Marshal.GetDelegateForFunctionPointer(this.pFT_Read, typeof(tFT_Read));
                    byte[] lpBuffer = new byte[numBytesToRead];
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, lpBuffer, numBytesToRead, ref numBytesRead);
                        dataBuffer = Encoding.ASCII.GetString(lpBuffer);
                        dataBuffer = dataBuffer.Substring(0, (int)numBytesRead);
                    }
                    return ft_status;
                }
                if (this.pFT_Read == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Read.");
                }
            }
            return ft_status;
        }

        public FT_STATUS ReadEEPROMLocation(uint Address, ref ushort EEValue)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_ReadEE != IntPtr.Zero)
                {
                    tFT_ReadEE delegateForFunctionPointer = (tFT_ReadEE)Marshal.GetDelegateForFunctionPointer(this.pFT_ReadEE, typeof(tFT_ReadEE));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, Address, ref EEValue);
                    }
                    return ft_status;
                }
                if (this.pFT_ReadEE == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_ReadEE.");
                }
            }
            return ft_status;
        }

        public FT_STATUS ReadFT2232EEPROM(FT2232_EEPROM_STRUCTURE ee2232)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Read != IntPtr.Zero)
                {
                    tFT_EE_Read delegateForFunctionPointer = (tFT_EE_Read)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Read, typeof(tFT_EE_Read));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_2232)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 2,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        ee2232.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
                        ee2232.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
                        ee2232.Description = Marshal.PtrToStringAnsi(pData.Description);
                        ee2232.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                        ee2232.VendorID = pData.VendorID;
                        ee2232.ProductID = pData.ProductID;
                        ee2232.MaxPower = pData.MaxPower;
                        ee2232.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
                        ee2232.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
                        ee2232.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable5);
                        ee2232.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable5);
                        ee2232.USBVersionEnable = Convert.ToBoolean(pData.USBVersionEnable5);
                        ee2232.USBVersion = pData.USBVersion5;
                        ee2232.AIsHighCurrent = Convert.ToBoolean(pData.AIsHighCurrent);
                        ee2232.BIsHighCurrent = Convert.ToBoolean(pData.BIsHighCurrent);
                        ee2232.IFAIsFifo = Convert.ToBoolean(pData.IFAIsFifo);
                        ee2232.IFAIsFifoTar = Convert.ToBoolean(pData.IFAIsFifoTar);
                        ee2232.IFAIsFastSer = Convert.ToBoolean(pData.IFAIsFastSer);
                        ee2232.AIsVCP = Convert.ToBoolean(pData.AIsVCP);
                        ee2232.IFBIsFifo = Convert.ToBoolean(pData.IFBIsFifo);
                        ee2232.IFBIsFifoTar = Convert.ToBoolean(pData.IFBIsFifoTar);
                        ee2232.IFBIsFastSer = Convert.ToBoolean(pData.IFBIsFastSer);
                        ee2232.BIsVCP = Convert.ToBoolean(pData.BIsVCP);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Read == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Read.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS ReadFT2232HEEPROM(FT2232H_EEPROM_STRUCTURE ee2232h)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Read != IntPtr.Zero)
                {
                    tFT_EE_Read delegateForFunctionPointer = (tFT_EE_Read)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Read, typeof(tFT_EE_Read));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_2232H)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 3,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        ee2232h.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
                        ee2232h.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
                        ee2232h.Description = Marshal.PtrToStringAnsi(pData.Description);
                        ee2232h.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                        ee2232h.VendorID = pData.VendorID;
                        ee2232h.ProductID = pData.ProductID;
                        ee2232h.MaxPower = pData.MaxPower;
                        ee2232h.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
                        ee2232h.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
                        ee2232h.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable7);
                        ee2232h.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable7);
                        ee2232h.ALSlowSlew = Convert.ToBoolean(pData.ALSlowSlew);
                        ee2232h.ALSchmittInput = Convert.ToBoolean(pData.ALSchmittInput);
                        ee2232h.ALDriveCurrent = pData.ALDriveCurrent;
                        ee2232h.AHSlowSlew = Convert.ToBoolean(pData.AHSlowSlew);
                        ee2232h.AHSchmittInput = Convert.ToBoolean(pData.AHSchmittInput);
                        ee2232h.AHDriveCurrent = pData.AHDriveCurrent;
                        ee2232h.BLSlowSlew = Convert.ToBoolean(pData.BLSlowSlew);
                        ee2232h.BLSchmittInput = Convert.ToBoolean(pData.BLSchmittInput);
                        ee2232h.BLDriveCurrent = pData.BLDriveCurrent;
                        ee2232h.BHSlowSlew = Convert.ToBoolean(pData.BHSlowSlew);
                        ee2232h.BHSchmittInput = Convert.ToBoolean(pData.BHSchmittInput);
                        ee2232h.BHDriveCurrent = pData.BHDriveCurrent;
                        ee2232h.IFAIsFifo = Convert.ToBoolean(pData.IFAIsFifo7);
                        ee2232h.IFAIsFifoTar = Convert.ToBoolean(pData.IFAIsFifoTar7);
                        ee2232h.IFAIsFastSer = Convert.ToBoolean(pData.IFAIsFastSer7);
                        ee2232h.AIsVCP = Convert.ToBoolean(pData.AIsVCP7);
                        ee2232h.IFBIsFifo = Convert.ToBoolean(pData.IFBIsFifo7);
                        ee2232h.IFBIsFifoTar = Convert.ToBoolean(pData.IFBIsFifoTar7);
                        ee2232h.IFBIsFastSer = Convert.ToBoolean(pData.IFBIsFastSer7);
                        ee2232h.BIsVCP = Convert.ToBoolean(pData.BIsVCP7);
                        ee2232h.PowerSaveEnable = Convert.ToBoolean(pData.PowerSaveEnable);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Read == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Read.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS ReadFT232BEEPROM(FT232B_EEPROM_STRUCTURE ee232b)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Read != IntPtr.Zero)
                {
                    tFT_EE_Read delegateForFunctionPointer = (tFT_EE_Read)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Read, typeof(tFT_EE_Read));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_BM)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 2,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        ee232b.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
                        ee232b.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
                        ee232b.Description = Marshal.PtrToStringAnsi(pData.Description);
                        ee232b.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                        ee232b.VendorID = pData.VendorID;
                        ee232b.ProductID = pData.ProductID;
                        ee232b.MaxPower = pData.MaxPower;
                        ee232b.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
                        ee232b.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
                        ee232b.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable);
                        ee232b.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable);
                        ee232b.USBVersionEnable = Convert.ToBoolean(pData.USBVersionEnable);
                        ee232b.USBVersion = pData.USBVersion;
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Read == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Read.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS ReadFT232REEPROM(FT232R_EEPROM_STRUCTURE ee232r)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Read != IntPtr.Zero)
                {
                    tFT_EE_Read delegateForFunctionPointer = (tFT_EE_Read)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Read, typeof(tFT_EE_Read));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_232R)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 2,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        ee232r.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
                        ee232r.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
                        ee232r.Description = Marshal.PtrToStringAnsi(pData.Description);
                        ee232r.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                        ee232r.VendorID = pData.VendorID;
                        ee232r.ProductID = pData.ProductID;
                        ee232r.MaxPower = pData.MaxPower;
                        ee232r.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
                        ee232r.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
                        ee232r.UseExtOsc = Convert.ToBoolean(pData.UseExtOsc);
                        ee232r.HighDriveIOs = Convert.ToBoolean(pData.HighDriveIOs);
                        ee232r.EndpointSize = pData.EndpointSize;
                        ee232r.PullDownEnable = Convert.ToBoolean(pData.PullDownEnableR);
                        ee232r.SerNumEnable = Convert.ToBoolean(pData.SerNumEnableR);
                        ee232r.InvertTXD = Convert.ToBoolean(pData.InvertTXD);
                        ee232r.InvertRXD = Convert.ToBoolean(pData.InvertRXD);
                        ee232r.InvertRTS = Convert.ToBoolean(pData.InvertRTS);
                        ee232r.InvertCTS = Convert.ToBoolean(pData.InvertCTS);
                        ee232r.InvertDTR = Convert.ToBoolean(pData.InvertDTR);
                        ee232r.InvertDSR = Convert.ToBoolean(pData.InvertDSR);
                        ee232r.InvertDCD = Convert.ToBoolean(pData.InvertDCD);
                        ee232r.InvertRI = Convert.ToBoolean(pData.InvertRI);
                        ee232r.Cbus0 = pData.Cbus0;
                        ee232r.Cbus1 = pData.Cbus1;
                        ee232r.Cbus2 = pData.Cbus2;
                        ee232r.Cbus3 = pData.Cbus3;
                        ee232r.Cbus4 = pData.Cbus4;
                        ee232r.RIsD2XX = Convert.ToBoolean(pData.RIsD2XX);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Read == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Read.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS ReadFT4232HEEPROM(FT4232H_EEPROM_STRUCTURE ee4232h)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Read != IntPtr.Zero)
                {
                    tFT_EE_Read delegateForFunctionPointer = (tFT_EE_Read)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Read, typeof(tFT_EE_Read));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_4232H)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 4,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        ee4232h.Manufacturer = Marshal.PtrToStringAnsi(pData.Manufacturer);
                        ee4232h.ManufacturerID = Marshal.PtrToStringAnsi(pData.ManufacturerID);
                        ee4232h.Description = Marshal.PtrToStringAnsi(pData.Description);
                        ee4232h.SerialNumber = Marshal.PtrToStringAnsi(pData.SerialNumber);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                        ee4232h.VendorID = pData.VendorID;
                        ee4232h.ProductID = pData.ProductID;
                        ee4232h.MaxPower = pData.MaxPower;
                        ee4232h.SelfPowered = Convert.ToBoolean(pData.SelfPowered);
                        ee4232h.RemoteWakeup = Convert.ToBoolean(pData.RemoteWakeup);
                        ee4232h.PullDownEnable = Convert.ToBoolean(pData.PullDownEnable8);
                        ee4232h.SerNumEnable = Convert.ToBoolean(pData.SerNumEnable8);
                        ee4232h.ASlowSlew = Convert.ToBoolean(pData.ASlowSlew);
                        ee4232h.ASchmittInput = Convert.ToBoolean(pData.ASchmittInput);
                        ee4232h.ADriveCurrent = pData.ADriveCurrent;
                        ee4232h.BSlowSlew = Convert.ToBoolean(pData.BSlowSlew);
                        ee4232h.BSchmittInput = Convert.ToBoolean(pData.BSchmittInput);
                        ee4232h.BDriveCurrent = pData.BDriveCurrent;
                        ee4232h.CSlowSlew = Convert.ToBoolean(pData.CSlowSlew);
                        ee4232h.CSchmittInput = Convert.ToBoolean(pData.CSchmittInput);
                        ee4232h.CDriveCurrent = pData.CDriveCurrent;
                        ee4232h.DSlowSlew = Convert.ToBoolean(pData.DSlowSlew);
                        ee4232h.DSchmittInput = Convert.ToBoolean(pData.DSchmittInput);
                        ee4232h.DDriveCurrent = pData.DDriveCurrent;
                        ee4232h.ARIIsTXDEN = Convert.ToBoolean(pData.ARIIsTXDEN);
                        ee4232h.BRIIsTXDEN = Convert.ToBoolean(pData.BRIIsTXDEN);
                        ee4232h.CRIIsTXDEN = Convert.ToBoolean(pData.CRIIsTXDEN);
                        ee4232h.DRIIsTXDEN = Convert.ToBoolean(pData.DRIIsTXDEN);
                        ee4232h.AIsVCP = Convert.ToBoolean(pData.AIsVCP8);
                        ee4232h.BIsVCP = Convert.ToBoolean(pData.BIsVCP8);
                        ee4232h.CIsVCP = Convert.ToBoolean(pData.CIsVCP8);
                        ee4232h.DIsVCP = Convert.ToBoolean(pData.DIsVCP8);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Read == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Read.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS Reload(ushort VendorID, ushort ProductID)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Reload != IntPtr.Zero)
                {
                    tFT_Reload delegateForFunctionPointer = (tFT_Reload)Marshal.GetDelegateForFunctionPointer(this.pFT_Reload, typeof(tFT_Reload));
                    return delegateForFunctionPointer(VendorID, ProductID);
                }
                if (this.pFT_Reload == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Reload.");
                }
            }
            return ft_status;
        }

        public FT_STATUS Rescan()
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Rescan != IntPtr.Zero)
                {
                    tFT_Rescan delegateForFunctionPointer = (tFT_Rescan)Marshal.GetDelegateForFunctionPointer(this.pFT_Rescan, typeof(tFT_Rescan));
                    return delegateForFunctionPointer();
                }
                if (this.pFT_Rescan == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Rescan.");
                }
            }
            return ft_status;
        }

        public FT_STATUS ResetDevice()
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_ResetDevice != IntPtr.Zero)
                {
                    tFT_ResetDevice delegateForFunctionPointer = (tFT_ResetDevice)Marshal.GetDelegateForFunctionPointer(this.pFT_ResetDevice, typeof(tFT_ResetDevice));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle);
                    }
                    return ft_status;
                }
                if (this.pFT_ResetDevice == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_ResetDevice.");
                }
            }
            return ft_status;
        }

        public FT_STATUS ResetPort()
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_ResetPort != IntPtr.Zero)
                {
                    tFT_ResetPort delegateForFunctionPointer = (tFT_ResetPort)Marshal.GetDelegateForFunctionPointer(this.pFT_ResetPort, typeof(tFT_ResetPort));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle);
                    }
                    return ft_status;
                }
                if (this.pFT_ResetPort == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_ResetPort.");
                }
            }
            return ft_status;
        }

        public FT_STATUS RestartInTask()
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_RestartInTask != IntPtr.Zero)
                {
                    tFT_RestartInTask delegateForFunctionPointer = (tFT_RestartInTask)Marshal.GetDelegateForFunctionPointer(this.pFT_RestartInTask, typeof(tFT_RestartInTask));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle);
                    }
                    return ft_status;
                }
                if (this.pFT_RestartInTask == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_RestartInTask.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetBaudRate(uint BaudRate)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetBaudRate != IntPtr.Zero)
                {
                    tFT_SetBaudRate delegateForFunctionPointer = (tFT_SetBaudRate)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBaudRate, typeof(tFT_SetBaudRate));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, BaudRate);
                    }
                    return ft_status;
                }
                if (this.pFT_SetBaudRate == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBaudRate.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetBitMode(byte Mask, byte BitMode)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL == IntPtr.Zero)
            {
                return ftStatus;
            }
            if (!(this.pFT_SetBitMode != IntPtr.Zero))
            {
                if (this.pFT_SetBitMode == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBitMode.");
                }
                return ftStatus;
            }
            tFT_SetBitMode delegateForFunctionPointer = (tFT_SetBitMode)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBitMode, typeof(tFT_SetBitMode));
            if (!(this.ftHandle != IntPtr.Zero))
            {
                return ftStatus;
            }
            FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
            this.GetDeviceType(ref deviceType);
            switch (deviceType)
            {
                case FT_DEVICE.FT_DEVICE_AM:
                    ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                    this.ErrorHandler(ftStatus, ftErrorCondition);
                    break;

                case FT_DEVICE.FT_DEVICE_100AX:
                    ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                    this.ErrorHandler(ftStatus, ftErrorCondition);
                    break;

                default:
                    if ((deviceType == FT_DEVICE.FT_DEVICE_BM) && (BitMode != 0))
                    {
                        if ((BitMode & 1) == 0)
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                    }
                    else if ((deviceType == FT_DEVICE.FT_DEVICE_2232) && (BitMode != 0))
                    {
                        if ((BitMode & 0x1f) == 0)
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if ((BitMode == 2) & (this.InterfaceIdentifier != "A"))
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                    }
                    else if ((deviceType == FT_DEVICE.FT_DEVICE_232R) && (BitMode != 0))
                    {
                        if ((BitMode & 0x25) == 0)
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                    }
                    else if ((deviceType == FT_DEVICE.FT_DEVICE_2232H) && (BitMode != 0))
                    {
                        if ((BitMode & 0x5f) == 0)
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if (((BitMode == 8) | (BitMode == 0x40)) & (this.InterfaceIdentifier != "A"))
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                    }
                    else if ((deviceType == FT_DEVICE.FT_DEVICE_4232H) && (BitMode != 0))
                    {
                        if ((BitMode & 7) == 0)
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if ((BitMode == 2) & ((this.InterfaceIdentifier != "A") & (this.InterfaceIdentifier != "B")))
                        {
                            ftErrorCondition = FT_ERROR.FT_INVALID_BITMODE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                    }
                    break;
            }
            return delegateForFunctionPointer(this.ftHandle, Mask, BitMode);
        }

        public FT_STATUS SetBreak(bool Enable)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((this.pFT_SetBreakOn != IntPtr.Zero) & (this.pFT_SetBreakOff != IntPtr.Zero))
                {
                    tFT_SetBreakOn delegateForFunctionPointer = (tFT_SetBreakOn)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBreakOn, typeof(tFT_SetBreakOn));
                    tFT_SetBreakOff off = (tFT_SetBreakOff)Marshal.GetDelegateForFunctionPointer(this.pFT_SetBreakOff, typeof(tFT_SetBreakOff));
                    if (!(this.ftHandle != IntPtr.Zero))
                    {
                        return ft_status;
                    }
                    if (Enable)
                    {
                        return delegateForFunctionPointer(this.ftHandle);
                    }
                    return off(this.ftHandle);
                }
                if (this.pFT_SetBreakOn == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBreakOn.");
                }
                if (this.pFT_SetBreakOff == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetBreakOff.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetCharacters(byte EventChar, bool EventCharEnable, byte ErrorChar, bool ErrorCharEnable)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetChars != IntPtr.Zero)
                {
                    tFT_SetChars delegateForFunctionPointer = (tFT_SetChars)Marshal.GetDelegateForFunctionPointer(this.pFT_SetChars, typeof(tFT_SetChars));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, EventChar, Convert.ToByte(EventCharEnable), ErrorChar, Convert.ToByte(ErrorCharEnable));
                    }
                    return ft_status;
                }
                if (this.pFT_SetChars == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetChars.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetDataCharacteristics(byte DataBits, byte StopBits, byte Parity)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetDataCharacteristics != IntPtr.Zero)
                {
                    tFT_SetDataCharacteristics delegateForFunctionPointer = (tFT_SetDataCharacteristics)Marshal.GetDelegateForFunctionPointer(this.pFT_SetDataCharacteristics, typeof(tFT_SetDataCharacteristics));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, DataBits, StopBits, Parity);
                    }
                    return ft_status;
                }
                if (this.pFT_SetDataCharacteristics == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetDataCharacteristics.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetDeadmanTimeout(uint DeadmanTimeout)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetDeadmanTimeout != IntPtr.Zero)
                {
                    tFT_SetDeadmanTimeout delegateForFunctionPointer = (tFT_SetDeadmanTimeout)Marshal.GetDelegateForFunctionPointer(this.pFT_SetDeadmanTimeout, typeof(tFT_SetDeadmanTimeout));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, DeadmanTimeout);
                    }
                    return ft_status;
                }
                if (this.pFT_SetDeadmanTimeout == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetDeadmanTimeout.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetDTR(bool Enable)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((this.pFT_SetDtr != IntPtr.Zero) & (this.pFT_ClrDtr != IntPtr.Zero))
                {
                    tFT_SetDtr delegateForFunctionPointer = (tFT_SetDtr)Marshal.GetDelegateForFunctionPointer(this.pFT_SetDtr, typeof(tFT_SetDtr));
                    tFT_ClrDtr dtr2 = (tFT_ClrDtr)Marshal.GetDelegateForFunctionPointer(this.pFT_ClrDtr, typeof(tFT_ClrDtr));
                    if (!(this.ftHandle != IntPtr.Zero))
                    {
                        return ft_status;
                    }
                    if (Enable)
                    {
                        return delegateForFunctionPointer(this.ftHandle);
                    }
                    return dtr2(this.ftHandle);
                }
                if (this.pFT_SetDtr == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetDtr.");
                }
                if (this.pFT_ClrDtr == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_ClrDtr.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetEventNotification(uint eventmask, EventWaitHandle eventhandle)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetEventNotification != IntPtr.Zero)
                {
                    tFT_SetEventNotification delegateForFunctionPointer = (tFT_SetEventNotification)Marshal.GetDelegateForFunctionPointer(this.pFT_SetEventNotification, typeof(tFT_SetEventNotification));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, eventmask, eventhandle.SafeWaitHandle);
                    }
                    return ft_status;
                }
                if (this.pFT_SetEventNotification == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetEventNotification.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetFlowControl(ushort FlowControl, byte Xon, byte Xoff)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetFlowControl != IntPtr.Zero)
                {
                    tFT_SetFlowControl delegateForFunctionPointer = (tFT_SetFlowControl)Marshal.GetDelegateForFunctionPointer(this.pFT_SetFlowControl, typeof(tFT_SetFlowControl));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, FlowControl, Xon, Xoff);
                    }
                    return ft_status;
                }
                if (this.pFT_SetFlowControl == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetFlowControl.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetLatency(byte Latency)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetLatencyTimer != IntPtr.Zero)
                {
                    tFT_SetLatencyTimer delegateForFunctionPointer = (tFT_SetLatencyTimer)Marshal.GetDelegateForFunctionPointer(this.pFT_SetLatencyTimer, typeof(tFT_SetLatencyTimer));
                    if (!(this.ftHandle != IntPtr.Zero))
                    {
                        return ft_status;
                    }
                    FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                    this.GetDeviceType(ref deviceType);
                    if (((deviceType == FT_DEVICE.FT_DEVICE_BM) || (deviceType == FT_DEVICE.FT_DEVICE_2232)) && (Latency < 2))
                    {
                        Latency = 2;
                    }
                    return delegateForFunctionPointer(this.ftHandle, Latency);
                }
                if (this.pFT_SetLatencyTimer == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetLatencyTimer.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetResetPipeRetryCount(uint ResetPipeRetryCount)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetResetPipeRetryCount != IntPtr.Zero)
                {
                    tFT_SetResetPipeRetryCount delegateForFunctionPointer = (tFT_SetResetPipeRetryCount)Marshal.GetDelegateForFunctionPointer(this.pFT_SetResetPipeRetryCount, typeof(tFT_SetResetPipeRetryCount));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ResetPipeRetryCount);
                    }
                    return ft_status;
                }
                if (this.pFT_SetResetPipeRetryCount == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetResetPipeRetryCount.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetRTS(bool Enable)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if ((this.pFT_SetRts != IntPtr.Zero) & (this.pFT_ClrRts != IntPtr.Zero))
                {
                    tFT_SetRts delegateForFunctionPointer = (tFT_SetRts)Marshal.GetDelegateForFunctionPointer(this.pFT_SetRts, typeof(tFT_SetRts));
                    tFT_ClrRts rts2 = (tFT_ClrRts)Marshal.GetDelegateForFunctionPointer(this.pFT_ClrRts, typeof(tFT_ClrRts));
                    if (!(this.ftHandle != IntPtr.Zero))
                    {
                        return ft_status;
                    }
                    if (Enable)
                    {
                        return delegateForFunctionPointer(this.ftHandle);
                    }
                    return rts2(this.ftHandle);
                }
                if (this.pFT_SetRts == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetRts.");
                }
                if (this.pFT_ClrRts == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_ClrRts.");
                }
            }
            return ft_status;
        }

        public FT_STATUS SetTimeouts(uint ReadTimeout, uint WriteTimeout)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_SetTimeouts != IntPtr.Zero)
                {
                    tFT_SetTimeouts delegateForFunctionPointer = (tFT_SetTimeouts)Marshal.GetDelegateForFunctionPointer(this.pFT_SetTimeouts, typeof(tFT_SetTimeouts));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, ReadTimeout, WriteTimeout);
                    }
                    return ft_status;
                }
                if (this.pFT_SetTimeouts == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_SetTimeouts.");
                }
            }
            return ft_status;
        }

        public FT_STATUS StopInTask()
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_StopInTask != IntPtr.Zero)
                {
                    tFT_StopInTask delegateForFunctionPointer = (tFT_StopInTask)Marshal.GetDelegateForFunctionPointer(this.pFT_StopInTask, typeof(tFT_StopInTask));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle);
                    }
                    return ft_status;
                }
                if (this.pFT_StopInTask == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_StopInTask.");
                }
            }
            return ft_status;
        }

        public FT_STATUS Write(byte[] dataBuffer, int numBytesToWrite, ref uint numBytesWritten)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Write != IntPtr.Zero)
                {
                    tFT_Write delegateForFunctionPointer = (tFT_Write)Marshal.GetDelegateForFunctionPointer(this.pFT_Write, typeof(tFT_Write));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, dataBuffer, numBytesToWrite, ref numBytesWritten);
                    }
                    return ft_status;
                }
                if (this.pFT_Write == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Write.");
                }
            }
            return ft_status;
        }

        public FT_STATUS Write(string dataBuffer, int numBytesToWrite, ref uint numBytesWritten)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_Write != IntPtr.Zero)
                {
                    tFT_Write delegateForFunctionPointer = (tFT_Write)Marshal.GetDelegateForFunctionPointer(this.pFT_Write, typeof(tFT_Write));
                    byte[] bytes = Encoding.ASCII.GetBytes(dataBuffer);
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, bytes, numBytesToWrite, ref numBytesWritten);
                    }
                    return ft_status;
                }
                if (this.pFT_Write == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_Write.");
                }
            }
            return ft_status;
        }

        public FT_STATUS WriteEEPROMLocation(uint Address, ushort EEValue)
        {
            FT_STATUS ft_status = FT_STATUS.FT_OTHER_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_WriteEE != IntPtr.Zero)
                {
                    tFT_WriteEE delegateForFunctionPointer = (tFT_WriteEE)Marshal.GetDelegateForFunctionPointer(this.pFT_WriteEE, typeof(tFT_WriteEE));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        ft_status = delegateForFunctionPointer(this.ftHandle, Address, EEValue);
                    }
                    return ft_status;
                }
                if (this.pFT_WriteEE == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_WriteEE.");
                }
            }
            return ft_status;
        }

        public FT_STATUS WriteFT2232EEPROM(FT2232_EEPROM_STRUCTURE ee2232)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Program != IntPtr.Zero)
                {
                    tFT_EE_Program delegateForFunctionPointer = (tFT_EE_Program)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Program, typeof(tFT_EE_Program));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_2232)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if ((ee2232.VendorID == 0) | (ee2232.ProductID == 0))
                        {
                            return FT_STATUS.FT_INVALID_PARAMETER;
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 2,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        if (ee2232.Manufacturer.Length > 0x20)
                        {
                            ee2232.Manufacturer = ee2232.Manufacturer.Substring(0, 0x20);
                        }
                        if (ee2232.ManufacturerID.Length > 0x10)
                        {
                            ee2232.ManufacturerID = ee2232.ManufacturerID.Substring(0, 0x10);
                        }
                        if (ee2232.Description.Length > 0x40)
                        {
                            ee2232.Description = ee2232.Description.Substring(0, 0x40);
                        }
                        if (ee2232.SerialNumber.Length > 0x10)
                        {
                            ee2232.SerialNumber = ee2232.SerialNumber.Substring(0, 0x10);
                        }
                        pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee2232.Manufacturer);
                        pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee2232.ManufacturerID);
                        pData.Description = Marshal.StringToHGlobalAnsi(ee2232.Description);
                        pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee2232.SerialNumber);
                        pData.VendorID = ee2232.VendorID;
                        pData.ProductID = ee2232.ProductID;
                        pData.MaxPower = ee2232.MaxPower;
                        pData.SelfPowered = Convert.ToUInt16(ee2232.SelfPowered);
                        pData.RemoteWakeup = Convert.ToUInt16(ee2232.RemoteWakeup);
                        pData.Rev5 = Convert.ToByte(true);
                        pData.PullDownEnable5 = Convert.ToByte(ee2232.PullDownEnable);
                        pData.SerNumEnable5 = Convert.ToByte(ee2232.SerNumEnable);
                        pData.USBVersionEnable5 = Convert.ToByte(ee2232.USBVersionEnable);
                        pData.USBVersion5 = ee2232.USBVersion;
                        pData.AIsHighCurrent = Convert.ToByte(ee2232.AIsHighCurrent);
                        pData.BIsHighCurrent = Convert.ToByte(ee2232.BIsHighCurrent);
                        pData.IFAIsFifo = Convert.ToByte(ee2232.IFAIsFifo);
                        pData.IFAIsFifoTar = Convert.ToByte(ee2232.IFAIsFifoTar);
                        pData.IFAIsFastSer = Convert.ToByte(ee2232.IFAIsFastSer);
                        pData.AIsVCP = Convert.ToByte(ee2232.AIsVCP);
                        pData.IFBIsFifo = Convert.ToByte(ee2232.IFBIsFifo);
                        pData.IFBIsFifoTar = Convert.ToByte(ee2232.IFBIsFifoTar);
                        pData.IFBIsFastSer = Convert.ToByte(ee2232.IFBIsFastSer);
                        pData.BIsVCP = Convert.ToByte(ee2232.BIsVCP);
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Program == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Program.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS WriteFT2232HEEPROM(FT2232H_EEPROM_STRUCTURE ee2232h)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Program != IntPtr.Zero)
                {
                    tFT_EE_Program delegateForFunctionPointer = (tFT_EE_Program)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Program, typeof(tFT_EE_Program));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_2232H)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if ((ee2232h.VendorID == 0) | (ee2232h.ProductID == 0))
                        {
                            return FT_STATUS.FT_INVALID_PARAMETER;
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 3,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        if (ee2232h.Manufacturer.Length > 0x20)
                        {
                            ee2232h.Manufacturer = ee2232h.Manufacturer.Substring(0, 0x20);
                        }
                        if (ee2232h.ManufacturerID.Length > 0x10)
                        {
                            ee2232h.ManufacturerID = ee2232h.ManufacturerID.Substring(0, 0x10);
                        }
                        if (ee2232h.Description.Length > 0x40)
                        {
                            ee2232h.Description = ee2232h.Description.Substring(0, 0x40);
                        }
                        if (ee2232h.SerialNumber.Length > 0x10)
                        {
                            ee2232h.SerialNumber = ee2232h.SerialNumber.Substring(0, 0x10);
                        }
                        pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee2232h.Manufacturer);
                        pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee2232h.ManufacturerID);
                        pData.Description = Marshal.StringToHGlobalAnsi(ee2232h.Description);
                        pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee2232h.SerialNumber);
                        pData.VendorID = ee2232h.VendorID;
                        pData.ProductID = ee2232h.ProductID;
                        pData.MaxPower = ee2232h.MaxPower;
                        pData.SelfPowered = Convert.ToUInt16(ee2232h.SelfPowered);
                        pData.RemoteWakeup = Convert.ToUInt16(ee2232h.RemoteWakeup);
                        pData.PullDownEnable7 = Convert.ToByte(ee2232h.PullDownEnable);
                        pData.SerNumEnable7 = Convert.ToByte(ee2232h.SerNumEnable);
                        pData.ALSlowSlew = Convert.ToByte(ee2232h.ALSlowSlew);
                        pData.ALSchmittInput = Convert.ToByte(ee2232h.ALSchmittInput);
                        pData.ALDriveCurrent = ee2232h.ALDriveCurrent;
                        pData.AHSlowSlew = Convert.ToByte(ee2232h.AHSlowSlew);
                        pData.AHSchmittInput = Convert.ToByte(ee2232h.AHSchmittInput);
                        pData.AHDriveCurrent = ee2232h.AHDriveCurrent;
                        pData.BLSlowSlew = Convert.ToByte(ee2232h.BLSlowSlew);
                        pData.BLSchmittInput = Convert.ToByte(ee2232h.BLSchmittInput);
                        pData.BLDriveCurrent = ee2232h.BLDriveCurrent;
                        pData.BHSlowSlew = Convert.ToByte(ee2232h.BHSlowSlew);
                        pData.BHSchmittInput = Convert.ToByte(ee2232h.BHSchmittInput);
                        pData.BHDriveCurrent = ee2232h.BHDriveCurrent;
                        pData.IFAIsFifo7 = Convert.ToByte(ee2232h.IFAIsFifo);
                        pData.IFAIsFifoTar7 = Convert.ToByte(ee2232h.IFAIsFifoTar);
                        pData.IFAIsFastSer7 = Convert.ToByte(ee2232h.IFAIsFastSer);
                        pData.AIsVCP7 = Convert.ToByte(ee2232h.AIsVCP);
                        pData.IFBIsFifo7 = Convert.ToByte(ee2232h.IFBIsFifo);
                        pData.IFBIsFifoTar7 = Convert.ToByte(ee2232h.IFBIsFifoTar);
                        pData.IFBIsFastSer7 = Convert.ToByte(ee2232h.IFBIsFastSer);
                        pData.BIsVCP7 = Convert.ToByte(ee2232h.BIsVCP);
                        pData.PowerSaveEnable = Convert.ToByte(ee2232h.PowerSaveEnable);
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Program == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Program.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS WriteFT232BEEPROM(FT232B_EEPROM_STRUCTURE ee232b)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Program != IntPtr.Zero)
                {
                    tFT_EE_Program delegateForFunctionPointer = (tFT_EE_Program)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Program, typeof(tFT_EE_Program));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_BM)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if ((ee232b.VendorID == 0) | (ee232b.ProductID == 0))
                        {
                            return FT_STATUS.FT_INVALID_PARAMETER;
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 2,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        if (ee232b.Manufacturer.Length > 0x20)
                        {
                            ee232b.Manufacturer = ee232b.Manufacturer.Substring(0, 0x20);
                        }
                        if (ee232b.ManufacturerID.Length > 0x10)
                        {
                            ee232b.ManufacturerID = ee232b.ManufacturerID.Substring(0, 0x10);
                        }
                        if (ee232b.Description.Length > 0x40)
                        {
                            ee232b.Description = ee232b.Description.Substring(0, 0x40);
                        }
                        if (ee232b.SerialNumber.Length > 0x10)
                        {
                            ee232b.SerialNumber = ee232b.SerialNumber.Substring(0, 0x10);
                        }
                        pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee232b.Manufacturer);
                        pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232b.ManufacturerID);
                        pData.Description = Marshal.StringToHGlobalAnsi(ee232b.Description);
                        pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee232b.SerialNumber);
                        pData.VendorID = ee232b.VendorID;
                        pData.ProductID = ee232b.ProductID;
                        pData.MaxPower = ee232b.MaxPower;
                        pData.SelfPowered = Convert.ToUInt16(ee232b.SelfPowered);
                        pData.RemoteWakeup = Convert.ToUInt16(ee232b.RemoteWakeup);
                        pData.Rev4 = Convert.ToByte(true);
                        pData.PullDownEnable = Convert.ToByte(ee232b.PullDownEnable);
                        pData.SerNumEnable = Convert.ToByte(ee232b.SerNumEnable);
                        pData.USBVersionEnable = Convert.ToByte(ee232b.USBVersionEnable);
                        pData.USBVersion = ee232b.USBVersion;
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Program == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Program.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS WriteFT232REEPROM(FT232R_EEPROM_STRUCTURE ee232r)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Program != IntPtr.Zero)
                {
                    tFT_EE_Program delegateForFunctionPointer = (tFT_EE_Program)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Program, typeof(tFT_EE_Program));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_232R)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if ((ee232r.VendorID == 0) | (ee232r.ProductID == 0))
                        {
                            return FT_STATUS.FT_INVALID_PARAMETER;
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 2,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        if (ee232r.Manufacturer.Length > 0x20)
                        {
                            ee232r.Manufacturer = ee232r.Manufacturer.Substring(0, 0x20);
                        }
                        if (ee232r.ManufacturerID.Length > 0x10)
                        {
                            ee232r.ManufacturerID = ee232r.ManufacturerID.Substring(0, 0x10);
                        }
                        if (ee232r.Description.Length > 0x40)
                        {
                            ee232r.Description = ee232r.Description.Substring(0, 0x40);
                        }
                        if (ee232r.SerialNumber.Length > 0x10)
                        {
                            ee232r.SerialNumber = ee232r.SerialNumber.Substring(0, 0x10);
                        }
                        pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee232r.Manufacturer);
                        pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee232r.ManufacturerID);
                        pData.Description = Marshal.StringToHGlobalAnsi(ee232r.Description);
                        pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee232r.SerialNumber);
                        pData.VendorID = ee232r.VendorID;
                        pData.ProductID = ee232r.ProductID;
                        pData.MaxPower = ee232r.MaxPower;
                        pData.SelfPowered = Convert.ToUInt16(ee232r.SelfPowered);
                        pData.RemoteWakeup = Convert.ToUInt16(ee232r.RemoteWakeup);
                        pData.PullDownEnableR = Convert.ToByte(ee232r.PullDownEnable);
                        pData.SerNumEnableR = Convert.ToByte(ee232r.SerNumEnable);
                        pData.UseExtOsc = Convert.ToByte(ee232r.UseExtOsc);
                        pData.HighDriveIOs = Convert.ToByte(ee232r.HighDriveIOs);
                        pData.EndpointSize = 0x40;
                        pData.PullDownEnableR = Convert.ToByte(ee232r.PullDownEnable);
                        pData.SerNumEnableR = Convert.ToByte(ee232r.SerNumEnable);
                        pData.InvertTXD = Convert.ToByte(ee232r.InvertTXD);
                        pData.InvertRXD = Convert.ToByte(ee232r.InvertRXD);
                        pData.InvertRTS = Convert.ToByte(ee232r.InvertRTS);
                        pData.InvertCTS = Convert.ToByte(ee232r.InvertCTS);
                        pData.InvertDTR = Convert.ToByte(ee232r.InvertDTR);
                        pData.InvertDSR = Convert.ToByte(ee232r.InvertDSR);
                        pData.InvertDCD = Convert.ToByte(ee232r.InvertDCD);
                        pData.InvertRI = Convert.ToByte(ee232r.InvertRI);
                        pData.Cbus0 = ee232r.Cbus0;
                        pData.Cbus1 = ee232r.Cbus1;
                        pData.Cbus2 = ee232r.Cbus2;
                        pData.Cbus3 = ee232r.Cbus3;
                        pData.Cbus4 = ee232r.Cbus4;
                        pData.RIsD2XX = Convert.ToByte(ee232r.RIsD2XX);
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Program == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Program.");
                }
            }
            return ftStatus;
        }

        public FT_STATUS WriteFT4232HEEPROM(FT4232H_EEPROM_STRUCTURE ee4232h)
        {
            FT_STATUS ftStatus = FT_STATUS.FT_OTHER_ERROR;
            FT_ERROR ftErrorCondition = FT_ERROR.FT_NO_ERROR;
            if (this.hFTD2XXDLL != IntPtr.Zero)
            {
                if (this.pFT_EE_Program != IntPtr.Zero)
                {
                    tFT_EE_Program delegateForFunctionPointer = (tFT_EE_Program)Marshal.GetDelegateForFunctionPointer(this.pFT_EE_Program, typeof(tFT_EE_Program));
                    if (this.ftHandle != IntPtr.Zero)
                    {
                        FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_UNKNOWN;
                        this.GetDeviceType(ref deviceType);
                        if (deviceType != FT_DEVICE.FT_DEVICE_4232H)
                        {
                            ftErrorCondition = FT_ERROR.FT_INCORRECT_DEVICE;
                            this.ErrorHandler(ftStatus, ftErrorCondition);
                        }
                        if ((ee4232h.VendorID == 0) | (ee4232h.ProductID == 0))
                        {
                            return FT_STATUS.FT_INVALID_PARAMETER;
                        }
                        FT_PROGRAM_DATA pData = new FT_PROGRAM_DATA
                        {
                            Signature1 = 0,
                            Signature2 = uint.MaxValue,
                            Version = 4,
                            Manufacturer = Marshal.AllocHGlobal(0x20),
                            ManufacturerID = Marshal.AllocHGlobal(0x10),
                            Description = Marshal.AllocHGlobal(0x40),
                            SerialNumber = Marshal.AllocHGlobal(0x10)
                        };
                        if (ee4232h.Manufacturer.Length > 0x20)
                        {
                            ee4232h.Manufacturer = ee4232h.Manufacturer.Substring(0, 0x20);
                        }
                        if (ee4232h.ManufacturerID.Length > 0x10)
                        {
                            ee4232h.ManufacturerID = ee4232h.ManufacturerID.Substring(0, 0x10);
                        }
                        if (ee4232h.Description.Length > 0x40)
                        {
                            ee4232h.Description = ee4232h.Description.Substring(0, 0x40);
                        }
                        if (ee4232h.SerialNumber.Length > 0x10)
                        {
                            ee4232h.SerialNumber = ee4232h.SerialNumber.Substring(0, 0x10);
                        }
                        pData.Manufacturer = Marshal.StringToHGlobalAnsi(ee4232h.Manufacturer);
                        pData.ManufacturerID = Marshal.StringToHGlobalAnsi(ee4232h.ManufacturerID);
                        pData.Description = Marshal.StringToHGlobalAnsi(ee4232h.Description);
                        pData.SerialNumber = Marshal.StringToHGlobalAnsi(ee4232h.SerialNumber);
                        pData.VendorID = ee4232h.VendorID;
                        pData.ProductID = ee4232h.ProductID;
                        pData.MaxPower = ee4232h.MaxPower;
                        pData.SelfPowered = Convert.ToUInt16(ee4232h.SelfPowered);
                        pData.RemoteWakeup = Convert.ToUInt16(ee4232h.RemoteWakeup);
                        pData.PullDownEnable8 = Convert.ToByte(ee4232h.PullDownEnable);
                        pData.SerNumEnable8 = Convert.ToByte(ee4232h.SerNumEnable);
                        pData.ASlowSlew = Convert.ToByte(ee4232h.ASlowSlew);
                        pData.ASchmittInput = Convert.ToByte(ee4232h.ASchmittInput);
                        pData.ADriveCurrent = ee4232h.ADriveCurrent;
                        pData.BSlowSlew = Convert.ToByte(ee4232h.BSlowSlew);
                        pData.BSchmittInput = Convert.ToByte(ee4232h.BSchmittInput);
                        pData.BDriveCurrent = ee4232h.BDriveCurrent;
                        pData.CSlowSlew = Convert.ToByte(ee4232h.CSlowSlew);
                        pData.CSchmittInput = Convert.ToByte(ee4232h.CSchmittInput);
                        pData.CDriveCurrent = ee4232h.CDriveCurrent;
                        pData.DSlowSlew = Convert.ToByte(ee4232h.DSlowSlew);
                        pData.DSchmittInput = Convert.ToByte(ee4232h.DSchmittInput);
                        pData.DDriveCurrent = ee4232h.DDriveCurrent;
                        pData.ARIIsTXDEN = Convert.ToByte(ee4232h.ARIIsTXDEN);
                        pData.BRIIsTXDEN = Convert.ToByte(ee4232h.BRIIsTXDEN);
                        pData.CRIIsTXDEN = Convert.ToByte(ee4232h.CRIIsTXDEN);
                        pData.DRIIsTXDEN = Convert.ToByte(ee4232h.DRIIsTXDEN);
                        pData.AIsVCP8 = Convert.ToByte(ee4232h.AIsVCP);
                        pData.BIsVCP8 = Convert.ToByte(ee4232h.BIsVCP);
                        pData.CIsVCP8 = Convert.ToByte(ee4232h.CIsVCP);
                        pData.DIsVCP8 = Convert.ToByte(ee4232h.DIsVCP);
                        ftStatus = delegateForFunctionPointer(this.ftHandle, pData);
                        Marshal.FreeHGlobal(pData.Manufacturer);
                        Marshal.FreeHGlobal(pData.ManufacturerID);
                        Marshal.FreeHGlobal(pData.Description);
                        Marshal.FreeHGlobal(pData.SerialNumber);
                    }
                    return ftStatus;
                }
                if (this.pFT_EE_Program == IntPtr.Zero)
                {
                    MessageBox.Show("Failed to load function FT_EE_Program.");
                }
            }
            return ftStatus;
        }

        private string InterfaceIdentifier
        {
            get
            {
                string str = string.Empty;
                if (this.IsOpen)
                {
                    FT_DEVICE deviceType = FT_DEVICE.FT_DEVICE_BM;
                    this.GetDeviceType(ref deviceType);
                    if (((deviceType == FT_DEVICE.FT_DEVICE_2232) | (deviceType == FT_DEVICE.FT_DEVICE_2232H)) | (deviceType == FT_DEVICE.FT_DEVICE_4232H))
                    {
                        string str2;
                        this.GetDescription(out str2);
                        return str2.Substring(str2.Length - 1);
                    }
                }
                return str;
            }
        }

        public bool IsOpen
        {
            get
            {
                if (this.ftHandle == IntPtr.Zero)
                {
                    return false;
                }
                return true;
            }
        }

        public class FT_BIT_MODES
        {
            public const byte FT_BIT_MODE_ASYNC_BITBANG = 1;
            public const byte FT_BIT_MODE_CBUS_BITBANG = 0x20;
            public const byte FT_BIT_MODE_FAST_SERIAL = 0x10;
            public const byte FT_BIT_MODE_MCU_HOST = 8;
            public const byte FT_BIT_MODE_MPSSE = 2;
            public const byte FT_BIT_MODE_RESET = 0;
            public const byte FT_BIT_MODE_SYNC_BITBANG = 4;
            public const byte FT_BIT_MODE_SYNC_FIFO = 0x40;
        }

        public class FT_CBUS_OPTIONS
        {
            public const byte FT_CBUS_BITBANG_RD = 12;
            public const byte FT_CBUS_BITBANG_WR = 11;
            public const byte FT_CBUS_CLK12 = 8;
            public const byte FT_CBUS_CLK24 = 7;
            public const byte FT_CBUS_CLK48 = 6;
            public const byte FT_CBUS_CLK6 = 9;
            public const byte FT_CBUS_IOMODE = 10;
            public const byte FT_CBUS_PWRON = 1;
            public const byte FT_CBUS_RXLED = 2;
            public const byte FT_CBUS_SLEEP = 5;
            public const byte FT_CBUS_TXDEN = 0;
            public const byte FT_CBUS_TXLED = 3;
            public const byte FT_CBUS_TXRXLED = 4;
        }

        public class FT_DATA_BITS
        {
            public const byte FT_BITS_7 = 7;
            public const byte FT_BITS_8 = 8;
        }

        public enum FT_DEVICE
        {
            FT_DEVICE_BM,
            FT_DEVICE_AM,
            FT_DEVICE_100AX,
            FT_DEVICE_UNKNOWN,
            FT_DEVICE_2232,
            FT_DEVICE_232R,
            FT_DEVICE_2232H,
            FT_DEVICE_4232H
        }

        public class FT_DEVICE_INFO_NODE
        {
            public string Description;
            public uint Flags;
            public IntPtr ftHandle;
            public uint ID;
            public uint LocId;
            public string SerialNumber;
            public FTDI.FT_DEVICE Type;
        }

        public class FT_DRIVE_CURRENT
        {
            public const byte FT_DRIVE_CURRENT_12MA = 12;
            public const byte FT_DRIVE_CURRENT_16MA = 0x10;
            public const byte FT_DRIVE_CURRENT_4MA = 4;
            public const byte FT_DRIVE_CURRENT_8MA = 8;
        }

        public class FT_EEPROM_DATA
        {
            public string Description = "USB-Serial Converter";
            public string Manufacturer = "FTDI";
            public string ManufacturerID = "FT";
            public ushort MaxPower = 0x90;
            public ushort ProductID = 0x6001;
            public bool RemoteWakeup;
            public bool SelfPowered;
            public string SerialNumber = "";
            public ushort VendorID = 0x403;
        }

        private enum FT_ERROR
        {
            FT_NO_ERROR,
            FT_INCORRECT_DEVICE,
            FT_INVALID_BITMODE,
            FT_BUFFER_SIZE
        }

        public class FT_EVENTS
        {
            public const uint FT_EVENT_LINE_STATUS = 4;
            public const uint FT_EVENT_MODEM_STATUS = 2;
            public const uint FT_EVENT_RXCHAR = 1;
        }

        [Serializable]
        public class FT_EXCEPTION : Exception
        {
            public FT_EXCEPTION()
            {
            }

            public FT_EXCEPTION(string message)
                : base(message)
            {
            }

            protected FT_EXCEPTION(SerializationInfo info, StreamingContext context)
                : base(info, context)
            {
            }

            public FT_EXCEPTION(string message, Exception inner)
                : base(message, inner)
            {
            }
        }

        public class FT_FLAGS
        {
            public const uint FT_FLAGS_HISPEED = 2;
            public const uint FT_FLAGS_OPENED = 1;
        }

        public class FT_FLOW_CONTROL
        {
            public const ushort FT_FLOW_DTR_DSR = 0x200;
            public const ushort FT_FLOW_NONE = 0;
            public const ushort FT_FLOW_RTS_CTS = 0x100;
            public const ushort FT_FLOW_XON_XOFF = 0x400;
        }

        public class FT_LINE_STATUS
        {
            public const byte FT_BI = 0x10;
            public const byte FT_FE = 8;
            public const byte FT_OE = 2;
            public const byte FT_PE = 4;
        }

        public class FT_MODEM_STATUS
        {
            public const byte FT_CTS = 0x10;
            public const byte FT_DCD = 0x80;
            public const byte FT_DSR = 0x20;
            public const byte FT_RI = 0x40;
        }

        public class FT_PARITY
        {
            public const byte FT_PARITY_EVEN = 2;
            public const byte FT_PARITY_MARK = 3;
            public const byte FT_PARITY_NONE = 0;
            public const byte FT_PARITY_ODD = 1;
            public const byte FT_PARITY_SPACE = 4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private class FT_PROGRAM_DATA
        {
            public uint Signature1;
            public uint Signature2;
            public uint Version;
            public ushort VendorID;
            public ushort ProductID;
            public IntPtr Manufacturer;
            public IntPtr ManufacturerID;
            public IntPtr Description;
            public IntPtr SerialNumber;
            public ushort MaxPower;
            public ushort PnP;
            public ushort SelfPowered;
            public ushort RemoteWakeup;
            public byte Rev4;
            public byte IsoIn;
            public byte IsoOut;
            public byte PullDownEnable;
            public byte SerNumEnable;
            public byte USBVersionEnable;
            public ushort USBVersion;
            public byte Rev5;
            public byte IsoInA;
            public byte IsoInB;
            public byte IsoOutA;
            public byte IsoOutB;
            public byte PullDownEnable5;
            public byte SerNumEnable5;
            public byte USBVersionEnable5;
            public ushort USBVersion5;
            public byte AIsHighCurrent;
            public byte BIsHighCurrent;
            public byte IFAIsFifo;
            public byte IFAIsFifoTar;
            public byte IFAIsFastSer;
            public byte AIsVCP;
            public byte IFBIsFifo;
            public byte IFBIsFifoTar;
            public byte IFBIsFastSer;
            public byte BIsVCP;
            public byte UseExtOsc;
            public byte HighDriveIOs;
            public byte EndpointSize;
            public byte PullDownEnableR;
            public byte SerNumEnableR;
            public byte InvertTXD;
            public byte InvertRXD;
            public byte InvertRTS;
            public byte InvertCTS;
            public byte InvertDTR;
            public byte InvertDSR;
            public byte InvertDCD;
            public byte InvertRI;
            public byte Cbus0;
            public byte Cbus1;
            public byte Cbus2;
            public byte Cbus3;
            public byte Cbus4;
            public byte RIsD2XX;
            public byte PullDownEnable7;
            public byte SerNumEnable7;
            public byte ALSlowSlew;
            public byte ALSchmittInput;
            public byte ALDriveCurrent;
            public byte AHSlowSlew;
            public byte AHSchmittInput;
            public byte AHDriveCurrent;
            public byte BLSlowSlew;
            public byte BLSchmittInput;
            public byte BLDriveCurrent;
            public byte BHSlowSlew;
            public byte BHSchmittInput;
            public byte BHDriveCurrent;
            public byte IFAIsFifo7;
            public byte IFAIsFifoTar7;
            public byte IFAIsFastSer7;
            public byte AIsVCP7;
            public byte IFBIsFifo7;
            public byte IFBIsFifoTar7;
            public byte IFBIsFastSer7;
            public byte BIsVCP7;
            public byte PowerSaveEnable;
            public byte PullDownEnable8;
            public byte SerNumEnable8;
            public byte ASlowSlew;
            public byte ASchmittInput;
            public byte ADriveCurrent;
            public byte BSlowSlew;
            public byte BSchmittInput;
            public byte BDriveCurrent;
            public byte CSlowSlew;
            public byte CSchmittInput;
            public byte CDriveCurrent;
            public byte DSlowSlew;
            public byte DSchmittInput;
            public byte DDriveCurrent;
            public byte ARIIsTXDEN;
            public byte BRIIsTXDEN;
            public byte CRIIsTXDEN;
            public byte DRIIsTXDEN;
            public byte AIsVCP8;
            public byte BIsVCP8;
            public byte CIsVCP8;
            public byte DIsVCP8;
        }

        public class FT_PURGE
        {
            public const byte FT_PURGE_RX = 1;
            public const byte FT_PURGE_TX = 2;
        }

        public enum FT_STATUS
        {
            FT_OK,
            FT_INVALID_HANDLE,
            FT_DEVICE_NOT_FOUND,
            FT_DEVICE_NOT_OPENED,
            FT_IO_ERROR,
            FT_INSUFFICIENT_RESOURCES,
            FT_INVALID_PARAMETER,
            FT_INVALID_BAUD_RATE,
            FT_DEVICE_NOT_OPENED_FOR_ERASE,
            FT_DEVICE_NOT_OPENED_FOR_WRITE,
            FT_FAILED_TO_WRITE_DEVICE,
            FT_EEPROM_READ_FAILED,
            FT_EEPROM_WRITE_FAILED,
            FT_EEPROM_ERASE_FAILED,
            FT_EEPROM_NOT_PRESENT,
            FT_EEPROM_NOT_PROGRAMMED,
            FT_INVALID_ARGS,
            FT_OTHER_ERROR
        }

        public class FT_STOP_BITS
        {
            public const byte FT_STOP_BITS_1 = 0;
            public const byte FT_STOP_BITS_2 = 2;
        }

        public class FT2232_EEPROM_STRUCTURE : FTDI.FT_EEPROM_DATA
        {
            public bool AIsHighCurrent;
            public bool AIsVCP = true;
            public bool BIsHighCurrent;
            public bool BIsVCP = true;
            public bool IFAIsFastSer;
            public bool IFAIsFifo;
            public bool IFAIsFifoTar;
            public bool IFBIsFastSer;
            public bool IFBIsFifo;
            public bool IFBIsFifoTar;
            public bool PullDownEnable;
            public bool SerNumEnable = true;
            public ushort USBVersion = 0x200;
            public bool USBVersionEnable = true;
        }

        public class FT2232H_EEPROM_STRUCTURE : FTDI.FT_EEPROM_DATA
        {
            public byte AHDriveCurrent = 4;
            public bool AHSchmittInput;
            public bool AHSlowSlew;
            public bool AIsVCP = true;
            public byte ALDriveCurrent = 4;
            public bool ALSchmittInput;
            public bool ALSlowSlew;
            public byte BHDriveCurrent = 4;
            public bool BHSchmittInput;
            public bool BHSlowSlew;
            public bool BIsVCP = true;
            public byte BLDriveCurrent = 4;
            public bool BLSchmittInput;
            public bool BLSlowSlew;
            public bool IFAIsFastSer;
            public bool IFAIsFifo;
            public bool IFAIsFifoTar;
            public bool IFBIsFastSer;
            public bool IFBIsFifo;
            public bool IFBIsFifoTar;
            public bool PowerSaveEnable;
            public bool PullDownEnable;
            public bool SerNumEnable = true;
        }

        public class FT232B_EEPROM_STRUCTURE : FTDI.FT_EEPROM_DATA
        {
            public bool PullDownEnable;
            public bool SerNumEnable = true;
            public ushort USBVersion = 0x200;
            public bool USBVersionEnable = true;
        }

        public class FT232R_EEPROM_STRUCTURE : FTDI.FT_EEPROM_DATA
        {
            public byte Cbus0 = 5;
            public byte Cbus1 = 5;
            public byte Cbus2 = 5;
            public byte Cbus3 = 5;
            public byte Cbus4 = 5;
            public byte EndpointSize = 0x40;
            public bool HighDriveIOs;
            public bool InvertCTS;
            public bool InvertDCD;
            public bool InvertDSR;
            public bool InvertDTR;
            public bool InvertRI;
            public bool InvertRTS;
            public bool InvertRXD;
            public bool InvertTXD;
            public bool PullDownEnable;
            public bool RIsD2XX;
            public bool SerNumEnable = true;
            public bool UseExtOsc;
        }

        public class FT4232H_EEPROM_STRUCTURE : FTDI.FT_EEPROM_DATA
        {
            public byte ADriveCurrent = 4;
            public bool AIsVCP = true;
            public bool ARIIsTXDEN;
            public bool ASchmittInput;
            public bool ASlowSlew;
            public byte BDriveCurrent = 4;
            public bool BIsVCP = true;
            public bool BRIIsTXDEN;
            public bool BSchmittInput;
            public bool BSlowSlew;
            public byte CDriveCurrent = 4;
            public bool CIsVCP = true;
            public bool CRIIsTXDEN;
            public bool CSchmittInput;
            public bool CSlowSlew;
            public byte DDriveCurrent = 4;
            public bool DIsVCP = true;
            public bool DRIIsTXDEN;
            public bool DSchmittInput;
            public bool DSlowSlew;
            public bool PullDownEnable;
            public bool SerNumEnable = true;
        }

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_Close(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_ClrDtr(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_ClrRts(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_CreateDeviceInfoList(ref uint numdevs);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_CyclePort(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_EE_Program(IntPtr ftHandle, FTDI.FT_PROGRAM_DATA pData);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_EE_Read(IntPtr ftHandle, FTDI.FT_PROGRAM_DATA pData);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_EE_UARead(IntPtr ftHandle, byte[] pucData, int dwDataLen, ref uint lpdwDataRead);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_EE_UASize(IntPtr ftHandle, ref uint dwSize);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_EE_UAWrite(IntPtr ftHandle, byte[] pucData, int dwDataLen);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_EraseEE(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetBitMode(IntPtr ftHandle, ref byte ucMode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetComPortNumber(IntPtr ftHandle, ref int dwComPortNumber);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetDeviceInfo(IntPtr ftHandle, ref FTDI.FT_DEVICE pftType, ref uint lpdwID, byte[] pcSerialNumber, byte[] pcDescription, IntPtr pvDummy);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetDeviceInfoDetail(uint index, ref uint flags, ref FTDI.FT_DEVICE chiptype, ref uint id, ref uint locid, byte[] serialnumber, byte[] description, ref IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetDriverVersion(IntPtr ftHandle, ref uint lpdwDriverVersion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetLatencyTimer(IntPtr ftHandle, ref byte ucLatency);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetLibraryVersion(ref uint lpdwLibraryVersion);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetModemStatus(IntPtr ftHandle, ref uint lpdwModemStatus);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetQueueStatus(IntPtr ftHandle, ref uint lpdwAmountInRxQueue);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_GetStatus(IntPtr ftHandle, ref uint lpdwAmountInRxQueue, ref uint lpdwAmountInTxQueue, ref uint lpdwEventStatus);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_Open(uint index, ref IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_OpenEx(string devstring, uint dwFlags, ref IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_OpenExLoc(uint devloc, uint dwFlags, ref IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_Purge(IntPtr ftHandle, uint dwMask);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_Read(IntPtr ftHandle, byte[] lpBuffer, uint dwBytesToRead, ref uint lpdwBytesReturned);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_ReadEE(IntPtr ftHandle, uint dwWordOffset, ref ushort lpwValue);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_Reload(ushort wVID, ushort wPID);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_Rescan();

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_ResetDevice(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_ResetPort(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_RestartInTask(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetBaudRate(IntPtr ftHandle, uint dwBaudRate);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetBitMode(IntPtr ftHandle, byte ucMask, byte ucMode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetBreakOff(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetBreakOn(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetChars(IntPtr ftHandle, byte uEventCh, byte uEventChEn, byte uErrorCh, byte uErrorChEn);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetDataCharacteristics(IntPtr ftHandle, byte uWordLength, byte uStopBits, byte uParity);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetDeadmanTimeout(IntPtr ftHandle, uint dwDeadmanTimeout);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetDtr(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetEventNotification(IntPtr ftHandle, uint dwEventMask, SafeHandle hEvent);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetFlowControl(IntPtr ftHandle, ushort usFlowControl, byte uXon, byte uXoff);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetLatencyTimer(IntPtr ftHandle, byte ucLatency);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetResetPipeRetryCount(IntPtr ftHandle, uint dwCount);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetRts(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetTimeouts(IntPtr ftHandle, uint dwReadTimeout, uint dwWriteTimeout);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_SetUSBParameters(IntPtr ftHandle, uint dwInTransferSize, uint dwOutTransferSize);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_StopInTask(IntPtr ftHandle);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_Write(IntPtr ftHandle, byte[] lpBuffer, int dwBytesToWrite, ref uint lpdwBytesWritten);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate FTDI.FT_STATUS tFT_WriteEE(IntPtr ftHandle, uint dwWordOffset, ushort wValue);
    }
}
