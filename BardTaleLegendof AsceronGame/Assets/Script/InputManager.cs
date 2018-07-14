using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {
	public static InputManager instance = null;

	void Awake() {
		if (InputManager.instance != null && instance != this) {
			Destroy(gameObject);
		}
		else
			instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("escape")) {
			GameManager.instance.LoadMenuPanel();
		}
	}
}
