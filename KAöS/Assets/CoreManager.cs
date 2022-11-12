using System;
using UnityEngine;

public class CoreManager : MonoBehaviour,IDamageable
{
    public static CoreManager Instance;
    
    private Rigidbody2D rb;
    
    public int currentLife;
    public int currentLevel = 1;
    public float experience;
    public AnimationCurve expToLevelUp = AnimationCurve.Linear(0,100,100,1000);

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))GetExperience(90);
    }

    public void GetExperience(int amount)
    {
        experience += amount;
        if (experience > expToLevelUp.Evaluate(currentLevel + 1))
        {
           UpgradePanel.Instance.gameObject.SetActive(true);
           experience = 0;
        }
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
