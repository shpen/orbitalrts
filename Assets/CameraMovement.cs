using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour {
	private const float SPEED = 1f;

	private Camera _cam;

	// Use this for initialization
	void Start () {
		_cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 direction = Vector3.zero;
		Vector3 forward = _cam.transform.forward;
		Vector3 right = _cam.transform.right;
		if (Input.GetKey("w")) {
			direction += forward;
		}
		if (Input.GetKey("a")) {
			direction -= right;
		}
		if (Input.GetKey("s")) {
			direction -= forward;
		}
		if (Input.GetKey("d")) {
			direction += right;
		}
		direction.y = 0;
		transform.position += direction.normalized * SPEED;
	}
}
