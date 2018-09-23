using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

    public CardsData[] Cards;
    public DropZone drawZone;
    [SerializeField]
    GameObject card = null;
    [SerializeField]
    GameObject deckViewerObject;
    DeckViewer deckViewer;
    

    public Text TextCardInDeck;
    int cardInDeck;
    public int CardInDeck
    {
        get { return cardInDeck; }
        set
        {
            cardInDeck = value;
            TextCardInDeck.text = "\n" + cardInDeck;
        }
    }

    public event DeckEvent.DeckDelegate OnEmpty;

    public void Draw()
    {
        Draw(5);        
    }

    private void Awake()
    {
        deckViewer = deckViewerObject.GetComponent<DeckViewer>();
    }

    private void Start()
    {
        foreach(CardsData cardData in Cards)
        {
            if (cardData != null)
                cardData.LifeChange = cardData.Life;
        }

        SetCardInGameText();
        ShuffleArray(Cards);
        Draw();
    }

    public void ShuffleArray<T>(T[] arr)
    {
        
        for (int i = CardInDeck - 1; i > 0; i--)
        {
            int r = Random.Range(0, i + 1);
            T tmp = arr[i];
            arr[i] = arr[r];
            arr[r] = tmp;
        }
    }

    public void Draw(int _for)
    {
        if (Cards[0] != null)
        {
            for (int i = 0; i < _for; i++)
            {
                if (Cards[0] != null)
                {
                    Instantiate(card, drawZone.transform).GetComponent<Card>().Data = Cards[0];
                    CardInDeck--;
                    for (int n = 0; n < Cards.Length; n++)
                    {
                        // Per Spostare tutte le carte in cima 
                        if (Cards[n] != null)
                        {
                            if (n != Cards.Length - 1)
                                Cards[n] = Cards[n + 1];
                            else
                                Cards[n] = null;
                        }
                        else
                            break;
                    }
                }
                else
                {
                    InvockOnEmpty();
                    if (Cards[0] != null)
                    {
                        _for -= i - 1;
                        i = 0;
                    }
                }
            }
        }
        else
        {
            InvockOnEmpty();
            Draw(_for);
        }
    }

    public void ViewCard()
    {
        if (!deckViewerObject.activeInHierarchy)
        {
            deckViewerObject.SetActive(true);
            deckViewer.ViewDeck();
        }
        else
        {
            deckViewer.Close();
        }
    }

    public void SetCardInGameText()
    {
        foreach (CardsData cards in Cards)
        {
            if (cards != null)
                CardInDeck++;
        }
    }

    #region Event

    void InvockOnEmpty()
    {
        if (OnEmpty != null)
            OnEmpty(this);
    }

    #endregion

    private void OnMouseDown()
    {
        deckViewerObject.SetActive(true);
        deckViewer.ViewDeck();
    }
}

public class DeckEvent
{
    public delegate void DeckDelegate(Deck deck);
}