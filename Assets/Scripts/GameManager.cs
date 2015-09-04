using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class GameManager : MonoBehaviour {

	public MiniGame[] games;
	public GameObject MainCamera;
	public ScoringUI ScoreUI;
	public MainMenu mainMenu;
	public static bool ThereIsAGame = false;
	public int realScore = -1;
	public StagePlace currentStage;
	public bool resetMinigameStats = false;
	//===================================
	private int MinigamesFinished = 0;
	private MiniGame currentMinigame;
	private List<string> playedGamesTitle = new List<string>();
	private int gameNumber = -1;


	#region "Region concerning minigame triggers"

	/// <summary>
	/// Signals the end of game.
	/// </summary>
	/// <param name="difficulty">Difficulty of the ended game</param>
	/// <param name="scoreadd">Score that will be added</param>
	/// <param name="status">0 for lose, 1 for win.</param>
	public void SignalEndOfGame(Difficulty difficulty, int scoreadd, bool win){
		Debug.Log (currentMinigame.title + " has ended");
		Destroy(currentMinigame.gameObject);
		ScoreUI.score += scoreadd;
		if(win)
			GamesStatus.UpdateMinigameStats (currentMinigame.title, difficulty, true);
		else
			ScoreUI.DecreaseHealth();
		//TODO Gameover if no health
		MinigamesFinished++;
		ScoreUI.gameObject.SetActive (true);
	}

	void OnEnable () {
		//Create games status if none exist
		Debug.Log ("Started");
		mainMenu.gameObject.SetActive (true);
	}

	/// <summary>
	/// Is called after score counting is done in the score UI
	/// </summary>
	public void ScoreCountingEnded(){
		ScoreUI.gameObject.SetActive (false);
		NewGame();
	}

	IEnumerator wait(){
		yield return new WaitForSeconds(2f);
	}

	public void DisableCamera(){
		MainCamera.SetActive(false);
	}

	public void EnableCamera(){
		MainCamera.SetActive(true);
	}

	public void SetCurrentStageIn(StagePlace stage){
		currentStage = stage;
	}
	#endregion

	#region "Region concerning game management" 
	public void NewGame(){
		int RandomUpperBound = games.GetLength(0);
		gameNumber = GetGameNumber (RandomUpperBound);
		//Show Title
		ThereIsAGame = true;
		TitleProcedure(gameNumber);
		//Show Demo
		StartGame (gameNumber);
	}

	IEnumerator TitleProcedure(int gameNumber){
		if (GetDifficulty() != Difficulty.Impossible){
			GameObject titleUI = Instantiate(GetGame(gameNumber).TitleCard);
			yield return new WaitForSeconds(1f);
			DestroyObject (titleUI);
		}
		//GetGame (gameNumber).TitleCard.SetActive (false);
	}

	void ShowScoreUI(){
		updateUI();
//		ScoreUI.enabled = true;
	}

	void HideScoreUI(){
//		ScoreUI.enabled = false;
	}

	bool hasPlayed(string title){
		return playedGamesTitle.Contains (title);
	}

	// Use this for initialization

	int GetGameNumber(int upperbound)
	{
		return Random.Range (0, upperbound);
	}

	void MoveCamera(int GameNumber){
		//MainCamera.gameObject.GetComponent<RectTransform> ().Translate (games [GameNumber].transform.position);
		//MainCamera.gameObject.transform.position = games [GameNumber].transform.position;
		//transform.position.z = -10f;
	}

	MiniGame GetGame(int GameNumber){
		return games [GameNumber];
	}

	void StartGame(int GameNumber){
		//MoveCamera (gameNumber);
		currentMinigame = (MiniGame)Instantiate (GetGame(GameNumber), new Vector3 (0, 0, -10), Quaternion.identity);
		currentMinigame.StartMinigame(GetDifficulty(), !hasPlayed(currentMinigame.title));
	}

	Difficulty GetDifficulty(){
		if (MinigamesFinished < 3)
			return Difficulty.VeryEasy;
		else if (MinigamesFinished < 5)
			return Difficulty.Easy;
		else if (MinigamesFinished < 8)
			return Difficulty.Intermediate;
		else if (MinigamesFinished < 10)
			return Difficulty.Hard;
		else if (MinigamesFinished < 14)
			return Difficulty.VeryHard;
		else
			return Difficulty.Impossible;
	}

	bool isUnlocked(MiniGame minigame){
		return minigame.isUnlocked(currentStage);
	}

	#endregion

	#region "Region concerning saving highscore, and keeping it"
	
	public int highScore = -1;

	void updateUI(){
//		ScoringUI.realScore = realScore;
	}

	void saveScore(){
		PlayerPrefs.SetInt("HighScore", realScore);
	}

	void loadScore(){
		try{
			highScore = PlayerPrefs.GetInt ("Highscore");
		}
		catch(UnityException e){
			highScore = 0;
		}
	}
	#endregion
}
