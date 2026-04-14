using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LGCIM.Lib;

namespace LGCIMServer
{
    public partial class ServerCIM
    {
        public event EventHandler ShowRecipeForm = delegate { };


        void InitS15()
        {
            gem.OnS15F72Event += Gem_OnS15F72Event;
            gem.OnS15F76Event += Gem_OnS15F76Event;
            gem.OnS15F78Event += Gem_OnS15F78Event;
            gem.OnS15F79Event += Gem_OnS15F79Event;
        }

        public void SendS15F65()
        {
            var func = new CIMS15F65();
            func.UNITID = serverStatus.UnitID;
            gem.HSMSSend(func);
        }

        public void SendS15F71()
        {
            var func = new CIMS15F71();
            func.UNITID = serverStatus.UnitID;
            func.LEVEL = "1";   // Level 1, 2, 3 Recipe ID
            func.RECIPEIDTYPE = RECIPEIDTYPE.RECIPEIDTYPEEnum.Equipment;
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "1" });
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "2" });
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "3" });

            gem.HSMSSend(func);
        }

        private void Gem_OnS15F72Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS15F72 cim = (CIMS15F72)e.cimClass;

            serverStatus.CIMRecipes = new List<RecipeSet>();
            foreach (var recipe in cim.RECIPE_EQS)
            {
                serverStatus.CIMRecipes.Add(new RecipeSet
                {
                    RECIPEID = recipe.RECIPEID,
                    REFCIPEIDTYPE = recipe.RECIPEIDTYPE,
                });
            }

            ShowRecipeForm(this, null);
        }

        public void SendS15F75(string recipeID)
        {
            var func = new CIMS15F75();
            func.UNITID = serverStatus.UnitID;
            func.RECIPEID = recipeID;
            func.LEVEL = "1";
            func.RECIPEIDTYPE = RECIPEIDTYPE.RECIPEIDTYPEEnum.Equipment;
            func.RECIPEVER = DateTime.Now.ToString("yyyyMMddhhmm");
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "1" });
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "2" });
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "3" });

            gem.HSMSSend(func);
        }

        private void Gem_OnS15F76Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            try
            {
                CIMS15F76 cim = (CIMS15F76)e.cimClass;

                var find = serverStatus.CIMRecipes.Find(r => r.RECIPEID == cim.RECIPE_STEP.RECIPEID);
                if (find != null)
                {
                    find.LEVEL = cim.RECIPE_STEP.LEVEL;
                    find.REFCIPEIDTYPE = cim.RECIPE_STEP.RECIPEIDTYPE;

                    foreach (var recipe in cim.RECIPE_STEP.RECIPEVER_STEPS)
                    {
                        var rParams = recipe.RPARMITEMS;

                        var scanRecipe = new CIMRecipe();
                        if(rParams.Count != 4)
                        {
                            find.ScanRecipe.Add(scanRecipe);    //초기값
                            continue;
                        }
                        scanRecipe.ScanNo = int.Parse(rParams[0].RPARMVAL);
                        scanRecipe.PARAM_A = rParams[1].RPARMVAL;
                        scanRecipe.PARAM_B = rParams[2].RPARMVAL;
                        scanRecipe.PARAM_C = rParams[3].RPARMVAL;

                        find.ScanRecipe.Add(scanRecipe);
                    }

                    ShowRecipeForm(this, null);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void SendS15F73(string recipeID)
        {
            var func = new CIMS15F73();
            func.RECIPE_LOWER_REQ.UNITID = serverStatus.UnitID;
            func.RECIPE_LOWER_REQ.RECIPEID = recipeID;
            func.RECIPE_LOWER_REQ.LEVEL = "1";
            func.RECIPE_LOWER_REQ.RECIPEIDTYPE = RECIPEIDTYPE.RECIPEIDTYPEEnum.Equipment;
            func.RECIPE_LOWER_REQ.RECIPEIDVER = DateTime.Now.ToString("yyyyMMddhhmm");
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "1" });
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "2" });
            func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "3" });

            gem.HSMSSend(func);
        }

        public void SendS15F77(RecipeSet recipeSet, RECIPEIDCMD.RECIPEIDCMDEnum cmd)
        {            
            try
            {
                var func = new CIMS15F77();
                func.UNITID = serverStatus.UnitID;
                func.RECIPEIDCMD = cmd;
                func.RECIPEID = recipeSet.RECIPEID;
                func.LEVEL = recipeSet.LEVEL;
                func.RECIPEIDVER = DateTime.Now.ToString("yyyyMMddhhmm");
                func.RECIPEIDTYPE = recipeSet.REFCIPEIDTYPE;

                foreach (var scanRecipe in recipeSet.ScanRecipe)
                {
                    var step = new RPARMITEM_STEP();
                    step.STEP = "0";    //step ??? 일단 사용 안함 (0)

                    PropertyInfo[] property = scanRecipe.GetType().GetProperties();

                    foreach (var p in property) 
                    {
                        step.RPARMITEMS.Add(new RPARMITEM
                        {
                            RPARAMNAME = p.Name,
                            RPARMVAL = p.GetValue(scanRecipe).ToString()
                        });
                    }

                    func.RPARMITEM_STEPS.Add(step);
                }

                func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "1" });
                func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "2" });
                func.ESDS.Add(new ESD { ESDITEM = "ESD", ESDVALUE = "3" });

                gem.HSMSSend(func);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        private void Gem_OnS15F78Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS15F78 cim = (CIMS15F78)e.cimClass;

            switch ((REACK.REACKEnum)cim.REACK.Value)
            {
                case REACK.REACKEnum.Accept:
                    break;

                case REACK.REACKEnum.ParamError:
                    break;
            }
        }



        private void Gem_OnS15F79Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS15F79 cim = (CIMS15F79)e.cimClass;
            var func = new CIMS15F80();
            func.UNITID = serverStatus.UnitID;
            func.RECIPEID = cim.RECIPEID;
            func.LEVEL = cim.LEVEL;
            func.RECIPEVER = cim.RECIPEIDVER;
            func.RECIPEIDTYPE = cim.RECIPEIDTYPE;
            func.PHACK = (byte)PHACK.PHACKEnum.Accept;

            var recipCheck = serverStatus.CIMRecipes.Find(r => r.RECIPEID.ToUpper() == cim.RECIPEID.Value.ToUpper().TrimEnd());
            RECIPEIDCMD.RECIPEIDCMDEnum cmd = cim.RECIPEIDCMD;

            switch (cmd)
            {
                case RECIPEIDCMD.RECIPEIDCMDEnum.Built:
                    break;

                case RECIPEIDCMD.RECIPEIDCMDEnum.Change:
                    if (recipCheck == null)
                    {
                        func.PHACK = (byte)PHACK.PHACKEnum.NoRecipeID;
                        gem.HSMSSend(func);
                        return;
                    }
                    break;

                case RECIPEIDCMD.RECIPEIDCMDEnum.Delete:
                    if (recipCheck == null)
                    {
                        func.PHACK = (byte)PHACK.PHACKEnum.NoRecipeID;
                        gem.HSMSSend(func);
                        return;
                    }
                    break;

                case RECIPEIDCMD.RECIPEIDCMDEnum.Rename:
                    break;
            }

            func.PHACK = (byte)PHACK.PHACKEnum.Accept;
            gem.HSMSSend(func);
        }
    }
}
