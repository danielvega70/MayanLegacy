using UnityEngine;
using UnityEngine.Events;

public class EnemyBase : MonoBehaviour
{
    [Header("Estadísticas Básicas")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float defense = 0f;
    [SerializeField] private bool isInvulnerable = false;

    [Header("Colisiones y Daño")]
    [Tooltip("Collider para detectar proyectiles")]
    [SerializeField] private Collider projectileCollider;
    [Tooltip("Collider para detectar espadas")]
    [SerializeField] private Collider swordCollider;

    [Header("Configuración de Tags")]
    [SerializeField] private string projectileTag = "PlayerProjectile";
    [SerializeField] private string swordTag = "PlayerSword";

    [Header("Daño por Fuente")]
    [SerializeField] private float projectileDamage = 5f;
    [SerializeField] private float swordDamage = 10f;

    [Header("Configuración de Muerte")]
    [SerializeField] private GameObject deathEffectPrefab;
    [SerializeField] private float destroyDelay = 2f;
    [SerializeField] private bool disableColliderOnDeath = true;
    [SerializeField] private bool disableRendererOnDeath = true;

    [Header("Eventos")]
    public UnityEvent onDeath;
    public UnityEvent<float> onDamaged;
    public UnityEvent<float> onHealed;

    private float currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnEnable()
    {
        if (projectileCollider != null)
            SetupCollider(projectileCollider, projectileDamage, projectileTag);

        if (swordCollider != null)
            SetupCollider(swordCollider, swordDamage, swordTag);
    }

    private void SetupCollider(Collider col, float damage, string expectedTag)
    {
        EnemyDamageTrigger trigger = col.gameObject.GetComponent<EnemyDamageTrigger>();
        if (trigger == null)
            trigger = col.gameObject.AddComponent<EnemyDamageTrigger>();

        trigger.Configure(this, damage, expectedTag);
    }

    public void TakeDamage(float damage)
    {
        if (isDead || isInvulnerable) return;

        float finalDamage = Mathf.Max(0, damage - defense);
        currentHealth -= finalDamage;

        onDamaged?.Invoke(finalDamage);

        Debug.Log($"{gameObject.name} recibió {finalDamage} de daño. Vida restante: {currentHealth}");

        if (currentHealth <= 0)
            Die();
    }

    public void Heal(float amount)
    {
        if (isDead) return;

        float healAmount = Mathf.Min(maxHealth - currentHealth, amount);
        currentHealth += healAmount;

        onHealed?.Invoke(healAmount);

        Debug.Log($"{gameObject.name} curado por {healAmount}. Vida actual: {currentHealth}");
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        onDeath?.Invoke();
        Debug.Log($"{gameObject.name} ha muerto.");

        if (deathEffectPrefab != null)
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

        if (disableColliderOnDeath)
        {
            Collider[] colliders = GetComponentsInChildren<Collider>();
            foreach (var col in colliders)
                col.enabled = false;
        }

        if (disableRendererOnDeath)
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            foreach (var rend in renderers)
                rend.enabled = false;
        }

        Destroy(gameObject, destroyDelay);
    }

    // Clase interna para manejar daño específico
    private class EnemyDamageTrigger : MonoBehaviour
    {
        private EnemyBase enemy;
        private float damage;
        private string expectedTag;

        public void Configure(EnemyBase _enemy, float _damage, string _expectedTag)
        {
            enemy = _enemy;
            damage = _damage;
            expectedTag = _expectedTag;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (enemy == null) return;

            if (!string.IsNullOrEmpty(expectedTag) && other.CompareTag(expectedTag))
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
