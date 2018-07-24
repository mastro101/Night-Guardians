using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scarti : MonoBehaviour
{

    public CardsData[] Cards;
    public Text TextScarti;
    Deck deck;
    int scartedCard;
    int ScartedCard
    {
        get { return scartedCard; }
        set
        {
            scartedCard = value;
            TextScarti.text = "Scarti\n" + scartedCard;
        }
    }

    private void Awake()
    {
        deck = FindObjectOfType<Deck>();
        Cards = new CardsData[deck.Cards.Length];
    }

    private void Start()
    {
        deck.OnEmpty += refillDeck;
        TextScarti.text = "Scarti";
    }

    void refillDeck(Deck _deck)
    {
        if (Cards[0] != null)
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                _deck.Cards[i] = Cards[i];
            }
            ScartedCard = 0;
            _deck.SetCardInGameText();
        }
        else
            FindObjectOfType<EndCondiction>().EndGame(false);
    }

    public void ScartCard(Card _card)
    {
        for (int i = 0; i < Cards.Length; i++)
        {
            if (Cards[i] == null)
            {
                Cards[i] = _card.Data;
                ScartedCard++;
                break;
            }
        }
    }
}
