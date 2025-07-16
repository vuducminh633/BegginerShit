using UnityEngine;
using System.Collections;

public class BombLogic : MonoBehaviour
{
    public ParticleSystem bombVFX;
    public AudioSource audioSource;
    public AudioClip clip;
    [SerializeField] private float BombRange = 2f;

    public void TriggerBomb()
    {
        StartCoroutine(BombSequence());
    }

    IEnumerator BombSequence()
    {
        ParticleSystem vfx = Instantiate(bombVFX, transform.position, Quaternion.identity);
        vfx.Play();
        audioSource.PlayOneShot(clip);

        PushEnemies();

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
      
    }

    void PushEnemies()
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, BombRange);
        foreach (Collider obj in hit)
        {
            if (obj.CompareTag("Enemy"))
            {
                Rigidbody rb = obj.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 pushDir = (obj.transform.position - transform.position).normalized;
                    rb.AddForce(pushDir * 150, ForceMode.Impulse);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, BombRange);
    }
}
