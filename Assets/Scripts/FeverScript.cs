using UnityEngine;
using System.Collections;
using TouchScript.Gestures;

public class FeverScript : MiniGame {

	public GameObject ThermoBar;
	[Range (0.01f, 3f)]
	public float ThermoBarSpeed = 0.3f;
	[Range (3.0f, 3.66f)]
	public float LowerWinTreshold = 3.2f;
	[Range (3.66f, 4.41f)]
	public float UpperWinTreshold = 4.1f;
	[Range (1f, 2f)]
	public float BarStarting = 1.51f;
	[Range (6.1f, 6.4f)]
	public float ThermoBreakPoint = 6.3f;

	private bool stopped = false;

	void OnEnable(){
		GetComponent<TapGesture>().Tapped += OnTap;
	}
	
	void OnDisable(){
		GetComponent<TapGesture>().Tapped -= OnTap;
	}
	
	void OnTap(object sender, System.EventArgs e)
	{
		Debug.Log ("Tapped: " + gameObject.name);
		if (GetThermoXScale() >= LowerWinTreshold && GetThermoXScale() <= UpperWinTreshold){
			stopped = true;
			SetUpWin();
		}
		else {
			GetComponent<TapGesture>().Tapped -= OnTap;
			Debug.Log ("The tapping of thermometer is disabled. Coz u suck");

		}
	}

	// Update is called once per frame
	void Update () {
		if (!stopped){
			if (GetThermoXScale() > ThermoBreakPoint)
				SetUpLose();
			else
				ThermoBar.transform.localScale += new Vector3(GetWidenRate(), 0) * Time.deltaTime;
		}
		else{
		}
	}

	public override void SetUpTitle(){

	}

	public override void SetUpLose(){
		MainCameraFunctions.DisableCamera();
		LoseUI.gameObject.SetActive (true);
	}
	public override void SetUpWin(){
		WinUI.gameObject.SetActive (true);
	}
	public override void SetUp(){
		ThermoBar.transform.localScale = new Vector3(BarStarting, 1.121374f);
	}

	float GetWidenRate(){
		float result = ThermoBarSpeed;
		switch (currentDifficulty){
		case Difficulty.VeryEasy:
			result *= veryEasyMultiplier;
			break;
		case Difficulty.Easy:
			result *= easyMultiplier;
			break;
		case Difficulty.Intermediate:
			result *= intermeddiateMultiplier;
			break;
		case Difficulty.Hard:
			result *= hardMultiplier;
			break;
		case Difficulty.VeryHard:
			result *= veryHardMultiplier;
			break;
		case Difficulty.Impossible:
			result *= impossibleMultiplier;
			break;
		default:
			result *= veryEasyMultiplier;
			break;
		}
		return result;
	}

	public float GetThermoXScale(){
		return ThermoBar.transform.localScale.x;
	}                   
}