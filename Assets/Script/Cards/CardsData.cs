using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 0)]
public class CardsData : ScriptableObject {

    public CardType Type;
    public Buff Supporto;
    public string Name;
    public int Attack;
    public int Life;
    public int PurificationOrDarkness;
    public string Description;
    public Sprite SpriteImage;
    public AudioClip SoundCreature;
}

public enum CardType
{
    Guardian,
    Nightmare,
}

public enum Buff
{
    Attack,
    Life,
    Purification,
}