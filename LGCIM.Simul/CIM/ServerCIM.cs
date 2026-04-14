using LGHSMSBase;
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LGCIMServer
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class ServerCIM
    {
        public LGGem gem = new LGGem();
        public ServerStatus serverStatus = new ServerStatus();

        private static readonly string ConfigFilePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Setup", "ecsConfig.xml");

        public void InitGem()
        {
            var config = LoadConfig();

            gem.Config = config;
            gem.ShowConfig(config);
            SaveConfig(config);

            gem.Init();

            InitS1();
            InitS2();
            InitS5();
            InitS6();
            InitS7();
            InitS10();
            InitS15();

            gem.OnSelectEvent += Gem_OnSelectEvent;
        }

        private void Gem_OnSelectEvent(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private static HSMSConfig LoadConfig()
        {
            try
            {
                if (File.Exists(ConfigFilePath))
                {
                    using var sr = new StreamReader(ConfigFilePath, Encoding.Default);
                    var xs = new XmlSerializer(typeof(HSMSConfig));
                    return (HSMSConfig)xs.Deserialize(sr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ECS Config Load Error] {ex.Message}");
            }

            // 기본값
            return new HSMSConfig
            {
                Mode = HSMSConfig.ModeEnum.PASSIVE,
                IP = "127.0.0.1",
                Port = 8000,
                ShowErrorMessageBox = false,
                Device = 1,
                T3 = 90,
                T5 = 10,
                T6 = 5,
                LinkTestTime = 45,
                LogFileName = "ECS",
                LogPath = @"D:\LGCIM"
            };
        }

        private static void SaveConfig(HSMSConfig config)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(ConfigFilePath));
                using var sw = new StreamWriter(ConfigFilePath, false, Encoding.Default);
                var xs = new XmlSerializer(typeof(HSMSConfig));
                xs.Serialize(sw, config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ECS Config Save Error] {ex.Message}");
            }
        }

    }
}
