using UnityEngine;
using UnityEngine.Events;

/*
 * A primitive health system that optionally also changes
 * the sprite of the given sprite renderer, based on health.
 * The index of the chosen sprite will correspond to the current health.
 */

public class HealthWithVisuals : MonoBehaviour
{
    public int maxHealth;
    int currentHealth;

    public bool destroyOnDeath = true;

    public UnityEvent OnDeath;

    [Space]
    [Header("Optional visual change on taking damage:")]
    [Space]

    public bool changeSpriteOnDamage = false;
    SpriteRenderer spriteRenderer;
    public Sprite[] healthSprites;


    private void Start()
    {
        currentHealth = maxHealth;

        if (changeSpriteOnDamage)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer == null)
            {
                Debug.LogError("HealthWithVisuals requires that the gameobject " +
                    "its attached to has a SpriteRenderer!");
                return;
            }

            if (healthSprites.Length <= maxHealth)
            {
                Debug.LogWarning($"Because you have {maxHealth} max health, you need {maxHealth}" +
                    $" health sprites.");
            }
        }
    }

    public void DealDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth == 0)
        {
            OnDeath.Invoke();

            if (destroyOnDeath)
            { 
                Destroy(this.gameObject);
                return;
            }
        }

        if (!changeSpriteOnDamage)
        {
            return;
        }
        if (healthSprites.Length <= currentHealth)
        {
            Debug.LogWarning($"Couldn't change sprite because there is no sprite " +
                $"in healthSprites with index {currentHealth}");
            return;
        }

        spriteRenderer.sprite = healthSprites[currentHealth];
    }
}
