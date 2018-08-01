using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class Combo : ScriptableObject {

    public string Name;
    public string Description;
    public int BaseValue;
    public int ScaleValue
    {
        get
        {
            if (StatValue == null)
            {
                throw new Exception("Missing Player Stat reference! Combo asset: " + Name);
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

    public IntVariable StatValue;

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
