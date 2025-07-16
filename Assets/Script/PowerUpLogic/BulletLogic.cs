using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAnimal>()?.RegisterHit();
            //Call post processing function
            Destroy(gameObject);
        }
    }
}
