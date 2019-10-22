using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParents : MonoBehaviour
{
    
    public Transform player;      

    private void OnTriggerEnter(Collider other)
    {

        
        player = other.transform.parent; //other는 플레이어 아래에 속한 매니저콜라이더이므로 그 부모(플레이어)를 삽입
        if (player.tag == "Player")
        {
            
            player.transform.SetParent(transform); //발판에 플레이어를 자식화해 이동을 일치시킴
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        //이걸 해주는 이유는 발판에서 떨어져도 부모자식 관계가 붙어있어 함께 움직이는 것을 막기위함
        player = other.transform.parent; //other는 플레이어 아래에 속한 매니저콜라이더이므로 그 부모(플레이어)를 삽입
        if (player.tag == "Player")
        {

            player.transform.SetParent(null); //떨어지면 부모를 해제해 자유이동 가능
        }
    }
}
