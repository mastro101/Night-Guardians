using UnityEngine;
using System.Collections;

//checked
public class AttackForEgg : AbsAttackForSomething
{
	protected override int EffectMightValue()
	{
		return EggUtility.EggInDeck;
	}
}
