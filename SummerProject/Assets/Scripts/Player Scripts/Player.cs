using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public static Player Instance;

    [SerializeField]
    private int health;
    public int Health { get; set; }

    [SerializeField]
    private int will;
    public int Will { get; set; }

    [SerializeField]
    private int physical;
    public int Physical { get; set; }

    [SerializeField]
    private int weapon;
    public int Weapon { get; set; }

    [SerializeField]
    private int speed;
    public int Speed { get; set; }

	// Use this for initialization
	void Start () {
		if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this.gameObject);

        this.Health = health;
        this.Will = will;
        this.Physical = physical;
        this.Weapon = weapon;
        this.Speed = speed;
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

    public int GetStat(MoveStat type)
    {
        if (type == MoveStat.Physical)
            return Physical;
        if (type == MoveStat.Weapon)
            return Weapon;
        if (type == MoveStat.Will)
            return Will;

        return 0;
    }
}
