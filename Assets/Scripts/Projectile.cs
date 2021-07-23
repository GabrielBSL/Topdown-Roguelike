using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed { get; set; }
    public float damage { get; set; }

    private Rigidbody2D rb;

    protected virtual void Awake() => rb = GetComponent<Rigidbody2D>();
    public void Redirect() => rb.velocity = transform.up * speed;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        print(other.name);

        if (other.CompareTag("Enemy"))
            other.GetComponent<Enemy>().ReceiveHit(damage);
        
        gameObject.SetActive(false);
    }
}
