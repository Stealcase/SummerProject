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

    private Move playerAtk;
    private Move playerMod;

    private EnemyMove enemyMove;

    private IntVariable playerHP;
    private IntVariable playerMaxHP;
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
        if (bm.PlayerMove1.Type == MoveType.Attack)
        {
            playerAtk = bm.PlayerMove1;
            playerMod = bm.PlayerMove2;
        }
        else
        {
            playerAtk = bm.PlayerMove2;
            playerMod = bm.PlayerMove1;
        }

        playerHP = bm.PlayerHPVar;
        playerMaxHP = bm.PlayerMaxHPVar;
        playerCourage = bm.PlayerCourageVar;
        playerCharge = bm.PlayerChargeVar;

        enemyHP = bm.EnemyHPVar;
        enemyCourage = bm.EnemyCourageVar;

        slash = bm.slash;

        Debug.Log("----------RESOLVE PHASE----------");

        if (playerMod.Name == "Run")
        {
            turn = Turn.Player;
        }
        else if (playerMod.Name == "Block")
        {
            turn = Turn.Player;
            block = true;
        }
        else
        {
            //TODO: Add speed check to determine turn order
            turn = Turn.Player;
        }
    }

    public void Execute()
    {

        if (turn == Turn.Player)
        {
            Debug.Log("PLAYER TURN");
            //Combo/immunity check should happen here
            
            if (playerAtk.Priority > playerMod.Priority && !block)
            {
                ExecutePlayerAtk(playerAtk);
                ExecutePlayerMod(playerMod);
            }
            else if (playerMod.Priority > playerAtk.Priority && !block)
            {
                ExecutePlayerMod(playerMod);
                ExecutePlayerAtk(playerAtk);
            }
            else if (block)
            {
                ExecutePlayerMod(playerMod);
            }
            
            //TODO: Fix Roar
            if (roar)
            {
                if (playerCourage.Value < 0)
                {
                    if (playerMod.Name == "Roar")
                    {
                        Debug.Log(playerMod.Name + ": Player Courage +" + playerMod.TotalValue);
                        playerCourage.Value += playerMod.TotalValue;
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

            if (block && enemyMove.Type == MoveType.Attack)
            {
                Debug.Log("Enemy used " + enemyMove.Name + " for " + enemyMove.BaseValue + " Damage");
                Debug.Log("Player blocks " + playerMod.TotalValue + " Damage");

                int damage;
                damage = enemyMove.BaseValue - playerMod.TotalValue;

                if (damage > 0)
                {
                    Debug.Log("Final Damage: " + damage);
                    playerHP.Value -= damage;
                }
                else if (damage <= 0)
                {
                    Debug.Log("Final Damage: " + 0);
                }

                Debug.Log("Player HP: " + playerHP.Value);

                enemyAttacked = true;
            }
            else if (enemyMove.Type == MoveType.Attack)
            {
                Debug.Log("Enemy used " + enemyMove.Name + ": Player HP -" + enemyMove.BaseValue);
                playerHP.Value -= enemyMove.BaseValue;
                Debug.Log("Player HP: " + playerHP.Value);
            }
            else if (enemyMove.Type == MoveType.Modifier)
            {
                Debug.Log("Enemy used " + enemyMove.Name + " (does nothing)");
            }

            enemyDone = true;
        }

        if (playerDone && enemyDone)
        {
            Debug.Log("PLAYER AND ENEMY TURNS COMPLETED");

            if (identify)
            {
                Debug.Log("Identify: Enemy's next attack is " + bm.EnemyMoveQueue.Peek().Name);
            }
            else if (block)
            {
                if (enemyAttacked)
                    ExecutePlayerAtk(playerAtk);
                else
                    Debug.Log("Block and " + playerAtk.Name + " failed, enemy did not attack");
            }

            bm.RunMovePhase();
            return;
        }
    }

    public void Exit()
    {
        bm.PlayerMove1 = null;
        bm.PlayerMove2 = null;
        playerAtk = null;
        playerMod = null;
    }

    public string Log()
    {
        return "Resolve Move Phase";
    }

    public void ExecutePlayerAtk(Move atkMove)
    {
        Debug.Log("Executing Player Attack Move");

        if (atkMove.Target.ToString() == "Player")
        {
            if (atkMove.TargetStat.ToString() == "HP")
            {
                if (!((playerHP.Value + atkMove.TotalValue) > playerMaxHP.Value))
                {
                    Debug.Log(atkMove.Name + ": Player HP +" + atkMove.TotalValue);
                    playerHP.Value += atkMove.TotalValue;
                    Debug.Log("Player HP: " + playerHP.Value);
                }
                else
                {
                    playerHP.Value = playerMaxHP.Value;
                    Debug.Log("Player HP: " + playerHP.Value + " (Max)");
                }
            }
        }
        else if (atkMove.Target.ToString() == "Enemy")
        {
            if (atkMove.TargetStat.ToString() == "HP")
            {
                if (atkMove.Name == "Slash" && !slash)
                {
                    Debug.Log(atkMove.Name + ": Enemy HP -" + atkMove.TotalValue);
                    bm.slash = true;
                }
                else if (atkMove.Name == "Slash" && slash)
                {
                    Debug.Log("Crazy Slash: Enemy HP -" + atkMove.TotalValue);
                    bm.slash = false;
                }
                else
                    Debug.Log(atkMove.Name + ": Enemy HP -" + atkMove.TotalValue);

                enemyHP.Value -= atkMove.TotalValue;
                Debug.Log("Enemy HP: " + enemyHP.Value);
                atkSuccess = true;
            }
            else if (atkMove.Name == "Declare Amazingness")
            {
                Debug.Log(atkMove.Name + ": Enemy Courage -" + atkMove.TotalValue);
                enemyCourage.Value -= atkMove.TotalValue;
                Debug.Log("Enemy Courage: " + enemyCourage.Value);
            }
        }        
    }

    public void ExecutePlayerMod(Move modMove)
    {
        Debug.Log("Executing Player Modifier Move");

        if (modMove.Name == "Run")
        {
            Debug.Log(modMove.Name + " is in effect");
            return;
        }

        if (modMove.Target.ToString() == "Player")
        {
            if (modMove.Name == "Hero Pose")
            {
                if (atkSuccess)
                {
                    Debug.Log(modMove.Name + ": Player Courage +" + modMove.TotalValue);
                    playerCourage.Value += modMove.TotalValue;
                    Debug.Log("Player Courage: " + playerCourage.Value);
                    atkSuccess = false;
                }
                else
                {
                    Debug.Log("Hero Pose failed, no successful attack");
                }
            }
            else if (modMove.Name == "Roar")
            {
                Debug.Log(modMove.Name);
                roar = true;
            }
            else if (modMove.Name == "Charge Sword")
            {
                Debug.Log(modMove.Name + ": Player Charge +" + modMove.TotalValue);
                playerCharge.Value += modMove.TotalValue;
                Debug.Log("Player Charge: " + playerCharge.Value);
            }
            else if (modMove.Name == "Run")
            {
                Debug.Log(modMove.Name + " (Does nothing)");
                run = true;
            }
            else if (modMove.Name == "Evade")
            {
                Debug.Log(modMove.Name + " (Does nothing)");
            }
            else if (modMove.Name == "Block")
            {
                Debug.Log(modMove.Name + ": Block " + modMove.TotalValue + " damage (Attack happens after enemy attack)");
                Debug.Log("Player HP: " + playerHP.Value + " (+" + modMove.TotalValue + ")");
            }
        }
        else if (modMove.Target.ToString() == "Enemy")
        {
            if (modMove.Name == "Identify")
            {
                Debug.Log(modMove.Name + ": (takes effect after player/enemy turns)");
                identify = true;
            }
        }
    }

    //Old method for executing moves
    /*public void ExecutePlayerMove(Move move)
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
                Debug.Log(move.Name + ": Player can block " + move.TotalValue + " damage");
                playerHP.Value += move.TotalValue;
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
                Debug.Log(move.Name + ": (takes effect after player/enemy turns)");
                identify = true;
            }
        }
    }*/
}
