using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraSupportLife : AbsExtraSupportSomething
{
	protected override Buff GetBuffType()
	{
		return Buff.Life;
	}
}
