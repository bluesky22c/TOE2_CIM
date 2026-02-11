using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TOECIMLib
{
    [Serializable]
    public class RecipeSet
    {
        public RecipeSet()
        {

        }

        public string RECIPEID { get; set; } = "";
        public string REFCIPEIDTYPE { get; set; } = "E";    //EQP Recipe
        public string LEVEL { get; set; } = "1";
        public string GROUPID { get; set; } = "GROUPID";
        public string DESC { get; set; } = "";
        public List<CIMRecipe> ScanRecipe { get; set; } = new List<CIMRecipe>();


        //void NullCheck()
        //{
        //    if(ScanRecipe.Count == 0)
        //    {
        //        ScanRecipe.Add(new Recipe { ScanNo = 0 });
        //        ScanRecipe.Add(new Recipe { ScanNo = 1 });
        //    }
        //}

        public static RecipeSet Load(string orgPath, string recipeName)
        {
            try
            {
                var path = Path.Combine(orgPath, recipeName, $"{recipeName}.json");
                if (File.Exists(path))
                {
                    return JsonConvert.DeserializeObject<RecipeSet>(File.ReadAllText(path));
                }
                else
                {
                    return new RecipeSet();
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public void Save(string orgPth, string recipeName)
        {
            try
            {
                var fileName = Path.Combine(orgPth, recipeName, $"{recipeName}.json");
                var dir = Path.GetDirectoryName(fileName);
                if (Directory.Exists(dir) == false)
                    Directory.CreateDirectory(dir);
                File.WriteAllText(fileName, JsonConvert.SerializeObject(this, Formatting.Indented));
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        #region xml
        //public bool Save(string fileName)
        //{
        //    try
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(fileName));
        //        using (StreamWriter sw = new StreamWriter(fileName, false, Encoding.Default))
        //        {
        //            XmlSerializer xs = new XmlSerializer(typeof(Recipe));
        //            xs.Serialize(sw, this);
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        throw ee;
        //    }
        //    return true;
        //}

        //static public Recipe Load(string fileName)
        //{
        //    Recipe setup = new Recipe();

        //    try
        //    {
        //        if (File.Exists(fileName) == false)
        //        {
        //            setup.Save(fileName);
        //            return setup;
        //        }
        //        using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
        //        {
        //            XmlSerializer xs = new XmlSerializer(typeof(Recipe));
        //            setup = (Recipe)xs.Deserialize(sr);
        //        }
        //    }
        //    catch (Exception ee)
        //    {
        //        throw ee;
        //    }
        //    return setup;
        //}
        #endregion

    }

    /// <summary>
    /// Cim으로 보고할 레시피 파라미터
    /// 보고할 파라미터가 정해지면 아래 property에 추가하여 사용
    /// </summary>
    [Serializable]
    public class CIMRecipe
    {
        public CIMRecipe()
        {

        }

        public int ScanNo { get; set; } = 0;
        public string PARAM_A { get; set; } = "1";
        public string PARAM_B { get; set; } = "2";
        public string PARAM_C { get; set; } = "3";
    }


    /// <summary>
    /// 보고할 레시피 파라미터
    /// 보고할 파라미터가 정해지면 아래 enum에 추가하여 사용
    /// </summary>
    public enum PPID_ITEM
    {
        SCANNO = 0,
        PARAM_A,
        PARAM_B,
        PARAM_C,
    }
}
