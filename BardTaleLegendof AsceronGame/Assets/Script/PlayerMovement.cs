using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public static PlayerMovement instance = null;
	[SerializeField] private GameObject StatPanel;
	private Animator animator;
	private Rigidbody rigid;
	[SerializeField] private float inputDelay = 0.1f;
	[SerializeField] private float forwardVel = 12;
	[SerializeField] private float rotateVel = 100;
	private Quaternion targetRotation;
	private float forwardInput, turnInput; 
	[SerializeField] private float speed;
	[SerializeField] private float rotateSpeed = 3.0f;
    private GameManager gameManager = new GameManager();
    
	private void Awake() {
        if (instance == null) {
            instance = this;
        }
        //gameObject.transform.position = DataManager.instance.playerPosition;
	}

	private void Start() {
		targetRotation = transform.rotation;
		rigid = GetComponent<Rigidbody>();
		animator = GetComponent<Animator>();
		forwardInput = turnInput = 0;
	}

	public Quaternion TargetRotation {
		get {
			return targetRotation;
		}
	}

	void GetInput () {
		forwardInput = Input.GetAxis("Vertical");
		turnInput = Input.GetAxis("Horizontal");
	}

	void Update() {
		GetInput();
		Turn();
		if (Input.GetKeyDown("e")) {
			StatPanel.SetActive(true);
        }

		if (Input.GetKeyDown("escape")) {
			StatPanel.SetActive(false);
        }
	}
    
	private void FixedUpdate() {
		Run();
	}

	void Run() {
		if (Mathf.Abs(forwardInput) > inputDelay) {
			rigid.velocity = transform.forward * forwardInput * forwardVel;
			animator.SetBool("Moving", true);
		}
		else {
			rigid.velocity = Vector3.zero;
			animator.SetBool("Moving", false);
		}
	}

	void Turn() {
		if (Mathf.Abs(turnInput) >inputDelay) {
			targetRotation *= Quaternion.AngleAxis(rotateVel * turnInput * Time.deltaTime, Vector3.up);
		}
		transform.rotation = targetRotation;
	}

	public Vector2 ReturnPlayerPosition() {
        Vector2 playerPosition = transform.position;
        return playerPosition;
    }
}
