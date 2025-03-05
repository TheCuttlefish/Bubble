using UnityEngine;
using System.Collections.Generic;

// Helper class to organize sound variations in Inspector
[System.Serializable]
public class SoundGroup
{
    [Tooltip("Identifier used in code (collect/damage/death)")]
    public string groupID;
    
    [Tooltip("Sound variations (put at least 2)")]
    public AudioClip[] clips;
    
    [Range(0f, 1f), Tooltip("Overall volume for these sounds")]
    public float volume = 1f;
    
    [Range(0.1f, 3f), Tooltip("Pitch randomness range")]
    public float pitchVariation = 0.1f;

    [System.NonSerialized] public int lastPlayedIndex = -1;
}

// Main audio controller with singleton pattern
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    // Singleton instance access
    public static AudioManager Instance { get; private set; }
    float currentPitch = 1f; 
    float pitchSmoothTime = 0.2f;  //Adjust this for smoothness
    [Header("Sound Groups")]
    public SoundGroup collectSounds;
    public SoundGroup damageSounds;
    public SoundGroup deathSounds;
    public SoundGroup movementSounds;
    public SoundGroup lastStandLaunchSounds;
    public SoundGroup lastStand;

    [Header("Music & Loops")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource idleLoopSource;

    [Header("Settings")]
    [SerializeField, Range(1, 15)] int audioPoolSize = 5;
    
    List<AudioSource> audioSourcePool = new List<AudioSource>();

    void Awake()
    {
        // Replace existing Awake() with this ▼
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject); // Keep between scenes
        InitializePool();
        
        // Auto-reconnect music source
        if(musicSource == null) musicSource = GetComponentInChildren<AudioSource>();
    }

    // Create pool of AudioSources for performance
    void InitializePool()
    {
        for (int i = 0; i < audioPoolSize; i++)
        {
            GameObject child = new GameObject($"SFX_Source_{i+1}");
            child.transform.SetParent(transform);
            
            AudioSource source = child.AddComponent<AudioSource>();
            audioSourcePool.Add(source);
        }
    }

    // Public method to play randomized sounds
    public void PlayRandomized(string soundType)
    {
        SoundGroup targetGroup = GetSoundGroup(soundType);
        
        if (targetGroup == null || targetGroup.clips.Length == 0)
        {
            Debug.LogWarning($"Sound group {soundType} not configured!");
            return;
        }

        // Only avoid repeats if we have multiple clips
        int newIndex = targetGroup.lastPlayedIndex;
        if (targetGroup.clips.Length > 1)
        {
            do {
                newIndex = Random.Range(0, targetGroup.clips.Length);
            } while (newIndex == targetGroup.lastPlayedIndex);
        }
        else
        {
            newIndex = 0;
        }

        // Update history
        targetGroup.lastPlayedIndex = newIndex;

        AudioSource availableSource = GetAvailableSource();
        if (availableSource == null) return;

        availableSource.clip = targetGroup.clips[newIndex];
        availableSource.volume = targetGroup.volume;
        availableSource.pitch = 1f + Random.Range(-targetGroup.pitchVariation, targetGroup.pitchVariation);
        availableSource.Play();
    }

    // Update idle loop parameters based on player state
    public void UpdateIdleLoop(float speedFactor, float sizeFactor)
    {
        if (idleLoopSource == null) return;

        // Calculate parameters using Mathf.Lerp for smooth transitions
        float targetPitch = Mathf.Lerp(0.8f, 1.2f, speedFactor);
        currentPitch = Mathf.Lerp(currentPitch, targetPitch, Time.deltaTime / pitchSmoothTime);
        idleLoopSource.pitch = currentPitch;
        idleLoopSource.volume = Mathf.Lerp(0.3f, 1f, sizeFactor);
    }

    // Helper methods
    AudioSource GetAvailableSource()
    {
        foreach (AudioSource source in audioSourcePool)
        {
            if (!source.isPlaying) return source;
        }
        return null; // All sources in use
    }

    SoundGroup GetSoundGroup(string id)
    {
        switch (id.ToLower())
        {
            case "collect": return collectSounds;
            case "damage": return damageSounds;
            case "death": return deathSounds;
            case "movement": return movementSounds;
            case "laststandlaunch": return lastStandLaunchSounds;
            case "laststand": return lastStand;
            default: return null;
        }
    }

    void Start()
    {
        // Start music if configured
        if (musicSource != null && !musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopIdleLoop()
    {
        if (idleLoopSource != null && idleLoopSource.isPlaying)
        {
            idleLoopSource.Stop();
        }
    }

    public void RestartIdleLoop()
    {
        if (idleLoopSource != null && !idleLoopSource.isPlaying)
        {
            idleLoopSource.Play();
            currentPitch = 1f; // Reset smoothed pitch
        }
    }
}
