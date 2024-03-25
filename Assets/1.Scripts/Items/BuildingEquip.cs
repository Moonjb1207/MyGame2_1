using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingEquip : MonoBehaviour
{
    public Image Img;
    public string myName;
    public TMPro.TMP_Text equiping;

    private void Awake()
    {
        //Img = 
    }

    private void OnEnable()
    {
        //Img.sprite = EquipmentManager.Instance.GetBuildingStat(myName).myImg;
    }

    public void EquipBuilding()
    {
        InventoryManager.Instance.EquipBuilding(myName);

        GetComponentInParent<BuildingEquipContainer>().LoadBuilding();
    }

    public void setMyBuilding(string itemName)
    {
        myName = itemName;
    }

    public void setMyImg()
    {
        Img.sprite = EquipmentManager.Instance.GetBuildingStat(myName).myImg;
    }

    public void ShowInfo()
    {
        string name = myName;
        string ability = "HP : " + EquipmentManager.Instance.GetBuildingStat(myName).buildingHP.ToString();
        string aE = EquipmentManager.Instance.GetBuildingStat(myName).buildingInfo;
    }
}
