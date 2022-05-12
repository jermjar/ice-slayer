using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icicle : MonoBehaviour
{
    public GameObject player;

    private void Awake()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Boss") || other.gameObject.CompareTag("HitBox"))
        {
            Destroy(gameObject);
        }
    }
}
