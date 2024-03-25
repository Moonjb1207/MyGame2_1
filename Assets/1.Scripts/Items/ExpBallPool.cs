using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpBallPool : MonoBehaviour
{
    private static ExpBallPool instance;
    public static ExpBallPool Instance => instance;

    public ExpBall[] balls;
    public Queue<ExpBall> ballQueue = new Queue<ExpBall>();
    public ExpBall ballPrefab;

    private void Awake()
    {
        instance = this;

        balls = GetComponentsInChildren<ExpBall>(true);

        for (int i = 0; i < balls.Length; i++)
        {
            ballQueue.Enqueue(balls[i]);
        }
    }

    public ExpBall DequeueBall(int exp, Transform trans)
    {
        ExpBall myExpBall;

        if (ballQueue.Count == 0)
        {
            myExpBall = Instantiate(ballPrefab);
        }
        else
        {
            myExpBall = ballQueue.Dequeue();
        }

        myExpBall.transform.SetParent(null);
        myExpBall.Exp(exp);
        myExpBall.transform.position = trans.position;
        myExpBall.gameObject.SetActive(true);
        return myExpBall;
    }

    public void EnqueueBall(ExpBall ball)
    {
        ball.transform.SetParent(transform);
        ball.gameObject.SetActive(false);
        ballQueue.Enqueue(ball);
    }
}
