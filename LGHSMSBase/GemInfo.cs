using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    public class EQPReport : CimBasicClass
    {
        public EQPReport()
        {
            L1_2 = new ListType(2);
            L2_1 = new ListType(1);
            L3_4 = new ListType(4);

            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public ListType L2_1 { get; set; }
        public ListType L3_4 { get; set; }
        public UNITID UNITID { get; set; }
        public EQPSTATUS EQPStatus { get; set; }
        public ALID ALID { get; set; }
        public ALTX ALTX { get; set; }

        //public ListType L4_0 { get; set; } = new ListType(0);
        public List<IONIZERRPT> IONIZERRPTS { get; set; } = new List<IONIZERRPT>();
    }
    [Serializable]
    public class IONIZERRPT : CimBasicClass
    {
        public IONIZERRPT()
        {
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; } = new ListType(2);
        public IONIZERID IONIZERID { get; set; }
        public IONIZERSTATUS IONIZERSTATUS { get; set; }
    }

    public class SUBUNIT : CimBasicClass
    {
        public SUBUNIT()
        {
            L1_3 = new ListType(3);
            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public UNITID UNITID { get; set; }
        public SUBUNITID SUBUNITID { get; set; }
        public List<GLSCODE> GLSCODES { get; set; } = new List<GLSCODE>();
    }

    public class LoadGlass : CimBasicClass
    {
        public LoadGlass()
        {
            L1_21 = new ListType(21);

            AutoCreateInstance();
        }

        public ListType L1_21 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public SLOTID SLOTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public PRCID PRCID { get; set; }
        public GLSODR GLSODR { get; set; }
        public GLSID GLSID { get; set; }
        public GLSJUDGE GLSJUDGE { get; set; }
        public PNLIF PNLIF { get; set; }
        public SUBMDLIF SUBMDLIF { get; set; }
        public PORTID PORTID { get; set; }
        public GLSCODE GLSCODE { get; set; }
        public REINPUT REINPUT { get; set; }
        public GLSTHICK GLSTHICK { get; set; }
        public PARTNUM PARTNUM { get; set; }
        public PRODTYPE PRODTYPE { get; set; }
        public ATTRIBUTE ATTRIBUTE { get; set; }
        public GLSTYPE GLSTYPE { get; set; }
        public KEYID KEYID { get; set; }
        public List<LSD> LSDS { get; set; }
        public List<GSD> GSDS { get; set; }
    }

    public class UnLoadGlass : CimBasicClass
    {
        public UnLoadGlass()
        {
            L1_21 = new ListType(21);

            AutoCreateInstance();
        }

        public ListType L1_21 { get; set; }
        public GLSODR GLSODR { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public SLOTID SLOTID { get; set; }
        public GLSID GLSID { get; set; }
        public PNLIF PNLIF { get; set; }
        public SUBMDLIF SUBMDLIF { get; set; }
        public PRCID PRCID { get; set; }
        public DICODE DICODE { get; set; }
        public PORTID PORTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public GLSJUDGE GLSJUDGE { get; set; }
        public PARTNUM PARTNUM { get; set; }
        public PRODTYPE PRODTYPE { get; set; }
        public ATTRIBUTE ATTRIBUTE { get; set; }
        public GLSTYPE GLSTYPE { get; set; }
        public GLSPROC GLSPROC { get; set; }
        public GLSCODE GLSCODE { get; set; }
        public DEST DEST { get; set; }
        public KEYID KEYID { get; set; }
        public List<GSD> GSDS { get; set; }
    }


    public class LSD : CimBasicClass
    {
        public LSD()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public LSDITEM LSDITEM { get; set; }
        public LSDVALUE LSDVALUE { get; set; }
    }

    public class GSD : CimBasicClass
    {
        public GSD()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public GSDITEM GSDITEM { get; set; }
        public GSDVALUE GSDVALUE { get; set; }
    }
    public class ESD : CimBasicClass
    {
        public ESD()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public ESDITEM ESDITEM { get; set; }
        public ESDVALUE ESDVALUE { get; set; }
    }

    public class SLOTITEM : CimBasicClass
    {
        public SLOTITEM()
        {
            L1_10 = new ListType(10);

            AutoCreateInstance();
        }

        public ListType L1_10 { get; set; }
        public SLOTID SLOTID { get; set; }
        public SLTLOTID SLTLOTID { get; set; }
        public GLSID GLSID { get; set; }
        public SRECIPEID SRECIPEID { get; set; }
        public GLSEXIST GLSEXIST { get; set; }
        public GLSJUDGE GLSJUDGE { get; set; }
        public PARTNUM PARTNUM { get; set; }
        public PNLIF PNLIF { get; set; } 
        public SUBMDLIF SUBMDLIF { get; set; }
        public List<GSD> GSDS { get; set; }
    }
    public class SLOTITEM_ACK : CimBasicClass
    {
        public SLOTITEM_ACK()
        {
            L1_3 = new ListType(3);

            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public SLOTID SLOTID { get; set; }
        public SRECIPEID SRECIPEID { get; set; }
        public ACKC7S ACKC7S { get; set; }
    }
    public class SLOTITEM_SELECT : CimBasicClass
    {
        public SLOTITEM_SELECT()
        {
            L1_4 = new ListType(4);

            AutoCreateInstance();
        }

        public ListType L1_4 { get; set; }
        public SLOTID SLOTID { get; set; }
        public SRECIPEID SRECIPEID { get; set; }
        public ACKC7S ACKC7S { get; set; }
        public SELECTED SELECTED { get; set; }
    }
    public class SLOTITEM_LOTEND : CimBasicClass
    {
        public SLOTITEM_LOTEND()
        {
            L1_10 = new ListType(10);

            AutoCreateInstance();
        }

        public ListType L1_10 { get; set; }
        public GLSODR GLSODR { get; set; }
        public SLOTID SLOTID { get; set; }
        public GLSID GLSID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public GLSEXIST GLSEXIST { get; set; }
        public GLSSELECTED GLSSELECTED { get; set; }
        public PROCUNITID PROCUNITID { get; set; }
        public GLSJUDGE GLSJUDGE { get; set; }
        public PNLIF PNLIF { get; set; }
        public SUBMDLIF SUBMDLIF { get; set; }
    }

    public class SLOTITEM_SAMPLE : CimBasicClass
    {
        public SLOTITEM_SAMPLE()
        {
            L1_4 = new ListType(4);

            AutoCreateInstance();
        }
        
        public ListType L1_4 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public PORTID PORTID { get; set; }
        public SLOTID SLOTID { get; set; }
    }

    public class UNITITEM : CimBasicClass
    {
        public UNITITEM()
        {
            L1_6 = new ListType(6);

            AutoCreateInstance();
        }
        public ListType L1_6 { get; set; }
        public EUNITID EUNITID { get; set;}
        public LOWERRECIPEID LOWERRECIPEID { get; set; }
        public LOWERRECIPEIDVER LOWERRECIPEIDVER { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<STEPITEM> STEPITEMS { get; set; }
    }
    public class STEPITEM : CimBasicClass
    {
        public STEPITEM()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public STEP STEP { get; set; }
        public List<RPARMITEM> RPARMITEMS { get; set; }
    }

    public class RPARMITEM : CimBasicClass
    {
        public RPARMITEM()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public RPARMNAME RPARAMNAME { get; set; }
        public RPARMVAL RPARMVAL { get; set; }
    }

    public class GLSPRCItem : CimBasicClass
    {
        public GLSPRCItem()
        {
            L1_7 = new ListType(7);

            AutoCreateInstance();
        }
        public ListType L1_7 { get; set; }
        public GLSID GLSID { get; set; }
        public GLSJUDGE GLSJUDGE { get; set; }
        public PNLIF PNLIF { get; set; }
        public SUBMDLIF SUBMDLIF { get; set; }
        public GLSCODE GLSCODE { get; set; }
        public KEYID KEYID { get; set; }
        public List<GSD> GSDS { get; set; }
    }

    public class DVItem : CimBasicClass
    {
        public DVItem()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public DVNAME DVNAME { get; set; }
        public DVVAL DVVAL { get; set; }
    }

    public class CassetteMAP : CimBasicClass
    {
        public CassetteMAP()
        {
            L1_5 = new ListType(5);

            AutoCreateInstance();
        }

        public ListType L1_5 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public PORTID PORTID { get; set; }
        public MAP MAP { get; set; }
        public CSTREADY CSTREADY { get; set; }
    }
    public class RECIPEItem : CimBasicClass
    {
        public RECIPEItem()
        {
            L1_3 = new ListType(3);

            AutoCreateInstance();
        }

        public ListType L1_3 { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<RECIPEIDVER> RECIPEIDVERS { get; set; }
    }

    public class RECIPE_EQ : CimBasicClass
    {
        public RECIPE_EQ()
        {
            L1_5 = new ListType(5);

            AutoCreateInstance();
        }

        public ListType L1_5 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<RECIPEVER> RECIPEVERS { get; set; }
    }

    public class RECIPE_LOWER_REQ : CimBasicClass
    {
        public RECIPE_LOWER_REQ()
        {
            L1_5 = new ListType(5);

            AutoCreateInstance();
        }

        public ListType L1_5 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
    }
    public class RECIPE_LOWER_RES : CimBasicClass
    {
        public RECIPE_LOWER_RES()
        {
            L1_5 = new ListType(5);

            AutoCreateInstance();
        }

        public ListType L1_5 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<RECIPEIDVER> RECIPEIDVERS { get; set; }
    }

    public class RECIPE_STEP : CimBasicClass
    {
        public RECIPE_STEP()
        {
            L1_6 = new ListType(6);

            AutoCreateInstance();
        }

        public ListType L1_6 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public RECIPEIDVER RECIPEIDVERS { get; set; }
        public List<RPARMITEM_STEP> RECIPEVER_STEPS { get; set; }
    }

    public class RECIPEVER : CimBasicClass
    {
        public RECIPEVER()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public RECIPEIDSTATUS RECIPEIDSTATUS { get; set; }
    }
    public class RPARMITEM_STEP : CimBasicClass
    {
        public RPARMITEM_STEP()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public STEP STEP { get; set; }
        public List<RPARMITEM> RPARMITEMS { get; set; }
    }
    public class LOT_PR : CimBasicClass
    {
        public LOT_PR()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public LOTPRITEM LOTPRITEM { get; set; }
        public LOTPRDATA LOTPRDATA { get; set; }
    }
    public class LOT_DV : CimBasicClass
    {
        public LOT_DV()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public LOTDVNAME LOTDVNAME { get; set; }
        public LOTDVVAL LOTDVVAL { get; set; }
    }
    public class GLS_PR : CimBasicClass
    {
        public GLS_PR()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public GLSPRITEM GLSPRITEM { get; set; }
        public GLSPRDATA LOTPRDAGLSPRDATATA { get; set; }
    }
    public class GLS_DV : CimBasicClass
    {
        public GLS_DV()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public GLSDVNAME GLSDVNAME { get; set; }
        public GLSDVVAL GLSDVVAL { get; set; }
    }
    public class GLS_EQOP : CimBasicClass
    {
        public GLS_EQOP()
        {
            L1_5 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_5 { get; set; }
        public GLSID GLSID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public EQPINFO EQPINFO { get; set; }
        public List<GLS_PR> GLSPRS { get; set; }
        public List<GLS_DV> GLSDVS { get; set; }
    }
    public class UNIT_EQOP : CimBasicClass
    {
        public UNIT_EQOP()
        {
            L1_2 = new ListType(2);

            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public DACK DACK { get; set; }
    }

    public class REWORK_ITEM : CimBasicClass
    {
        public REWORK_ITEM()
        {
            L1_2 = new ListType(2);
            AutoCreateInstance();
        }

        public ListType L1_2 { get; set; }
        public SLOTID SLOTID { get; set; }
        public SELECTED SELECTED { get; set; }
    }

    public class RECIPE_GROUP : CimBasicClass
    {
        public RECIPE_GROUP()
        {
            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public GROUPID GROUPID { get; set; }
        public List<RECIPEID> RECIPEIDS { get; set; }
    }
}
