using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class Choking : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		GetComponent<TapGesture>().Tapped += OnTap;
	}
	
	// Update is called once per frame
	void OnDisable () {
		GetComponent<TapGesture>().Tapped -= OnTap;
	}

	void OnTap(object sender, System.EventArgs e){

	}
}
