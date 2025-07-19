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

    // Invincibility/Grace Period System
    [Header("Invincibility Settings")]
    public float invincibilityDuration = 2f; // Grace period duration in seconds
    public float blinkInterval = 0.1f; // How fast the player blinks during invincibility
    private bool isInvincible = false;
    private Renderer[] playerRenderers; // Array for all renderers in 3D character
    private Collider[] playerColliders; // Array for all colliders in player
    private Coroutine invincibilityCoroutine; // Track the invincibility coroutine
    private Coroutine blinkCoroutine; // Track the blink coroutine

    // Public method to check if player is currently invincible
    public bool IsInvincible()
    {
        return isInvincible;
    }

    // Public method to take damage - other scripts should use this instead of directly modifying health
    public void TakeDamage(int damage = 1)
    {
        if (isInvincible || isSheild)
        {
            if (isSheild)
            {
                audioSource.PlayOneShot(ShieldhitFSX);
            }
            return;
        }

        audioSource.PlayOneShot(hitFSX);
        currentHealth -= damage;
        StartCoroutine(shake.Shaking());
        
        // Start invincibility period
        StartInvincibility();
    }

    // Separate method to start invincibility to avoid conflicts
    private void StartInvincibility()
    {
        // Stop any existing invincibility to avoid overlapping
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
        }
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
        }
        
        invincibilityCoroutine = StartCoroutine(InvincibilityPeriod());
    }

    // Public method to stop invincibility (for external use if needed)
    public void StopInvincibility()
    {
        if (invincibilityCoroutine != null)
        {
            StopCoroutine(invincibilityCoroutine);
            invincibilityCoroutine = null;
        }
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        
        isInvincible = false;
        SetRenderersVisible(true);
        SetCollidersEnabled(true);
    }

    //CamShake
    public CamShake shake;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        
        // Get all renderers in the player object and its children (for 3D characters)
        playerRenderers = GetComponentsInChildren<Renderer>();
        
        // Filter out shield renderers to avoid affecting shield visibility
        var filteredRenderers = new System.Collections.Generic.List<Renderer>();
        foreach (var renderer in playerRenderers)
        {
            // Skip renderers that belong to shield objects
            if (renderer.GetComponent<ShieldEffect>() == null)
            {
                filteredRenderers.Add(renderer);
            }
        }
        playerRenderers = filteredRenderers.ToArray();
        
        // Get all colliders in the player object and its children
        playerColliders = GetComponentsInChildren<Collider>();
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
            // Use the TakeDamage method which handles invincibility checking
            TakeDamage();

            if (!isSheild && !isInvincible) 
            {
                Vector3 pushDir = -(other.transform.position - rb.transform.position).normalized;
                rb.AddForce(pushDir * pushForce, ForceMode.Impulse);
                StartCoroutine(Reset());
            }

        }
    }

    // Helper method to set visibility of all player renderers
    private void SetRenderersVisible(bool visible)
    {
        if (playerRenderers != null)
        {
            foreach (var renderer in playerRenderers)
            {
                if (renderer != null)
                {
                    renderer.enabled = visible;
                }
            }
        }
    }

    // Helper method to enable/disable all player colliders
    private void SetCollidersEnabled(bool enabled)
    {
        if (playerColliders != null)
        {
            foreach (var collider in playerColliders)
            {
                if (collider != null)
                {
                    // Skip shield colliders to maintain shield functionality
                    if (collider.GetComponent<ShieldEffect>() == null)
                    {
                        collider.enabled = enabled;
                    }
                }
            }
        }
    }

    IEnumerator InvincibilityPeriod()
    {
        isInvincible = true;
        
        // Disable colliders so player can pass through enemies
        SetCollidersEnabled(false);
        
        // Start blinking effect
        blinkCoroutine = StartCoroutine(BlinkEffect());
        
        // Wait for invincibility duration
        yield return new WaitForSeconds(invincibilityDuration);
        
        // End invincibility
        isInvincible = false;
        
        // Stop blinking
        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }
        
        // Ensure player is visible when invincibility ends
        SetRenderersVisible(true);
        
        // Re-enable colliders
        SetCollidersEnabled(true);
        
        // Clear the coroutine reference
        invincibilityCoroutine = null;
    }

    IEnumerator BlinkEffect()
    {
        while (isInvincible)
        {
            SetRenderersVisible(false);
            yield return new WaitForSeconds(blinkInterval);
            
            if (isInvincible) // Check again in case invincibility ended during wait
            {
                SetRenderersVisible(true);
                yield return new WaitForSeconds(blinkInterval);
            }
        }
        
        // Ensure player is visible when blinking stops
        SetRenderersVisible(true);
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
