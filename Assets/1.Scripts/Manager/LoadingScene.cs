using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadManager.Instance.LoadScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
