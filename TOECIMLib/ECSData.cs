using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOECIMLib
{
    [Serializable]
    public enum ReportType 
    {
        Load,
        Unload,
    }

    [Serializable]
    public class ECSData
    {
        public ECSData()
        {
            AGlass = new GlassInfo();
            BGlass = new GlassInfo();
        }

        public GlassInfo AGlass { get; set; }
        public GlassInfo BGlass { get; set; }
    }

    [Serializable]
    public class GlassInfo
    {
        public GlassInfo()
        {

        }


        public void SetGSD(string name, string value)
        {
            var gsd = GSDS.Find(g => g.GSDITEM.Contains(name));
            if (gsd == null)
            {
                // log
                return;
            }
            gsd.GSDVALUE = value;
        }
        public void ClearInfo()
        {
            ReportType = ReportType.Load;
            //LOTID = "";
            //CSTID = "";
            SLOTID = "";
            RECIPEID = "";
            PRCID = "";
            GLSODR = "";
            GLSID = "";
            GLSJUDGE = "";
            PNLIF = "";
            SUBMDLIF = "";
            PORTID = 0;
            GLSCODE = 0;
            REINPUT = 0;
            GLSTHICK = "";
            PARTNUM = "";
            PRODTYPE = "";
            ATTRIBUTE = "";
            GLSTYPE = "";
            KEYID = "";
            DICODE = 0;
            GLSPROC = "";
            DEST = 0;

            LSDS = new List<LSD>();
            GSDS = new List<GSD>();
        }
        public void InitGlassInfo()
        {
            ReportType = ReportType.Load;
            LOTID = "CETL5A001200";
            CSTID = "PXCCST100041";
            SLOTID = "25";
            RECIPEID = "123QDLS3";
            PRCID = "77490";
            GLSODR = "1";
            GLSID = "6TL5900063A00";
            GLSJUDGE = "G";
            PNLIF = "CGGGGGGGGGGGGGGGGG";
            SUBMDLIF = "";
            PORTID = 2;
            GLSCODE = 16153;
            REINPUT = 0;
            GLSTHICK = "50";
            PARTNUM = "62084-0129A";
            PRODTYPE = "PRODUCTION";
            ATTRIBUTE = "CELL_GAP_G";
            GLSTYPE = "N";
            KEYID = "CETL5A001200_20251106152106";
            DICODE = 0;
            GLSPROC = "0";
            DEST = 0;

            LSDS = new List<LSD>
            {
                new LSD { LSDITEM = "FROM_CST_QTY", LSDVALUE = "54" },
                new LSD { LSDITEM = "LOT_JUDGE",    LSDVALUE = "G"  },
                new LSD { LSDITEM = "LOT_TYPE",     LSDVALUE = "G"  },
                new LSD { LSDITEM = "MODEL_ATTR",   LSDVALUE = "OLED" }
            };
            GSDS = new List<GSD>();
        }
        public ReportType ReportType { get; set; }

        public bool GlassInfoExist => GLSCODE != 0;

        public string LOTID { get; set; } = "";
        public string CSTID { get; set; } = "";
        public string SLOTID { get; set; } = "";
        public string RECIPEID { get; set; } = "";
        public string PRCID { get; set; } = "";

        /// <summary>
        /// 0 : The first glass
        /// 1 : All glasses except the first glass and the last glass
        /// 2 : The Last glass
        /// 3 = the only glass, when there is just one in a LOT.
        /// </summary>
        public string GLSODR { get; set; } = "";
        public string GLSID { get; set; } = "";
        public string GLSJUDGE { get; set; } = "";
        public string PNLIF { get; set; } = "";
        public string SUBMDLIF { get; set; } = "";
        public byte PORTID { get; set; }
        public ushort GLSCODE { get; set; }

        public LoadGlass ToLoadGlass()
        {
            var result = new LoadGlass();
            result.LOTID = LOTID;
            result.CSTID = CSTID;
            result.SLOTID = SLOTID;
            result.RECIPEID = RECIPEID;
            result.PRCID = PRCID;
            result.GLSODR = GLSODR;
            result.GLSID = GLSID;
            result.GLSJUDGE = GLSJUDGE;
            result.PNLIF = PNLIF;
            result.SUBMDLIF = SUBMDLIF;
            result.PORTID = PORTID;
            result.GLSCODE = GLSCODE;
            result.REINPUT = REINPUT;
            result.GLSTHICK = GLSTHICK;
            result.PARTNUM = PARTNUM;
            result.PRODTYPE = PRODTYPE;
            result.ATTRIBUTE = ATTRIBUTE;
            result.GLSTYPE = GLSTYPE;
            result.KEYID = KEYID;

            foreach(var l in LSDS)
                result.LSDS.Add(new LGHSMSBase.LSD { LSDITEM = l.LSDITEM, LSDVALUE = l.LSDVALUE });

            foreach (var g in GSDS)
                result.GSDS.Add(new LGHSMSBase.GSD { GSDITEM = g.GSDITEM, GSDVALUE = g.GSDVALUE });

            return result;
        }

        /// <summary>
        /// 0 : Not Re-input
        /// 1 : Re-input
        /// </summary>
        public byte REINPUT { get; set; }
        public string GLSTHICK { get; set; } = "";
        public string PARTNUM { get; set; } = "";
        public string PRODTYPE { get; set; } = "";
        public string ATTRIBUTE { get; set; } = "";

        /// <summary>
        /// N : Normal
        /// D : Dummy
        /// </summary>
        public string GLSTYPE { get; set; } = "";
        public string KEYID { get; set; } = "";

        /// <summary>
        /// Different Code -> Unload 데이터에만 존재
        /// 0 : All GLS IDs are completely matching with GLS IDs from ECS.
        /// 1 : Any GLS ID is not matching with GLS ID from ECS
        /// 2 : When each ID of glass is not checked by VCR or OCR in the equipment has VCR or OCR
        /// </summary>
        public byte DICODE { get; set; }

        /// <summary>
        /// <GLSPROC> Glass Process Status -> Unload 데이터에만 존재
        /// 0 : Unprocessed
        /// 1 : Processing
        /// 2 : Processed
        /// 3 : Rejected
        /// </summary>
        public string GLSPROC { get; set; } = "";

        /// <summary>
        /// Glass Next Destination -> Unload 데이터에만 존재
        /// 0 = ECS determine destination of glass.
        /// </summary>
        public ushort DEST { get; set; }
        public List<LSD> LSDS { get; set; } = new List<LSD>();
        public List<GSD> GSDS { get; set; } = new List<GSD>();
    }

    [Serializable]
    /// <summary>
    /// Lot Specific Data
    /// </summary>
    public class LSD
    {
        public LSD()
        {
            LSDITEM = "LSDITEM";
            LSDVALUE = "LSDVALUE";
        }

        public string LSDITEM { get; set; }
        public string LSDVALUE { get; set; }
    }

    [Serializable]
    /// <summary>
    /// Glass Specific Data
    /// </summary>
    public class GSD
    {
        public GSD()
        {
            GSDITEM = "GSDITEM";
            GSDVALUE = "GSDVALUE";
        }

        public string GSDITEM { get; set; }
        public string GSDVALUE { get; set; }
    }

    [Serializable]
    public class LotInfo
    {
        public LotInfo()
        {
            Glasses = new List<ECSData>();
        }

        public LOTID LOTID { get; set; } = null!;
        public CSTID CSTID { get; set; } = null!;
        public PORTID PORTID { get; set; } = null!;
        public LOTJUDGE LOTJUDGE { get; set; } = null!;
        public List<ECSData> Glasses { get; set; }
    }
}
