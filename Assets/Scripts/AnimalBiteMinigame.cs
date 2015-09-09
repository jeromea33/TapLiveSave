using UnityEngine;
using System.Collections;

public class AnimalBiteMinigame : MiniGame {

	public GameObject[] animalBits;

	private bool win = false;
	public override void Update(){
		base.Update();
		if (!stopped && StartProcess){
			win = false;
			foreach(GameObject animalBit in animalBits){
				if(!animalBit.activeSelf){
					win = true;
				}
				else{
					win = false;
					break;
				}
			}

			if(win)
				SetUpWin();
			else if (GetBarPercentage() < 0f){
				SetUpLose();
			}
		}


	}

	public override void SetUpWin(){
		stopped = true;
		DestroyTimer();

		WinUI.SetActive (true);
	}

	public override void SetUpLose(){
		stopped = true;
		DestroyTimer();
		SignalForEndOfGame(0f);
	}
}
