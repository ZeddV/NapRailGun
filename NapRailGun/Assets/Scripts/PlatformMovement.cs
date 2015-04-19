using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlatformMovement : MonoBehaviour {

	public bool loop = false;
	public List<Transform> positions = new List<Transform>();
	public List<float> speeds = new List<float>();

	//private List<Transform> tilesTransforms;
	private bool forward;
	private int currentIteration;
	private bool singlePos;

	private Transform lastPosition;
	private Transform targetPosition;


	void Start () {
		forward = true;
		currentIteration = 1;
		singlePos = false;
		//tilesTransforms = gameObject.GetComponentsInChildren<Transform>();
		if (positions.Capacity > 1) {
			singlePos = false;

			lastPosition = positions[0];
			targetPosition = positions[1];
		}
	}
	
	void Update () {
		if (singlePos)
			return;

		//TODO IF NEEDED
		/*if (transform.position.Equals (targetPosition.position)) {
			if(currentIteration == 
			currentIteration += forward ? 1 : -1;
		}

		transform.position = Vector2.Lerp(startPoint, endPoint, (Time.time - startTime) / duration);
		*/

	}
}
