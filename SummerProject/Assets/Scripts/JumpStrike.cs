using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpStrike : IMove {

    private int baseValue;
    private int priority;
    private int cooldown;

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


    public MoveType Type()
    {
        return global::MoveType.Weapon;
    }

    public string Name()
    {
        return "JumpStrike";
    }
    
}
