using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioSource musicsource; //배경음

    public AudioSource btnsource; // fx볼륨

    public Slider bgmbar;
    public Slider fxbar;


    //Property prop;

    private void Start()
    {        
        // 전에 저장해둔 설정이 있다면 자동 로드. 없다면 1
        musicsource.volume = PlayerPrefs.GetFloat("BGM_Volume", 1); 
        btnsource.volume = PlayerPrefs.GetFloat("FX_Volume", 1);
        bgmbar.value = PlayerPrefs.GetFloat("BGM_Volume", 1); //슬라이더 조절
        fxbar.value = PlayerPrefs.GetFloat("FX_Volume", 1); // 슬라이더 조절

    }
    



    public void SetMusicVolume(float volume)
    {
        musicsource.volume = volume;
        Property.bgmvolume = volume;
        PlayerPrefs.SetFloat("BGM_Volume", Property.bgmvolume); //변경시 자동저장
        

    }

    public void SetButtonVolume(float volume)
    {
        btnsource.volume = volume;
        Property.fxvolume = volume;
        PlayerPrefs.SetFloat("FX_Volume", Property.fxvolume); //변경시 자동저장

    }

    public void OnSfx()
    {
        btnsource.Play();
    }
    
}
