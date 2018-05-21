using System;
using UnityEngine;

[Serializable]
public class DebuffClass {
    
    private bool isEffected = false;
    private int rounds = 0;
    private int strength = 0;

    public DebuffClass() { }

    public void SetEffected(bool effect) { isEffected = effect; }
    public void SetRounds(int _rounds) { rounds = _rounds; }
    public void SetStrength(int _strength) { strength = _strength; }

    public bool reduceRound()
    {
        rounds--;
        if (rounds == 0)
        {
            isEffected = false;
            strength = 0;
            return true;
        }
        else
        {
            return false;
        }
            
    }

    public bool GetEffected() { return isEffected; }
    public int GetRounds() { return rounds; }
    public int GetStrength() { return strength; }
}
