using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
   


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BombLogic logic = GetComponentInParent<BombLogic>();
            PlayerController player = other.GetComponent<PlayerController>();
            player.currentHealth--;
            if (logic != null)
            {
                logic.TriggerBomb();
            }

            Destroy(gameObject);


        }
    }
    
}


