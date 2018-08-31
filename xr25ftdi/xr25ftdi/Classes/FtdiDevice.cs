using FTD2XX_NET;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace xr25ftdi.Classes
{
    public class FtdiDevice : IDevice
    {
        private FTDI ftdi = new FTDI();
        private FTDI.FT_DEVICE_INFO_NODE[] deviceList;

        public FtdiDevice(string portName)
        {
            var ftStatus = ftdi.OpenBySerialNumber(portName);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                throw new Exception("Failed to open device (error " + ftStatus.ToString() + ")");
            }

            if (!ftdi.IsOpen)
            {
                throw new Exception("Device is not open");
            }

            ftStatus = SetDeviceProperty(62500
                , FTDI.FT_DATA_BITS.FT_BITS_8
                , FTDI.FT_STOP_BITS.FT_STOP_BITS_1
                , FTDI.FT_PARITY.FT_PARITY_NONE
                , FTDI.FT_FLOW_CONTROL.FT_FLOW_NONE
                , 0
                , 0
                , 1000
                , 0
                , false
                , false);

            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                throw new Exception("Failed to set data characteristics (error " + ftStatus.ToString() + ")");
            }
        }        

        public byte GetByte()
        {
            byte[] buffer = new byte[1];
            uint readedBytes = 0;

            do
            {
                var ftStatus = ftdi.Read(buffer, 1, ref readedBytes);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    Thread.Sleep(5);
                    continue;
                }
            }
            while (readedBytes < 1);

            return buffer[0];
        }

        private FTDI.FT_STATUS SetDeviceProperty(uint baudrate
                                                , byte databits
                                                , byte stopbits
                                                , byte parity
                                                , ushort flowcontrol
                                                , byte Xon
                                                , byte Xoff
                                                , uint readtimeout
                                                , uint writetimeout
                                                , bool dtr
                                                , bool rts)
        {
            FTDI.FT_STATUS ftStatus = ftdi.SetBaudRate(baudrate);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return ftStatus;
            }

            ftStatus = ftdi.SetDataCharacteristics(databits, stopbits, parity);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return ftStatus;
            }

            ftStatus = ftdi.SetFlowControl(flowcontrol, Xon, Xoff);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return ftStatus;
            }

            ftStatus = ftdi.SetTimeouts(readtimeout, writetimeout);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return ftStatus;
            }

            ftStatus = ftdi.SetDTR(dtr);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return ftStatus;
            }

            ftStatus = ftdi.SetRTS(rts);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                return ftStatus;
            }

            return FTDI.FT_STATUS.FT_OK;
        }
    }
}
