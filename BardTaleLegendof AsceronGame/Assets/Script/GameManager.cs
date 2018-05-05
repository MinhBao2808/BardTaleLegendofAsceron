using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour {
    public static GameManager instance = null;
    private int countPlayerMove = 0;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

	public void CountPlayerMovement () {
        countPlayerMove = countPlayerMove + 1;
        if (countPlayerMove >= Random.Range(10,10000)) {
            SceneManager.LoadScene("BattleScene");
        }
    }

    public void LoadMapScene() {//go to map 
        SceneManager.LoadScene(1);
    }

    public void LoadGameMenu() {//go to game menu
        SceneManager.LoadScene(0);
    }
}
