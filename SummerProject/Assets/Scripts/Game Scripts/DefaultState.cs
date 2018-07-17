using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : IState {

    private bool spawnEnemy;
    
    public DefaultState()
    {
        this.spawnEnemy = true;
    }

    public DefaultState(bool spawnEnemy)
    {
        this.spawnEnemy = spawnEnemy;
    }

	public void Enter()
    {
        if (!spawnEnemy)
        {
            GameObject.FindWithTag("Enemy").gameObject.SetActive(false);
        }
    }

    public void Execute()
    {

    }

    public void Exit()
    {

    }

    public string Log()
    {
        return "Default";
    }
}
