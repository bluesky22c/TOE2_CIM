using MMISoft.HSMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LGHSMSBase
{
    [Serializable]
    public class CIMS15F0 : CimBasicClass
    {
        public CIMS15F0()
        {
            Title = "Abort Transaction";
            Stream = 15;
            Function = 0;

            Wait = false;
        }
    }

    [Serializable]
    public class CIMS15F65 : CimBasicClass
    {
        public CIMS15F65()
        {
            Title = "Current Recipe ID List Request";
            Stream = 15;
            Function = 65;
            Wait = true;

            AutoCreateInstance();
        }
        public UNITID UNITID { get; set; }
    }

    [Serializable]
    public class CIMS15F66 : CimBasicClass
    {
        public CIMS15F66()
        {
            Title = "Current Recipe ID List Request Acknowledge";
            Stream = 15;
            Function = 66;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public UNITID UNITID { get; set; }
        public List<RECIPEItem> RECIPEItems { get; set; }
    }

    [Serializable]
    public class CIMS15F71 : CimBasicClass
    {
        public CIMS15F71()
        {
            Title = "Recipe List Report Request";
            Stream = 15;
            Function = 71;
            Wait = true;

            L1_4 = new ListType(4);
            AutoCreateInstance();
        }
        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F72 : CimBasicClass
    {
        public CIMS15F72()
        {
            Title = "Equipment Recipe List Report";
            Stream = 15;
            Function = 72;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public List<RECIPE_EQ> RECIPE_EQS { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F73 : CimBasicClass
    {
        public CIMS15F73()
        {
            Title = "Lower Recipe List Report Request";
            Stream = 15;
            Function = 73;
            Wait = true;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public RECIPE_LOWER_REQ RECIPE_LOWER_REQ { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F74 : CimBasicClass
    {
        public CIMS15F74()
        {
            Title = "Lower Recipe List Report";
            Stream = 15;
            Function = 74;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public List<RECIPE_LOWER_RES> RECIPE_LOWER_RESS { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F75 : CimBasicClass
    {
        public CIMS15F75()
        {
            Title = "Recipe Parameter Report Request";
            Stream = 15;
            Function = 75;
            Wait = true;

            L1_6 = new ListType(6);
            AutoCreateInstance();
        }
        public ListType L1_6 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public RECIPEIDVER RECIPEVER { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F76 : CimBasicClass
    {
        public CIMS15F76()
        {
            Title = "Recipe Parameter Report";
            Stream = 15;
            Function = 76;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public RECIPE_STEP RECIPE_STEP { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F77 : CimBasicClass
    {
        public CIMS15F77()
        {
            Title = "Recipe Parameter Change Send";
            Stream = 15;
            Function = 77;
            Wait = true;

            L1_8 = new ListType(8);
            AutoCreateInstance();
        }
        public ListType L1_8 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEIDCMD RECIPEIDCMD { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<RPARMITEM_STEP> RPARMITEM_STEPS { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F78 : CimBasicClass
    {
        public CIMS15F78()
        {
            Title = "Recipe Change Command Acknowledge";
            Stream = 15;
            Function = 78;
            Wait = false;

            L1_6 = new ListType(6);
            AutoCreateInstance();
        }
        public ListType L1_6 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public REACK REACK { get; set; }
    }

    [Serializable]
    public class CIMS15F79 : CimBasicClass
    {
        public CIMS15F79()
        {
            Title = "Recipe Parameter Change Report";
            Stream = 15;
            Function = 79;
            Wait = true;

            L1_8 = new ListType(8);
            AutoCreateInstance();
        }
        public ListType L1_8 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEIDCMD RECIPEIDCMD { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<RPARMITEM_STEP> RPARMITEM_STEPS { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F80 : CimBasicClass
    {
        public CIMS15F80()
        {
            Title = "Recipe Change Report Acknowledge";
            Stream = 15;
            Function = 80;
            Wait = false;

            L1_6 = new ListType(6);
            AutoCreateInstance();
        }
        public ListType L1_6 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public PHACK PHACK { get; set; }
    }

    [Serializable]
    public class CIMS15F81 : CimBasicClass
    {
        public CIMS15F81()
        {
            Title = "Equipment Operation Reference Data Send";
            Stream = 15;
            Function = 81;
            Wait = true;

            L1_6 = new ListType(6);
            AutoCreateInstance();
        }
        public ListType L1_6 { get; set; }
        public UNITID UNITID { get; set; }
        public USEMODE USEMODE { get; set; }
        public LOTID LOTID { get; set; }
        public List<LOT_PR> LOTPRS { get; set; }
        public List<LOT_DV> LOTDVS { get; set; }
        public List<GLS_EQOP> GLS_EQOPS { get; set; }
    }

    [Serializable]
    public class CIMS15F82 : CimBasicClass
    {
        public CIMS15F82()
        {
            Title = "Equipment Operation Reference Data Send Acknowledge";
            Stream = 15;
            Function = 82;
            Wait = false;

            L1_1 = new ListType(1);
            AutoCreateInstance();
        }
        public ListType L1_1 { get; set; }
        public List<UNIT_EQOP> UNIT_EQOPS { get; set; } 
    }

    [Serializable]
    public class CIMS15F83 : CimBasicClass
    {
        public CIMS15F83()
        {
            Title = "Equipment Operation Reference Data Apply Report";
            Stream = 15;
            Function = 83;
            Wait = true;

            L1_4 = new ListType(4);
            AutoCreateInstance();
        }
        public ListType L1_4 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LOTID LOTID { get; set; }
        public AACK AACK { get; set; }
    }

    [Serializable]
    public class CIMS15F84 : CimBasicClass
    {
        public CIMS15F84()
        {
            Title = "Operation Reference Data Send Acknowledge";
            Stream = 15;
            Function = 84;
            Wait = false;
        }
    }

    [Serializable]
    public class CIMS15F95 : CimBasicClass
    {
        public CIMS15F95()
        {
            Title = "Equipment Constant Recipe Parameter Request";
            Stream = 15;
            Function = 95;
            Wait = true;

            L1_6 = new ListType(6);
            AutoCreateInstance();
        }
        public ListType L1_6 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public List<ESD> ESDS { get; set; }
    }
    [Serializable]
    public class CIMS15F96 : CimBasicClass
    {
        public CIMS15F96()
        {
            Title = "Equipment Constant Recipe Parameter Send";
            Stream = 15;
            Function = 96;
            Wait = false;

            L1_2 = new ListType(2);
            AutoCreateInstance();
        }
        public ListType L1_2 { get; set; }
        public RECIPE_STEP RECIPEE_STEP { get; set; }
        public List<ESD> ESDS { get; set; }
    }
    [Serializable]
    public class CIMS15F97 : CimBasicClass
    {
        public CIMS15F97()
        {
            Title = "Equipment Constant Recipe Change Command";
            Stream = 15;
            Function = 97;
            Wait = true;

            L1_8 = new ListType(8);
            AutoCreateInstance();
        }
        public ListType L1_8 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEIDCMD RECIPEIDCMD { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<RPARMITEM_STEP> RPARMITEM_STEPS { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F98 : CimBasicClass
    {
        public CIMS15F98()
        {
            Title = "Equipment Constant Recipe Change Command Acknowledge";
            Stream = 15;
            Function = 98;
            Wait = false;

            L1_7 = new ListType(7);
            AutoCreateInstance();
        }
        public ListType L1_7 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public REACK REACK { get; set; }
        public EQPMSG EQPMSG { get; set; }
    }

    [Serializable]
    public class CIMS15F99 : CimBasicClass
    {
        public CIMS15F99()
        {
            Title = "Equipment Constant Recipe Change Report";
            Stream = 15;
            Function = 99;
            Wait = true;

            L1_8 = new ListType(8);
            AutoCreateInstance();
        }
        public ListType L1_8 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEIDCMD RECIPEIDCMD { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEIDVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public List<RPARMITEM_STEP> RPARMITEM_STEPS { get; set; }
        public List<ESD> ESDS { get; set; }
    }

    [Serializable]
    public class CIMS15F100 : CimBasicClass
    {
        public CIMS15F100()
        {
            Title = "Equipment Constant Recipe Change Report Acknowledge";
            Stream = 15;
            Function = 100;
            Wait = false;

            L1_7 = new ListType(7);
            AutoCreateInstance();
        }
        public ListType L1_7 { get; set; }
        public UNITID UNITID { get; set; }
        public RECIPEID RECIPEID { get; set; }
        public LEVEL LEVEL { get; set; }
        public RECIPEIDVER RECIPEVER { get; set; }
        public RECIPEIDTYPE RECIPEIDTYPE { get; set; }
        public PHACK PHACK { get; set; }
        public HOSTMSG HOSTMSG { get; set; }
    }
}
