using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MasterScript : MonoBehaviour {

	public int playerQuantity = 4;
	public bool pinkMode = false;
	public GameObject prefabPlayer;
	public GameObject prefabStatus;
	public Transform canvasStatus;
	public Text txtMid;

	bool PlayerAuswahl = true;
	bool finish = false; 
	bool begin = true;
	bool timebegin = false;
	bool pause = false;
	float tempTime; 
	int secondsOnBegin = 1;

	GameObject grayLayer;
	GameObject pinkLayer;
	ArrayList players = new ArrayList();
	ArrayList playerStatus = new ArrayList();
	Vector3[] playerStatusPosition = new Vector3[4];

	// Use this for initialization
	void Start () {
		playerStatusPosition[0] = new Vector3(-681.5f, -129.6f, 0);
		playerStatusPosition[1] = new Vector3(-373.96f, -129.6f, 0);
		playerStatusPosition[2] = new Vector3(-681.5f, -254f, 0);
		playerStatusPosition[3] = new Vector3(-373.96f, -254f, 0);
		pinkLayer = GameObject.Find ("PinkLayer");
		grayLayer = GameObject.Find ("GrayLayer");
		if(!pinkMode){
			pinkLayer.SetActive(false);
		}
		GameObject panel;
		for(int i = 0; i < playerQuantity; i++){
			panel = Instantiate (prefabStatus, playerStatusPosition[i], prefabStatus.transform.rotation ) as GameObject;
			panel.transform.parent = canvasStatus;
			panel.GetComponent<RectTransform>().anchoredPosition = playerStatusPosition[i];
			panel.GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if(begin){
			Time.timeScale = 0;
			if(!timebegin){
				tempTime = Time.unscaledTime;
				timebegin = true;
			} else{
				if(Time.unscaledTime > tempTime+2){
					secondsOnBegin += 1;
					tempTime = Time.unscaledTime;
					txtMid.text = secondsOnBegin+"";
					if(secondsOnBegin == 4){
						txtMid.text = "Fight!";
						Time.timeScale = 1;
						grayLayer.SetActive(false);
					} else if(secondsOnBegin == 5){
						begin = false;
						Time.timeScale = 1;
						txtMid.transform.gameObject.SetActive(false);
					}
				}
			}
		}
		if(finish){

		}
		if(!begin){
			if(Input.GetKeyDown(KeyCode.P)){
				if(pause){
					pause = false;
					Time.timeScale = 1;
					grayLayer.SetActive(false);
					txtMid.transform.gameObject.SetActive(false);
				} else {
					pause = true;
					Time.timeScale = 0;
					grayLayer.SetActive(true);
					txtMid.transform.gameObject.SetActive(true);
					txtMid.text = "Pause";
				}
			}
		}



	}


}
