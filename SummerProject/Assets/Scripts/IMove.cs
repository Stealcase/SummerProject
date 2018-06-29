using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveType
{
    Will,
    Physical,
    Weapon
}

public interface IMove {

    int BaseValue();
    int ScaledValue();
    int Priority();
    int Cooldown();
    
    MoveType Type();
    string Name();

}
