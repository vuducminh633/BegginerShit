using UnityEngine;

public class DeleteObjBox : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
        }
    }
}
