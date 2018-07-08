using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour {
	public Camera mainCamera;
	public bool maintainWidth = true;
	[Range(-1, 1)]
    public int adaptPosition;
	float defaultWidth;
	float defaultHeight;
	Vector3 CameraPos;
	// Use this for initialization
	void Start () {
		CameraPos = mainCamera.transform.position;
		defaultHeight = mainCamera.orthographicSize;
		defaultWidth = mainCamera.orthographicSize * mainCamera.aspect;
	}
	
	// Update is called once per frame
	void Update () {
		if (maintainWidth == true) {
			mainCamera.orthographicSize = defaultWidth / mainCamera.aspect;
			//mainCamera.transform.position = new Vector3(CameraPos.x, CameraPos.y + adaptPosition * (defaultHeight - mainCamera.orthographicSize), CameraPos.z);
		}
		else {
			//mainCamera.transform.position = new Vector3(CameraPos.x + adaptPosition * (defaultWidth - mainCamera.orthographicSize * mainCamera.aspect), CameraPos.y, CameraPos.z);
		}
	}
}
