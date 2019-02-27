using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggEvent
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

	/*public delegate void EggDelegate();

	public static EggDelegate AddedEgg;

	public static void AddEgg(int _egg = 1)
	{
		AddedEgg();
		Debug.Log(EggInDeck + " Egg in deck");
	}*/
}
