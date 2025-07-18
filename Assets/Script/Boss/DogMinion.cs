using System.Collections;
using UnityEngine;

public class DogMinion : MonoBehaviour
{
    public ParticleSystem flameVFX;
    float minY = 138f, maxY = 206f;
    float waitRotationDuration = 3.5f;
    float flameDuration = 3f;

    public GameObject ColliderPreFab;

    private bool hasRotated = false;
    private float elapsedTime = 0f;
    public ParticleSystem smokeIndication;  public Transform spawnSmokePos;

    private void Update()
    {
        if (!hasRotated)
        {
            elapsedTime += Time.deltaTime;
        
            if (elapsedTime > waitRotationDuration)
            {
                RotateDog();
                elapsedTime = 0;
                hasRotated = true;
                StartCoroutine(StartFlame());
            }
            
        } 
      
    }

    IEnumerator StartFlame()
    {
        Instantiate(smokeIndication, spawnSmokePos.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Shootflame();

        
    }

    private void Shootflame()
    {
        Instantiate(flameVFX, spawnSmokePos.position, Quaternion.identity);
        hasRotated = false;
    }

    void RotateDog()
    {
        float targetY = Random.Range(minY, maxY);
        transform.rotation = Quaternion.Euler(0, targetY, 0);
    }


}
