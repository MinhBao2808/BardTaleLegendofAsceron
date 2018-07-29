using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
                 
public class SaveUIManager : MonoBehaviour {
	public static SaveUIManager instance = null;
	[SerializeField] private GameObject saveButton;
	[SerializeField] private GameObject savePanel;
	[SerializeField] private GameObject leftStatPanel;
	[SerializeField] private GameObject rightStatPanel;
	private int lvPlayer1, lvPlayer2, maxHpPlayer1, maxHpPlayer2, hpPlayer1, hpPlayer2, maxMpPlayer1, maxMpPlayer2, mpPlayer1, mpPlayer2;

	public GameObject[] saveFileButton;

	void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

	public void LoadSavePanel () {
		saveButton.SetActive(false);
		savePanel.SetActive(true);
		for (int i = 0; i < GameManager.instance.index; i++) {
			saveFileButton[i].SetActive(true);
		}
	}

	public void BackButton() {
		saveButton.SetActive(true);
		savePanel.SetActive(false);
	}
}
