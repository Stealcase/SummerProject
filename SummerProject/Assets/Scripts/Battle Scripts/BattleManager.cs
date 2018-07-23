using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour {

    //battleStateMachine switches between battle states
    public StateMachine BattleStateMachine { get; private set; }

    private GameObject enemy;
    public Enemy EnemyScript { get; private set; }

    public SelectedMove SelectedMove;

    //Move variables for passing between phases
    public Move PlayerMove1 { get; set; }
    public Move PlayerMove2 { get; set; }

    //When Battle is entered a MovePhase is started
	public void Awake()
    {
        Debug.Log("BattleManager: Entered Battle");
        BattleStateMachine = new StateMachine();
        BattleStateMachine.ChangeState(new MovePhase(this));
    }

    public void Update()
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
        
        if (EnemyScript.IsDead)
        {
            Debug.Log("VICTORY!!!");
            GameManager.Instance.LoadScene("BasicMovementTest");
            return;
        }

        BattleStateMachine.UpdateState();
    }

    public void Exit()
    {
      
    }

    //Switches to MovePhase
    public void RunMovePhase()
    {
        this.BattleStateMachine.ChangeState(new MovePhase(this));
    }

    //Switches to ResolveMovePhase
    public void RunResolvePhase()
    {
        this.BattleStateMachine.ChangeState(new ResolveMovePhase(this));
    }
}
