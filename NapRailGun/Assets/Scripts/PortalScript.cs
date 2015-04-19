using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {

	public bool active = true;

	public static float minVelocity = 18;
		
	public GameObject linkedPortal;
	private Direction linkedDirection;
	private Vector3 scaledLinkedDirection;
	private PortalScript linkedPortalScript;

	public Direction exitDirection;
	public Vector3 direction;



	public enum Direction{LEFT, RIGHT, UP, DOWN};

	public Direction getExitDirection() {
		return exitDirection;
	}

	// Use this for initialization
	void Start () {
		switch (exitDirection) {
			case Direction.LEFT:
			direction = new Vector3(-1, 0, 0);
			break;		

			case Direction.RIGHT:
			direction = new Vector3(1, 0,0);
			break;		

			case Direction.UP:
			direction = new Vector3(0, 1,0);
			break;	

			case Direction.DOWN:
			direction = new Vector3(0, -1,0);
			break;		
		}

		if (!active)
			return;

		linkedPortalScript = linkedPortal.GetComponent<PortalScript> ();
		////Debug.Log ("LPS " + linkedPortalScript);
		//scaledLinkedDirection = linkedDirection * 0.55f;



		////Debug.Log (direction);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider)
	//void OnCollisionEnter2D(Collision2D collision)
	{
		//Collider2D collider = collision.collider;
		if (!active)
			return;

		//Debug.Log ("Coll");
		linkedDirection = linkedPortalScript.getExitDirection();

		GameObject player = collider.gameObject;
		Vector3 playerPosition = player.transform.position;
		Vector3 velocity = player.GetComponent<Rigidbody2D> ().velocity;
		Vector3 offsetVector = playerPosition - transform.position;

		//Debug.Log ("V " + velocity);

		
		float velInMain = 0, velInSec = 0;
		float offInMain = 0;


		switch (exitDirection) {
		case Direction.LEFT:
			velInMain = velocity.x;
			velInSec = -velocity.y;

			offInMain = -offsetVector.x;
			break;		
			
		case Direction.RIGHT:
			velInMain = -velocity.x;
			velInSec = velocity.y;

			offInMain = offsetVector.x;

			break;		
			
		case Direction.UP:
			velInMain = -velocity.y;
			velInSec = -velocity.x;

			offInMain = offsetVector.y;
			break;	

		case Direction.DOWN:
			velInMain = velocity.y;
			velInSec = velocity.x;

			offInMain = -offsetVector.y;
			break;		
		}
		//Debug.Log (offInMain);


		
		Vector3 linkedPosition = linkedPortal.transform.position;
		float boundsX = player.GetComponent<SpriteRenderer>().bounds.size.x / 2 * 1.1f;
		float boundsY = player.GetComponent<SpriteRenderer>().bounds.size.y / 2 * 1.05f;

		float posX = linkedPosition.x;
		float posY = linkedPosition.y;
		////Debug.Log (posY);
		float posZ = linkedPosition.z;

		////Debug.Log (velocity);

		offInMain *= 1.05f;
		//Debug.Log ("1 "+player.transform.position);
		switch (linkedDirection) {
		case Direction.LEFT:
			posY += offsetVector.y;
			posX -= offInMain;

			player.transform.position = new Vector3(posX, posY, posZ);

			velocity.x = -velInMain;
			velocity.y = velInSec;

			break;		
			
		case Direction.RIGHT:
			posY += offsetVector.y;
			posX += offInMain;

			player.transform.position = new Vector3(posX, posY, posZ);

			velocity.x = velInMain;
			velocity.y = -velInSec;

			break;		
			
		case Direction.UP:
			posY += offInMain;
			posX += offsetVector.x;
			
			player.transform.position = new Vector3(posX, posY, posZ);

			velocity.y = velInMain;
			velocity.x = -velInSec;

			velocity.y = Mathf.Abs(velocity.y) < minVelocity ? minVelocity * Mathf.Sign (velocity.y) : velocity.y;
			break;	
			
		case Direction.DOWN:
			posY -= offInMain;
			posX += offsetVector.x;
			
			player.transform.position = new Vector3(posX, posY, posZ);

			velocity.y = -velInMain;
			velocity.x = velInSec;

			break;		
		}

		//Debug.Log ("2 " + player.transform.position);

		player.GetComponent<Rigidbody2D> ().velocity = velocity;
		////Debug.Log (velocity);

		
	}

	/*void OnTriggerEnter2D(Collider2D collider)
	{
		if (!active)
			return;

		linkedDirection = linkedPortalScript.getDirection();
		////Debug.Log ("LDS " +linkedDirection);

		//Debug.Log (collider.gameObject.name);
		GameObject player = collider.gameObject;

		Vector3 scaledBoundsSize = new Vector3 (
			collider.bounds.size.x,
			collider.bounds.size.y,
			collider.bounds.size.z
			);
		scaledBoundsSize.Scale (scaledLinkedDirection);
		////Debug.Log ("SBS " +scaledBoundsSize);

		Vector3 offset = transform.position - player.transform.position;

		//multiplyRespectToZero (offset, linkedDirection);
		changeDirection (offset, linkedDirection);
		multiplyZeroAsMinusOne (offset, linkedDirection);



		////Debug.Log (

		Vector3 destination = linkedPortal.transform.position + scaledBoundsSize - offset;
		////Debug.Log (destination);

		////Debug.Log ("Setting position...");

		player.transform.position = destination; //destination.x, destination.y, destination.z);

		////Debug.Log ("Changing direction...");

		changeDirection (player.GetComponent<Rigidbody2D> ().velocity, linkedDirection);

	}

	void changeDirection(Vector3 velocity, Vector3 direction) {
		Vector3 factor = divideAndNorm(velocity, direction);

		multiplyRespectToZero (velocity, factor);
	}

	void multiplyRespectToZero(Vector3 target, Vector3 factor) {
		target.Set (
			factor.x != 0 ? target.x * factor.x : target.x,
			factor.y != 0 ? target.y * factor.y : target.y,
			target.z);
	}

	void multiplyZeroAsMinusOne (Vector3 target, Vector3 factor)
	{
		target.Set (
			factor.x == 0 ? target.x * -1 : target.x,
			factor.y == 0 ? target.y * -1 : target.y,
			target.z);
	}

	Vector3 divideAndNorm(Vector3 zaehler, Vector3 nenner) {
		Vector3 result = new Vector3 (
			nenner.x != 0 ? zaehler.x / nenner.x : 0,
			nenner.y != 0 ? zaehler.y / nenner.y : 0,
			zaehler.z);
		result.Normalize ();
		return result;
	}*/
}
