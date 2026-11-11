using UnityEngine;                      

public class PlayerHealthManager : MonoBehaviour // PlayerHealthManager != PlayerHealthmanager (I spent too long trying to fix this typo.......)
{
    [SerializeField] private float maxHealth = 3f; // Maximum Player Health

    private float currentHealth; // Current Player Health

    [SerializeField] private Healthbar healthbar; // Healthbar Reference

    [SerializeField] private GameObject gameOverPanel; // Game Over Panel

    private bool gameOver = false; // Game Over State

    // Initialise Player Health Started At 3 Which  Feels Good, you can see this above
    void Start()
    {
        Time.timeScale = 1f;
        Debug.Log("Player started with health: " + currentHealth); // adding for test purposes
        currentHealth = maxHealth;

        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        gameOverPanel.SetActive(false); // Hide Game Over Screen
    }

    // Take Health Away From Player
    public void TakeDamage(float damage)
    {
        if (gameOver) return;

        currentHealth -= damage;

        // Prevent Health Going Below Zero (no negative health)
        currentHealth = Mathf.Max(currentHealth, 0);

        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        // Check If Player Has No Health
        if (currentHealth <= 0)
        {
            gameOver = true;

            gameOverPanel.SetActive(true); // Show Game Over Screen

            // Pause Game (no health, no game)
            Time.timeScale = 0f;
        }
    }

    // Test Player Damage
    void Update()
    {
        if (gameOver)
        {
            // Restart Game With R
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartPlayer();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.H)) // TEST INPUT
        {
            TakeDamage(1f);
        }
    }

    // Restart Player
    void RestartPlayer() // Extra addition to act as a game over (not sure if this is fullt right)
    {
        currentHealth = maxHealth;

        gameOver = false;

        healthbar.UpdateHealthBar(maxHealth, currentHealth);

        gameOverPanel.SetActive(false); // Hide Game Over Screen

        // Resume Game
        Time.timeScale = 1f;
    }
}
