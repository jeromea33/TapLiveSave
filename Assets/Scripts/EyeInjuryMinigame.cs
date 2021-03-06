﻿using UnityEngine;
using System.Collections;

public class EyeInjuryMinigame : MiniGame {

	[Range(1f, 30f)]
	public float moveSpeed;
	public GameObject guy;
	public GameObject glass;
	public GameObject drop;
	public Collider2D dropCollider;
	public GameObject eye;
	public Collider2D eyeCollider;
	protected bool checkWinLose = false;
    protected bool stopChecking = false;

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
        if (checkWinLose && !stopChecking)
        {
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
        glass.SetActive(false);
		stopped = true;
		DestroyTimer();
        stopChecking = true;
        guy.SetActive(false);
		WinUI.SetActive (true);
	}

	public override void SetUpLose(){
		stopped = true;
		DestroyTimer();
        stopChecking = true;
        guy.SetActive(false);
		LoseUI.SetActive(true);
	}
}
