using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContenitoreCards : MonoBehaviour {

    EnemySpawn enemySpawn;
    public CardsData[] Cards;
    public CardsData[] EnemyCards;
    CardsData card;
    PotenzaFazioni potenzaFazioni;
    LevelManager levelManager;

    bool IsPossibleFazione = true;
    int SumGradoEnemy;

    private void Awake()
    {
        if (FindObjectOfType<EnemySpawn>())
        {
            enemySpawn = FindObjectOfType<EnemySpawn>();
        }
        if (FindObjectOfType<PotenzaFazioni>())
        {
            potenzaFazioni = FindObjectOfType<PotenzaFazioni>();
        }
        if (FindObjectOfType<LevelManager>())
        {
            levelManager = FindObjectOfType<LevelManager>();
        }
    }

    private void Start()
    {
        fillEnemyDeck();
    }

    private void fillEnemyDeck()
    {
        if (enemySpawn != null)
        {
            enemySpawn.EnemyLeft = 0;
            for (SumGradoEnemy = 0; SumGradoEnemy < levelManager.LevelIncontro;)
            {
                do
                {
                    IsPossibleFazione = false;
                    card = EnemyCards[Random.Range(0, EnemyCards.Length)];
                    if (card != null)
                    {
                        foreach (Fazioni fazione in potenzaFazioni.PossibiliFazioniDaIncontrare)
                        {
                            if (card.Fazione == fazione)
                            {
                                IsPossibleFazione = true;
                                break;
                            }
                        }
                        if (card.Grado + SumGradoEnemy == levelManager.LevelIncontro && IsPossibleFazione == true)
                            break;
                    }
                }
                while (!IsPossibleFazione || SumGradoEnemy + card.Grado > levelManager.LevelIncontro);
                SumGradoEnemy += card.Grado;
                enemySpawn.FillEnemyCards(card);
            }
            SumGradoEnemy = 0;
        }
    }
}
