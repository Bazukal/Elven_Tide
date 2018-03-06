using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatClass{

    private int hpMin;
    private int hpMax;

    private int mpMin;
    private int mpMax;

    private int strMin;
    private int strMax;

    private int agiMin;
    private int agiMax;

    private int mindMin;
    private int mindMax;

    private int soulMin;
    private int soulMax;

    private int defMin;
    private int defMax;

    public StatClass() { }

    public StatClass(int _hpMin, int _hpMax, int _mpMin, int _mpMax, int _strMin, int _strMax, int _agiMin,
        int _agiMax, int _mindMin, int _mindMax, int _soulMin, int _soulMax, int _defMin, int _defMax)
    {
        hpMin = _hpMin;
        hpMax = _hpMax;
        mpMin = _mpMin;
        mpMax = _mpMax;
        strMin = _strMin;
        strMax = _strMax;
        agiMin = _agiMin;
        agiMax = _agiMax;
        mindMin = _mindMin;
        mindMax = _mindMax;
        soulMin = _soulMin;
        soulMax = _soulMax;
        defMin = _defMin;
        defMax = _defMax;
    }

    //getters for stat growth
    public int getHPMin() { return hpMin; }
    public int getHPMax() { return hpMax; }
    public int getMPMin() { return mpMin; }
    public int getMPMax() { return mpMax; }
    public int getStrMin() { return strMin; }
    public int getStrMax() { return strMax; }
    public int getAgiMin() { return agiMin; }
    public int getAgiMax() { return agiMax; }
    public int getMindMin() { return mindMin; }
    public int getMindMax() { return mindMax; }
    public int getSoulMin() { return soulMin; }
    public int getSoulMax() { return soulMax; }
    public int getDefMin() { return defMin; }
    public int getDefMax() { return defMax; }
}
