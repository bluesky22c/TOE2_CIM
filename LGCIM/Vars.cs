using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LGCIM.Lib;

namespace LGCIM
{
    static public class Vars
    {
        static public GLSMVSTATUS.GLSMVSTATUSEnum curGLSM = GLSMVSTATUS.GLSMVSTATUSEnum.CR;
        static public ClientCIM cim;
        public static VersionManager VerManager { get; set; }

        static Vars()
        {
            Console.WriteLine("Static Constructor: Vars is initializing...");
            try
            {
                Console.WriteLine("Initializing Vars.VerManager...");
                VerManager = new VersionManager();
                Console.WriteLine("Static Constructor: Vars initialization complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[VARS STATIC ERROR] {ex.GetType().Name}: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"[VARS STATIC INNER ERROR] {ex.InnerException.GetType().Name}: {ex.InnerException.Message}");
                }
                throw; 
            }
        }

        public static string VersionJsonPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "VersionHistory.json");
        static public EQPSTATUS.EQPSTATUSEnum CIMEQPStatsu(EQPStatus machineStatus)
        {
            switch (machineStatus)
            {
                case EQPStatus.RUN:
                    return EQPSTATUS.EQPSTATUSEnum.RUN;

                case EQPStatus.IDLE:
                    return EQPSTATUS.EQPSTATUSEnum.IDLE;

                case EQPStatus.DOWN:
                    return EQPSTATUS.EQPSTATUSEnum.DOWN;

                default:
                    // 알람 등 기타 상태는 IDLE 또는 DOWN으로 처리할지 결정 필요. 일단 IDLE로 매핑하거나 유지.
                    return EQPSTATUS.EQPSTATUSEnum.IDLE;
            }

        }
    }
}
