﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
                lifeText.color = Color.red;
            if (Type == CardType.Pirata)
            {
                if (life <= 0)
                {
                    isAlive = false;
                }
            }
            else
            {
                if (life < 0)
                    isAlive = false;
            }
        }
    }

    public int Grado;
    public Fazioni Fazione;

    FazioniClass fazioniClass;

    [SerializeField]
    GameObject TextAndImageObject = null, nameObject = null, statisticObject = null, attackObject = null, lifeObject = null, descriptionObject = null, fazioneObject = null;
    [SerializeField]
    Sprite[] Covers = null;
    [SerializeField]
    Image imageCard = null, imageCover = null, imageFazione = null;
    [SerializeField]
    TextMeshProUGUI nameText = null, attackText = null, lifeText = null, descriptionText = null;

    Deck deck;

    private void Awake()
    {
        soundCreature = FindObjectOfType<AudioSource>();
        combatManager = FindObjectOfType<CombatManager>();
        fazioniClass = FindObjectOfType<FazioniClass>();
        deck = FindObjectOfType<Deck>();
    }

    private void Start()
    {
        if (transform.parent.GetComponent<DropZone>())
            Zone = transform.parent.GetComponent<DropZone>().Type;
    }

    void insertData()
    {
        Type = Data.Type;
        Grado = Data.Grado;
        Attack = Data.Attack;
        Life = Data.Life;
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
                break;
            case Fazioni.Orientali:
                imageFazione.sprite = fazioniClass.Orientali;
                break;
            case Fazioni.PiratiVeri:
                imageFazione.sprite = fazioniClass.PiratiVeri;
                break;
            case Fazioni.Marina:
                imageFazione.sprite = fazioniClass.Marina;
                break;
            case Fazioni.Voodoo:
                imageFazione.sprite = fazioniClass.Voodoo;
                break;
            case Fazioni.Kraken:
                imageFazione.sprite = fazioniClass.Kraken;
                break;
            default:
                imageFazione.sprite = fazioniClass.PiratiVeri;
                break;
        }
        if (Type == CardType.Pirata)
        {
            TextAndImageObject.transform.rotation = Quaternion.Euler(0, 0, 0);
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
            fazioneObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            imageCover.sprite = Covers[0];
            switch (Data.Supporto)
            {
                case Buff.Attack:
                    attackText.color = Color.blue;
                    break;
                case Buff.Life:
                    lifeText.color = Color.blue;
                    break;
                default:
                    break;
            }
        }
        else if (Type == CardType.Nave)
        {
            TextAndImageObject.transform.rotation = Quaternion.Euler(0, 0, 180);
            statisticObject.transform.rotation = Quaternion.Euler(0, 180, 180);
            imageCard.transform.rotation = Quaternion.Euler(0, 0, 0);
            nameText.color = Color.white;
            nameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            descriptionText.color = Color.white;
            descriptionObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            attackText.color = Color.white;
            attackObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            lifeText.color = Color.white;
            lifeObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            fazioneObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            imageCover.sprite = Covers[1];
        }
    }

    public void Evolve()
    {
        Debug.Log("What? " + Data.Name + " is evolving!");
        Data = Data.Evolution;
    }

    public void PlaySound()
    {
        soundCreature.clip = Data.SoundCreature;
        soundCreature.Play();
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
}
