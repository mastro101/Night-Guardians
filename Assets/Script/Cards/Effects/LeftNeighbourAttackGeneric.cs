using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNeighbourAttackGeneric : AbsNeighbourAttackForSomething
{
	protected override bool ApplyLeft()
	{
		return true;
	}

	protected override bool ApplyRight()
	{
		return false;
	}

	protected override int EffectMightValue()
	{
		return Variable;
	}
}
