using UnityEngine;
using System.Collections;

public class EnergyBallScript : MonoBehaviour {

	public EnergyBallController controller;

	public float energyValue = 35;

	void Start () {
		
	}
	
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if(collision.collider.gameObject.GetComponent<RespawnScript>() == null)
		{
			return;
		}

			StatusControl script = collision.collider.gameObject.GetComponent<PlatformerCharacter2D>().statusScript;
			if(script != null) {
				script.addEnergy(energyValue);
			}

			Component[] colliders = gameObject.GetComponents<Collider2D> ();
			Destroy (colliders [0]);
			Destroy (colliders [1]);
			Destroy (gameObject.GetComponent<SpriteRenderer> ());

			gameObject.GetComponent<AudioSource>().Play();

			//Number stuff
			controller.maxBalls++;
			Destroy(gameObject, gameObject.GetComponent<AudioSource>().clip.length);
	}
}
