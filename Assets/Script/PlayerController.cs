using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 20;
    public float turnspeed =0;
    public float horizontalInput;
    private float forwardInput;
    public AudioClip hitFSX;
    public AudioClip ShieldhitFSX;
    public AudioSource audioSource;
    public PowerUpManager powerUpManager;


    int maxHealth = 6;
    public int currentHealth = 0;

    private Rigidbody rb;

    public float pushForce = 10;

    public bool isSheild;

    public GameObject bombPrefab; 

    private float sidewaysIdleTimer = 0f;
    private float checkInterval = 0.2f;
    private float sidewaysIdleThreshold = 3f;


    //CamShake
    public CamShake shake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        isSheild = false;
        StartCoroutine(CheckSidewaysIdle());
    }

    // Update is called once per frame
    private Vector3 inputDirection;
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
       

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            StartCoroutine(ResetLevel());
        }
    }
    private void FixedUpdate()
    {
        float x = horizontalInput * speed * Time.fixedDeltaTime;
        float z = forwardInput * speed * speedMultiplier * Time.fixedDeltaTime;

        Vector3 move = new Vector3(x, 0f, z);
        rb.MovePosition(rb.position + move);
    }

    public int powerUpCount = 0;
    public bool isPowerUp;

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            powerUpCount++;
            isPowerUp = false;

            IPowerUp powerUp = other.GetComponent<IPowerUp>();
            powerUp.Apply(this);
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {     
                if (!isSheild)
                {

                audioSource.PlayOneShot(hitFSX);
                currentHealth--;
                StartCoroutine(shake.Shaking());

                }
            else
            {
                audioSource.PlayOneShot(ShieldhitFSX);
            }

            if (!isSheild) 
            {
                Vector3 pushDir = -(other.transform.position - rb.transform.position).normalized;
                rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
                StartCoroutine(Reset());
            }

        }
    }

    IEnumerator Reset()
    {
       
        yield return new WaitForSeconds(0.15f);
        rb.linearVelocity = Vector3.zero;


    }

    IEnumerator ResetLevel()
    {
        GameObject camera = GameObject.Find("MainCamera");
        if (camera != null)
        {
           
            rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

           
            Vector3 pushDir = (camera.transform.position - rb.transform.position).normalized;

           
            rb.AddForce(pushDir * 2f * pushForce, ForceMode.Impulse);
        }

        
        yield return new WaitForSeconds(1f);

        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);


    }




    private float speedMultiplier = 1f;

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    IEnumerator CheckSidewaysIdle()
    {
        while (true)
        {
            if (Mathf.Approximately(horizontalInput, 0f))
            {
                sidewaysIdleTimer += checkInterval;

                if (sidewaysIdleTimer >= sidewaysIdleThreshold)
                {
                   
                    Vector3 spawnPos = transform.position + transform.forward * 30f;
                    Instantiate(bombPrefab, spawnPos, Quaternion.identity);
                    sidewaysIdleTimer = 0f; 
                }
            }
            else
            {
                sidewaysIdleTimer = 0f; 
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}
