using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    [SerializeField] private ShieldEffect shieldEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Approximate the hit point from the enemy toward the shield
            Vector3 hitPoint = GetComponent<Collider>().ClosestPoint(other.transform.position);

            if (shieldEffect != null)
            {
                shieldEffect.HitShield(hitPoint);
            }
        }
    }
}