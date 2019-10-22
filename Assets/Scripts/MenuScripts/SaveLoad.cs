using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoad : MonoBehaviour
{
    //사운드 세팅은 자동저장 클리어 여부는 수동저장

    public void Save()
    {
        //PlayerPrefs.SetFloat("BGM_Volume", Property.bgmvolume);
        //PlayerPrefs.SetFloat("FX_Volume", Property.fxvolume);
        PlayerPrefs.SetString("Stage1Clear", Property.stage1Clear.ToString());//bool을 string으로 변환해 저장

    }

    public void Load()
    {

        //Property.bgmvolume = PlayerPrefs.GetFloat("BGM_Volume");
        //Property.fxvolume = PlayerPrefs.GetFloat("FX_Volume");        
        Property.stage1Clear = System.Convert.ToBoolean(PlayerPrefs.GetString("Stage1Clear", "false"));
        //string을 bool로 변환(오른쪽은 기본값, 만일 디폴트 없이 저장값이 없으면 에러남)

    }

    public void change()//상태 체크용
    {
        //Property.stage1Clear = true;

    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
        
    }
}
