using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public Image Img;
    public string myName;
    public ItemType myType;
    public TMPro.TMP_Text equiping;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    public void SelectItem()
    {
        InventoryManager.Instance.Equip(myType, myName);
        Player.Instance?.EquipItem(myType, myName);

        GetComponentInParent<MainEquipmentContainer>().LoadEquipment();
    }

    public void setMyItem(string itemName, ItemType itemType)
    {
        myType = itemType;
        myName = itemName;
    }

    public void setImg()
    {
        if (myType == ItemType.weapon)
        {
            Img.sprite = EquipmentManager.Instance.GetWeaponStat(myName).myImg;
        }
    }

    public void ShowInfo()
    {
        
    }
}
