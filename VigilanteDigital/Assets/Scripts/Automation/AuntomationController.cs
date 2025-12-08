using System.Collections.Generic;
using UnityEngine;

public class AutomationController : MonoBehaviour
{
    public List<AutomationRule> rules;
    public List<MonoBehaviour> devices; // qualquer dispositivo que implemente IAutomatable

    void Update()
    {
        foreach (var rule in rules)
        {
            foreach (var device in devices)
            {
                if (device is IAutomatable automatable)
                {
                    if (automatable.CheckCondition(rule.condition))
                    {
                        automatable.ExecuteAction(rule.action);
                    }
                }
            }
        }
    }
}

