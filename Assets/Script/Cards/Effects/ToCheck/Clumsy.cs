using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clumsy : CardEffect {

    private void Start()
    {
        card.OnDeath += ResetAttack;
        card.OnScarti += ResetAttack;
        card.OnHand += ResetAttack;
        card.OnField += saveOldAttack;
        card.OnAttack += ApplyEffect;
        
    }

    int turn = 1 ;
    int oldAttack;

    void saveOldAttack()
    {
        oldAttack = card.Attack;
    }

    void ResetAttack()
    {
        turn = 1;
        card.Attack = oldAttack;
    }

    public override void ApplyEffect(Card _card)
    {
        base.ApplyEffect(_card);
        if (turn % 2 == 0)
        {
            card.Attack = 0;
            Debug.Log(card.name + " è troppo stanco per attaccare");
        }
        else
            card.Attack = oldAttack;
        turn++;
    }
}
