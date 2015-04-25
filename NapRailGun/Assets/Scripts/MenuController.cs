using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {

	public GameObject pnlStart;
	public GameObject pnlMenu;

	public GameObject pnlPlayerSettings;
	public Text txtQuantity;
	public Slider sliderQuantity;

	public Toggle[] checkBoxes;
	public Text[] playerNames;
	public Text txtWarning;

	public GameSettings gameSettings;

	int menuCounter = 1;

	// Use this for initialization
	void Start () {
		pnlPlayerSettings.SetActive(false);
		txtWarning.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		switch(menuCounter){
			case 0:
				if(Input.GetKeyDown (KeyCode.KeypadEnter)){
					pnlStart.gameObject.SetActive(false);
					menuCounter = 1;
				}
				break;
		}
	}

	public void btnStartAction(){
		menuCounter = 2;
		pnlMenu.gameObject.SetActive(false);
		pnlPlayerSettings.SetActive(true);
	}

	public void btnQuitAction(){
		Application.Quit();
	}

	public void changeSliderAction(){
		txtQuantity.text = ""+sliderQuantity.value;
	}

	public void OnChangeCheckboxes(){
		for(int i = 0; i < checkBoxes.Length; i++){
				gameSettings.players[i].active = checkBoxes[i].isOn;
		}
	}

	public void OnChangeName(){
		for(int i = 0; i < playerNames.Length; i++){
			gameSettings.players[i].name = playerNames[i].text;
		}
	}

	public void btnStartGame(){
		int counter = 0;
		for(int i = 0; i < playerNames.Length; i++){
			gameSettings.players[i].name = playerNames[i].text;
			gameSettings.players[i].active = checkBoxes[i].isOn;
			if(checkBoxes[i].isOn){
				counter++;
			}
		}
		if(counter > 1){
			Application.LoadLevel(1);
		} else {
			txtWarning.gameObject.SetActive(true);
		}
	}

	public void btnBack(){
		menuCounter = 1;
		txtWarning.gameObject.SetActive(false);
		pnlMenu.gameObject.SetActive(true);
		pnlPlayerSettings.SetActive(false);
	}

}
