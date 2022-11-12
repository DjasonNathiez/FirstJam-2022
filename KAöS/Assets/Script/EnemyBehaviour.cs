using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IDamageable
{
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

    private void Update()
    {
        //Set the direction of the enemy, looking to reach the player here
        distToPlayers = Vector3.Distance(transform.position, CoreManager.Instance.transform.position);

        //Move the enemy in the direction
        if (distToPlayers > stopDistance)
        {
            transform.position += transform.right * (speed * Time.deltaTime);
        }

        direction = CoreManager.Instance.transform.position - transform.position;
        
        //Set the look of the enemy (change between up & right, about the sprite)
        transform.right = direction;

        if (distToPlayers <= attackRange && !attackOnCd)
        {
            CoreManager.Instance.TakeDamage(attackDamage);
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
            CoreManager.Instance.GetExperience(experienceAmountValue);
            Destroy(gameObject);
        }
    }
}
