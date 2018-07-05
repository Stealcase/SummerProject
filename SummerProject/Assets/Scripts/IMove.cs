using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoveID
{
    JumpStrike,
    Run
}

public enum ComboID
{
    EpicLeapStrike,
    SlideTackle
}

public interface IMove {

    int BaseValue();
    int ScaledValue();
    int Priority();
    int Cooldown();

    Dictionary<MoveID, ComboID> GetCombo();

    MoveStat Type();
    string Name();


}
