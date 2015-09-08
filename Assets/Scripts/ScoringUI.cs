using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoringUI : MonoBehaviour {

	public Text scoreText;
	public Image Health1;
	public Image Health2;
	public Image Health3;
	public Image UpperText;
	public Image LowerText;
	public Sprite cross;
	public Sprite health;
	public Sprite sceneComplete;
	public Sprite sceneFail;
	public Sprite gameOver;

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
			Health3.sprite = cross;
		}
		else if (numberOfHealth == 2){
			numberOfHealth--;
			Health2.sprite = cross;
		}
		else if (numberOfHealth == 1){
			numberOfHealth--;
			Health1.sprite = cross;
		}
	}

	public void GameOver(){
		UpperText.sprite = gameOver;
	}

	public void ResetHealth(){
		numberOfHealth = 3;
		Health1.sprite = health;
		Health2.sprite = health;
		Health3.sprite = health;
	}

	public void SetUpComplete(){
		UpperText.sprite = sceneComplete;
	}

	public void SetUpFail(){
		UpperText.sprite = sceneFail;
		DecreaseHealth();
	}

	public void ResetScore(){
		score = 0;
		scoreText.text = "0";
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
