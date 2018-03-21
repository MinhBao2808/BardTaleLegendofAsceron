using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour {
    [SerializeField] private GameObject[] enemyEncouterPrefab;
    private int countPlayerMove = 0;
    private bool spawning = false;

    void Start () {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == "BattleScene") {
            int index = Random.Range(0, enemyEncouterPrefab.Length);
            Instantiate(enemyEncouterPrefab[index],enemyEncouterPrefab[index].transform.position,enemyEncouterPrefab[index].transform.rotation);
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
    }

	public void CountPlayerMovement () {
        countPlayerMove = countPlayerMove + 1;
        if (countPlayerMove >= Random.Range(10,10000)) {
            SceneManager.LoadScene("BattleScene");
        }
    }
}
