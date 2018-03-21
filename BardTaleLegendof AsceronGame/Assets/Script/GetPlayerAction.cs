using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerAction : MonoBehaviour {
    private bool actionStarted = false;
    private Vector3 startPosition;

	private void Start() {
        startPosition = transform.position;
	}

	public void updatePlayerUI () {
        //Get use face
        GameObject player = GameObject.Find("PlayerUnitInformation") as GameObject;
        //player.GetComponent<BattleManager>().NewHealthStat(this.gameObject);
        //player.GetComponent<BattleManager>().NewManaStat(this.gameObject);

    }

    public void AttackTarget (GameObject target) {
        StartCoroutine(TimeForAction(target));
        GameObject turnSystem = GameObject.Find("BattleManager");
        turnSystem.GetComponent<BattleManager>().nextTurn();
    }

    IEnumerator TimeForAction(GameObject target)  {
        actionStarted = true;
        Vector3 targetPosition = new Vector3(target.transform.position.x - 2.0f, target.transform.position.y, target.transform.position.z);
        while (MoveTowardsTarget(targetPosition)) {
            yield return null;
        }
        //wait a bit
        yield return new WaitForSeconds(0.5f);
        //do damage

        //owner attack return to start positon
        Vector3 firstPosition = startPosition;
        while (MoveTowardsTarget(firstPosition)) {
            yield return null;
        }
        if (actionStarted) {
            Debug.Log("fuck");
            yield break;
        }
    }

    private bool MoveTowardsTarget(Vector3 target) {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, 25.0f * Time.deltaTime));
    }
}
