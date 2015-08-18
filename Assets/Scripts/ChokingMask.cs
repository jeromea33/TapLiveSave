using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class ChokingMask : MonoBehaviour {

	void OnEnable(){
		GetComponent<TapGesture>().Tapped += OnTap;
	}

	void OnDisable(){
		GetComponent<TapGesture>().Tapped -= OnTap;
	}
	
	void OnTap(object sender, System.EventArgs e)
	{
		GetComponentInParent<ChokingMinigame> ().OnTap();
		print ("Tapped: Choking guy");
	}
}
