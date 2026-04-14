using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LGCIMServer.Define;
using LGCIMServer.EQPSimul;
using LGCIM.Lib; // For AlarmType if needed

namespace LGCIMServer
{
    public class ScenarioRunner
    {
        private EQPSimulator _eqpSimulator;
        private ServerCIM _ecsSimulator;

        public ScenarioRunner(EQPSimulator eqp, ServerCIM ecs)
        {
            _eqpSimulator = eqp;
            _ecsSimulator = ecs;
        }

        public event Action<string> ScenarioStarted;
        public event Action<string, int> StepExecuting; // ActionName, StepIndex
        public event Action<string> ScenarioCompleted;

        public Define.Scenario GetScenario(string id) => GetPredefinedScenario(id);

        public async Task RunScenarioAsync(string scenarioId)
        {
            var scenario = GetPredefinedScenario(scenarioId);
            if (scenario == null) 
            {
                Log($"Scenario {scenarioId} not found.");
                return;
            }

            Log($"Starting Scenario: {scenario.Name}");
            ScenarioStarted?.Invoke(scenario.Name);

            for (int i = 0; i < scenario.Steps.Count; i++)
            {
                var step = scenario.Steps[i];
                if (step.DelayBefore > 0)
                {
                    Log($"Waiting {step.DelayBefore}ms...");
                    await Task.Delay(step.DelayBefore);
                }

                StepExecuting?.Invoke(step.Action, i);
                ExecuteStep(step);
            }

            Log($"Scenario {scenario.Name} Completed.");
            ScenarioCompleted?.Invoke(scenario.Name);
        }

        private void ExecuteStep(Define.ScenarioStep step)
        {
             Log($"Executing Step: {step.Action}");
             try
             {
                 switch (step.Action)
                 {
                     // Scenario A: Normal Flow
                     case "LoadReady": _eqpSimulator.LoadReady(); break;
                     case "LoadDone": _eqpSimulator.LoadDone(); break;
                     case "InspectDone": _eqpSimulator.InspectDone(); break;
                     case "UnloadReady": _eqpSimulator.UnloadReady(); break;
                     case "ExchangeReady": _eqpSimulator.ExchangeReadyDone(); break;
                     case "UnloadDone": _eqpSimulator.UnloadDone(); break;
                     
                     // Scenario D: Lot End
                     case "LotEnd": _ecsSimulator.SendS6F65(); break;
                     
                     // Scenario E: Unload Glass Data
                     case "UnloadGlassData": _ecsSimulator.SendS6F63(); break;

                     // Helper: Alarm
                     case "AlarmSet":
                        if(step.Params != null && step.Params.ContainsKey("id"))
                        {
                            int id = int.Parse(step.Params["id"]);
                            _eqpSimulator.SetAlarm(AlarmType.LightAlarm, id);
                        }
                        break;
                     case "AlarmClear": _eqpSimulator.ClearAlarm(); break;

                     // Recipe
                     case "RecipeCreate": _eqpSimulator.RecipeCreate(step.Params?["id"] ?? "TEST_RECIPE"); break;
                     case "RecipeChange": _eqpSimulator.RecipeChange(step.Params?["id"] ?? "TEST_RECIPE"); break;
                     case "RecipeDelete": _eqpSimulator.RecipeDelete(step.Params?["id"] ?? "TEST_RECIPE"); break;
                     
                     default:
                        Log($"Unknown Action: {step.Action}");
                        break;
                 }
             }
             catch (Exception ex)
             {
                 Log($"Error executing step {step.Action}: {ex.Message}");
             }
        }

        private Define.Scenario GetPredefinedScenario(string id)
        {
            switch (id.ToUpper())
            {
                case "A":
                    return new Define.Scenario
                    {
                        Name = "Scenario A: Normal Loading Sequence",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "LoadReady", DelayBefore = 500 },
                            new Define.ScenarioStep { Action = "LoadDone", DelayBefore = 2000 },
                            new Define.ScenarioStep { Action = "InspectDone", DelayBefore = 3000 },
                            new Define.ScenarioStep { Action = "UnloadReady", DelayBefore = 2000 },
                            new Define.ScenarioStep { Action = "UnloadDone", DelayBefore = 2000 }
                        }
                    };
                case "B": // Alarm Scenario
                     return new Define.Scenario
                    {
                        Name = "Scenario B: Alarm Handling",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "AlarmSet", Params = new Dictionary<string, string>{{"id", "1001"}}, DelayBefore = 1000 },
                            new Define.ScenarioStep { Action = "AlarmClear", DelayBefore = 3000 }
                        }
                    };
                case "C": // Unload Scenario (Recipe steps removed)
                     return new Define.Scenario
                    {
                        Name = "Scenario C: Unload Test",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "ExchangeReady", DelayBefore = 1000 },
                            new Define.ScenarioStep { Action = "UnloadReady", DelayBefore = 1000 },
                            new Define.ScenarioStep { Action = "UnloadDone", DelayBefore = 2000 }
                        }
                    };
                case "D": // Unload Glass Data (Formerly E)
                     return new Define.Scenario
                    {
                        Name = "Scenario D: Unload Glass Data (S6F63)",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "UnloadGlassData", DelayBefore = 1000 }
                        }
                    };
                case "E": // Lot End (Formerly D)
                     return new Define.Scenario
                    {
                        Name = "Scenario E: Lot End (S6F65)",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "LotEnd", DelayBefore = 1000 }
                        }
                    };
                case "RECIPE":
                     return new Define.Scenario
                    {
                        Name = "Recipe Test (Create & Change)",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "RecipeCreate", Params = new Dictionary<string, string>{{"id", "TEST_RECIPE_NEW"}}, DelayBefore = 500 },
                            new Define.ScenarioStep { Action = "RecipeChange", Params = new Dictionary<string, string>{{"id", "TEST_RECIPE_CHANGE"}}, DelayBefore = 500 }
                        }
                    };
                case "RECIPE_CREATE":
                     return new Define.Scenario
                    {
                        Name = "Recipe Create Test",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "RecipeCreate", Params = new Dictionary<string, string>{{"id", "TEST_RECIPE_NEW"}}, DelayBefore = 500 }
                        }
                    };
                case "RECIPE_CHANGE":
                     return new Define.Scenario
                    {
                        Name = "Recipe Change Test",
                        Steps = new List<Define.ScenarioStep>
                        {
                            new Define.ScenarioStep { Action = "RecipeChange", Params = new Dictionary<string, string>{{"id", "TEST_RECIPE_CHANGE"}}, DelayBefore = 500 }
                        }
                    };
                default:
                    return null;
            }
        }

        private void Log(string msg)
        {
            // Log to debug output mostly, but could hook into EQPSimulator logs if exposed
            System.Diagnostics.Debug.WriteLine($"[SCENARIO] {msg}");
            
            // Optional: Try to invoke EQP log if possible, but might need reflection or public access
            // _eqpSimulator.OnLog?.Invoke(...) is not directly accessible if we want to be safe, 
            // but we can add a method to EQPSimulator if needed. 
            // For now, Console.WriteLine is safe for simulators.
            Console.WriteLine($"[SCENARIO] {msg}");
        }
    }
}
