using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggUtility
{
	private static string EggFileCardName = "KrakenTokenEgg";
	
	public static int EggInDeck
	{
		get
		{
			return CardsOfDeck.cardsOfDeckPointer.CheckCardNumber(EggFileCardName);
		}
		private set { }
	}
}
