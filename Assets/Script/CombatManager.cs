using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    DropZone zoneField = null, zoneEnemy = null, zoneSupport = null, hand = null;
    public Card[] cardInField;
    public Card Enemy;
    public Card Support;
    Scarti scarti;
    EnemySpawn enemiesSpawn;
    Deck deck;
    Resources resources;
    [HideInInspector]
    public bool InCombat = false;
    int numberOfCardInField;
    int CardDestroied;

    private void Awake()
    {
        cardInField = new Card[zoneField.CardLimit];
        scarti = FindObjectOfType<Scarti>();
        enemiesSpawn = FindObjectOfType<EnemySpawn>();
        deck = FindObjectOfType<Deck>();
        resources = FindObjectOfType<Resources>();
    }

    private void Start()
    {
        if (zoneEnemy.transform.childCount != 0)
            Enemy = zoneEnemy.transform.GetChild(0).GetComponent<Card>();
    }

    public void ButtonCombat()
    {
        if (zoneEnemy.transform.childCount != 0 && zoneField.transform.childCount != 0)
            Combat();
        else
            Debug.Log("Devi posizionare le carte");
    }

    public void Combat()
    {
        InCombat = true;
        Debug.Log("MORTAL COMBAAAAAAT");
        if (zoneSupport.transform.childCount != 0)
            Support = zoneSupport.transform.GetChild(0).gameObject.GetComponent<Card>();
        Enemy = zoneEnemy.transform.GetChild(0).GetComponent<Card>();
        numberOfCardInField = zoneField.transform.childCount;
        // Prendo i componenti delle carte in campo 
        for (int i = 0; i < numberOfCardInField; i++)
        {
            cardInField[i] = zoneField.transform.GetChild(i).gameObject.GetComponent<Card>();

            // Aggiungo il valore del supporto
            if (Support != null)
            {
                switch (Support.Data.Supporto)
                {
                    case Buff.Attack:
                        cardInField[i].Attack++;
                        break;
                    case Buff.Life:
                        cardInField[i].Life++;
                        break;
                    default:
                        Debug.Log("No buff");
                        break;
                }
            }
        }

        if (Enemy != null)
        {
            //conta i guardiani in campo
            CardDestroied = 3 - numberOfCardInField;

            do
            {
                // Il nemico combatte i guardiano a turno
                for (int i = 0; i < zoneField.CardLimit; i++)
                {
                    if (cardInField[i] != null && InCombat)
                    {
                        if (Enemy.IsAlive)
                        {
                            Attack(i);
                            Purifica(i);
                        }
                    }
                }
            } while (InCombat && CardDestroied < 3);

            if (!Enemy.IsAlive || Enemy.Type == CardType.Guardian)
                enemiesSpawn.SpawnEnemy();
            else if (CardDestroied == 3)
                FindObjectOfType<EndCondiction>().EndGame(false);
        }
        else
        {
            InCombat = false;
            Debug.Log("Non ci sono nemici");
        }
    }

    void Attack(int _cardInField)
    {
        Enemy.Life -= cardInField[_cardInField].Attack;
        cardInField[_cardInField].Life -= Enemy.Attack;
        cardInField[_cardInField].Data.LifeChange = cardInField[_cardInField].Life;
        CheckLifeAndDestroy(_cardInField);
    }

    void CheckLifeAndDestroy(int _cardInField)
    {        
        if (!cardInField[_cardInField].IsAlive)
        {
            Destroy(cardInField[_cardInField].gameObject);
            CardDestroied++;
        }
        if (!Enemy.IsAlive)
        {
            InCombat = false;
            scartGuardian();
            Destroy(Enemy.gameObject);
            resources.AddCoin(1);
        }
    }

    void Purifica (int _cardInField)
    {
        if (Enemy.IsAlive)
        {
            if (Enemy.Life == 0)
                Enemy.IsPurificato = true;
        }

        if (Enemy.IsPurificato)
        {
            Enemy.Data.LifeChange = Enemy.Data.Life;
            scarti.ScartCard(Enemy);
            Destroy(Enemy.gameObject);
            InCombat = false;
            scartGuardian();
        }
    }

    void scartGuardian()
    {
        // Scarta i guardiani sopravvissuti
        for (int i = 0; i < (zoneField.CardLimit); i++)
        {
            if (cardInField[i] != null)
            {
                if (cardInField[i].IsAlive)
                {
                    scarti.ScartCard(cardInField[i]);
                    Destroy(cardInField[i].gameObject);
                }
            }
        }

        if (Support != null)
        {
            scarti.ScartCard(Support);
            Destroy(Support.gameObject);
        }

        int cardInHand = hand.transform.childCount;
        for (int i = 0; i < cardInHand; i++)
        {
            scarti.ScartCard(hand.transform.GetChild(i).GetComponent<Card>());
            Destroy(hand.transform.GetChild(i).gameObject);
        }
        deck.Draw(5);
    }
}