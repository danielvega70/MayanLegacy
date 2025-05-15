using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterhealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float damageCooldown = 0.5f;
    private float lastDamageTime = -999f;

    [Tooltip("Nombre del tag usado para identificar las partículas dañinas")]
    public string damagingTag = "SwordParticle";

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto tiene el tag correcto
        if (other.CompareTag(damagingTag))
        {
            if (Time.time - lastDamageTime >= damageCooldown)
            {
                TakeDamage(10);
                lastDamageTime = Time.time;
            }
        }
    }

    void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"[DAMAGE] -{amount} | Salud restante: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("[DEAD] El personaje ha muerto.");
        // Aquí puedes colocar animaciones, efectos, etc.
        gameObject.SetActive(false);
    }
}