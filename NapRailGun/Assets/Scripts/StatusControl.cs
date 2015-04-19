using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StatusControl : MonoBehaviour {

	public Image healthFill; 
	public Image energyFill;
	public Text txtName;
	
	float energy = 100f;
	float health = 100f;

	void Awake(){

	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool subEnergy(float sub){
		if (energy < sub) {
			////Debug.Log("NLOSS " + energy);
			return false;
		}
		energy -= sub;

		energyFill.fillAmount = (1f/100f)*this.energy;

		return true;
	}

	public bool addEnergy(float energy){
		bool ret = true;
		this.energy += energy;
		if(this.energy > 100){
			this.energy = 100;
			//Debug.Log("NADD " + energy);
			ret = false;
		} else {
			//Debug.Log("ADD " + energy);
		}

		energyFill.fillAmount = (1f/100f)*this.energy;
		return ret;
	}

	public bool subHealth(float health){
		this.health -= health;
		if(this.health <= 0){
			this.health = 0;
			healthFill.fillAmount = (1f/100f)*this.health;
			return false;
		}
		healthFill.fillAmount = (1f/100f)*this.health;
		return true;
	}
	
	public void addHealth(float health){
		this.health += health;
		if(this.health > 100){
			this.health = 100;
		}
		healthFill.fillAmount = (1f/100f)*this.health;
	}

	public void die(RespawnScript respawnScript, GameObject tombstonePrefab) {
		respawnScript.change (-1);
		
		GameObject tombStone = Instantiate (tombstonePrefab, transform.position, transform.rotation) as GameObject;
		TombStoneScript script = tombStone.GetComponent<TombStoneScript> ();
		script.player = gameObject;
		script.respawnScript = respawnScript;
		script.work ();
		
		gameObject.SetActive (false);
	}

	float getHealth() {
		return health;
	}

	float getEnergy(){
		return energy;
	}

	void setName(string name){
		txtName.text = name;
	}
}
