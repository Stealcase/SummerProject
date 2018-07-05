using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveTagDisplay : MonoBehaviour {
    public TextMeshProUGUI textName;
    public Image sprite;

    public Move move;
    public SelectedMove selectedMove;

    private Button button;

	// Use this for initialization
	void Start () {
		if (move != null)
        {
            Prime();
        }

        //Null check bc. many missing buttons ATM. Avoid errors.
        //Listener listens for OnClick event and runs method.
        if (GetComponent<Button>() != null)
        {
            this.button = GetComponent<Button>();
            button.onClick.AddListener(delegate { selectedMove.SetMove(move); });
        }
        
	}
	
	// Update is called once per frame
	void Update () {
		 
	}

    public void Prime()
    {
        if (textName != null)
        {
            textName.text = move.Name;
        }
        if (sprite != null)
        {
            sprite.sprite = move.Sprite;
        }
    }

}
