using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float health;
    [SerializeField] protected int damage;
    
    [SerializeField] protected float attackDelay;
    protected float attackDelayTimer;

    protected Transform Player;
    protected Rigidbody2D m_rb;
    protected Animator m_Animation;
    
    private float m_CurrentHealth;
    
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        m_CurrentHealth = health;
        m_rb = GetComponent<Rigidbody2D>();
        m_Animation = GetComponent<Animator>();
    }

    protected virtual void Start()
    {
        Player = FindObjectOfType<PlayerController>().transform;
    }

    public virtual void ReceiveHit(float damage)
    {
        m_CurrentHealth -= damage;

        if (m_CurrentHealth <= 0)
        {
            StartDeath();
        }
    }

    protected virtual void StartDeath()
    {
        Destroy(gameObject);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if(!other.transform.CompareTag("Player")) return;
        
        other.transform.GetComponent<PlayerHealth>().ReceiveHit(damage, transform.position);
    }
}
