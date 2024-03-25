using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<string, ItemType> myItems = new Dictionary<string, ItemType>();
    public string myHelmet;
    public string myArmor;
    public string myWeapon;

    private void Awake()
    {
        myItems.Add("knife", ItemType.weapon);
        myItems.Add("none_helmet", ItemType.helmet);
        myItems.Add("none_armor", ItemType.armor);

        myHelmet = "none_helmet";
        myArmor = "none_armor";
        myWeapon = "knife";
    }

    public void EquipWeapon_I(string weaponName)
    {
        myWeapon = weaponName;
    }

    public void EquipArmor_I(string armorName)
    {
        myArmor = armorName;
    }
    public void EquipHelmet_I(string helmetName)
    {
        myHelmet = helmetName;
    }

    public void AddItems(string itemName, ItemType itemType)
    {
        if (myItems.ContainsKey(itemName))
        {
            return;
        }

        myItems.Add(itemName, itemType);
    }

    public List<string> showWeapons()
    {
        List<string> weapons = new List<string>();
        foreach(KeyValuePair<string, ItemType> data in myItems)
        {
            if(data.Value == ItemType.weapon)
            {
                weapons.Add(data.Key);
            }
        }

        return weapons;
    }

    public List<string> showArmors()
    {
        List<string> armors = new List<string>();
        foreach (KeyValuePair<string, ItemType> data in myItems)
        {
            if (data.Value == ItemType.armor)
            {
                armors.Add(data.Key);
            }
        }

        return armors;
    }

    public List<string> showHelmets()
    {
        List<string> helmets = new List<string>();
        foreach (KeyValuePair<string, ItemType> data in myItems)
        {
            if (data.Value == ItemType.helmet)
            {
                helmets.Add(data.Key);
            }
        }

        return helmets;
    }
}
