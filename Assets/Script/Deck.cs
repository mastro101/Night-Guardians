using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour {

    [SerializeField]
    CardsData[] Cards;
    [SerializeField]
    DropZone Hand;
    [SerializeField]
    GameObject card;

    public void Draw()
    {
        Instantiate(card, Hand.transform);
        card.GetComponent<Card>().Data = Cards[Random.Range(0, Cards.Length + 1)];
    }
}
