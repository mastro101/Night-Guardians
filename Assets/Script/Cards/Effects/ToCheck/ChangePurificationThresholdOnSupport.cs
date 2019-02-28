using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePurificationThresholdOnSupport : ChangePurificationThresholdOnField
{
	private void Start()
	{
		card.OnSupport += SubscribeEvent;
		card.OnField += UnsubscribeEvent;
		card.OnScarti += UnsubscribeEvent;
		card.OnDeath += UnsubscribeEvent;
		card.OnHand += UnsubscribeEvent;
	}
}
