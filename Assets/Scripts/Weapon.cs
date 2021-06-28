using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject shootPrefab;

    [SerializeField] private float weaponFireRate;
    private float m_WeaponFireRateTimer;

    [SerializeField] private float projectileSpeed;
    [SerializeField] private float projectileDamage;
    [SerializeField] private float projectilePoolSize;

    [SerializeField] private Transform cameraFocusPoint;

    private InputMap m_InputMap;
    private Queue<GameObject> projectilePool;
    private bool m_Shooting;

    private Vector2 m_LookingDirection;
    private Rigidbody2D playerRigidbody;
    
    // Start is called before the first frame update
    void Awake()
    {
        m_InputMap = new InputMap();

        m_InputMap.Player.Shoot.performed += ctx => ActivateShooting(ctx);
        m_InputMap.Player.Shoot.canceled += ctx => ActivateShooting(ctx);

        m_LookingDirection = Vector2.left;
        playerRigidbody = transform.parent.GetComponent<Rigidbody2D>();

        projectilePool = new Queue<GameObject>();

        for (int i = 0; i < projectilePoolSize; i++)
        {
            GameObject newProjectile = Instantiate(shootPrefab, spawnPoint.position, transform.rotation);
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        
            projectileComponent.speed = projectileSpeed;
            projectileComponent.damage = projectileDamage;
            
            projectilePool.Enqueue(newProjectile);
            newProjectile.SetActive(false);
        }
    }

    private void OnEnable() => m_InputMap.Enable();
    private void OnDisable() => m_InputMap.Disable();

    private void Update()
    {
        AjustRotation();
        Shoot();
    }

    private void AjustRotation()
    {
        Vector2 direction;
        GameObject closestEnemy = GetClosestEnemy();

        if(closestEnemy != null)
        {
            direction = closestEnemy.transform.position - transform.position;
            cameraFocusPoint.localPosition = direction / 2;
        }

        else
        {
            if(playerRigidbody.velocity != Vector2.zero) 
                m_LookingDirection = playerRigidbody.velocity;

            direction = m_LookingDirection;
            cameraFocusPoint.localPosition = Vector3.zero;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.rotation = rotation;
    }

    private GameObject GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0) return null;

        GameObject closestEnemy = enemies[0];
        float closestDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);

        foreach (var enemy in enemies)
        {
            if ((transform.position - enemy.transform.position).sqrMagnitude > Mathf.Pow(closestDistance, 2)) 
                continue;

            closestEnemy = enemy;
            closestDistance = Vector3.Distance(transform.position, closestEnemy.transform.position);
        }

        return closestEnemy;
    }

    private void Shoot()
    {
        if (Time.time < m_WeaponFireRateTimer || !m_Shooting) return;
        m_WeaponFireRateTimer = Time.time + weaponFireRate;

        GameObject projectile = projectilePool.Dequeue();

        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = transform.rotation;

        projectile.SetActive(true);
        projectile.GetComponent<Projectile>().Redirect();
        projectilePool.Enqueue(projectile);
    }

    private void ActivateShooting(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) m_Shooting = true;
        else m_Shooting = false;
    }
}
