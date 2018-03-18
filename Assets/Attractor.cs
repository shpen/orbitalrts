using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractor : MonoBehaviour {
    private const float G = 2f;

    public static readonly List<Attractor> Attractors = new List<Attractor>();

    public static Vector3 GetTotalAttractionForce(Vector3 position, float mass) {
        Vector3 force = Vector3.zero;
        foreach (Attractor attractor in Attractors) {
            force += attractor.GetAttractionForce(position, mass);
        }
        return force;
    }

    private Rigidbody _rb;

    public Rigidbody GetRigidbody() {
        return _rb;
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
        Attractors.Add(this);
    }

    private void OnDisable() {
        Attractors.Remove(this);
    }

    private Vector3 GetAttractionForce(Vector3 position, float mass) {
        Vector3 direction = _rb.position - position;
        float distanceSqr = direction.sqrMagnitude;

        float forceMag = G * _rb.mass * mass / distanceSqr;
        return direction.normalized * forceMag;
    }
}
