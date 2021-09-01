//  
//  USB-Relay Utility
//      - A generic tool to handle common 1-8 channel USB-Relay boards.
//      - Developd for Astro-Photograpy Equipment Power Control.
//
//  Author: Min Xie (minxie.dallas@gmail.com)
//

using System;
using System.Collections.Generic;

namespace usbrelay
{
    class Program
    {
        enum Operations { NULL, LIST, STATUS, ONOFF };

        static void Main(string[] args)
        {
            if (args.Length < 1) {
                usage();
            }
            else {
                // command line data
                Operations operation = Operations.NULL;
                string serial = "";
                var on_channels = new HashSet<int>();
                var off_channels = new HashSet<int>();

                parse_arguments(args, ref operation, ref serial, ref on_channels, ref off_channels);

                // process commands
                UsbRelayWrapper control = new UsbRelayWrapper(serial);
                switch(operation)
                {
                    case Operations.LIST: control.list(); break;
                    case Operations.STATUS: control.status(); break;
                    case Operations.ONOFF: control.on_off_channels(on_channels, off_channels); break;
                    default: break;
                }
            }
        }

        static public void usage()
        {
            Console.WriteLine("NAME");
            Console.WriteLine("\tusbrelay - a simple utility to control, list and query USB-Relay devices");
            Console.WriteLine();
            Console.WriteLine("SYNOPSIS");
            Console.WriteLine("\tusbrelay [ -list ] [ -status ] [ -serial serial-number ]");
            Console.WriteLine("\t         [ -on channels ] [ -off channels ]");
            Console.WriteLine();
            Console.WriteLine("COMMAND LINE OPTIONS");
            Console.WriteLine("\t-list\t\tList all available serial numbers of USB-Relay devices connected.");
            Console.WriteLine();
            Console.WriteLine("\t-status\t\tDisplay on/off status of all the channels on the USB-Relay devices connected.");
            Console.WriteLine();
            Console.WriteLine("\t-serial serial-number");
            Console.WriteLine("\t\t\tSpecify the serial number of the USB-Relay device to operate.");
            Console.WriteLine();
            Console.WriteLine("\t-on channels");
            Console.WriteLine("\t\t\tTurn on the relay channels specified.");
            Console.WriteLine();
            Console.WriteLine("\t-off channels");
            Console.WriteLine("\t\t\tTurn off the relay channels specified.");
            Console.WriteLine();
            Console.WriteLine("EXAMPLES");
            Console.WriteLine("\tusbrelay -list");
            Console.WriteLine();
            Console.WriteLine("\tusbrelay -status");
            Console.WriteLine();
            Console.WriteLine("\tusbrelay -serial BITFT -on 1");
            Console.WriteLine("\tusbrelay -serial BITFT -on 1 2 3");
            Console.WriteLine();
            Console.WriteLine("\tusbrelay -serial BITFT -off 2");
            Console.WriteLine("\tusbrelay -serial BITFT -off 2 5 6");
            Console.WriteLine();
            Console.WriteLine("\tusbrelay -serial BITFT -on 1 -off 2");
            Console.WriteLine("\tusbrelay -serial BITFT -on 1 3 5 -off 2 4 6");
            Console.WriteLine();
        }

        static void parse_arguments(string[] args, ref Operations operation, ref string serial, 
            ref HashSet<int> on_channels, ref HashSet<int> off_channels)
        {
            for (int arg_index = 0; arg_index < args.Length;)
            {
                switch (args[arg_index])
                {
                    case "-list":
                        operation = Operations.LIST;
                        arg_index++;
                        break;
                    case "-status":
                        operation = Operations.STATUS;
                        arg_index++;
                        break;
                    case "-serial":
                        if (++arg_index < args.Length)
                            serial = args[arg_index];
                        arg_index++;
                        break;
                    case "-on":
                        operation = Operations.ONOFF;
                        if (++arg_index < args.Length)
                            if (parse_channels(args, ref arg_index, ref on_channels) == false)
                                return;
                        break;
                    case "-off":
                        operation = Operations.ONOFF;
                        if (++arg_index < args.Length)
                            if (parse_channels(args, ref arg_index, ref off_channels) == false)
                                return;
                        break;
                    default:
                        arg_index++;
                        break;
                }
            }
        }

        static bool parse_channels(string[] args, ref int arg_index, ref HashSet<int> channels)
        {
            while(arg_index < args.Length)
            {
                if (args[arg_index].Substring(0, 1) == "-")
                    return true;
                int channel = 0;
                try { channel = Convert.ToInt32(args[arg_index++]); } catch { }
                if (channel > 0)
                    channels.Add(channel);
            }
            return false;
        }

    }
}

