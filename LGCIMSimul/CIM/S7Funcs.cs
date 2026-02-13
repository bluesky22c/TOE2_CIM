using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LGCIMServer
{
    public partial class ServerCIM
    {
        void InitS7()
        {
            gem.OnS7F63Event += Gem_OnS7F63Event;
            gem.OnS7F78Event += Gem_OnS7F78Event;
        }

        public void SendS7F19()
        {
            var func = new CIMS7F19();
            func.UNITID = serverStatus.UnitID;
            gem.HSMSSend(func);
        }

        public string MakeS7F19()
        {
            var func = new CIMS7F19();
            func.UNITID = serverStatus.UnitID;
            return func.BodyString();
        }

        public void SendS7F61(int portCount, string recipeID = "RecipeID")
        {
            for (int port = 0; port < portCount; port++)
            {
                var func = new CIMS7F61();

                func.LOTID = "LOTID";
                func.CSTID = "CSTID";
                func.RECIPEID = recipeID;
                func.PRCID = "PRCID";
                func.PORTID = (byte)(port + 1);
                func.UNITID = serverStatus.UnitID;
                func.MULTIFLAG = portCount > 1 ? "T" : "F";

                func.LSDS.Add(new LSD());
                
                int slotCount = 3;
                for (int i = 0; i < slotCount; i++)
                {
                    var slotItem = new SLOTITEM();
                    slotItem.SLOTID = (i+1).ToString("000");
                    slotItem.GLSID = $"GLSID_{i + 1}";
                    slotItem.SRECIPEID = $"{i + 1}";
                    slotItem.GSDS.Add(new GSD());
                    func.SLOTITEMS.Add(slotItem);
                }

                gem.HSMSSend(func);
            }
        }

        private void Gem_OnS7F63Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS7F63 cim = (CIMS7F63)e.cimClass;

            var func = new CIMS7F64();
            func.UNITID = cim.UNITID;
            func.PORTID = cim.PORTID;
            func.LOTID = cim.LOTID;
            func.ACKC7 = (byte)ACKC7.ACKC7enum.Accept;
            gem.HSMSSend(func);

            Thread.Sleep(500);

            var func_77 = new CIMS7F77();
            func_77.LOTID = cim.LOTID;
            func_77.RECIPEID = cim.RECIPEID;
            func_77.PORTID = cim.PORTID;
            func_77.UNITID = cim.UNITID;
            gem.HSMSSend(func_77);
        }

        public void SendS7F67()
        {
            var func = new CIMS7F67();
            func.UNITID = serverStatus.UnitID;
            gem.HSMSSend(func);
        }

        public void SendS7F69()
        {
            var func = new CIMS7F69();

            //조회한 레시피의 group list 얻어오기
            List<GROUPID> GroupList = new List<GROUPID>(); 
            foreach (var recipe in serverStatus.CIMRecipes)
            {
                var groupID = recipe.GROUPID;

                //중복 검사
                var find = GroupList.Find(g => g.Value.TrimEnd() == recipe.GROUPID);
                if (find == null)
                {
                    GroupList.Add(groupID);
                }
            }

            func.UNITID = serverStatus.UnitID;
            func.GROUPIDS.AddRange(GroupList);

            gem.HSMSSend(func);
        }

        public void SendS7F77(string recipeID)
        {
            var func = new CIMS7F77();
            func.LOTID = "LOTID";
            func.CSTID = "CSTID";
            func.RECIPEID = recipeID;
            func.PRCID = "PRCID";
            func.PORTID = 0;
            func.UNITID = serverStatus.UnitID;
            gem.HSMSSend(func);
        }

        private void Gem_OnS7F78Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
        //    CIMS7F78 cim = (CIMS7F78)e.cimClass;
        //    //GLSSTATUS  = Exist
        //    serverStatus.GLSSTATUS = "1";
        //    //Glass Load Start
        //    //SendS2F87(serverStatus.GLSSTATUS);
        }
    }
}
