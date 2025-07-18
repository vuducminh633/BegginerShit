using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    public GameObject boss;
    public GameObject Spawner;

    private void Update()
    {
        if (ScoreManager.instance.currentScore >= 10)
        {
           Spawner.SetActive(false);
            Instantiate(boss, transform.position, Quaternion.Euler(0, 180, 0));
            enabled = false;
        }
    }
}
