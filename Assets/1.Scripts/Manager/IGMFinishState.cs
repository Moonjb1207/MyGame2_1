using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMFinishState : IGMState
{
    protected float myTime = 10.0f;

    public override void EnterState()
    {
        remainTime = myTime;
        IGUIManager.Instance.timeBar.fillAmount = remainTime / myTime;

        //attack end
    }

    public override void UpdateState()
    {
        remainTime -= Time.deltaTime;
        IGUIManager.Instance.timeBar.fillAmount = remainTime / myTime;

        if (remainTime < 0)
        {
            manager.NextState(manager.buildingState);
        }
    }
}
