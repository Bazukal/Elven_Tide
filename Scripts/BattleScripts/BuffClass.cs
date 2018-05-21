using System;

[Serializable]
public class BuffClass{

    private bool isBuffed = false;
    private int rounds = 0;
    private int strength = 0;

    public BuffClass() { }

    public void SetBuffed(bool buffed) { isBuffed = buffed; }
    public void SetRounds(int _rounds) { rounds = _rounds; }
    public void SetStrength(int _strength) { strength = _strength; }

    public bool GetBuffed() { return isBuffed; }
    public int GetRounds() { return rounds; }
    public int GetStrength() { return strength; }

    public bool UpdateRounds()
    {
        if(isBuffed)
        {
            rounds--;
            if(rounds == 0)
            {
                isBuffed = false;
                strength = 0;
                return true;
            }
        }

        return false;
    }
}
