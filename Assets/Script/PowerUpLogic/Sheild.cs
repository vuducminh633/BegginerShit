using System;
using System.Collections;
using UnityEngine;

public class Sheild : MonoBehaviour, IPowerUp
{
    public void Apply(PlayerController player)
    {
        ShieldEffect shieldEffect = player.GetComponentInChildren<ShieldEffect>();
        player.powerUpManager.ActivateShield(5f, shieldEffect);
    }

    
   
}
