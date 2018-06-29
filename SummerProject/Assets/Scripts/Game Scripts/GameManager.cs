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
    private int previousSceneIdx;

    public Vector3 lastPlayerPos;

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

        //TEMPORARY FOR TESTING PURPOSES, DefaultState does nothing atm.
        if (gameStateMachine.CurrentState == null)
        {
            ChangeGameState(new BattleState());
        }

	}
	
	// Update is called once per frame
	void Update () {
        gameStateMachine.UpdateState();

        if (CurrentGameState() != "Battle")
        {
            lastPlayerPos = Player.Instance.transform.position;
        }
	}

    // Other scripts load scenes through the GameManager.
    // Build index of previous scene is stored for reverting.
    public void LoadScene(string sceneName)
    {
        previousSceneIdx = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneName);
    }

    // Revert to previous scene
    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(previousSceneIdx);
    }

    /* Other scripts can tell GM to change states by calling
     * this method and passing it an IState object*/
    public void ChangeGameState(IState newState)
    {
        gameStateMachine.ChangeState(newState);
        Debug.Log("Previous GAME State: " + PreviousGameState());
        Debug.Log("Current GAME State: " + CurrentGameState());
    }

    public void ChangeToPreviousGameState()
    {
        gameStateMachine.ChangeToPreviousState();
        Debug.Log("Previous GAME State: " + PreviousGameState());
        Debug.Log("Current GAME State: " + CurrentGameState());
    }

    // Returns the name of the current game state. (Bad method name)
    public string CurrentGameState()
    {
        return gameStateMachine.LogCurrent();
    }

    // Returns the name of the previous game state. (Bad method name)
    public string PreviousGameState()
    {
        return gameStateMachine.LogPrevious();
    }
}
