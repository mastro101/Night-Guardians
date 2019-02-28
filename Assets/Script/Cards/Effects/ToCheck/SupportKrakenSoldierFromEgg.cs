using UnityEngine;
using System.Collections;

public class SupportKrakenSoldierFromEgg : CardEffect
{
    GameObject newCard;
    CardsData soldier;

    private void Start()
    {
        card.OnField += SubscribeEvent;
        card.OnScarti += UnsubscribeEvent;
        card.OnDeath += UnsubscribeEvent;
        card.OnHand += UnsubscribeEvent;
    }

    public override void SubscribeEvent()
    {
        base.SubscribeEvent();
        combatManager.OnStartFight += ApplyEffect;
    }

    public override void UnsubscribeEvent()
    {
        base.UnsubscribeEvent();
        combatManager.OnStartFight -= ApplyEffect;
    }

    // NON FINITO

   //public override void ApplyEffect()
   //{
   //    base.ApplyEffect();
   //    if (combatManager.Support == gameObject.GetComponent<Card>() && combatManager.NumberOfCardInField < 3)
   //    {
   //        newCard = Instantiate(card.gameObject);
   //        newCard.GetComponent<Card>().Data = soldier;
   //        deck.FillDeck(soldier);
   //
   //        newCard.transform.SetParent(combatManager.zoneField.transform);
   //        newCard.GetComponent<Card>().positionCard = PositionCard.OnField;
   //    }
   //    
   //}
}
