using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveTagDisplay : MonoBehaviour {
    public TextMeshProUGUI textName;
    public Image sprite;

    public MoveTag move;

	// Use this for initialization
	void Start () {
		if (move != null)
        {
            Prime(move);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Prime(MoveTag move)
    {
        this.move = move;
        if (textName != null)
        {
            textName.text = move.displayName;
        }
        if (sprite != null)
        {
            sprite.sprite = move.sprite;
        }

    }
}
