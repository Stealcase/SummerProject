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
    private BattleManager battleManager;

    private Move playerMove1;
    private Move playerMove2;

    private int playerHP;
    private IntVariable enemyHP;

    public ResolveMovePhase(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }
    
    public void Enter()
    {
        playerMove1 = battleManager.PlayerMove1;
        playerMove2 = battleManager.PlayerMove2;
        enemyHP = battleManager.EnemyHPVar;
        
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
            enemyHP.Value -= playerMove1.TotalValue;
            Debug.Log(playerMove1.TotalValue);
            enemyHP.Value -= playerMove2.TotalValue;
            Debug.Log(playerMove2.TotalValue);
            turn = Turn.Enemy;
        }
        else if (turn == Turn.Enemy)
        {
            battleManager.RunMovePhase();
            return;
        }
    }

    //Reset for good measure.
    public void Exit()
    {
        battleManager.PlayerMove1 = null;
        battleManager.PlayerMove2 = null;
        playerMove1 = null;
        playerMove2 = null;
    }

    public string Log()
    {
        return "Resolve Move Phase";
    }
}
