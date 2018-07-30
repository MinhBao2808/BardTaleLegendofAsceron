using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	
	[SerializeField] private float enemySpeed;
	[SerializeField] private float rayLength;
	[SerializeField] private LayerMask layer;

	private bool isHit;
	private GameObject player;
	private bool enemySeePlayer = false;
	private Vector2 enemyStartPosition;

    

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").gameObject;
		enemyStartPosition = this.transform.position;
		foreach (Vector2 enemy in DataManager.instance.listEnemyDefeatedPosition) {
			if(enemyStartPosition == enemy) {
				Destroy(this.gameObject);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		var target = player.transform.position;
		isHit = Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),rayLength,layer.value);
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.yellow);
		if (isHit) {
			Debug.Log("a");
			//DataManager.instance.listEnemyDefeatedPosition.Enqueue(enemyStartPosition);
			//Destroy(this.gameObject);
			//GameManager.instance.GoToBattle();
			enemySeePlayer = true;

		}
		if (enemySeePlayer == true) {
			//move to player
			Vector3 lookAt = player.transform.position;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);
            transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter(Collider collision) {
		if (collision.gameObject.tag == "Player") {
			//enemySeePlayer = true;
			DataManager.instance.listEnemyDefeatedPosition.Enqueue(enemyStartPosition);
            Destroy(this.gameObject);
            GameManager.instance.GoToBattle();
		}
	}
}
