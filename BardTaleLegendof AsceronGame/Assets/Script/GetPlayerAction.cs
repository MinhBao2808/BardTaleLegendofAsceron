using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerAction : MonoBehaviour {
    private bool actionStarted = false;
    private Vector3 startPosition;
    public GameObject owner;

	//private void Start() {
 //       //startPosition = transform.position;
 //       Debug.Log(startPosition);
	//}

	public void updatePlayerUI () {
        //Get use face
        GameObject player = GameObject.Find("PlayerUnitInformation") as GameObject;
        //player.GetComponent<BattleManager>().NewHealthStat(this.gameObject);
        //player.GetComponent<BattleManager>().NewManaStat(this.gameObject);

    }

    public void AttackTarget (GameObject target,GameObject player) {
        startPosition = player.gameObject.transform.position;
        if (BattleManager.instance.isEnemyTurn() == false) {
            StartCoroutine(TimeForAction(target));
        }
    }

    IEnumerator TimeForAction(GameObject target)  {

        Vector3 targetPosition = new Vector3(target.transform.position.x - 2.0f, target.transform.position.y, target.transform.position.z);
        while (MoveTowardsTarget(targetPosition)) {
            yield return null;
        }
        //wait a bit
        yield return new WaitForSeconds(0.5f);
        //do damage
        actionStarted = true;
        Hit(target);
        //owner attack return to start positon
        Vector3 firstPosition = startPosition;
        while (MoveTowardsTarget(firstPosition)) {
            yield return null;
        }
        if (actionStarted == true) {
            GameObject turnSystem = GameObject.Find("BattleManager");
            turnSystem.GetComponent<BattleManager>().nextTurn();
        }

    }

    private void Hit (GameObject target) {
        PlayerStat ownerStat = this.owner.GetComponent<PlayerStat>();
        PlayerStat targetStat = target.GetComponent<PlayerStat>();
        targetStat.ReceiveDamage(ownerStat.attack);
    }

    private bool MoveTowardsTarget(Vector3 target) {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, 25.0f * Time.deltaTime));
    }
}
