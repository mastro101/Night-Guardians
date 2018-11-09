using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsOfDeck : MonoBehaviour {

    public CardsData[] Cards;

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
                if (_cards[n] == null)
                    break;
                n++;
            }                
        }
    }
}
