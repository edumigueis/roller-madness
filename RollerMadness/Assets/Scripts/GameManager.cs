using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	public static GameManager gm;

	[Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
	public GameObject player;
	private GameObject boss;

	public enum gameStates {Playing, Death, GameOver, BeatLevel};
	public gameStates gameState = gameStates.Playing;

	public int score=0;
	public bool canBeatLevel = false;
	public int beatLevelScore=0;

	public enum levels {Fase1, Fase2};
	public levels level = levels.Fase1;


	public GameObject mainCanvas;
	public Text mainScoreDisplay;
	public GameObject gameOverCanvas;
	public Text gameOverScoreDisplay;

	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public GameObject beatLevelCanvas;

	public AudioSource backgroundMusic;
	public AudioClip gameOverSFX;

	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public AudioClip beatLevelSFX;

	private Health playerHealth;
	private Health bossHealth;

	void Start () {
		if (gm == null) 
			gm = gameObject.GetComponent<GameManager>();

		if (player == null) {
			player = GameObject.FindWithTag("Player");
		}

		if (level == levels.Fase2) {
			boss = GameObject.FindWithTag("Boss");
			bossHealth = boss.GetComponent<Health>();
		}


		playerHealth = player.GetComponent<Health>();

		string playerDifficulty = PlayerPrefs.GetString("Difficulty");

        switch(playerDifficulty)
		{
			case "Rookie":
				playerHealth.numberOfLives = 3;
				if (level == levels.Fase1) {
					beatLevelScore = 5;
				}
				else if (level == levels.Fase2) {
					beatLevelScore = 10;
					bossHealth.healthPoints = 10f;
				}
				Debug.Log("Rookie");
				break;
			case "Easy":
				playerHealth.numberOfLives = 2;
				if (level == levels.Fase1) {
					beatLevelScore = 7;
				}
				else if (level == levels.Fase2) {
					beatLevelScore = 15;
					bossHealth.healthPoints = 15f;
				}
				Debug.Log("Easy");
				break;
			case "Normal":
				playerHealth.numberOfLives = 1;
				if (level == levels.Fase1) {
					beatLevelScore = 10;
				}
				else if (level == levels.Fase2) {
					beatLevelScore = 25;
					bossHealth.healthPoints = 25f;
				}
				Debug.Log("Normal");
				break;
			case "Hard":
				playerHealth.numberOfLives = 1;
				if (level == levels.Fase1) {
					beatLevelScore = 15;
				}
				else if (level == levels.Fase2) {
					beatLevelScore = 35;
					bossHealth.healthPoints = 35;
				}
				Debug.Log("Hard");
				break;
		}

		// setup score display
		Collect (0);

		// make other UI inactive
		gameOverCanvas.SetActive (false);
		if (canBeatLevel)
			beatLevelCanvas.SetActive (false);
	}

	void Update () {
		switch (gameState)
		{
			case gameStates.Playing:
				if (playerHealth.isAlive == false)
				{
					// update gameState
					gameState = gameStates.Death;

					// set the end game score
					gameOverScoreDisplay.text = mainScoreDisplay.text;

					// switch which GUI is showing		
					mainCanvas.SetActive (false);
					gameOverCanvas.SetActive (true);
				} else if (canBeatLevel && score>=beatLevelScore) {
					// update gameState
					gameState = gameStates.BeatLevel;

					// hide the player so game doesn't continue playing
					player.SetActive(false);

					// switch which GUI is showing			
					mainCanvas.SetActive (false);
					beatLevelCanvas.SetActive (true);
				}
				break;
			case gameStates.Death:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume<=0.0f) {
					AudioSource.PlayClipAtPoint (gameOverSFX,gameObject.transform.position);

					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.BeatLevel:
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume<=0.0f) {
					AudioSource.PlayClipAtPoint (beatLevelSFX,gameObject.transform.position);
					
					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.GameOver:
				// nothing
				break;
		}

	}


	public void Collect(int amount) {
		score += amount;
		if (canBeatLevel) {
			mainScoreDisplay.text = score.ToString () + " of "+beatLevelScore.ToString ();
		} else {
			mainScoreDisplay.text = score.ToString ();
		}

	}
}
