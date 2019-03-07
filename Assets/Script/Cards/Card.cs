using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public int ID;
    [SerializeField]
    CardsData data;
    public CardsData Data
    {
        get { return data; }
        set
        {
            data = value;
            insertData();
        }
    }
    public Color injuredcolor;
    public Color supportcolor;

    public AudioSource soundCreature;
    public DropZoneType Zone;
    [HideInInspector]
    public CombatManager combatManager;
    [HideInInspector]
    public CardType Type;
    int attack;
    public int Attack
    {
        get { return attack; }
        set
        {
            attack = value;
            attackText.text = attack.ToString();
        }
    }
    bool isAlive = true;
    public bool IsAlive
    {
        get { return isAlive; }
        set
        {
            isAlive = value;
            if (!isAlive)
            {
                /*if (Data.Name == "KrakenTokenEgg")
                {
                    EggEvent.AddEgg(-1);
                }*/
                deck.RemoveCard(Data.Name);
                invokeOnDeath();
                Debug.Log(Data.Name + " is death");
            }
        }
    }
    bool isPurificato;
    public bool IsPurificato
    {
        get { return isPurificato; }
        set
        {
            isPurificato = value;
            if (isPurificato)
            {
                combatManager.InCombat = false;
                Type = CardType.Pirata;
                Debug.Log(Data.Name + " è stato purificato");
            }
        }
    }
    int life;
    public int Life
    {
        get
        { return life; }
        set
        {
            life = value;
            lifeText.text = life.ToString();
            if (life < Data.Life)
                lifeText.color = injuredcolor;
            if (Type == CardType.Pirata)
            {
                if (life <= 0)
                {
                    IsAlive = false;
                }
            }
            else
            {
                if (life < 0)
                    IsAlive = false;
            }
        }
    }
    public EffectVariable[] Effects;
    [HideInInspector]
    int[] variableEffects;
    public int Grado;
    public Fazioni Fazione;

	[HideInInspector] public bool DrawedNow = false;

    PositionCard _positionCard;
    public PositionCard positionCard
    {
        get { return _positionCard; }
        set
        {
            _positionCard = value;
			switch (_positionCard)
			{
				case PositionCard.OnDeck:
					invokeOnDeck();
					break;
				case PositionCard.OnHand:
					InvokeOnHand();
					break;
				case PositionCard.OnField:
					InvokeOnField();
					break;
				case PositionCard.OnScarti:
					InvokeOnScarti();
					break;
				case PositionCard.OnSupport:
					InvokeOnSupport();
					break;
				default:
					break;
			}
		}
	}

	// Position
	public event CardEvent.CardEventDelegate OnDeck;
    public event CardEvent.CardEventDelegate OnHand;
    public event CardEvent.CardEventDelegate OnField;
	public event CardEvent.CardEventDelegate OnSupport;
	public event CardEvent.CardEventDelegate OnScarti;
	public event CardEvent.CardEventDelegate OnDraw;

	// 
	public event CardEvent.CardEventDelegate OnDeath;
    public event CardEvent.CardEventDelCombat OnAttack;



    FazioniClass fazioniClass;

    [SerializeField]
    GameObject TextAndImageObject = null, nameObject = null, statisticObject = null, attackObject = null, lifeObject = null, descriptionObject = null, fazioneObject = null;
    [SerializeField]
    Sprite[] Covers = null;
    public Image imageCard = null, imageCover = null, imageFazione = null, imageFazioneInBasso = null;
    [SerializeField]
    TextMeshProUGUI nameText = null, attackText = null, lifeText = null, descriptionText = null;

    Deck deck;
    Scene scene;

	[HideInInspector] public bool addEffectScript = true;


    private void Awake()
    {
        soundCreature = FindObjectOfType<AudioSource>();
        combatManager = FindObjectOfType<CombatManager>();
        fazioniClass = FindObjectOfType<FazioniClass>();
        deck = FindObjectOfType<Deck>();
        scene = SceneManager.GetActiveScene();
    }

    private void Start()
    {
        if (transform.parent.GetComponent<DropZone>()) {
			Zone = transform.parent.GetComponent<DropZone>().Type;
		}
	}

	private void Update()
	{
		/*
		 * soluzione adottata perchè eseguendo queste operazioni al momento effettivo in cui si pesca la carta,
		 * dato che alcuni component in quel momento non sono completamente settati non avendo eseguito lo Start,
		 * altrimenti non vengono lanciati gli eventi relativi al trovarsi in mano
		 */
		if(DrawedNow) {
			DrawedNow = false;
			positionCard = PositionCard.OnHand;
			InvokeOnDraw();
		}
	}

	void insertData()
    {
        Type = Data.Type;
        Grado = Data.Grado;
        Attack = Data.Attack;
        Life = Data.Life;
        if (Data.Effects != null)
        {
            Effects = new EffectVariable[Data.Effects.Length];
            variableEffects = new int[Data.Effects.Length];

            for (int i = 0; i < Effects.Length; i++)
            {
                Effects[i] = Data.Effects[i];
                variableEffects[i] = Effects[i].Variable;
                addEffect(i);
            }
        }
        nameText.text = Data.Name;
        imageCard.sprite = Data.SpriteImage;
        attackText.text = Data.Attack.ToString();
        lifeText.text = Data.Life.ToString();
        descriptionText.text = Data.Description;
        Fazione = Data.Fazione;
        switch (Fazione)
        {
            case Fazioni.NonMorti:
                imageFazione.sprite = fazioniClass.NonMorti;
                SetImgLogo(fazioniClass.NonMorti);
                break;
            case Fazioni.Orientali:
                imageFazione.sprite = fazioniClass.Orientali;
				SetImgLogo(fazioniClass.Orientali);
                break;
            case Fazioni.PiratiVeri:
                imageFazione.sprite = fazioniClass.PiratiVeri;
				SetImgLogo(fazioniClass.PiratiVeri);
                break;
            case Fazioni.Marina:
                imageFazione.sprite = fazioniClass.Marina;
				SetImgLogo(fazioniClass.Marina);
                break;
            case Fazioni.Voodoo:
                imageFazione.sprite = fazioniClass.Voodoo;
				SetImgLogo(fazioniClass.Voodoo);
                break;
            case Fazioni.Kraken:
                imageFazione.sprite = fazioniClass.Kraken;
				SetImgLogo(fazioniClass.Kraken);
                break;
            default:
                imageFazione.sprite = fazioniClass.PiratiVeri;
				SetImgLogo(fazioniClass.PiratiVeri);
                break;
        }
        if (Type == CardType.Pirata)
        {
            /* TextAndImageObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            statisticObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            imageCard.transform.rotation = Quaternion.Euler(0, 0, 0);
            nameText.color = Color.black;
            nameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            descriptionText.color = Color.black;
            descriptionObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            attackText.color = Color.black;
            attackObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            lifeText.color = Color.black;
            lifeObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            fazioneObject.transform.rotation = Quaternion.Euler(0, 0, 0);*/
            imageCover.sprite = Covers[0];
            switch (Data.Supporto)
            {
                case Buff.Attack:
                    attackText.color = supportcolor;
                    break;
                case Buff.Life:
                    lifeText.color = supportcolor;
                    break;
                default:
                    break;
            }
        }
        else if (Type == CardType.Nave)
        {
           /* TextAndImageObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            statisticObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            imageCard.transform.rotation = Quaternion.Euler(0, 0, 0);
            nameText.color = Color.white;
            nameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            descriptionText.color = Color.white;
            descriptionObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            attackText.color = Color.white;
            attackObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            lifeText.color = Color.white;
            lifeObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            fazioneObject.transform.rotation = Quaternion.Euler(0, 0, 0);*/
            imageCover.sprite = Covers[1]; 
        }
    }

    public void Evolve()
    {
        Debug.Log("What? " + Data.Name + " is evolving!");
        deck.RemoveCard(Data.Name);
        Data = Data.Evolution;
        deck.FillDeck(Data);
    }

    public void PlaySound()
    {
		if (soundCreature != null) 
		{ 
			soundCreature.clip = Data.SoundCreature;
			soundCreature.Play();
		}
    }

    void addEffect(int i)
    {
        if (scene.name == "Incontro")
        {
			CardEffect addedObject = null;
			Debug.Log(gameObject.name);	//c'è un errore probabilmente nella schermata di purificazione delle carte
			switch (Effects[i].Effect)
			{
				case Effect.Breed:
					addedObject = gameObject.AddComponent<Breed>();
					break;
				case Effect.AttackForEgg:
					addedObject = gameObject.AddComponent<AttackForEgg>();
					break;
				case Effect.LifeForEgg:
					addedObject = gameObject.AddComponent<LifeForEgg>();
					break;
				case Effect.Clumsy:
					addedObject = gameObject.AddComponent<Clumsy>();
					break;
				case Effect.TentaclesAttack:
					addedObject = gameObject.AddComponent<TentaclesAttack>();
					break;
				case Effect.TentaclesLife:
					addedObject = gameObject.AddComponent<TentaclesLife>();
					break;
				case Effect.Regeneration:
					addedObject = gameObject.AddComponent<Regeneration>();
					break;
				case Effect.ExtraSupportAttack:
					addedObject = gameObject.AddComponent<ExtraSupportAttack>();
					break;
				case Effect.ExtraSupportLife:
					addedObject = gameObject.AddComponent<ExtraSupportLife>();
					break;
				case Effect.ChangePurificationThresholdOnField:
					addedObject = gameObject.AddComponent<ChangePurificationThresholdOnField>();
					break;
				case Effect.ChangePurificationThresholdOnSupport:
					addedObject = gameObject.AddComponent<ChangePurificationThresholdOnSupport>();
					break;
				case Effect.LeftNeighbourAttackGeneric:
					addedObject = gameObject.AddComponent<LeftNeighbourAttackGeneric>();
					break;
				case Effect.RightNeighbourAttackGeneric:
					addedObject = gameObject.AddComponent<RightNeighbourAttackGeneric>();
					break;
				case Effect.LeftNeighbourLifeGeneric:
					addedObject = gameObject.AddComponent<LeftNeighbourLifeGeneric>();
					break;
				case Effect.RightNeighbourLifeGeneric:
					addedObject = gameObject.AddComponent<RightNeighbourLifeGeneric>();
					break;
				default:
					break;
			}

			if (addedObject != null)
				addedObject.Variable = variableEffects[i];
		}
	}

	public void Fight(Card _enemy)
    {
        InvokeOnAttack(_enemy);
		Debug.LogWarning("recive " + Attack + " damage, life remaining: " + _enemy.Life);
        _enemy.Life -= Attack;
    }

	public void SetImgLogo(Sprite logo) {
		if(imageFazioneInBasso != null)
			imageFazioneInBasso.sprite = logo;
	}

	public void ResetCardAttack() {
		attack = data.Attack;
	}

    #region Event

    void invokeOnDeck()
    {
        if (OnDeck != null)
            OnDeck();
    }

    void invokeOnDeath()
    {
        if (OnDeath != null)
            OnDeath();
    }

    void InvokeOnField()
    {
        if (OnField != null)
            OnField();
    }

	void InvokeOnSupport()
	{
		if (OnSupport != null)
			OnSupport();
	}

	void InvokeOnHand()
    {
        if (OnHand != null)
            OnHand();
    }

    void InvokeOnScarti()
    {
        if (OnScarti != null)
            OnScarti();
    }

	void InvokeOnDraw() {
		if (OnDraw != null)
			OnDraw();
	}

    public void InvokeOnAttack(Card _enemy)
    {
        if (OnAttack != null)
            OnAttack(_enemy);
    }

    private void OnDestroy()
    {
        Deck inDeck = transform.parent.parent.GetComponent<Deck>();
        Scarti scarti = transform.parent.parent.GetComponent<Scarti>();
        if (inDeck != null)
        {
            inDeck.CardInDeck--;
        }
        else if (scarti != null)
        {
            scarti.ScartedCard--;
            return;
        }
        else if (transform.parent.GetComponent<DropZone>())
        {
            if (transform.parent.GetComponent<DropZone>().Type == DropZoneType.Hand)
                deck.Draw(1);
        }
    }

	#endregion
}

public class CardEvent
{
    public delegate void CardEventDelegate();
    public delegate void CardEventDelCombat(Card _enemy);
}

public enum PositionCard
{
    OnDeck,
    OnHand,
    OnField,
	OnSupport,
    OnScarti,
}