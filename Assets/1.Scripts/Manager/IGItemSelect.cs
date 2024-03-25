using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IGItemSelect : MonoBehaviour
{
    public Image Img;
    public string myName;
    public ItemType myType;

    private void Awake()
    {
        //Img = 
    }

    //public void SelectItem()
    //{
    //    if (myType == ItemType.weapon)
    //    {
    //        InventoryManager.Instance.EquipWeapon_I(myName);
    //        Player.Instance?.EquipWeapon(myName);
    //    }
    //    else if (myType == ItemType.armor)
    //    {
    //        InventoryManager.Instance.EquipArmor_I(myName);
    //        Player.Instance?.EquipArmor(myName);
    //    }
    //    else if (myType == ItemType.helmet)
    //    {
    //        InventoryManager.Instance.EquipHelmet_I(myName);
    //        Player.Instance?.EquipHelmet(myName);
    //    }
    //}

    public void setMyName(string itemName)
    {
        myName = itemName;
    }

    public void setMyType(ItemType itemType)
    {
        myType = itemType;
    }
}
