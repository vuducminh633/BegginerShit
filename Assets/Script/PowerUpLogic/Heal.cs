using UnityEngine;

public class Heal : MonoBehaviour, IPowerUp
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Apply(PlayerController player)
    {
        player.currentHealth++;
    }


}
