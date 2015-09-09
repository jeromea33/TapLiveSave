using UnityEngine;
using System.Collections;

public class HeatstrokeMinigame : MiniGame {

	public GameObject boundsBox;
	public GameObject[] people;

	[Range(1f,5f)]
	public float normalPedSpeed = 3f;
	[Range(0.1f,1f)]
	public float veryEasyPedMultiplier = 1f;
	[Range(0.1f,3f)]
	public float easyPedMultiplier = 2f;
	[Range(0.1f,5f)]
	public float intermediatePedMultiplier = 3f;
	[Range(0.1f,10f)]
	public float hardPedMultiplier = 5f;
	[Range(0.1f,20f)]
	public float veryHardPedMultiplier = 7f;
	[Range(0.1f,30f)]
	public float impossiblePedMultiplier = 10f;


	// Update is called once per frame
	public override void Update () {
		base.Update();
		//Debug.Log (BoundsBoxContainsPeople());
		if(BarUIObject.GetCurrentBarPercentage() < 0){
			if(BoundsBoxContainsPeople() < 1)
				SetUpWin();
			else
				SetUpLose();
		}
	}

	public override void SetUp(){
		float pedspeed = normalPedSpeed;
		bool trollActivate = false;
		switch(currentDifficulty){
		case Difficulty.VeryEasy:
			pedspeed = normalPedSpeed * veryEasyPedMultiplier;
			break;
		case Difficulty.Easy:
			pedspeed = normalPedSpeed * easyPedMultiplier;
			break;
		case Difficulty.Intermediate:
			pedspeed = normalPedSpeed * intermediatePedMultiplier;
			break;
		case Difficulty.Hard:
			pedspeed = normalPedSpeed * hardPedMultiplier;
			break;
		case Difficulty.VeryHard:
			pedspeed = normalPedSpeed * veryHardPedMultiplier;
			trollActivate = true;
			break;
		case Difficulty.Impossible:
			pedspeed = normalPedSpeed * impossiblePedMultiplier;
			trollActivate = true;
			break;
		default:
			break;
		}
		Debug.Log (currentDifficulty);
		foreach (GameObject pedestrian in people){
			pedestrian.GetComponent<PeopleScript>().speed = pedspeed;
			pedestrian.GetComponent<PeopleScript>().trollololol = trollActivate;
		}
	}

	int BoundsBoxContainsPeople(){
		int x = 0;
		foreach (GameObject guy in people){
			if(boundsBox.gameObject.GetComponent<BoxCollider2D>().bounds.Contains (guy.transform.position)){
				x = x + 1;
			}
		}
		return x;
	}

	public override void SetUpLose(){
		SignalForEndOfGame (0);
	}

	public override void SetUpWin(){
		SignalForEndOfGame (1);
	}
}
