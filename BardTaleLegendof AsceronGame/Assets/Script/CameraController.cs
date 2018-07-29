using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform target;
	public float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0, 6, -8);
	public float xTilt = 10;
	private Vector3 destination = Vector3.zero;
	private PlayerMovement playerMovement;
	private float rotateVel = 0;


	// Use this for initialization
	void Start () {
		SetCameraTarget(target);
	}
   
	void SetCameraTarget(Transform t) {
		target = t;
		if (target.GetComponent<PlayerMovement>()) {
			playerMovement = target.GetComponent<PlayerMovement>();
		}
	}

	private void LateUpdate() {
		MoveToTheTarget();
		LookAtTarget();
	}
    
	void MoveToTheTarget() {
		destination = playerMovement.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget() {
		float eulerYAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler(transform.eulerAngles.x, eulerYAngle, 0);
	}
}
