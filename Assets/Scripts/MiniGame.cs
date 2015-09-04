using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour {

	public string title;
	public StagePlace StageIn = StagePlace.Home;
	public GameObject DemoUI; //UI
	public GameObject TitleCard; //UI
	public GameObject LoseUI;
	public GameObject WinUI;
	public TimerBarUI BarUI;
	[Range(5, 20)]
	public float cameraSize = 5f;

	public bool useBar = false;
	
	[Range(0.1f, 100f)]
	public float barSpeed; //The speed of bar in intermediate difficulty

	[Range(0f, 100f)]
	public float initialBarValuePercentage;
	public bool isBarIncreasing = false;

	[Range(0.01f, 1f)]
	public float veryEasyMultiplier = 0.55f;
	[Range(0.01f, 1f)]
	public float easyMultiplier = 0.85f;
	[Range(0.01f, 2f)]
	public float intermeddiateMultiplier = 1f;
	[Range(0.01f, 5f)]
	public float hardMultiplier = 2.25f;
	[Range(0.01f, 5f)]
	public float veryHardMultiplier = 3.25f;
	[Range(0.01f, 5f)]
	public float impossibleMultiplier = 4.25f;

	protected GameObject MainCamera;
	protected Difficulty currentDifficulty;
	protected TimerBarUI BarUIObject;

	protected Difficulty bestDifficultyAccomplished = Difficulty.None;

	public void bestDifficultyAccomplishedUpdate(Difficulty update){
		bestDifficultyAccomplished = update;
	}

	public void StartMinigame(Difficulty difficulty, bool enabledemo){
		if (enabledemo){
			SetUpDemo();
		}
		currentDifficulty = difficulty;
		if (enabledemo){
			SetUpDemo();
		}
		SetUpTitle();
		SetUp ();
		SetupCamera();
		SetBarUI(difficulty);
		Debug.Log ("Current Game Difficulty: " + difficulty);
		currentDifficulty = difficulty;
	}

	public virtual void SetUp(){}

	protected void SetupCamera(){
		//MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		//MainCamera.SetActive (true);
		//MainCamera.GetComponent<Camera>().orthographicSize = cameraSize;
		MainCameraFunctions.EnableCamera();
		MainCameraFunctions.ChangeCameraSize(cameraSize);
	}

	protected void AddToBar(float value){
		BarUIObject.AddToTimer (value);
	}

	void OnDestroy(){
		Destroy (BarUIObject.gameObject);
	}

	protected void SetBarUI(Difficulty difficulty){
		float RealBarSpeed = barSpeed;
		switch (difficulty){
		case Difficulty.VeryEasy:
			RealBarSpeed = barSpeed * veryEasyMultiplier;
			break;
		case Difficulty.Easy:
			RealBarSpeed = barSpeed * easyMultiplier;
			break;
		case Difficulty.Intermediate:
			RealBarSpeed = barSpeed * intermeddiateMultiplier;
			break;
		case Difficulty.Hard:
			RealBarSpeed = barSpeed * hardMultiplier;
			break;
		case Difficulty.VeryHard:
			RealBarSpeed = barSpeed * veryHardMultiplier;
			break;
		case Difficulty.Impossible:
			RealBarSpeed = barSpeed * impossibleMultiplier;
			break;
		}
		BarUIObject = (TimerBarUI)Instantiate (BarUI,Vector3.zero, Quaternion.identity);
		BarUIObject.BarSpeed = RealBarSpeed;
		BarUIObject.initialBarValuePercentage = initialBarValuePercentage;
		switch (isBarIncreasing){
		case true:
			BarUIObject.initialAction = TimerBarAction.Increase;
			break;
		case false:
			BarUIObject.initialAction = TimerBarAction.Decrease;
			break;
		}
		BarUIObject.gameObject.SetActive (true);
		Debug.Log ("Timer setup complete");
	}

	public void SignalForEndOfGame(float win){
		if(win == 1.0f)
			GameController().SignalEndOfGame(currentDifficulty, 100, true);
		else
			GameController().SignalEndOfGame(currentDifficulty, 0, false);
	}

	GameManager GameController(){
		return GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	protected void StopTimer(){
		BarUIObject.StopTimer();
	}

	protected float GetBarPercentage(){
		return BarUIObject.GetCurrentBarPercentage();
	}

	/// <summary>
	/// Returns true if the minigame is unlocked.
	/// </summary>
	/// <returns><c>true</c>, if currentScenePlace >= unlocked until, <c>false</c> otherwise.</returns>
	/// <param name="currentScenePlace">Current unlocked scene place.</param>
	public bool isUnlocked(StagePlace currentStagePlace){
		return StageIn <= currentStagePlace;
	}

	public virtual void SetUpDemo(){}
	public virtual void SetUpTitle(){}
	public virtual void SetUpLose(){}
	public virtual void SetUpWin(){}
}

public enum StagePlace{
	Home,
	School,
	Outdoor
}

