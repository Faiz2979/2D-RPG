using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    [Header("Attributes")]
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [Header("Movement")]
    [Serialize] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField]private float groundedLeeway;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
