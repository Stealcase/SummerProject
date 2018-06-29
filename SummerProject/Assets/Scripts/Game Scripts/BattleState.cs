using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turn
{
    Player,
    Enemy
}

public class BattleState : IState {

    private Turn turn { get; set; }
    private GameObject enemy;
    private Enemy enemyScript;


	public void Enter()
    {
        GameManager.Instance.LoadScene("BattleScenePlaceholder");
        turn = Turn.Player;
    }

    public void Execute()
    {
        /* For testing: An the enemy prefab is pre-placed into the battlescene, so 
        *  FindWithTag is used to acquire a reference to the enemy GameObject. Logical 
        *  thing is to tell BattleState what enemy to load based on which one the Player 
        *  collided with.
        */
        if (enemy == null)
        {
            enemy = GameObject.FindWithTag("Enemy");
            enemyScript = enemy.GetComponent<Enemy>();
        }
        
        if (enemyScript.IsDead)
        {
            Debug.Log("VICTORY!!!");
            GameManager.Instance.ChangeGameState(new DefaultState(false));
            return;
        }

        /* The final battle system will have to be split into phases. The following 
         * part will probably be split into multiple scripts when the system becomes 
         * more complex.
         */

        /* For now: Moves are hard-coded in the BattleState, no information about
         * moves or their base damage anywhere else. The stats are fetched from the
         * Player script and added onto the base damages.
         */
        switch (turn)
        {
            case Turn.Player:
                int damage = 0;
                if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    Debug.Log("JUMP STRIKE");
                    damage = 3 + Player.Instance.Weapon;
                    enemyScript.LoseHealth(damage);

                    Debug.Log("Damage: " + damage);
                    Debug.Log("Enemy Health: " + enemyScript.Health);
                    turn = Turn.Enemy;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    Debug.Log("FOOT SWIPE");
                    damage = 2 + Player.Instance.Physical;
                    enemyScript.LoseHealth(damage);

                    Debug.Log("Damage: " + damage);
                    Debug.Log("Enemy Health: " + enemyScript.Health);
                    turn = Turn.Enemy;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha3))
                {
                    Debug.Log("SWORD STAB");
                    damage = 4 + Player.Instance.Weapon;
                    enemyScript.LoseHealth(damage);

                    Debug.Log("Damage: " + damage);
                    Debug.Log("Enemy Health: " + enemyScript.Health);
                    turn = Turn.Enemy;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha4))
                {
                    Debug.Log("SLASH");
                    damage = 2 + Player.Instance.Weapon;
                    enemyScript.LoseHealth(damage);

                    Debug.Log("Damage: " + damage);
                    Debug.Log("Enemy Health: " + enemyScript.Health);
                    turn = Turn.Enemy;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha5))
                {
                    Debug.Log("PUNCH");
                    damage = 3 + Player.Instance.Physical;
                    enemyScript.LoseHealth(damage);

                    Debug.Log("Damage: " + damage);
                    Debug.Log("Enemy Health: " + enemyScript.Health);
                    turn = Turn.Enemy;
                }
                break;
            case Turn.Enemy:
                turn = Turn.Player;
                break;
        }
    }

    public void Exit()
    {
        GameManager.Instance.LoadScene("BasicMovementTest");
        //GameManager.Instance.LoadPreviousScene();
        //Player.Instance.transform.position = GameManager.Instance.lastPlayerPos;
    }

    public string Log()
    {
        return "Battle";
    }
}
