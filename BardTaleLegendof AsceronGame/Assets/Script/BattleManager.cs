using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {
    public static BattleManager instance = null;
    [SerializeField] private GameObject playerHealthName;//get player health text game object
    [SerializeField] private GameObject playerManaName;//get player mana text game objec
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text playerManaText;
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject actionsMenu, enemyUnitsMenu;

    private List<PlayerStat> unitStats;
    //private List<int> unitTurn;
    private GameObject playerParty;
    public GameObject enemyEncounter;

	void Awake() {
        if (instance == null) {
            instance = this;
        }	 
	}

	void Start() {
        this.playerParty = GameObject.Find("PlayerParty");
        unitStats = new List<PlayerStat>();
        //unitTurn = new List<int>();
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        foreach (GameObject playerUnit in playerUnits) {
            PlayerStat currentStat = playerUnit.GetComponent<PlayerStat>();
            currentStat.CalculateNextTurn(0);
            unitStats.Add(currentStat);
            //unitTurn.Add(currentStat.nextActTurn);
        }
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemyUnits) {
            PlayerStat currentStat = enemy.GetComponent<PlayerStat>();
            currentStat.CalculateNextTurn(0);
            unitStats.Add(currentStat);
            //unitTurn.Add(currentStat.nextActTurn);
        }
        //unitTurn.Sort();
        unitStats.Sort();
        this.actionsMenu.SetActive(false);
        this.playerHealthName.SetActive(false);
        this.playerManaName.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        this.nextTurn();
	}

    public void SetPlayerInfoUI(PlayerStat currentPlayerStat) {
        playerHealthName.SetActive(true);
        playerManaName.SetActive(true);
        playerHealthText.text = "" + currentPlayerStat.health;
        playerManaText.text = "" + currentPlayerStat.mana;
    }

    public void SetActivePlayerInfoUI () {
        playerHealthName.SetActive(false);
        playerManaName.SetActive(false);
    }

    public void nextTurn() {
        GameObject[] remainEnemyUnit = GameObject.FindGameObjectsWithTag("Enemy");
        if (remainEnemyUnit.Length == 0) {
            SceneManager.LoadScene(1);
        }
        GameObject[] remainPlayerUnit = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if (remainPlayerUnit.Length == 0) {
            SceneManager.LoadScene(0);
        }
        PlayerStat currentUnitStat = unitStats[0];
        unitStats.Remove(currentUnitStat);
        if (!currentUnitStat.isDead()) {
            GameObject currentUnit = currentUnitStat.gameObject;
            currentUnitStat.CalculateNextTurn(currentUnitStat.nextActTurn);
            unitStats.Add(currentUnitStat);
            //unitStats.Sort();
            if (currentUnit.tag == "PlayerUnit") {
                this.playerParty.GetComponent<ChoosePlayer>().SelectCurrentPlayer(currentUnit.gameObject,currentUnitStat);
            }
            else {
                currentUnit.GetComponent<EnemyAction>().Action();
            }
        }
        else {
            this.nextTurn();
        }
    }
}
