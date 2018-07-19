using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveUIManager : MonoBehaviour {
	public static SaveUIManager instance = null;
	[SerializeField] private GameObject saveButton;
	[SerializeField] private GameObject savePanel;
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
