using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class AsthmaMinigame : MiniGame {

	[Header("Asthma Settings")]
	public GameObject AsthmaBar;
	public GameObject AsthmaMainBar;
	public GameObject Inhaler;
	public GameObject Asthmagirl;

	[Range(0f, 1f)]
	public float InitialBarLength;
	[Range(1f, 6f)]
	public float MaxBarLength = 5.474487f;
	[Range(0f, 1f)]
	public float MinBarLength = 0f;


	[Header("Asthma On Tap Settings")]
	[Range(0.01f, 1f)]
	public float TapAddValue = 0.5f;
	[Range(0.01f, 2f)]
	public float veryEasyTapAddMultipler;
	[Range(0.01f, 2f)]
	public float easyTapAddMultipler;
	[Range(0.01f, 2f)]
	public float intermediateTapAddMultipler;
	[Range(0.01f, 2f)]
	public float hardTapAddMultipler;
	[Range(0.01f, 2f)]
	public float veryHardTapAddMultipler;
	[Range(0.01f, 2f)]
	public float impossibleTapAddMultipler;

	[Header("Asthma Bar Settings")]
	[Range(0.01f, 1f)]
	public float BarDecrease = 0.5f;
	[Range(0.01f, 2f)]
	public float veryEasyBarDecreaseMultipler;
	[Range(0.01f, 2f)]
	public float easyBarDecreaseMultipler;
	[Range(0.01f, 2f)]
	public float intermediateBarDecreaseMultipler;
	[Range(0.01f, 2f)]
	public float hardBarDecreaseMultipler;
	[Range(0.01f, 2f)]
	public float veryHardBarDecreaseMultipler;
	[Range(0.01f, 2f)]
	public float impossibleBarDecreaseMultipler;

	private float BarWidth = 0.8041242f;
	private bool stopped = false;

	// Update is called once per frame
	void Update () {
		if (!stopped){
			AsthmaBar.transform.localScale -= new Vector3(0f, RealBarDecreaseValue()) * Time.deltaTime;
			if (GetBarPercentage() <= 0f && BarUIObject != null){
				SetUpLose();
			}
			if (AsthmaBar.transform.localScale.y >= MaxBarLength){
				AsthmaBar.transform.localScale = new Vector3(BarWidth, MaxBarLength);

				SetUpWin();
			}
		}
	}

	float getBarStatus(){
		return AsthmaBar.transform.localScale.y;
	}


	public override void SetUpTitle(){

	}

	public override void SetUpLose(){
		stopped = true;
		DestroyTimer();
		Asthmagirl.SetActive (false);
		AsthmaMainBar.SetActive (false);
		LoseUI.SetActive(true);
	}

	public override void SetUpWin(){
		stopped = true;
		DestroyTimer();
		Asthmagirl.SetActive (false);
		AsthmaMainBar.SetActive (false);
		WinUI.SetActive(true);
	}

	public override void SetUp(){
		AsthmaBar.transform.localScale = new Vector3(BarWidth, InitialBarLength);
		Inhaler.GetComponent<TapGesture>().Tapped += OnTap;
	}

	void OnDisable(){
		Inhaler.GetComponent<TapGesture>().Tapped -= OnTap;
	}

	void OnTap(object sender, System.EventArgs param){
		Inhaler.GetComponent<Animator>().SetTrigger("Tapped");
		AsthmaBar.transform.localScale += new Vector3(0f, RealTapAddValue());
	}

	float RealTapAddValue(){
		switch(currentDifficulty){
		case Difficulty.VeryEasy:
			return TapAddValue * veryEasyTapAddMultipler;
			break;
		case Difficulty.Easy:
			return TapAddValue * easyTapAddMultipler;
			break;
		case Difficulty.Intermediate:
			return TapAddValue * intermediateTapAddMultipler;
			break;
		case Difficulty.Hard:
			return TapAddValue * hardTapAddMultipler;
			break;
		case Difficulty.VeryHard:
			return TapAddValue * veryHardTapAddMultipler;
			break;
		case Difficulty.Impossible:
			return TapAddValue * impossibleTapAddMultipler;
			break;
		}
		return TapAddValue;
	}

	float RealBarDecreaseValue(){
		switch(currentDifficulty){
		case Difficulty.VeryEasy:
			return BarDecrease * veryEasyBarDecreaseMultipler;
			break;
		case Difficulty.Easy:
			return BarDecrease * easyBarDecreaseMultipler;
			break;
		case Difficulty.Intermediate:
			return BarDecrease * intermediateBarDecreaseMultipler;
			break;
		case Difficulty.Hard:
			return BarDecrease * hardBarDecreaseMultipler;
			break;
		case Difficulty.VeryHard:
			return BarDecrease * veryHardBarDecreaseMultipler;
			break;
		case Difficulty.Impossible:
			return BarDecrease * impossibleBarDecreaseMultipler;
			break;
		}
		return TapAddValue;
	}
}
