using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    [Serializable]
    public class CIMS7F0 : CimBasicClass
    {
        public CIMS7F0()
        {
            Title = "Abort Transaction";
            Stream = 7 ;
            Function = 0;
            Wait = false;
        }
    }

    [Serializable]
    public class CIMS7F19 : CimBasicClass
    {
        public CIMS7F19()
        {
            Title = "Current EPPD Request";
            Stream = 7;
            Function = 19;
            Wait = true;

            //L1_1 = new ListType(1);
            AutoCreateInstance();

        }
        //public ListType L1_1 { get; set; }
        public UNITID UNITID { get; set; }
    }
    [Serializable]
    public class CIMS7F20 : CimBasicClass
    {
        public CIMS7F20()
        {
            Title = "Current EPPD Request Acknowledge";
            Stream = 7;
            Function = 20;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();

        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public List<RECIPEID> RECIPEIDS { get; set; }
    }

    [Serializable]
    public class CIMS7F61 : CimBasicClass
    {
        public CIMS7F61()
        {
            Title = "Recipe ID Confirm Request";
            Stream = 7;
            Function = 61;
            Wait = true;

            L1_14 = new ListType(14);
            AutoCreateInstance();

        }
        public ListType L1_14 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public PRCID PRCID { get; set; }
        public PORTID PORTID { get; set; }
        public UNITID UNITID { get; set; }
        public GLSTHICK GLSTHICK { get; set; }
        public PARTNUM PARTNUM { get; set; }
        public PRODTYPE PRODTYPE { get; set; }
        public ATTRIBUTE ATTRIBUTE { get; set; }
        public MODECODE MODECODE { get; set; }
        public MULTIFLAG MULTIFLAG { get; set; }
        public List<LSD> LSDS { get; set; }
        public List<SLOTITEM> SLOTITEMS { get; set; }
    }

    [Serializable]
    public class CIMS7F62 : CimBasicClass
    {
        public CIMS7F62()
        {
            Title = "Recipe ID Confirm and Sampling Data Request Acknowledge";
            Stream = 7;
            Function = 62;
            Wait = false;

            L1_7 = new ListType(7);
            AutoCreateInstance();
        }
        public ListType L1_7 { get; set; }
        public UNITID UNITID { get; set; }
        public PORTID PORTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LOTID LOTID { get; set; }
        public SAMPLINGFLAG SAMPLINGFLAG { get; set; }
        public ACKC7M ACKC7M { get; set; }
        public List<SLOTITEM_ACK> SLOTITEMACKS { get; set; }
    }
    [Serializable]
    public class CIMS7F63 : CimBasicClass
    {
        public CIMS7F63()
        {
            Title = "Sampling Data Report";
            Stream = 7;
            Function = 63;
            Wait = true;

            L1_5 = new ListType(5);
            AutoCreateInstance();
        }
        public ListType L1_5 { get; set; }
        public UNITID UNITID { get; set; }
        public PORTID PORTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LOTID LOTID { get; set; }
        public List<SLOTITEM_SELECT> SLOTITEMSELECT { get; set; }
    }
    [Serializable]
    public class CIMS7F64 : CimBasicClass
    {
        public CIMS7F64()
        {
            Title = "Sampling Data Report Acknowledge";
            Stream = 7;
            Function = 64;
            Wait = false;

            L1_4 = new ListType(4);
            AutoCreateInstance();
        }
        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public PORTID PORTID { get; set; }
        public LOTID LOTID { get; set; }
        public ACKC7 ACKC7 { get; set; }
    }
    [Serializable]
    public class CIMS7F67 : CimBasicClass
    {
        public CIMS7F67()
        {
            Title = "Current Recipe Group List Request";
            Stream = 7;
            Function = 67;
            Wait = true;

            AutoCreateInstance();
        }
        public UNITID UNITID { get; set; }
    }
    [Serializable]
    public class CIMS7F68 : CimBasicClass
    {
        public CIMS7F68()
        {
            Title = "Current Recipe Group List";
            Stream = 7;
            Function = 68;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public List<GROUPID> GROUPID { get; set; }
    }

    [Serializable]
    public class CIMS7F69 : CimBasicClass
    {
        public CIMS7F69()
        {
            Title = "Current Recipe ID List Request by Group";
            Stream = 7;
            Function = 69;
            Wait = true;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public List<GROUPID> GROUPIDS { get; set; }
        
    }
    [Serializable]
    public class CIMS7F70 : CimBasicClass
    {
        public CIMS7F70()
        {
            Title = "Current Recipe ID List Request by Group Acknowledge";
            Stream = 7;
            Function = 70;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public List<RECIPE_GROUP> RECIPE_GROUPS { get; set; }
    }

    [Serializable]
    public class CIMS7F77 : CimBasicClass
    {
        public CIMS7F77()
        {
            Title = "Recipe Body Request";
            Stream = 7;
            Function = 77;
            Wait = true;

            L1_6 = new ListType(6);
            AutoCreateInstance();
        }
        public ListType L1_6 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public PRCID PRCID { get; set; }
        public PORTID PORTID { get; set; }
        public UNITID UNITID { get; set; }
    }
    [Serializable]
    public class CIMS7F78 : CimBasicClass
    {
        public CIMS7F78()
        {
            Title = "Recipe Body";
            Stream = 7;
            Function = 78;
            Wait = false;

            L1_8 = new ListType(8);
            AutoCreateInstance();
        }
        public ListType L1_8 { get; set; }
        public LOTID LOTID { get; set; }
        public CSTID CSTID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public PRCID PRCID { get; set; }
        public PORTID PORTID { get; set; }
        public UNITID UNITID { get; set; }
        public List<UNITITEM> UNITITEMS { get; set; }
    }
}
