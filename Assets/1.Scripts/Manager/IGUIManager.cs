using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IGUIManager : MonoBehaviour
{
    private static IGUIManager instance;
    public static IGUIManager Instance => instance;

    public GameObject BInvenUI;
    public GameObject PauseUI;
    public GameObject GameoverUI;
    public GameObject SettingUI;
    public GameObject HelpUI;
    public GameObject LevelUpUI;

    public TMPro.TMP_Text coin;
    public TMPro.TMP_Text myExp;
    public TMPro.TMP_Text needExp;
    public TMPro.TMP_Text score;

    public Button GameoverButton;
    public Button PauseButton;

    public Image timeBar;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        coin.text = Player.Instance.myGold.ToString();
        myExp.text = Player.Instance.myExp.ToString();
        needExp.text = Player.Instance.myExpNeed.ToString();

        InGameManager.Instance.AddScore(0);

        GameoverButton.onClick.AddListener(LoadManager.Instance.Change_to_MainScene);
        PauseButton.onClick.AddListener(LoadManager.Instance.Change_to_MainScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenPause()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ClosePause()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenBInven()
    {
        BInvenUI.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void CloseBInven()
    {
        BInvenUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void GotoMain()
    {
        Time.timeScale = 1.0f;
        LoadManager.Instance.Change_to_MainScene();
    }

    public void OpenSetting()
    {
        SettingUI.SetActive(true);
    }

    public void CloseSetting()
    {
        SettingUI.SetActive(false);
    }

    public void OpenHelp()
    {
        HelpUI.SetActive(true);
    }

    public void CloseHelp()
    {
        HelpUI.SetActive(false);
    }

    public void OpenLevelUp()
    {
        Time.timeScale = 0.0f;
        LevelUpUI.SetActive(true);
    }

    public void CloseLevelUp()
    {
        LevelUpUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
