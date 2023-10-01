using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class AudioManager : MonoBehaviour
{
    [Header("Volumn Settings")]
    [Range(0, 1)]
    public float masterVolumn = 1;
    [Range(0, 1)]
    public float MusicVolumn = 1;
    [Range(0, 1)]
    public float SFXVolumn = 1;

    private Bus MasterBus;
    private Bus MusicBackgroundBus;
    private Bus SFXBus;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MasterBus = RuntimeManager.GetBus("bus:/");
            MusicBackgroundBus = RuntimeManager.GetBus("bus:/Background Music");
            SFXBus = RuntimeManager.GetBus("bus:/Sound Effect");
        } else Destroy(gameObject);

    }

    private void Update() {
        MasterBus.setVolume(masterVolumn);
        MusicBackgroundBus.setVolume(MusicVolumn);
        SFXBus.setVolume(SFXVolumn);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }
}