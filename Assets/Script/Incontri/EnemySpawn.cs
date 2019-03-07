using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{

    List<CardsData> enemies = new List<CardsData>();
    [SerializeField]
    DropZone spawnZone;
    [SerializeField]
    GameObject card = null;
    public Text TextEnemyLeft;

    //int enemyLeft;
    public int EnemyLeft
    {
        get { return enemies.Count; }
        private set { }
    }

    private void Start()
    {
		if(enemies == null) {
			enemies = new List<CardsData>();
		}
		SpawnEnemy();
	}

    public void FillEnemyCards(CardsData cardsData)
    {
		Debug.LogWarning("Nemico: " + cardsData.name);
		if (enemies == null)
		{
			enemies = new List<CardsData>();
		}

		enemies.Add(cardsData);
    }
	public void FillEnemyCards(List<CardsData> cardsData) {
		if (enemies == null)
		{
			enemies = new List<CardsData>();
		}

		foreach (CardsData tmpCardsData in cardsData) {
			FillEnemyCards(tmpCardsData);
		}
	}


	public void SpawnEnemy()
    {
        if (enemies.Count > 0)
        {
			Debug.LogWarning(enemies.Count);
			Debug.LogWarning("Nemico: " + enemies[enemies.Count - 1]);
			Instantiate(card, spawnZone.transform).GetComponent<Card>().Data = enemies[enemies.Count-1];
        }
    }

	public void RemoveLastEnemy()
	{
		if(enemies != null && enemies.Count > 0)
			enemies.RemoveAt(enemies.Count - 1);
	}

}
