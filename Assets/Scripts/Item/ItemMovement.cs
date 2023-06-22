using System;
using System.Collections;
using UnityEngine;

public class ItemMovement : MonoBehaviour
{
	public float moveSpeed;

	private void FixedUpdate()
	{
		transform.Translate(moveSpeed * Time.fixedDeltaTime * Vector3.down);
	}
}
