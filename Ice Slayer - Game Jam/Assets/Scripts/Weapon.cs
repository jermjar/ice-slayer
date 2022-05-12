using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;
    public Animator animator;
    public SnowBar snowBar;

    public float currentSnow;
    public float maxSnow = 3f;

    // Start is called before the first frame update
    void Awake()
    {
        currentSnow = 0f;
        snowBar.SetSnow(currentSnow);
    }

    // Update is called once per frame
    void Update()
    {
        currentSnow = Mathf.Clamp(currentSnow, 0, maxSnow);
        if (Input.GetButtonDown("Fire1") && (currentSnow >= maxSnow))
        {
            Shoot();
            animator.SetTrigger("Throw");
            currentSnow = 0f;
            snowBar.SetSnow(currentSnow);
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            currentSnow += Time.deltaTime;
            snowBar.SetSnow(currentSnow);
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            currentSnow += Time.deltaTime;
            snowBar.SetSnow(currentSnow);
        }
    }
}
