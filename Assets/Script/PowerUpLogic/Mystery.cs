using UnityEngine;

public class Mystery : MonoBehaviour, IPowerUp
{
   public void Apply(PlayerController player)
    {
        MysteryBoxLogic box = GetComponentInParent<MysteryBoxLogic>();
        box.ActiveRandomPower(player);
        box.DeActiveParent();
    }
}
