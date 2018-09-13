using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn
{
    Player,
    Enemy,
    None
}

public class ResolveMovePhase : IState {

    private Turn turn;
    private BattleManager bm;

    private Move playerMove1;
    private Move playerMove2;

    private EnemyMove enemyMove;

    private IntVariable playerHP;
    private IntVariable playerCourage;
    private IntVariable playerCharge;
    private IntVariable enemyHP;
    private IntVariable enemyCourage;

    private bool playerDone = false;
    private bool enemyDone = false;

    private bool roar = false;
    private bool run = false;
    private bool block = false;
    private bool identify = false;
    private bool slash;
    private bool atkSuccess = false;
    private bool enemyAttacked = false;

    public ResolveMovePhase(BattleManager battleManager)
    {
        this.bm = battleManager;
    }
    
    public void Enter()
    {
        playerMove1 = bm.PlayerMove1;
        playerMove2 = bm.PlayerMove2;

        playerHP = bm.PlayerHPVar;
        playerCourage = bm.PlayerCourageVar;
        playerCharge = bm.PlayerChargeVar;

        enemyHP = bm.EnemyHPVar;
        enemyCourage = bm.EnemyCourageVar;

        slash = bm.slash;

        //TODO: Add speed check to determine turn order
        Debug.Log("----------RESOLVE PHASE----------");

        if ((playerMove1.Name == "Run" || playerMove2.Name == "Run") || (playerMove1.Name == "Block" || playerMove2.Name == "Block"))
        {
            turn = Turn.Player;
        }
        else
        {
            turn = Turn.Player;
        }
    }

    public void Execute()
    {

        if (turn == Turn.Player)
        {
            Debug.Log("PLAYER TURN");
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

            playerDone = true;
            turn = Turn.Enemy;
        }
        else if (turn == Turn.Enemy)
        {
            Debug.Log("ENEMY TURN");

            //TODO: disabled/paralyzed check here

            //TODO: replace temporary enemy moves
            enemyMove = bm.EnemyMoveQueue.Dequeue();

            Debug.Log("Enemy used " + enemyMove.Name + ": Player HP -" + enemyMove.BaseValue);
            playerHP.Value -= enemyMove.BaseValue;
            Debug.Log("Player HP: " + playerHP.Value);

            if (enemyMove.Type == MoveType.Attack)
                enemyAttacked = true;

            enemyDone = true;
        }

        if (playerDone && enemyDone)
        {
            Debug.Log("PLAYER AND ENEMY TURNS COMPLETED");

            if (identify)
            {
                Debug.Log("Identify: Enemy's next attack is " + bm.EnemyMoveQueue.Peek().Name);
            }

            bm.RunMovePhase();
            return;
        }
    }

    public void Exit()
    {

        bm.PlayerMove1 = null;
        bm.PlayerMove2 = null;
        playerMove1 = null;
        playerMove2 = null;
    }

    public string Log()
    {
        return "Resolve Move Phase";
    }

    public void ExecutePlayerMove(Move move)
    {
        if (move.Name == "Block" || move.Name == "Run")
        {
            Debug.Log(move.Name + " is in effect");
            return;
        }

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
            else if (move.Name == "Block")
            {
                Debug.Log(move.Name + " (Does nothing)");
                block = true;
            }
        }
        else if (move.Target.ToString() == "Enemy")
        {
            if (move.TargetStat.ToString() == "HP")
            {
                if (move.Name == "Slash" && !slash)
                {
                    Debug.Log(move.Name + ": Enemy HP -" + move.TotalValue);
                    bm.slash = true;
                }
                else if (move.Name == "Slash" && slash)
                {
                    Debug.Log("Crazy Slash: Enemy HP -" + move.TotalValue);
                    bm.slash = false;
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
            else if (move.Name == "Identify")
            {
                Debug.Log(move.Name + ": (takes effect after player/enemy turns)" );
                identify = true;
            }
        }
    }
}
