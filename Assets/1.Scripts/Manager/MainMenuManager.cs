using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject SettingMenu;
    public GameObject HelpMenu;

    public Button PlayButton;

    private void Awake()
    {

    }

    private void Start()
    {
        PlayButton.onClick.AddListener(LoadManager.Instance.GameStart);
    }
    public void OpenSettingMenu()
    {
        SettingMenu.SetActive(true);
    }

    public void CloseSettingMenu()
    {
        SettingMenu.SetActive(false);
    }

    public void OpenHelpMenu()
    {

    }

    public void CloseHelpMenu()
    {

    }
}