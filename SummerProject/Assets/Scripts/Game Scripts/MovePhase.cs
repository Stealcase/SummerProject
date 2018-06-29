using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : IState {

    private IMove playerMove_1;
    private IMove playerMove_2;

    private BattleState battleState;

    //Constructor takes in parent BattleState reference
    public MovePhase(BattleState battleState)
    {
        this.battleState = battleState;
    }

    public void Enter()
    {

    }

    public void Execute()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            playerMove_1 = new JumpStrike();
        }
    }

    public void Exit()
    {

    }

    public string Log()
    {
        return "Move Phase";
    }
	
}
