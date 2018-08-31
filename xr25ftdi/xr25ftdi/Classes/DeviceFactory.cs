using System;
using System.Collections.Generic;
using System.Text;

namespace xr25ftdi.Classes
{
    public class DeviceFactory
    {
        public static IDevice GetDevice(string name)
        {
            if(string.Compare(name, "demo") == 0)
            {
                return new DemoDevice();
            }
            else
            {
                return new FtdiDevice(name);
            }
        }
    }
}
