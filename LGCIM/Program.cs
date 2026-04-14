using Elp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LGCIM
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    static class Program
    {
        static Program()
        {
            Console.WriteLine("Static Constructor: Program is initializing...");
        }

        static public int CIMPort = 5000;
        static public SystemSetup SystemSetup { get; set; }
        static public string BaseDir;
        static public string LogDir;
        static public string systemSetupDir;
        

        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Console.WriteLine("Application Entry Point: Main Starting...");
            try
            {
                Console.WriteLine("Initializing Static Variables...");
                try 
                {
                    // Properties.Settings 접근을 완전히 제거하여 초기화 중 충돌 가능성을 차단합니다.
                    BaseDir = AppDomain.CurrentDomain.BaseDirectory;
                    
                    // 개발 환경이나 특정 환경에서 WorkDir이 다를 경우를 위한 수동 설정 (예시)
                    // 실제 운영 시에는 JSON 설정 파일 등을 사용하는 것이 안전합니다.
                    if (BaseDir.Contains("bin\\Debug")) 
                    {
                        // 디버그 경로일 경우 프로젝트 루트 근처로 설정 시도
                        // BaseDir = @"d:\engion\toe\cim"; 
                    }

                    LogDir = Path.Combine(BaseDir, "Log");
                    systemSetupDir = Path.Combine(BaseDir, "Setup", "systemSetup.xml");
                    
                    Console.WriteLine($"BaseDir (Isolated): {BaseDir}");
                    Console.WriteLine($"LogDir: {LogDir}");
                    Console.WriteLine($"systemSetupDir: {systemSetupDir}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[STATIC INIT ERROR] {ex.GetType().Name}: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                    throw;
                }

                Console.WriteLine("Application Starting...");
                try
                {
                    Console.WriteLine("Initializing Vars.cim...");
                    Vars.cim = new ClientCIM();
                    
                    Console.WriteLine("Initializing Log...");
                    ClientCIM.log = new LogEx(Program.LogDir);
                    Elp.Global.Log = ClientCIM.log;
                    var log = ClientCIM.log;

                    Console.WriteLine("Enabling Visual Styles...");
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);

                    Console.WriteLine("Loading CIMStatus...");
                    Vars.cim.cimStatus = CIMStatus.Load();

                    Console.WriteLine("Creating MainForm...");
                    MainForm mainForm = new MainForm();
                    
                    Console.WriteLine("Running Application...");
                    Application.Run(mainForm);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"[FATAL ERROR] {ex.GetType().Name}: {ex.Message}");
                    Console.WriteLine(ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"[INNER ERROR] {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                        Console.WriteLine(ex.InnerException.StackTrace);
                    }
                }
                finally
                {
                    Console.WriteLine("Finally Block: Attempting to save status...");
                    try {
                        if (Vars.cim?.cimStatus != null)
                            Vars.cim.cimStatus.Save();
                    } catch (Exception ex) {
                        Console.WriteLine($"Error during shutdown save: {ex.Message}");
                    }
                    Console.WriteLine("Application Exiting.");
                }
            }
            catch (Exception topEx)
            {
                Console.WriteLine($"[TOP-LEVEL ERROR] {topEx.GetType().Name}: {topEx.Message}");
                Console.WriteLine(topEx.StackTrace);
            }
        }
    }
}
