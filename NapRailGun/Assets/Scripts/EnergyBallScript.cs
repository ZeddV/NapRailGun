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

		if (collision.collider.gameObject.tag.Equals ("Player")) {
			//UpdatePlayer
			StatusControl script = collision.collider.gameObject.GetComponent<PlatformerCharacter2D>().statusScript;
			if(script != null) {
				script.addEnergy(energyValue);
			}

			gameObject.GetComponent<AudioSource>().Play();

			//Number stuff
			controller.maxBalls++;
			Destroy(gameObject);
		}
	}
}
