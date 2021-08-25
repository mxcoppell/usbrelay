# usbrelay

This small utility was initially implemented to automate power management for different components by controlling multiple USB-Relay boards on the windows OS based astrophotography control computer. 

The implementation is based on the great work of [usb-relay-hid](https://github.com/pavel-a/usb-relay-hid) project by [pavel-a](https://github.com/pavel-a). Most of the common USB-Relay boards from Amazon, Aliexpress, or Taobao are supported. This project provides a simple control utility for querying and controlling multiple USB-Relay boards connected to the Windows computer.

![2-Channel & 8-Channel USB-Relay Boards](https://github.com/mxcoppell/usbrelay/blob/master/images/usbrelay-boards.jpg?raw=true)

The binary provided is a stand-alone executable. All required dynamic link libraries are embedded into the the single executable. 

To build this project, please add [Costura.Fody](https://github.com/Fody/Costura) to the project. (Right click 'usbrelay' project in Visual Studio. Select 'Manage NuGet Packages...')

# serial numbers of the USB-Relay boards

Most likely, the boards from Amazon, eBay or other places will come with serial number "BITFT". This will be an issue when two or more USB-Relay boards need to be connected to the same computer. Unique serial numbers need to be assigned to each board.

The easiest way to do this is to use a Linux box. For example, a Raspberry Pi. 

Install 'usbrelay' package.
```
> sudo apt install usbrelay
```

Change the default board serial number 'BITFT' to 'NEWBN'(just an example, your own serial number preferred).
```
> sudo usbrelay BITFT=NEWBN
BITFT_1=0
BITFT_2=0
Setting new serial
```

Verify the new serial number.
```
> sudo usbrelay
NEWBN_1=0
NEWBN_2=0
```

# binary release

[Version 1.0.0.2](https://github.com/mxcoppell/usbrelay/releases/tag/1.0.0.2) - [usbrelay-v1.0.0.2.zip](https://github.com/mxcoppell/usbrelay/releases/download/1.0.0.2/usbrelay-v1.0.0.2.zip)

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

The shared library [usb_relay_device.dll] and helper code [UsbRelayDeviceHelper.cs](https://github.com/mxcoppell/usbrelay/blob/master/usbrelay/UsbRelayDeviceHelper.cs) are from project [usb-relay-hid](https://github.com/pavel-a/usb-relay-hid). They are dual-licensed: GPL + commercial.

There are no software packages (SDK) provided by the hardware vendors.
