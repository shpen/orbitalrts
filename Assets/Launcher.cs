using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Launcher : MonoBehaviour {
	private const float SIMULATION_INTERVAL = 0.1f;
	private const float FORCE = 2f;

	public static readonly List<Rigidbody> SimulatedProjectiles = new List<Rigidbody>();

	private SphereCollider _collider;
	private Rigidbody _projectilePrefab;
	private Rigidbody _pathSimulationPrefab;

	private float _lastSimulationTime;

	// Use this for initialization
	void Start () {
		_collider = GetComponent<SphereCollider>();
		_projectilePrefab = Resources.Load<Rigidbody>("Projectile");
		_pathSimulationPrefab = Resources.Load<Rigidbody>("PathSimulation");
	}

	public void Launch(Vector3 direction) {
		Vector3? startPoint = GetProjectileStart(direction);
		if (startPoint == null) {
			return;
		}

		Rigidbody projectile = Instantiate(_projectilePrefab, startPoint.Value, Quaternion.Euler(direction));
		projectile.GetComponent<Attractee>().velocity = direction * FORCE;
		//projectile.AddForce(direction * FORCE, ForceMode.Impulse);
	}

	public void SimulateLaunch(Vector3 direction) {
		if (_lastSimulationTime + SIMULATION_INTERVAL > Time.time) {
			return;
		}

		Vector3? startPoint = GetProjectileStart(direction);
		if (startPoint == null) {
			return;
		}

		Rigidbody projectile = Instantiate(_pathSimulationPrefab, startPoint.Value, Quaternion.Euler(direction));
		//projectile.AddForce(direction * FORCE * PathSimulation.speedSqrt, ForceMode.Impulse);
		projectile.GetComponent<Attractee>().velocity = direction * FORCE;// * PathSimulation.speed;
		SimulatedProjectiles.Add(projectile);
		_lastSimulationTime = Time.time;
	}

	public void CancelLaunchSimulation() {
//		foreach (Rigidbody rb in SimulatedProjectiles) {
//			Destroy(rb.gameObject);
//		}
	}

	private Vector3? GetProjectileStart(Vector3 direction) {
		float scale = Mathf.Max(transform.localScale.x, transform.localScale.z);
		float radius = _collider.radius * scale;
		if (direction.magnitude < radius) {
			return null;
		}

		return _collider.transform.position + (direction.normalized * radius) + direction.normalized;
	}

//	public List<Vector3> GetTrajectory(Vector3 direction, float timePerSegmentInSeconds, float maxTravelDistance) {
//		var positions = new List<Vector3>();
//		Vector3? startPoint = GetProjectileStart(direction);
//		if (startPoint == null) {
//			return positions;
//		}
//
//		var currentPos = startPoint.Value;
//		positions.Add(currentPos);
//
//		float speed = direction.magnitude / 100f;
//
//		var traveledDistance = 0.0f;
//		while(traveledDistance < maxTravelDistance) {
//			traveledDistance += speed * timePerSegmentInSeconds;
//			var hasHitSomething = TravelTrajectorySegment(currentPos, direction, speed, timePerSegmentInSeconds, positions);
//			if (hasHitSomething)
//			{
//				break;
//			}
//			var lastPos = currentPos;
//			currentPos = positions[positions.Count - 1];
//			direction = currentPos - lastPos;
//			direction.Normalize();
//		}
//
//		return positions;
//	}
//
//	private bool TravelTrajectorySegment(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, List<Vector3> positions) {
//		Vector3 force = Attractor.GetTotalAttractionForce(startPos, _projectilePrefab.mass);
//		var newPos = startPos + direction * speed * timePerSegmentInSeconds + force * timePerSegmentInSeconds;
//
//		RaycastHit hitInfo;
//		var hasHitSomething = Physics.Linecast(startPos, newPos, out hitInfo);
//		if (hasHitSomething) {
//			newPos = hitInfo.transform.position;
//		}
//		positions.Add(newPos);
//
//		return hasHitSomething;
//	}
//
//	private bool TravelTrajectorySegment(Vector3 startPos, Vector3 direction, float speed, float timePerSegmentInSeconds, List<Vector3> positions) {
//		Vector3 force = Attractor.GetTotalAttractionForce(startPos, _projectilePrefab.mass);
//		//var newPos = startPos + direction * speed * timePerSegmentInSeconds + force * timePerSegmentInSeconds;
//		var newPos =
//
//		RaycastHit hitInfo;
//		var hasHitSomething = Physics.Linecast(startPos, newPos, out hitInfo);
//		if (hasHitSomething) {
//			newPos = hitInfo.transform.position;
//		}
//		positions.Add(newPos);
//
//		return hasHitSomething;
//	}
}
