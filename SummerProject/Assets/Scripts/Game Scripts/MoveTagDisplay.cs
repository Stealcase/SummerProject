using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveTagDisplay : MonoBehaviour {
    public TextMeshProUGUI textName;
    public Image sprite;

    public MoveTag move;
    private Button button;

	// Use this for initialization
	void Start () {
		if (move != null)
        {
            Prime(move);
        }

        //Null check bc. many missing buttons ATM. Avoid errors.
        //Listener listens for OnClick event and runs method.
        if (GetComponent<Button>() != null)
        {
            this.button = GetComponent<Button>();
            button.onClick.AddListener(delegate { PassMoveIDToMovePhase(); });
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

    //Passes MoveID to MovePhase (Horrible name)
    public void PassMoveIDToMovePhase()
    {
        MovePhase.Instance.CreateMoveFromMoveID(move.moveID);
        Debug.Log("Selected Move: " + move.moveID);
    }

}
