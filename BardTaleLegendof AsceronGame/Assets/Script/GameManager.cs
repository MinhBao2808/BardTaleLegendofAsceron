using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Level {
	Passive,
	Normal,
	Aggressive,
	Nightmare
}

public class GameManager:MonoBehaviour {
    public static GameManager instance = null;
	[SerializeField] private GameObject enemyPrefab;
	//[SerializeField] private AudioClip battleMusic;
	[SerializeField] private GameObject battleMusic;
	[SerializeField] private GameObject gameLevelPanel;
	[SerializeField] private GameObject menuPanel; 
    private int countPlayerMove = 0;
    private Vector3 currentPlayerPosition = new Vector3();
	public Level level;

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
		battleMusic.SetActive(true);
		SceneManager.LoadScene("BattleScene");
    }

	public void LoadDifficultys() {//go to map 
								   //SceneManager.LoadScene(1);
		menuPanel.SetActive(false);
		gameLevelPanel.SetActive(true);
		//battleMusic.SetActive(false);
	}

	public void LoadMapScene() {
		battleMusic.SetActive(false);
		SceneManager.LoadScene(1);
	}

	public void ChoosePassiveDifficulty() {//choose passive difficultys
		level = Level.Passive;
		DataManager.instance.gameLevel = level;
		SceneManager.LoadScene(1);
	}

	public void ChooseNormalDifficulty() {//choose normal difficulty
		level = Level.Normal;
		DataManager.instance.gameLevel = level;
		SceneManager.LoadScene(1);
	}

	public void ChooseAggressiveDifficulty() {//choose aggressive difficulty
		level = Level.Aggressive;
		DataManager.instance.gameLevel = level;
		SceneManager.LoadScene(1);
	}

	public void ChooseNightmareDifficulty () {//choose nightmare difficulty
		level = Level.Nightmare;
		DataManager.instance.gameLevel = level;
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
