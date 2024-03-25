using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    int myGold;
    bool eating;
    Collider myCol = null;
    public LayerMask myPlayer;

    float Speed = 20.0f;

    float dist;
    float time;
    Vector3 dir;

    public bool rotate;
    public float rotationSpeed;

    private void OnEnable()
    {
        rotate = true;
        rotationSpeed = 10;
        dist = 0.5f;
        time = 30.0f;
        dir = transform.up;
        eating = false;
    }

    private void Awake()
    {
        myCol = GetComponent<Collider>();
        if (myCol == null)
        {
            myCol = GetComponentInChildren<Collider>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (rotate)
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);

        if (time > 0)
        {
            time -= Time.deltaTime;

            if (dist <= 0)
            {
                dir = -dir;
                dist = 0.5f;
            }

            float delta = Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(dir * delta, Space.World);
        }
        else
        {
            CoinPool.Instance.EnqueueCoin(this);
        }
    }

    public void Gold(int gold)
    {
        myGold = gold;
    }

    private void OnTriggerStay(Collider other)
    {
        if ((myPlayer & 1 << other.gameObject.layer) != 0)
        {
            if (!eating)
            {
                IBattle ib = other.GetComponent<IBattle>();

                if (ib != null)
                {
                    GetGold(other.gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((myPlayer & 1 << other.gameObject.layer) != 0)
        {
            if (!eating)
            {
                IBattle ib = other.GetComponent<IBattle>();

                if (ib != null)
                {
                    GetGold(other.gameObject);
                }
            }
        }
    }

    public void GetGold(GameObject player)
    {
        eating = true;
        StartCoroutine(GettingGold(player));
    }

    IEnumerator GettingGold(GameObject player)
    {
        yield return new WaitForSeconds(1.0f);

        Vector3 dir = player.transform.position - transform.position;
        float dist = dir.magnitude;

        while (dist > 0)
        {
            dir = player.transform.position - transform.position;
            dist = dir.magnitude;

            float delta = Speed * Time.deltaTime;

            if (delta > dist)
            {
                delta = dist;
            }
            dist -= delta;

            transform.Translate(dir.normalized * delta, Space.World);

            yield return null;
        }

        CoinPool.Instance.EnqueueCoin(this);

        player.GetComponent<Player>().AddGold(myGold);
    }
}
