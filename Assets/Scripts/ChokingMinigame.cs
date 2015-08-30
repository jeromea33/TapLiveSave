using UnityEngine;
using System.Collections;

public class ChokingMinigame : MiniGame {
	
	public float TapAddValue = 75f;

	// Update is called once per frame

	private bool endflag = false;
	void Update (){
	if (!endflag)
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

	private float GetTimerAdd(){
		switch (currentDifficulty){
		case Difficulty.VeryEasy:
			return TapAddValue * 2.25f;
		case Difficulty.Easy:
			return TapAddValue * 1.50f;
		case Difficulty.Intermediate:
			return TapAddValue;
		case Difficulty.Hard:
			return TapAddValue * 0.80f;
		case Difficulty.VeryHard:
			return TapAddValue * 0.75f;
		case Difficulty.Impossible:
			return TapAddValue * 0.65f;
		default:
			return TapAddValue;
		}
	}

	public override void SetUpWin(){
		GetComponent<Animator>().SetTrigger ("WinTrigger");
		BarUIObject.MakeBarFull();
		BarUIObject.gameObject.SetActive (false);
	}

	public override void SetUpDemo(){
		
	}
	
	public override void SetUpTitle(){
		
	}
	
	public override void SetUpLose(){
		GetComponent<Animator>().SetTrigger("LoseTrigger");
		BarUIObject.gameObject.SetActive(false);
	}
}
