using Elp.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using LGCIM.Lib;
//using TOELib;
using Newtonsoft.Json;

namespace LGCIM
{
    static public class Common
    {
        /// <summary>
        /// Deep Copy (JSON based for .NET 8 compatibility)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="a"></param>
        /// <returns></returns>
        static public T Clone<T>(T a)
        {
            if (ReferenceEquals(a, null)) return default;
            var json = JsonConvert.SerializeObject(a);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// CIM에서 받은 glass 데이터를 밖에서 공통으로 사용하는 CommonGlassInfo로 변환
        /// </summary>
        /// <param name="cimGlass"></param>
        /// <returns></returns>
        public static CommonGlassInfo ConvertCIMtoCommonGlassData(GlassInfo cimGlass)
        {
            var common = new CommonGlassInfo();
            if (cimGlass == null)
            {
                return common;
            }

            common.LotID = cimGlass.LOTID;
            common.CassetteID = cimGlass.CSTID;
            common.SlotNo = cimGlass.SLOTID;
            common.PPID = cimGlass.RECIPEID;
            common.ProcessID = cimGlass.PRCID;
            common.GlassODR = cimGlass.GLSODR;
            common.GlassID = cimGlass.GLSID;
            common.JudgeCode = cimGlass.GLSJUDGE;
            common.PanelInfo = cimGlass.PNLIF;
            common.SubMDLInfo = cimGlass.SUBMDLIF;
            common.PortID = cimGlass.PORTID;
            common.GlassCode = (byte)cimGlass.GLSCODE;
            common.ReInput = cimGlass.REINPUT;
            common.GlassThick = cimGlass.GLSTHICK;
            common.PartNum = cimGlass.PARTNUM;
            common.ProdType = cimGlass.PRODTYPE;
            common.Attribute = cimGlass.ATTRIBUTE;
            common.GlassType = cimGlass.GLSTYPE;
            common.KeyID = cimGlass.KEYID;
            common.Dicode = cimGlass.DICODE;
            common.GlassProcess = cimGlass.GLSPROC;
            common.Dest = cimGlass.DEST;
            common.LastGlass = cimGlass.GLSODR == "2" || cimGlass.GLSODR == "3" ? true : false;

            //foreach (var l in cimGlass.LSDS)
            //{
            //    common.LSDS = new List<TOELib.LSD>();
            //    common.LSDS.Add(new TOELib.LSD { LSDITEM = l.LSDITEM, LSDVALUE = l.LSDVALUE });
            //}

            //foreach (var g in cimGlass.GSDS)
            //{
            //    common.GSDS = new List<TOELib.GSD>();
            //    common.GSDS.Add(new TOELib.GSD { GSDITEM = g.GSDITEM, GSDVALUE = g.GSDVALUE });
            //}
            return common;
        }

        /// <summary>
        /// 밖에서 공통으로 사용하는 CommonGlassInfo를 CIM에서 사용하는 glass 데이터로 변환
        /// </summary>
        /// <param name="common"></param>
        /// <returns></returns>
        public static GlassInfo ConvertCommontoCIMGlassData(CommonGlassInfo common)
        {
            var cimGlass = new GlassInfo();
            if(common == null)
            {
                return cimGlass;
            }

            cimGlass.LOTID = common.LotID;
            cimGlass.CSTID = common.CassetteID;
            cimGlass.SLOTID = common.SlotNo;
            cimGlass.RECIPEID = common.PPID;
            cimGlass.PRCID = common.ProcessID;
            cimGlass.GLSODR = common.GlassODR;
            cimGlass.GLSID = common.GlassID;
            cimGlass.GLSJUDGE = common.JudgeCode;
            cimGlass.PNLIF = common.PanelInfo;
            cimGlass.SUBMDLIF = common.SubMDLInfo;
            cimGlass.PORTID = common.PortID;
            cimGlass.GLSCODE = common.GlassCode;
            cimGlass.REINPUT = common.ReInput;
            cimGlass.GLSTHICK = common.GlassThick;
            cimGlass.PARTNUM = common.PartNum;
            cimGlass.PRODTYPE = common.ProdType;
            cimGlass.ATTRIBUTE = common.Attribute;
            cimGlass.GLSTYPE = common.GlassType;
            cimGlass.KEYID = common.KeyID;
            cimGlass.DICODE = common.Dicode;
            cimGlass.GLSPROC = common.GlassProcess;
            cimGlass.DEST = common.Dest;

            //foreach (var l in common.LSDS)
            //{
            //    cimGlass.LSDS = new List<LGCIM.Lib.LSD>();
            //    cimGlass.LSDS.Add(new LGCIM.Lib.LSD { LSDITEM = l.LSDITEM, LSDVALUE = l.LSDVALUE });
            //}

            //foreach (var g in cimGlass.GSDS)
            //{
            //    cimGlass.GSDS = new List<LGCIM.Lib.GSD>();
            //    cimGlass.GSDS.Add(new LGCIM.Lib.GSD { GSDITEM = g.GSDITEM, GSDVALUE = g.GSDVALUE });
            //}

            return cimGlass;
        }

        public static async Task<EQPtoCIMData> ToConsoleAsync(CIMtoEQPData toConsole, bool wait = true, int timeout = 5000, int retries = 3)
        {
            return await Task.Run(() => ToConsole(toConsole, wait, timeout, retries));
        }

        public static EQPtoCIMData ToConsole(CIMtoEQPData toConsole, bool wait = true, int timeout = 5000, int retries = 3)
        {
            int maxRetries = retries;
            for (int i = 0; i < maxRetries; i++)
            {
                try
                {
                    var commData = new CimCommData { ToConsole = toConsole };
                    commData.WaitResponse = wait;
                    commData.Timeout = timeout;
                    var host = Program.SystemSetup.ConsoleHostAddr;
                    var port = Program.SystemSetup.ConsolePortNo;
                    // File.AppendAllText("client_debug.txt", $"{DateTime.Now}: Attempting to connect to {host}:{port}\n");
                    var client = new JsonClient<CimCommData>(null, host, port, null);

                    var task = Task.Run(() => client.Send(commData));
                    if (task.Wait(timeout))
                    {
                        var rVal = task.Result;
                        if (rVal == null)
                        {
                            throw new Exception("Response is null (Connection failed or invalid response)");
                        }
                        return rVal.ToCIM;
                    }
                    else
                    {
                        throw new TimeoutException("ToConsole Timeout");
                    }
                }
                catch (Exception ex)
                {
                    if (i == maxRetries - 1)
                    {
                        Console.WriteLine($"ToConsole Failed after {maxRetries} retries: {ex.Message}");
                        return new EQPtoCIMData { Error = true, eqpStatus = LGCIM.Lib.EQPStatus.DOWN };
                    }
                    Thread.Sleep(100);
                }
            }
            return new EQPtoCIMData { Error = true, eqpStatus = LGCIM.Lib.EQPStatus.DOWN };
        }


    }
}
