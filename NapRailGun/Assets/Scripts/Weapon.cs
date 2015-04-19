using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float fireRate = 0;
	public float Damage = 10;
	public LayerMask whatToHit;
	public Transform bulletPrefab;
	public Transform bulletFlashPrefab;

	public float effectSpawnRate = 1;
	public string fireButton;

	float timeToSpawnEffect = 0;
	
	float timeToFire = 0;
	Transform firePoint;
	LineRenderer beam;

	Vector2 shootDirection;
	bool left;

	// Use this for initialization
	void Awake () {
		Debug.Log ("Awake");
		beam = transform.Find("FirePoint/Beam").GetComponent<LineRenderer>();
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.LeftArrow)){
			left = true;
		} 
		else if(Input.GetKeyDown (KeyCode.RightArrow)){
			left = false;
		}

		if(left){
			shootDirection = new Vector2(-1, 0);
		} else {
			shootDirection = new Vector2(1, 0);
		}
		if(Input.GetKey(KeyCode.DownArrow)){
			Debug.Log ("Down");
			shootDirection = new Vector2(0, -1.5f);
		} else if(Input.GetKey(KeyCode.UpArrow)){
			shootDirection = new Vector2(0, 1.5f);
		}

		//Check Shoot
		if (fireRate == 0) {
			if (Input.GetAxis (fireButton)> 0.2f) {
				Debug.Log ("Fire");
				Shoot();
			}
		}
		else {
			if (Input.GetButton (fireButton) && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}

		//LaserBeam
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, shootDirection, 100);
		//Debug.Log ("Hitpoint: "+hit.point);
		beam.SetPosition(0, firePoint.position);
		beam.SetPosition(1, hit.point);
	}
	
	void Shoot () {

		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		Debug.Log ("direction"+(mousePosition-firePointPosition));
		//direction = mousePosition-firePointPosition for mouse
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, shootDirection, 100);

		if(Time.time >= timeToSpawnEffect){
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}

		/*Debug.DrawLine (firePointPosition, (direction)*100, Color.cyan);
		if (hit.collider != null) {
			Debug.DrawLine (firePointPosition, hit.point, Color.red);
			Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
		}*/
	}

	void Effect(){
		Transform bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as Transform;
		bullet.gameObject.GetComponent<MoveBullet> ().player = gameObject;
		Debug.Log (bullet.gameObject.GetComponent<MoveBullet> ().player);
		Transform effectInstanst = Instantiate(bulletFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		Destroy(effectInstanst.gameObject, 0.2f);
	}
}
