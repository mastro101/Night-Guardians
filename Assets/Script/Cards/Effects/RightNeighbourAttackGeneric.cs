using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightNeighbourAttackGeneric : AbsNeighbourAttackForSomething
{
	protected override bool ApplyLeft()
	{
		return false;
	}

	protected override bool ApplyRight()
	{
		return true;
	}

	protected override int EffectMightValue()
	{
		return Variable;
	}
}
