using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCollider : MonoBehaviour
{
    NewMovement player;
    GameManager gamemanager;

    GameObject slope;

    
    Vector3 savepoint; // 세이브포인트 좌표
    //GameObject shortcut;

    CameraFollow cameraf; // 숏컷시 카메라용

    bool leverusable = false; //레버 사용
    public bool movingplatformlever = false;

    float timer = 0;
    GameObject cameratarget;

    Raycast_code ray;
    MovingPlatform mp; // mp용 레버 따로만들 것 게임오브젝트 넣어서 moving = true

    int tutorialsequence = 0;//튜토리얼에 사용할 사진순서
    public GameObject[] tutorialexample = new GameObject[8]; //사용할 사진
    GameObject tutorial;
    GameObject tutorialButton;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        slope = GameObject.FindGameObjectWithTag("slope");
        cameraf = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraFollow>();
        ray = player.GetComponent<Raycast_code>();

        
    }

    private void Update()
    {
        //-------------레버 통해 카메라 움직임
        if (leverusable)
        {
            timer++;
            player.interrupt = true; //카메라 움직이는동안 플레이어 움직임 멈춤
        }

        if (timer == 60.0f)
        {
            if (!movingplatformlever) { Destroy(cameratarget); } //발판 레버가 아니라 숏컷이면 파괴
           //디스트로이 해야 카메라 옮겨감
            player.interrupt = false; //플레이어 움직임 풀어줌
            leverusable = false;
            timer = 0f; //타이머 초기화
            movingplatformlever = false;
        }
        //-------------레버 통해 카메라 움직임

        //무빙 플랫폼과 그냥 숏컷 나눌 것

        if (!ray.movable_down && !ray.movable_up)// 찌부
        {
            player.transform.position = savepoint;
            //life = -1
        }

        if (tutorial != null)
        {
            if (Input.GetKeyDown("x"))
            {
                Destroy(tutorial);
                Time.timeScale = 1f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tutorialexample")
        {
            Destroy(other.transform.gameObject);
            tutorial = Instantiate(tutorialexample[tutorialsequence], GameObject.FindGameObjectWithTag("canvas").transform);
            Time.timeScale = 0f;
            tutorialsequence++;
        }


        //숏컷 레버
        if (other.tag == "lever")
        {
            leverusable = true; // 레버 사용중
            //cameraf.target = other.transform.parent.gameObject.transform;
            cameratarget = other.transform.parent.gameObject;
            cameraf.target = cameratarget.transform;
            //Destroy(other.transform.parent.gameObject);
            //달각하는 사운드 추가 심심하면 카메라로 숏컷가든지
      
        }

        //움직이는 객체 레버
        if (other.tag == "movinglever")
        {
            leverusable = true; // 레버 사용중
            movingplatformlever = true;// 그냥 숏컷과 구분 위해
            //cameraf.target = other.transform.parent.gameObject.transform;
            cameratarget = other.transform.parent.gameObject;
            cameraf.target = cameratarget.transform;
            MovingPlatform movingplatform = cameratarget.gameObject.GetComponent<MovingPlatform>();
            movingplatform.movingon = true; //발판 움직임 시작
            Destroy(other.transform.gameObject); // 레버 파괴
            //왜 디스트로이 되는지 알아볼것 레버만 디스트로이하고 카메라 옮겨야함

        }

        //세이브
        if (other.tag == "savepoint")
        {
            savepoint = other.transform.position;
            savepoint.y = other.transform.position.y + 1.0f;
            savepoint.z = other.transform.position.z - 1.0f;
            //savepoint.position = tempsavepoint;
            //print("save");
            //print(tempsavepoint.y);

        }

        //피해객체
        if (other.tag == "Dead")
        {            
            print("1회 사망");
            gamemanager.deathcount++;//죽음 카운트
            if (gamemanager.deathcount >= 3) //3회 이상 사망시(fixedtime설정 관계로 2번씩 충돌돼 6으로 카운트함)
            {
                gamemanager.dead = true;
            }
            player.transform.position = savepoint; //플레이어 정비소로 위치 이동
            //life = -1


        }

        //클리어에 필요한 조각
        if (other.tag == "tissue") // 전부 모아야 클리어 가능
        {
            gamemanager.fragmentoftissue++;//매니저에 갯수 저장
            //other.enabled = false;
            Destroy(other.transform.gameObject); //콜라이더의 게임오브젝트 파괴
            
        }

        //돈
        if (other.tag == "gold") // 골드 > 점수에 반영
        {
            gamemanager.gold++; // 매니저에 갯수 저장
            
            Destroy(other.transform.gameObject); //콜라이더의 게임오브젝트 파괴
            //Destroy(other.transform.parent.gameObject);
        }

        //늪 진입
        if (other.tag == "swamp")
        {
            player.movingspeedmul = player.movingspeedmul / 5;
        }

        //갈고리 on
        if (other.tag == "hookpoint")
        {
            player.activatehook = true;
            player.hookpoint = other.gameObject; //훅포인트 넣기
        }
        

        //클리어(나중에 조건 선택)
        if (other.tag == "finishline")
        {
            //Debug.Log("결승점 도착");
            gamemanager.finishline = true;
            Property.stage1Clear = true;
            PlayerPrefs.SetString("Stage1Clear", Property.stage1Clear.ToString());
            //클리어시 저장 피니시라인별로 스테이지네임 따로만들어서(태그로) 각각 저장할 것
            
        }

    }

    private void OnTriggerExit(Collider other)
    {
        
        if (other.tag == "hookpoint")
        {
            player.activatehook = false;
        }
        if (other.tag == "finishline")
        {
            //Debug.Log("결승점 도착");
            gamemanager.finishline = false;
        }
        //늪 탈출
        if (other.tag == "swamp")
        {
            player.movingspeedmul = player.normalmovingspeedmul;
        }



    }
}
