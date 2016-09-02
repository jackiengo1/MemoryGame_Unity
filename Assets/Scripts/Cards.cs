using UnityEngine;
using System.Collections;

public class Cards : MonoBehaviour  {

	private int type; //holds the type of card
	private static Vector3 pos; //the position of the card
	private float rotatingSpeed = 180f; //the amount to rotate
	public AudioSource sound; //holds reference to audio when player clicks card


	void Start(){

	}
		
	void Update(){

	}


	void OnMouseDown(){ //when mouse is pressed down

		sound = GetComponent<AudioSource> (); //get the sound component of the game object

		flipOver (); //flip the card over

		if (GameController.getNumOfCards () < 2) { //if there are less than 2 cards fliped
			this.gameObject.GetComponent<BoxCollider> ().enabled = false; //disable the card flipped
		}//end if

		clickSound (); //play sound
			
	}//end OnMouseDown




	private void clickSound(){ // play the sound when clicking a card
		if (!GameController.isTimerOn ()) {
			sound.Play ();
		}
	}//end clickSound






	public void initPos(Vector3 _pos){ //initialize the position of the card

		pos = _pos; //get position
		this.transform.position = pos; //set the position
	
	}//end initPos






	public void initType(int _type){

		type = _type; //set the type of the card
	}//end initType





	public int getType(){ //get the type of the card
		return type;
	}//end getType





	public void flipOver(){ //flips a card over

		if (GameController.getNumOfCards () < 2) { //only flip when less than 2 cards are flipped
			
			transform.Rotate (new Vector3 (0, rotatingSpeed, 0)); //flip the card

			GameController.incrementNumOfCards (); //increase the number of cards flipped

			if (GameController.getNumOfCards () == 1) { //if first card is flipped

				GameController.setSelectedCard (1, this.gameObject); //get reference to this card

			} else if (GameController.getNumOfCards () == 2) { //if second card is flipped

				GameController.setSelectedCard (2, this.gameObject); //get reference to this card

			}//end else if

		}//end if
			
	}//end flipover





	public void flipBackOfCard(){ //if the card 
		transform.Rotate (new Vector3 (0, 180, 0));
	}//end flipBackOfCard




}//end class
