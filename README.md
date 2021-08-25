# usbrelay

This small utility was originally implemented for automation of power management for different components by controlling multiple USB-Relay boards on the Astrophotograpy control computer. 

The implementation is based on the great work of [usb-relay-hid](https://github.com/pavel-a/usb-relay-hid) project by [pavel-a](https://github.com/pavel-a). Most of the common USB-Relay boards from Amazon, Aliexpress or Taobao are supported. The goal of this project is to provide a simple control utility for querying and controlling multiple USB-Relay boards connected to the control Windows computer.

![2-Channel & 8-Channel USB-Relay Boards](https://github.com/mxcoppell/usbrelay/blob/master/images/usbrelay-boards.jpg?raw=true)

The binary provided is a stand-alone executable. All required dynamic link libraries are embedded into the the single executable. 

To build this project, please add [Costura.Fody](https://github.com/Fody/Costura) to the project. (Right click 'usbrelay' project in Visual Studio. Select 'Manage NuGet Packages...')

# binary download

Source/Document download: [This GitHub Project](https://github.com/mxcoppell/usbrelay/) 

Binary download (Windows 10): [usbrelay.zip](https://github.com/mxcoppell/usbrelay/blob/master/binary/usbrelay.zip)

# current state

Current version is implemented in C# (VS2019). Porting to other platforms should be straightfoward. 

# help

```
NAME
        usbrelay - a simple utility to control, list and query USB-Relay devices

SYNOPSIS
        usbrelay [ -list ] [ -status ] [ -serial serial-number ]
                 [ -open channels ] [ -close channels ]

COMMAND LINE OPTIONS
        -list           List all available serial numbers of USB-Relay devices connected.

        -status         Display open/close status of all the channels on the USB-Relay devices connected.

        -serial serial-number
                        Specify the serial number of the USB-Relay device to operate.

        -open channels
                        Open the relay channels specified.

        -close channels
                        Close the relay channels specified.

EXAMPLES
        usbrelay -list

        usbrelay -status

        usbrelay -serial BITFT -open 1
        usbrelay -serial BITFT -open 1 2 3

        usbrelay -serial BITFT -close 2
        usbrelay -serial BITFT -close 2 5 6

        usbrelay -serial BITFT -open 1 -close 2
        usbrelay -serial BITFT -open 1 3 5 -close 2 4 6
```

# examples

# license

The shared library [usb_relay_device.dll] and helper code [UsbRelayDeviceHelper.cs](https://github.com/mxcoppell/usbrelay/blob/master/usbrelay/UsbRelayDeviceHelper.cs) are from project [usb-relay-hid](https://github.com/pavel-a/usb-relay-hid). They are dual licensed: GPL + commercial.

There are no software packages (SDK) provided by the hardware vendors.
