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
			DestroyImmediate(g);
		}
		creamContainer.Tapped -= OnDoubleTap;
	}

	void OnDoubleTap(object sender, System.EventArgs args){
		Instantiate(cream, creamStartPosition.transform.position, Quaternion.identity);
	}
}
