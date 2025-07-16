using UnityEngine;
using System.Collections;


public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUpObj;
    private Vector3 spawnPoint;
    public GameObject mystryPowerUp;

    private int playerPowerUpCount = 0;

    public PlayerController player;

   


    private void Start()
    {
        StartCoroutine(SpawnPowerUpRoutine());
    }


    IEnumerator SpawnPowerUpRoutine()
    {
        while (true)
        {
            if (!player.isPowerUp)
            {
                yield return new WaitForSeconds(5f);
                if (player.powerUpCount >= 3)
                {
                    yield return new WaitForSeconds(3f);
                    Vector3 mysteryPos = new Vector3(0,0, 15f);
                    Instantiate(mystryPowerUp, mysteryPos, Quaternion.identity);
                    player.powerUpCount = 0;
                }
                else
                {
                    Vector3 spawnPos = GetRandomPosition();
                    GameObject powerUp = powerUpObj[Random.Range(0, powerUpObj.Length)];
                    GameObject curPower = Instantiate(powerUp, spawnPos, Quaternion.identity);
                    Destroy(curPower, 20f);
                }

                player.isPowerUp = true;
                Invoke(nameof(ResetPowerUp), 5f);
            }
            yield return null;
        }
       
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3( Random.Range(-17.7f, 15.5f),2,25);
    }
    void ResetPowerUp()
    {
        player.isPowerUp = false;
    }
}
