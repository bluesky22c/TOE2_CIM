using Engion;
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TOECIM
{
    public class SystemSetup
    {
        //어떻게 관리하는지 확인 필요
        public List<byte> SubUnitID { get; set; } = new List<byte>();

        [Category("Path"), Description("레시피 저장 경로")]
        public string RecipePath { get; set; } = "";

        [Category("Timeout"), Description("Timeout 발생 대기 시간 (단위 : sec)")]
        public int Timeout { get; set; } = 200;

        [Category("Rework")]
        public bool UseRework { get; set; } = false;

        [Category("Console")]
        public string ConsoleHostAddr { get; set; } = "localhost";

        [Category("Console")]
        public int ConsolePortNo { get; set; } = 3501;

        [Category("CIM")]
        public int CIMPortNo { get; set; } = 3500;

        [Browsable(false)]
        public HSMSConfig Config { get; set; }

        public SystemSetup()
        {
            Check();
        }
        public bool Save(string fileName, LogEx log)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(SystemSetup));
                    xs.Serialize(sw, this);
                }
            }
            catch (Exception ee)
            {
                log.AddLogMessage(LogType.Error, 0, "(SystemSetup Save) " + ee.Message);
                return false;
            }
            return true;
        }

        static public SystemSetup Load(string fileName, LogEx log)
        {
            SystemSetup setup = new SystemSetup();
            Console.WriteLine($"SystemSetup.Load: Attempting to load from {fileName}");

            try
            {
                if (File.Exists(fileName) == false)
                {
                    Console.WriteLine("SystemSetup.Load: File not exists.");
                    log.AddLogMessage(LogType.Error, 0, $"{fileName} File Not Exists. New File Create.");
                    setup.Save(fileName, log);
                    return setup;
                }
                using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
                {
                    Console.WriteLine("SystemSetup.Load: File read success. Preparing XmlSerializer...");
                    try {
                        XmlSerializer xs = new XmlSerializer(typeof(SystemSetup));
                        Console.WriteLine("SystemSetup.Load: XmlSerializer created. Deserializing...");
                        setup = (SystemSetup)xs.Deserialize(sr);
                        Console.WriteLine("SystemSetup.Load: Deserialization success.");
                    } catch (Exception ex) {
                        Console.WriteLine($"[XML SERIALIZER ERROR] {ex.GetType().Name}: {ex.Message}");
                        throw;
                    }
                    setup.Check();
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine($"[SYSTEMSETUP LOAD ERROR] {ee.GetType().Name}: {ee.Message}");
                if (ee.InnerException != null)
                {
                    Console.WriteLine($"[SYSTEMSETUP LOAD INNER ERROR] {ee.InnerException.GetType().Name}: {ee.InnerException.Message}");
                    Console.WriteLine(ee.InnerException.StackTrace);
                }
                log.AddLogMessage(LogType.Error, 0, "(SystemSetup Load) " + ee.Message);
            }
            return setup;
        }

        void Check()
        {
            if (SubUnitID.Count == 0)
            {
                SubUnitID.Add(100);
            }

            if(Config == null)
            {
                Config = new HSMSConfig();
                Config.Mode = HSMSConfig.ModeEnum.ACTIVE;
                Config.IP = "localhost";   // Test Addr
                Config.Port = 8000;
                Config.ShowErrorMessageBox = false;
                Config.Device = 1;
                Config.T3 = 90;
                Config.T5 = 10;
                Config.T6 = 5;
                Config.LinkTestTime = 45;
                Config.LogPath = "d:\\elp\\toe\\cim\\log";
            }
        }

    }
}
