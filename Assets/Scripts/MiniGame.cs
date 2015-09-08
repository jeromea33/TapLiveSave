using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour {

	[Header("Test Settings")]
	[Tooltip("True if you are going to test the scenario")]
	public bool isTesting = false;
	[Tooltip("Difficulty of the test")]
	public Difficulty testDiffulty = Difficulty.Easy;

	[Header("General Settings")]
	public string title;
	public StagePlace StageIn = StagePlace.Home;
	public GameObject DemoUI; //UI
	public GameObject TitleCard; //UI
	public GameObject LoseUI;
	public GameObject WinUI;
	public TimerBarUI BarUI;
	[Range(5, 20)]	
	public float cameraSize = 5f;
	public Color BackgroundColor = Color.blue;

	[Header("Timer Bar Settings")]
	public bool useBar = false;
	
	[Range(0.1f, 100f)]
	public float barSpeed; //The speed of bar in intermediate difficulty

	[Range(0f, 100f)]
	public float initialBarValuePercentage;
	public bool isBarIncreasing = false;

	[Header("General Difficulty Settings")]
	[Range(0.01f, 5f)]
	public float veryEasyMultiplier = 0.55f;
	[Range(0.01f, 5f)]
	public float easyMultiplier = 0.85f;
	[Range(0.01f, 5f)]
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

	public void Start(){
		if(isTesting){
			StartMinigame(testDiffulty, true);
		}
	}

	public void bestDifficultyAccomplishedUpdate(Difficulty update){
		bestDifficultyAccomplished = update;
	}

	public void StartMinigame(Difficulty difficulty, bool enabledemo){
		if (enabledemo)
			SetUpDemo();
		currentDifficulty = difficulty;
		if (enabledemo)
			SetUpDemo();
		SetUpTitle();
		SetUp ();
		try{
			SetupCamera();
		}
		catch{}
		if (useBar)
			SetBarUI(difficulty);
		Debug.Log ("Current Game Difficulty: " + difficulty);
		currentDifficulty = difficulty;
		isTesting = false;
	}

	protected void SetupCamera(){
		//MainCamera = GameObject.FindGameObjectWithTag ("MainCamera");
		//MainCamera.SetActive (true);
		//MainCamera.GetComponent<Camera>().orthographicSize = cameraSize;
		MainCameraFunctions.EnableCamera();
		MainCameraFunctions.ChangeBackgroundColor(BackgroundColor);
		MainCameraFunctions.ChangeCameraSize(cameraSize);
	}

	protected void AddToBar(float value){
		if (BarUIObject != null)
			BarUIObject.AddToTimer (value);
	}

	void OnDestroy(){
		if(useBar){
			if (BarUIObject != null)
				Destroy (BarUIObject.gameObject);
		}
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
		return GameObject.FindGameObjectWithTag(GameManager.Tag).GetComponent<GameManager>();
	}

	protected void StopTimer(){
		if (BarUIObject != null)
			BarUIObject.StopTimer();
	}

	protected float GetBarPercentage(){
		if (BarUIObject != null)
			return BarUIObject.GetCurrentBarPercentage();
		else
			return 0;
	}

	/// <summary>
	/// Returns true if the minigame is unlocked.
	/// </summary>
	/// <returns><c>true</c>, if currentScenePlace >= unlocked until, <c>false</c> otherwise.</returns>
	/// <param name="currentScenePlace">Current unlocked scene place.</param>
	public bool isUnlocked(StagePlace currentStagePlace){
		return StageIn <= currentStagePlace;
	}

	protected void Disable(){
		gameObject.SetActive (false);
	}

	protected void Enable(){
		gameObject.SetActive (true);
	}

	protected void DestroyTimer(){
		Destroy (BarUIObject.gameObject);
	}

	public virtual void SetUpDemo(){}
	public virtual void SetUpTitle(){}
	public virtual void SetUpLose(){}
	public virtual void SetUpWin(){}
	public virtual void SetUp(){}
}

public enum StagePlace{
	Home,
	School,
	Outdoor
}

