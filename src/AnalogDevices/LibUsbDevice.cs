﻿using System;
using System.Collections.Generic;
using System.Linq;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace AnalogDevices
{
    internal class LibUsbDevice : IUsbDevice
    {
        private readonly UsbDevice _libUsbDevice;

        public LibUsbDevice(UsbDevice libUsbDevice)
        {
            _libUsbDevice = libUsbDevice;
        }

        public static IEnumerable<LibUsbDevice> AllDevices
        {
            get { return UsbDevice.AllDevices.Where(x=>x.Device!=null).Select(x => new LibUsbDevice(x.Device)); }
        }

        public virtual void ControlTransfer(byte requestType, byte request, int value, int index, byte[] buffer = null,
            int? length = null)
        {
            //SGEORGE takes about 100-200ns
            var libUsbSetupPacket = new UsbSetupPacket(requestType, request, (short) value, (short) index,
                (short) (length ?? 0));

   
           


            //SGEORGE takes about 850600 ns or 0.8506 ms
            _libUsbDevice.ControlTransfer(ref libUsbSetupPacket, buffer, libUsbSetupPacket.Length,
                out int _);
            
            
            //SGEORGE takes about 100-200ns
            ControlTransferSent?.Invoke(this,
                new ControlTransferSentEventArgs(requestType, request, value, index, buffer, length));


           

        }

        public event EventHandler<ControlTransferSentEventArgs> ControlTransferSent;
        public int Vid => _libUsbDevice.UsbRegistryInfo.Vid;
        public int Pid => _libUsbDevice.UsbRegistryInfo.Pid;
        public string SymbolicName => _libUsbDevice.UsbRegistryInfo.SymbolicName;

        public void Dispose()
        {
            _libUsbDevice.Close();
        }
    }
}