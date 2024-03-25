using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, IBattle
{
    public EnemyData data;

    public float curHP;
    public float curDelay;
    public bool dead;
    
    public Transform target;

    [SerializeField] protected EnemyState curEnemyState;
    public EnemyMovementState movementState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;

    public LayerMask myEnemy;

    public EnemyQueueNum myNum;

    public EnemyRespawn mySpawn;

    [SerializeField] List<DeBuff> debuffList = new List<DeBuff>();

    public GameObject hpBarCanvas;
    public Image hpBar;

    public Collider myColl;
    public Rigidbody myRigid;

    public GameObject damagedEffect;

    Renderer[] _allRenderer = null;

    protected Renderer[] allRenderer
    {
        get
        {
            if (_allRenderer == null)
            {
                _allRenderer = GetComponentsInChildren<Renderer>();
            }

            return _allRenderer;
        }
    }

    private void Awake()
    {
        movementState = GetComponentInChildren<EnemyMovementState>();
        attackState = GetComponentInChildren<EnemyAttackState>();
        deadState = GetComponentInChildren<EnemyDeadState>();

        myColl = GetComponent<Collider>();
        myRigid = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        myColl.enabled = true;
        myRigid.useGravity = true;
        hpBarCanvas.SetActive(true);

        curHP = data.hp;
        hpBar.fillAmount = curHP / data.hp;
        
        target = Player.Instance?.transform;
        curDelay = 0;
        dead = false;

        NextState(movementState);
    }

    public void NextState(EnemyState state)
    {
        curEnemyState = state;
        curEnemyState.EnterState();
    }

    // Start is called before the first frame update
    void Start()
    {
        target = Player.Instance?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        curEnemyState?.UpdateState();

        for (int i = 0; i < debuffList.Count;)
        {
            DeBuff deb = debuffList[i];
            deb.keepTime -= Time.deltaTime;

            if (deb.keepTime < 0.0f)
            {
                switch (deb.type)
                {
                    case DeBuffType.Slow:
                        movementState.ChangeMoveSpeed(1 / deb.value);
                        foreach (Renderer ren in allRenderer)
                        {
                            ren.material.SetColor("_Color", Color.white);
                        }
                        break;
                    case DeBuffType.Burn:
                        foreach (Renderer ren in allRenderer)
                        {
                            ren.material.SetColor("_Color", Color.white);
                        }
                        break;
                    case DeBuffType.Bleeding:
                        foreach (Renderer ren in allRenderer)
                        {
                            ren.material.SetColor("_Color", Color.white);
                        }
                        break;
                }

                debuffList.RemoveAt(i);
                continue;
            }

            switch (deb.type)
            {
                case DeBuffType.Slow:

                    break;
                case DeBuffType.Burn:
                    deb.curDamageTime -= Time.deltaTime;
                    if (deb.curDamageTime <= 0.0f)
                    {
                        OnDamage(deb.value);
                        deb.curDamageTime = deb.maxDamageTime;
                    }
                    break;
                case DeBuffType.Bleeding:
                    deb.curDamageTime -= Time.deltaTime;
                    if (deb.curDamageTime <= 0.0f)
                    {
                        OnDamage(deb.value);
                        deb.curDamageTime = deb.maxDamageTime;
                    }
                    break;
            }

            debuffList[i] = deb;
            i++;
        }
    }

    public void AddDeBuff(DeBuff deb)
    {
        for (int i = 0; i < debuffList.Count; i++)
        {
            if (debuffList[i].type == deb.type)
            {
                DeBuff temp = debuffList[i];
                temp.keepTime = deb.keepTime;
                debuffList[i] = temp;
                return;
            }
        }

        switch (deb.type)
        {
            case DeBuffType.Slow:
                movementState.ChangeMoveSpeed(deb.value);
                foreach (Renderer ren in allRenderer)
                {
                    movementState.ChangeMoveSpeed(deb.value);
                    ren.material.SetColor("_Color", Color.blue);
                }
                break;

            case DeBuffType.Burn:
                foreach (Renderer ren in allRenderer)
                {
                    ren.material.SetColor("_Color", Color.red);
                }
                break;

            case DeBuffType.Bleeding:
                foreach (Renderer ren in allRenderer)
                {
                    ren.material.SetColor("_Color", Color.magenta);
                }
                break;
        }
        debuffList.Add(deb);
    }

    public void OnDamage(float dmg)
    {
        if(curHP > 0)
            curHP -= dmg;

        hpBar.fillAmount = curHP / data.hp;

        GameObject temp = Instantiate(damagedEffect);
        temp.transform.position = transform.position;

        if (curHP <= 0 && !dead)
        {
            curEnemyState.myAnim.SetTrigger("IsDying");

            if (curEnemyState == movementState)
                movementState.agent.enabled = false;

            NextState(deadState);
            dead = true;
        }
    }

    public bool IsLive
    {
        get => true;
    }

    public virtual void OnAttack()
    {
        //IBattle ib = Player.Instance.GetComponent<IBattle>();
        //ib?.OnDamage(attackDamage);
        //enemyMovement.myAnim.SetTrigger("Attacking");
    }
    
    public void Dead()
    {
        curEnemyState.myAnim.SetBool("DyingEnd", true);
    }

    public void SaveMySpawn(EnemyRespawn myspawn)
    {
        mySpawn = myspawn;
    }

    public void AddExp(int exp)
    {

    }
    public void AddGold(int gold)
    {

    }
}
