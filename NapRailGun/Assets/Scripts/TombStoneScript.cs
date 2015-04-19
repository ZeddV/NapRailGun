using UnityEngine;
using System.Collections;

public class TombStoneScript : MonoBehaviour {

	public GameObject player;
	public RespawnScript respawnScript;

	// Use this for initialization
	void Start () {
		transform.position = player.transform.position;

		if (!(Physics.Raycast (transform.position, Vector3.up, 1.1f))) {
			transform.position.Set(transform.position.x, transform.position.y + 1f, transform.position.z);
		}
	}

	void awakePlayer() {
		respawnScript.change (1);
		player.SetActive (true);
		Destroy (gameObject);
	}

	public void work() {
		Invoke ("awakePlayer", 7f);
	}
	
	
	// Update is called once per frame
	void Update () {
	
	}
}
