using UnityEngine;

public class PlayerControllerFarmer : MonoBehaviour
{
    private Rigidbody rb;
    public float gravityModifier;
    public float jumpForce = 10f;
    private bool isGround = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGround = false;   
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        isGround = true;
       // InvokeRepeating
    }

}
