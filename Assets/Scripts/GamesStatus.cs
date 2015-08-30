using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;


[Serializable]
public class GamesStatus {

	public static List<string> minigameTitles;
	public static List<Difficulty> minigameAccomplishedDifficulties;

	public List<string> _minigameTitles;
	public List<Difficulty> _minigameAccomplishedDifficulties;

	public static void CreateList(MiniGame[] minigames){

		minigameTitles = new List<string>();

		foreach (MiniGame item in minigames) {
			minigameTitles.Add (item.title);
		}

		minigameAccomplishedDifficulties = new List<Difficulty>();

		foreach(string x in minigameTitles){
			minigameAccomplishedDifficulties.Add (Difficulty.VeryEasy);
		}
	}

	public static void FillList(){
		Stream stream = new FileStream("difdata", FileMode.Open, FileAccess.Read);
		BinaryFormatter reader = new BinaryFormatter();
		GamesStatus stats = (GamesStatus)reader.Deserialize(stream);
		minigameTitles = stats._minigameTitles;
		minigameAccomplishedDifficulties = stats._minigameAccomplishedDifficulties;
		stream.Close ();
	}

	/// <summary>
	/// Updates the status of the minigame
	/// </summary>
	/// <param name="Title">Title of the minigame as MiniGame.title</param>
	/// <param name="gameDifficulty">Game difficulty.</param>
	public static void UpdateTitle(string Title,Difficulty gameDifficulty){
		int x = 0;
		if(minigameTitles.Contains (Title)){
			x = minigameTitles.IndexOf (Title);
			minigameAccomplishedDifficulties[x] = gameDifficulty;
		}
	}

	public static Difficulty GetDifficultyOf(string Title){
		int x = 0;
		if(minigameTitles.Contains (Title)){
			x = minigameTitles.IndexOf (Title);
			return minigameAccomplishedDifficulties[x];
		}
		else
			return Difficulty.None;
	}

	public static void Save(){
		Stream stream = new FileStream("difdata", FileMode.Create, FileAccess.Write);
		BinaryFormatter formatter = new BinaryFormatter();
		GamesStatus x = new GamesStatus();
		x._minigameTitles = minigameTitles;
		x._minigameAccomplishedDifficulties = minigameAccomplishedDifficulties;
		formatter.Serialize (stream, x);
		stream.Flush();
		stream.Close ();
	}
}