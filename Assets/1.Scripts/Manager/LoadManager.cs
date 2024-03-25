using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class LoadManager : MonoBehaviour
{
    private static LoadManager instance;
    public static LoadManager Instance => instance;

    public string curScene;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    public void ChangecurScene(string scene)
    {
        curScene = scene;
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadSceneAsync(scene);
    }

    public void LoadScene()
    {
        StartCoroutine(LoadingScene(curScene));
    }

    public void GameStart()
    {
        ChangecurScene("PlayGame99");
        SceneManager.LoadSceneAsync("Loading");
    }

    public void Change_to_MainScene()
    {
        Time.timeScale = 1.0f;
        ChangecurScene("Main");

        SceneManager.LoadSceneAsync("Loading");
    }
    public void FirstLoadScene()
    {
        curScene = "Main";
        StartCoroutine(LoadingScene(curScene));
    }

    IEnumerator LoadingScene(string scene)
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);

        ao.allowSceneActivation = false;

        while(!ao.isDone)
        {
            if (ao.progress >= 0.9f)
            {
                yield return new WaitForSeconds(2.0f);
                ao.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
