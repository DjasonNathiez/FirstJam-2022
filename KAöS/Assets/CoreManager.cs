using UnityEngine;

public class CoreManager : MonoBehaviour,IDamageable
{
    public static CoreManager Instance;
    
    private Rigidbody2D rb;
    
    public int currentLife;
    public float experience;
    public float coreToLevelUp = 100;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;

    }

    private void Start()
    {
        Debug.Log("Create");
        rb = GetComponent<Rigidbody2D>();
    }

    public void GetExperience(int amount)
    {
        experience += amount;
    }
    
    public void Impulse(Vector2 dir, float force)
    {
        rb.AddForce(dir*force);
    }

    public void TakeDamage(int amount)
    {
        currentLife -= amount;
    }
    
}
