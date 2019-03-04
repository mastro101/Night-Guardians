using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{

    [SerializeField]
    CardsData[] enemies;
    [SerializeField]
    DropZone spawnZone;
    [SerializeField]
    GameObject card = null;
    public Text TextEnemyLeft;

    int enemyLeft;
    public int EnemyLeft
    {
        get { return enemyLeft; }
        set
        {
            enemyLeft = value;
            TextEnemyLeft.text = "\n" + enemyLeft;
        }
    }

    private void Start()
    {
        SpawnEnemy();
    }

    public void FillEnemyCards(CardsData cardsData)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] == null)
            {
                enemies[i] = cardsData;
                EnemyLeft++;
                break;
            }
        }
    }
	public void FillEnemyCards(List<CardsData> cardsData) {
		foreach(CardsData tmpCardsData in cardsData) {
			FillEnemyCards(tmpCardsData);
		}
	}


	public void SpawnEnemy()
    {
        if (enemies[0] != null)
        {
            Instantiate(card, spawnZone.transform).GetComponent<Card>().Data = enemies[0];
            EnemyLeft--;
            for (int n = 0; n < enemies.Length; n++)
            {
                if (enemies[n] != null)
                {
                    if (n != enemies.Length - 1)
                        enemies[n] = enemies[n + 1];
                    else
                        enemies[n] = null;
                }
            }
        }
        else
        {
            Debug.Log("You Win");
            FindObjectOfType<EndCondiction>().InEnd = true;
        }
    }

}
