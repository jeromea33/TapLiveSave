using UnityEngine;
using System.Collections;

public class MiniGame : MonoBehaviour {

	public GameObject DemoUI; //UI
	public GameObject TitleCard; //UI
	public GameObject LoseUI;
	public GameObject WinUI;
	public TimerBarUI BarUI;
	public bool useBar = false;
	public float barSpeed; //The speed of bar in intermediate difficulty
	public float initialBarValuePercentage;
	public bool isBarIncreasing = false;
	public string title;
	
	protected Camera MainCamera;
	protected Difficulty currentDifficulty;
	protected TimerBarUI BarUIObject;

	public void StartMinigame(Difficulty difficulty, bool enabledemo){
		if (enabledemo){
			SetUpDemo();
		}
		currentDifficulty = difficulty;
		if (enabledemo){
			SetUpDemo();
		}
		SetUpTitle();
		SetBarUI(difficulty);
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
			RealBarSpeed = barSpeed * 0.55f;
			break;
		case Difficulty.Easy:
			RealBarSpeed = barSpeed * 0.85f;
			break;
		case Difficulty.Intermediate:
			RealBarSpeed = barSpeed;
			break;
		case Difficulty.Hard:
			RealBarSpeed = barSpeed * 2.25f;
			break;
		case Difficulty.VeryHard:
			RealBarSpeed = barSpeed * 3.25f;
			break;
		case Difficulty.Impossible:
			RealBarSpeed = barSpeed * 4.25f;
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

	public void SignalForEndOfGame(){
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SignalEndOfGame();
	}

	protected void StopTimer(){
		BarUIObject.StopTimer();
	}

	protected float GetBarPercentage(){
		return BarUIObject.GetCurrentBarPercentage();
	}

	public virtual void SetUpDemo(){}
	public virtual void SetUpTitle(){}
	public virtual void SetUpLose(){}
	public virtual void SetUpWin(){}
}
