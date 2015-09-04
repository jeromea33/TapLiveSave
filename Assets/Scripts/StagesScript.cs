using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// The script regarding handling of stages and its updates and stuff.
/// </summary>
public class StagesScript : MonoBehaviour {
	/// <summary>
	/// The tag of the gameobject who holds the script
	/// </summary>
	public static string Tag = "StageController";

	/// <summary>
	/// The required difficulty in order for the school scenarios to be unlocked
	/// </summary>
	public Difficulty requiredSchoolDifficulty;

	/// <summary>
	/// The required difficulty in order for the outdoor scenarios to be unlocked
	/// </summary>
	public Difficulty requiredMountainsDifficulty;

	/// <summary>
	/// The list of titles for home scenarios. This variable is used for unlocking
	/// and housekeeping
	/// </summary>
	private List<string> homeMinigameTitles = new List<string>();

	/// <summary>
	/// The list of titles for school scenarios. This variable is used for unlocking
	/// and housekeeping
	/// </summary>
	private List<string> schoolMinigameTitles = new List<string>();

	/// <summary>
	/// The list of titles for outdoor scenarios. This variable is used for unlocking
	/// and housekeeping
	/// </summary>
	private List<string> outdoorMinigameTitles = new List<string>();

	/// <summary>
	/// Game object that points to the home sprite
	/// </summary>
	public GameObject home;

	/// <summary>
	/// Game object that points to the school sprite
	/// </summary>
	public GameObject school;

	/// <summary>
	/// Game object that points to the mountains sprite
	/// </summary>
	public GameObject mountains;

	/// <summary>
	/// The color of the non activated scenarios
	/// </summary>
	public Color notActivatedColor = new Color(139,139,139);

	/// <summary>
	/// The color of the activated scenarios
	/// </summary>
	public Color activatedColor = new Color(255,255,255);

	/// <summary>
	/// Game object that points to the game manager controller
	/// </summary>
	private GameManager gameManager;

	/// <summary>
	/// On start of the script, gets GameManager instance and colors everyone
	/// Also initializes GameStatus and set the list of titles.
	/// </summary>
	void Start(){
		gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		GamesStatus.InitializeStatus(gameManager.resetMinigameStats);
		MainCameraFunctions.DisableCamera();
		GetStageOfTitles();
		goColor ();
	}

	/// <summary>
	/// Gets the list of titles that corresponds to their stages and maps them.
	/// </summary>
	void GetStageOfTitles(){
		foreach(MiniGame minigame in gameManager.games){
			if(minigame.StageIn == StagePlace.Home)
				homeMinigameTitles.Add (minigame.title);
			else if(minigame.StageIn == StagePlace.School)
				schoolMinigameTitles.Add (minigame.title);
			else if(minigame.StageIn == StagePlace.Outdoor)
				outdoorMinigameTitles.Add (minigame.title);
		}
	}

	/// <summary>
	/// Start the minigames
	/// </summary>
	public void StartMinigames(){
		gameObject.SetActive (false);
		GameObject.FindGameObjectWithTag ("GameController").GetComponent <GameManager>().NewGame();
	}

	/// <summary>
	/// Updates the animation after wait
	/// </summary>
	void Update () {
		wait ();
		if(IsMountainsUnlocked ()){
			GetComponent<Animator>().SetTrigger ("ToMountain");
		}
		else if (IsSchoolUnlocked()){
			GetComponent<Animator>().SetTrigger ("ToSchool");
		}
		else{
			GetComponent<Animator>().SetTrigger ("ToHouse");
		}
	}

	/// <summary>
	/// Delays animation for 
	/// </summary>
	IEnumerator wait(){
		yield return new WaitForSeconds(3f);
	}


	/// <summary>
	/// Colors the sprites depending if they were unlocked or not
	/// Home is always unlocked
	/// </summary>
	public void goColor(){
		if(IsSchoolUnlocked()){
			school.GetComponent<SpriteRenderer>().color = activatedColor;
		}
		else{
			school.GetComponent<SpriteRenderer>().color = notActivatedColor;
			mountains.GetComponent<SpriteRenderer>().color = notActivatedColor;
			return;
		}
		if (IsMountainsUnlocked()){
			mountains.GetComponent<SpriteRenderer>().color = activatedColor;
		}
		else {
			mountains.GetComponent<SpriteRenderer>().color = notActivatedColor;
		}

	}

	/// <summary>
	/// Determines whether or not school stage is unlocked
	/// </summary>
	/// <returns><c>true</c> if school stage is unlocked; otherwise, <c>false</c>.</returns>
	public bool IsSchoolUnlocked(){
		bool result = false;
		foreach(string title in homeMinigameTitles){
			if (GamesStatus.GetDifficultyOf (title) >= requiredSchoolDifficulty){
				result = true;
			}
			else{ 
				return false;
			}
		}
		return result;
	}

	/// <summary>
	/// Determines whether or not outdoor stage is unlocked
	/// </summary>
	/// <returns><c>true</c> if outdoor stage is unlocked; otherwise, <c>false</c>.</returns>
	public bool IsMountainsUnlocked(){
		bool result = false;
		foreach(string title in schoolMinigameTitles){
			if (GamesStatus.GetDifficultyOf (title) >= requiredMountainsDifficulty){
				result = true;
			}
			else{
				return false;
			}
		}
		return result;
	}

	/// <summary>
	/// Changes the stage place.
	/// </summary>
	/// <param name="current_stage">Current_stage.</param>
	public void changeStagePlace(StagePlace current_stage){
		gameManager.currentStage = current_stage;
	}
}
