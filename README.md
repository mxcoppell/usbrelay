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

[Version 1.0.0.3](https://github.com/mxcoppell/usbrelay/releases/tag/1.0.0.3) - [usbrelay-v1.0.0.3.zip](https://github.com/mxcoppell/usbrelay/releases/download/1.0.0.3/usbrelay-v1.0.0.3.zip)

# current state

Current version is implemented in C# (VS2019). Porting to other platforms should be straightfoward. 

# help

```
NAME
        usbrelay - a simple utility to control, list and query USB-Relay devices

SYNOPSIS
        usbrelay [ -list ] [ -status ] [ -serial serial-number ]
                 [ -on channels ] [ -off channels ]

COMMAND LINE OPTIONS
        -list           List all available serial numbers of USB-Relay devices connected.

        -status         Display on/off status of all the channels on the USB-Relay devices connected.

        -serial serial-number
                        Specify the serial number of the USB-Relay device to operate.

        -on channels
                        Turn on the relay channels specified.

        -off channels
                        Turn off the relay channels specified.

EXAMPLES
        usbrelay -list

        usbrelay -status

        usbrelay -serial BITFT -on 1
        usbrelay -serial BITFT -on 1 2 3

        usbrelay -serial BITFT -off 2
        usbrelay -serial BITFT -off 2 5 6

        usbrelay -serial BITFT -on 1 -off 2
        usbrelay -serial BITFT -on 1 3 5 -off 2 4 6
```

# examples

List all availabe USB-Relay boards.
```
PS D:\bin>
PS D:\bin> .\usbrelay.exe -list
Serial    Type
------    ----
SMLFT     TwoChannel
BIGFT     TwoChannel
```

Display the status of all availabe ports.
```
PS D:\bin>
PS D:\bin> .\usbrelay.exe -status
Serial   C1   C2   C3   C4   C5   C6   C7   C8
------  ---- ---- ---- ---- ---- ---- ---- ----
SMLFT   OFF  OFF
BIGFT   OFF  OFF
```

On board with serial BIGFT, turn on channel 1 and turn off channel 2.
```
PS D:\bin>
PS D:\bin> .\usbrelay.exe -serial BIGFT -on 1 -off 2
Turn on channel 1 on BIGFT: success
Turn off channel 2 on BIGFT: success

Serial   C1   C2   C3   C4   C5   C6   C7   C8
------  ---- ---- ---- ---- ---- ---- ---- ----
SMLFT   OFF  OFF
BIGFT   ON   OFF
```

On board with serial SMLFT, turn on both channel 1 & 2.
```
PS D:\bin>
PS D:\bin> .\usbrelay.exe -serial SMLFT -on 1 2
Turn on channel 1 on SMLFT: success
Turn on channel 2 on SMLFT: success

Serial   C1   C2   C3   C4   C5   C6   C7   C8
------  ---- ---- ---- ---- ---- ---- ---- ----
SMLFT   ON   ON
BIGFT   ON   OFF
```

On board with serial BIGFT, turn off both channel 1 & 2.
```
PS D:\bin>
PS D:\bin> .\usbrelay.exe -serial BIGFT -off 1 2
Turn off channel 1 on BIGFT: success
Turn off channel 2 on BIGFT: success

Serial   C1   C2   C3   C4   C5   C6   C7   C8
------  ---- ---- ---- ---- ---- ---- ---- ----
SMLFT   ON   ON
BIGFT   OFF  OFF
```

# license

The shared library [usb_relay_device.dll] and helper code [UsbRelayDeviceHelper.cs](https://github.com/mxcoppell/usbrelay/blob/master/usbrelay/UsbRelayDeviceHelper.cs) are from project [usb-relay-hid](https://github.com/pavel-a/usb-relay-hid). They are dual-licensed: GPL + commercial.

There are no software packages (SDK) provided by the hardware vendors.
