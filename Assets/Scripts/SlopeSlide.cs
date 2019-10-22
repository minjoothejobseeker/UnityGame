using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeSlide : MonoBehaviour
{
    public Transform endpos; // 끝 포지션
    Vector3 despos; // 가야하는 포지션
    public Transform player;
    float speed = 5.5f; // 이동 속도

    public bool slide = false; //플레이어가 올라 탔는지

    NewMovement player_code;

    void Start()
    {
        despos = endpos.position; // 처음엔 끝점으로 이동한다
        player_code = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
    }
    void Update()
    {
        if (slide)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, despos, speed * Time.deltaTime * 3);
            player_code.movable = false;
        }
        else { player_code.movable = true; }

    }

    

    private void OnTriggerEnter(Collider other)
    {


        player = other.transform.parent; //other는 플레이어 아래에 속한 매니저콜라이더이므로 그 부모(플레이어)를 삽입
        if (player.tag == "Player")
        {
            slide = true;          
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //이걸 해주는 이유는 발판에서 떨어져도 부모자식 관계가 붙어있어 함께 움직이는 것을 막기위함
        player = other.transform.parent; //other는 플레이어 아래에 속한 매니저콜라이더이므로 그 부모(플레이어)를 삽입
        if (player.tag == "Player")
        {
            slide = false;
        }
    }

}
