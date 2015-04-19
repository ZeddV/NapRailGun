using UnityEngine;
using System.Collections;

public class MoveBullet : MonoBehaviour {

	public int moveSpeed = 230;
	public int damage = 30;

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
		//Debug.Log (transform.tag+" "+transform.name+"COLLISION WITH: "+collision.transform.tag);
		//Debug.Log ("Player: "+player.name);
		//Debug.Log (""+collision.gameObject.GetComponentInParent<Transform>().gameObject.name);
		if (collision.transform.tag != transform.tag && collision.transform.tag != player.tag) {
			Debug.Log ("TREFFER!"+collision.transform.name+" "+collision.transform.tag);
			StatusControl status = collision.transform.GetComponent<PlatformerCharacter2D>().statusScript;
			if(status != null){
				Debug.Log("DAMAGE!: "+damage);
				status.subHealth(damage);
			}
			Destroy (gameObject);
		}	

	}
}
