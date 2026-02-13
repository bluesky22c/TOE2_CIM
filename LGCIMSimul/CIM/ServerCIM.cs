using LGHSMSBase;
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGCIMServer
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public partial class ServerCIM
    {
        public LGGem gem = new LGGem();
        public ServerStatus serverStatus = new ServerStatus();

        public void InitGem()
        {
            HSMSConfig config = new HSMSConfig();
            config.Mode = HSMSConfig.ModeEnum.PASSIVE;
            //socket.Config.IP = "11.92.189.193"; // CIM Addr
            config.IP = "localhost";   // Test Addr
            config.Port = 8000;
            config.ShowErrorMessageBox = false;
            config.Device = 1;
            config.T3 = 90;
            config.T5 = 10;
            config.T6 = 5;

            config.LinkTestTime = 45;
            //config.LogPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Log", "Host");
            config.LogFileName = "HOST";
            config.LogPath = Path.Combine("d:\\ELP\\TOE_CIM");

            gem.ShowConfig(config);

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

    }
}
