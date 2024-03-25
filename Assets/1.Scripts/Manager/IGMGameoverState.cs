using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IGMGameoverState : IGMState
{
    public override void EnterState()
    {
        Player.Instance.GameOver();
        //Ŭ���� UI ����
        IGUIManager.Instance.GameoverUI.SetActive(true);
    }

    public override void UpdateState()
    {

    }
}
