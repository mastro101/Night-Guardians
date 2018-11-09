﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ChoosePurification : MonoBehaviour, IPointerDownHandler
{
    [HideInInspector]
    public Card OriginalCard;
    Transform parentGO;
    CloseWindow closeWindow;
    EndCondiction endCondiction;

    private void Awake()
    {
        parentGO = transform.parent;
        closeWindow = transform.parent.parent.GetChild(1).GetComponent<CloseWindow>();
        endCondiction = FindObjectOfType<EndCondiction>();
    }

    private void Start()
    {
        closeWindow.OnClose += destroyOriginalCard;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OriginalCard.Data = GetComponent<Card>().Data;
        for (int i = 0; i < parentGO.childCount; i++)
        {
            Destroy(parentGO.GetChild(i).gameObject);
        }

        if (endCondiction.InEnd)
            endCondiction.EndGame(true);

        parentGO.parent.gameObject.SetActive(false);
    }

    void destroyOriginalCard()
    {
        if (OriginalCard != null)
            Destroy(OriginalCard.gameObject);
    }
}
