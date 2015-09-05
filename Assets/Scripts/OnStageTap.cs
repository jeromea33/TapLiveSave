using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class OnStageTap : MonoBehaviour {

	public StagePlace stageOfThis;
	public StagesScript stageManager; 

	void OnEnable(){
		GetComponent<TapGesture>().Tapped += OnTap;
	}

	void OnDisable(){
		GetComponent<TapGesture>().Tapped -= OnTap;
	}

	void OnTap(object sender, System.EventArgs e){
		Debug.Log("Tapped: " + gameObject.name);
		stageManager.toStage(stageOfThis);
	}

}
