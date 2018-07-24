using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    int EnemyLeft
    {
        get { return enemyLeft; }
        set
        {
            enemyLeft = value;
            TextEnemyLeft.text = "Enemy\n" + enemyLeft;
        }
    }

    private void Start()
    {
        EnemyLeft = enemies.Length;
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (enemies != null) // DA AGGIUSTARE TROPPO GENEREICO FORSE PARENTESI QUADRE
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
        }
    }

}
