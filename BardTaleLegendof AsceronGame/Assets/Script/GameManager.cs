using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour {
    public static GameManager instance = null;
	[SerializeField] private GameObject[] spawnPoint;
	[SerializeField] private GameObject enemyPrefab;
    private int countPlayerMove = 0;
    private Vector3 currentPlayerPosition = new Vector3();

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

	private void Start() {
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	public void GoToBattle () {
        currentPlayerPosition = PlayerMovement.instance.ReturnPlayerPosition();
        DataManager.instance.playerPosition = currentPlayerPosition;
		SceneManager.LoadScene("BattleScene");
    }

    public void LoadMapScene() {//go to map 
        SceneManager.LoadScene(1);
    }

    public void LoadGameMenu() {//go to game menu
        SceneManager.LoadScene(0);
    }

	private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
		if (scene.name == "MapScene") {
			SpawnEnemy();
			SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

	private void SpawnEnemy () {
		for (int i = 0; i < spawnPoint.Length; i++) {
			int randomPosition = Random.Range(0, spawnPoint.Length - 1);
			//Debug.Log(randomPosition);
			Instantiate(enemyPrefab, spawnPoint[i].transform.position, spawnPoint[i].transform.rotation);
		}
	}

    //public Vector3 ReturnCurrentPlayerPosition() {
    //    return currentPlayerPosition;
    //}
}
