using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpCardData : MonoBehaviour
{
    private static LevelUpCardData instance;
    public static LevelUpCardData Instance => instance;

    public TextAsset cardData;
    public LevelUpCards cardDatas;

    public List<LevelUpCard> CardDatas = new List<LevelUpCard>();

    private void Awake()
    {
        if (instance == null)
            instance = this;

        cardDatas = JsonUtility.FromJson<LevelUpCards>(cardData.text);

        for (int i = 0; i < cardDatas.cards.Length; i++)
        {
            CardDatas.Add(cardDatas.cards[i]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void removeCard(string ename, cardType mytype)
    {
        for (int i = 0; i < CardDatas.Count; i++)
        {
            if (ename == CardDatas[i].name && mytype == CardDatas[i].myType)
            {
                CardDatas.RemoveAt(i);
                break;
            }
        }
    }

    public void addCard(string ename, cardType mytype)
    {
        for (int i = 0; i < cardDatas.cards.Length; i++)
        {
            if (ename == cardDatas.cards[i].name && mytype == cardDatas.cards[i].myType)
            {
                if (CardDatas.Contains(cardDatas.cards[i]))
                    break;
                CardDatas.Add(cardDatas.cards[i]);
                break;
            }
        }
    }
    
    public LevelUpCard GetCardStat(string ename, cardType mytype)
    {
        LevelUpCard myCard;

        for (int i = 0; i < cardDatas.cards.Length; i++)
        {
            if (ename == cardDatas.cards[i].name && mytype == cardDatas.cards[i].myType)
            {
                myCard = cardDatas.cards[i];
                return myCard;
            }
        }

        return null;
    }
}

[System.Serializable]
public class LevelUpCards
{
    public LevelUpCard[] cards;
}

public class LevelUpCard
{
    public string name;
    public int weight;
    public cardType myType;
    public float value1;
    public float value2;
    public float value3;
    public string s_value1;
    public string s_value2;
    public string s_value3;
}

public enum cardType
{ 
    Weapon,
    WeaponUpgrade,
    Building,
    BuildingUpgrade,
    StatUpgrade,
}

