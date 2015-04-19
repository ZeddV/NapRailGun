using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {

	public int moveSpeed = 230;
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
		Destroy (gameObject,10);
	}
}
