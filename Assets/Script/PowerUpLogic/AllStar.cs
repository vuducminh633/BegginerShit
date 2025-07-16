using UnityEngine;

public class AllStar : MonoBehaviour, IPowerUp
{
   public void Apply(PlayerController player)
    {
        ShieldEffect shieldEffect = player.GetComponentInChildren<ShieldEffect>();
        player.powerUpManager.ActivateShield(3f, shieldEffect);

        Gun gun = player.GetComponentInChildren<Gun>();
        gun.ActiveGun();

        player.currentHealth++;
    }
}
