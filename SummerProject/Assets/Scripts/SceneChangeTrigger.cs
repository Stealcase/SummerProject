using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour {

    [SerializeField]
    private string targetTag;

    [SerializeField]
    private string nextSceneName;

    public IntVariable PrevSceneIndexVar;

	// Use this for initialization
	void Start () {
        if (targetTag == "")
            targetTag = "Player";
	}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == targetTag)
        {
            if (nextSceneName != "")
            {
                PrevSceneIndexVar.Value = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }
}
