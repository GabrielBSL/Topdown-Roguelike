using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetectionEnemySpawner : MonoBehaviour
{
    [SerializeField] Transform enemyListObject;
    private List<GameObject> m_EnemiesList;

    private bool hasSpawned;

    private void Start()
    {
        m_EnemiesList = new List<GameObject>();

        foreach (Transform child in enemyListObject)
        {
            m_EnemiesList.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player") || hasSpawned) return;
        hasSpawned = true;

        ActivateEnemies();
    }

    private void ActivateEnemies()
    {
        foreach (GameObject enemy in m_EnemiesList)
        {
            enemy.SetActive(true);
        }
    }
}
