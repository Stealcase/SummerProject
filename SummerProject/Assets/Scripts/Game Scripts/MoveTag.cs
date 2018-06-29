using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MoveID is used to identify which move is selected from GUI.
//Set MoveID for a MoveTag via. dropdown in inspector.
public enum MoveID
{
    JumpStrike,
    Run
}

public class MoveTag : MonoBehaviour {
    public string displayName;
    [Multiline]
    public string desc;
    public Sprite sprite;
    public MoveID moveID;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
