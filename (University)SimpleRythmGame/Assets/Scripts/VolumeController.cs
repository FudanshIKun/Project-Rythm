using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class VolumeController : MonoBehaviour
{
    [SerializeField] Slider volumeSlider; // Volume Setting
    AudioSource Speaker; // main AudioSource

    public void ChangeVolume(){
        if (volumeSlider == null){return;}
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load(){
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume"); // Load value from PlayerPrefs
    }

    private void Save(){
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value); // Save volumeslider's value as keyname "musicVolume"
    }

    void Start()
    {
        Speaker = GetComponent<AudioSource>();

        if (volumeSlider != null){
            if(!PlayerPrefs.HasKey("musicVolume")){
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }else{
            volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            }
        }
    }

    
    void Update()
    {
        ChangeVolume();
    }
}
