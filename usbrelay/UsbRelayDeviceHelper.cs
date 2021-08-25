using System;
using System.Runtime.InteropServices;

namespace usbrelay
{
    public class UsbRelayDeviceHelper
    {

        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_init", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Init();
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_exit", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Exit();
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_enumerate", CallingConvention = CallingConvention.Cdecl)]

        //  public static extern UsbRelayDeviceInfo Enumerate();
        public static extern IntPtr usb_relay_device_enumerate();
        public static UsbRelayDeviceInfo Enumerate()
        {
            IntPtr x = UsbRelayDeviceHelper.usb_relay_device_enumerate();
            UsbRelayDeviceInfo a = (UsbRelayDeviceInfo)Marshal.PtrToStructure(x, typeof(UsbRelayDeviceInfo));
            return a;
        }
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_free_enumerate", CallingConvention = CallingConvention.Cdecl)]
        public static extern void FreeEnumerate(UsbRelayDeviceInfo deviceInfo);

        /// <summary>  
        /// Open device that serial number is serialNumber  
        /// </summary>  
        /// <param name="serialNumber"></param>  
        /// <param name="stringLength"></param>  
        /// <returns>This funcation returns a valid handle to the device on success or NULL on failure.</returns>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_open_with_serial_number", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenWithSerialNumber([MarshalAs(UnmanagedType.LPStr)] string serialNumber, int stringLength);

        /// <summary>  
        /// Open a usb relay device  
        /// </summary>  
        /// <param name="deviceInfo"></param>  
        /// <returns>This funcation returns a valid handle to the device on success or NULL on failure.</returns>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_open", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Open(UsbRelayDeviceInfo deviceInfo);

        /// <summary>  
        /// Close a usb relay device  
        /// </summary>  
        /// <param name="hHandle"></param>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_close", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Close(int hHandle);

        /// <summary>  
        /// open a relay channel on the USB-Relay-Device  
        /// </summary>  
        /// <param name="hHandle">Which usb relay device your want to operate</param>  
        /// <param name="index">Which channel your want to open</param>  
        /// <returns>0 -- success; 1 -- error; 2 -- index is outnumber the number of the usb relay device</returns>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_open_one_relay_channel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenOneRelayChannel(int hHandle, int index);

        /// <summary>  
        /// open all relay channel on the USB-Relay-Device  
        /// </summary>  
        /// <param name="hHandle">which usb relay device your want to operate</param>  
        /// <returns>0 -- success; 1 -- error</returns>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_open_all_relay_channel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int OpenAllRelayChannels(int hHandle);

        /// <summary>  
        /// close a relay channel on the USB-Relay-Device  
        /// </summary>  
        /// <param name="hHandle">which usb relay device your want to operate</param>  
        /// <param name="index">which channel your want to close</param>  
        /// <returns>0 -- success; 1 -- error; 2 -- index is outnumber the number of the usb relay device</returns>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_close_one_relay_channel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseOneRelayChannel(int hHandle, int index);

        /// <summary>  
        /// close all relay channel on the USB-Relay-Device  
        /// </summary>  
        /// <param name="hHandle">hich usb relay device your want to operate</param>  
        /// <returns>0 -- success; 1 -- error</returns>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_close_all_relay_channel", CallingConvention = CallingConvention.Cdecl)]
        public static extern int CloseAllRelayChannels(int hHandle);

        /// <summary>  
        /// status bit: High --> Low 0000 0000 0000 0000 0000 0000 0000 0000, one bit indicate a relay status.  
        /// the lowest bit 0 indicate relay one status, 1 -- means open status, 0 -- means closed status.  
        /// bit 0/1/2/3/4/5/6/7/8 indicate relay 1/2/3/4/5/6/7/8 status  
        /// </summary>  
        /// <param name="hHandle"></param>  
        /// <param name="status"></param>  
        /// <returns>0 -- success; 1 -- error</returns>  
        [DllImport("usb_relay_device.dll", EntryPoint = "usb_relay_device_get_status", CallingConvention = CallingConvention.Cdecl)]
        public static extern int GetStatus(int hHandle, ref int status);
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public class UsbRelayDeviceInfo
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string SerialNumber;

            public IntPtr DevicePath { get; set; }

            public UsbRelayDeviceType Type { get; set; }

            public IntPtr Next { get; set; }
        }

        public enum UsbRelayDeviceType
        {
            OneChannel = 1,
            TwoChannel = 2,
            FourChannel = 4,
            EightChannel = 8
        }

    }
}

