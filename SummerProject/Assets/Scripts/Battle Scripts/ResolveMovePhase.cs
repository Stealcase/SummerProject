using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn
{
    Player,
    Enemy
}

public class ResolveMovePhase : IState {

    private Turn turn;
    private BattleManager battleState;

    private Enemy enemyScript;

    private Move playerMove1;
    private Move playerMove2;

    public ResolveMovePhase(BattleManager battleState)
    {
        this.battleState = battleState;
    }
    
    public void Enter()
    {
        this.playerMove1 = battleState.PlayerMove1;
        this.playerMove2 = battleState.PlayerMove2;
        this.enemyScript = battleState.EnemyScript;
        turn = Turn.Player;
        Debug.Log("Entered Resolve Phase");
    }

    /* To test the phases, the move is "resolved" by simply applying the 
     * damage of both moves to the enemy. When the turn switches, MovePhase
     * is entered again and new moves can be selected. Repeats until enemy is dead.
     */
    public void Execute()
    {

        if (turn == Turn.Player)
        {
            battleState.EnemyScript.LoseHealth(playerMove1.TotalValue);
            battleState.EnemyScript.LoseHealth(playerMove2.TotalValue);
            turn = Turn.Enemy;
        }
        else if (turn == Turn.Enemy)
        {
            battleState.RunMovePhase();
            return;
        }
    }

    //Reset for good measure.
    public void Exit()
    {
        battleState.PlayerMove1 = null;
        battleState.PlayerMove2 = null;
        playerMove1 = null;
        playerMove2 = null;
    }

    public string Log()
    {
        return "Resolve Move Phase";
    }
}
