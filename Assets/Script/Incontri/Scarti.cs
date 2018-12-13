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
    [SerializeField]
    GameObject deckViewerObject;
    ScartiViewer scartiViewer;
    DeckViewer deckViewer;
    public Transform playableCardInScartiTR;

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
        deckViewer = deckViewerObject.GetComponent<DeckViewer>();
    }

    private void Start()
    {
        deck.OnEmpty += refillDeck;
        TextScarti.text = "";
    }

    void refillDeck(Deck _deck)
    {
        if (playableCardInScartiTR.childCount > 0)
        {
            if (playableCardInScartiTR.GetChild(0) != null)
            {
                int n = playableCardInScartiTR.childCount;
                for (int i = 0; i < n; i++)
                {
                    playableCardInScartiTR.GetChild(0).SetParent(_deck.playableCardInDeckTR);
                    _deck.playableCardInDeckTR.GetChild(0).SetSiblingIndex(Random.Range(0, i + 1));
                }
                ScartedCard = 0;
                _deck.SetCardInGameText();
            }
            else
            {
                if (_deck.drawZone)
                    FindObjectOfType<EndCondiction>().EndGame(false);
            }
        }
    }

    public void ScartCard(Card _card)
    {
        _card.transform.SetParent(playableCardInScartiTR);
        _card.transform.position = new Vector2(1500, -500);
        _card.positionCard = PositionCard.OnScarti;
        ScartedCard++;
    }

    public void ViewCard()
    {
        if (!scartiViewerObject.activeInHierarchy)
        {
            if (deckViewerObject.activeInHierarchy)
            {
                deckViewerObject.SetActive(false);
                deckViewer.Close();
            }
            if (ScartedCard != 0)
            {
                scartiViewerObject.SetActive(true);
                scartiViewer.ViewScarti();
            }
        }
        else
        {
            scartiViewer.Close();
        }
    }
}
