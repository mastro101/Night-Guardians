using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [SerializeField]
    public DropZone zoneField = null, zoneEnemy = null, zoneSupport = null, hand = null;
    public Card[] cardsInField;
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
    public int NumberOfCardInField;
    int CardDestroied;
    public Transform chooseCardsPanel;
    ContenitoreCards contenitoreCards;
    PotenzaFazioni potenzaFazioni;
    EndCondiction endCondiction;
	GameObject card; 
	bool endFight;
    Card[] playedCards;
    public Transform recapPanel;

	public int purificationThreshold = 0;
	[HideInInspector] public Card oldEnemy = null;

	public event CombatManagerEvent.CombatManagerDelegate OnSupport;
	public event CombatManagerEvent.CombatManagerDelegate OnStartFight;
	public event CombatManagerEvent.CombatManagerDelegate OnStartTurn;
	public event CombatManagerEvent.CombatManagerDelegate OnEndTurn;
    public event CombatManagerEvent.CombatManagerDelegate OnEndFight;

	private void Awake()
    {
        cardsInField = new Card[zoneField.CardLimit];
        scarti = FindObjectOfType<Scarti>();
        enemiesSpawn = FindObjectOfType<EnemySpawn>();
        deck = FindObjectOfType<Deck>();
        resources = FindObjectOfType<Resources>();
        contenitoreCards = FindObjectOfType<ContenitoreCards>();
        potenzaFazioni = FindObjectOfType<PotenzaFazioni>();
        endCondiction = FindObjectOfType<EndCondiction>();
        playedCards = new Card[4];
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
        if (zoneSupport.transform.childCount != 0)
            Support = zoneSupport.transform.GetChild(0).gameObject.GetComponent<Card>();
        else
            Support = null;
        Enemy = zoneEnemy.transform.GetChild(0).GetComponent<Card>();
        NumberOfCardInField = zoneField.transform.childCount;
		// Prendo i componenti delle carte in campo 
		UpdateCardsInField();

		if(Support != null) 
		{
			CalculateSupport(Support.Data.Supporto, 1);
			invokeOnSupport();
		}
		

		if (Enemy != null)
        {
            //conta i guardiani in campo
            CardDestroied = 3 - NumberOfCardInField;
            if( Support != null)
            {
                GameObject c = Instantiate(Support.gameObject, recapPanel.GetChild(0));
                Destroy(c.GetComponent<Draggable>());

            }
            invokeOnStartFight();

            do
            {
				invokeOnStartTurn();
				// Il nemico combatte i guardiano a turno
				for (int i = 0; i < zoneField.CardLimit; i++)
                {
                    if (cardsInField[i] != null && InCombat)
                    {
                        if (Enemy.IsAlive)
                        {
                            Attack(i);
                            Purifica(i);
                        }
                    }
                }
                invokeOnEndTurn(endFight);
            } while (InCombat && CardDestroied < 3);

			//probabilmente c'è un problema in questo if
            if (!Enemy.IsAlive || Enemy.Type == CardType.Pirata)
			{
				enemiesSpawn.RemoveLastEnemy();
				//Destroy(Enemy.gameObject);
				oldEnemy = Enemy;
				enemiesSpawn.SpawnEnemy();
			}

			if(enemiesSpawn.EnemyLeft <= 0) 
			{
				Debug.Log("You Win");
				endCondiction.EndGame(true);
			}
			else if (CardDestroied == 3 && Enemy.IsAlive && !chooseCardsPanel.gameObject.activeInHierarchy && !recapPanel.gameObject.activeInHierarchy)
			{
                endCondiction.EndGame(false);
			}
        }
    }

    void Attack(int _cardInField)
    {
        cardsInField[_cardInField].Fight(Enemy);
        Enemy.Fight(cardsInField[_cardInField]);
        CheckLifeAndDestroy(_cardInField);
    }

    public void CheckLifeAndDestroy(int _cardInField)
    {        
        if (!cardsInField[_cardInField].IsAlive)
        {
            cardsInField[_cardInField].transform.SetParent(recapPanel.GetChild(0));
            Destroy(cardsInField[_cardInField].gameObject);
            CardDestroied++;
        }
        if (!Enemy.IsAlive)
        {
			//probabilmente non entra mai in questo if
            InCombat = false;
            invokeOnEndFight();

            potenzaFazioni.RemovePotenza(Enemy.Fazione, Enemy.Grado);
            scartGuardian();
			
            resources.AddCoin(1);
            evolveCard();
        }
    }

	public void CalculateSupport(Buff supportType, int value = 1) {
		for (int i = 0; i < NumberOfCardInField; i++)
		{
			switch (supportType)
			{
				case Buff.Attack:
					ChangeCardAttack(i, value);
					break;
				case Buff.Life:
					ChangeCardLife(i, value);
					break;
				default:
					Debug.Log("No buff");
					break;
			}
		}
	}

	public void ChangeCardAttack(int cardPosition, int value) {
		if (cardPosition >= 0 && cardPosition < cardsInField.Length && cardsInField[cardPosition] != null)
			cardsInField[cardPosition].Attack += value;
	}

	public void ChangeCardLife(int cardPosition, int value) {
		if (cardPosition >= 0 && cardPosition < cardsInField.Length && cardsInField[cardPosition] != null)
			cardsInField[cardPosition].Life += value;
	}

	public int GetCardPosition(Card card) {
		for(int i = 0; i < cardsInField.Length; i++) {
			if(cardsInField[i] == card) {
				return i;
			}
		}
		return -1;
	}

	public void UpdateCardsInField() {
		for (int i = 0; i < NumberOfCardInField; i++)
		{
			cardsInField[i] = zoneField.transform.GetChild(i).gameObject.GetComponent<Card>();
		}
	}

    void evolveCard()
    {
		bool evolvingCardFlag = false;
        for (int i = 0; i < (zoneField.CardLimit); i++)
        {
            if (cardsInField[i] != null)
            {
                if (cardsInField[i].IsAlive)
                {
                    if (cardsInField[i].Data.Evolution != null)
                    {
                        recapPanel.gameObject.SetActive(true);
                        if (!chooseCardsPanel.gameObject.activeInHierarchy)
                            chooseCardsPanel.gameObject.SetActive(true);
                        card = Instantiate(cardsInField[i].gameObject, chooseCardsPanel.GetChild(0));
                        card.AddComponent<EvolveCard>().OriginalCard = cardsInField[i];
                        Destroy(card.GetComponent<Draggable>());
                        chooseCardsPanel.gameObject.SetActive(false);
						evolvingCardFlag = true;
					}
                }
            }
        } 

        if (Support != null)
        {
            if (Support.Data.Evolution != null)
            {
                recapPanel.gameObject.SetActive(true);
                if (!chooseCardsPanel.gameObject.activeInHierarchy)
                    chooseCardsPanel.gameObject.SetActive(true);
                Support.transform.rotation = Quaternion.Euler(0, 0, 0);
                card = Instantiate(Support.gameObject, chooseCardsPanel.GetChild(0));
                card.AddComponent<EvolveCard>().OriginalCard = Support;
                Destroy(card.GetComponent<Draggable>());
                chooseCardsPanel.gameObject.SetActive(false);
				evolvingCardFlag = true;

			}
        }

		if(!evolvingCardFlag) {
			recapPanel.gameObject.SetActive(true);
			GameObject tmp = GameObject.FindGameObjectWithTag("RecapSelectButton");
			if(tmp != null) {
				tmp.SetActive(false);
			}
			if (!chooseCardsPanel.gameObject.activeInHierarchy)
				chooseCardsPanel.gameObject.SetActive(true);
			//Support.transform.rotation = Quaternion.Euler(0, 0, 0);
			//card = Instantiate(Support.gameObject, chooseCardsPanel.GetChild(0));
			//card.AddComponent<EvolveCard>().OriginalCard = Support;
			//Destroy(card.GetComponent<Draggable>());
			chooseCardsPanel.gameObject.SetActive(false);
			evolvingCardFlag = true;
		}

    }

    void choosePurificatedCard()
    {
        recapPanel.gameObject.SetActive(true);
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
        chooseCardsPanel.gameObject.SetActive(false);
    }

    void Purifica (int _cardInField)
    {
        if (Enemy.IsAlive)
        {
            if (Enemy.Life <= 0 && Enemy.Life >= purificationThreshold)
			{
				Enemy.IsPurificato = true;
            }
		}

        if (Enemy.IsPurificato)
        {
            potenzaFazioni.RemovePotenza(Enemy.Fazione, Enemy.Grado);
            choosePurificatedCard();
            scarti.ScartCard(Enemy);
            InCombat = false;
            invokeOnEndFight();
            scartGuardian();
        }
    }

    void scartGuardian()
    {
        // Scarta i guardiani sopravvissuti
        for (int i = 0; i < (zoneField.CardLimit); i++)
        {
            if (cardsInField[i] != null)
            {
                if (cardsInField[i].IsAlive)
                {
                    scarti.ScartCard(cardsInField[i]);
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
        if (enemiesSpawn.EnemyLeft > 0)
            deck.Draw(5);
    }

	public void WinGame() {
		InCombat = false;
		invokeOnEndFight();
		endCondiction.EndGame(true);
	}

	public void DestroyOldEnemy() {
		if(oldEnemy != null)
			Destroy(oldEnemy.gameObject);
	}

    #region Event

    void invokeOnStartFight()
    {
        Debug.Log("Inizio Combattimento");
        if (OnStartFight != null)
            OnStartFight();
    }

    void invokeOnEndTurn(bool isEndCombat)
    {
        if (!isEndCombat)
        {
            Debug.Log("Fine Turno");
            if (OnEndTurn != null)
                OnEndTurn();
        }
        endFight = false;
    }

	void invokeOnStartTurn() {
		if (OnStartTurn != null)
			OnStartTurn();
	}

    void invokeOnEndFight()
    {
        endFight = true;
        Debug.Log("Fine Combattimento");
		//UpdateCardsInField();
		for (int i = 0; i < NumberOfCardInField; i++)
        {
			cardsInField[i].ResetCardAttack();
		}
        for (int i = 0; i < 3; i++)
        {
            if (cardsInField[i] != null)
            {
                GameObject c = Instantiate(cardsInField[i].gameObject, recapPanel.GetChild(0));
                Destroy(c.GetComponent<Draggable>());
            }
        }
        if (OnEndFight != null)
            OnEndFight();
		purificationThreshold = 0;
        resetDelegate();
    }

	void invokeOnSupport() {
		if (OnSupport != null)
			OnSupport();
	}


	void resetDelegate()
    {
        OnStartFight = null;
        OnEndTurn = null;
        OnEndFight = null;
		OnStartTurn = null;
		OnSupport = null;
	}
    #endregion
}

public class CombatManagerEvent
{
    public delegate void CombatManagerDelegate();
}