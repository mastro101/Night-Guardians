using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    DropZone zoneField = null, zoneEnemy = null, zoneSupport = null, hand = null;
    public Card[] cardInField;
    [HideInInspector]
    public Card Enemy;
    [HideInInspector]
    public Card Support;
    Scarti scarti;
    EnemySpawn enemiesSpawn;
    Deck deck;
    Resources resources;
    [HideInInspector]
    public bool InCombat = false;
    int numberOfCardInField;
    int CardDestroied;
    [SerializeField]
    Transform chooseCardsPanel;
    ContenitoreCards contenitoreCards;
    PotenzaFazioni potenzaFazioni;

    private void Awake()
    {
        cardInField = new Card[zoneField.CardLimit];
        scarti = FindObjectOfType<Scarti>();
        enemiesSpawn = FindObjectOfType<EnemySpawn>();
        deck = FindObjectOfType<Deck>();
        resources = FindObjectOfType<Resources>();
        contenitoreCards = FindObjectOfType<ContenitoreCards>();
        potenzaFazioni = FindObjectOfType<PotenzaFazioni>();
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

            if (!Enemy.IsAlive || Enemy.Type == CardType.Pirata)
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

            potenzaFazioni.RemovePotenza(Enemy.Fazione, Enemy.Grado);
            scartGuardian();
            Destroy(Enemy.gameObject);
            resources.AddCoin(1);
            evolveCard();
        }
    }

    GameObject card;

    void evolveCard()
    {
        for (int i = 0; i < (zoneField.CardLimit); i++)
        {
            if (cardInField[i] != null)
            {
                if (cardInField[i].IsAlive)
                {
                    if (cardInField[i].Data.Evolution != null)
                    {
                        if (!chooseCardsPanel.gameObject.activeInHierarchy)
                            chooseCardsPanel.gameObject.SetActive(true);
                        card = Instantiate(cardInField[i].gameObject, chooseCardsPanel.GetChild(0));
                        card.AddComponent<EvolveCard>().OriginalCard = cardInField[i];
                        Destroy(card.GetComponent<Draggable>());
                    }
                }
            }
        }

        if (Support != null)
        {
            if (Support.Data.Evolution != null)
            {
                if (!chooseCardsPanel.gameObject.activeInHierarchy)
                    chooseCardsPanel.gameObject.SetActive(true);
                Support.transform.rotation = Quaternion.Euler(0, 0, 0);
                card = Instantiate(Support.gameObject, chooseCardsPanel.GetChild(0));
                card.AddComponent<EvolveCard>().OriginalCard = Support;
                Destroy(card.GetComponent<Draggable>());
            }
        }
    }

    void choosePurificatedCard()
    {
        chooseCardsPanel.gameObject.SetActive(true);
        int i;
        int card1 = -1, card2 = -1;
        int limit;
        if (Enemy.Grado == 3)
            limit = 2;
        else
            limit = 3;
        for (int x = 0; x < limit; x++)
        {
            do
            {
                i = Random.Range(0, contenitoreCards.Cards.Length);
            }
            while (contenitoreCards.Cards[i].Grado != Enemy.Grado || contenitoreCards.Cards[i].Fazione != Enemy.Fazione || (x == 2 && card1 == i) || (x == 3 && card2 == i));
            if (x == 1)
                card1 = i;
            if (x == 2)
                card2 = i;

            card = Instantiate(Enemy.gameObject, chooseCardsPanel.GetChild(0));
            card.GetComponent<Card>().Data = contenitoreCards.Cards[i];
            card.AddComponent<ChoosePurification>().OriginalCard = Enemy;
            Destroy(card.GetComponent<Draggable>());
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
            potenzaFazioni.RemovePotenza(Enemy.Fazione, Enemy.Grado);
            choosePurificatedCard();
            scarti.ScartCard(Enemy);
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
                }
            }
        }

        if (Support != null)
        {
            scarti.ScartCard(Support);
        }

        int cardInHand = hand.transform.childCount;
        for (int i = 0; i < cardInHand; i++)
        {
            scarti.ScartCard(hand.transform.GetChild(0).GetComponent<Card>());
        }
        deck.Draw(5);
    }


}