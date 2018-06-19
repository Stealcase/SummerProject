using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultState : IState {

	public void Enter()
    {
        GameManager.Instance.ChangeScene("BasicMovementTest");
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
