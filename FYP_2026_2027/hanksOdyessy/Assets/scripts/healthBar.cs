
using UnityEngine;
using UnityEngine.UI;                    

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Image healthBar;  // Healthbar Image
    [SerializeField] private float reduceSpeed = 2f; // Healthbar Reduction Speed

    private float target = 1f;

    // Update Healthbar Amount
    public void UpdateHealthBar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }

    // give a feel of polish with the health bar decreasing
    void Update()
    {
            healthBar.fillAmount = Mathf.MoveTowards(
            healthBar.fillAmount,
            target,
            reduceSpeed * Time.unscaledDeltaTime
        );
    }
}
