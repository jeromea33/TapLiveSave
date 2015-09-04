using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Games status class for GameStats management
/// </summary>
[Serializable]
public class GamesStatus {

	private const string SAVEPATH = "difdata";

	/// <summary>
	/// Status list of the minigames
	/// </summary>
	private static List<MinigameStats> minigameStatus;
	/// <summary>
	/// minigameStatus used for saving as it is created inside an object and saved
	/// as binary
	/// </summary>
	public List<MinigameStats> _minigameStatus;

	/// <summary>
	/// Creates the list or the initial status of saved games
	/// </summary>
	/// <param name="minigames">The minigames list from GameManager.minigames[]</param>
	private static void CreateList(MiniGame[] minigames){
		//Initialize the list
		minigameStatus = new List<MinigameStats>();
		MinigameStats placeholder;
		//Fill the list
		foreach (MiniGame item in minigames) {
			placeholder = new MinigameStats(item.title);
			minigameStatus.Add (placeholder);
		}
	}

	/// <summary>
	/// Main initializer of lists
	/// Create list if it doesn't exists
	/// If there are new minigames creates a new list
	/// Load list if it exists
	/// </summary>
	public static void InitializeStatus(bool force){
		MiniGame[] minigames = GameObject.FindGameObjectWithTag (GameManager.Tag).GetComponent<GameManager>().games;
		if (force){
			CreateList(minigames);
			return;
		}
		try{
			if (File.Exists(SAVEPATH) && CheckIfUpdated(minigames))
				Load ();
			else
				CreateList(minigames);
		}
		catch(Exception e){
			Debug.Log ("Error on Message initialize" + e.Message);
			CreateList(minigames);
		}
	}

	/// <summary>
	/// Performs a series of test to check wether or not the list of minigames were updated
	/// Just have faith
	/// </summary>
	/// <returns><c>true</c>, if faithfull <c>false</c> otherwise.</returns>
	/// <param name="minigames">Minigames</param>
	private static bool CheckIfUpdated(MiniGame[] minigames){
		List<string> titles = new List<string>();
		GamesStatus x = new GamesStatus();
		bool faith = true;
		x.GetGames();
		if (minigames.Length != x._minigameStatus.Count)
			return false;
		return faith;
	}

	/// <summary>
	/// Fills the GameStatus.minigameTitles and GameStatus.minigameAccomplishedDifficulties
	/// Is Equivalent to loading a save game
	/// </summary>
	private static void FillList(){
		Stream stream = new FileStream(SAVEPATH, FileMode.Open, FileAccess.Read);
		BinaryFormatter reader = new BinaryFormatter();
		GamesStatus stats = (GamesStatus)reader.Deserialize(stream);
		minigameStatus = stats._minigameStatus;
		stream.Close ();
	}

	/// <summary>
	/// Used for update checks
	/// </summary>
	public void GetGames(){
		Stream stream = new FileStream(SAVEPATH, FileMode.Open, FileAccess.Read);
		BinaryFormatter reader = new BinaryFormatter();
		GamesStatus stats = (GamesStatus)reader.Deserialize(stream);
		_minigameStatus = stats._minigameStatus;
		stream.Close ();
	}

	/// <summary>
	/// Alias for FillList()
	/// </summary>
	private static void Load(){
		FillList();
	}

	/// <summary>
	/// Updates Minigame stats
	/// </summary>
	/// <returns><c>true</c>, if stats was updated, <c>false</c> otherwise.</returns>
	/// <param name="Title">Title of the minigame</param>
	/// <param name="gameDifficulty">New game difficulty of the minigame</param>
	/// <param name="save">Automatically saves the game if true</param>
	public static bool UpdateMinigameStats(string Title, Difficulty gameDifficulty, bool save){
		foreach(MinigameStats stats in minigameStatus){
			if(stats.title.ToLower().Equals(Title.ToLower())){
				stats.difficultyStatus = gameDifficulty;
				if (save)
					Save();
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Gets the finished difficulty of the said minigame title.
	/// For example if choking's hard has been completed and saved, it returns that.
	/// </summary>
	/// <returns>The difficulty status of the said minigame</returns>
	/// <param name="Title">The title of the minigame</param>
	public static Difficulty GetDifficultyOf(string Title){
		foreach(MinigameStats stats in minigameStatus){
			if(stats.title.ToLower().Equals(Title.ToLower())){
				return stats.difficultyStatus;
			}
		}
		return Difficulty.None;
	}

	/// <summary>
	/// Saves the GameStatus.minigameTitles and GameStatus.minigameAccomplishedDifficulties
	/// Must be called everytime a game is won.
	/// </summary>
	public static void Save(){
		Stream stream = new FileStream(SAVEPATH, FileMode.Create, FileAccess.Write);
		BinaryFormatter formatter = new BinaryFormatter();
		GamesStatus x = new GamesStatus();
		x._minigameStatus = minigameStatus;
		formatter.Serialize (stream, x);
		stream.Flush();
		stream.Close ();
	}
}

/// <summary>
/// A struct for stats in games, for easier housekeeping
/// </summary>
[Serializable]
public class MinigameStats{
	/// <summary>
	/// The title of the minigame
	/// </summary>
	public string title;
	/// <summary>
	/// The difficulty status of the minigame
	/// </summary>
	public Difficulty difficultyStatus;

	/// <summary>
	/// Initialize the Stats.
	/// </summary>
	/// <param name="theTitle">The title of the minigame</param>
	/// <param name="theDifficulty">The difficulty of the minigame</param>
	public MinigameStats(string theTitle, Difficulty theDifficulty){
		title = theTitle;
		difficultyStatus = theDifficulty;
	}

	/// <summary>
	/// Initialize the stats, Difficulty is automatically set to Difficulty.None
	/// </summary>
	/// <param name="theTitle">The title of the minigame</param>
	public MinigameStats(string theTitle){
		title = theTitle;
		difficultyStatus = Difficulty.None;
	}

	/// <summary>
	/// Provides an in class validation if isGame is Unlocked
	/// </summary>
	/// <returns><c>true</c>, if game is unlocked,  <c>false</c> otherwise.</returns>
	/// <param name="requiredDifficulty">Required difficulty.</param>
	public bool isGameUnlocked(Difficulty requiredDifficulty){
		return requiredDifficulty >= difficultyStatus;
	}
}