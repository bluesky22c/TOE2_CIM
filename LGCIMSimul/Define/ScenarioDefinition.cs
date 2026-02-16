using System;
using System.Collections.Generic;

namespace LGCIMServer.Define
{
    public class Scenario
    {
        public string Name { get; set; }
        public List<ScenarioStep> Steps { get; set; } = new List<ScenarioStep>();
    }

    public class ScenarioStep
    {
        public string Action { get; set; } // e.g., "LoadReady", "AlarmSet", "LotEnd"
        public Dictionary<string, string> Params { get; set; } // e.g., {"id": "1"}
        public int DelayBefore { get; set; } // ms
    }
}
