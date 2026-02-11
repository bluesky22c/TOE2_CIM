using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;

namespace TOECIM
{
    public partial class ClientCIM
    {
        void InitS7()
        {
            gem.OnS7F19Event += Gem_OnS7F19Event;
            gem.OnS7F61Event += Gem_OnS7F61Event;
            gem.OnS7F67Event += Gem_OnS7F67Event;
            gem.OnS7F69Event += Gem_OnS7F69Event;
            gem.OnS7F77Event += Gem_OnS7F77Event;
        }


        private void Gem_OnS7F19Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS7F19 cim = (CIMS7F19)e.cimClass;
            var func = new CIMS7F20();
            func.UNITID = cim.UNITID;
            log.Information("Recipe Send");
            var groupList = Directory.GetDirectories(Program.SystemSetup.RecipePath);
            foreach (var g in groupList)
            {
                var recipeFileList = Directory.GetDirectories(g);
                foreach (string dir in recipeFileList)  
                {
                    string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                    log.Information($"Recipe Name {recipeName}");
                    func.RECIPEIDS.Add(recipeName);
                }
            }
            func.UNITID = cimStatus.UnitID;
            gem.HSMSSend(func);
        }

        private void Gem_OnS7F61Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS7F61 cim = (CIMS7F61)e.cimClass;
            var func = new CIMS7F62();
            func.UNITID = cim.UNITID;
            func.PORTID = cim.PORTID;
            func.RECIPEID = cim.RECIPEID;
            func.LOTID = cim.LOTID;
            func.SAMPLINGFLAG = cimStatus.SAMPLINGFLAG;
            func.ACKC7M.Value = (byte)ACKC7M.ACKC7MEnum.Accept;

            cimStatus.Attribute = cim.PRODTYPE.Value.Trim(' ');
            func.SLOTITEMACKS = new List<SLOTITEM_ACK>();
            foreach (var s in cim.SLOTITEMS)
            {
                var slotItemAck = new SLOTITEM_ACK
                {
                    SLOTID = s.SLOTID,
                    SRECIPEID = s.SRECIPEID,
                    ACKC7S = (byte)ACKC7S.ACKC7Senum.Accept
                };
                func.SLOTITEMACKS.Add(slotItemAck);
            }
            gem.HSMSSend(func);

            if(func.ACKC7M.Value == (byte)ACKC7M.ACKC7MEnum.Error)
            {
                return;
            }

            //WAIT 상태일때 레시피 check (S7F63, S7F77)
            if (cimStatus.SAMPLINGFLAG == SAMPLINGFLAG.SAMPLINGFLAGEnum.WAIT)
            {
                var func_sampling = new CIMS7F63();
                func_sampling.UNITID = cim.UNITID;
                func_sampling.PORTID = cim.PORTID;
                func_sampling.RECIPEID = cim.RECIPEID;
                func_sampling.LOTID = cim.LOTID;
                func_sampling.SLOTITEMSELECT.AddRange(CheckSlotRecipe(cim.SLOTITEMS));

                gem.HSMSSend(func_sampling);
            }
        }

        private void Gem_OnS7F67Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS7F67 cim = (CIMS7F67)e.cimClass;
            var func = new CIMS7F68();
            func.UNITID = cim.UNITID;

            var recipeFileList = Directory.GetDirectories(Program.SystemSetup.RecipePath);
            foreach (string dir in recipeFileList)
            {
                string groupName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                func.GROUPID.Add(groupName);
                //RecipeSet recipe = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeName);

                ////중복 검사
                //var find = func.GROUPID.Find(g => g.Value.TrimEnd() == recipe.GROUPID);
                //if (find == null)
                //{
                //    func.GROUPID.Add(recipe.GROUPID);
                //}
            }

            gem.HSMSSend(func);
        }


        private void Gem_OnS7F69Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            try
            {
                CIMS7F69 cim = (CIMS7F69)e.cimClass;
                var func = new CIMS7F70();
                func.UNITID = cim.UNITID;

                var recipeFileList = Directory.GetDirectories(Program.SystemSetup.RecipePath);

                foreach (var group in cim.GROUPIDS)
                {
                    var groupItem = new RECIPE_GROUP();

                    foreach (string dir in recipeFileList)
                    {
                        string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                        RecipeSet recipe = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeName);

                        if (recipe.GROUPID == group.Value.TrimEnd().ToUpper())
                        {
                            groupItem.GROUPID = group;
                            groupItem.RECIPEIDS.Add(recipe.RECIPEID);
                        }
                    }

                    func.RECIPE_GROUPS.Add(groupItem);
                }

                gem.HSMSSend(func);
            }
            catch (Exception exp)
            {
                log.Error(exp.ToString());
            }
        }

        /// <summary>
        /// sItems에서 받아 온 SRECIPEID를 check하여 레시피가 있으면 SELECTED = 1, 없으면 SELECTED = 0
        /// </summary>
        /// <param name="sItems"></param>
        /// <returns></returns>
        private List<SLOTITEM_SELECT> CheckSlotRecipe(List<SLOTITEM> sItems)
        {
            var result = new List<SLOTITEM_SELECT>();

            //장비가 가진 모든 RecipeID를 받아옴
            var recipeIDList = new List<string>();
            foreach (string dir in Directory.GetDirectories(Program.SystemSetup.RecipePath))
            {
                string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                recipeIDList.Add(recipeName);
            }

            foreach (var s in sItems)
            {
                //if (s.GLSEXIST == 0)
                //    continue;

                //recipe check logic
                var check = recipeIDList.Find(r => r.ToUpper() == s.SRECIPEID.Value.TrimEnd().ToUpper());

                var res = new SLOTITEM_SELECT();
                res.SLOTID = s.SLOTID;
                res.SRECIPEID = s.SRECIPEID;

                if(check == null)
                {
                    res.ACKC7S = (byte)ACKC7S.ACKC7Senum.Error;
                    res.SELECTED = "0";
                }
                else
                {
                    res.ACKC7S = (byte)ACKC7S.ACKC7Senum.Accept;
                    res.SELECTED = "1";
                }

                result.Add(res);
            }

            return result;
        }


        // 확인 필요
        private void Gem_OnS7F77Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            try
            {
                CIMS7F77 cim = (CIMS7F77)e.cimClass;

                var func = new CIMS7F78();
                func.LOTID = cim.LOTID;
                func.CSTID = cim.CSTID;
                func.RECIPEID = cim.RECIPEID;
                //func.RECIPEIDVER
                func.PRCID = cim.PRCID;
                func.PORTID = cim.PORTID;
                func.UNITID = cim.UNITID;

                UNITITEM unitItem = new UNITITEM();

                var recipeFileList = Directory.GetDirectories(Program.SystemSetup.RecipePath);
                foreach (string dir in recipeFileList)
                {
                    string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                    if (recipeName == cim.RECIPEID)
                    {
                        RecipeSet recipe = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeName);

                        unitItem.EUNITID = (ushort)func.UNITID;
                        //unitItem.LOWERRECIPEID = ;
                        //unitItem.LOWERRECIPEIDVER = ;
                        unitItem.LEVEL = recipe.LEVEL;
                        unitItem.RECIPEIDTYPE = recipe.REFCIPEIDTYPE;

                        
                        foreach (var scanRecipe in recipe.ScanRecipe)
                        {
                            var step = new STEPITEM();
                            step.STEP = "0";    //step ??? 일단 사용 안함 (0)

                            var rParams = RECIPEtoRPARAM(scanRecipe);
                            step.RPARMITEMS.AddRange(rParams);

                            unitItem.STEPITEMS.Add(step);
                        }

                        func.UNITITEMS.Add(unitItem);
                    }
                }

                gem.HSMSSend(func);
            }
            catch (Exception exp)
            {
                log.Error(exp.ToString());
            }
        }
    }
}
