using Engion;
using LGHSMSBase;
using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;
//using TOELib;

namespace TOECIM
{
    public partial class ClientCIM
    {

        public ClientCIM()
        {
            Console.WriteLine("ClientCIM: Constructor Starting...");
            gem = new LGGem();
            cimStatus = new CIMStatus();
            timeout = new ETimeout();
            AlarmDBManager = new AlarmManager();
            Console.WriteLine("ClientCIM: Constructor Complete.");
        }

        public LGGem gem { get; set; }

        public CIMStatus cimStatus { get; set; }
        public static LogEx log { get; set; }
        public ETimeout timeout { get; set; }

        public AlarmManager AlarmDBManager { get; set; }

        public void InitGem()
        {
            //------------------SystemSetup으로 이동
            //HSMSConfig config = new HSMSConfig();
            //config.Mode = HSMSConfig.ModeEnum.ACTIVE;
            ////socket.Config.IP = "11.92.189.193"; // CIM Addr
            //config.IP = "localhost";   // Test Addr
            ////config.IP = "192.168.0.26";   // Test Addr
            //config.Port = 8000;
            //config.ShowErrorMessageBox = false;
            //config.Device = 1;
            //config.T3 = 90;
            //config.T5 = 10;
            //config.T6 = 5;

            //config.LinkTestTime = 45;
            //config.LogPath = "c:\\tmp\\client";

            var config = Program.SystemSetup.Config;
            gem.Config = config;

            gem.OnGemEvent += Gem_OnGemEvent;
            gem.OnSelectEvent += Gem_OnSelectEvent;

            gem.Init();

            InitS1();
            InitS2();
            InitS5();
            InitS6();
            InitS7();
            InitS10();
            InitS15();
        }

        private void Gem_OnSelectEvent(object sender, EventArgs e)
        {
            log.AddLogMessage(LogType.Information, 0, $"Selected.");
            SendS1F1();
        }

        private void Gem_OnGemEvent(object sender, GemEventArgs e)
        {
        }

        public void OnlineRemote()
        {
            SendS2F61(RCS.RCSEnum.ONLineRemote);
        }

        public void OnlineLocal()
        {
            SendS2F61(RCS.RCSEnum.OnLineLocal);
        }

        public void Offline()
        {
            SendS2F61(RCS.RCSEnum.OffLine);
        }

        public void ReportOPMode(OPMODE.OPMODEEnum opMode)
        {
            SendS2F63(opMode);
        }

        public void ReportEQPStatus(EQPSTATUS.EQPSTATUSEnum eqpStatus)
        {
            //EQP Status가 Down이 되는 경우에 Console -> Control로 ESC NomalDown되도록 통신 보냄
            if(eqpStatus == EQPSTATUS.EQPSTATUSEnum.RUN || eqpStatus == EQPSTATUS.EQPSTATUSEnum.IDLE)
            {
                //Nomal On
                Common.ToConsole(new CIMtoEQPData() { Command = CIMtoEQP.NomalStatusChange, NomalStatus = true });
            }
            else
            {
                //Nomal Off
                Common.ToConsole(new CIMtoEQPData() { Command = CIMtoEQP.NomalStatusChange, NomalStatus = false });
            }

            SendS6F91(eqpStatus);
            cimStatus.EQPStatus = eqpStatus;
        }

        public void ReportGLSMVStatus(GLSMVSTATUS.GLSMVSTATUSEnum glsMvStatus)
        {
            SendS6F67(glsMvStatus);
            Vars.curGLSM = glsMvStatus;
        }

        void RestartScenario()
        {
            EventHandler<GemEventArgs> sentTimeoutHandler = null;
            EventHandler<GemEventArgs> s1f2Handler = null;
            EventHandler<GemEventArgs> s2f62Handler = null;

            // 등록된 이벤트를 모두 취소함.
            void DoneProc()
            {
                gem.OnS1F2Event -= s1f2Handler;
                gem.OnSentEvent -= sentTimeoutHandler;
                gem.OnS2F62Event -= s2f62Handler;
            }

            s1f2Handler = (_, args) =>
            {
                SendS2F61(cimStatus.RCS);
            };

            s2f62Handler = (_, args) =>
            {
                DoneProc();
                if (cimStatus.IsOnline)
                {
                    SendS2F63(cimStatus.OPMode);    //opmode report
                    SendS2F17();    //date, time report
                    SendS1F61();    //equipment status report
                    SendS1F67();    //all glass position report
                    //if(cimStatus.TryRCS == RCS.RCSEnum.ONLineRemote &&
                    //    cimStatus.CimCommData.ToCIM.Command == EQPtoCIM.LoadReadyDone)
                    //{
                    //    SendS6F61();
                    //}
                }
            };

            // timeout s9f9가 전송되면 등록된 flow를 취소한다.
            sentTimeoutHandler = (_, args) =>
            {
                args.cimClass.Stream = 9;
                args.cimClass.Function = 9;
                DoneProc();
            };

            gem.OnSentEvent += sentTimeoutHandler;
            gem.OnS1F2Event += s1f2Handler;
            gem.OnS2F62Event += s2f62Handler;

        }

        public void AlarmOccur(List<AlarmInfo> alarmList)
        {
            var reportedAlarms = cimStatus.AlarmList;
            foreach (var a in alarmList)
            {
                var find = AlarmInfo.GetError(a.ALID, reportedAlarms);
                if (find != null)
                    continue;
                // 새로운 알람이면 보고
                //cim.AlarmDBManager.SetAlarm(a);
                reportedAlarms.Add(a);
                a.ALCD = ALCD.SetCode(ALCD.ALCDEnum.EQPStatusWarning);
                SendS5F1(a, true);

            }
            cimStatus.Save();

            if (cimStatus.CurAlarm != null && cimStatus.CurAlarm.AlarmType == AlarmType.HeavyAlarm)
                ReportEQPStatus(EQPSTATUS.EQPSTATUSEnum.DOWN);
        }

    }
}
