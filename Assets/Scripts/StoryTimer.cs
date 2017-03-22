using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StoryTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Invoke("startGameScene", 5);
  }

  void startGameScene () {
    SceneManager.LoadScene("Game");
  }

}