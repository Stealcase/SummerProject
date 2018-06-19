using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/* The GameManager class is a Singleton, meaning there can only be 
 * one instance of it at any time. This is for data preservation
 * between scenes. Alternative: Make GameManager a static class. 
 * The singleton approach is the most "Unity friendly" way of doing it.*/

public class GameManager : MonoBehaviour {

    // Static variable for GameManager to instantiate itself.
    public static GameManager Instance;
    public StateMachine gameStateMachine;

	// Use this for initialization
	void Start () {
		
        /* If another GameManager exists, destroy yourself */
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        
        DontDestroyOnLoad(this.gameObject);

        gameStateMachine = new StateMachine();

	}
	
	// Update is called once per frame
	void Update () {
        gameStateMachine.UpdateState();
	}

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ChangeGameState(IState newState)
    {
        gameStateMachine.ChangeState(newState);
        Debug.Log("Previous State: " + gameStateMachine.LogPrevious());
        Debug.Log("Current State: " + gameStateMachine.LogCurrent());
    }

    public string CurrentGameState()
    {
        return gameStateMachine.LogCurrent();
    }
}
