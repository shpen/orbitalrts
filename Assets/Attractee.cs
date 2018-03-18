using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractee : MonoBehaviour {
	private Rigidbody _rb;

	public Vector3 Velocity = Vector3.zero;

	public virtual int GetSimulationSpeed() {
		return 1;
	}

	private void Awake() {
		_rb = GetComponent<Rigidbody>();
		_rb.useGravity = false;
		_rb.isKinematic = true;
	}

	private void Reset() {
		_rb = GetComponent<Rigidbody>();
		_rb.useGravity = false;
		_rb.isKinematic = true;
	}

	private void FixedUpdate() {
		int timeSteps = Mathf.RoundToInt(GetSimulationSpeed());
		for (int i = 0; i < timeSteps; i++) {
			Vector3 force = Attractor.GetTotalAttractionForce(_rb.transform.position, _rb.mass);
			Vector3 newVelocity = Velocity + force * Time.deltaTime;
			_rb.transform.position += (Velocity + newVelocity) / 2 * Time.deltaTime;
			Velocity = newVelocity;
		}
	}
}
