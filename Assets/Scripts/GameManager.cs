using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    NewMovement player;
    //GameManager deadline;
    TextEditor textEditor;

    public int fragmentoftissue;
    public float gold; //int형이면 10개 이하일때 점수계산에서 0점나옴

    [SerializeField]
    public int necessaryfragment; // 클리어에 필요한 조각 수

    
    public bool dead = false; // 죽는 조건 달성시 true
    public bool finishline = false; // 클리어지점 도달여부
    public bool clear = false;

    AudioSource bgm; // 배경음악 볼륨 컨트롤(효과음은 따로 스크립트)

    public Text timer; //게임 플레이 시간 텍스트표시
    float time = 0;

    float timescore = 0; //시간점수
    public float totalscore = 0; //종합점수

    bool calculateisover = false; // 계산 끝남 체크

    public float deathcount = 0; //사망 횟수. 3되면 게임오버

    float deathtimer = 0;//죽은 후 씬 전환 타이머
    bool countdownafterdeath = false;//게임오버 시간 카운트용 

    public GameObject[] life = new GameObject[3];
    int currentlife;//현재 목숨

    void Start()
    {
        fragmentoftissue = 0;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
        //necessaryfragment = 1; // 필요 조각 수 
        bgm = GameObject.FindGameObjectWithTag("bgm").GetComponent<AudioSource>();
        bgm.volume = Property.bgmvolume;
        currentlife = 0;



    }

    void CalculateScore() //클리어시 점수계산
    {
        if (calculateisover == false)
        {
            if (time < 300)
            {
                timescore = 1000 - (time * 100 / 60);
            }
            else
            {
                timescore = 500; // 5분 뒤엔 무조건 500점
            }

            
            totalscore = Mathf.Floor(gold / 10 * timescore * (3 - (deathcount/2))); // floor 함수로 버림
            //fixedtime이 두배라 deathcount가 2개씩 체크돼서 /2해주는 것
            calculateisover = true;
        }

        print(totalscore);
    }

    void LifeUI()
    {
        if (currentlife != deathcount)
        {
            if (life[currentlife] != null)
                Destroy(life[currentlife]);
            else if (life[currentlife] == null)
            { }
                currentlife++;
        }
    }


    void Update()
    {
        LifeUI();

        time += Time.deltaTime; //흐른 시간
        if (!finishline)
        {
            if (!dead)
            {
                
                timer.text = string.Format("{0:N2}", time);
            }
        }
        //사망
        if (dead)
        {
            //플레이어 삭제
            //Destroy(GameObject.FindGameObjectWithTag("Player"));
            if (countdownafterdeath == false)
            {
                deathtimer = time;
                countdownafterdeath = true;
                
            }
            //print("시간" + time + "  죽은시각" + deathtimer);
            if (time - deathtimer > 5)//5초 뒤 타이틀메뉴로 이동
            {
                //print("새신");
                SceneManager.LoadScene("TitleMenu");
            }
            
        }

        //스테이지 끝 도착
        if (finishline)
        {
            //충분한 조각을 얻었을 경우
            if (necessaryfragment == fragmentoftissue)
            {
                CalculateScore();//점수계산

                //나중에 다시 조절 texteditor에서 현재는 클리어만 나오게함
                Debug.Log("클리어");

                //클리어 여부 확인-------------
                if (Property.tutorialClear)
                {
                    if (Property.stage1Clear)
                    {
                        if (Property.stage2Clear)
                        {
                            //엔딩씬 제작 
                            
                             Property.stage3Clear = true; 
                        }
                        else { Property.stage2Clear = true; }
                    }
                    else{Property.stage1Clear = true;}
                }
                else { Property.tutorialClear = true; }

                //클리어 여부 확인-------------

                if (countdownafterdeath == false)
                {
                    deathtimer = time;
                    countdownafterdeath = true;

                }
                //print("시간" + time + "  죽은시각" + deathtimer);
                if (time - deathtimer > 5)//5초 뒤 타이틀메뉴로 이동
                {
                    //print("새신");
                    SceneManager.LoadScene("TitleMenu");
                }
                

            }
            else//부족한 경우
            {
                CalculateScore();
                Debug.Log("필요한 조각이 부족하다");
            }

        }

        //포즈
        if (Input.GetKeyDown("p"))
        {//나중에 일시정지 글자라도 넣어줄 것
            if (Time.timeScale == 0f)
            {
                Time.timeScale = 1f;
            }
            else {
                Time.timeScale = 0f;
            }
        }
    }
}
