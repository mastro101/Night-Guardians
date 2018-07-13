using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 0)]
public class CardsData : ScriptableObject {

    public CardType Type;
    public string Name;
    public int Attack;
    public int Life;
    public int PurificationOrDarkness;
    public string Description;
    public Sprite SpriteImage;
}

public enum CardType
{
    Guardian,
    Nightmare,
}
