using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateEnemyMenu : MonoBehaviour {
    [SerializeField] private GameObject targetEnemyUnitPrefab;//enemy button
    [SerializeField] private string menuName;
    [SerializeField] private Vector2 intialPosition, itemDimensions;
    [SerializeField] KillEmeny killEmenyScript;

	void Awake() {
        GameObject enemyUnitMenu = GameObject.Find("EnemyUnitMenu");
        GameObject[] itemExist = GameObject.FindGameObjectsWithTag("Enemy");
        Vector2 nextPosition = new Vector2(this.intialPosition.x, this.intialPosition.y + (itemExist.Length * this.itemDimensions.y));
        GameObject targetEnemyUnit = Instantiate(this.targetEnemyUnitPrefab, enemyUnitMenu.transform) as GameObject;

        targetEnemyUnit.name = "Target" + this.gameObject.name;
        targetEnemyUnit.transform.localPosition = nextPosition;
        targetEnemyUnit.GetComponent<Button>().onClick.AddListener(() => SelectEnemyTarget());
        targetEnemyUnit.GetComponentsInChildren<Text>()[0].text = "" + targetEnemyUnit.name;
	}

    public void SelectEnemyTarget() {
        GameObject playerPartyData = GameObject.Find("PlayerParty");
        playerPartyData.GetComponent<ChoosePlayer>().PlayerAttackEnemy(this.gameObject);
    }
}
