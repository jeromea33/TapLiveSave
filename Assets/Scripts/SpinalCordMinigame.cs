using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class SpinalCordMinigame : MiniGame {

	public GameObject Girl;

	void OnDisable(){
		Girl.GetComponent<TapGesture>().Tapped -= OnTap;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(GetBarPercentage() < 0f){
			SetUpWin();
		}
	}

	public override void SetUpWin(){
		Girl.SetActive(false);
		stopped = true;
		DestroyTimer();
		WinUI.SetActive (true);
	}

	public override void SetUpLose(){
		Girl.SetActive(false);
		stopped = true;
		DestroyTimer();
		LoseUI.SetActive(true);
	}

	public override void SetUp(){
		Girl.GetComponent<TapGesture>().Tapped += OnTap;
	}

	void OnTap(object sender, System.EventArgs e){
		SetUpLose();
	}
}
