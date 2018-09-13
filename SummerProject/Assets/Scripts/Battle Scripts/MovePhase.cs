using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEditor;


public class MovePhase : IState {

    private Move playerMove1;
    private Move playerMove2;
    private SelectedMove selectedMoveRef;
    private Move selectedMove;

    private EnemyMoveSelector enemyMoveSelector;
    private EnemyMove enemySelectedMove;

    private BattleManager bm;

    // private XmlReader xml;

    /* MovePhase is always created by the RunMovePhase() method in BattleState,
     * BattleState passes MovePhase a reference to itself when this happens. Therefore
     * the MovePhase constructor takes in a BattleState object.
     */
    public MovePhase(BattleManager battleManager)
    {
        this.bm = battleManager;
    }

    public void Enter()
    {
        selectedMoveRef = bm.SelectedMoveVar;
        selectedMoveRef.ClearMove();

        enemyMoveSelector = bm.EnemyMoveSelector;

        Debug.Log("----------MOVE PHASE----------");
    }

    public void Execute()
    {

        while (bm.EnemyMoveQueue.Count < 2)
        {
            enemySelectedMove = enemyMoveSelector.SelectMove();
            bm.EnemyMoveQueue.Enqueue(enemySelectedMove);
            Debug.Log("Queued " + enemySelectedMove.Name + " in Enemy Move Queue");
        }

        if (this.selectedMoveRef != null)
        {
            selectedMove = selectedMoveRef.move;
            selectedMoveRef.ClearMove();
        }

        if (selectedMove != null)
        {

            if (playerMove1 == null && playerMove2 == null)
            {
                playerMove1 = selectedMove;
                Debug.Log(playerMove1.Name + " is a " + playerMove1.Type.ToString());
            }
            else if (playerMove1 != null && playerMove2 == null)
            {
                if (selectedMove.Type == playerMove1.Type)
                {
                    playerMove1 = selectedMove;
                    Debug.Log("You reselected a " + playerMove1.Type.ToString() + " move");
                }
                else
                {
                    playerMove2 = selectedMove;
                    Debug.Log(playerMove2.Name + " is a " + playerMove2.Type.ToString() + "");
                }
            }
        }

        //If both moves have been selected, then ResolveMovePhase.
        if (playerMove1 != null && playerMove2 != null)
        {
            Debug.Log("You selected " + playerMove1.Name + " and " + playerMove2.Name);
            bm.RunResolvePhase();
            return;
        }
    }

    //On exit, pass selected moves to BattleState and reset.
    public void Exit()
    {
        bm.PlayerMove1 = playerMove1;
        bm.PlayerMove2 = playerMove2;
    }


    public string Log()
    {
        return "Move Phase";
    }
	
}
