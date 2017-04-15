using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level2Timer : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Invoke("startGameScene", 30);
  }

  void startGameScene () {
    SceneManager.LoadScene("StoryP3");
  }

}