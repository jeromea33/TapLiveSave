using UnityEngine;
using System.Collections;

public class StagesScript : MonoBehaviour {

	public Difficulty requiredSchoolDifficulty;
	public Difficulty requiredMountainsDifficulty;
	public string[] homeMinigameTitles;
	public string[] schoolMinigameTitles;
	public string[] outdoorMinigameTitles;
	public GameObject home;
	public GameObject school;
	public GameObject mountains;
	public Color notActivatedColor = new Color(139,139,139);
	public Color activatedColor = new Color(255,255,255);
	private GameManager gameManager;

	/// <summary>
	/// On start of the script, gets GameManager instance and colors everyone
	/// Also initializes GameStatus
	/// </summary>
	void Start(){
		gameManager = (GameManager)GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		GamesStatus.InitializeStatus(gameManager.resetMinigameStats);
		MainCameraFunctions.DisableCamera();
		goColor ();
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
	/// Colors the 
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

	public void changeStagePlace(StagePlace current_stage){
		gameManager.currentStage = current_stage;
	}
}
