using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAudioVolume : MonoBehaviour
{
    //효과음의 볼륨을 설정
    //배경음은 게임매니저에서 자동 할당
 
    public AudioSource fx; // 효과음
    GameObject player; //플레이어 위치 얻기 위함
    float validdistance = 12.0f; //소리가 들리기 시작하는 거리

    void Start()
    {
        fx.volume = Property.fxvolume;//설정에서 세팅한 볼륨 대입
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //print(Vector3.Distance(player.transform.position, this.transform.position));
        if (Vector3.Distance(player.transform.position, this.transform.position) < validdistance)
        {
            fx.enabled = true;
            //fx.loop = true;
        }
        else
        {
            fx.enabled = false;
            //fx.loop = false;
        }
    }

}
