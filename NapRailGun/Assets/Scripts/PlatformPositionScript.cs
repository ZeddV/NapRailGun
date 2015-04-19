using UnityEngine;
using System.Collections;

public class PlatformPositionScript : MonoBehaviour {

	public GameObject platform;
	// Use this for initialization
	void Start () {
		//Set relative position (to platform) to absolute position
		if (platform != null) {
			float newX = transform.position.x + platform.transform.position.x;
			float newY = transform.position.y + platform.transform.position.y;

			transform.position.Set(newX, newY, transform.position.z);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
