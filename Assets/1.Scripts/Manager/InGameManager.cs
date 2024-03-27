using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameManager : MonoBehaviour
{
    private static InGameManager instance;
    public static InGameManager Instance => instance;

    public EnemyRespawn[] mySpawner;
    public int spawnerCount;

    [SerializeField] IGMState curIGState;

    public IGMBuildingState buildingState;
    public IGMDefenseState defenseState;
    public IGMFinishState finishState;
    public IGMGameoverState gameoverState;

    public BuildingManager buildManagner;
    public Button buildButton;

    public int wave;

    public int score;

    public int respawnCount;
    public int maxCount;
    public int respawnDelay;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        mySpawner = GetComponentsInChildren<EnemyRespawn>();

        score = 0;
        spawnerCount = 0;
        wave = 0;

        respawnCount = 20;
        maxCount = 10;
        respawnDelay = 1;

        buildingState = GetComponent<IGMBuildingState>();
        defenseState = GetComponent<IGMDefenseState>();
        finishState = GetComponent<IGMFinishState>();
        gameoverState = GetComponent<IGMGameoverState>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //SoundManager.Instance.PlayPlayBGSound();
        //error

        NextState(buildingState);
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player.Instance.IsLive)
        {
            NextState(gameoverState);
        }
        curIGState?.UpdateState();
    }

    public void NextState(IGMState state)
    {
        if (state == curIGState) return;

        curIGState = state;
        curIGState.EnterState();
    }

    public void AddScore(int s)
    {
        score += s;

        IGUIManager.Instance.score.text = score.ToString();
    }
    
    public void AddWave()
    {
        respawnCount = respawnCount + respawnCount * wave;
        maxCount = maxCount + maxCount * wave / 10;
    }
}
