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
    //PlayerPosition
	public Vector3 playerPosition = new Vector3();
	//stat player1
	public float levelPlayer1;
	public float currentExpPlayer1;
    public float healthPlayer1;
	public float maxHealthPlayer1;
    public float manaPlayer1;
	public float maxManaPlayer1;
    public float attackPlayer1;
    public float magicPlayer1;
    public float defensePlayer1;
    public float speedPlayer1;
	//stat player2
	public float levelPlayer2;
	public float currentExpPlayer2;
    public float healthPlayer2;
	public float maxHealthPlayer2;
    public float manaPlayer2;
	public float maxManaPlayer2;
    public float attackPlayer2;
    public float magicPlayer2;
    public float defensePlayer2;
    public float speedPlayer2;
    //list enemy defeated position
	public Queue listEnemyDefeatedPosition = new Queue();
	//game difficulty
	public Level gameLevel;
}
