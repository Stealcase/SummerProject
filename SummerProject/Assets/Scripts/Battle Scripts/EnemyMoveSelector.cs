using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMoveSelector : MonoBehaviour {

    public EnemyMove selectedMove;

    public EnemyMoveList enemy1_moves;

	// Use this for initialization
	void Start () {
		
	}
	
    public void SelectMoves()
    {
        System.Random r = new System.Random();
        int rnd = r.Next(0, 2);

        if (rnd == 0)
        {
            selectedMove = enemy1_moves.move1;
        }
        else if (rnd == 1)
        {
            selectedMove = enemy1_moves.move2;
        }
    }

}
