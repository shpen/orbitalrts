using System.Collections;
using System.Collections.Generic;
using cakeslice;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PlayerController : MonoBehaviour {
	private Camera _mainCamera;
	private LineRenderer _lineRenderer;

	private GameObject _oldObject;
	private Launcher _oldLauncher;

	// Use this for initialization
	void Start () {
		_mainCamera = Camera.main;
		_lineRenderer = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update () {
		Ray mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);

		// Find the mouse point at the ground plane
		Plane ground = new Plane(Vector3.up, Vector3.zero);
		float distance;
		ground.Raycast(mouseRay, out distance);
		Vector3 groundPoint = mouseRay.GetPoint(distance);

		// While we are dragging a line from a launcher
		if (Input.GetMouseButton(0) && _oldLauncher != null) {
			//_lineRenderer.enabled = true;
//			_lineRenderer.SetPositions(new []{
//				_oldObject.transform.position, groundPoint
//			});

//			List<Vector3> positions = _oldLauncher.GetTrajectory(_oldObject.transform.position - groundPoint, 0.1f, 10);
//			DrawTrajectory(positions);
			_oldLauncher.SimulateLaunch(_oldObject.transform.position - groundPoint);

			return;
		}
		_lineRenderer.enabled = false;

		// After finishing dragging a line
		if (Input.GetMouseButtonUp(0) && _oldLauncher != null) {
			Launcher launcher = (Launcher)_oldObject.GetComponent(typeof(Launcher));
			launcher.CancelLaunchSimulation();
			launcher.Launch(_oldObject.transform.position - groundPoint);
		}

		// Find what game object we're pointing at
		GameObject newObject = null;
		RaycastHit mouseHit;
		if (Physics.Raycast(mouseRay, out mouseHit, Mathf.Infinity)) {
			newObject = mouseHit.transform.gameObject;
		}

		if (newObject != _oldObject) {
			if (_oldObject != null) {
				Outline outline = (Outline)_oldObject.GetComponent(typeof(Outline));
				outline.enabled = false;
			}
			if (newObject != null) {
				Outline outline = (Outline)newObject.GetComponent(typeof(Outline));
				outline.enabled = true;
				_oldLauncher = (Launcher)newObject.GetComponent(typeof(Launcher));
			} else {
				_oldLauncher = null;
			}
			_oldObject = newObject;
		}
	}




	void DrawTrajectory(List<Vector3> positions) {
		_lineRenderer.positionCount = positions.Count;
		_lineRenderer.SetPositions(positions.ToArray());
	}
}
