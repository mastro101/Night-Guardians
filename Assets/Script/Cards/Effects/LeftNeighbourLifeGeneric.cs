using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNeighbourLifeGeneric : AbsNeighbourLifeForSomething
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