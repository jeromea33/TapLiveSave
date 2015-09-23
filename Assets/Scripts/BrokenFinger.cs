using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class BrokenFinger : MiniGame {

	[Header("Broken Finger Settings")]
	public GameObject finger;
	public GameObject[] pencilPoints;
	public GameObject pencil;
	public BrokenFingerBandage[] bandageParts;

	// Update is called once per frame
	void Update () {
		base.Update ();
		if(!stopped && StartProcess){
			if(GetBarPercentage() < 0f){
				SetUpLose();
			}
			else if (AllBandagePartsActive()){
				SetUpWin();
			}
		}
	}

	public bool isBandageTappable(){
		bool result = true;
		foreach(GameObject x in pencilPoints){
			Debug.Log(pencil.GetComponent<BoxCollider2D>().OverlapPoint(x.transform.position));
			if(!pencil.GetComponent<BoxCollider2D>().OverlapPoint(x.transform.position))
				result = false;
		}
		return result;
	}

	public bool AllBandagePartsActive(){
		bool result = true;
		foreach(BrokenFingerBandage x in bandageParts){
			if(!x.IsBandageActive())
				result = false;
		}
		return result;
	}

	public override void SetUpLose(){
		stopped = true;
		DestroyTimer();
		pencil.SetActive(false);
		foreach(BrokenFingerBandage bandagePart in bandageParts){
			DestroyImmediate(bandagePart.gameObject);
		}
		finger.SetActive(false);
		//SignalForEndOfGame(0f);
		LoseUI.SetActive(true);
	}

	public override void SetUpWin(){
		stopped = true;
		DestroyTimer();
		pencil.SetActive(false);
		foreach(BrokenFingerBandage bandagePart in bandageParts){
			bandagePart.gameObject.SetActive(false);
		}
		finger.SetActive(false);
		//SignalForEndOfGame(1.0f);
		WinUI.SetActive(true);
	}

}
