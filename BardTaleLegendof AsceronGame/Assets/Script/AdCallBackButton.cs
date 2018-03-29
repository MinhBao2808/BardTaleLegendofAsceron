using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdCallBackButton : MonoBehaviour {
    public float timer;
    private float time;
	// Use this for initialization

    void Awake() {
        time = timer;
    }

	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => addCallBack());
	}
	
    private void addCallBack() {
        GameObject playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<ChoosePlayer>().SelectAttack();
    }

	void Update() {
        if (BattleManager.instance.isEnemyTurn() == false) {
            timer -= Time.deltaTime;
            if (timer <= 0.0f) {
                timer = time;
                GameObject turnSystem = GameObject.Find("BattleManager");
                turnSystem.GetComponent<BattleManager>().nextTurn();
            }
        }
	}
}
