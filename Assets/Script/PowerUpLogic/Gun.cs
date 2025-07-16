using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject gunObj;
    [SerializeField] private GameObject buletPrefab;
    [SerializeField] private Transform firePoint;
    public float fireSpeed = 50;
    public float activeDuration = 5f;
    public float fireInterval = 0.25f;

    private float elapsedTime = 0f;
    private float fireTimer = 0f;
    private bool isActive = false;

    internal void ActiveGun()
    {
        gunObj.SetActive(true);
        elapsedTime = 0f;
        fireTimer = 0f;
        isActive = true;
    }

    private void Update()
    {
        if (!isActive) return;

        elapsedTime += Time.deltaTime;
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireInterval)
        {
            FireBullet();
            fireTimer = 0f;
        }

        if (elapsedTime >= activeDuration)
        {
            DeActivateGun();
        }

    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(buletPrefab, firePoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.linearVelocity = -firePoint.forward * fireSpeed;  // x, y is firepoint position and firepoint.z is negative
        Destroy(bullet, 5f);
    }
    private void DeActivateGun()
    {
        isActive = false;
        gunObj.SetActive(false);
    }
}
