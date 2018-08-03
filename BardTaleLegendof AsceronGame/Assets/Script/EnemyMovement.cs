using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MovingObject {
	[SerializeField] private float rayLength;
	[SerializeField] private float timeToFollowPlayer;
	[SerializeField] private LayerMask layer;

	private bool isHitForward, isHitRight45Degree, isHitLeft45Degree, isHitRight30Degree, isHitLeft30Degree, isHitRight60Degree, isHitLeft60Degree, isHitRight15Degree, isHitLeft15Degree;
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
		//var a = yourAngleInDegree * Mathf.Deg2Rad;
		var target = player.transform.position;
		isHitForward = Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),rayLength,layer.value);
		isHitRight45Degree = Physics.Raycast(transform.position, transform.TransformDirection((Vector3.forward + Vector3.right).normalized), rayLength, layer.value);
		isHitLeft45Degree = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + Vector3.left).normalized, rayLength, layer.value);
		isHitRight15Degree = Physics.Raycast(transform.position, transform.TransformDirection((Vector3.forward * Mathf.Cos(15 * Mathf.Deg2Rad) + Vector3.right * Mathf.Sin(15 * Mathf.Deg2Rad)).normalized), rayLength, layer.value);
		isHitLeft15Degree = Physics.Raycast(transform.position, transform.TransformDirection((Vector3.forward * Mathf.Cos(-15 * Mathf.Deg2Rad) + Vector3.left * Mathf.Sin(-15 * Mathf.Deg2Rad)).normalized), rayLength, layer.value);
		//Debug.DrawRay(transform.position, transform.TransformDirection((Vector3.forward + Vector3.right).normalized) * rayLength, Color.yellow);
		//Debug.DrawRay(transform.position, transform.TransformDirection((Vector3.forward + Vector3.left).normalized) * rayLength, Color.yellow);
		//Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayLength, Color.red);
		//Debug.DrawRay(transform.position, transform.TransformDirection((Vector3.forward * Mathf.Cos(15 * Mathf.Deg2Rad) + Vector3.right * Mathf.Sin(15 * Mathf.Deg2Rad)).normalized) * rayLength, Color.red);
		//Debug.DrawRay(transform.position, transform.TransformDirection((Vector3.forward * Mathf.Cos(-15 * Mathf.Deg2Rad) + Vector3.right * Mathf.Sin(-15 * Mathf.Deg2Rad)).normalized) * rayLength, Color.red);
		float angles = transform.eulerAngles.y - 60;
		for (int i = 0; i < 7; i++) {
			Debug.DrawRay(transform.position, transform.TransformDirection((Vector3.forward * Mathf.Cos(i * Mathf.Deg2Rad) + Vector3.right * Mathf.Sin(i * Mathf.Deg2Rad)).normalized) * rayLength, Color.red);
            angles += 15;
        }

		if (isHitForward || isHitRight45Degree || isHitLeft45Degree) { 
			//DataManager.instance.listEnemyDefeatedPosition.Enqueue(enemyStartPosition);
			//Destroy(this.gameObject);
			//GameManager.instance.GoToBattle();
			enemySeePlayer = true;
		}
		if (enemySeePlayer == true && timeToFollowPlayer >=0) {
			//move to player
			Vector3 lookAt = player.transform.position;
            lookAt.y = transform.position.y;
            transform.LookAt(lookAt);
			transform.position = Vector3.MoveTowards(transform.position, target, sprintSpeed * Time.deltaTime);
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
