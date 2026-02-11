using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;

namespace LGCIMServer
{
    public class ServerStatus
    {
        [Category("Sever Status")]
        public string EQModelType { get; set; } = "PXCEPI";

        [Category ("Sever Status")]
        public ushort UnitID { get; set; } = 0102;

        //[Category ("Sever Status")]
        //public byte StageID { get; set; } = 1;

        [Category("Sever Status")]
        public string GLSSTATUS { get; set; } = "0";

        static ECSData ecs = new ECSData();
        [Category("Glass Data")]
        public GlassInfo AGlass { get; set; } = ecs.AGlass;
        [Category("Glass Data")]
        public GlassInfo BGlass { get; set; } = ecs.BGlass;

        [Browsable(false)]
        public List<SLOTITEM_LOTEND> LOTENDPNLINFOs { get; set; } = new List<SLOTITEM_LOTEND>();

        [Browsable(false)]
        public List<RecipeSet> CIMRecipes { get; set; } = new List<RecipeSet>();

        public bool ReworkFlag { get; set; } = false;
    }
}
