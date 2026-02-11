using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
namespace MMISoft.HSMS
{
    public class HSMSConfig
    {
        public HSMSConfig()
        {
            Console.WriteLine("HSMSConfig: Constructor Starting...");
            LogFileName = "HSMS";
            LogPath = "HSMS";
            LogFileSize = 1000000;
            ShowErrorMessageBox = false;
            Mode = ModeEnum.PASSIVE;
            IP = "localhost";
            Port = 7000;
            T3 = 5;
            T5 = 45;
            T6 = 45;
            T7 = 45;
            Try3 = 1;
            Try5 = 1;
            Try6 = 1;
            Try7 = 1;
            LinkTestTime = 45;
            AutoMakeSystemByte = true;
            Console.WriteLine("HSMSConfig: Constructor Complete.");
        }
        public enum ModeEnum { ACTIVE, PASSIVE}

        public bool ShowErrorMessageBox { get; set; }
        [Category("연결")]
        public int Device { get; set; }
        [Category("연결")]
        public int KnownDeviceID { get; set; }
        [Category("연결")]
        public int Port { get; set; }
        [Category("연결")]
        public string IP { get; set; }
        [Category("연결")]
        public int LinkTestTime { get; set; }
        [Category("연결")]
        public ModeEnum Mode { get; set; }

        [Category("Timeout")]
        public int T3 { get; set; }
        [Category("Timeout")]
        public int T5 { get; set; }
        [Category("Timeout")]
        public int T6 { get; set; }
        [Category("Timeout")]
        public int T7 { get; set; }

        [Category("Timeout")]
        public int Try3 { get; set; }
        [Category("Timeout")]
        public int Try5 { get; set; }
        [Category("Timeout")]
        public int Try6 { get; set; }
        [Category("Timeout")]
        public int Try7 { get; set; }

        [Category("Log")]
        public string LogFileName { get; set; }
        [Category("Log")]
        public int LogFileSize { get; set; }
        [Category("Log")]
        public string LogPath { get; set; }

        public bool AutoMakeSystemByte { get; set; }
    
        public bool NoSendSelectReqPassiveMode { get; set; }

        public string HSMSEncoding { get; set; } = "euc-kr";
        public string LogEncoding { get; set; } = "utf-8";

    }
}
