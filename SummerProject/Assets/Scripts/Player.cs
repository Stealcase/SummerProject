using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

	// Use this for initialization
	void Start () {
		if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {

        //Temporary code for testing state change.
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (GameManager.Instance.CurrentGameState() != "Default")
            {
                GameManager.Instance.ChangeGameState(new DefaultState());
            }
            else
            {
                Debug.Log("Already in Default State");
            }
        }
		
	}

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            GameManager.Instance.ChangeGameState(new BattleState());
        }
    }
}
