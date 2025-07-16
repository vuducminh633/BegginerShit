using System;
using System.Transactions;
using UnityEngine;

public class EnemyAnimal : MonoBehaviour
{
    public float speed = 10;
    // Update is called once per frame

    public float rayDistance = 2f; 
    public float knockbackForce = 10f;
    private bool isKnockedBack = false;

    int count = 0;
    private Rigidbody rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        if (!isKnockedBack)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);

            // Raycast forward to detect the player
            Ray ray = new Ray(transform.position, transform.forward);
        
            if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    knockbackForce1();
                }
            }
        }

    }

    private void knockbackForce1()
    {
        isKnockedBack = true;

       
        rb.linearVelocity = Vector3.zero;

        
        Vector3 knockbackDirection = (-transform.forward + Vector3.up).normalized;
        rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
    }

    public void RegisterHit()
    {
        count++;
        if (count >= 2)
        {
            Destroy(gameObject);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + new Vector3(0,1f,0),rayDistance *Vector3.forward);
    }
}
