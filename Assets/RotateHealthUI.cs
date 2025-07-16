using UnityEngine;

public class RotateHealthUI : MonoBehaviour
{
    public float speed = 20;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * speed);    
    }
}
