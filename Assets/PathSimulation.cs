using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSimulation : Attractee {
    public override int GetSimulationSpeed() {
        return 20;
    }

    private void Start() {
        Invoke("Remove", 1f);
    }

    public void Remove() {
        Launcher.SimulatedProjectiles.Remove(this);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Planet")) {
            Remove();
        }
    }
}
