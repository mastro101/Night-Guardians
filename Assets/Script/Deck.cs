using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    [SerializeField]
    CardsData[] Cards;
    [SerializeField]
    DropZone drawZone;
    [SerializeField]
    GameObject card;
    DeckType Type;

    public void Draw()
    {
        switch (Type)
        {
            case DeckType.PlayerDeck:
                Draw(5);
                break;
            case DeckType.EnemyDeck:
                Draw(1);
                break;
            default:
                break;
        }
        
    }

    public void Draw(int _for)
    {
        for (int i = 0; i < _for; i++)
        {
            if (drawZone.transform.childCount < drawZone.CardLimit)
            {
                Instantiate(card, drawZone.transform);
                card.GetComponent<Card>().Data = Cards[0];
                for (int n = 0; n < Cards.Length; n++)
                {
                    if (n != Cards.Length - 1)
                        Cards[n] = Cards[n + 1];
                    else
                        Cards[n] = null;
                }
            }
        }
    }
}

public enum DeckType
{
    PlayerDeck,
    EnemyDeck,
}
