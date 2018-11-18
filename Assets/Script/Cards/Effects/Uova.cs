using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uova : CardEffect {

    private void Start()
    {
        card.OnDeath += ApplyEffect;
    }

    public override void ApplyEffect()
    {
        base.ApplyEffect();
        Debug.Log("Creato un uovo");
    }
}
