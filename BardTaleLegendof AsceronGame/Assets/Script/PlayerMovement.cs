using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovingObject {
    public static PlayerMovement instance = null;
	private CharacterController characterController;
	private Quaternion targetRotation;
	[SerializeField] private float turnSmoothTime;
	private float turnSmoothVelocity;
	public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    
	private void Awake() {
        if (instance == null) {
            instance = this;
        }
        //gameObject.transform.position = DataManager.instance.playerPosition;
	}

	private void Start() {
		targetRotation = transform.rotation;
		anim = GetComponent<Animator>();
		characterController = GetComponent<CharacterController>();
	}
  

	void Update() {
		//GetInput();
		//Turn();
		isRun = Input.GetKey(KeyCode.LeftShift);
		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		Vector2 inputDir = input.normalized;
		if (inputDir != Vector2.zero) {
			currentSpeed = ((isRun) ? sprintSpeed : walkSpeed) * inputDir.magnitude;
			float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
			anim.SetBool("Moving", true);
			Vector3 velocity = transform.forward * currentSpeed;
			characterController.Move(velocity * Time.deltaTime);
		}
		else {
			anim.SetBool("Moving", false);
		}
	}

	public Vector2 ReturnPlayerPosition() {
        Vector2 playerPosition = transform.position;
        return playerPosition;
    }
}
