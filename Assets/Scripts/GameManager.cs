using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

public class GameManager : MonoBehaviour {

	public GameObject DemoUI;
	public GameObject DemoUIPanel;

	/// <summary>
	/// The tag uses this controller. Used for housekeeping and safety (Less bugs)
	/// </summary>
	public static string Tag = "GameController";

	/// <summary>
	/// The list of minigames that the game uses
	/// </summary>
	public MiniGame[] games;

	/// <summary>
	/// The reference to the main camera
	/// </summary>
	public GameObject MainCamera;

	/// <summary>
	/// The reference to the UI used for scoring
	/// Is called everytime a game ended and auto updates when the 
	/// scoreUI score is changed
	/// </summary>
	public ScoringUI ScoreUI;

	/// <summary>
	/// Reference to pause menu
	/// </summary>
	public GameObject pauseMenu;

	/// <summary>
	/// The reference to the main menu
	/// So that we can just activate it when the user quits
	/// </summary>
	public MainMenu mainMenu;

	public StagesScript stageManager;

	public bool allGamesMode = false;

	/// <summary>
	/// The variable used to determine if there is a game or not
	/// Was used before to autodetect when the game is ended
	/// Is now deprecated since new games were called when scoreUI counting ended
	/// </summary>
	public static bool ThereIsAGame = false;

	/// <summary>
	/// Deprecated: Don't know if it is actually used
	/// </summary>
	public int realScore = -1;

	/// <summary>
	/// The current stage of the game
	/// Must be updated everytime a game ends
	/// </summary>
	public StagePlace currentStage;

	/// <summary>
	/// A flag that when activated. Resets the game status in GameStatus class
	/// </summary>
	public bool resetMinigameStats = false;
	//===================================
	/// <summary>
	/// The number of minigames finished. Is used to determine the difficulty of
	/// the games
	/// </summary>
	private int MinigamesFinished = 0;

	/// <summary>
	/// A reference to the current minigame being played
	/// </summary>
	private MiniGame currentMinigame;

	/// <summary>
	/// The titles of the played games. Will be used to determine
	/// if games are played or not. So that it would not repeat
	/// </summary>
	private List<string> playedGamesTitle = new List<string>();

	/// <summary>
	/// The index of the game in the minigames array that is currently being played
	/// </summary>
	private int gameNumber = -1;

	/// <summary>
	/// The last game number.
	/// </summary>
	private int lastGameNumber = -1;

	[Range(1, 20)]
	public int toEasyDifficultyGamesRequirment = 5;
	[Range(1, 20)]
	public int toIntermediateDifficultyGamesRequirment = 5;
	[Range(1, 20)]
	public int toHardDifficultyGamesRequirment = 5;
	[Range(1, 20)]
	public int toVeryHardDifficultyGamesRequirment = 5;
	[Range(1, 20)]
	public int toImpossibleDifficultyGamesRequirment = 5;


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
		if(win){
			GamesStatus.UpdateMinigameStats (currentMinigame.title, difficulty, true);
			ScoreUI.SetUpComplete();
		}
		else
			ScoreUI.SetUpFail();
		if (ScoreUI.numberOfHealth == 0){
			GameOver();
			return;
		}
		MinigamesFinished++;
		UpdateHighScore();
		ScoreUI.gameObject.SetActive (true);
	}

	/// <summary>
	/// Upon enable, Is going to activate the main menu so that it appears first
	/// </summary>
	void OnEnable () {
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

	/// <summary>
	/// Disables the camera.
	/// </summary>
	public void DisableCamera(){
		MainCamera.SetActive(false);
	}

	/// <summary>
	/// Enables the camera.
	/// </summary>
	public void EnableCamera(){
		MainCamera.SetActive(true);
	}

	/// <summary>
	/// Sets the current stage in.
	/// </summary>
	/// <param name="stage">The current stage</param>
	public void SetCurrentStageIn(StagePlace stage){
		MinigamesFinished = 0;
		currentStage = stage;
	}

	/// <summary>
	/// Runs this code upon life == 0
	/// </summary>
	void GameOver(){
		ScoreUI.gameObject.SetActive (true);
	}

	public void SignalForMainMenu(){
		mainMenu.gameObject.SetActive (true);
	}


	#endregion
	
	#region "Region concerning game management" 

	/// <summary>
	/// Creates and plays a new game. The game is randomly generated
	/// mode_all = Will play all unlockes stages
	/// off mode_all = Will play stages only belong to current stage. Won't update it either
	/// </summary>

	private int lastGamePlayed;
	public void NewGame(){
		//Calculate current stage
		bool continueFlag = true;
		StagePlace x;
		while (continueFlag){
			gameNumber = lastGamePlayed;
			while(gameNumber == lastGamePlayed){
				gameNumber = GetGameNumber (games.GetLength(0));
			}
			x = games[gameNumber].StageIn;
			if(allGamesMode){
				if (x <= currentStage){
					continueFlag = false;
				}
			}
			else{
				if (x == currentStage){
					continueFlag = false;
				}
			}
		}
		//Show Title
		ThereIsAGame = true;
		TitleProcedure(gameNumber);
		//Show Demo
		StartGame (gameNumber);
		lastGamePlayed = gameNumber;
	}

	/// <summary>
	/// DEPRECATED: Shows the titleUI of the game number
	/// </summary>
	/// <returns>None</returns>
	/// <param name="gameNumber">The game number</param>
	IEnumerator TitleProcedure(int gameNumber){
		if (GetDifficulty() != Difficulty.Impossible){
			GameObject titleUI = Instantiate(GetGame(gameNumber).TitleCard);
			yield return new WaitForSeconds(1f);
			DestroyObject (titleUI);
		}
		//GetGame (gameNumber).TitleCard.SetActive (false);
	}
	
	bool hasPlayed(string title){
		return playedGamesTitle.Contains (title);
	}

	/// <summary>
	/// Gets a random game number
	/// </summary>
	/// <returns>The game number.</returns>
	/// <param name="upperbound">The maximum number of the games</param>
	int GetGameNumber(int upperbound)
	{
		return Random.Range (0, upperbound);
	}

	/// <summary>
	/// Gets the game object of the game number
	/// </summary>
	/// <returns>The minigame object</returns>
	/// <param name="GameNumber">Game number.</param>
	MiniGame GetGame(int GameNumber){
		return games [GameNumber];
	}

	/// <summary>
	/// Starts the minigame
	/// </summary>
	/// <param name="GameNumber">Game number of the game to be started</param>
	void StartGame(int GameNumber){
		//MoveCamera (gameNumber);
		currentMinigame = (MiniGame)Instantiate (GetGame(GameNumber), new Vector3 (0, 0, -10), Quaternion.identity);
		//currentMinigame = GetGame(GameNumber);
		//currentMinigame.gameObject.SetActive(true);
		StartCoroutine(currentMinigame.StartMinigame(GetDifficulty(), /*!hasPlayed(currentMinigame.title)*/ true));
	}

	/// <summary>
	/// Gets the difficulty. More like sets the difficulty
	/// </summary>
	/// <returns>The difficulty.</returns>
	Difficulty GetDifficulty(){
		if (MinigamesFinished <= toEasyDifficultyGamesRequirment)
			return Difficulty.VeryEasy;
		else if (MinigamesFinished <= toEasyDifficultyGamesRequirment
		         + toIntermediateDifficultyGamesRequirment)
			return Difficulty.Easy;
		else if (MinigamesFinished <= toEasyDifficultyGamesRequirment
		         + toIntermediateDifficultyGamesRequirment
		         + toHardDifficultyGamesRequirment)
			return Difficulty.Intermediate;
		else if (MinigamesFinished <= toEasyDifficultyGamesRequirment
		         + toIntermediateDifficultyGamesRequirment
		         + toHardDifficultyGamesRequirment
		         + toVeryHardDifficultyGamesRequirment)
			return Difficulty.Hard;
		else if (MinigamesFinished <= toEasyDifficultyGamesRequirment
		         + toIntermediateDifficultyGamesRequirment
		         + toHardDifficultyGamesRequirment
		         + toVeryHardDifficultyGamesRequirment
		         + toImpossibleDifficultyGamesRequirment)
			return Difficulty.VeryHard;
		else
			return Difficulty.Impossible;
	}

	/// <summary>
	/// Checks if the game is unlocked
	/// </summary>
	/// <returns><c>true</c>, if unlocked was ised, <c>false</c> otherwise.</returns>
	/// <param name="minigame">Minigame.</param>
	bool isUnlocked(MiniGame minigame){
		return minigame.isUnlocked(currentStage);
	}

	/// <summary>
	/// Gets a list of minigames in the 
	/// </summary>
	/// <returns>The list of minigames of the stage.</returns>
	/// <param name="stage">Stage</param>
	List<MiniGame> GetMinigamesOf(StagePlace stage){
		List<MiniGame> result = new List<MiniGame>();
		foreach(MiniGame game in games){
			if(game.StageIn == stage){
				result.Add (game);
			}
		}
		return result;
	}

	List<int> GetNumberOfMinigames(StagePlace stage){
		List<int> result = new List<int>();
		int counter = 0;
		foreach(MiniGame game in games){
			if(game.StageIn == stage){
				result.Add (counter);
			}
			counter++;
		}
		return result;
	}

	StagePlace GetCurrentStage(){
		//TODO assign current stage depending on completed scenarios per lever
		//Step 1. Get games per stage
		//Step 2. Test each stage. If one fails. the stage is currently on that stage
		//Step 3. return
		List<MiniGame> homeStages = GetMinigamesOf(StagePlace.Home);
		List<MiniGame> schoolStages = GetMinigamesOf(StagePlace.School);
		List<MiniGame> outdoorStages = GetMinigamesOf(StagePlace.Outdoor);
		foreach(MiniGame minigame_one in homeStages){
			if(GamesStatus.GetDifficultyOf (minigame_one.title) <= stageManager.requiredSchoolDifficulty)
				return StagePlace.Home;
		}
		foreach(MiniGame minigame_two in schoolStages){
			if(GamesStatus.GetDifficultyOf (minigame_two.title) <= stageManager.requiredMountainsDifficulty)
				return StagePlace.School;
		}
		return StagePlace.Outdoor;
	}

	#endregion

	#region "Region concerning pausing the game"

	public bool Paused(){
		return Time.timeScale == 1.0f;
	}

	public void Pause(){
		pauseMenu.SetActive (true);
		Time.timeScale = 0f;
	}

	public void UnPause(){
		pauseMenu.SetActive (false);
		Time.timeScale = 1f;
	}

	#endregion

	#region "Region concerning saving highscore, and keeping it"
	
	public static int highScore = -1;

	void UpdateScoreUI(){
		ScoreUI.UpdateHighScore(CurrentScore());
	}

	void UpdateHighScore(){
		if (CurrentScore() >  highScore){
			highScore = CurrentScore();
			UpdateScoreUI();
			SaveScore();
		}
	}

	public void SaveScore(){
		PlayerPrefs.SetInt("HighScore", CurrentScore());
	}

	public int LoadScore(){
		try{
			highScore = PlayerPrefs.GetInt ("HighScore");
			ScoreUI.UpdateHighScore (GameManager.highScore);
			return highScore;
		}
		catch(UnityException e){
			return highScore = 0;
		}
	}

	int CurrentScore(){
		return ScoreUI.score;
	}

	public static int ForceLoadScore(){
		try{
			highScore = PlayerPrefs.GetInt ("HighScore");
			return highScore;
		}
		catch(UnityException e)
		{
			return 0;
		}
	}
	#endregion

	public void reset(){
		this.gameObject.SetActive (false);
		this.gameObject.SetActive (true);
	}
}
