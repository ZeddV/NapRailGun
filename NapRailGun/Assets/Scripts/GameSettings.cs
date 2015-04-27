using UnityEngine;
using System.Collections;

public class GameSettings : MonoBehaviour {

	public Player[] players = new Player[4]; 

	void Awake(){
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		players[0] = new Player("Player 1", 1, true);
		players[1] = new Player("Player 2", 2, true);
		players[2] = new Player("Player 3", 3, false);
		players[3] = new Player("Player 4", 4, false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public class Player{
		public int playerNr;
		public string name;
		public bool active;

		public Player(string name, int playerNr, bool active){
			this.name = name;
			this.playerNr = playerNr;
			this.active = active;
		}
	}
}
