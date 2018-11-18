using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardEffect : MonoBehaviour
{
    protected Card card;
    protected CombatManager combatManager;

    protected virtual void Awake()
    {
        card = GetComponent<Card>();
        combatManager = FindObjectOfType<CombatManager>();
    }

    public virtual void ApplyEffect()
    { }
    public virtual void ApplyEffect(int value)
    { }
}