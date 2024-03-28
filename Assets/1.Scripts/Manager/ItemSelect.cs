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
                Player.Instance.UpgradeStat(myName, myType);
                break;
            case cardType.Building:
                InventoryManager.Instance.AddBuildings(myName);
                LevelUpCardData.Instance.removeCard(myName, myType);
                break;
            case cardType.BuildingUpgrade:
                InventoryManager.Instance.LevelUpBuildings(myName);
                break;
            case cardType.Weapon:
                InventoryManager.Instance.myWeapon = myName;
                InventoryManager.Instance.AddWeapons(myName);
                break;
            case cardType.WeaponUpgrade:
                InventoryManager.Instance.LevelUpWeapons(myName);
                Player.Instance.curWeapon.UpgradeWeapon
                    (LevelUpCardData.Instance.GetCardStat(myName, myType).value1, 
                    LevelUpCardData.Instance.GetCardStat(myName, myType).value2,
                LevelUpCardData.Instance.GetCardStat(myName, myType).value3
                );
                break;
        }

        IGUIManager.Instance.CloseLevelUp();
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
