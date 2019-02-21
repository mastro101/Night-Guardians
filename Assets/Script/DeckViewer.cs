using UnityEngine;
using System.Collections;

public class DeckViewer : MonoBehaviour
{
    [SerializeField]
    GameObject deckViewerObject;
    Deck deck;
    [SerializeField]
    GameObject cardObject;
    GameObject card;

    private void Awake()
    {
        deck = FindObjectOfType<Deck>();
    }

    public void ViewDeck()
    {
        for (int i = 0; i < deck.CardInDeck; i++)
        {
            card = Instantiate(deck.playableCardInDeckTR.GetChild(i).gameObject, deckViewerObject.transform);
            card.transform.SetSiblingIndex(Random.Range(0, i + 1));
            Destroy(card.GetComponent<Draggable>());
        }
    }

    public void Close()
    {
        for (int i = 0; i < deck.CardInDeck; i++)
        {
            Destroy(deckViewerObject.transform.GetChild(i).gameObject);
        }
        deckViewerObject.SetActive(false);
    }

}
