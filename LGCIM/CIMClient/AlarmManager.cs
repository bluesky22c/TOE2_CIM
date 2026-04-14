using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LGCIM.Lib;

namespace LGCIM
{
    public class AlarmManager
    {
        private List<AlarmInfo> _alarmHistory = new List<AlarmInfo>();
        private readonly string _filePath;
        private static readonly object _lock = new object();

        public AlarmManager()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            _filePath = Path.Combine(directory, "alarmHistory.json");
            Load();
        }

        public void InitDB()
        {
            // DB 호환성을 위해 유지 (필요 시 Load 재호출)
            Load();
        }

        private void Load()
        {
            lock (_lock)
            {
                try
                {
                    if (File.Exists(_filePath))
                    {
                        var json = File.ReadAllText(_filePath, Encoding.Default);
                        _alarmHistory = JsonConvert.DeserializeObject<List<AlarmInfo>>(json) ?? new List<AlarmInfo>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AlarmManager Load Error] {ex.Message}");
                }
            }
        }

        private void Save()
        {
            lock (_lock)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(_alarmHistory, Formatting.Indented, new StringEnumConverter());
                    File.WriteAllText(_filePath, json, Encoding.Default);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[AlarmManager Save Error] {ex.Message}");
                }
            }
        }

        public void SetAlarm(AlarmInfo alarm)
        {
            if (alarm == null) return;

            lock (_lock)
            {
                // 새 알람인 경우 ID 부여 (DB AutoIncrement 대체)
                if (alarm.ID == 0)
                {
                    alarm.ID = _alarmHistory.Count > 0 ? _alarmHistory.Max(a => a.ID) + 1 : 1;
                }
                
                _alarmHistory.Add(alarm);
                Save();
            }
        }

        public void ReleaseAlarm(AlarmInfo alarm)
        {
            if (alarm == null) return;

            lock (_lock)
            {
                var find = _alarmHistory.FirstOrDefault(a => a.ID == alarm.ID);
                if (find != null)
                {
                    find.IsRelease = true;
                    find.ALCD = alarm.ALCD;
                    Save();
                }
            }
        }

        public List<AlarmInfo> ReadAllAlarm()
        {
            lock (_lock)
            {
                return _alarmHistory.ToList();
            }
        }

        public List<AlarmInfo> SearchAlarm(string sql)
        {
            // 기존 SQL 검색 모델을 간소화 (단순 리스트 반환으로 하되, 필요 시 필터링 로직 추가 가능)
            // Andrej Karpathy Style: 최대한 단순하게 유지
            lock (_lock)
            {
                return _alarmHistory.ToList();
            }
        }
    }
}
