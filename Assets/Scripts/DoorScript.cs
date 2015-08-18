using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class DoorScript : MonoBehaviour {

	public string state = "open";

	void OnEnable(){
		GetComponent <TapGesture> ().Tapped += OnTap;
	}

	void OnDisable(){
		GetComponent <TapGesture> ().Tapped -= OnTap;
	}
	
	// Update is called once per frame
	void OnTap(object sender, System.EventArgs e){
		Debug.Log ("Door Tapped");
		if (GetComponent<Animator>().GetBool("State")){
			GetComponent<Animator>().SetBool("State", false);
			state = "open";
		}
		else if (!GetComponent<Animator>().GetBool("State"))
		{
			GetComponent<Animator>().SetBool("State", true);
			state = "closed";
		}

	}
}
