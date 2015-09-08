using UnityEngine;
using System.Collections;

/// <summary>
///	Do NOT TOUCH the BURN minigame coz it contains bizarre MATH
/// </summary>
public class BurnMinigame : MiniGame {

	[Header("Burn Minigame Settings. Do not touch")]
	public GameObject RayCastOrigin;
	public Vector3 RayCastRotation;
	[Range(70f,90f)]
	public float activateRayCastTreshold = 80f;
	[Range(0f,5f)]
	public float InitialParticleCount;
	[Range(5f,50f)]
	public float MaximumParticleCount = 40f;
	public GameObject sink;
	public GameObject hand;
	public GameObject faucet;
	public GameObject handle;
	public int gameWinDenominator = 100;
	public ParticleSystem particleSystem;

	private int gameWinCounter = 0;
	private bool stopped = false;

	// Update is called once per frame
	// Bizzare math here. DO NOT TOUCH
	public override void Update () {
		base.Update();
		if (!stopped){
			RaycastHit2D rayCasthit = Physics2D.Raycast(RayCastOrigin.transform.position, RayCastRotation);
			Debug.DrawRay(RayCastOrigin.transform.position, RayCastRotation);
			if(rayCasthit.collider != null && getHandleRotation() > activateRayCastTreshold){
				gameWinCounter++;
				if(gameWinCounter > gameWinDenominator){
					SetUpWin();
					return;
				}
			}
			if(GetBarPercentage() < 0f && BarUIObject != null){
				SetUpLose();
			}
			particleSystem.emissionRate = Mathf.InverseLerp(0f, activateRayCastTreshold, getHandleRotation()) * MaximumParticleCount;
		}
	}

	protected float getHandleRotation(){
		return handle.transform.rotation.eulerAngles.z;
	}



	public override void SetUpTitle(){

	}

	public override void SetUpLose(){
		stopped = true;
		DestroyTimer();
		faucet.SetActive (false);
		sink.SetActive (false);
		hand.SetActive (false);
		LoseUI.SetActive (true);
	}

	public override void SetUpWin(){
		stopped = true;
		DestroyTimer();
		//faucet.SetActive (false);
		//sink.SetActive (false);
		//hand.SetActive (false);
		WinUI.SetActive (true);
	}

	public override void SetUp(){
		particleSystem.emissionRate = 0f;
	}
}
