using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject iciclePrefab;
    private float spawnRangeX = 9f;
    private float startDelay = 0f;
    private float spawnInterval = 0.85f;

    private void Awake()
    {
        InvokeRepeating("SpawnIcicles", startDelay, spawnInterval);
    }

    private void SpawnIcicles()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-spawnRangeX, spawnRangeX), 6.5f);
        Instantiate(iciclePrefab, spawnPos, iciclePrefab.transform.rotation);
    }
}
