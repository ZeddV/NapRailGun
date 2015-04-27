using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public float energyCost = 20;

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

	public Vector2 shootDirection;
	bool left;

	public StatusControl statusScript;
	public Platformer2DUserControl controlsScript;

	void Start()
	{
		statusScript = gameObject.GetComponent<PlatformerCharacter2D> ().statusScript;
		controlsScript = gameObject.GetComponent<Platformer2DUserControl>();
	}

	// Use this for initialization
	void Awake () {
		////Debug.Log ("Awake");
		beam = transform.Find("FirePoint/Beam").GetComponent<LineRenderer>();
		firePoint = transform.FindChild ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No firePoint? WHAT?!");
		}
		//statusScript = gameObject.GetComponent<PlatformerCharacter2D> ().statusScript;
		////Debug.Log ("WEAPON "+gameObject.GetComponent<PlatformerCharacter2D> ());
		//float i = Input.GetAxis (controlsScript.axisVertical);
		//Debug.Log (gameObject.GetComponent<PlatformerCharacter2D> ().tag +"\n\t"+i);

	}
	
	// Update is called once per frame
	void Update () {
		float horizontalDirection = Input.GetAxis (controlsScript.axisHorizontal);
		if(horizontalDirection > 0){
			left = true;
		} 
		else if(horizontalDirection < 0){
			left = false;
		}

		if(left){
			changeRotationOfFirePoint(0);
			shootDirection = new Vector2(1, 0);
		} else {
			changeRotationOfFirePoint(180);
			shootDirection = new Vector2(-1, 0);
		}
		float vertDirection = Input.GetAxis (controlsScript.axisVertical);
		if(vertDirection < 0){
			//Debug.Log ("Down");
			changeRotationOfFirePoint(270);
			shootDirection = new Vector2(0, -1.5f);
		} else if(vertDirection > 0){
			changeRotationOfFirePoint(90);
			shootDirection = new Vector2(0, 1.5f);
		}

		//Check Shoot
		if (fireRate == 0) {
			if (Input.GetButton (controlsScript.fire)) {
					//Debug.Log ("Fire");
					Shoot();
				}
		}
		else {
			if (Input.GetButton (controlsScript.fire) && Time.time > timeToFire) {
				timeToFire = Time.time + 1/fireRate;
				Shoot();
			}
		}

		//LaserBeam
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, shootDirection, 100);
		////Debug.Log ("Hitpoint: "+hit.point);
		beam.SetPosition(0, firePoint.position);
		beam.SetPosition(1, hit.point);
		beam.sortingLayerName = "UI";
	}
	
	void Shoot () {
		if(!statusScript.subEnergy(energyCost)) 
		{
			return;
		}

		Vector2 mousePosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);

		//direction = mousePosition-firePointPosition for mouse
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, shootDirection, 100);

		if(Time.time >= timeToSpawnEffect){
			Effect ();
			timeToSpawnEffect = Time.time + 1/effectSpawnRate;
		}

		/*//Debug.DrawLine (firePointPosition, (direction)*100, Color.cyan);
		if (hit.collider != null) {
			//Debug.DrawLine (firePointPosition, hit.point, Color.red);
			//Debug.Log ("We hit " + hit.collider.name + " and did " + Damage + " damage.");
		}*/
	}

	void Effect(){
		Transform bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation) as Transform;
		bullet.gameObject.GetComponent<MoveBullet> ().player = gameObject;
		////Debug.Log (bullet.gameObject.GetComponent<MoveBullet> ().player);
		Transform effectInstanst = Instantiate(bulletFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
		Destroy(effectInstanst.gameObject, 0.2f);
	}

	void changeRotationOfFirePoint(float z){
		var rotationVector = firePoint.rotation.eulerAngles;
		rotationVector.z = z;
		firePoint.rotation = Quaternion.Euler(rotationVector);
	}
}
