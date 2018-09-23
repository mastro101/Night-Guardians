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
            card = Instantiate(cardObject, deckViewerObject.transform);
            card.GetComponent<Card>().Data = deck.Cards[i];
            card.transform.SetSiblingIndex(Random.Range(0, i));
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
