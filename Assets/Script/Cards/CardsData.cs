using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 0)]
public class CardsData : ScriptableObject {

    public CardType Type;
    public Buff Supporto;
    public Fazioni Fazione;
    public int Grado;
    public string Name;
    public int Attack;
    public int Life;
    public EffectVariable[] Effects;
    public string Description;
    public Sprite SpriteImage;
	public Sprite SpriteImageOnDrag;
	public AudioClip SoundCreature;
    public CardsData Evolution;
}

public enum CardType
{
    Pirata,
    Nave,
}

public enum Buff
{
    Attack,
    Life,
}

public enum Fazioni
{
    NonMorti,
    Orientali,
    PiratiVeri,
    Marina,
    Voodoo,
    Kraken,
    Starter,
}

public enum Effect
{
    Breed,
    AttackForEgg,
    LifeForEgg,
    Clumsy,
    TentaclesAttack,
    TentaclesLife,
}

[Serializable]
public class EffectVariable
{
    public Effect Effect;
    public int Variable;
}