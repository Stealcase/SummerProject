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

    private IntVariable playerHP;
    private IntVariable playerCourage;
    private IntVariable playerCharge;
    private IntVariable enemyHP;

    private bool roar;
    private bool atkSuccess;

    public ResolveMovePhase(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }
    
    public void Enter()
    {
        playerMove1 = battleManager.PlayerMove1;
        playerMove2 = battleManager.PlayerMove2;

        playerHP = battleManager.PlayerHPVar;
        playerCourage = battleManager.PlayerCourageVar;
        playerCharge = battleManager.PlayerChargeVar;

        enemyHP = battleManager.EnemyHPVar;

        roar = false;
        atkSuccess = false;

        turn = Turn.Player;
        Debug.Log("RESOLVE PHASE");
    }

    /* To test the phases, the move is "resolved" by applying the 
     * damage of both moves to the enemy. When the turn switches, MovePhase
     * is entered again and new moves can be selected. Repeats until enemy is dead.
     */
    public void Execute()
    {

        if (turn == Turn.Player)
        {
            /*enemyHP.Value -= playerMove1.TotalValue;
            Debug.Log(playerMove1.TotalValue);
            enemyHP.Value -= playerMove2.TotalValue;
            Debug.Log(playerMove2.TotalValue);*/

            //Combo/immunity check should happen here
            
            if (playerMove1.Priority > playerMove2.Priority)
            {
                ExecutePlayerMove(playerMove1);
                ExecutePlayerMove(playerMove2);
            }
            else if (playerMove2.Priority > playerMove1.Priority)
            {
                ExecutePlayerMove(playerMove2);
                ExecutePlayerMove(playerMove1);
            }
            
            if (roar)
            {
                if (playerCourage.Value < 0)
                {
                    if (playerMove1.Name == "Roar")
                    {
                        Debug.Log(playerMove1.Name + ": Player Courage +" + playerMove1.TotalValue);
                        playerCourage.Value += playerMove1.TotalValue;
                        Debug.Log("Player Courage: " + playerCourage.Value);

                    }
                    else if (playerMove2.Name == "Roar")
                    {
                        Debug.Log(playerMove2.Name + ": Player Courage +" + playerMove2.TotalValue);
                        playerCourage.Value += playerMove2.TotalValue;
                        Debug.Log("Player Courage: " + playerCourage.Value);
                    }
                }
                else
                {
                    Debug.Log("Roar: Player has no fear");
                    Debug.Log("Player Courage: " + playerCourage.Value);
                }
                roar = false;
            }

            turn = Turn.Enemy;
        }
        else if (turn == Turn.Enemy)
        {
            battleManager.RunMovePhase();
            return;
        }
    }

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

    public void ExecutePlayerMove(Move move)
    {
        if (move.Target.ToString() == "Player")
        {
            if (move.TargetStat.ToString() == "HP")
            {
                Debug.Log(move.Name + ": Player HP +" + move.TotalValue);
                playerHP.Value += move.TotalValue;
                Debug.Log("Player HP: " + playerHP.Value);
            }
            else if (move.Name == "Hero Pose")
            {
                if (atkSuccess)
                {
                    Debug.Log(move.Name + ": Player Courage +" + move.TotalValue);
                    playerCourage.Value += move.TotalValue;
                    Debug.Log("Player Courage: " + playerCourage.Value);
                    atkSuccess = false;
                }
                else
                {
                    Debug.Log("Hero Pose failed, no successful attack");
                }
            }
            else if (move.Name == "Roar")
            {
                Debug.Log(move.Name);
                roar = true;
            }
            else if (move.Name == "Charge Sword")
            {
                Debug.Log(move.Name + ": Player Charge +" + move.TotalValue);
                playerCharge.Value += move.TotalValue;
                Debug.Log("Player Charge: " + playerCharge.Value);
            }
            else if (move.Name == "Run")
            {
                Debug.Log(move.Name + " (Does nothing)");
            }
            else if (move.Name == "Evade")
            {
                Debug.Log(move.Name + " (Does nothing)");
            }
            else if (move.Name == "Identify")
            {
                Debug.Log(move.Name + " (Does nothing)");
            }
            else if (move.Name == "Block")
            {
                Debug.Log(move.Name + " (Does nothing)");
            }
        }
        else if (move.Target.ToString() == "Enemy")
        {
            if (move.TargetStat.ToString() == "HP")
            {
                if (move.Name == "Slash")
                    Debug.Log(move.Name + ": Enemy HP -" + move.TotalValue + " (Not fully implemented)");
                else
                    Debug.Log(move.Name + ": Enemy HP -" + move.TotalValue);

                enemyHP.Value -= move.TotalValue;
                Debug.Log("Enemy HP: " + enemyHP.Value);
                atkSuccess = true;
            }
            else if (move.Name == "Declare Amazingness")
            {
                Debug.Log(move.Name + " (Does nothing)");
            }
        }
    }
}
