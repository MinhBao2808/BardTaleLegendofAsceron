using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	[SerializeField] private float enemySpeed;
	[SerializeField] private float rayLength;
	[SerializeField] private LayerMask layer;

	private RaycastHit2D hit;
	private GameObject player;
	private bool enemySeePlayer = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		var target = player.transform.position;
		hit = Physics2D.Raycast(transform.position,Vector2.zero,rayLength,layer.value);
		if (hit.collider != null) {
			GameManager.instance.GoToBattle();
		}
		if (enemySeePlayer == true) {
			//move to player
			transform.position = Vector3.MoveTowards(transform.position, target, enemySpeed * Time.deltaTime);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			enemySeePlayer = true;
		}
	}
}
