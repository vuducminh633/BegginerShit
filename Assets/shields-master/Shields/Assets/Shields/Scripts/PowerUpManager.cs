using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour
{
    public PlayerController player;

    public void ActivateShield(float duration, ShieldEffect shieldEffect)
    {
        StartCoroutine(ShieldRoutine(duration, shieldEffect));
    }

    private IEnumerator ShieldRoutine(float duration, ShieldEffect shieldEffect)
    {
        // Stop any ongoing invincibility when shield activates to prevent conflicts
        if (player.IsInvincible())
        {
            player.StopInvincibility();
        }
        
        player.isSheild = true;
        if (shieldEffect != null)
            shieldEffect.EnableShield();

        yield return new WaitForSeconds(duration);

        player.isSheild = false;
        player.isPowerUp = false;
        if (shieldEffect != null)
            shieldEffect.DisableShield();
    }
}
