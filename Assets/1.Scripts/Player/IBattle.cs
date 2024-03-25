using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBattle
{
    public void OnDamage(float dmg);
    public void AddExp(int exp);
    public void AddGold(int gold);

    public bool IsLive
    {
        get;
    }
}

public enum DeBuffType
{
    Slow,
    Burn,
    Bleeding,

}

public struct DeBuff
{
    public DeBuff(DeBuffType t, float keep, float v, float dt)
    {
        type = t;
        keepTime = keep;
        value = v;
        maxDamageTime = dt;
        curDamageTime = 0.0f;
    }

    public DeBuffType type;
    public float keepTime;
    public float value;
    public float maxDamageTime;
    public float curDamageTime;
}