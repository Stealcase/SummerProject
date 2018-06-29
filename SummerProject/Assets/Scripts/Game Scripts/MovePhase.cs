using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePhase : IState {

    private IMove playerMove1;
    private IMove playerMove2;

    private BattleState battleState;

    /* MovePhase must be accessible by MoveTagDisplay, easiest solution was
     * to make MovePhase a singleton. The Instance is static (hence globally
     * accessible) and there should always be 1 MovePhase anyway (singleton is ok)
     * Because singleton, variables must be explicitly set to null to be "erased" 
     * (maybe, not sure)
     */
    public static MovePhase Instance;

    /* MovePhase is always created by the RunMovePhase() method in BattleState,
     * BattleState passes a reference to itself when this happens. Therefore
     * the MovePhase constructor takes in a BattleState object.
     */
    public MovePhase(BattleState battleState)
    {
        Instance = this;
        this.battleState = battleState;
    }

    public void Enter()
    {
        Debug.Log("Entered Move Phase");
        playerMove1 = null;
        playerMove2 = null;
    }

    public void Execute()
    {
        //If both moves have been selected, then ResolveMovePhase.
        if (playerMove1 != null && playerMove2 != null)
        {
            battleState.RunResolvePhase();
            return;
        }
    }

    //On exit, pass selected moves to BattleState and reset.
    public void Exit()
    {
        battleState.PlayerMove1 = playerMove1;
        battleState.PlayerMove2 = playerMove2;
        playerMove1 = null;
        playerMove2 = null;
    }

    //Sets moves in order. Each move can be any move.
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

    //Checks MoveID and creates the corresponding move (one for now)
    public void CreateMoveFromMoveID(MoveID moveID)
    {
        if (moveID == MoveID.JumpStrike)
        {
            SetMove(new JumpStrike());
        }
    }

    public string Log()
    {
        return "Move Phase";
    }
	
}
