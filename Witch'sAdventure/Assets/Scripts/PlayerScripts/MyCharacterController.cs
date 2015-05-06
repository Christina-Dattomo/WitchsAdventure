using UnityEngine;
using System.Collections;
//using InControl;

public class MyCharacterController : MonoBehaviour {

	public enum CharacterState { Enabled, Disabled }
	public CharacterState currentCharacterState = CharacterState.Enabled;
	public float speed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	public Transform camera;
	private Vector3 moveDirection = Vector3.zero;

	public Transform dialogTarget;

	void Update() {
		switch (currentCharacterState) 
		{
		case CharacterState.Enabled:
			CharacterController controller = GetComponent<CharacterController>();
			if (controller.isGrounded) 
			{
				moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
				moveDirection = transform.TransformDirection(moveDirection);
				moveDirection *= speed;
				//			if (Input.GetButton("Jump"))
				//				moveDirection.y = jumpSpeed;
				
			}
			moveDirection.y -= gravity * Time.deltaTime;
			controller.Move(moveDirection * Time.deltaTime);
			this.transform.eulerAngles = new Vector3(this.transform.eulerAngles.x, camera.transform.eulerAngles.y, this.transform.eulerAngles.z);
			break;
		case CharacterState.Disabled:
			transform.LookAt(dialogTarget);
			break;
		}
	}
}
