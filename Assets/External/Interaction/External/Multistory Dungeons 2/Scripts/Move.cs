using UnityEngine;
using System.Collections;

namespace manastation.multistorydungeons{

public class Move : MonoBehaviour {

	private float speed = 7f;
	private float gravity = 2000f;
	float horizontalMovement;
	float verticalMovement;
	private CharacterController character;
	private Vector3 destination = Vector3.zero;
	
	void Start()
	{		
		character = GetComponent<CharacterController>();		
	}
	
	void Update () {
		horizontalMovement = ControlFreak2.CF2Input.GetAxis("Horizontal");
		verticalMovement = ControlFreak2.CF2Input.GetAxis("Vertical");
		
		destination.Set(horizontalMovement, 0, verticalMovement);
        destination = transform.TransformDirection(destination);
        destination *= speed;
		
		destination.y -= gravity * Time.deltaTime;
        character.Move(destination * Time.deltaTime);		
	}
}
}
