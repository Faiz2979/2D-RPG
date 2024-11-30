using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public Rigidbody2D rb;
    public int health=10,maxHealth=10;
    Transform Target;
    public Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update(){
        if(Target){
            Vector3 direction = (Target.transform.position - transform.position).normalized;
            moveDirection = new Vector2(direction.x,direction.y);
        }
    }
    void FixedUpdate()
    {
        if(Target){
            rb.velocity = new Vector2(moveDirection.x , moveDirection.y) * moveSpeed;
        }
    }

    public void TakeDamage(int damage){
        health -= damage;
        if(health <= 0){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }
}
