using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SelectedMove : ScriptableObject {

    public Move move;

    public void SetMove(Move move)
    {
        this.move = move;
    }

    public Move GetMove()
    {
        return this.move;
    }

    public void ClearMove()
    {
        this.move = null;
    }

}
