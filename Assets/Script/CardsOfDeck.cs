using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsOfDeck : MonoBehaviour {

	public static CardsOfDeck cardsOfDeckPointer;

    public CardsData[] Cards;

	public void Awake()
	{
		if(cardsOfDeckPointer == null) {
			cardsOfDeckPointer = this;
		}
	}

	public void FillDeck(CardsData _card)
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i] == null)
            {
                Cards[i] = _card;
                break;
            }
            else if (i == Cards.Length)
                Debug.Log("Deck Pieno");
        }
    }

    public void FillDeck(CardsData[] _cards)
    {
        int n = 0;
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i] == null)
            {
                Cards[i] = _cards[n];
                if (_cards[n + 1] == null)
                    break;
                n++;
            }                
        }
    }

	#region Check Some Deck Property

	public int CheckDeckActSize() {
		int count = 0;
		for(int i = 0; i < Cards.Length; i++) {
			if(Cards[i] != null) {
				count++;
			}
		}
		return count;
	}

	public int CheckCardNumber(CardsData cardToCheck) 
	{
		int count = 0;
		foreach(CardsData card in Cards) {
			if(card != null && card == cardToCheck) {
				count++;
			}
		}

		return count;
	}
	/// <summary>
	/// controlla in base al nome del File
	/// </summary>
	/// <param name="cardToCheck">nome del file della carta da controllare</param>
	/// <returns></returns>
	public int CheckCardNumber(string cardToCheck)
	{
		int count = 0;
		for (int i = 0; i < Cards.Length; i++)
		{
			if (Cards[i] != null && Cards[i].name == cardToCheck)
			{
				count++;
			}
		}

		return count;
	}

	public bool CheckCardPresence(CardsData cardToCheck) {
		return CheckCardNumber(cardToCheck) > 0;
	}
	/// <summary>
	/// controlla in base al nome del File
	/// </summary>
	/// <param name="cardToCheck">nome del file della carta da controllare</param>
	/// <returns></returns>
	public bool CheckCardPresence(string cardToCheck)
	{
		return CheckCardNumber(cardToCheck) > 0;
	}


	public int CheckCardsNumber(CardsData[] cardsToCheck) {
		int count = 0;
		foreach(CardsData cardToCheck in cardsToCheck) {
			count += CheckCardNumber(cardToCheck);
		}
		return count;
	}
	/// <summary>
	/// controlla in base ai nomi dei File
	/// </summary>
	/// <param name="cardsToCheck">nomi del file delle carte da controllare</param>
	/// <returns></returns>
	public int CheckCardsNumber(string[] cardsToCheck)
	{
		int count = 0;
		for(int i = 0; i < cardsToCheck.Length; i++)
		{
			count += CheckCardNumber(cardsToCheck[i]);
		}
		return count;
	}

	#endregion
}
