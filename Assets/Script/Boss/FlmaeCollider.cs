using UnityEngine;

public class FlmaeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            
            player.currentHealth--; 
            

            Destroy(gameObject); 
        }
    }
}
