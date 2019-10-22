using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Transform startpos; //시작 포지션
    public Transform endpos; // 끝 포지션
    
    public float speed = 2.0f; // 이동 속도
    int movingphase1 = 1; //움직임 단계

    //트랜스폼으로 하면 좌표가 계속 변경되므로 옮김
    Vector3 spos; // 시작 포지션
    Vector3 epos; // 끝포지션
    Vector3 despos; // 가야하는 포지션

    public bool movingon = false; // 레버가 없는 경우 유니티 내부에서 트루로 체크해줌

    void Start()
    {
        //트랜스폼으로 하면 좌표가 계속 변경되므로 타 변수에 옮겨 사용
        spos = startpos.position;
        epos = endpos.position;
        despos = endpos.position; // 처음엔 끝점으로 이동한다
    }

    //public leveron == true면 작동하게 
    void Update()
    {
        if (movingon) //레버에 따라 true됨
        {
            if (movingphase1 == 1) //1단계 움직임
            {
                transform.position = Vector3.MoveTowards(transform.position, despos, speed * Time.deltaTime);
            }
            else //2단계 움직임
            {
                transform.position = Vector3.MoveTowards(transform.position, despos, speed * Time.deltaTime);
            }

            //목적지 도착시 목적지 교체
            if (Vector3.Distance(transform.position, despos) <= 0.05f) // 사이 거리가 0.05f 아래면
            {
                if (movingphase1 == 1)
                    despos = spos; //1단계면 시작점을 목적지로 재설정
                else
                    despos = epos; //2단계면 끝점을 목적지로 재설정

                movingphase1 = movingphase1 * (-1); // 단계 구분용 변수
                //편도로 움직이는 함정 만들때 윗줄 삭제할것

            }
        }
    }

    
}
