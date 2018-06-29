using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleState : IState {

    //battleStateMachine switches between battle phases
    private StateMachine battleStateMachine;
    public StateMachine BattleStateMachine { get; private set; }

    private GameObject enemy;

    private Enemy enemyScript;
    public Enemy EnemyScript { get; private set; }

    private IMove playerMove1;
    public IMove PlayerMove1 { get; set; }

    private IMove playerMove2;
    public IMove PlayerMove2 { get; set; }

    //When BattleState is entered a MovePhase is started
	public void Enter()
    {
        GameManager.Instance.LoadScene("BattleScenePlaceholder");
        battleStateMachine = new StateMachine();
        battleStateMachine.ChangeState(new MovePhase(this));
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
            EnemyScript = enemy.GetComponent<Enemy>();
        }
        
        //When enemy is dead revert game state to default (ignore the "false" parameter, does nothing)
        if (EnemyScript.IsDead)
        {
            Debug.Log("VICTORY!!!");
            GameManager.Instance.ChangeGameState(new DefaultState(false));
            return;
        }

        battleStateMachine.UpdateState();
    }

    public void Exit()
    {
        GameManager.Instance.LoadScene("BasicMovementTest");
        //GameManager.Instance.LoadPreviousScene();
        //Player.Instance.transform.position = GameManager.Instance.lastPlayerPos;
    }

    //Switches to MovePhase
    public void RunMovePhase()
    {
        this.battleStateMachine.ChangeState(new MovePhase(this));
    }

    //Switches to ResolveMovePhase
    public void RunResolvePhase()
    {
        this.battleStateMachine.ChangeState(new ResolveMovePhase(this));
    }

    public string Log()
    {
        return "Battle";
    }
}
