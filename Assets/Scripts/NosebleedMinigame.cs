using UnityEngine;
using System.Collections;
using TouchScript.Gestures.Simple;

public class NosebleedMinigame : MiniGame {

	[Header("Nose bleed settings")]
	public GameObject blood;
	public GameObject maskLeft;
	public GameObject maskRight;
	public GameObject maskCenter;
	public GameObject Nosebleed;
	[Tooltip("Game will end if this is reached in Blood.localScale.x")]
	public float gameEndTreshold = 50f;
	[Tooltip("Game Blood.localScale.x will start here")]
	public float gameStartTreshold = 1f;
	[Tooltip("Game will win if this is met")]
	[Range(0.001f, 2f)]
	public float distanceRequirment = 0.01f;
	[Range(1f, 20f)]
	public float bloodDropSpeed = 5f;
	[Range(1f, 20f)]
	public float bloodSpreadSpeed = 1f;

	// Distance markers
	public float leftToNose = 100f;
	public float rightToNose = 100f;
	public float leftToRight = 100f;

	// Update is called once per frame
	public override void Update () {
		base.Update();
		if(getBloodLength() > gameEndTreshold){
			SetUpLose();
		}
		else if (StartProcess){
			leftToNose 	= Mathf.Abs (maskLeft.transform.position.sqrMagnitude 
						- maskCenter.transform.position.sqrMagnitude);
			rightToNose = Mathf.Abs (maskCenter.transform.position.sqrMagnitude
						- maskRight.transform.position.sqrMagnitude);
			leftToRight = Mathf.Abs (maskLeft.transform.position.sqrMagnitude 
						- maskRight.transform.position.sqrMagnitude);

			blood.transform.localScale += new Vector3(bloodSpreadSpeed ,bloodDropSpeed) * Time.deltaTime;
			if (leftToNose <= distanceRequirment && rightToNose <= distanceRequirment && leftToRight <= distanceRequirment){
				blood.SetActive (false);
				SetUpWin();
			}
		}
	}

	void OnDisable(){
		maskLeft.GetComponent<SimplePanGesture>().PanStarted -= OnPanStartLeft;
		maskLeft.GetComponent<SimplePanGesture>().PanCompleted -= OnPanEndLeft;
		maskRight.GetComponent<SimplePanGesture>().PanStarted -= OnPanStartRight;
		maskRight.GetComponent<SimplePanGesture>().PanCompleted -= OnPanEndRight;
	}

	public override void SetUpLose(){
		Nosebleed.SetActive (false);
		blood.SetActive (false);
		LoseUI.SetActive(true);
	}

	public override void SetUpWin(){
		Nosebleed.SetActive (false);
		WinUI.SetActive (true);
	}

	public override void SetUp(){
		SetRealSpeed();
		blood.transform.localScale = new Vector3(1f ,gameStartTreshold);
		maskLeft.GetComponent<SimplePanGesture>().PanStarted += OnPanStartLeft;
		maskLeft.GetComponent<SimplePanGesture>().PanCompleted += OnPanEndLeft;
		maskRight.GetComponent<SimplePanGesture>().PanStarted += OnPanStartRight;
		maskRight.GetComponent<SimplePanGesture>().PanCompleted += OnPanEndRight;
	}

	/// <summary>
	/// Sets the real speed of the blood coming out of her nose. 
	/// Based on difficulty of course!
	/// </summary>
	void SetRealSpeed(){
		float result_drop = bloodDropSpeed;
		float result_spread = bloodSpreadSpeed;
		switch (currentDifficulty){
		case Difficulty.VeryEasy:
			result_drop = bloodDropSpeed * veryEasyMultiplier;
			result_spread = bloodSpreadSpeed * veryEasyMultiplier;
			break;
		case Difficulty.Easy:
			result_drop = bloodDropSpeed * easyMultiplier;
			result_spread = bloodSpreadSpeed * easyMultiplier;
			break;
		case Difficulty.Intermediate:
			result_drop = bloodDropSpeed * intermeddiateMultiplier;
			result_spread = bloodSpreadSpeed * intermeddiateMultiplier;
			break;
		case Difficulty.Hard:
			result_drop = bloodDropSpeed * hardMultiplier;
			result_spread = bloodSpreadSpeed * hardMultiplier;
			break;
		case Difficulty.VeryHard:
			result_drop = bloodDropSpeed * veryHardMultiplier;
			result_spread = bloodSpreadSpeed * veryHardMultiplier;
			break;
		case Difficulty.Impossible:
			result_drop = bloodDropSpeed * impossibleMultiplier;
			result_spread = bloodSpreadSpeed * impossibleMultiplier;
			break;
		}
		bloodDropSpeed = result_drop;
	}

	float getBloodLength(){
		return blood.transform.localScale.y;
	}

	private Vector3 leftMaskPosition;
	private Vector3 rightMaskPosition;
	void OnPanStartLeft(object sender, System.EventArgs e)
	{
		leftMaskPosition = maskLeft.transform.position;
		print ("Pan Start: " + gameObject.name);
	}

	void OnPanEndLeft(object sender, System.EventArgs e)
	{
		maskLeft.transform.position = leftMaskPosition;
		print ("Pan End: " + gameObject.name);
	}

	void OnPanStartRight(object sender, System.EventArgs e)
	{
		rightMaskPosition = maskRight.transform.position;
		print ("Pan Start: " + gameObject.name);
	}
	
	void OnPanEndRight(object sender, System.EventArgs e)
	{
		maskRight.transform.position = rightMaskPosition;
		print ("Pan End: " + gameObject.name);
	}
}
