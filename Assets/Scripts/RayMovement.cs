using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayMovement : MonoBehaviour
{
    private GameObject player; //플레이어 오브젝트

    private Rigidbody rigidbody;
    Vector3 playerAngle; // 플레이어 보는 방향

    public float movingspeed = 20.0f;
    public float normalmovingspeed = 20.0f;
    float gravity = 7.0f; // 이걸 느리게하고 델타타임 빠르게하는걸 알아볼것

    bool lookright = true;
    bool leftmovable = true;
    bool rightmovable = true;
    bool isfloor = false;
    bool isjumping = false;
    private bool jumppeak = false; //최고점에 도달했는지 여부
    float posYgap;
    private Vector3 playerposatnow;

    bool pushingspace = false;

    Rigidbody playerrg;

    Raycast_code ray;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); // 플레이어 할당
        rigidbody = GetComponent<Rigidbody>(); // 리지드바디 할당
        ray = player.GetComponent<Raycast_code>();


    }

    // Update is called once per frame
    void FixedUpdate()
    //void Update()
    {


        Gravity();
        //0.55 벽점프 0.527벽
        //print(ray.downhit.distance + "ee");


        //1.25이 적절
        if (!ray.movable_down)
        {
            isfloor = true;
            jumppeak = false;
            pushingspace = false;
            isjumping = false;

        }
        else
        {
            isfloor = false;
        }

        //if (ray.movable_right)
        //{
        //    if (Input.GetKey("d")) // 연속적인 눌림에 대응위해
        //    {
        //        // bool canmoveright 해서 이동 막아볼 것
        //        NomalWalk(movingspeed * Time.fixedDeltaTime * 3, 0, 0);

        //        AutoRotation(90);
        //        lookright = true; // 두

        //    }
        //}


        if (Input.GetKey("d")) // 연속적인 눌림에 대응위해
        {
            if (!ray.movable_right)

            { NomalWalk(-movingspeed * Time.fixedDeltaTime * 15, 0, 0); }

            // bool canmoveright 해서 이동 막아볼 것
            NomalWalk(movingspeed * Time.fixedDeltaTime * 15, 0, 0);



            AutoRotation(90);
            lookright = true; // 두


        }



        //좌로 이동

        if (Input.GetKey("a"))
        {
            if (!ray.movable_left)

            { NomalWalk(movingspeed * Time.fixedDeltaTime * 15, 0, 0); }


            NomalWalk(-movingspeed * Time.fixedDeltaTime * 15, 0, 0);

            AutoRotation(270);
            lookright = false;
        }


        //0.6

        //점프----------------------------------------------------------점프        
        if (Input.GetKey("space") && pushingspace == false)
        {

            if (isjumping == false && isfloor)
            {
                playerposatnow = player.transform.position; // 플레이어 y좌표 저장(점프 높이 제한으로 사용)
                isjumping = true;
            }
            pushingspace = true; //스페이스 누르고 있으면 반복 점프 방지

        }
        // 포지션이 상승하는 부분
        if (isjumping == true && jumppeak == false)
        {
            Vector3 tempPosition = player.transform.position;

            tempPosition.y += 20.0f * Time.fixedDeltaTime;
            player.transform.position = tempPosition;
            posYgap = player.transform.position.y - playerposatnow.y;
        }

        if ((isjumping == true && (posYgap >= 5)) || (isjumping == true && Input.GetKeyUp("space")))
        // 점프를 중간에 떼면 멈춤
        //isground가 아니라 트리거 붙었을때 비슷한걸 따로 만들어줘서 해야할듯
        {
            isjumping = false;
            jumppeak = true;
            // 붙은상태로 점프를 누르고있으면 점프피크는 true로 바뀌는데 새충돌이 없어서 jumppeak가 false로 안됨
        }
        if (Input.GetKeyUp("space"))
        {
            pushingspace = false;//스페이스바 해제
        }
        //-------------------------------------------------------------------------------------



        if (!ray.movable_up)
        {
            isjumping = false;
            jumppeak = true;
        }

    }
    void AutoRotation(float angle)
    {
        playerAngle = player.transform.rotation.eulerAngles;
        playerAngle.y = angle;
        player.transform.rotation = Quaternion.Euler(playerAngle);
    }

    void NomalWalk(float x, float y, float z)
    {
        Vector3 tempPosition = player.transform.position;
        tempPosition.x += x;
        tempPosition.y += y;
        tempPosition.z += z;
        player.transform.position = tempPosition;
        //글로벌 좌표를 더해준다
    }

    void Gravity()
    {
        print("중력1");
        if (!isfloor)
        {
            print("중력2");
            //점프는 중력 이기게끔
            Vector3 tempPosition = player.transform.position;

            tempPosition.y -= gravity * Time.fixedDeltaTime;
            player.transform.position = tempPosition;
        }
    }
}
