using UnityEngine;
using System.Collections;

public class AnimalBiteMinigame : MiniGame {

	public GameObject[] animalBits;
	public GameObject feet;

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
		DestroyImmediate(feet);
		stopped = true;
		DestroyTimer();

		WinUI.SetActive (true);
	}

	public override void SetUpLose(){
		DestroyImmediate(feet);
		stopped = true;
		DestroyTimer();
		LoseUI.SetActive(true);
	}
}
