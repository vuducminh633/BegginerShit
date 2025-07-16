
using UnityEngine;

public class MysteryBoxLogic : MonoBehaviour
{
    public GameObject parentMysteryBox;
   
    public void DeActiveParent()
    {
        Destroy(parentMysteryBox, .5f);
    }

    public void ActiveRandomPower(PlayerController player)
    {
        int random = Random.Range(1, 5);

        switch(random)
        {
            case 1:
                Gun gun = player.GetComponentInChildren<Gun>();
                gun.ActiveGun();
                break;
            case 2:
                player.currentHealth++;
                break;
            case 3:
                ShieldEffect shieldEffect = player.GetComponentInChildren<ShieldEffect>();
                player.powerUpManager.ActivateShield(3f, shieldEffect);
                break;
            case 4:
                ShieldEffect shieldEffect1 = player.GetComponentInChildren<ShieldEffect>();
                player.powerUpManager.ActivateShield(3f, shieldEffect1);

                Gun gun1= player.GetComponentInChildren<Gun>();
                gun1.ActiveGun();

                player.currentHealth++;
                break;
            case 5:
                
                // Bomb
                    break;

        }
    }

   

}