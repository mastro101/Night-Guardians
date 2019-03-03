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
	Breed = 0,
    AttackForEgg = 1,
    LifeForEgg = 2,
    Clumsy = 3,
    TentaclesAttack = 4,
    TentaclesLife = 5,
	Regeneration = 6,
	ExtraSupportAttack = 7,
	ExtraSupportLife = 8,
	ChangePurificationThresholdOnField = 9,
	ChangePurificationThresholdOnSupport = 10,
	LeftNeighbourAttackGeneric = 11,
	RightNeighbourAttackGeneric = 12,
	LeftNeighbourLifeGeneric = 13,
	RightNeighbourLifeGeneric = 14
}

[Serializable]
public class EffectVariable
{
    public Effect Effect;
    public int Variable;
}