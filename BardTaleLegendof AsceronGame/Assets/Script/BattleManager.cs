using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {
    public static BattleManager instance = null;
    [SerializeField] private GameObject playerHealthName;//get player health text game object
    [SerializeField] private GameObject playerManaName;//get player mana text game object
    [SerializeField] private GameObject[] enemyEncouterPrefab;
	[SerializeField] private GameObject playerAvatar;
	[SerializeField] private Image playerAvatarImage;
    [SerializeField] private Text playerHealthText;
    [SerializeField] private Text playerManaText;
    //[SerializeField] private GameObject player;
    [SerializeField] private GameObject actionsMenu, enemyUnitsMenu;

    private List<PlayerStat> unitStats;
    //private List<int> unitTurn;
    private GameObject playerParty;
    public GameObject enemyEncounter;
    public float timer;
    private float time;
    private bool enemyTurn = false;
    private bool playerSelectAttack = false;
    private bool playerAttack = false;

	void Awake() {
        if (instance == null) {
            instance = this;
        }
        time = timer;
        playerAttack = false;
        int index = Random.Range(0, enemyEncouterPrefab.Length);
        Instantiate(enemyEncouterPrefab[index], enemyEncouterPrefab[index].transform.position, enemyEncouterPrefab[index].transform.rotation);
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
		this.playerAvatar.SetActive(false);
        this.playerManaName.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        this.nextTurn();
	}

    void Update() {
        if (enemyTurn == false && playerSelectAttack == false) {
            timer -= Time.deltaTime;
            Debug.Log(timer);
            if (timer <= 0.0f) {
                timer = time;
                this.nextTurn();
            }
        }
        else if (playerAttack == true && enemyTurn == false) {
            timer = time;
        }
    }

    public void SetPlayerInfoUI(PlayerStat currentPlayerStat) {
        playerHealthName.SetActive(true);
        playerManaName.SetActive(true);
		playerAvatar.SetActive(true);
		playerAvatarImage.overrideSprite = currentPlayerStat.playerAvatar;
        playerHealthText.text = "" + currentPlayerStat.health;
        playerManaText.text = "" + currentPlayerStat.mana;
    }

    public void SetActivePlayerInfoUI () {
        playerHealthName.SetActive(false);
        playerManaName.SetActive(false);
    }

    public void nextTurn() {
        this.actionsMenu.SetActive(false);
        this.playerHealthName.SetActive(false);
		this.playerAvatar.SetActive(false);
        this.playerManaName.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        playerAttack = false;
        GameObject[] remainEnemyUnit = GameObject.FindGameObjectsWithTag("Enemy");
        if (remainEnemyUnit.Length == 0) {
            GameObject[] playerObject = GameObject.FindGameObjectsWithTag("PlayerUnit");
            for (var i = 0; i < playerObject.Length; i++) {
				if (playerObject[i].gameObject.name == "Player1") {
					DataManager.instance.maxHealthPlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().maxHealth;
					DataManager.instance.maxManaPlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().maxHealth;
                    DataManager.instance.attackPlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().attack;
                    DataManager.instance.defensePlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().defense;
                    DataManager.instance.healthPlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().health;
                    DataManager.instance.magicPlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().magic;
                    DataManager.instance.manaPlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().mana;
                    DataManager.instance.speedPlayer1 = playerObject[i].gameObject.GetComponent<PlayerStat>().speed;
                }
                else if(playerObject[i].gameObject.name == "Player2") {
					DataManager.instance.maxHealthPlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().maxHealth;
					DataManager.instance.maxManaPlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().maxMana;
                    DataManager.instance.attackPlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().attack;
                    DataManager.instance.defensePlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().defense;
                    DataManager.instance.healthPlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().health;
                    DataManager.instance.magicPlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().magic;
                    DataManager.instance.manaPlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().mana;
                    DataManager.instance.speedPlayer2 = playerObject[i].gameObject.GetComponent<PlayerStat>().speed;
                }
            }
            GameManager.instance.LoadMapScene();
        }
        GameObject[] remainPlayerUnit = GameObject.FindGameObjectsWithTag("PlayerUnit");
        if (remainPlayerUnit.Length == 0) {
            GameManager.instance.LoadGameMenu();
        }
        PlayerStat currentUnitStat = unitStats[0];
        unitStats.Remove(currentUnitStat);
        if (!currentUnitStat.isDead()) {
            GameObject currentUnit = currentUnitStat.gameObject;
            currentUnitStat.CalculateNextTurn(currentUnitStat.nextActTurn);
            unitStats.Add(currentUnitStat);
            //unitStats.Sort();
            if (currentUnit.tag == "PlayerUnit") {
                enemyTurn = false;
                this.playerParty.GetComponent<ChoosePlayer>().SelectCurrentPlayer(currentUnit.gameObject,currentUnitStat);
            }
            else {
                enemyTurn = true;
                currentUnit.GetComponent<EnemyAction>().Action();
            }
        }
        else {
            this.nextTurn();
        }
    }

    public bool isEnemyTurn() {
        return enemyTurn;
    }

    public void SetPlayerSelectAttack() {
        playerSelectAttack = true;
    }

    public bool isPlayerSelectAttack() {
        return playerSelectAttack;
    }
    public void GetPlayerSelectAttack() {
        playerSelectAttack = false;
    }

    public bool isPlayerAttack() {
        return playerAttack;
    }
    public void SetPlayerAttack() {
        playerAttack = true;
    }
    public void GetPlayerAttack() {
        playerAttack = false;
    }
}
