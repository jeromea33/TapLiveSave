using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SprainMinigame : MiniGame {

	[Header("Sprain Settings")]
	public GameObject[] bandages;
	public GameObject[] bandagePoints;

	int index = 0;
	int bandagesInCorrectPosition = 0;

	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(!stopped){
			index = 0;
			bandagesInCorrectPosition = 0;
			foreach(GameObject bandage in bandages){
				if(bandage.GetComponent<Collider2D>().bounds.Contains(bandagePoints[index].transform.position)){
					//Debug.Log ("Bandage " + index + " is in correct position");
					bandagesInCorrectPosition++;
				}
				index++;
			}
			if(bandagesInCorrectPosition >= bandagePoints.Length){
				SetUpWin();
			}
			else if (GetBarPercentage() > 1f && StartProcess){
				SetUpLose();
			}
		}
	}

	public override void SetUpWin(){
		stopped = true;
		SignalForEndOfGame (1f);
	}

	public override void SetUpLose(){
		stopped = true;
		SignalForEndOfGame (0f);
	}
}
