using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => instance;

    //public Dictionary<string, ItemType> myItems = new Dictionary<string, ItemType>();
    public Dictionary<ItemType, List<string>> myItemDic = new Dictionary<ItemType, List<string>>();
    public Dictionary<string, int> mybuildingDic = new Dictionary<string, int>();

    public string myWeapon;
    public string myBuilding;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        myItemDic.Add(ItemType.weapon, new List<string>());

        myWeapon = "machete";
        myBuilding = "Box";

        myItemDic[ItemType.weapon].Add(myWeapon);

        mybuildingDic.Add("Box", 1);
    }

    private void Start()
    {
    }

    //public void EquipWeapon_I(string weaponName)
    //{
    //    myWeapon = weaponName;
    //}

    //public void EquipArmor_I(string armorName)
    //{
    //    myArmor = armorName;
    //}
    //public void EquipHelmet_I(string helmetName)
    //{
    //    myHelmet = helmetName;
    //}

    public void Equip(ItemType type, string equipName)
    {
        switch (type)
        {
            case ItemType.weapon:
                myWeapon = equipName;
                break;
        }
    }

    public void EquipBuilding(string buildingName)
    {
        myBuilding = buildingName;
    }

    public void AddItems(string itemName, ItemType itemType)
    {
        if (myItemDic[itemType].Contains(itemName))
        {
            return;
        }

        myItemDic[itemType].Add(itemName);
    }

    public void AddBuildings(string buildingName)
    {
        if (mybuildingDic[buildingName] > 0)
        {
            return;
        }

        mybuildingDic[buildingName] = 1;
    }

    public List<string> ShowEquipments(ItemType itemType)
    {
        if (!myItemDic.ContainsKey(itemType)) return null;

        return myItemDic[itemType];
    }

    public List<string> ShowBuildings()
    {
        List<string> buildings = new List<string>();

        for (int i = 0; i < EquipmentManager.Instance.buildingData.buildingStats.Length; i++)
        {
            if (mybuildingDic[(EquipmentManager.Instance.buildingData.buildingStats[i].buildingName)] > 0)
            {
                buildings.Add(EquipmentManager.Instance.buildingData.buildingStats[i].buildingName);
            }
        }

        return buildings;
    }
}
