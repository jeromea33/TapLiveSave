using UnityEngine;
using System.Collections;

public class EyeInjuryMinigame : MiniGame {

	[Range(1f, 30f)]
	public float moveSpeed;
	public GameObject glass;
	public GameObject drop;
	public Collider2D dropCollider;
	public GameObject eye;
	public Collider2D eyeCollider;
	protected bool checkWinLose = false;

	public override void Update(){
		base.Update();
		if(!stopped && StartProcess){
			glass.gameObject.transform.position += new Vector3(Input.acceleration.x, 0f) * Time.deltaTime * moveSpeed;
			if(GetBarPercentage() < 0f && !checkWinLose){
				checkWinLose = true;
				stopped = true;
				drop.GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
		if (checkWinLose){
			check();
		}
	}

	public void check(){
		if(drop.gameObject.transform.position.y < -14f){
			SetUpLose();
		}
		else if (dropCollider.IsTouching(eyeCollider)){
			SetUpWin();
		}
	}

	public override void SetUpWin(){
		SignalForEndOfGame(1.0f);
	}
	public override void SetUpLose(){
		SignalForEndOfGame(0f);
	}
}
