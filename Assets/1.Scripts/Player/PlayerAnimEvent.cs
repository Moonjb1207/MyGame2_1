using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimEvent : MonoBehaviour
{
    public UnityEvent meleeAttacking = null;
    public UnityEvent gameOver = null;

    public void GameOver()
    {
        gameOver?.Invoke();
    }

    public void MeleeAttacking()
    {
        meleeAttacking?.Invoke();
    }
}
