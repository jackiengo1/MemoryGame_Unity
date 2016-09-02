using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameButtons : MonoBehaviour {

	Button currentBtn; //reference to button pressed

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick(){

		currentBtn = GetComponent<Button> (); //get which button is pressed

		if (currentBtn.name == "PlayAgainBtn") {
			
			SceneManager.LoadScene ("_Scene_Gameplay"); //restart the game if play again button is pressed

		} else if (currentBtn.name == "ExitBtn") { //quit the application if exit button is pressed

			Application.Quit ();

		} else if (currentBtn.name == "RestartBtn") { //restart the game if restart button is pressed
			
			SceneManager.LoadScene ("_Scene_Gameplay");

		}

	}//end onClick
}//end class
