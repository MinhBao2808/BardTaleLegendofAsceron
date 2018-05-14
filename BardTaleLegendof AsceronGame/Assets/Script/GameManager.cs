using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour {
    public static GameManager instance = null;
	[SerializeField] private GameObject enemyPrefab;
	//[SerializeField] private AudioClip battleMusic;
	[SerializeField] private GameObject battleMusic;
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
		//battleMusic.SetActive(true);
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
			//SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

	private void SpawnEnemy () {
		GameObject[] spawnPointObject = GameObject.FindGameObjectsWithTag("SpawnPoint");
		for (int i = 0; i < spawnPointObject.Length; i++) {
			//Debug.Log(randomPosition);
			var enemySpawn = Instantiate(enemyPrefab, spawnPointObject[i].transform.position, spawnPointObject[i].transform.rotation);
		}
	}

	public void PlayerGoToNewMap() {
		DataManager.instance.listEnemyDefeatedPosition.Clear();
	}

    //public Vector3 ReturnCurrentPlayerPosition() {
    //    return currentPlayerPosition;
    //}
}
