using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class LvExp
{
    public string myName;
    public Sprite myImg;

    public LvExp(string n, Sprite ne)
    {
        myImg = ne;
        myName = n;
    }
}


[CreateAssetMenu(fileName = "LvExp Data", menuName = "ScriptableObject/LvExp Data", order = -1)]
public class LvExpData : ScriptableObject
{
    [SerializeField] LvExp[] lvexpData;

    public LvExp[] LvExpDatas
    {
        get => lvexpData;
    }
}
