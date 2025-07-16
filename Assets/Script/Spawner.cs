using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject[] animelPrefab;

    public float minDelay = 1f;
    public float maxDelay = 3f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(2f); // initial delay

        while (true)
        {
            Spawn();

            float delay = Random.Range(minDelay, maxDelay);
            yield return new WaitForSeconds(delay);
        }
    }

    void Spawn()
    {
        int i = Random.Range(0, animelPrefab.Length);
        GameObject enemy = Instantiate(animelPrefab[i], transform.position, animelPrefab[i].transform.rotation);
        Destroy(enemy, 20f);
    }
}
