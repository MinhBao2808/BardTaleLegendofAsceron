using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {
    public static DataManager instance = null;
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else if (instance != null) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    //PlayeerPosition
    public Vector3 playerPosition = new Vector3();
    //stat player1
    public float healthPlayer1;
    public float manaPlayer1;
    public float attackPlayer1;
    public float magicPlayer1;
    public float defensePlayer1;
    public float speedPlayer1;
    //stat player2
    public float healthPlayer2;
    public float manaPlayer2;
    public float attackPlayer2;
    public float magicPlayer2;
    public float defensePlayer2;
    public float speedPlayer2;
}
