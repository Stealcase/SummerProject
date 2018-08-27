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

    private EnemyMoveSelector EMoveSelector;

    private EnemyMove enemyMove1;

    private IntVariable playerHP;
    private IntVariable playerCourage;
    private IntVariable playerCharge;
    private IntVariable enemyHP;
    private IntVariable enemyCourage;

    private bool roar = false;
    private bool run = false;
    private bool slash;
    private bool atkSuccess = false;

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
        enemyCourage = battleManager.EnemyCourageVar;

        slash = battleManager.slash;

        EMoveSelector = battleManager.EMoveSelector;

        turn = Turn.Player;
        Debug.Log("RESOLVE PHASE");
    }

    public void Execute()
    {

        //Speed check here

        if (turn == Turn.Player)
        {

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

            EMoveSelector.SelectMoves();
            enemyMove1 = EMoveSelector.selectedMove;
            Debug.Log("Enemy used " + enemyMove1.Name);

            playerHP.Value -= enemyMove1.BaseValue;
            Debug.Log("Player HP: " + playerHP.Value);

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
                run = true;
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
                if (move.Name == "Slash" && !slash)
                {
                    Debug.Log(move.Name + ": Enemy HP -" + move.TotalValue + " (Not fully implemented)");
                    battleManager.slash = true;
                }
                else if (move.Name == "Slash" && slash)
                {
                    Debug.Log("Crazy Slash: Enemy HP -" + move.TotalValue);
                    battleManager.slash = false;
                }
                else
                    Debug.Log(move.Name + ": Enemy HP -" + move.TotalValue);

                enemyHP.Value -= move.TotalValue;
                Debug.Log("Enemy HP: " + enemyHP.Value);
                atkSuccess = true;
            }
            else if (move.Name == "Declare Amazingness")
            {
                Debug.Log(move.Name + ": Enemy Courage -" + move.TotalValue);
                enemyCourage.Value -= move.TotalValue;
                Debug.Log("Enemy Courage: " + enemyCourage.Value);
            }
        }
    }
}
