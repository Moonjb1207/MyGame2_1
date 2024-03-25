using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingEquipContainer : MonoBehaviour
{
    public List<BuildingEquip> buildings = new List<BuildingEquip>();
    public BuildingEquip buildingEquip;

    private void Awake()
    {
        BuildingEquip[] buildingsarr = GetComponentsInChildren<BuildingEquip>(true);

        for (int i = 0; i < buildingsarr.Length; i++)
        {
            buildings.Add(buildingsarr[i]);
        }
    }

    private void OnEnable()
    {
        LoadBuilding();
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadBuilding();
    }

    public void LoadBuilding()
    {
        if (InventoryManager.Instance == null)
            return;

        List<string> showBuildings = InventoryManager.Instance.ShowBuildings();

        if (showBuildings.Count < buildings.Count)
        {
            for (int i = 0; i < showBuildings.Count; i++)
            {
                buildings[i].setMyBuilding(showBuildings[i]);
                buildings[i].setMyImg();
                buildings[i].gameObject.SetActive(true);
            }
            for (int i = showBuildings.Count; i < buildings.Count; i++)
            {
                buildings[i].gameObject.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < buildings.Count; i++)
            {
                buildings[i].setMyBuilding(showBuildings[i]);
                buildings[i].setMyImg();
                buildings[i].gameObject.SetActive(true);
            }
            for (int i = buildings.Count; i < showBuildings.Count; i++)
            {
                buildings.Add(Instantiate(buildingEquip));
                buildings[i].setMyBuilding(showBuildings[i]);
                buildings[i].setMyImg();
                buildings[i].transform.SetParent(transform);
            }
        }
    }
}
