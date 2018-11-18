using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Deck : MonoBehaviour {

    public CardsData[] Cards;
    public DropZone drawZone;
    [SerializeField]
    GameObject Card = null;
    GameObject card;
    [SerializeField]
    GameObject deckViewerObject;
    [SerializeField]
    GameObject scartiViewerObject;
    DeckViewer deckViewer;
    ScartiViewer scartiViewer;
    CardsOfDeck cardsOfDeck;
    public Transform playableCardInDeckTR;
    

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
        if (scartiViewer)
            scartiViewer = scartiViewerObject.GetComponent<ScartiViewer>();
        cardsOfDeck = FindObjectOfType<CardsOfDeck>();
    }

    private void Start()
    {
        FillDeck(cardsOfDeck.Cards);
        foreach(CardsData cardData in Cards)
        {
            if (cardData != null)
            {
                CardInDeck++;
            }
        }

        ShuffleArray(Cards);
        CreateCards();
        Draw();
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

    public void CreateCards()
    {
        foreach (CardsData cardData in Cards)
        {
            if (cardData != null)
            {
                card = Instantiate(Card, playableCardInDeckTR);
                card.GetComponent<Card>().Data = cardData;
                card.GetComponent<Card>().positionCard = PositionCard.OnDeck;
                card.transform.position = new Vector2(-200, -500);
            }
            else
                break;
        }
    }

    public void Draw(int _for)
    {
        if (drawZone != null)
        {
            if (playableCardInDeckTR.childCount > 0)
            {
                if (playableCardInDeckTR.GetChild(0) != null)
                {
                    for (int i = 0; i < _for; i++)
                    {
                        if (playableCardInDeckTR.childCount > 0)
                        {
                            if (playableCardInDeckTR.GetChild(0) != null)
                            {
                                playableCardInDeckTR.GetChild(0).SetParent(drawZone.transform);
                                CardInDeck--;
                                //for (int n = 0; n < Cards.Length; n++)
                                //{
                                //    // Per Spostare tutte le carte in cima 
                                //    if (Cards[n] != null)
                                //    {
                                //        if (n != Cards.Length - 1)
                                //            Cards[n] = Cards[n + 1];
                                //        else
                                //            Cards[n] = null;
                                //    }
                                //    else
                                //        break;
                                //}
                            }
                            else
                            {
                                InvockOnEmpty();
                                if (playableCardInDeckTR.GetChild(0) != null)
                                {
                                    _for -= i - 1;
                                    i = 0;
                                }
                            }
                        }
                        else
                        {
                            InvockOnEmpty();
                            if (playableCardInDeckTR.GetChild(0) != null)
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
            else
            {
                InvockOnEmpty();
                Draw(_for);
            }
        }
    }

    public void ViewCard()
    {
        if (!deckViewerObject.activeInHierarchy)
        {
            if (scartiViewer != null)
            {
                if (scartiViewerObject.activeInHierarchy)
                {
                    scartiViewerObject.SetActive(false);
                    scartiViewer.Close();
                }
            }
            if (CardInDeck != 0)
            {
                deckViewerObject.SetActive(true);
                deckViewer.ViewDeck();
            }
        }
        else
        {
            deckViewer.Close();
        }
    }

    public void SetCardInGameText()
    {
        CardInDeck = playableCardInDeckTR.childCount;
    }

    #region Event

    void InvockOnEmpty()
    {
        if (OnEmpty != null)
            OnEmpty(this);
    }

    #endregion

}

public class DeckEvent
{
    public delegate void DeckDelegate(Deck deck);
}