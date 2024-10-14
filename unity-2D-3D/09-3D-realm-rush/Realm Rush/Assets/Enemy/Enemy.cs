using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int goldReward = 25;
    [SerializeField] int goldPenalty = 25;

    [Tooltip("Valore aggiunto alla vita,speed e gold penalty nemica quando muore")]
    [SerializeField] int difficultyRamp = 1;
    public int DifficultyRamp { get { return difficultyRamp; } }

    Bank bank;
    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (bank == null) { return; }
        bank.Deposit(goldReward);
    }

    public void StealGold()
    {
        if (bank == null) { return; }
        bank.Withdraw(goldPenalty);
    }

    public void IncreaseDifficulty()
    {
        goldPenalty += difficultyRamp;
    }
}
