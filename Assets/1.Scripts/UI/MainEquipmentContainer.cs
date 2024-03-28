using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEquipmentContainer : MonoBehaviour
{
    public List<ItemSelect> items = new List<ItemSelect>();
    public ItemSelect selectItem;
    public ItemType itemType;

    int totalWeight;

    private void Awake()
    {
        ItemSelect[] itemsarr = GetComponentsInChildren<ItemSelect>(true);

        for (int i = 0; i < itemsarr.Length; i++)
        {
            items.Add(itemsarr[i]);
        }
    }

    private void OnEnable()
    {
        LoadEquipment();
    }

    private void Start()
    {
        LoadEquipment();
    }

    public List<LevelUpCard> RandomCardPick()
    {
        List<LevelUpCard> cards = new List<LevelUpCard>();

        calTotalWeight();

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, totalWeight) + 1;
            int curTotal = 0;

            LevelUpCard randCard = new LevelUpCard();

            for (int j = 0; j < LevelUpCardData.Instance.CardDatas.Count; j++)
            {
                curTotal += LevelUpCardData.Instance.CardDatas[j].weight;

                if (rand <= curTotal)
                {
                    randCard = LevelUpCardData.Instance.CardDatas[j];
                    break;
                }
            }

            //건물을 가지고 있지 않을 때 업그레이드가 뜨는 경우
            if (randCard.myType == cardType.BuildingUpgrade)
            {
                if(!InventoryManager.Instance.CheckBuildings(randCard.name))
                {
                    i--;
                    continue;
                }
            }
            //현재 장착중인 무기 이외의 무기업그레이드가 뜨는 경우
            if (randCard.myType == cardType.WeaponUpgrade)
            {
                if (InventoryManager.Instance.myWeapon != randCard.name)
                {
                    i--;
                    continue;
                }
            }
            //현재 장착중인 무기가 뜨는경우
            if (randCard.myType == cardType.Weapon)
            {
                if(InventoryManager.Instance.myWeapon == randCard.name)
                {
                    i--;
                    continue;
                }
            }

            bool isFirst = false;

            for (int j = 0; j < cards.Count; i++)
            {
                if (randCard.Equals(cards[j]))
                {
                    isFirst = true;
                    break;
                }
            }

            if (isFirst)
            {
                i--;
                continue;
            }
            else
            {
                cards.Add(randCard);
            }
        }

        return cards;
    }

    public void calTotalWeight()
    {
        for (int i = 0; i < LevelUpCardData.Instance.CardDatas.Count; i++)
        {
            totalWeight += LevelUpCardData.Instance.CardDatas[i].weight;
        }
    }

    public void LoadEquipment()
    {
        if (InventoryManager.Instance == null)
            return;

        List<LevelUpCard> showItems = RandomCardPick();

        if (showItems.Count <= items.Count)
        {
            for (int i = 0; i < showItems.Count; i++)
            {
                items[i].setMyItem(showItems[i].name, showItems[i].myType);
                items[i].setImg();
                items[i].setInfo();
                items[i].gameObject.SetActive(true);
            }
            for (int i = showItems.Count; i < items.Count; i++)
            {
                items[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++)
            {
                items[i].setMyItem(showItems[i].name, showItems[i].myType);
                items[i].setImg();
                items[i].setInfo();
                items[i].gameObject.SetActive(true);
            }
            for (int i = items.Count; i < showItems.Count; i++)
            {
                items.Add(Instantiate(selectItem));
                items[i].setMyItem(showItems[i].name, showItems[i].myType);
                items[i].setImg();
                items[i].setInfo();
                items[i].transform.SetParent(transform);
            }
        }
    }
}
