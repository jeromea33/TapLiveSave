using UnityEngine;
using System.Collections;

public class FaintingScenario : MiniGame {

	public GameObject spoon;
	public GameObject spoonStirPoint;
	public GameObject fruit;
	public GameObject glass;
	public GameObject glassPoint;
	public Sprite emptySprite;
	public Sprite fullSprite;
	[Range(20, 200)]
	public int stirRequirment;

	private bool glassFull = false;
	private int stirInt = 0;
	public override void Update(){
		base.Update();
		if(!stopped && StartProcess){
			checkIfTurnGlassToFull();
			if (glassFull){
				if(glass.GetComponent<BoxCollider2D>().OverlapPoint(spoonStirPoint.transform.position)){
					stirInt++;
				}
				if(stirInt >= stirRequirment){
					SetUpWin();
				}
			}
			if(GetBarPercentage() < 0f){
				SetUpLose();
			}
		}
	}

	public void checkIfTurnGlassToFull(){
		if (fruit.GetComponent<CircleCollider2D>().OverlapPoint(glassPoint.transform.position)){
			fruit.SetActive(false);
			glass.GetComponent<SpriteRenderer>().sprite = fullSprite;
			glassFull = true;
		}
	}

	public override void SetUpWin(){
		stopped = true;
		DestroyTimer();
		WinUI.SetActive (true);
	}

	public override void SetUpLose(){
		stopped = true;
		glass.SetActive(false);
		DestroyTimer();
		LoseUI.SetActive(true);
	}
}
