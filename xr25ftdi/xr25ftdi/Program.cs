using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using xr25ftdi.Classes;

namespace xr25ftdi
{
    class Program
    {
        static void Main(string[] args)
        {
            IDevice device = DeviceFactory.GetDevice(args[0]);
            byte b,hi,lo;
            int v;

            do
            {
                if (device.GetByte() == 0xFF && device.GetByte() == 0x00)
                {
                    Console.Clear();

                    b = device.GetByte();
                    WriteToConsole("Program ver: ({0}){1}", b.ToString("x"), b);

                    b = device.GetByte();
                    WriteToConsole("Prom ver: ({0}){1}", b.ToString("x"), b);

                    b = device.GetByte();
                    WriteToConsole("Flags IO: ({0}){1}", b.ToString("x"), b);

                    b = device.GetByte();
                    WriteToConsole("MAP: ({0}){1}", b.ToString("x"), ((int)b * 12.4).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("ECT: ({0}){1}", b.ToString("x"), (((int)b * 0.625) - 40.0).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("IAT: ({0}){1}", b.ToString("x"), (((int)b * 0.625) - 40.0).ToString(".00"));                    

                    b = device.GetByte();
                    WriteToConsole("VBAT: ({0}){1}", b.ToString("x"), (((int)b * 0.0312) + 8.0).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("P-CO: ({0}){1}", b.ToString("x"), ((int)b * 0.4).ToString(".00"));

                    lo = device.GetByte();
                    hi = device.GetByte();
                    v = (hi * byte.MaxValue) + lo;
                    WriteToConsole("RPM: ({0}){1}", v.ToString("x"), ((15.6 * 1000) / v).ToString(".00"));

                    lo = device.GetByte();
                    hi = device.GetByte();
                    v = (hi * byte.MaxValue) + lo;
                    WriteToConsole("INJ: ({0}){1}", v.ToString("x"), ((15.6 * 1000) / v).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("KNOCK: ({0}){1}", b.ToString("x"), ((int)b * 0.4).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("SPARK: ({0}){1}", b.ToString("x"), b);

                    b = device.GetByte();
                    WriteToConsole("RCO-RR: ({0}){1}", b.ToString("x"), ((int)b * 0.4).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("RCO-WG: ({0}){1}", b.ToString("x"), (((int)b * -3.7) + 1040).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("?: ({0}){1}", b.ToString("x"), b);

                    b = device.GetByte();
                    WriteToConsole("ERRORS PERM: ({0}){1}[{2}]", b.ToString("x"), b, Convert.ToString(b, 2));

                    b = device.GetByte();
                    WriteToConsole("SPEED: ({0}){1}", b.ToString("x"), b);

                    b = device.GetByte();
                    WriteToConsole("RR-nom: ({0}){1}", b.ToString("x"), (((int)b / 2.5)).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("TPS: ({0}){1}", b.ToString("x"), (((int)b / 2.5)).ToString(".00"));

                    b = device.GetByte();
                    WriteToConsole("ERRORS 1: ({0}){1}[{2}]", b.ToString("x"), b, Convert.ToString(b, 2));

                    b = device.GetByte();
                    WriteToConsole("ERRORS 2: ({0}){1}[{2}]", b.ToString("x"), b, Convert.ToString(b, 2));
                }

                Thread.Sleep(50);
            }
            while (true);
        }

        static void WriteToConsole(string message, params object[] values)
        {
            Console.Write(string.Format(message + "\r\n", values));
        }
    }
}
