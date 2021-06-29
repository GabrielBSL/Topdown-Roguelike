using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropGenerator : MonoBehaviour
{
    [System.Serializable]
    private struct DropItem
    {
        public GameObject dropObj;
        public float dropWeight;
    }

    [SerializeField] [Range(0f, 1f)] private float dropChance;
    [SerializeField] private DropItem[] items;


    private void OnDestroy()
    {
        if (Random.Range(0f, 1f) < dropChance) return;

        float allWeights = 0;

        for (int i = 0; i < items.Length; i++)
        {
            allWeights += items[i].dropWeight;
        }

        float randomWeight = Random.Range(0, allWeights);

        for (int i = 0; i < items.Length; i++)
        {
            if(randomWeight - items[i].dropWeight <= 0)
            {
                Instantiate(items[i].dropObj, 
                    transform.position, 
                    Quaternion.identity);
                return;
            }

            randomWeight -= items[i].dropWeight;
        }
    }
}
