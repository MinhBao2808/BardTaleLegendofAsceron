using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartBattle : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        this.gameObject.SetActive(true);
	}
	
    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        if (scene.name == "GameMenu") {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
        else {
            this.gameObject.SetActive(scene.name == "BattleScene");
        }
    }
}
