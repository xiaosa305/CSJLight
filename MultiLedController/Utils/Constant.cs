﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiLedController.utils
{
    public class Constant
    {
        public const string TAG_XIAOSA = "XiaoSa";
        public const string RECEIVE_START_DEBUF_MODE = "OK:poweron>";
        public static bool IsLogInFile = false;
        private static readonly byte[] Receive_SearchDevice = new byte[]
        {
            0x41,0x72,0x74,0x2D,0x4E,0x65,0x74,0x00
                ,0x00,0x021
                ,0xC0,0xA8,0x01,0x08//ip地址
                ,0x36,0x19
                //,0x04
                ,0x01
                ,0x00

                ,0x00,0x00 //15位的端口地址的14-8位被编码成字段最低7位，用来结合SubSwitch和Swin[]或Swout[]产生完整集合地址;15位端口地址的7-4位被编码成字段的最低4位。用来结合SubSwitch和Swin[]或Swout[]产生完整集合地址
                
                ,0x00
                ,0x12
                ,0x00
                ,0x00
                ,0x48
                
                ,0x43,0x59,0x4C,0x2D,0x32,0x32,0x30,0x34,0x38,0x20,0x20,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                ,0x43,0x59,0x4C,0x2D,0x32,0x30,0x34,0x38,0x20,0x20,0x41,0x72,0x74,0x2D,0x4E,0x65,0x74,0x20,0x6E,0x6F,0x64,0x65,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                ,0x23,0x30,0x30,0x30,0x31,0x20,0x5B,0x30,0x30,0x30,0x30,0x5D,0x20,0x53,0x74,0x6D,0x41,0x72,0x74,0x4E,0x6F,0x64,0x65,0x20,0x69,0x73,0x20,0x72,0x65,0x61,0x64,0x72,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
                ,0x00
                ,0x04
                ,0x80,0x80,0x80,0x80
                ,0x08,0x08,0x08,0x08
                ,0x00,0x00,0x00,0x00    
                ,0x00,0x01,0x02,0x03//4个端口
                ,0x00,0x01,0x02,0x03//4个端口
                ,0x00
                ,0x00
                ,0x00
                ,0x00,0x00,0x00
                ,0x00
                ,0x04,0xD4,0xC4,0x53,0xB0,0x59//MAC
                ,0x00,0x00,0x00,0x00    //BindIp
                ,0x00   //BindInex
                ,0x00   //Status2   
                ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
        };
        private static readonly byte[] M5_Receive_SearchDevice = new byte[]
        {
            0x41,0x72,0x74,0x2D,0x4E,0x65,0x74,0x00
            ,0x00,0x21
            ,0xC0,0xA8,0x1F,0x08
            ,0x36,0x19
            ,0x01
            ,0x00
            ,0x00,0x00//netSwitch和subSwitch
            ,0x00
            ,0x12
            ,0x00
            ,0x00
            ,0x48
            ,0x41
            //ShowName
            ,0x48,0x38,0x30,0x32,0x52,0x41,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            //LongName
            ,0x4C,0x45,0x44,0x20,0x43,0x6F,0x6E,0x74
            ,0x72,0x6F,0x6C,0x6C,0x65,0x72,0x20,0x41
            ,0x72,0x74,0x4E,0x65,0x74,0x2D,0x4E,0x6F
            ,0x64,0x65,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            //NodeReport
            ,0x23,0x30,0x30,0x30,0x31,0x20,0x5B,0x30
            ,0x30,0x33,0x30,0x5D,0x20,0x72,0x65,0x61
            ,0x64,0x79,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00//NumPortsHi
            ,0x04//NumPortsLo
            ,0x80,0x80,0x80,0x80//PortTypes
            ,0x08,0x08,0x08,0x08//GoodInput
            ,0x80,0x80,0x80,0x80//GoodOutput
            ,0x00,0x01,0x02,0x03//SwIn
            ,0x00,0x01,0x02,0x03//SwOut
            ,0x00//SwVideo
            ,0x00//SwMacro
            ,0x00//SwRemote
            ,0x00//Spare
            ,0x00//Spare
            ,0x00//Spare
            ,0x00//Style
            ,0x48,0x67,0x29,0x48,0x1A,0x75//MacAddress
            ,0xC0,0xA8,0x1F,0x0B//BindIp
            ,0x00//BIndIndex
            ,0x08//Status2
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00
            ,0x00
        };

        public static byte[] GetReceiveDataBySerchDeviceOrder()
        {
            return Receive_SearchDevice;
        }
        public static byte[] GetM5ReceiveDataBySerchDeviceOrder()
        {
            return M5_Receive_SearchDevice;
        }
    }
}
