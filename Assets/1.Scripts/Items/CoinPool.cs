using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    private static CoinPool instance;
    public static CoinPool Instance => instance;

    public Coin[] coins;
    public Queue<Coin> coinQueue = new Queue<Coin>();
    public Coin coinPrefab;

    private void Awake()
    {
        instance = this;

        coins = GetComponentsInChildren<Coin>(true);

        for (int i = 0; i < coins.Length; i++)
        {
            coinQueue.Enqueue(coins[i]);
        }
    }

    public Coin DequeueCoin(int gold, Transform trans)
    {
        Coin myCoin;

        if (coinQueue.Count == 0)
        {
            myCoin = Instantiate(coinPrefab);
        }
        else
        {
            myCoin = coinQueue.Dequeue();
        }

        myCoin.transform.SetParent(null);
        myCoin.Gold(gold);
        myCoin.transform.position = trans.position;
        myCoin.gameObject.SetActive(true);

        return myCoin;
    }

    public void EnqueueCoin(Coin coin)
    {
        coin.transform.SetParent(transform);
        coin.gameObject.SetActive(false);
        coinQueue.Enqueue(coin);
    }
}
