using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingText : MonoBehaviour
{
    TMPro.TMP_Text loadingText;

    string[] loadingtexts = new string[4];
    public GameObject[] RawImage;

    float changetext;
    float curtime;

    int curindex;

    // Start is called before the first frame update
    void Start()
    {
        int rnd = Random.Range(0, 5);

        for (int i = 0; i < RawImage.Length; i++)
        {
            RawImage[i].SetActive(false);

            if (rnd == i)
                RawImage[i].SetActive(true);
        }

        changetext = 0.5f;
        curtime = 0;
        curindex = 0;

        loadingtexts[0] = "Loading.";
        loadingtexts[1] = "Loading..";
        loadingtexts[2] = "Loading...";
        loadingtexts[3] = "Loading....";

        loadingText = GetComponent<TMPro.TMP_Text>();

        loadingText.text = loadingtexts[curindex];
    }

    // Update is called once per frame
    void Update()
    {
        curtime += Time.deltaTime;

        if(curtime >= changetext)
        {
            if(++curindex >= loadingtexts.Length)
            {
                curindex = 0;
            }

            loadingText.text = loadingtexts[curindex];
            curtime = 0;
        }
    }
}
