using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapContainer : MonoBehaviour
{
    public TapBtn[] tapBtns;
    public TapPanel[] tapPanels;

    private void Awake()
    {
        tapBtns = GetComponentsInChildren<TapBtn>(); //하위의 활성화된 오브젝트의 모든 TapBtn 컴포넌트 찾아 배열로 반환
        tapPanels = GetComponentsInChildren<TapPanel>(true);//하위의 모든 TapPanel 컴포넌트 배열 찾기 - 비활성화된 오브젝트 포함
        
        curIdx = 0;

        for(int i = 0; i < tapBtns.Length; i++)
        {
            tapBtns[i].idx = i;
        }
    }

    public int curIdx //캡슐화 - 현재 객체에서 접근 가능하지만 외부 객체에서 접근 불가
    {
        private set;
        get;
    }

    private void Start()
    {
        Select(curIdx);
    }

    public void Select(int idx)
    {
        tapPanels[curIdx].gameObject.SetActive(false);
        curIdx = idx; //선택된 인덱스
                      //직접해보기 - tapPanels 배열의 인덱스 curIdx만 활성화하기
        tapPanels[curIdx].gameObject.SetActive(true);

        for(int i = 0; i < tapBtns.Length; i++)
        {
            tapBtns[i].Selected(curIdx);
        }
    }
}