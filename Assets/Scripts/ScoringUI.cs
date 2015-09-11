using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoringUI : MonoBehaviour {
	
	public Button gameOverButton;
	public Text scoreText;
	public Text highScoreText;
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
	public GameManager gameManager;
	public GameObject popup;
	public StagesScript stageMap;
	public Sprite popUpSchoolSprite;
	public Sprite popUpOutputSprite;

	public int score = 0;
	public int numberOfHealth = 3;
	private int internalScore = 0;
	private bool stopProcess = false;

	private bool isCounting = false;

	private bool schoolPopupactivate = true;
	private bool outdoorPopupactivate = true;

	/// <summary>
	/// To force loading of highscore
	/// </summary>
	void OnEnable(){
		stopProcess = false;
		highScoreText.text = GameManager.ForceLoadScore().ToString();
	}

	// Update is called once per frame
	void Update () {
		if (!stopProcess){
			internalScore = int.Parse (scoreText.text);
			if (internalScore < score)
				AddToScore(1);
			else if (internalScore > score)
				AddToScore(-1);
			else if (internalScore == score){
				if (IsGameOver()){
					GameOver();
					return;
				}
				else if(isCounting == true && score == stageMap.highScoreSchoolUnlock && schoolPopupactivate){
					schoolPopupactivate = false;
					isCounting = false;
					stopProcess = true;
					popup.GetComponent<Image>().sprite = popUpSchoolSprite;
					popup.SetActive(true);
				}
				else if (isCounting == true && score == stageMap.highScoreOutdoorUnlock && outdoorPopupactivate){
					outdoorPopupactivate = false;
					isCounting = false;
					stopProcess = true;
					popup.GetComponent<Image>().sprite = popUpOutputSprite;
					popup.SetActive(true);
				}
				else{
					StartCoroutine(FinishedCounting ());
				}
			}
			else
				isCounting = false;
		}
	}

	public bool IsScoreUpdateOver(){
		return isCounting;
	}

	public IEnumerator FinishedCounting(){
		yield return new WaitForSeconds(1);
		Debug.Log("Finished Counting");
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ScoreCountingEnded();
		stopProcess = false;
	}

	public void OnTapPopUp(){
		Debug.Log("Finished Counting");
		GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().ScoreCountingEnded();
		popup.SetActive (false);
		stopProcess = false;
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
		Health1.enabled = false;
		Health2.enabled = false;
		Health3.enabled = false;
		gameOverButton.gameObject.SetActive (true);
	}

	public void GameOverPress(){
		gameOverButton.gameObject.SetActive (false);
		gameObject.SetActive (false);
		ResetHealth();
		ResetScore();
		gameManager.reset();
	}

	public void ResetHealth(){
		numberOfHealth = 3;
		Health1.enabled = true;
		Health2.enabled = true;
		Health3.enabled = true;
		Health1.sprite = health;
		Health2.sprite = health;
		Health3.sprite = health;
		score = 0;
		scoreText.text = "0";
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

	public void UpdateHighScore(int hs){
		highScoreText.text = hs.ToString();
	}
}
