using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField]
    private int health;
    public int Health { get; set; }

    private bool isDead;
    public bool IsDead { get; set; }

	// Use this for initialization
	void Start () {
        this.Health = health;
        this.IsDead = false;
	}
	
	// Update is called once per frame
	void Update () {

        if (this.Health <= 0)
        {
            this.IsDead = true;
        }

	}

    public void LoseHealth(int dmg)
    {
        Health -= dmg;
    }
}
