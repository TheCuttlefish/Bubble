using UnityEngine;

public class CollectableAmbient : MonoBehaviour
{
    [Header("3D Sound Settings")]
    [SerializeField] AudioClip[] ambientVariations;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float pitchVariation = 0.1f;

    void OnEnable()
    {
        if (ambientVariations.Length > 0 && audioSource != null)
        {
            // Randomize without immediate repeats
            int randomIndex = Random.Range(0, ambientVariations.Length);
            audioSource.clip = ambientVariations[randomIndex];
            
            // Apply variations
            audioSource.pitch = 1 + Random.Range(-pitchVariation, pitchVariation);
            audioSource.Play();
        }
    }

    public void StopAmbient()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

}

