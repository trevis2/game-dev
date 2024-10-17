using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 150;
    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance; } }

    [SerializeField] TextMeshProUGUI displayBalance;
    [SerializeField] GameObject gameOverPanel;

    private void Awake()
    {
        currentBalance = startingBalance;
        gameOverPanel.SetActive(false);
        UpdateGoldDisplay(currentBalance);
    }
    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount);
        UpdateGoldDisplay(currentBalance);
    }

    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount);
        UpdateGoldDisplay(currentBalance);
        if (currentBalance < 0)
        {
            gameOverPanel.SetActive(true);
            Invoke("ReloadScene", 2f);
        }
    }

    void UpdateGoldDisplay(int currentBalance)
    {
        displayBalance.text = "GOLD: " + currentBalance;
    }

    void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}

