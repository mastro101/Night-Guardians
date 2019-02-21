using UnityEngine;
using System.Collections;

public class Regeneration : CardEffect
{
	private int startingHp;

	private void Start()
	{
		combatManager.OnEndTurn += ApplyEffect;
		combatManager.OnStartTurn += SaveLife;
	}

	public override void ApplyEffect()
    {
        if(card.IsAlive)
        {
            card.Life = startingHp;
        }
    }

	public void SaveLife() {
		startingHp = card.Life;
		Debug.LogWarning(startingHp);
	}

}
