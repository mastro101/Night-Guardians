using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraSupportAttack : AbsExtraSupportSomething
{
	protected override Buff GetBuffType()
	{
		return Buff.Attack;
	}
}
