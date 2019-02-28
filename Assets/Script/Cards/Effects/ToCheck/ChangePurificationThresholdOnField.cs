using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePurificationThresholdOnField : CardEffect
{

	private void Start()
	{
		card.OnField += SubscribeEvent;
		card.OnSupport += UnsubscribeEvent;
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

	public override void ApplyEffect()
	{
		base.ApplyEffect();
		combatManager.purificationThreshold += Variable;
	}
}
