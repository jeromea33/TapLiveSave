using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class WindowScript : MonoBehaviour {
	
	public string state = "open";
	//====================================
	private BoxCollider2D[] Colliders;



	public void Tapped(string direction){
		if (direction == "left") {
			if(GetComponent<Animator>().GetBool ("LeftClosed"))
				GetComponent<Animator>().SetBool ("LeftClosed", false);
			else
				GetComponent<Animator>().SetBool ("LeftClosed", true);
		}
		else if (direction == "right"){
			if(GetComponent<Animator>().GetBool ("RightClosed"))
				GetComponent<Animator>().SetBool ("RightClosed", false);
			else
				GetComponent<Animator>().SetBool ("RightClosed", true);
		}

		if (GetComponent<Animator> ().GetBool ("LeftClosed") && GetComponent<Animator> ().GetBool ("RightClosed"))
			state = "closed";
		else
			state = "open";
	}
}
