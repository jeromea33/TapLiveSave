using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	public Sprite DemoImage;
	public float DemoWaitTime = 2f;
	public Sprite HintImage;
	public float HintWaitTime = 2f;
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

	protected GameManager gameManager;
	protected bool StartProcess = false;

	public void Start(){
		if(isTesting){
			StartCoroutine(StartMinigame(testDiffulty, true));
		}
	}

	public void bestDifficultyAccomplishedUpdate(Difficulty update){
		bestDifficultyAccomplished = update;
	}

	public IEnumerator StartMinigame(Difficulty difficulty, bool enabledemo){
		gameManager = GameObject.FindGameObjectWithTag (GameManager.Tag).GetComponent<GameManager>();
		if (enabledemo && DemoImage != null){
			Debug.Log ("Wait Time: " + DemoWaitTime);
			SetUpDemo();
			yield return new WaitForSeconds(DemoWaitTime);
			gameManager.DemoUI.SetActive(false);
		}
		currentDifficulty = difficulty;
		SetUp ();
		try{
			SetupCamera();
		}
		catch{}
		SetUpHint();
		MainCameraFunctions.DisableInput();
		yield return new WaitForSeconds(HintWaitTime);
		MainCameraFunctions.EnableInput();
		gameManager.HintUI.SetActive (false);
		if (useBar)
			SetBarUI(difficulty);
		Debug.Log ("Current Game Difficulty: " + difficulty);
		currentDifficulty = difficulty;
		isTesting = false;
		StartProcess = true;
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

	protected IEnumerator MinigameWait(float timer){
		yield return new WaitForSeconds(timer);
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
		foreach(GameObject timer in GameObject.FindGameObjectsWithTag("TimerBar")){
			DestroyImmediate (timer);
		}
	}

	public void SetUpDemo(){
		gameManager.DemoUI.SetActive (true);
		gameManager.DemoUIPanel.GetComponent <Image>().sprite = DemoImage;
	}

	public void SetUpHint(){
		gameManager.HintUI.SetActive (true);
		gameManager.HintUIPanel.GetComponent <Image>().sprite = HintImage;
	}

	public virtual void Update(){
		//Debug.Log ("Acceleration" + Input.acceleration.x);
		//Debug.Log ("Menu Button" + Input.GetAxis("MenuAxis"));

		if(Application.platform == RuntimePlatform.Android){
			if (Input.GetKey (KeyCode.Escape)){
				gameManager.Pause();
			}
		}

		if (Input.GetKey (KeyCode.Escape)){
			gameManager.Pause();
		}

		if (!StartProcess){
			return;
		}
	}
	public virtual void SetUpLose(){}
	public virtual void SetUpWin(){}
	public virtual void SetUp(){}
}

public enum StagePlace{
	Home,
	School,
	Outdoor
}

