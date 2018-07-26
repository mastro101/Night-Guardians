﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

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
                Data.LifeChange = Data.Life;
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
                Data = Data.DataPurificatedCard;
                combatManager.InCombat = false;
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
            if (life <= 0)
            {
                isAlive = false;
            }
        }
    }

    int purificationOrDarkness;
    public int PurificationOrDarkness
    {
        get { return purificationOrDarkness; }
        set
        {
            purificationOrDarkness = value;
            purificationOrDarknessText.text = purificationOrDarkness.ToString();
            if (purificationOrDarkness == 0 && Data.Type == CardType.Nightmare)
                IsPurificato = true;
        }
    }

    [SerializeField]
    GameObject TextAndImageObject = null, nameObject = null, statisticObject = null, attackObject = null, lifeObject = null, purificationOrDarknessObject = null, descriptionObject = null;
    [SerializeField]
    Sprite[] Covers = null;
    [SerializeField]
    Image imageCard = null, imageCover = null;
    [SerializeField]
    TextMeshProUGUI nameText = null, attackText = null, lifeText = null, purificationOrDarknessText = null, descriptionText = null;

    private void Awake()
    {
        soundCreature = FindObjectOfType<AudioSource>();
        combatManager = FindObjectOfType<CombatManager>();
    }

    private void Start()
    {
        Zone = transform.parent.GetComponent<DropZone>().Type;
        if (Type == CardType.Guardian)
            Life = Data.LifeChange;
    }

    void insertData()
    {
        Type = Data.Type;
        Attack = Data.Attack;
        Life = Data.Life;
        PurificationOrDarkness = Data.PurificationOrDarkness;
        nameText.text = Data.Name;
        imageCard.sprite = Data.SpriteImage;
        attackText.text = Data.Attack.ToString();
        lifeText.text = Data.Life.ToString();
        purificationOrDarknessText.text = Data.PurificationOrDarkness.ToString();
        descriptionText.text = Data.Description;
        if (Type == CardType.Guardian)
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
            purificationOrDarknessText.color = Color.black;
            purificationOrDarknessObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            imageCover.sprite = Covers[0];
            switch (Data.Supporto)
            {
                case Buff.Attack:
                    attackText.color = Color.blue;
                    break;
                case Buff.Life:
                    lifeText.color = Color.blue;
                    break;
                case Buff.Purification:
                    purificationOrDarknessText.color = Color.blue;
                    break;
                default:
                    break;
            }
        }
        else if (Type == CardType.Nightmare)
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
            purificationOrDarknessText.color = Color.white;
            purificationOrDarknessObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            imageCover.sprite = Covers[1];
        }
    }

    public void PlaySound()
    {
        soundCreature.clip = Data.SoundCreature;
        soundCreature.Play();
    }
}
