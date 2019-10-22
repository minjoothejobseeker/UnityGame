using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
    public void Stage1Btn()
    {
        if(Property.tutorialClear == true)
        SceneManager.LoadScene("Stage1");
    }

    public void SoundSetting()
    {
        SceneManager.LoadScene("SoundSetting");
    }

    public void TitleMenu()
    {
        SceneManager.LoadScene("TitleMenu");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadingScene()
    {
        SceneManager.LoadScene("Loading");
    }

    public void Quit()
    {
        Application.Quit();

    }

    private void Awake()
    {
        Screen.SetResolution(Screen.width, (Screen.width * 16) / 9, true); //화면 비율 고정
        Camera.main.aspect = 16f / 9f;


    }
}
