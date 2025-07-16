using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private PlayerController playerController;
    int health;
    public GameObject[] heart;

    private void Start()
    {
        
    }

    private void Update()
    {
        health = playerController.currentHealth;
        UpdateHeart();
    }

    public void UpdateHeart()
    {
        for(int i = 0; i< heart.Length; i++)
        {
            if(i < health)
            {
                heart[i].SetActive(true); 
            }
            else
            {
                heart[i].SetActive(false);  
            }
        }
    }
}
