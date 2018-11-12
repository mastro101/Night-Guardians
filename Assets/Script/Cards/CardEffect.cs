using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    protected Card card;

    protected virtual void Awake()
    {
        card = GetComponent<Card>();
    }

    public virtual void ApplyEffect()
    { }
    public virtual void ApplyEffect(int value)
    { }
}

