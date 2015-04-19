using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {

	public int moveSpeed = 230;

	public GameObject player;

	void start() {
		Debug.Log ("collision");

	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
		Destroy (gameObject,10);
	}

	void OnCollisionEnter2D (Collision2D collision) {
		//Debug.Log (player);
		//Debug.Log (collision.gameObject.GetComponentInParent<Transform>().gameObject.name);
		if (!(collision.gameObject == gameObject || collision.gameObject == collision.gameObject.GetComponentInParent<Transform>().gameObject)) {
			Destroy (gameObject);
		}	
	}
}
