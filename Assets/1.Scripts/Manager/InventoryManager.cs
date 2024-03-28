using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance => instance;

    //public Dictionary<string, ItemType> myItems = new Dictionary<string, ItemType>();
    //public Dictionary<ItemType, List<string>> myItemDic = new Dictionary<ItemType, List<string>>();
    public Dictionary<string, int> mybuildingDic = new Dictionary<string, int>();
    public Dictionary<string, int> myWeaponDic = new Dictionary<string, int>();

    public string myWeapon;
    public string myBuilding;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        myWeapon = "Colt";
        myBuilding = "Box";

        AddWeapons(myWeapon);
        AddBuildings(myBuilding);
    }

    private void Start()
    {
        LevelUpCardData.Instance.removeCard(myBuilding, cardType.Building);
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

    public void EquipWeapon(string wName)
    {
        myWeapon = wName;
    }

    public void AddWeapons(string wName)
    {
        if (myWeaponDic.ContainsKey(wName))
        {
            return;
        }

        myWeaponDic.Add(wName, 1);
    }

    public void LevelUpWeapons(string wName)
    {
        if (!myWeaponDic.ContainsKey(wName))
        {
            return;
        }

        myWeaponDic[wName]++;
    }

    public void EquipBuilding(string buildingName)
    {
        myBuilding = buildingName;
    }

    public void AddBuildings(string buildingName)
    {
        if (mybuildingDic.ContainsKey(buildingName))
        {
            return;
        }

        mybuildingDic.Add(buildingName, 1);
    }

    public void LevelUpBuildings(string buildingName)
    {
        if (!mybuildingDic.ContainsKey(buildingName))
        {
            return;
        }

        mybuildingDic[buildingName]++;
    }

    public bool CheckBuildings(string buildingName)
    {
        if (mybuildingDic.ContainsKey(buildingName))
            return true;
        else
            return false;
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
