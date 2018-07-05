using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Attack,
    Modifier
}

public enum MoveStat
{
    Will,
    Physical,
    Weapon
}

public enum Target
{
    Player,
    Enemy
}

public enum TargetStat
{
    HP,
    Courage,
    Shield,
    Charge
}

[CreateAssetMenu]
public class Move : ScriptableObject {

    public string Name;
    public string Description;
    public int BaseValue;
    public int ScaledValue;
    public int Priority;
    public int Cooldown;

    public Sprite Sprite;

    public MoveType Type;
    public MoveStat Stat;
    public Target Target;
    public TargetStat TargetStat;

}
