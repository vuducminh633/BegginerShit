using UnityEngine;

public class BoundaryUp : MonoBehaviour
{
    public float minSpeedMultiplier = 0f;
    public float maxSpeedMultiplier = 1f;
    public float maxDepth = 2f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player == null) return;

            float verticalInput = Input.GetAxis("Vertical");
            if (verticalInput > 0.1f)
            {
                Vector3 fieldCenter = transform.position;
                Vector3 playerPos = other.transform.position;

                float depthZ =  fieldCenter.z - playerPos.z;
                float t = Mathf.Clamp01(depthZ / maxDepth);

                float slowdownFactor = Mathf.Lerp(maxSpeedMultiplier, minSpeedMultiplier, t);

                player.SetSpeedMultiplier(slowdownFactor);
            }
            else if (verticalInput < -0.1f) // pressing up
            {
                player.SetSpeedMultiplier(1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetSpeedMultiplier(1f); // fully restore speed on exit
            }
        }
    }
}
