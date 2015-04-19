using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {

	public MasterScript masterScript;
	public int numPlayers = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void change(int val){
		numPlayers += val;
		if (numPlayers == 1) {
			masterScript.setFinish (true);
		}
	}
}
