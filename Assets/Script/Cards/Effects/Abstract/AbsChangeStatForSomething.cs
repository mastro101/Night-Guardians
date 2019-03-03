using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsChangeStatForSomething : CardEffect {
	private void Start()
	{
		card.OnField += ApplyEffect;
		card.OnHand += ApplyEffect;
	}

	public override void ApplyEffect()
	{
		base.ApplyEffect();
		ChangeStat();
	}

	/// <summary>
	/// da reimplementare nei figli per cambiare il valore di aggiunta alla vita
	/// </summary>
	/// <returns></returns>
	protected abstract int EffectMightValue();

	protected abstract void ChangeStat();
}

public abstract class AbsLifeForSomething : AbsChangeStatForSomething
{
	protected override void ChangeStat()
	{
		card.Life = card.Data.Life + (Variable * EffectMightValue());
	}
}

public abstract class AbsAttackForSomething : AbsChangeStatForSomething
{
	protected override void ChangeStat()
	{
		card.Attack = card.Data.Attack + (Variable * EffectMightValue());
	}
}



public abstract class AbsNeighbourChangeStatForSomething : AbsChangeStatForSomething {
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

	protected abstract bool ApplyLeft();
	protected abstract bool ApplyRight();
	/// <summary>
	/// da overridare per aumentare la distanza del potenziamento, con 1 potenzia le carte adiacenti, con 2 le carte a distanza 2
	/// sotto l'1 l'effetto non si attiva mai
	/// </summary>
	/// <returns></returns>
	protected virtual int Distance() {
		return 1;
	}
}

public abstract class AbsNeighbourLifeForSomething : AbsNeighbourChangeStatForSomething
{
	protected override void ChangeStat()
	{
		int index = combatManager.GetCardPosition(card);
		if(index >= 0) 
		{
			for (int i = 1; i <= Distance(); i++)
			{
				if (ApplyLeft())
				{
					combatManager.ChangeCardLife(index - i, EffectMightValue());
				}

				if (ApplyRight())
				{
					combatManager.ChangeCardLife(index + i, EffectMightValue());
				}
			}
		}
	}
}

public abstract class AbsNeighbourAttackForSomething : AbsNeighbourChangeStatForSomething
{
	protected override void ChangeStat()
	{
		int index = combatManager.GetCardPosition(card);
		if (index >= 0)
		{
			for (int i = 1; i <= Distance(); i++)
			{
				if (ApplyLeft())
				{
					combatManager.ChangeCardAttack(index - i, EffectMightValue());
				}

				if (ApplyRight())
				{
					combatManager.ChangeCardAttack(index + i, EffectMightValue());
				}
			}
		}
	}
}