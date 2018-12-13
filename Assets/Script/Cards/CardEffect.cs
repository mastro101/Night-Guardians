using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    protected Card card;
    protected Deck deck;
    protected Scarti scarti;
    protected CombatManager combatManager;
    public int Variable;

    protected virtual void Awake()
    {
        card = GetComponent<Card>();
        deck = FindObjectOfType<Deck>();
        scarti = FindObjectOfType<Scarti>();
        combatManager = FindObjectOfType<CombatManager>();
    }

    public virtual void ApplyEffect()
    {

    }
    public virtual void ApplyEffect(Card _card)
    {

    }

    public virtual void SubscribeEvent()
    {
        Debug.Log("Iscritto");
    }

    public virtual void UnsubscribeEvent()
    { }
}