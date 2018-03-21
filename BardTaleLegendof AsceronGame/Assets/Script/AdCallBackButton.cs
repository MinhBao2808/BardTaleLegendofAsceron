using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdCallBackButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => addCallBack());
	}
	
    private void addCallBack() {
        GameObject playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<ChoosePlayer>().SelectAttack();
    }
}
