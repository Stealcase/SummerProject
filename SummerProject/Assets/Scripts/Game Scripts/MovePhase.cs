﻿using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEditor;


public class MovePhase : IState {

    private Move playerMove1;
    private Move playerMove2;
    private SelectedMove selectedMoveRef;
    private Move selectedMove;

    private BattleState battleState;

    // private XmlReader xml;

    /* MovePhase is always created by the RunMovePhase() method in BattleState,
     * BattleState passes MovePhase a reference to itself when this happens. Therefore
     * the MovePhase constructor takes in a BattleState object.
     */
    public MovePhase(BattleState battleState)
    {
        this.battleState = battleState;
    }

    public void Enter()
    {
        Debug.Log("Entered Move Phase");
        selectedMoveRef = (SelectedMove)AssetDatabase.LoadAssetAtPath("Assets/Scripts/Utility/Test/SelectedMove.asset", typeof(SelectedMove));
        selectedMoveRef.ClearMove();
        // xml = XmlReader.Create(@"C:\Users\Martin\Documents\GitHub\SummerProject\SummerProject\Assets\Scripts\MoveData\MoveData.xml");
    }

    public void Execute()
    {
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
                Debug.Log(playerMove1.Name + " is a " + playerMove1.Type.ToString() + " bruh");
            }
            else if (playerMove1 != null && playerMove2 == null)
            {
                if (selectedMove.Type == playerMove1.Type)
                {
                    playerMove1 = selectedMove;
                    Debug.Log("You reselected a " + playerMove1.Type.ToString() + " move bruh");
                }
                else
                {
                    playerMove2 = selectedMove;
                    Debug.Log(playerMove2.Name + " is a " + playerMove2.Type.ToString() + " bruh");
                }
            }
        }

        //If both moves have been selected, then ResolveMovePhase.
        if (playerMove1 != null && playerMove2 != null)
        {
            //battleState.RunResolvePhase();
            Debug.Log("You selected " + playerMove1.Name + " and " + playerMove2.Name + " bruh");
            return;
        }
    }

    //On exit, pass selected moves to BattleState and reset.
    public void Exit()
    {

    }


    public string Log()
    {
        return "Move Phase";
    }
	
}
