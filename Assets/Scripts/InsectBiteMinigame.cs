using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class InsectBiteMinigame : MiniGame {

	public GameObject[] bites;
	public ArrayList creams;

	/// <summary>
	/// The prefab
	/// </summary>
	public GameObject cream;
	public TapGesture creamContainer;
	public GameObject creamStartPosition;
    public GameObject takip;

	public bool AllBitesTouchedByCreams(){
		foreach (GameObject bite in bites){
			if(!bite.GetComponent<BiteScript>().isCreamed){
				return false;
			}
		}
		return true;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(AllBitesTouchedByCreams()){
			SetUpWin();
		}
		else if (GetBarPercentage() < 0f){
			SetUpLose();
		}
	}

	void OnEnable(){
		creamContainer.Tapped += OnDoubleTap;
	}

	void OnDisable(){
		foreach(GameObject g in GameObject.FindGameObjectsWithTag ("Cream")){
            g.SetActive(false);
			DestroyImmediate(g);
		}
		creamContainer.Tapped -= OnDoubleTap;
	}

	void OnDoubleTap(object sender, System.EventArgs args){
		Instantiate(cream, creamStartPosition.transform.position, Quaternion.identity);
	}
	
	public override void SetUpWin(){
        cream.SetActive(false);
        stopped = true;
		DestroyTimer();
		WinUI.SetActive (true);
	}

	public override void SetUpLose(){
        cream.SetActive(false);
		stopped = true;
		DestroyTimer();
		LoseUI.SetActive(true);
	}
}
