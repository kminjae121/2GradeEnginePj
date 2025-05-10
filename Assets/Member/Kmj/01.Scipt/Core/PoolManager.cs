using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    private List<GameObject>[] pools;

    private void Awake()
    {
        pools = List<GameObject>[enemyPrefab.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int idx)
    {
        GameObject select = null;

        foreach (GameObject item in pools[idx])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {
            select = Instantiate(enemyPrefab[idx], transform); 
            pools[idx].Add(select);

        }
        return select;
    }
}
