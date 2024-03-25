using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour, IBattle
{
    private static Player instance;
    public static Player Instance => instance;

    public float moveSpeed;
    public Rigidbody rg;
    public float curHP;
    public float maxHP;

    public Transform bodyTr;
    public LayerMask myEnemy;

    [SerializeField] Weapon[] weapons;
    public Weapon curWeapon;

    public float meleeDamage;

    public int myLevel;
    public int myExp;

    public LvExpData lvexpData;

    public Joystick moveJoystick;
    public Joystick rotJoystick;

    Animator myAnim;

    public int myGold;

    public GameObject damagedEffect;
    public GameObject levelupEffect;

    public Image hpBar;
    public GameObject hpBarCanvas;

    [SerializeField] List<DeBuff> debuffList = new List<DeBuff>();

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
        instance = this;

        rg = GetComponent<Rigidbody>();
        curHP = maxHP = 100;
        moveSpeed = 7;
        myLevel = 1;
        myExp = 0;

        bodyTr = transform.Find("P_Jungle_Charc");

        moveJoystick = GameObject.Find("moveJoystick")?.GetComponent<Joystick>();
        rotJoystick = GameObject.Find("rotJoystick")?.GetComponent<Joystick>();

        if (moveJoystick == null || rotJoystick == null)
            hpBarCanvas.SetActive(false);
        else
        {
            hpBarCanvas.SetActive(true);
            hpBar.fillAmount = curHP / maxHP;
        }

        myAnim = bodyTr.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        EquipItem(ItemType.weapon, InventoryManager.Instance.myWeapon);

        //myGold = InventoryManager.Instance.myGold;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveJoystick == null || rotJoystick == null) return;

        //if(Input.GetKey(KeyCode.W))
        //{
        //    //transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        //    MoveTo(Vector3.forward);
        //}

        if (!BuildingManager.Instance.BuildState)
            OnAttack();

        if (!myAnim.GetBool("Dying"))
        {
            if (moveJoystick.Direction.magnitude > 0)
            {
                Vector3 dir = new Vector3(moveJoystick.Direction.x, 0, moveJoystick.Direction.y);

                MoveTo(dir);

                myAnim.SetBool("IsMoving", true);
            }
            else
            {
                rg.velocity = Vector3.zero;
                myAnim.SetBool("IsMoving", false);
            }

            if (rotJoystick.Direction.magnitude > 0)
            {
                Vector3 dir = new Vector3(rotJoystick.Direction.x, 0, rotJoystick.Direction.y);

                RotTo(dir);
            }
        }

        for (int i = 0; i < debuffList.Count;)
        {
            DeBuff deb = debuffList[i];
            deb.keepTime -= Time.deltaTime;

            if (deb.keepTime < 0.0f)
            {
                switch (deb.type)
                {
                    case DeBuffType.Slow:

                        break;
                    case DeBuffType.Burn:

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

    public void MoveTo(Vector3 dir)
    {
        rg.velocity = dir * moveSpeed;
    }

    public void RotTo(Vector3 dir)
    {
        bodyTr.forward = dir.normalized;
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

                break;

            case DeBuffType.Burn:

                break;

            case DeBuffType.Bleeding:
                foreach (Renderer ren in allRenderer)
                {
                    ren.material.SetColor("_Color", Color.red);
                }
                break;
        }
        debuffList.Add(deb);
    }

    public void OnDamage(float dmg)
    {
        curHP -= dmg;

        hpBar.fillAmount = curHP / maxHP;

        GameObject temp = Instantiate(damagedEffect);
        temp.transform.position = transform.position;

        if (curHP <= 0 && !myAnim.GetBool("Dying"))
        {
            myAnim.SetTrigger("IsDying");
            rg.velocity = Vector3.zero;
        }
    }

    public bool IsLive
    {
        get
        {
            if (curHP <= 0.0f)
            {
                return false;
            }
            return true;
        }
    }

    //public void OnMeleeAttack()
    //{
    //    if(myAnim.GetBool("IsAttacking"))
    //        return;

    //    if (curWeapon.stat.Damage == 0)
    //    {
    //        curWeapon.Attack();
    //        myAnim.SetTrigger("MK_Attacking");
    //    }
    //}

    //public void OnRangedAttack()
    //{
    //    if (myAnim.GetBool("IsAttacking") || curWeapon.stat.Damage == 0 || curWeapon.IsAttacking)
    //        return;

    //    curWeapon.Attack();
    //    myAnim.SetTrigger("R_Attacking");
    //}

    public void OnAttack()
    {
        if (myAnim.GetBool("IsAttacking") || curWeapon.IsAttacking) return;

        if (curWeapon.stat.meleeDamage > 0)
        {
            myAnim.SetTrigger("MK_Attacking");
        }
        else
        {
            curWeapon.Attack();
            myAnim.SetTrigger("R_Attacking");
        }
    }

    public void MeleeAttack()
    {
        curWeapon.Attack();
    }

    //IEnumerator Attacking()
    //{
    //    while (!myAnim.GetBool("IsAttacking") && !curWeapon.IsAttacking)
    //    {
    //        if (curWeapon.stat.Damage == 0)
    //        {
    //            curWeapon.Attack();
    //            myAnim.SetTrigger("MK_Attacking");
    //        }
    //        else
    //        {
    //            curWeapon.Attack();
    //            myAnim.SetTrigger("R_Attacking");
    //        }

    //        yield return null;
    //    }
    //}

    public void EquipItem(ItemType type, string equipName)
    {
        switch(type)
        {
            case ItemType.weapon:
                if (curWeapon != null)
                    curWeapon.gameObject.SetActive(false);

                for (int i = 0; i < weapons.Length; i++)
                {
                    if (weapons[i].stat.weaponName.Equals(equipName))
                    {
                        curWeapon = weapons[i];
                        curWeapon.gameObject.SetActive(true);
                        InventoryManager.Instance.myWeapon = equipName;
                        meleeDamage = curWeapon.stat.meleeDamage;
                        break;
                    }
                }
                break;
        }

        if (curWeapon != null)
            curWeapon.IsAttacking = false;
        if (myAnim != null)
            myAnim.SetBool("IsAttacking", false);
    }

    public void falseJoystick()
    {
        moveJoystick.gameObject.SetActive(false);
        rotJoystick.gameObject.SetActive(false);
    }

    public void trueJoystick()
    {
        moveJoystick.gameObject.SetActive(true);
        rotJoystick.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        myAnim.speed = 0.0f;
        Time.timeScale = 0.0f;
    }
    
    public void AddExp(int exp)
    {
        myExp += exp;

        if (myLevel >= lvexpData.LvExpDatas.Length - 1)
        {
            return;
        }

        if (myExp >= lvexpData.LvExpDatas[myLevel].needExp)
        {
            if (myLevel == lvexpData.LvExpDatas.Length - 1)
            {
                return;
            }

            int remain = myExp - lvexpData.LvExpDatas[myLevel].needExp;
            myExp = 0;
            myExp += remain;

            LevelUp();
        }

        IGUIManager.Instance.myExp.text = myExp.ToString();
        IGUIManager.Instance.needExp.text = lvexpData.LvExpDatas[myLevel].needExp.ToString();
    }

    public void AddGold(int gold)
    {
        myGold += gold;
        IGUIManager.Instance.coin.text = myGold.ToString();
    }

    public void UseGold(int use)
    {
        myGold -= use;
        IGUIManager.Instance.coin.text = myGold.ToString();
    }
    
    public bool CheckGold(int use)
    {
        if (myGold < use)
            return false;

        return true;
    }

    public void LevelUp()
    {
        myLevel++;

        InventoryManager.Instance.AddItems(EquipmentManager.Instance.weaponData.weaponStats[myLevel - 1].weaponName, ItemType.weapon);

        EquipItem(ItemType.weapon, EquipmentManager.Instance.weaponData.weaponStats[myLevel - 1].weaponName);

        GameObject temp = Instantiate(levelupEffect);
        temp.transform.position = transform.position;

        curHP = maxHP;
        hpBar.fillAmount = curHP / maxHP;
    }
}
