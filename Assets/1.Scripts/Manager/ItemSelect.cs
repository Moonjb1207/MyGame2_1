using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public Image Img;
    public string myName;
    public cardType myType;
    public TMPro.TMP_Text Info;

    public Button selectButton;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        
    }

    public void SelectItem()
    {
        switch (myType)
        {
            case cardType.StatUpgrade:

                break;
            case cardType.Building:
                InventoryManager.Instance.AddBuildings(myName);
                LevelUpCardData.Instance.removeCard(myName, myType);
                break;
            case cardType.BuildingUpgrade:
                InventoryManager.Instance.LevelUpBuildings(myName);
                break;
            case cardType.Weapon:
                LevelUpCardData.Instance.addCard(InventoryManager.Instance.myWeapon, cardType.Weapon);
                InventoryManager.Instance.myWeapon = myName;
                LevelUpCardData.Instance.removeCard(myName, myType);
                break;
            case cardType.WeaponUpgrade:
                InventoryManager.Instance.LevelUpWeapons(myName);
                break;
        }
    }

    public void setMyItem(string itemName, cardType itemType)
    {
        myType = itemType;
        myName = itemName;
    }

    public void setImg()
    {
        switch (myType)
        {
            case cardType.StatUpgrade:
                Img.sprite = EquipmentManager.Instance.GetStatUpStat(myName).myImg;
                break;
            case cardType.Building:
                Img.sprite = EquipmentManager.Instance.GetBuildingStat(myName).myImg;
                break;
            case cardType.BuildingUpgrade:
                Img.sprite = EquipmentManager.Instance.GetBuildingStat(myName).myImg;
                break;
            case cardType.Weapon:
                Img.sprite = EquipmentManager.Instance.GetWeaponStat(myName).myImg;
                break;
            case cardType.WeaponUpgrade:
                Img.sprite = EquipmentManager.Instance.GetWeaponStat(myName).myImg;
                break;
        }
    }

    public void setInfo()
    {
        switch(myType)
        {
            case cardType.StatUpgrade:
                break;
            case cardType.Building:
                break;
            case cardType.BuildingUpgrade:
                break;
            case cardType.Weapon:
                break;
            case cardType.WeaponUpgrade:
                break;
        }
    }
}
