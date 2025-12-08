using UnityEngine;


    public interface IAutomatable
    {
        void ExecuteAction(string action);
        bool CheckCondition(string condition);
        float EnergyCost { get; }
    }

