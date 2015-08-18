using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class LeftMaskScript : MonoBehaviour {

	public string direction;
	private TapGesture tapGesture;

	void OnEnable(){
		GetComponent<TapGesture>().Tapped += IsTapped;
	}

	void IsTapped(object sender, System.EventArgs e)
	{
		GetComponentInParent<WindowScript> ().Tapped (direction);
		print ("Tapped: " + direction);
	}
}
