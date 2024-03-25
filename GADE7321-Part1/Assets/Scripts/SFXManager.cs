using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SFXManager : MonoBehaviour
{
    private static SFXManager instance;
    private AudioSource audioSource;
    private Dictionary<string, AudioClip> soundMap = new Dictionary<string, AudioClip>();
    public List<AudioClip> soundClips;
    private float overallVolume = 1f;

    public static SFXManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SFXManager>();

                if (instance == null)
                {
                    GameObject obj = new GameObject("SFXManager");
                    instance = obj.AddComponent<SFXManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            audioSource = gameObject.AddComponent<AudioSource>();

            foreach (AudioClip clip in soundClips)
            {
                soundMap.Add(clip.name, clip);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string soundName, float duration, bool loop)
    {
        if (soundMap.ContainsKey(soundName))
        {
            audioSource.loop = loop;
            audioSource.PlayOneShot(soundMap[soundName], overallVolume);

            if (!loop)
            {
                StartCoroutine(StopSoundAfterDelay(duration));
            }
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }

    public void StopSound()
    {
        audioSource.Stop();
    }

    public void SetOverallVolume(float volume)
    {
        overallVolume = Mathf.Clamp01(volume);
        audioSource.volume = overallVolume;
    }

    private IEnumerator StopSoundAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        StopSound();
    }
}
