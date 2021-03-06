﻿using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System;
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
    public int Multiplier;
    public int Priority;
    public int Cooldown;

    public Sprite Sprite;

    public MoveType Type;
    public MoveStat Stat;
    public Target Target;
    public TargetStat TargetStat;

    public string GetFormattedDescription()
    {

        if (Description != "")
        {
            string d = Description;

            d = InsertValueIntoString(d, "DAMAGE", BaseValue);
            d = InsertValueIntoString(d, "HEALING", BaseValue);

            return d;
        }

        return "Description is empty";
    }

    private string InsertValueIntoString(string target, string delim, object value)
    {
        string[] s = target.Split(new string[] { delim }, StringSplitOptions.None);
        Debug.Log(s.Length);

        if (s.Length > 1)
            return s[0] + value + s[1];
      
        return target;
    }
}


