using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapBtn : MonoBehaviour
{
    public int idx;
    Button btn;
    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClickedTapBtn); //코드로 Button 컴포넌트에 함수 등록 
    }

    void OnClickedTapBtn()
    {
        GetComponentInParent<TapContainer>().Select(idx);
    }

    public void Selected(int curIdx)
    {
        if(idx == curIdx)
        {
            btn.image.color = ColorInfo.activeColor;
        }
        else
        {
            btn.image.color = Color.white;
        }
    }
}