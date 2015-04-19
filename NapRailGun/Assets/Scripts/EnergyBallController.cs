using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class EnergyBallController : MonoBehaviour {

	public int maxBalls = 6;

	public List<GameObject> tiles = new List<GameObject>();

	public GameObject prefab;

	//private bool initialized = true;

	void Start () {
		//StartCoroutine ("Timer");
		//Invoke ("initializeTiles", 0.2f);
			//yield return new WaitForSeconds(3f);
		Invoke("createBalls", Random.Range(3f, 5f));

	}

	IEnumerable Timer(){
		while (true) {
			Debug.Log ("INIT");

			 //yield return new WaitForSeconds(3f);
			Invoke("createBalls", Random.Range(3f, 5f));
						
		}
	}

	/*void initializeTiles ()
	{
		Debug.Log ("INIT");

		GameObject[] allObjects =  Object.FindObjectsOfType <GameObject>();
		foreach(GameObject obj in allObjects) {
			if (obj.layer == 8 && (obj.GetComponent<BoxCollider2D>() != null || obj.GetComponent<PolygonCollider2D>())) {
				Debug.Log("Layer " +obj);
				tiles.Add(obj);
			}
		}

	}*/
	
	void createBalls ()
	{
		Debug.Log ("BALLS");
		/*if (!initialized) {
			initializeTiles ();
			initialized = true;
		}*/

		int dummyMax = maxBalls;
		Vector3 position = new Vector3 (0, 0, 0);
		for (int i = 0; i < dummyMax; i++) {

			do {
				position.Set(Random.Range(-12, 12),Random.Range(-12, 12),0);
			} while(insideSomething(position));


			GameObject obj = Instantiate(prefab, position, transform.rotation) as GameObject;
			obj.GetComponent<EnergyBallScript>().controller = this;
			obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(100*Mathf.Sign(Random.Range(1,1)), 0));
			//Debug.Log(obj.GetComponent<EnergyBallScript>().controller);
			maxBalls--;

		}
		Debug.Log ("Repeat");
		Invoke("createBalls", Random.Range(3f, 5f));

	}

	bool insideSomething (Vector3 position)
	{
		if (Physics.Raycast (position, Vector3.left, 0.55f)) {
			return true;
		}
		if (Physics.Raycast (position, Vector3.right, 0.55f)) {
			return true;
		}
		if (Physics.Raycast (position, Vector3.up, 0.55f)) {
			return true;
		}
		if (Physics.Raycast (position, Vector3.down, 0.55f)) {
			return true;
		}

		return false;
	}

	void Update () {
	
	}
}
