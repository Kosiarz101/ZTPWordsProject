using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using ZTPWordsProject.AppModels;

public class RankingList
{

	private List<Ranking> ranking = new List<Ranking>();
	private static RankingList instance;

	private RankingList() {
		string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RankingDatabase.json");
		string serializedRanking = System.IO.File.ReadAllText(fullPath);
		ranking = JsonConvert.DeserializeObject<List<Ranking>>(serializedRanking);
	}

	public static RankingList getInstance()
	{
		if (instance == null)
		{
			instance = new RankingList();
		}
		return instance;
	}

	public Ranking GetRanking(int index)
	{
		if (index > ranking.Count || index < 0)
		{
			throw new IndexOutOfRangeException();
		}
		return ranking[index];
	}

	public void AddToList(Ranking ranking)
	{
		this.ranking.Add(ranking);
	}

	public void RemoveFromList(int index)
	{
		if (index > ranking.Count || index < 0)
		{
			throw new IndexOutOfRangeException();
		}
		ranking.RemoveAt(index);
	}
	public int GetCount()
    {
		return ranking.Count;
    }
	public void SaveToFile()
    {
		string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RankingDatabase.json");
		string serializedRanking = JsonConvert.SerializeObject(ranking, Formatting.Indented);
		System.IO.File.WriteAllText(fullPath, serializedRanking);
	}
}

