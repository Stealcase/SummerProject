using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine {

    private IState currentState;
    private IState previousState;

    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set; }

    //Changes to newState and stores previous
	public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            previousState = currentState;
        }

        currentState = newState;
        currentState.Enter();
    }

    //Changes to newState but storing previous is optional
    public void ChangeState(IState newState, bool storePrevious)
    {
        if (currentState != null)
        {
            currentState.Exit();
            if (storePrevious)
            {
                previousState = currentState;
            }
        }

        currentState = newState;
        currentState.Enter();
    }

    public void ChangeToPreviousState()
    {
        if (previousState != null)
        {
            ChangeState(previousState);
        }
        
    }

    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.Execute();
        }
    }

    public string LogCurrent()
    {
        if (currentState != null)
        {
            return currentState.Log();
        }
        return "None";
    }

    public string LogPrevious()
    {
        if (previousState != null)
        {
            return previousState.Log();
        }
        return "None";
    }
}
