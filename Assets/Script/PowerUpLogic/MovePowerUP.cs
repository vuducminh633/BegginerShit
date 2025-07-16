using UnityEngine;

public class MovePowerUP : MonoBehaviour
{
    public float rotationSpeed = 50;
    public float speed = 10;
     private Vector3 moveDirection;


    void Start()
    {
        moveDirection = -transform.forward; // lock in the initial forward direction
    }

    void Update()
    {
        transform.Translate(moveDirection * Time.deltaTime * speed, Space.World);
        if (!CompareTag("MysteryBox"))
        {
            transform.rotation *= Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f);
        }
       
    }
}
