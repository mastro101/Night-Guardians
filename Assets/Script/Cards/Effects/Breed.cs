using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breed : CardEffect {

    CardsData egg;
    GameObject newCard;

    private void Start()
    {
        ContenitoreCards cards = FindObjectOfType<ContenitoreCards>();
        egg = cards.FindCard("KrakenTokenEgg");

        card.OnField += SubscribeEvent;
        card.OnScarti += UnsubscribeEvent;
        card.OnDeath += UnsubscribeEvent;

    }

    public override void SubscribeEvent()
    {
        base.SubscribeEvent();
        combatManager.OnEndFight += ApplyEffect;
    }

    public override void UnsubscribeEvent()
    {
        base.UnsubscribeEvent();
        combatManager.OnEndFight -= ApplyEffect;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        newCard = Instantiate(card.gameObject);
        newCard.GetComponent<Card>().Data = egg;
        deck.FillDeck(egg);
        scarti.ScartCard(newCard.GetComponent<Card>()); 
        EggEvent.AddEgg();
    }
}

public class EggEvent
{
    public static int EggInDeck;

    public delegate void EggDelegate();

    public static EggDelegate AddedEgg;

    public static void AddEgg(int _egg = 1)
    {
        EggInDeck += _egg;
        AddedEgg();
        Debug.Log(EggInDeck + " Egg in deck");
    }
}
