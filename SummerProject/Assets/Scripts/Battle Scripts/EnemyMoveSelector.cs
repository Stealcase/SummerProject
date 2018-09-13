using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyMoveSelector : MonoBehaviour {

    public EnemyMoveList enemy1_moves;

	// Use this for initialization
	void Start () {
		
	}
	
    public EnemyMove SelectMove()
    {
        System.Random r = new System.Random();
        int rnd = r.Next(0, 2);

        if (rnd == 0)
        {
            return enemy1_moves.move1;

        }
        else if (rnd == 1)
        {
            return enemy1_moves.move2;
        }

        return null;
    }

}
