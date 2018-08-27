using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EnemyMove : ScriptableObject {

    public string Name;
    public string Description;
    public int BaseValue;
    public float Multiplier;
    public int Priority;
    public int Cooldown;

    public MoveType Type;
    public MoveStat Stat;
    public Target Target;
    public TargetStat TargetStat;
}
