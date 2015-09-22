using UnityEngine;
using System.Collections;

public class DehydrationMinigame : MiniGame {

	public GameObject[] glasses;
	public Collider2D mouthCollider;

	// Update is called once per frame

	public override void Update(){
		base.Update ();
		//if(!stopped && StartProcess){
			if(GetBarPercentage() < 0f){
				SetUpLose();
			}
			else if (GetActiveGlasses() < 1){
				SetUpWin();
			}
			//foreach(GameObject glass in glasses){
				//if(mouthCollider.is)
			//}
		//}
	}

	public override void SetUp(){
		foreach(GameObject glass in glasses){
			glass.SetActive (true);
		}
	}

	private int GetActiveGlasses(){
		int numberOfActiveGlasses = 0;
		foreach(GameObject glass in glasses){
			if(glass.activeSelf)
				numberOfActiveGlasses++;
		}
		return numberOfActiveGlasses;
	}

	public override void SetUpWin(){
		stopped = true;
		DestroyTimer();
		WinUI.SetActive (true);
	}

	public override void SetUpLose(){
		stopped = true;
		DestroyTimer();
		LoseUI.SetActive(true);
	}
}
