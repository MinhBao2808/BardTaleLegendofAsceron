using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text playerManaText;
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject actionsMenu, enemyUnitsMenu;

    private List<PlayerStat> unitStats;
    //private List<int> unitTurn;
    private GameObject playerParty;
    public GameObject enemyEncounter;

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
        this.enemyUnitsMenu.SetActive(false);
        this.nextTurn();
	}

	//void Update() {
    //    nextTurn();
    //}

    public void nextTurn() {
        GameObject[] remainEnemyUnit = GameObject.FindGameObjectsWithTag("Enemy");
        Debug.Log(remainEnemyUnit.Length);
        if (remainEnemyUnit.Length == 0) {
            SceneManager.LoadScene(1);
        }
        GameObject[] remainPlayerUnit = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if (remainPlayerUnit.Length == 0) {
            SceneManager.LoadScene(0);
        }
        //int turnOfUnit = unitTurn[0];
        PlayerStat currentUnitStat = unitStats[0];
        //foreach (PlayerStat unitStat in unitStats) {
        //    if (unitStat.nextActTurn == turnOfUnit) {
        //        currentUnitStat = unitStat;
        //    }
        //}
        //Debug.Log(turnOfUnit);
        unitStats.Remove(currentUnitStat);
        //unitTurn.Remove(turnOfUnit);
        if (!currentUnitStat.isDead()) {
            GameObject currentUnit = currentUnitStat.gameObject;
            currentUnitStat.CalculateNextTurn(currentUnitStat.nextActTurn);
            //Debug.Log(currentUnitStat.nextActTurn);
            unitStats.Add(currentUnitStat);
            //unitTurn.Add(currentUnitStat.nextActTurn);
            //unitTurn.Sort();
            unitStats.Sort();
            if (currentUnit.tag == "PlayerUnit") {
                this.playerParty.GetComponent<ChoosePlayer>().SelectCurrentPlayer(currentUnit.gameObject);
            }
            else {
                currentUnit.GetComponent<EnemyAction>().Action();
            }
        }
        else {
            this.nextTurn();
        }
    }

    //public void ChangePlayer(GameObject newPlayer) {
    //    this.player = newPlayer;
    //}

    //public float NewHealthStat(GameObject newPlayer) {
    //    return player.GetComponent<PlayerStat>().health;
    //}

    //public float NewManaStat(GameObject newPlayer) {
    //    return player.GetComponent<PlayerStat>().mana;
    //}
}
