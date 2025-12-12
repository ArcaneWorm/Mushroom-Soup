using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }

     public Transform playerBody;

    // ---- Player Health ---- //
    public float currentHealth;
    public float maxHealth;

    public GameOverScreen gameOverScreen;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            DisplayGameOver();
        }
    }

    public void SetHealth(float amount)
    {
        currentHealth = amount;
    }

    public bool AtMaxHealth()
    {
        return currentHealth >= maxHealth;
    }

    public void DisplayGameOver()
    {
        gameOverScreen.Setup();
    }
}
