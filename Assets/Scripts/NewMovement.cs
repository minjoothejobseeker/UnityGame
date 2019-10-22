using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    private GameObject player; //플레이어 오브젝트
    public GameObject hookpoint; //갈고리 걸 곳, 프론트콜라이더에서 최신화
    private Rigidbody rigidBody;
    Vector3 playerAngle; // 플레이어 보는 방향

    private Vector3 playerposatnow;

    //[SerializeField]
    public float movingspeedmul = 15.0f; // 무빙스피드에 곱해줄 것. 그냥 무빙스피드만 하면 속도가 안늘
    public float normalmovingspeedmul = 15.0f; // 무빙스피드에 곱해줄 것. 그냥 무빙스피드만 하면 속도가 안늘
    public float movingspeed = 1.0f;
    public float normalmovingspeed = 1.0f;
    float gravity = 14.0f; // 이걸 느리게하고 델타타임 빠르게하는걸 알아볼것 7
    // 해보니 똑같네
    // 보고서 작성용 로그 

    public bool lookright = true; // 어딜 쳐다보고 있는지 확인
    //타 스크립트에서 확인해야하는 것들
    
    //-------------------------------
    bool pushingspace = false; // 점프버튼 누르고있는 여부
    [SerializeField]
    bool isjumping = false; // true되면 점프한다 이거야
    [SerializeField]
    //bool isfalling = true; // isjumping이 이미 점프 가능 트리거로 사용되기때문 벽점프용
    private bool jumppeak = false; //최고점에 도달했는지 여부
    public bool usinghook = false; // 갈고리 통한 이동중
    bool pushinghookbutton = false; // 갈고리 반복 방지
    bool hookisinfrontofplayer = false;

    public bool boostmode = false; // 대시 대신 부스트모드

    //[SerializeField]
    //bool walljumpon = false; // true되면 벽점프함
    //float walljumpx = 0;
    ////float walljumpy = 0;
    //float walljumplimit = 3.5f; // 벽점프 한계값
                                //bool isdash = false; 
    [SerializeField]
    public bool movable = true; // 이동가능
    
    public bool activatehook = false; // 넘어가는 갈고리 포인트
    public bool activatehook2 = false; // 언덕 올라가기용 포인트

    float hookspeed = 50.0f;

    //점프때 위치차 계산
    float posYgap; 
    float posXgap;

    LineRenderer hookline; //갈고리 밧줄

    Vector3 hookdesiredpos; // 갈고리로 움직일 위치

    Raycast_code ray;

    public float boostenergy = 500; //부스트모드할때 사용할 에너지(현재)
    public float boostenergy_full = 500; // 부스트 모드 최고에너지

    public bool interrupt = false; // 타 코드에서 간섭용

    void Start()
    {
        //게임 전체 속도
        //Time.timeScale = 3.0f; // 1.2짱 1:기본 0:pause
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 할당
        rigidBody = GetComponent<Rigidbody>(); // 리지드바디 할당

        hookline = GameObject.FindGameObjectWithTag("Player").GetComponent<LineRenderer>();//--------------------------------------
        hookline.enabled = false; // 라인 일단 꺼둠

        boostmode = false; //혹시 모르니 일단 꺼둠
        ray = player.GetComponent<Raycast_code>();
    }

    
    void FixedUpdate()
    {

        if (movable && !interrupt)
        {
            //우로 이동
            if (Input.GetKey("d") ||Input.GetKey(KeyCode.RightArrow)) // 연속적인 눌림에 대응위해
            {
                if (!ray.movable_right)
                { NomalWalk(-movingspeed * Time.fixedDeltaTime * movingspeedmul, 0, 0); } //이동 중지

                // bool canmoveright 해서 이동 막아볼 것
                
                NomalWalk(movingspeed * Time.fixedDeltaTime * movingspeedmul, 0, 0);



                AutoRotation(90);
                lookright = true; // 두

            }
            //좌로 이동
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow))
            {

                if (!ray.movable_left)

                { NomalWalk(movingspeed * Time.fixedDeltaTime * movingspeedmul, 0, 0); }


                NomalWalk(-movingspeed * Time.fixedDeltaTime * movingspeedmul, 0, 0);

                AutoRotation(270);
                lookright = false;

            }
        }




        Gravity();

        

        //-------------------------------------충돌췍
        if (!ray.movable_down)//바닥
        {
            //Debug.Log("바닥닿");
            isjumping = false;
            jumppeak = false;
            //pushingspace = false;
            posYgap = 0;
           // isfalling = false;
        }
        //머리 충돌
        if (!ray.movable_up)//건축물에 머리가 닿을경우
        {
            isjumping = false;
            jumppeak = true;
        }


        //------------------------중력
        //if (ray.movable_down)
        //{
        //    isfalling = true;
        //    //Gravity();
        //}
        

        //-------------------------------------------------------------------------------------
        //점프----------------------------------------------------------점프        
        if (Input.GetKey("c") && pushingspace == false)
        {
            

            if (isjumping == false && !ray.movable_down)
            {
                
                playerposatnow = player.transform.position; // 플레이어 y좌표 저장(점프 높이 제한으로 사용)
                isjumping = true;
                //print(jumppeak);
            }
            pushingspace = true; //스페이스 누르고 있으면 반복 점프 방지

        }
        // 포지션이 상승하는 부분
        if (isjumping == true && jumppeak == false)
        {
            //print("점프3");
            Vector3 tempPosition = player.transform.position;
            tempPosition.y += 35.0f * Time.fixedDeltaTime;
            player.transform.position = tempPosition;            
            posYgap = player.transform.position.y - playerposatnow.y;
        }

        if ((isjumping == true && (posYgap >= 5)) || (isjumping == true && Input.GetKeyUp("space")) )
        // 점프를 중간에 떼면 멈춤
        //isground가 아니라 트리거 붙었을때 비슷한걸 따로 만들어줘서 해야할듯
        {
            isjumping = false;
            jumppeak = true;
            // 붙은상태로 점프를 누르고있으면 점프피크는 true로 바뀌는데 새충돌이 없어서 jumppeak가 false로 안됨
        }
        if (Input.GetKeyUp("c"))
        {
            pushingspace = false;//스페이스바 해제
        }
        //-------------------------------------------------------------------------------------


        



        //-------------------------달리기

        //부스트를 쓰는 것같은 파티클 추가할 것
        //시간제한 둘 것
        if (boostmode)
        {
            movingspeed = normalmovingspeed * 1.6f; // 이동속도 늘려줌

            //부스트모드에 에너지 필요
            if (boostenergy > 0)
            {
                boostenergy--; //부스트 에너지 
            }
            else if (boostenergy == 0)
            {
                boostmode = false;
                
            }

        }
        else if (boostmode == false)
        {
            movingspeed = normalmovingspeed; // 이동속도 원상복귀
            //부스트 에너지 충전
            if (boostenergy < boostenergy_full)
            {
                boostenergy++; 
            }
            else if (boostenergy == boostenergy_full)
            {
                
                //나중에 완충 사운드를 넣던지 할 것
            }

        }

        


        


        //갈고리시에 여기서 트루되버림 일단 임시로 둘 다 막음
        if ( !usinghook) // 벽점 끝나면 다시 움직일 수 있게함 
        {
            movable = true;
        }
        //--------------------------------------벽점프


        

    }

    void Update() //fixed를 2배속 했더니 키가 두 번씩 눌려서 따로 만듬
    {
        //부스트 모드
        if (Input.GetKeyDown("v"))
        {            
            if (boostmode)
            {
                boostmode = false;
            }
            else
            {
                boostmode = true;
            }
        }
        //부스트 모드

        
        //------------------------------------갈고리액션
              

        if (activatehook == true)
        {
            //여기서 특정 버튼 누르면 갈고리on
            // 라인렌더러 온
            //갈고리 이동이 끝날때까지 트리거 하나 더 만들 것
            //여기서 갈고리포인트부분에 인스턴스 하나 생성 false에서 디스트로이

            if (Input.GetKey("x") && pushinghookbutton == false)
            {
                hookline.enabled = true; // 밧줄 모양 on
                pushinghookbutton = true;

                //여기서 유싱훅 시작하면 이쪽진입 못하게하는 트리거 하나 더 만들 것
                if (!usinghook)
                {
                    //갈고리 이동 목표 좌표값 지정
                    // 플레이어가 갈고리포 왼측에 있을때
                    if ((hookpoint.transform.position.x - player.transform.position.x) >= 0)
                    {

                        hookdesiredpos.x = hookpoint.transform.position.x + 10.0f; // 오른쪽                         
                        hookdesiredpos.y = hookpoint.transform.position.y + 2.5f; // 높이는 같다

                        hookisinfrontofplayer = true;
                        usinghook = true; //이동 트리거

                    }
                    // 플레이어가 갈고리포 우측에 있을때
                    else if ((hookpoint.transform.position.x - player.transform.position.x) < 0)
                    {
                        hookdesiredpos.x = hookpoint.transform.position.x - 10.0f; // 왼쪽 
                        hookdesiredpos.y = hookpoint.transform.position.y + 2.5f; // 높이는 같다

                        hookisinfrontofplayer = false;
                        usinghook = true;
                    }
                }
            }
        }
        //갈고리 영역 벗어남
        else if (activatehook == false)
        {            
            hookline.enabled = false; //밧줄 모양 끔
        }
        

        //갈고리 사용 >> 좌표 이동
        if (usinghook)
        {
            movable = false; // 갈고리 끝날때까지 방향키 이동 금지
            Vector3 temppos = player.transform.position; // 현 위치값 설정

            //y좌표값 상승

            if (hookdesiredpos.y > player.transform.position.y && ray.movable_up)
            {
                temppos.y += hookspeed * Time.deltaTime * 1.2f;
            }
            
            else if ((hookdesiredpos.y == player.transform.position.y)
                || (hookdesiredpos.y - player.transform.position.y) < 0.5f) // 목표점에 도달했을 경우
            {
                temppos.y += gravity * Time.deltaTime; // 흔들림 방지
            }

            //갈고리가 플레이어보다 우측에 있음
            if (hookisinfrontofplayer == true)
            {
                if (hookdesiredpos.x >= player.transform.position.x && ray.movable_right)
                {                    
                    temppos.x += hookspeed * Time.deltaTime; // deltatime * 2~3해볼 것 
                    
                }
                else // 끝나는 부분
                {
                    pushinghookbutton = false; // 갈고리버튼 연속입력 불가
                    usinghook = false;
                    movable = true; //이동 허가
                    hookline.enabled = false;
                }

            }
            //갈고리포인트가 왼쪽에 있음
            if (hookisinfrontofplayer == false)
            {
                if (hookdesiredpos.x <= player.transform.position.x && ray.movable_left)
                {
                        temppos.x -= hookspeed * Time.deltaTime; // deltatime * 2~3해볼 것

                    
                }
                else // 끝나는 부분
                {
                    pushinghookbutton = false; // 갈고리버튼 연속입력 불가
                    usinghook = false;
                    movable = true; //이동 허가
                    hookline.enabled = false;
                }

            }
            player.transform.position = temppos;

            //갈고리 중간에 끊기
            if (Input.GetKeyDown("c")) //e로하면 갈고리 재작동됨
            {
                usinghook = false;
                movable = true; //이동 허가
                hookline.enabled = false;
                pushinghookbutton = false; // 갈고리버튼 연속입력 불가
            }
        }


        // 갈고리 라인 컨트롤
        if (hookline.enabled == true)
        {
            hookline.SetPosition(0, player.transform.position); // 출발지
            hookline.SetPosition(1, hookpoint.transform.position); // 도착지

        }
        //------------------------------------갈고리액션

    }

    

    //중력
    void Gravity()
    {
        if (ray.movable_down)
        {
            //점프는 중력 이기게끔
            Vector3 tempPosition = player.transform.position;
            tempPosition.y -= gravity * Time.fixedDeltaTime;
            player.transform.position = tempPosition;
        }
    }

    //플레이어 이동
    void NomalWalk(float x, float y, float z)
    {
        Vector3 tempPosition = player.transform.position;
        tempPosition.x += x;
        tempPosition.y += y;
        tempPosition.z += z;
        player.transform.position = tempPosition;
        //글로벌 좌표를 더해준다
    }

    //움직일때 자동으로 플레이어 방향전환
    void AutoRotation(float angle)
    {
        playerAngle = player.transform.rotation.eulerAngles;
        playerAngle.y = angle;
        player.transform.rotation = Quaternion.Euler(playerAngle);
    }
}




//-------------------------달리기




////--------------------------------------벽점프

////벽점프때 키보드막기 movable 해서 끝날때까지 

// //여기 lookright left부터 판별하고 거리 조절할 것
//if ((!ray.movable_left && ray.movable_down) || (!ray.movable_right && ray.movable_down))
//{

//    //점프를 누르는 순간 isjumping이 발동돼서 개지랄함
//    if (Input.GetKeyDown("space"))
//    {
//        movable = false; // 벽점프간 이동키 금지시킴
//        if (!ray.movable_right)
//        {
//            lookright = false;

//            walljumpon = true;
//        }
//        else if (!ray.movable_left)
//        {
//            lookright = true;

//            walljumpon = true;
//        }
//    }
//}

//if (walljumpon)
//{


//    playerposatnow = player.transform.position; // 벽점프 전 위치 저장

//    //벽이 점프 거리보다 가까우면 멈추는거 연구 walljumpoff

//    if (lookright )
//    {                
//        AutoRotation(90);
//        playerposatnow.y += movingspeed * Time.fixedDeltaTime * movingspeedmul * 5.0f; // 중력때문에 y는 값을 크게해줘야함
//        playerposatnow.x += movingspeed * Time.fixedDeltaTime * movingspeedmul;
//        Debug.Log("왼벽점");
//    }
//    else if (!lookright ) 
//    {          

//        AutoRotation(270);
//        playerposatnow.y += movingspeed * Time.fixedDeltaTime * movingspeedmul * 1.5f;
//        playerposatnow.x -= movingspeed * Time.fixedDeltaTime * movingspeedmul;

//        // 이렇게 말고 트리거 형식으로 작은수 더하기로 할 것
//        //여기서 월점프on 하면 업데이트에서 +해주고 똑같이 float 변수a 해서 a가 3되면 월점프 off
//    }
//    walljumpx += movingspeed * Time.fixedDeltaTime * movingspeedmul; // 점프길이 계산용
//    player.transform.position = playerposatnow;
//}
//if (walljumpx >= walljumplimit)
//{
//    Debug.Log("벽점Rmx");
//    walljumpon = false;
//    walljumpx = 0;
//}