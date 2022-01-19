using System;

public class RankingList
{
	public RankingList()
	{
		private List<Ranking> ranking = new List<Answers>;
	private static RankingList instance;

	private RankingList() { }

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
		return ranking.Find(index);
    }

	public void AddToList(Ranking ranking)
    {
		ranking.Add(ranking);
    }

	public void RemoveFromList(int index)
    {
		if(index > ranking.Count || index < 0)
        {
			throw new IndexOutOfRangeException();
        }
		ranking.RemoveAt(index);
    }
}
}
