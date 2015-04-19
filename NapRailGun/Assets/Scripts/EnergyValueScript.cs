using UnityEngine;
using System.Collections;

public class EnergyValueScript : MonoBehaviour {

	public float energy; //0-100

	public bool add(float add) {
		energy += add;

		if (energy > 100) {
			energy = 100;
			Debug.Log("NADD " + energy);
			return false;
		}
		Debug.Log("ADD " + energy);

		return true;
	}

	public bool remove(float sub) {
		if (energy < sub) {
			Debug.Log("NLOSS " + energy);
			return false;
		}
		energy -= sub;
		Debug.Log("LOSS " + energy);
		return true;
	}
}
