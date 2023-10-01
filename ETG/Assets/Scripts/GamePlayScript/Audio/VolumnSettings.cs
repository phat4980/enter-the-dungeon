using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumnSettings : MonoBehaviour
{
    private enum VolumeType {
        MASTER,
        MUSIC,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

    private Slider volumeSlider;

    private void Awake() {
        volumeSlider = this.GetComponentInChildren<Slider>();
    }

    private void Update() {
        switch(volumeType)
        {
            case VolumeType.MASTER:
                volumeSlider.value = AudioManager.instance.masterVolumn;
                break;
            case VolumeType.MUSIC:
                volumeSlider.value = AudioManager.instance.MusicVolumn;
                break;
            case VolumeType.SFX:
                volumeSlider.value = AudioManager.instance.SFXVolumn;
                break;
            default:
                Debug.LogWarning("Volume Type not supported" + volumeType);
                break;
        }
    }

    public void OnSliderValueChanged()
    {
         switch(volumeType)
        {
            case VolumeType.MASTER:
            AudioManager.instance.masterVolumn = volumeSlider.value;
                break;
            case VolumeType.MUSIC:
            AudioManager.instance.MusicVolumn = volumeSlider.value;
                break;
            case VolumeType.SFX:
            AudioManager.instance.SFXVolumn = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("Volume Type not supported" + volumeType);
                break;
        }
    }
}
