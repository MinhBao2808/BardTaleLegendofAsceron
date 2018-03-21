using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour {
    [SerializeField] private string targetsTag;
    private bool actionStarted = false;
    private Vector3 startPosition;

	void Start() {
        startPosition = transform.position;	
	}

	GameObject FindRandomTarget() {
        GameObject[] possibleTargets = GameObject.FindGameObjectsWithTag(targetsTag);
        if (possibleTargets.Length > 0) {
            int targetIndex = Random.Range(0, possibleTargets.Length);
            GameObject target = possibleTargets[targetIndex];
            return target;
        }
        else {
            return null;
        }
    }

    public void Action () {
        GameObject target = FindRandomTarget();
        StartCoroutine(TimeForAction(target));
        GameObject turnSystem = GameObject.Find("BattleManager");
        turnSystem.GetComponent<BattleManager>().nextTurn();
    }


    IEnumerator TimeForAction(GameObject target) {
        if (actionStarted) {
            yield break;
        }
        actionStarted = true;
        Vector3 targetPosition = new Vector3(target.transform.position.x + 1.0f, target.transform.position.y, target.transform.position.z);
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
    }

    private bool MoveTowardsTarget(Vector3 target) {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, 15.0f * Time.deltaTime));
    }
}
