using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IGMState : MonoBehaviour
{
    public InGameManager manager;
    public float remainTime;

    private void Awake()
    {
        manager = GetComponent<InGameManager>();
        remainTime = 0;
    }


    public abstract void EnterState();
    public abstract void UpdateState();
}
