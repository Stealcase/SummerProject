using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStrike : IMove {

    private int baseValue;
    private int priority;
    private int cooldown;

    private Dictionary<MoveID, ComboID> Combos;

    public JumpStrike()
    {
        baseValue = 3;
    }

    //No multiplier implemented yet
    public int ScaledValue()
    {
        return baseValue + (Player.Instance.Weapon);
    }

    //Get methods for move attributes
    public int BaseValue()
    {
        return baseValue;
    }

    public int Priority()
    {
        return priority;
    }

    public int Cooldown()
    {
        return cooldown;
    }

    public Dictionary<MoveID, ComboID> GetCombo()
    {
        return null;
    }


    public MoveStat Type()
    {
        return global::MoveStat.Weapon;
    }

    public string Name()
    {
        return "JumpStrike";
    }
    
}
