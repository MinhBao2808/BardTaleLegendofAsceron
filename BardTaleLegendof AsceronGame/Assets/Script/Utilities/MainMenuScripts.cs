using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScripts : MonoBehaviour {
    public Button continueButton;

    private void Start()
    {
        if(SaveLoadManager.Instance.LoadAllSavefile().Length == 0)
        {
            continueButton.gameObject.SetActive(false);
        }
        else
        {
            continueButton.gameObject.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
