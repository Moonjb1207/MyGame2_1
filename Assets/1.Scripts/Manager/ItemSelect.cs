using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSelect : MonoBehaviour
{
    public Image Img;
    public string myName;
    public string myType;
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
            case "StatUpgrade":
                Player.Instance.UpgradeStat(myName, myType);
                break;
            case "Building":
                InventoryManager.Instance.AddBuildings(myName);
                LevelUpCardData.Instance.removeCard(myName, myType);
                break;
            case "BuildingUpgrade":
                InventoryManager.Instance.LevelUpBuildings(myName);
                break;
            case "Weapon":
                InventoryManager.Instance.myWeapon = myName;
                InventoryManager.Instance.AddWeapons(myName);
                Player.Instance.EquipItem(ItemType.weapon, myName);
                break;
            case "WeaponUpgrade":
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

    public void setMyItem(string itemName, string itemType)
    {
        myType = itemType;
        myName = itemName;
    }

    public void setImg()
    {
        switch (myType)
        {
            case "StatUpgrade":
                Img.sprite = EquipmentManager.Instance.GetStatUpStat(myName).myImg;
                break;
            case "Building":
                Img.sprite = EquipmentManager.Instance.GetBuildingStat(myName).myImg;
                break;
            case "BuildingUpgrade":
                Img.sprite = EquipmentManager.Instance.GetBuildingStat(myName).myImg;
                break;
            case "Weapon":
                Img.sprite = EquipmentManager.Instance.GetWeaponStat(myName).myImg;
                break;
            case "WeaponUpgrade":
                Img.sprite = EquipmentManager.Instance.GetWeaponStat(myName).myImg;
                break;
        }
    }

    public void setInfo()
    {
        switch(myType)
        {
            case "StatUpgrade":
                break;
            case "Building":
                break;
            case "BuildingUpgrade":
                break;
            case "Weapon":
                break;
            case "WeaponUpgrade":
                break;
        }
    }
}
