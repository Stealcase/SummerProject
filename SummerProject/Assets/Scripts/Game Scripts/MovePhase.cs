using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : IState {

    private IMove playerMove1;
    private IMove playerMove2;

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
            playerMove1 = new JumpStrike();
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
