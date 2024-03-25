using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettingManager : MonoBehaviour
{
    public Slider[] Sound;
    private static SoundSettingManager instance;
    public static SoundSettingManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void OnEnable()
    {
        Sound[0].value = SettingManager.Instance.MSSound;
        Sound[1].value = SettingManager.Instance.BGSound;
        Sound[2].value = SettingManager.Instance.EFSound;
    }

    public void SaveSetting()
    {
        SettingManager.Instance.MSSound = Sound[0].value;
        SettingManager.Instance.BGSound = Sound[1].value;
        SettingManager.Instance.EFSound = Sound[2].value;

        //BGSoundManager.Inst.GS_myBG.volume = SettingManager.Inst.BGSound;

        SettingManager.Instance.SettingSave(SettingManager.Instance.settingDataPath);
    }

    //public void ChangeSound(int num)
    //{
    //    Sound[num].value = float.Parse(Soundnum[num].text) / 100;

    //    if (num > 0 && Sound[num].value > Sound[0].value)
    //    {
    //        Sound[num].value = Sound[0].value;
    //    }
    //    else if (num == 0)
    //    {
    //        Sound[1].value = Sound[0].value;
    //        Sound[2].value = Sound[0].value;
    //    }
    //}

    public void ChangeSound_Slider(int num)
    {
        //Soundnum[num].text = ((int)(Sound[num].value * 100)).ToString();

        if (num > 0 && Sound[num].value > Sound[0].value)
        {
            Sound[num].value = Sound[0].value;
        }
        else if (num == 0)
        {
            Sound[1].value = Sound[0].value;
            Sound[2].value = Sound[0].value;
        }

        SaveSetting();
        SoundManager.Instance.ChangeBGVolume();
        SoundManager.Instance.ChangeEFVolume();
    }
}
