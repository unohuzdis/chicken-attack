using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Level1Timer : MonoBehaviour {

	// Use this for initialization
	void Start () {
    Invoke("startGameScene", 25);
  }

  void startGameScene () {
    SceneManager.LoadScene("StoryP2");
  }

}