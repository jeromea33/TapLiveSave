using UnityEngine;
using System.Collections;

public class ChokingMinigame : MiniGame {

	[Header("Choking Settings")]
	[Range(0f, 200f)]
	public float TapAddValue = 75f;
	[Range(5f, 0.1f)]
	public float veryEasyTapMultiplier = 2.25f;
	[Range(5f, 0.1f)]
	public float easyTapMultiplier = 1.50f;
	[Range(5f, 0.1f)]
	public float intermediateTapMultiplier = 1f;
	[Range(5f, 0.1f)]
	public float hardTapMultiplier = 0.80f;
	[Range(5f, 0.1f)]
	public float veryHardTapMultiplier = 0.75f;
	[Range(5f, 0.1f)]
	public float impossibleTapMultiplier = 0.65f;

	// Update is called once per frame

	private bool endflag = false;
	public override void Update () {
		base.Update();
		if (GetBarPercentage() > 1f){
			SetUpWin ();
			endflag = true;
		}
		else if (GetBarPercentage() < 0.00) {
			SetUpLose ();
			endflag = true;
		}
	}

	public void OnTap(){
		Debug.Log ("Choking Tap that");
		GetComponent<Animator>().SetTrigger ("OnTapTrigger");
		float addvalue = GetTimerAdd();
		AddToBar(addvalue);
	}


	public override void SetUpWin(){
		GetComponent<Animator>().SetTrigger ("WinTrigger");
		MainCameraFunctions.DisableInput();
		BarUIObject.MakeBarFull();
		BarUIObject.gameObject.SetActive (false);
	}

	private float GetTimerAdd(){
		switch (currentDifficulty){
		case Difficulty.VeryEasy:
			return TapAddValue * veryEasyTapMultiplier;
		case Difficulty.Easy:
			return TapAddValue * easyTapMultiplier;
		case Difficulty.Intermediate:
			return TapAddValue * intermediateTapMultiplier;
		case Difficulty.Hard:
			return TapAddValue * hardTapMultiplier;
		case Difficulty.VeryHard:
			return TapAddValue * veryHardTapMultiplier;
		case Difficulty.Impossible:
			return TapAddValue * impossibleTapMultiplier;
		default:
			return TapAddValue;
		}
	}

	public override void SetUpLose(){
		GetComponent<Animator>().SetTrigger("LoseTrigger");
		BarUIObject.gameObject.SetActive(false);
		MainCameraFunctions.DisableInput();
	}
}
