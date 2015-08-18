using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class BulbScript : MonoBehaviour {

	public string state = "on";

	void OnEnable(){
		GetComponent<TapGesture> ().Tapped += OnTap;
	}

	void OnDisable(){
		GetComponent<TapGesture> ().Tapped -= OnTap;
	}

	void OnTap(object sender, System.EventArgs e){
		Debug.Log ("Bulb Tapped");
		if (GetComponent<Animator> ().GetBool ("State")) {
			GetComponent<Animator> ().SetBool ("State", false);
			state = "off";
		}
		else if(!GetComponent<Animator> ().GetBool ("State")){
				GetComponent<Animator> ().SetBool ("State", true);
				state = "on";
		}
	}

}
