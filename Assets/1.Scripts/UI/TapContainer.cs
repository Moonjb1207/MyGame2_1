using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapContainer : MonoBehaviour
{
    public TapBtn[] tapBtns;
    public TapPanel[] tapPanels;

    private void Awake()
    {
        tapBtns = GetComponentsInChildren<TapBtn>(); //������ Ȱ��ȭ�� ������Ʈ�� ��� TapBtn ������Ʈ ã�� �迭�� ��ȯ
        tapPanels = GetComponentsInChildren<TapPanel>(true);//������ ��� TapPanel ������Ʈ �迭 ã�� - ��Ȱ��ȭ�� ������Ʈ ����
        
        curIdx = 0;

        for(int i = 0; i < tapBtns.Length; i++)
        {
            tapBtns[i].idx = i;
        }
    }

    public int curIdx //ĸ��ȭ - ���� ��ü���� ���� ���������� �ܺ� ��ü���� ���� �Ұ�
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
        curIdx = idx; //���õ� �ε���
                      //�����غ��� - tapPanels �迭�� �ε��� curIdx�� Ȱ��ȭ�ϱ�
        tapPanels[curIdx].gameObject.SetActive(true);

        for(int i = 0; i < tapBtns.Length; i++)
        {
            tapBtns[i].Selected(curIdx);
        }
    }
}