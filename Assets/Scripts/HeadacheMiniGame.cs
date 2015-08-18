using UnityEngine;
using System.Collections;

public class HeadacheMiniGame : MiniGame {

	public WindowScript window;
	public BulbScript bulb;
	public DoorScript door;
	//==============================================

	void Update () {
		if (IsDoorClosed() && IsWindowClosed() && IsBulbOff()) {
			Debug.Log ("Win");
			SetUpWin();
		}
		else if (BarUIObject.GetCurrentBarPercentage() < 0.00f){
			Debug.Log ("Lose");
			SetUpLose();
		}
	}

	public override void SetUpDemo(){

	}

	public override void SetUpTitle(){

	}

	public override void SetUpLose(){
		SignalForEndOfGame();
	}

	private bool winFlag = false;
	public override void SetUpWin(){
		GetComponent<Animator>().SetTrigger ("Win");
		StopTimer();
	}

	bool IsDoorClosed(){
		return door.state.ToLower().Equals ("closed");
	}

	bool IsWindowClosed(){
		return window.state.ToLower ().Equals ("closed");
	}

	bool IsBulbOff(){
		return bulb.state.ToLower ().Equals ("off");
	}
}
