using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//checked
public class Breed : CardEffect {

    CardsData egg;
    GameObject newCard;

    private void Start()
    {
        ContenitoreCards cards = FindObjectOfType<ContenitoreCards>();
        egg = cards.FindCard("KrakenTokenEgg");
        card.OnField += SubscribeEvent;
		card.OnSupport += UnsubscribeEvent;
		card.OnScarti += UnsubscribeEvent;
        card.OnDeath += UnsubscribeEvent;
        card.OnHand += UnsubscribeEvent;

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
        for (int i = 0; i < Variable; i++)
        {
            newCard = Instantiate(card.gameObject);
            //egg.Grado = card.Grado;
            newCard.GetComponent<Card>().Data = egg;
            deck.FillDeck(egg);
            scarti.ScartCard(newCard.GetComponent<Card>());
        }
    }
}
