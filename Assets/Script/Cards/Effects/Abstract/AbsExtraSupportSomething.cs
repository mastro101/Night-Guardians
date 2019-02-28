using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsExtraSupportSomething : CardEffect {

	private void Start()
	{
		card.OnSupport += SubscribeEvent;
		card.OnField += UnsubscribeEvent;
		card.OnScarti += UnsubscribeEvent;
		card.OnDeath += UnsubscribeEvent;
		card.OnHand += UnsubscribeEvent;
	}

	public override void SubscribeEvent()
	{
		base.SubscribeEvent();
		combatManager.OnSupport += ApplyEffect;
	}

	public override void UnsubscribeEvent()
	{
		base.UnsubscribeEvent();
		combatManager.OnSupport -= ApplyEffect;
	}

	public override void ApplyEffect()
	{
		if (card.positionCard == PositionCard.OnSupport)
		{
			base.ApplyEffect();
			combatManager.CalculateSupport(GetBuffType(), Variable);
		}
	}

	protected abstract Buff GetBuffType();

}
