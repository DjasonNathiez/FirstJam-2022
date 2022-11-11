using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
    public LevelManager _levelManager;
    public CoreManager coreManager;

    public int experienceAmountValue;
    
    public int currentHealth;

    public int attackDamage;
    public int attackSpeed;
    public float attackRange;
    public bool attackOnCd;

    public float attackCdTime;
    private float atkCdTimer;
    
    public float speed;
    public float stopDistance;
    
    private float distToPlayers;
    
    private Vector3 direction;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        coreManager = FindObjectOfType<CoreManager>();
    }

    private void Update()
    {
        //Set the direction of the enemy, looking to reach the player here
        distToPlayers = Vector3.Distance(transform.position, _levelManager.core.transform.position);

        //Move the enemy in the direction
        if (distToPlayers > stopDistance)
        {
            transform.position += transform.right * (speed * Time.deltaTime);
        }

        direction = _levelManager.core.transform.position - transform.position;
        
        //Set the look of the enemy (change between up & right, about the sprite)
        transform.right = direction;

        if (distToPlayers <= attackRange && !attackOnCd)
        {
            coreManager.TakeDamage(attackDamage);
            attackOnCd = true;
            atkCdTimer = attackCdTime;
        }

        if (attackOnCd)
        {
            atkCdTimer -= Time.deltaTime;

            if (atkCdTimer <= 0)
            {
                attackOnCd = false;
            }
        }
    }
    
    
    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            coreManager.GetExperience(experienceAmountValue);
            Destroy(gameObject);
        }
    }
}
