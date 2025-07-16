using UnityEngine;
using System.Collections;

public class BGMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    public GameObject audioTriggerZonePrefab;
    public float spawnInterval = 60f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnTriggerZone();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnTriggerZone()
    {
        if (audioTriggerZonePrefab == null) return;

        Vector3 spawnPosition = new Vector3(
            Random.Range(-17.7f, 15.5f),
            2f,
            25f
        );

        GameObject triggerGO = Instantiate(audioTriggerZonePrefab, spawnPosition, Quaternion.identity);

        // Assign the onPlayerEnter callback dynamically
        TriggerZone trigger = triggerGO.GetComponent<TriggerZone>();
        if (trigger != null)
        {
            trigger.onPlayerEnter = SelectRandomAudio;
        }
    }

    void SelectRandomAudio()
    {
        if (audioClip.Length == 0 || audioSource == null) return;

        int index = Random.Range(0, audioClip.Length);
        audioSource.clip = audioClip[index];
        audioSource.Play();
    }
}
