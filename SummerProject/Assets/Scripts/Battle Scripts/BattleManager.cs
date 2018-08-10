using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    //battleStateMachine switches between battle states
    public StateMachine BattleStateMachine { get; private set; }

    public IntVariable PlayerHPVar;
    public IntVariable PlayerCourageVar;
    public IntVariable PlayerChargeVar;
    public IntVariable EnemyHPVar;
    public IntVariable EnemyCourageVar;

    public SelectedMove SelectedMoveVar;

    //Move variables for passing between phases
    public Move PlayerMove1 { get; set; }
    public Move PlayerMove2 { get; set; }

    public IntVariable PrevSceneIndexVar;

    [Tooltip("If specified, this scene is loaded upon battle exit instead of the previous scene")]
    public string NextSceneName;

    //Bools for special cases
    public bool slash;

    //When Battle is entered a MovePhase is started
    public void Awake()
    {
        Debug.Log("BattleManager: Entered Battle");
        BattleStateMachine = new StateMachine();
        BattleStateMachine.ChangeState(new MovePhase(this));

        slash = false;
    }

    public void Update()
    {
        if (EnemyHPVar.Value <= 0)
        {
            Debug.Log("VICTORY!!!");
            if (PrevSceneIndexVar != null && NextSceneName == "")
            {
                SceneManager.LoadScene(PrevSceneIndexVar.Value);
                return;
            }
            else if (NextSceneName != "")
            {
                SceneManager.LoadScene(NextSceneName);
                return;
            }

            Debug.Log("BattleManager: Previous Scene Index reference missing!");
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
        BattleStateMachine.ChangeState(new MovePhase(this));
    }

    //Switches to ResolveMovePhase
    public void RunResolvePhase()
    {
        BattleStateMachine.ChangeState(new ResolveMovePhase(this));
    }
}
