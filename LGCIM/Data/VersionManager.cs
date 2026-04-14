using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TOELib;

namespace LGCIM
{
    public class VersionManager
    {
        public List<VersionHistory> versionHistory { get; set; } = new List<VersionHistory>();
        public VersionManager()
        {
            InitVersion();
        }

        public void InitVersion()
        {
            Console.WriteLine("VersionManager: InitVersion Starting...");
            try {
                var fileName = Vars.VersionJsonPath;
                Console.WriteLine($"VersionManager: Loading from {fileName}");

                if (File.Exists(fileName))
                {
                    versionHistory = JsonConvert.DeserializeObject<List<VersionHistory>>(File.ReadAllText(fileName));
                    Console.WriteLine("VersionManager: Deserialization complete.");
                }
                else
                {
                    versionHistory = new List<VersionHistory>();
                    Console.WriteLine("VersionManager: File not found, created empty list.");
                }
            } catch (Exception ex) {
                Console.WriteLine($"VersionManager: Error in InitVersion: {ex.Message}");
            }
            Console.WriteLine("VersionManager: InitVersion Complete.");
        }

        public void Save()
        {
            File.WriteAllText(Vars.VersionJsonPath, JsonConvert.SerializeObject(versionHistory, Formatting.Indented));
        }

        public List<VersionHistory> SearchVersion(DateTime start, DateTime end)
        {
            List<VersionHistory> items;

            items = versionHistory;

            var findDate = items.FindAll(v => (Convert.ToDateTime(v.Date) >= start) && (end >= Convert.ToDateTime(v.Date)));

            return findDate;
        }
    }

    public class VersionHistory
    {
        public int No { get; set; }
        public string Date { get; set; } = "";
        public string Version { get; set; } = "";
        /// <summary>
        /// 작업자명 표기
        public string Operator { get; set; } = "";
        /// <summary>
        /// 작업 내용 표기
        public string UpdateContent { get; set; } = "";
        public string UpdateContentTitle { get; set; } = "업데이트 내용";
    }

}
