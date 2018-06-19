using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : IState {

	public void Enter()
    {
        GameManager.Instance.ChangeScene("BattleScenePlaceholder");
    }

    public void Execute()
    {
        
    }

    public void Exit()
    {

    }

    public string Log()
    {
        return "Battle";
    }
}
