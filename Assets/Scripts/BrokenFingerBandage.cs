using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class BrokenFingerBandage : MonoBehaviour {

	public SpriteRenderer usedSprite;
	//Reference to parent
	public BrokenFinger referenceToParent;

	void OnEnable(){
		usedSprite = GetComponent<SpriteRenderer>();
		usedSprite.enabled = false;
		GetComponent<TapGesture>().Tapped += OnTap;
	}

	void OnDisable(){
		GetComponent<TapGesture>().Tapped -= OnTap;
	}

	void OnTap(object sender, System.EventArgs e)
	{
		Debug.Log(referenceToParent.isBandageTappable() + "Tapped Finger" );
		if(referenceToParent.isBandageTappable())
			usedSprite.enabled = true;
	}

	public bool IsBandageActive(){
		return usedSprite.enabled;
	}
}
