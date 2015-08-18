using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

public class GameManager : MonoBehaviour {

	public MiniGame[] games;
	private Difficulty[] gamesMaxDifficulty;
	public GameObject MainCamera;
//	public ScoringUI ScoreUI;
	public static bool ThereIsAGame = false;
	public int realScore = -1;
	//===================================
	private int MinigamesFinished = 0;
	private MiniGame currentMinigame;
	private List<string> playedGamesTitle = new List<string>();
	private int gameNumber = -1;

	#region "Region concerning minigame triggers"
	
	public void SignalEndOfGame(){
		Debug.Log (currentMinigame.title + " has ended");
		Destroy(currentMinigame.gameObject);
		NewGame();
	}
	#endregion

	#region "Region concerning game management" 
	void NewGame(){
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
	void OnEnable () {
		NewGame();
	}

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
