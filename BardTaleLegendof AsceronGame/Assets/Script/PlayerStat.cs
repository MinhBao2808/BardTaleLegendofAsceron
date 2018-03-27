﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour, IComparable {
    //public Canvas canvas;
    public float damageTextOffset = 100f;
    private bool dead = false;
    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed;
    private GameObject damageTextObject;
    [SerializeField] private GameObject damageText;

    public int nextActTurn;
    Vector3 offset = Vector3.zero;

    public void ReceiveDamage (float damage) {
        this.health -= damage;
        GameObject canvas = GameObject.Find("Canvas");
        damageTextObject = Instantiate(this.damageText) as GameObject;
        damageTextObject.transform.SetParent(canvas.transform, false);
        damageTextObject.GetComponent<Text>().text = "" + damage;
        Vector3 worldPos = new Vector3(transform.position.x + damageTextOffset, transform.position.y + damageTextOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        //Debug.Log(screenPos);
        damageTextObject.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);
        if (this.health <= 0) {
            this.dead = true;
            this.gameObject.tag = "DeadUnit";
            Destroy(this.gameObject);
        }
    }

    public void CalculateNextTurn(int currentTurn) {
        this.nextActTurn = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
        //Debug.Log(this.nextActTurn);
    }

    public int CompareTo(object otherStats) {
        return nextActTurn.CompareTo(((PlayerStat)otherStats).nextActTurn);
    }

    public bool isDead() {
        return this.dead;
    }
}
