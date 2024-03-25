using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMGameoverState : IGMState
{
    public override void EnterState()
    {
        Player.Instance.GameOver();
        //클리어 UI 띄우기
        IGUIManager.Instance.GameoverUI.SetActive(true);
    }

    public override void UpdateState()
    {

    }
}
