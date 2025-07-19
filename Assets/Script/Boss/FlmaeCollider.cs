using UnityEngine;

public class FlmaeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            
            // Use the TakeDamage method which respects invincibility
            player.TakeDamage();

            Destroy(gameObject); 
        }
    }
}
