using UnityEngine;
using System.Collections;

public class HeadacheMiniGame : MiniGame {

	public WindowScript window;
	public BulbScript bulb;
	public DoorScript door;
	//==============================================
	private bool stopExecuting = false;


	public override void Update () {
		base.Update();
		if (!stopExecuting){
			if (IsDoorClosed() && IsWindowClosed() && IsBulbOff()) {
				Debug.Log ("Win");
				stopExecuting = true;
				SetUpWin();
			}
			else if (BarUIObject.GetCurrentBarPercentage() < 0.00f){
				Debug.Log ("Lose");
				stopExecuting = true;
				SetUpLose();
			}
		}
	}

	public override void SetUpLose(){
		SignalForEndOfGame(0f);
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
