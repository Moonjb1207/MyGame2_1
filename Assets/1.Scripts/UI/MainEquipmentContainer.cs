using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEquipmentContainer : MonoBehaviour
{
    public List<ItemSelect> items = new List<ItemSelect>();
    public ItemSelect selectItem;
    public ItemType itemType;


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

    public void LoadEquipment()
    {
        if (InventoryManager.Instance == null)
            return;

        List<string> showItems = InventoryManager.Instance.ShowEquipments(itemType);

        if (showItems.Count <= items.Count)
        {
            for (int i = 0; i < showItems.Count; i++)
            {
                items[i].setMyItem(showItems[i], itemType);
                items[i].setImg();
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
                items[i].setMyItem(showItems[i], itemType);
                items[i].setImg();
                items[i].gameObject.SetActive(true);
            }
            for (int i = items.Count; i < showItems.Count; i++)
            {
                items.Add(Instantiate(selectItem));
                items[i].setMyItem(showItems[i], itemType);
                items[i].setImg();
                items[i].transform.SetParent(transform);
            }
        }
    }
}
