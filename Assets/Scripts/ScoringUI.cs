using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoringUI : MonoBehaviour {

	public Text scoreText;
	public Image Health1;
	public Image Health2;
	public Image Health3;

	public int score = 0;
	public int numberOfHealth = 3;
	private int internalScore = 0;

	private bool isCounting = false;
	// Update is called once per frame
	void Update () {
		internalScore = int.Parse (scoreText.text);
		if (internalScore < score)
			AddToScore(1);
		else if (internalScore > score)
			AddToScore(-1);
		else if (internalScore == score){
			StartCoroutine(FinishedCounting ());
		}
		else
			isCounting = false;
	}

	public bool IsScoreUpdateOver(){
		return isCounting;
	}

	public IEnumerator FinishedCounting(){
		yield return new WaitForSeconds(1);
		Debug.Log("Finished Counting");
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ScoreCountingEnded();
	}

	public void DecreaseHealth(){
		if (numberOfHealth == 3){
			numberOfHealth--;
			Health3.enabled = false;
		}
		else if (numberOfHealth == 2){
			numberOfHealth--;
			Health2.enabled = false;
		}
		else if (numberOfHealth == 1){
			numberOfHealth--;
			Health1.enabled = false;
		}
	}

	public void ResetHealth(){
		numberOfHealth = 3;
		Health1.enabled = true;
		Health2.enabled = true;
		Health2.enabled = true;
	}

	public bool IsGameOver(){
		return (numberOfHealth < 1);
	}

	void AddToScore(int howMany){
		int x = int.Parse(scoreText.text);
		x += howMany;
		scoreText.text = x.ToString();
		isCounting = true;
	}
}
