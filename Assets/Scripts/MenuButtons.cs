using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour {

	public Button currentBtn; //reference to current game object clicked
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void onClick(){

		currentBtn = GetComponent<Button> (); //get game object clicked

		if (currentBtn.name == "PlayBtn") {  //loads game scene if play button is clicked
			SceneManager.LoadScene ("_Scene_Gameplay");
		} else if (currentBtn.name == "ExitBtn") { //quit if exit button is clicked

			Application.Quit ();
		}//end elseif
	}//end onClick



}//end class

