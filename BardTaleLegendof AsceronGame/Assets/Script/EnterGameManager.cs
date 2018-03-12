using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGameManager : MonoBehaviour {
    public void LoadMapScene () {
        SceneManager.LoadScene(1);
    }
}
