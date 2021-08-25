//  
//  USB-Relay Utility
//      - A generic tool to handle common 1-8 channel USB-Relay boards.
//      - Developd for Astro-Photograpy Equipment Power Control.
//
//  Author: Min Xie (minxie.dallas@gmail.com)
//

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace usbrelay
{
    class UsbRelayWrapper
    {
        private string target_serial { get; set; }

        public UsbRelayWrapper(string serial)
        {
            target_serial = serial;
        }
        ~UsbRelayWrapper() { }

        public int open_device_handle(string serial)
        {
            if (serial != "")
            {
                int retval = UsbRelayDeviceHelper.OpenWithSerialNumber(serial, serial.Length);
                return retval;
            }
            return 0;
        }

        public void open_close_channels(HashSet<int> on_channels, HashSet<int> off_channels)
        {
            int device_handle = open_device_handle(target_serial);
            if (device_handle == 0)
                Console.WriteLine(String.Format("ERROR: failed to open device {0}.", target_serial));

            foreach (int i in on_channels)
            {
                int retval = UsbRelayDeviceHelper.OpenOneRelayChannel(device_handle, i);
                Console.WriteLine(String.Format("Open channel {0} on {1}: {2}", i, target_serial, open_close_channel_msg(retval)));
            }
            foreach (int i in off_channels)
            {
                int retval = UsbRelayDeviceHelper.CloseOneRelayChannel(device_handle, i);
                Console.WriteLine(String.Format("Close channel {0} on {1}: {2}", i, target_serial, open_close_channel_msg(retval)));
            }

            UsbRelayDeviceHelper.Close(device_handle);

            Console.WriteLine();
            status();
        }

        private string open_close_channel_msg(int status)
        {
            switch (status)
            {
                case 0: return "success";
                case 1: return "error";
                case 2: return "index exceeds the channel number range of the usb relay device";
                default: return "unknown error";
            }
        }

        public void list()
        {
            UsbRelayDeviceHelper.UsbRelayDeviceInfo usb_relay_it = null;
            usb_relay_it = UsbRelayDeviceHelper.Enumerate();

            if (usb_relay_it != null)
            {
                Console.WriteLine("Serial    Type");
                Console.WriteLine("------    ----");
            }
            while (usb_relay_it != null)
            {
                Console.WriteLine(String.Format("{0}     {1}", usb_relay_it.SerialNumber, usb_relay_it.Type));
                IntPtr next_it = usb_relay_it.Next;
                usb_relay_it = (UsbRelayDeviceHelper.UsbRelayDeviceInfo)Marshal.PtrToStructure(
                    next_it, 
                    typeof(UsbRelayDeviceHelper.UsbRelayDeviceInfo));
            }
        }

        public void status()
        {
            UsbRelayDeviceHelper.UsbRelayDeviceInfo usb_relay_it = null;
            usb_relay_it = UsbRelayDeviceHelper.Enumerate();

            if (usb_relay_it != null)
            {
                Console.Write("Serial  ");
                for (int channel = 1; channel <= 8; channel++) Console.Write(String.Format(" C{0}  ", channel));
                Console.WriteLine();
                Console.Write("------  ");
                for (int channel = 0; channel < 8; channel++) Console.Write(String.Format("---- ", channel));
                Console.WriteLine();
            }

            while (usb_relay_it != null)
            {
                string serial = usb_relay_it.SerialNumber;
                Console.Write(String.Format("{0}   ", serial));

                int channels = Convert.ToInt32(usb_relay_it.Type);
                int device_handle = open_device_handle(serial);
                if (device_handle == 0)
                    Console.WriteLine("ERROR: failed to open device.");

                int status = 0;
                if (UsbRelayDeviceHelper.GetStatus(device_handle, ref status) != 0)
                    Console.WriteLine("ERROR: failed to retrieve channel status.");
                else
                {
                    for (int channel = 1; channel <= channels; channel++)
                        if ((status & (1 << (channel - 1))) > 0)
                            Console.Write("ON   ");
                        else
                            Console.Write("OFF  "); 
                }

                UsbRelayDeviceHelper.Close(device_handle);
                
                IntPtr next_it = usb_relay_it.Next;
                usb_relay_it = (UsbRelayDeviceHelper.UsbRelayDeviceInfo)Marshal.PtrToStructure(
                    next_it, 
                    typeof(UsbRelayDeviceHelper.UsbRelayDeviceInfo));
            }
        }

    }
}

