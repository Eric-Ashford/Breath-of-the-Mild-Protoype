using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
	private float vertical;
	private float horizontal;
	
	void FixedUpdate()
	{
	   GetInput();
	}

	void GetInput()
	{
		vertical = Input.GetAxis("Vertical");
		horizontal = Input.GetAxis("Horizontal");
	}
}
