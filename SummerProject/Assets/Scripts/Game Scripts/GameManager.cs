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

	}
	
	// Update is called once per frame
	void Update () {
         
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
}
