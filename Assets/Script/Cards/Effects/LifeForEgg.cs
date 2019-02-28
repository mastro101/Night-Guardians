using UnityEngine;
using System.Collections;

//checked
public class LifeForEgg : AbsLifeForSomething
{
	protected override int EffectMightValue()
	{
		return EggUtility.EggInDeck;
	}
}
