using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {
    private int countPlayerMove = 0;

	public void CountPlayerMovement () {
        countPlayerMove = countPlayerMove + 1;
        Debug.Log(countPlayerMove);
        if (countPlayerMove >= Random.Range(10,10000)) {
            SceneManager.LoadScene(2);
        }
    }
}
