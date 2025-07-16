using UnityEngine;

public class GunPickUp : MonoBehaviour, IPowerUp
{
    
    public void Apply(PlayerController player)
    {
        Gun gun = player.GetComponentInChildren<Gun>();
        gun.ActiveGun();
    }
}
