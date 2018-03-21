using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour, IComparable {
    
    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed;

    public int nextActTurn;

    private bool dead = false;

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
