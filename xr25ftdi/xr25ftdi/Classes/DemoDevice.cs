using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xr25ftdi.Classes
{
    public class DemoDevice : IDevice
    {
        private List<byte> data = new List<byte>();
        private int pointer = 0;

        public DemoDevice()
        {
            FileStream sr = new FileStream("Resources\\example.txt", FileMode.Open);
            while (sr.Position != sr.Length)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append((char)sr.ReadByte());
                sb.Append((char)sr.ReadByte());
                data.Add(Convert.ToByte(sb.ToString(), 16));
            }            
        }

        public byte GetByte()
        {
            if (data.Count == 0)
                return 0;
            else
            {
                if (pointer == data.Count)
                    pointer = 0;

                return data[pointer++];
            }
        }
    }
}
