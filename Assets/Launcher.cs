using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Launcher : MonoBehaviour {
	private const float SimulationInterval = 0.05f;
	private const float LaunchForce = 2f;

	public static readonly List<PathSimulation> SimulatedProjectiles = new List<PathSimulation>();

	private SphereCollider _collider;
	private Rigidbody _projectilePrefab;
	private PathSimulation _pathSimulationPrefab;

	private float _lastSimulationTime;

	void Start () {
		_collider = GetComponent<SphereCollider>();
		_projectilePrefab = Resources.Load<Rigidbody>("Projectile");
		_pathSimulationPrefab = Resources.Load<PathSimulation>("PathSimulation");
	}

	public void Launch(Vector3 direction) {
		Vector3? startPoint = GetProjectileStart(direction);
		if (startPoint == null) {
			return;
		}

		Rigidbody projectile = Instantiate(_projectilePrefab, startPoint.Value, Quaternion.Euler(direction));
		projectile.GetComponent<Attractee>().Velocity = direction * LaunchForce;
	}

	public void SimulateLaunch(Vector3 direction) {
		if (_lastSimulationTime + SimulationInterval > Time.time) {
			return;
		}

		Vector3? startPoint = GetProjectileStart(direction);
		if (startPoint == null) {
			return;
		}

		PathSimulation projectile = Instantiate(_pathSimulationPrefab, startPoint.Value, Quaternion.Euler(direction));
		projectile.GetComponent<Attractee>().Velocity = direction * LaunchForce;
		SimulatedProjectiles.Add(projectile);
		_lastSimulationTime = Time.time;
	}

	public void CancelLaunchSimulation() {
		foreach (PathSimulation x in SimulatedProjectiles) {
			Destroy(x.gameObject);
		}
		SimulatedProjectiles.Clear();
	}

	private Vector3? GetProjectileStart(Vector3 direction) {
		float scale = Mathf.Max(transform.localScale.x, transform.localScale.z);
		float radius = _collider.radius * scale;
		if (direction.magnitude < radius) {
			return null;
		}

		return _collider.transform.position + (direction.normalized * radius) + direction.normalized;
	}
}
