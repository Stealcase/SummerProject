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
    None,
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
    public int ScaleValue
    {
        get
        {
            if (StatValue == null)
            {
                throw new Exception("Missing Player Stat reference! Move asset: " + Name);
            }
            return (int)Math.Round(StatValue.Value * Multiplier);
        }
    }
    public int TotalValue
    {
        get
        {
            return BaseValue + ScaleValue;
        }
    }
    public float Multiplier;
    public int Priority;
    public int Cooldown;

    public IntVariable StatValue;

    [SerializeField]
    private Sprite sprite;

    public Sprite Sprite
    {
        get
        {
            if (sprite == null)
                Debug.Log("Missing Sprite reference! Move asset: " + Name);
            return sprite;
        }
    }

    public MoveType Type;
    public MoveStat Stat;
    public Target Target;
    public TargetStat TargetStat;

    public string GetFormattedDescription()
    {
        if (Description != "")
        {
            string d = Description;

            d = InsertValueIntoString(d, "BASEVAL", BaseValue);

            if (ScaleValue != 0)
            {
                string scaleString = "(+" + ScaleValue + ")";
                d = InsertValueIntoString(d, "SCALESTRING", scaleString);
            }
            else
                d = InsertValueIntoString(d, " SCALESTRING", "");

            d = InsertValueIntoString(d, "SCALEVAL", ScaleValue);
            d = InsertValueIntoString(d, "TOTALVAL", TotalValue);

            return d;
        }

        return "Description is empty";
    }

    private string InsertValueIntoString(string target, string delim, object value)
    {
        string[] s = target.Split(new string[] { delim }, StringSplitOptions.None);

        if (s.Length > 1)
            return s[0] + value + s[1];
      
        return target;
    }
}


