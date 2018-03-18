using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSimulation : Attractee {
    public static float speed = 10f;
    public static float speedSqrt = Mathf.Sqrt(speed);

    public override float GetSimulationSpeed() {
        return speed;
    }

    private void Start() {
        //GetRigidbody().detectCollisions = false;
        Invoke("Remove", 3f);
    }

    private void Remove() {
        Launcher.SimulatedProjectiles.Remove(GetRigidbody());
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Planet")) {
            Remove();
        }
    }

//    private void OnCollisionEnter(Collision other) {
//        if (other.collider.CompareTag("Planet")) {
//            Remove();
//        }
//    }
}
