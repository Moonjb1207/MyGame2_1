using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstLoadingScene : MonoBehaviour
{
    //float curTime = 0.0f;
    //float LoadingTime = 3.0f;

    private void Start()
    {
        LoadManager.Instance.FirstLoadScene();
    }

    private void Update()
    {
        //curTime += Time.deltaTime;
        //if (curTime > LoadingTime)
        //{
        //    LoadManager.Instance.Change_to_MainScene();
        //}
    }
}
