﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] private float speed;
    private GameManager gameManager = new GameManager();

	void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector2 currentVelocity = gameObject.GetComponent<Rigidbody2D>().velocity;
        float newVelocityX = 0.0f;
        float newVelocityY = 0.0f;

        //player movement code
        if (moveHorizontal < 0 && currentVelocity.x <= 0) {
            newVelocityX = -speed;
            gameManager.CountPlayerMovement();
        }
        else if (moveHorizontal > 0 && currentVelocity.x >= 0) {
            newVelocityX = speed;
            gameManager.CountPlayerMovement();
        }

        if (moveVertical < 0 && currentVelocity.y <=0) {
            newVelocityY = -speed;
            gameManager.CountPlayerMovement();
        }
        else if (moveVertical > 0 && currentVelocity.y >=0) {
            newVelocityY = speed;
            gameManager.CountPlayerMovement();
        }

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(newVelocityX, newVelocityY);
	}
}
