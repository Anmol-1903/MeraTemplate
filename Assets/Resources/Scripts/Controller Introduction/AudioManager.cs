using System;
using AYellowpaper.SerializedCollections;
using UnityEngine;
public class AudioManager : MonoBehaviour
{
     //public Transform refer;
    public SerializedDictionary<string, AudioClip> englishDictionary = new ();
    public SerializedDictionary<string, AudioClip> hindiDictionary = new ();
    public static AudioManager Instance;
    private void Awake() {
        if (Instance == null) Instance = this;
        else
            Destroy(Instance);
    }
    private void OnDestroy()
    {
        // Clean up the instance when this object is destroyed
        if (Instance == this)
        {
            Instance = null;
        }
    }
    public AudioSource audioSource;
    public void PlayAudioClip(String name)
    {
        if (AudioSelection.selectedLanguage == Language.English)
        {
            if (englishDictionary.TryGetValue(name, out AudioClip audioClip))
            {
                if(gameObject != null)
                if(audioSource.isPlaying) audioSource.Stop();
                    audioSource.PlayOneShot(audioClip);
            }
            
        }
        else if (AudioSelection.selectedLanguage == Language.Hindi)
        {
            if (hindiDictionary.TryGetValue(name, out AudioClip audioClip))
            {
                
                if(gameObject != null)
                // audioSource.clip = audioClip;
                if(audioSource.isPlaying) audioSource.Stop();
                    audioSource.PlayOneShot(audioClip);// what if that game object is not active yet?
            }
        }
    } 
    public void StopAudioSource()
    {
         audioSource.Stop();
    }
}
