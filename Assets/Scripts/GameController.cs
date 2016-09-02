using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GameController : MonoBehaviour {

	public GameObject[] cardType; //holds the different unique card types
	static GameObject selectedCard1; //reference to the first flipped card
	static GameObject selectedCard2; //reference to the second flipped card
	public Text scoreTxt; //reference to the score text
	public Text timeTxt; //reference to the time text
	public Text gameOverTxt; //reference to the game over text
	public Button playAgainBtn; //get reference to the play again button 
	public Button exitBtn; //get reference to the exit button
	public AudioClip gameOverLose, gameOverWin, win, lose; //reference to 4 sounds


	public AudioSource soundAudio; //the audio sound
	private static int selectedCards = 0; //number of cards flipped
	private float gameTime = 0; //the time spent playing the game
	private float timer = 0; //holds the amount of time to wait when two cards are flipped before flipping them back
	private static int score; //holds the score
	private float counter = 0; //counts the time to 1 second
	private static float numOfCardsLeft; //holds the cards left in the game
	private static bool timerOn; //determines if timer is running

	Vector3 position; //used to position the cards
	float xPos = -0.5f; //the x position and y position of the first card
	float yPos = 0.2f;

	GameObject[] cardDeck; //holds the cards generated

	// Use this for initialization
	void Start () {

		playAgainBtn.gameObject.SetActive (false); //hide the play again and exit buttons
		exitBtn.gameObject.SetActive (false);
		Time.timeScale = 1; //run the game
		selectedCards = 0; //reset all variables
		numOfCardsLeft = 12;
		score = 1000;
		timerOn = false;

		cardDeck = new GameObject[12]; //create new list

		initDeck (); //initialize list with cards

		shuffleDeck (cardDeck); //shuffle the cards
		displayCards (); //display the cards
			

	}//end start


	
	// Update is called once per frame
	void Update () {

		scoreTxt.text = "Score: " + score; //update the score

		countGameTime (); // updates the time of the game

		if (selectedCards >= 2) { //if 2 or more cards are selected
			timerOn = true;
			if (timer >= 2) { //if 2 seconds has passed
				timerOn = false;
				faceCardsDown (); //put the cards faced down again
				timer = 0; //reset variables
				selectedCards = 0;

				if (checkCards ()) { //check to see if the cards match

					deletePair (); //if they do delete them and play sound
					winSound ();

					if (numOfCardsLeft <= 0) { //if there are no more cards in the game,player wins the game
						wonGame ();
					}//end if

				} else {

					loseRound (); //if cards dont match player lose round

					if (score <= 0) { //if the score is 0 player loses game
						loseGame ();
					}//end if

				}//end else


			} else { //if 2 seconds has not passed, wait until 2 seconds have passed
				timer += Time.deltaTime;
			}//end else
		}//end if
	
	}//end update




	private void loseRound(){ //when player loses the round

		score -= 40; 
		selectedCard1.GetComponent<BoxCollider> ().enabled = true; //enable the cards for clicking again
		selectedCard2.GetComponent<BoxCollider> ().enabled = true;
		loseSound ();

	}//end loseRound

	private void wonGame(){ //player wins the game

		gameOverTxt.text = "You Win!" + " Score: " + score + " Time elapsed: " + gameTime + " sec"; //display score and time elapsed
		playAgainBtn.gameObject.SetActive (true); //show the play again and exit buttons
		exitBtn.gameObject.SetActive (true);
		Time.timeScale = 0; //stop the game
		selectedCards = 2; //prevent player from clicking other cards
		gameOverWinSound (); //play the sound

	}//end wonGame


	private void loseGame(){ //player loses the game

		gameOverTxt.text = "You lose!" + " Score: " + score + " Time elapsed: " + gameTime + " sec"; //dsiplay score and time
		playAgainBtn.gameObject.SetActive (true); //show the play again and exit buttons
		exitBtn.gameObject.SetActive (true);
		Time.timeScale = 0; //stop the game
		selectedCards = 2; //prevents player from clicking other cards
		gameOverLoseSound (); //play sound

	}//end loseGame


	private void deletePair(){ //deletes matched pair

		selectedCard1.SetActive (false); //deactivate them
		selectedCard2.SetActive (false);
		numOfCardsLeft -= 2; //subtract 2 from cards left
	}//end deletePair



		


	private void displayCards(){

		for (int j = 0; j < 12; j++) {

			position.Set (xPos, yPos, 0); //change the position for each card
			cardDeck[j].GetComponent<Cards> ().initPos (position);
			xPos += 0.3f; //move the x position of each card

			if (j == 3 || j == 7) { //move to next row
				yPos -= 0.3f;
				xPos = -0.5f;
			}//end if
		}//end for

	}//end displayCards







	public static void incrementNumOfCards(){ //increase number of flipped cards by 1

		selectedCards++;
	}//end incrementNumOfCards





	public static bool isTimerOn(){ //tells if timer is running or not

		return timerOn;
	}




	public static int getNumOfCards(){ //get number of flipped cards
		return selectedCards;
	}//end getNumOfCards





	private void faceCardsDown(){ //put the cards face down

		selectedCard1.GetComponent<Cards> ().flipBackOfCard ();
		selectedCard2.GetComponent<Cards> ().flipBackOfCard ();
	}//end faceCardsDown





	public static void setSelectedCard(int cardNum, GameObject card){ //set the first 2 flipped cards

		if (cardNum == 1) {
			selectedCard1 = card;
		}//end if

		if (cardNum == 2) {
			selectedCard2 = card;
		}//end if
	}//end setSelectedCard






	public static bool checkCards(){ //check if the cards are the same type returns true or false

		if (selectedCard1.GetComponent<Cards>().getType () == selectedCard2.GetComponent<Cards>().getType ()) {
			return true;
		} else {
			return false;
		}
	}//end checkCards







	public void countGameTime(){ //counts the game time and updates the text


		timeTxt.text = "Time: " + gameTime +" sec";
		counter += Time.deltaTime; //counter holds the the time it takes to reach 1 second

		if (counter >= 1) {
			gameTime++;
			counter = 0;
		}//end if

	}//end countGameTime





	private void initDeck(){ //initialize the deck

		for (int i = 0; i < 6; i++) {

			cardDeck [2*i] = Instantiate (cardType [i]) as GameObject; //put 2 of each type of card in the deck
			cardDeck [2 * i].GetComponent<Cards> ().initType (i); //initialize the type of the card
			cardDeck [(2*i)+1] = Instantiate (cardType [i]) as GameObject;
			cardDeck [(2 * i) + 1].GetComponent<Cards> ().initType (i);
		}//end for

	}//end initDeck






	public static void shuffleDeck(GameObject[] deck){ //shuffle the deck

		for (int i = deck.Length - 1; i > 0; i--) { //randomly shuffles the deck
			int r = Random.Range(0,i);
			GameObject tmp = deck[i];
			deck[i] = deck[r];
			deck[r] = tmp;
		}//end for


	}//end shuffleDeck








	private void loseSound(){ //plays losing sound
		soundAudio.clip = lose; //sets the clip to be played
		soundAudio.Play ();
	}//end loseSound

	private void winSound(){ //plays winning sound
		soundAudio.clip = win;
		soundAudio.Play ();
	}//end winSound
		

	private void gameOverWinSound(){ //plays winning sound when game is over
		soundAudio.clip = gameOverWin;
		soundAudio.Play ();
	}//end gameOverSound

	private void gameOverLoseSound(){ //plays losing sound when game is over
		soundAudio.clip = gameOverLose;
		soundAudio.Play ();
	}//end gameOverLoseSound

}//end class
