using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scarti : MonoBehaviour
{

    public CardsData[] Cards;
    public Text TextScarti;
    Deck deck;
    [SerializeField]
    GameObject card = null;
    [SerializeField]
    GameObject scartiViewerObject;
    ScartiViewer scartiViewer;

    int scartedCard;
    public int ScartedCard
    {
        get { return scartedCard; }
        set
        {
            scartedCard = value;
            TextScarti.text = "\n" + scartedCard;
        }
    }

    private void Awake()
    {
        deck = FindObjectOfType<Deck>();
        Cards = new CardsData[deck.Cards.Length];
        scartiViewer = scartiViewerObject.GetComponent<ScartiViewer>();
    }

    private void Start()
    {
        deck.OnEmpty += refillDeck;
        TextScarti.text = "";
    }

    void refillDeck(Deck _deck)
    {
        if (Cards[0] != null)
        {
            for (int i = 0; i < Cards.Length; i++)
            {
                _deck.Cards[i] = Cards[i];
                Cards[i] = null;
            }
            ScartedCard = 0;
            _deck.SetCardInGameText();
        }
        else
            if (_deck.drawZone)
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

    public void ViewCard()
    {
        if (!scartiViewerObject.activeInHierarchy)
        {
            scartiViewerObject.SetActive(true);
            scartiViewer.ViewScarti();
        }
        else
        {
            scartiViewer.Close();
        }
    }
}
