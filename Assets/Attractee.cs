using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractee : MonoBehaviour {
	public static readonly List<Attractee> Attractees = new List<Attractee>();

	private const float G = 2f;

	private Rigidbody _rb;

	public Vector3 velocity = Vector3.zero;

	public Rigidbody GetRigidbody() {
		return _rb;
	}

	public virtual float GetSimulationSpeed() {
		return 1f;
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

	private void OnEnable() {
		Attractees.Add(this);
	}

	private void OnDisable() {
		Attractees.Remove(this);
	}

	private void FixedUpdate() {
		int timeSteps = Mathf.RoundToInt(GetSimulationSpeed());

		for (int i = 0; i < timeSteps; i++) {
			Vector3 force = Vector3.zero;
			foreach (Attractor a in Attractor.Attractors) {
				Rigidbody arb = a.GetRigidbody();
				force -= GetAttractionForce(arb.position, arb.mass);
			}
			Vector3 newVelocity = velocity + force * Time.deltaTime;
			_rb.transform.position += (velocity + newVelocity) / 2 * Time.deltaTime;
			velocity = newVelocity;
		}


//		Vector3 newVelocity = _rb.velocity + force * scaledTime;
//		//_rb.position += (_rb.velocity + newVelocity) / 2 * scaledTime;
//		_rb.velocity = newVelocity;


		//_rb.velocity += force * scaledTime;
	}

	private Vector3 GetAttractionForce(Vector3 position, float mass) {
		Vector3 direction = _rb.transform.position - position;
		float distanceSqr = direction.sqrMagnitude;

		float forceMag = 1f * _rb.mass * mass / distanceSqr * G;
		return direction.normalized * forceMag;
	}
}
