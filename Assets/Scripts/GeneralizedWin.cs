using UnityEngine;
using System.Collections;

public class GeneralizedWin : MonoBehaviour {

	public bool win = false;

	public void Execute(){
		if(win)
			executeWin();
		else
			executeLose();
	}

	void executeWin(){
		gameObject.GetComponentInParent<MiniGame>().SignalForEndOfGame(1f);
	}

	void executeLose(){
		gameObject.GetComponentInParent<MiniGame>().SignalForEndOfGame(0f);
	}
}
