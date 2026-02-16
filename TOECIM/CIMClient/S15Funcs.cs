using LGHSMSBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TOECIMLib;

namespace TOECIM
{
    public partial class ClientCIM
    {
        void InitS15()
        {
            gem.OnS15F65Event += Gem_OnS15F65Event;
            gem.OnS15F71Event += Gem_OnS15F71Event;
            gem.OnS15F73Event += Gem_OnS15F73Event;
            gem.OnS15F75Event += Gem_OnS15F75Event;
            gem.OnS15F77Event += Gem_OnS15F77Event;
            gem.OnS15F80Event += Gem_OnS15F80Event;
        }

        private void Gem_OnS15F65Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS15F65 cim = (CIMS15F65)e.cimClass;
            var func = new CIMS15F66();
            func.UNITID = cim.UNITID;

            var recipeFileList = Directory.GetDirectories(Program.SystemSetup.RecipePath);
            foreach (string dir in recipeFileList)
            {
                string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                RecipeSet recipe = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeName);

                var recipeItem = new RECIPEItem();
                recipeItem.RECIPEID = recipe.RECIPEID;
                recipeItem.RECIPEIDTYPE = recipe.REFCIPEIDTYPE;
                recipeItem.RECIPEIDVERS.Add(DateTime.Now.ToString("yyyyMMddHHmm")); //RecipeIDVer 확인 필요

                func.RECIPEItems.Add(recipeItem);
            }

            gem.HSMSSend(func);
        }

        private void Gem_OnS15F71Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            try
            {
                CIMS15F71 cim = (CIMS15F71)e.cimClass;
                var func = new CIMS15F72();

                var recipeFileList = Directory.GetDirectories(Program.SystemSetup.RecipePath);
                foreach (string dir in recipeFileList)
                {
                    string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                    RecipeSet recipe = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeName);

                    var reportRecipe = new RECIPE_EQ
                    {
                        UNITID = cim.UNITID,
                        RECIPEID = recipe.RECIPEID,
                        LEVEL = recipe.LEVEL,
                        RECIPEIDTYPE = recipe.REFCIPEIDTYPE,
                    };

                    //RecipeVer 확인 필요
                    var recipeVer = new RECIPEVER
                    {
                        RECIPEIDVER = DateTime.Now.ToString("yyyyMMddHHmm"),
                        RECIPEIDSTATUS = RECIPEIDSTATUS.RECIPEIDSTATUSEnum.Running
                    };
                    reportRecipe.RECIPEVERS.Add(recipeVer);

                    func.RECIPE_EQS.Add(reportRecipe);
                }

                func.ESDS.AddRange(cim.ESDS);
                gem.HSMSSend(func);
            }
            catch (Exception exp)
            {
                log.Error(exp.ToString());
            }
        }

        //Lower recipe
        private void Gem_OnS15F73Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            try
            {
                CIMS15F73 cim = (CIMS15F73)e.cimClass;
                var func = new CIMS15F74();

                var recipeFileList = Directory.GetDirectories(Program.SystemSetup.RecipePath);
                foreach (string dir in recipeFileList)
                {
                    string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                    if (recipeName == cim.RECIPE_LOWER_REQ.RECIPEID)
                    {
                        RecipeSet recipe = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeName);

                        //하위 레시피 뭔지 확인 필요
                        //foreach (var scanRecipe in recipe.ScanRecipe)
                        //{
                        //    RECIPE_LOWER_RES lowerRecipe = new RECIPE_LOWER_RES();
                        //    lowerRecipe.UNITID = cim.RECIPE_LOWER_REQ.UNITID;
                        //    lowerRecipe.RECIPEID = recipeName;
                        //    lowerRecipe.LEVEL = recipe.LEVEL;
                        //    lowerRecipe.RECIPEIDTYPE = recipe.REFCIPEIDTYPE;
                        //    lowerRecipe.RECIPEIDVERS.Add(scanRecipe.ScanNo.ToString()); //recipIdVer ???

                        //    func.RECIPE_LOWER_RESS.Add(lowerRecipe);
                        //}
                    }
                }

                func.ESDS.AddRange(cim.ESDS);
                gem.HSMSSend(func);
            }
            catch (Exception exp)
            {
                log.Error(exp.ToString());
            }
        }

        //파라미터 조회
        private void Gem_OnS15F75Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            try
            {
                CIMS15F75 cim = (CIMS15F75)e.cimClass;
                var func = new CIMS15F76();

                func.RECIPE_STEP.UNITID = cim.UNITID;
                func.RECIPE_STEP.RECIPEID = cim.RECIPEID;
                func.RECIPE_STEP.LEVEL = cim.LEVEL;
                func.RECIPE_STEP.RECIPEIDVERS = cim.RECIPEVER;

                var recipeFileList = Directory.GetDirectories(Program.SystemSetup.RecipePath);
                foreach (string dir in recipeFileList)
                {
                    string recipeName = Path.GetFileNameWithoutExtension(dir).ToUpper();
                    if (recipeName == cim.RECIPEID)
                    {
                        RecipeSet recipe = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeName);

                        foreach (var scanRecipe in recipe.ScanRecipe)
                        {
                            var step = new RPARMITEM_STEP();
                            step.STEP = "0";    //step ??? 일단 사용 안함 (0)

                            var rParams = RECIPEtoRPARAM(scanRecipe);
                            step.RPARMITEMS.AddRange(rParams);

                            func.RECIPE_STEP.RECIPEVER_STEPS.Add(step);
                        }
                    }
                }
                func.ESDS.AddRange(cim.ESDS);
                gem.HSMSSend(func);
            }
            catch (Exception exp)
            {
                log.Error(exp.ToString());
            }
        }


        //레시피 파라미터 변경
        private void Gem_OnS15F77Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS15F77 cim = (CIMS15F77)e.cimClass;
            var func = new CIMS15F78();

            func.UNITID = cim.UNITID;
            func.RECIPEID = cim.RECIPEID;
            func.LEVEL = cim.LEVEL;
            func.RECIPEIDVER = cim.RECIPEIDVER;
            func.RECIPEIDTYPE = cim.RECIPEIDTYPE;

            //검사중일때는 변경 불가
            if (cimStatus.EQGlassIn == true)
            {
                RecipeChangePopUp(this, new RecipeArgs
                {
                    ACK = false,
                    RecipeCMD = cim.RECIPEIDCMD,
                    ByWho = ByWho.Equipment,
                    RecipeSet = new RecipeSet { RECIPEID = cim.RECIPEID.Value.TrimEnd(), DESC = "EQ Busy" }
                });
                log.Warning($"현재 RUN 상태입니다. 레시피 변경 불가.");
                func.REACK = (byte)REACK.REACKEnum.EQBusy;
                gem.HSMSSend(func);
                return;
            }

            var recipeID = cim.RECIPEID.Value.TrimEnd();
            var recipePath = Path.Combine(Program.SystemSetup.RecipePath, recipeID);

            RECIPEIDCMD.RECIPEIDCMDEnum paramType = cim.RECIPEIDCMD;
            switch (paramType)
            {
                //레시피 생성
                case RECIPEIDCMD.RECIPEIDCMDEnum.Built:
                    if (Directory.Exists(recipePath))
                    {
                        RecipeChangePopUp(this, new RecipeArgs
                        {
                            ACK = false,
                            RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Built,
                            ByWho = ByWho.Equipment,
                            RecipeSet = new RecipeSet { RECIPEID = recipeID, DESC = "RecipeExist" }
                        });
                        log.Warning($"[{recipeID}] 이미 존재하는 레시피입니다.");
                        func.REACK = (byte)REACK.REACKEnum.RecipeExist;
                        gem.HSMSSend(func);
                        return;
                    }

                    RecipeSet newRecipe = new RecipeSet();
                    newRecipe.RECIPEID = cim.RECIPEID;
                    newRecipe.LEVEL = cim.LEVEL;
                    newRecipe.REFCIPEIDTYPE = cim.RECIPEIDTYPE;
                    
                    foreach(var scan in cim.RPARMITEM_STEPS)
                    {
                        var scanRecipe = RPARAMtoRECIPE(scan);
                        if(scanRecipe == null)
                        {
                            RecipeChangePopUp(this, new RecipeArgs
                            {
                                ACK = false,
                                RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Built,
                                ByWho = ByWho.Equipment,
                                RecipeSet = new RecipeSet { RECIPEID = recipeID, DESC = "Parameter Error" }
                            });
                            log.Warning($"[{recipeID}] 레시피에 해당 파라미터가 없습니다.");
                            func.REACK = (byte)REACK.REACKEnum.ParamError;
                            gem.HSMSSend(func);
                            return;
                        }
                        newRecipe.ScanRecipe.Add(scanRecipe);
                    }

                    newRecipe.Save(Program.SystemSetup.RecipePath, newRecipe.RECIPEID);

                    RecipeChangePopUp(this, new RecipeArgs 
                    {
                        RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Built,
                        ByWho = ByWho.Host,
                        RecipeSet = newRecipe
                    });
                    break;

                //파라미터 변경
                case RECIPEIDCMD.RECIPEIDCMDEnum.Change:
                    RecipeSet changeRecipe = new RecipeSet();
                    changeRecipe.RECIPEID = cim.RECIPEID;
                    changeRecipe.LEVEL = cim.LEVEL;
                    changeRecipe.REFCIPEIDTYPE = cim.RECIPEIDTYPE;

                    foreach (var scan in cim.RPARMITEM_STEPS)
                    {
                        var scanRecipe = RPARAMtoRECIPE(scan);
                        if (scanRecipe == null)
                        {
                            RecipeChangePopUp(this, new RecipeArgs
                            {
                                ACK = false,
                                RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Change,
                                ByWho = ByWho.Equipment,
                                RecipeSet = new RecipeSet { RECIPEID = recipeID, DESC = "Parameter Error" }
                            });
                            log.Warning($"[{recipeID}] 레시피에 해당 파라미터가 없습니다.");
                            func.REACK = (byte)REACK.REACKEnum.ParamError;
                            gem.HSMSSend(func);
                            return;
                        }
                        changeRecipe.ScanRecipe.Add(scanRecipe);
                    }

                    changeRecipe.Save(Program.SystemSetup.RecipePath, changeRecipe.RECIPEID);
                    
                    RecipeChangePopUp(this, new RecipeArgs 
                    {
                        RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Change,
                        ByWho = ByWho.Host,
                        RecipeSet = changeRecipe
                    });
                    break;

                //레시피 삭제
                case RECIPEIDCMD.RECIPEIDCMDEnum.Delete:
                    if (!Directory.Exists(recipePath))
                    {
                        RecipeChangePopUp(this, new RecipeArgs
                        {
                            ACK = false,
                            RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Delete,
                            ByWho = ByWho.Equipment,
                            RecipeSet = new RecipeSet { RECIPEID = recipeID, DESC = "NoRecipeID" }
                        });
                        log.Warning($"[{recipeID}] 삭제할 레시피가 없습니다.");
                        func.REACK = (byte)REACK.REACKEnum.NoRecipeID;
                        gem.HSMSSend(func);
                        return;
                    }

                    RecipeChangePopUp(this, new RecipeArgs
                    {
                        RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Delete,
                        ByWho = ByWho.Host,
                        RecipeSet = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeID)
                    });

                    if (Directory.Exists(recipePath))
                    {
                        Directory.Delete(recipePath, true);
                    }
                    break;

                //레시피 이름 변경
                case RECIPEIDCMD.RECIPEIDCMDEnum.Rename:
                    //Rename 확인 필요함.. ECS에서 old recipeID / new recipeID 둘다 보내줘야 할듯
                    if (Directory.Exists(recipePath))
                    {
                        RecipeChangePopUp(this, new RecipeArgs
                        {
                            ACK = false,
                            RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Rename,
                            ByWho = ByWho.Equipment,
                            RecipeSet = new RecipeSet { RECIPEID = recipeID, DESC = "RecipeExist" }
                        });
                        log.Warning($"[{recipeID}] 이미 존재하는 레시피입니다.");
                        func.REACK = (byte)REACK.REACKEnum.RecipeExist;
                        gem.HSMSSend(func);
                        return;
                    }

                    string newName = Path.Combine(Program.SystemSetup.RecipePath, recipeID);
                    //Directory.Move(oldPath, newPath);

                    RecipeChangePopUp(this, new RecipeArgs
                    {
                        RecipeCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Rename,
                        ByWho = ByWho.Host,
                        RecipeSet = RecipeSet.Load(Program.SystemSetup.RecipePath, recipeID)
                    });
                    break;
            }

            func.REACK = (byte)REACK.REACKEnum.Accept;
            gem.HSMSSend(func);
        }

        RECIPEIDCMD.RECIPEIDCMDEnum LastCMD = RECIPEIDCMD.RECIPEIDCMDEnum.Change;
        public void SendS15F79(RecipeSet recipeSet, RECIPEIDCMD.RECIPEIDCMDEnum cmd)
        {
            var func = new CIMS15F79();
            func.UNITID = cimStatus.UnitID;
            func.RECIPEIDCMD = cmd;
            func.RECIPEID = recipeSet.RECIPEID;
            func.LEVEL = recipeSet.LEVEL;
            //func.RECIPEIDVER = DateTime.Now.ToString("yyyyMMddhhmm");   //확인 필요함
            func.RECIPEIDTYPE = recipeSet.REFCIPEIDTYPE;

            foreach (var scanRecipe in recipeSet.ScanRecipe)
            {
                var step = new RPARMITEM_STEP();
                step.STEP = "0";    //step ??? 일단 사용 안함 (0)

                var rParams = RECIPEtoRPARAM(scanRecipe);
                step.RPARMITEMS.AddRange(rParams);

                func.RPARMITEM_STEPS.Add(step);
            }

            //switch (cmd)
            //{
            //    case RECIPEIDCMD.RECIPEIDCMDEnum.Built:

            //        break;

            //    case RECIPEIDCMD.RECIPEIDCMDEnum.Change:
            //        break;

            //    case RECIPEIDCMD.RECIPEIDCMDEnum.Delete:
            //        break;

            //    case RECIPEIDCMD.RECIPEIDCMDEnum.Rename:
            //        break;
            //}

            LastCMD = cmd;
            gem.HSMSSend(func);
        }


        private void Gem_OnS15F80Event(object sender, MMISoft.HSMS.GemEventArgs e)
        {
            CIMS15F80 cim = (CIMS15F80)e.cimClass;
            var recipeID = cim.RECIPEID.Value.TrimEnd();

            string[] phack_items = Enum.GetNames(typeof(PHACK.PHACKEnum));
            if (cim.PHACK != (byte)PHACK.PHACKEnum.Accept)
            {
                RecipeChangePopUp(this, new RecipeArgs
                {
                    ACK = false,
                    RecipeCMD = LastCMD,
                    ByWho = ByWho.Host,
                    RecipeSet = new RecipeSet { RECIPEID = recipeID, DESC = phack_items[cim.PHACK] }
                });
            }
        }


        /// <summary>
        /// Recipe를 Rparam 클래스로 변환
        /// </summary>
        static public List<RPARMITEM> RECIPEtoRPARAM(CIMRecipe recipe)
        {
            List<RPARMITEM> result = new List<RPARMITEM>();

            try
            {
                PropertyInfo[] property = recipe.GetType().GetProperties();
                string[] ppid_items = Enum.GetNames(typeof(PPID_ITEM));

                for (int i = 0; i < ppid_items.Length; i++)
                {
                    var ppid_item = ppid_items[i].ToUpper();
                    var paramName = Array.Find(property, p => p.Name.ToUpper() == ppid_item);

                    if (paramName != null)
                    {
                        result.Add(new RPARMITEM
                        {
                            RPARAMNAME = ppid_item,
                            RPARMVAL = paramName.GetValue(recipe).ToString(),
                        });
                    }
                }

                return result;
            }
            catch(Exception exp)
            {
                log.Error(exp.ToString());
                return result;
            }
        }

        /// <summary>
        /// Rparam을 Recipe 클래스로 변환
        /// </summary>
        /// <param name="rparam"></param>
        /// <returns></returns>
        static public CIMRecipe RPARAMtoRECIPE(RPARMITEM_STEP rparam)
        {
            try
            {
                List<RPARMITEM> rParams = rparam.RPARMITEMS;
                CIMRecipe recipe = new CIMRecipe();
                PropertyInfo[] propertys = recipe.GetType().GetProperties();

                for (int i = 0; i < rParams.Count; i++)
                {
                    var paramName = rParams[i].RPARAMNAME.Value.ToString().ToUpper().TrimEnd();
                    var findPPIDName = Array.Find(propertys, p => p.Name.ToUpper() == paramName);

                    if (findPPIDName == null)
                    {
                        log.Verbose($"[RPARAMtoRECIPE] parameter error. {paramName}");
                        return null;
                    }

                    var recipeParam = recipe.GetType().GetProperty(findPPIDName.Name);
                    var recipeValue = Convert.ChangeType(rParams[i].RPARMVAL.Value, recipeParam.PropertyType);

                    recipeParam.SetValue(recipe, recipeValue, null);
                }

                return recipe;
            }
            catch (Exception exp)
            {
                log.Error(exp.ToString());
                return null;
            }
        }
    }
}
