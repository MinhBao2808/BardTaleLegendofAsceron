using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemyMenu : MonoBehaviour {
    [SerializeField] private GameObject targetEnemyUnitPrefab;//enemy button
    [SerializeField] private string menuName;
    [SerializeField] private Vector2 intialPosition, itemDimensions;
    [SerializeField] KillEmeny killEmenyScript;
    public float timer;
    private float time;

	void Awake() {
        time = timer;
        GameObject enemyUnitMenu = GameObject.Find("EnemyUnitMenu");
        GameObject[] itemExist = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 nextPosition = new Vector2(this.intialPosition.x, this.intialPosition.y + (itemExist.Length * this.itemDimensions.y));
        GameObject targetEnemyUnit = Instantiate(this.targetEnemyUnitPrefab, enemyUnitMenu.transform) as GameObject;

        targetEnemyUnit.name = "Target" + this.gameObject.name;
        targetEnemyUnit.transform.localPosition = nextPosition;
        targetEnemyUnit.GetComponent<Button>().onClick.AddListener(() => SelectEnemyTarget());
        targetEnemyUnit.GetComponentsInChildren<Text>()[0].text = "" + targetEnemyUnit.name;
        killEmenyScript.enemyMenuItem = targetEnemyUnit;
	}

    public void SelectEnemyTarget() {
        GameObject playerPartyData = GameObject.Find("PlayerParty");
        playerPartyData.GetComponent<ChoosePlayer>().PlayerAttackEnemy(this.gameObject);
    }

    void Update() {
        if (BattleManager.instance.isEnemyTurn() == false && BattleManager.instance.isPlayerSelectAttack() == true) {
            timer -= Time.deltaTime;
            if (timer <= 0.0f) {
                timer = time;
                GameObject turnSystem = GameObject.Find("BattleManager");
                turnSystem.GetComponent<BattleManager>().nextTurn();
            }
        }
    }
}
