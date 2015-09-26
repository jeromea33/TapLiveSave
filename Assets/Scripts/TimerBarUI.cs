using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerBarUI : MonoBehaviour {

	public Image emptyBar;
	public Image fullBar;
	public float BarSpeed = 1f;
	public TimerBarAction initialAction;
	public float initialBarValuePercentage = 100f;

	private float BarValue;
	private float fullBarInitialSize;
	private bool barStopped = false;
	
	// Update is called once per frame
	void OnEnable(){
		fullBarInitialSize = fullBar.rectTransform.rect.width;
		SetupInitialBarValue();
	}
	
	public void StopTimer(){
		barStopped = true;
	}

	public void ContinueTimer(){
		barStopped = false;
	}

	void SetupInitialBarValue(){
		Rect x = fullBar.rectTransform.rect;
		x.width = (x.width * (initialBarValuePercentage * 0.01f));

		Debug.Log (x.width);
		fullBar.rectTransform.sizeDelta =  new Vector2(x.width, x.height);
	}

	float BarWidthPercentage(){
		return (fullBarInitialSize * 0.01f);
	}

	public void AddToTimer(float addvalue){
		Vector2 x = fullBar.rectTransform.sizeDelta;
		x.x += addvalue;
		fullBar.rectTransform.sizeDelta = x;
	}

	public float GetCurrentBarPercentage(){
		return (fullBar.rectTransform.rect.width / fullBarInitialSize);
	}

	public void MakeBarFull(){
		Vector2 x = fullBar.rectTransform.sizeDelta;
		x.x = fullBarInitialSize;
		fullBar.rectTransform.sizeDelta = x;
	}

	private float rectWidth;

	void Update(){
		if (!barStopped){
			Vector2 x = fullBar.rectTransform.sizeDelta;
			if (initialAction == TimerBarAction.Decrease){
				x.x = x.x - (BarWidthPercentage() * Time.deltaTime * BarSpeed);
			}
			else if (initialAction == TimerBarAction.Increase){
				x.x = x.x + (BarWidthPercentage() * Time.deltaTime * BarSpeed);
			}
			fullBar.rectTransform.sizeDelta = x;
		}
	}
}

public enum TimerBarAction{
	Increase,
	Decrease
}