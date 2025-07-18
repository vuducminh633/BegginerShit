using UnityEngine;

public class Boss : MonoBehaviour
{
    public enum BossState
    {
        Idle,
        SpawningDogs,
        RotatingDogs,
        ActivatingFlame,
        WaitingForFlame,
        Finished
    }


    [SerializeField] private GameObject[] DogFlamePrefab;

    [SerializeField] private Transform spawnPointLeft;
    [SerializeField] private Transform spawnPointRight;

    [SerializeField] private float rotationSpeed = 30f;
    
    private BossState currentState = BossState.Idle;
    private int currentIndex = 0;


    private GameObject currentDogL;
    private GameObject currentDogR;
    private bool flameFinished = false;


    private void Update()
    {
        
    }

    void SetDog()
    {
        GameObject dog0 = Instantiate(DogFlamePrefab[0], spawnPointLeft.position, Quaternion.Euler(0, 180f, 0));

    }
    void RotateDog()
    {

    }
}
