using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.125f;
    Vector3 offset;

    GameManager gamemanager;

    NewMovement player;

    float targerfindertimer = 0f;

    private void Start()
    {
        Camera.main.aspect = 16f / 9f; //카메라 비율 고정
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
        offset.x = 2.5f;
        offset.y = 1.0f;
        offset.z = -11;
        //로테이션 x = 6, 0, 0이 적당

        FindPlayer();
    }
    void LateUpdate() //모든 프레임에 업데이트
        // fixedupdate는 프레임이 떨어져보여서 lateupdate로
    {

        if (target == null)
        {
            targerfindertimer++;//타이머 시작
                    
        }
        else
        {
            if (target.tag != "Player")
            {
                //타이머 20f후 플레이어로 교체
                targerfindertimer++;//타이머 시작
            }

            if (!gamemanager.dead)//죽으면 카메라 스크립트에서 플레이어 더이상 찾지 않게함, 스타트는 어차피 살아있을거니 안넣어도 상관X
            {
                SetCamPos();
            }
            //왼쪽보냐 오른쪽 보냐에 따라 offset값 변경
            if (player.lookright)
            { offset.x = 2.5f; }
            else
            { offset.x = -2.5f; }
        }

        if (targerfindertimer == 80f) //30프레임이 지나면
        {
            FindPlayer(); // 플레이어 찾기
            targerfindertimer = 0f;
        }



    }

    void FindPlayer()
    {
        
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void SetCamPos()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition =
            Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        // lerp란 두 벡터를 보간해주는 것
        transform.position = smoothedPosition;
    }

}


/* UPDATE FIXEDUPDATE LATEUPDATE의 차이
 * Update
 *      스크립트가 활성화일때 매 프레임 호출
 *      물리효과 적용X인 움직임, 단순타이머, 키입력받을때 사용
 * FixedUpdate
 *      프레임 기반이 아닌 Fixed Timestep에 설정된 값에 따라 호출
 *      물리 효과 오브젝트를 조정시 사용
 *      (Update는 불규칙한 호출이라 물리엔진 충돌검사가 제대로 안될 수 있어서)
 * LateUpdate
 *      모든 Update가 호출된 후, 마지막으로 호출
 *      주로 오브젝트를 따라가게 설정한 카메라에 사용
 *      (오브젝트가 Update에서 움직이는 경우가 많으므로)
 * 
 * */