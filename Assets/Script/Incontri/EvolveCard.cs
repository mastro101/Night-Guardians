﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class EvolveCard : MonoBehaviour , IPointerDownHandler
{
    [HideInInspector]
    public Card OriginalCard;
    Transform parentGO;
    EndCondiction endCondiction;

    private void Awake()
    {
        endCondiction = FindObjectOfType<EndCondiction>();
    }

    private void Start()
    {
        parentGO = transform.parent;
    }

    void evolveCard()
    {
        OriginalCard.Evolve();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        evolveCard();
        for (int i = 0; i < parentGO.childCount; i++)
        {
            Destroy(parentGO.GetChild(i).gameObject);
        }

        if (endCondiction.InEnd)
            endCondiction.EndGame(true);

        parentGO.parent.gameObject.SetActive(false);
    }
}
