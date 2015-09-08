using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class SpinalCordMinigame : MiniGame {

	public GameObject Girl;

	void OnDisable(){
		Girl.GetComponent<TapGesture>().Tapped -= OnTap;
	}
	
	// Update is called once per frame
	void Update () {
		if(GetBarPercentage() < 0f){
			SetUpWin();
		}
	}



	public override void SetUpTitle(){

	}

	public override void SetUpLose(){
		BarUI.StopTimer();
		Debug.Log ("Spinal Lose");
		SignalForEndOfGame (0f);
	}

	public override void SetUpWin(){
		BarUI.StopTimer();
		Debug.Log ("Spinal Win");
		SignalForEndOfGame (1.0f);
	}

	public override void SetUp(){
		Girl.GetComponent<TapGesture>().Tapped += OnTap;
	}

	void OnTap(object sender, System.EventArgs e){
		SetUpLose();
	}
}
