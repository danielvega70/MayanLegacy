using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collidersword : MonoBehaviour
{
    [Header("Configuración de Daño")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private float knockbackForce = 5f;

    [Header("Efectos")]
    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private bool showHitEffect = true;
    [SerializeField] private AudioClip hitSound;

    [Header("Configuración Avanzada")]
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private bool canHitMultipleEnemies = true;

    private AudioSource audioSource;
    private bool canDealDamage = true;
    private HashSet<Collider> hitEnemies = new HashSet<Collider>();

    private void Start()
    {
        // Configurar el audio source si hay un sonido de golpe
        if (hitSound != null && !TryGetComponent(out audioSource))
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.clip = hitSound;
        }
    }

    private void OnEnable()
    {
        // Resetear la lista de enemigos golpeados cuando se activa la espada
        hitEnemies.Clear();
        canDealDamage = true;
    }

    private void OnTriggerEnter(Collider other) => HandleCollision(other);

    private void OnCollisionEnter(Collision collision)
    {
        HandleCollision(collision.collider);
    }

    private void HandleCollision(Collider other)
    {
        // Verificar si podemos hacer daño y si el objeto tiene el tag correcto
        if (!canDealDamage || !other.CompareTag(targetTag)) return;

        // Verificar si ya golpeamos a este enemigo
        if (!canHitMultipleEnemies && hitEnemies.Contains(other)) return;

        // Registrar el enemigo golpeado
        hitEnemies.Add(other);

        // Intentar obtener y dañar al enemigo
        if (other.TryGetComponent<EnemyBase>(out var enemy))
        {
            // Aplicar daño
            enemy.TakeDamage(damage);

            // Aplicar knockback si el enemigo tiene Rigidbody
            if (other.TryGetComponent<Rigidbody>(out var rb))
            {
                Vector3 direction = (other.transform.position - transform.position).normalized;
                rb.AddForce(direction * knockbackForce, ForceMode.Impulse);
            }

            // Mostrar efecto de golpe
            if (showHitEffect && hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, other.transform.position, Quaternion.identity);
            }

            // Reproducir sonido de golpe
            if (audioSource != null && hitSound != null)
            {
                audioSource.Play();
            }

            // Iniciar cooldown si está configurado
            if (attackCooldown > 0)
            {
                StartCoroutine(DamageCooldown());
            }
        }
    }

    private IEnumerator DamageCooldown()
    {
        canDealDamage = false;
        yield return new WaitForSeconds(attackCooldown);
        canDealDamage = true;
    }
}