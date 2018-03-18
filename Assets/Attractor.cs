using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Attractor : MonoBehaviour {
    private const float FORCE = 100f;

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
    }

    private void Reset() {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
    }

    private void OnEnable() {
        Attractors.Add(this);
    }

    private void OnDisable() {
        Attractors.Remove(this);
    }

//    private void FixedUpdate() {
//        foreach (Attractee a in Attractee.Attractees) {
//            Rigidbody arb = a.GetRigidbody();
//            Vector3 force = GetAttractionForce(arb.position, arb.mass);
//            arb.AddForce(force * a.GetSimulationSpeed());
//        }
//    }

//    private void FixedUpdate() {
//        foreach (Attractee a in Attractee.Attractees) {
////            Vector3 force = GetAttractionForce(a.transform.position, 1);
////            Vector3 newVelocity = a.velocity + force * Time.deltaTime;
////            a.transform.position = (a.velocity + newVelocity) / 2 * Time.deltaTime;
////            a.velocity = newVelocity;
//
//            Rigidbody arb = a.GetRigidbody();
//            Vector3 force = GetAttractionForce(arb.position, 1);
//            Vector3 newVelocity = a.velocity + force * Time.deltaTime;
//            arb.position = (a.velocity + newVelocity) / 2 * Time.deltaTime;
//            a.velocity = newVelocity;
//        }
//    }

    private Vector3 GetAttractionForce(Vector3 position, float mass) {
        Vector3 direction = _rb.position - position;
        float distanceSqr = direction.sqrMagnitude;

        float forceMag = FORCE * _rb.mass * mass / distanceSqr;
        return direction.normalized * forceMag;
    }
}
