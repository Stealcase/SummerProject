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
        
    }

    public void Exit()
    {

    }

    public void SetMove(IMove move)
    {
        if (playerMove1 == null && playerMove2 == null)
        {
            playerMove1 = move;
        }
        else if (playerMove1 != null && playerMove2 == null)
        {
            playerMove2 = move;
        }
    }

    public string Log()
    {
        return "Move Phase";
    }
	
}
