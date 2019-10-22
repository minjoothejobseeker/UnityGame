using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast_code : MonoBehaviour
{
    //트랜스폼 담을 변수
    public Transform m_tr;

    //public Transform player;

    //레이 길이 지정 변수
    public float raydistance = 1000.0f;

    //충돌 정보 가져올 레이캐스트 히트 
    public RaycastHit righthit1;
    public RaycastHit righthit2;
    public RaycastHit righthit3;
    public RaycastHit downhit1;
    public RaycastHit downhit2;
    public RaycastHit downhit3;
    public RaycastHit uphit1;
    public RaycastHit uphit2;
    public RaycastHit uphit3;
    public RaycastHit lefthit1;
    public RaycastHit lefthit2;
    public RaycastHit lefthit3;

    public RaycastHit downlefthit;
    public RaycastHit downrighthit;

    public RaycastHit righthit4;
    public RaycastHit righthit5;
    public RaycastHit lefthit4;
    public RaycastHit lefthit5;

    //레이어 마스크 지정할 변수
    //public LayerMask m_layerMask = -1; //-1 = 전부
    public LayerMask m_layerMask = -1; //-1 = 전부

    ////충돌 정보 여럿 담을 레이캐스트 히트 배열
    //public RaycastHit[] hits;

    Vector3 rightraypos1; //오위
    Vector3 rightraypos2; //오중간
    Vector3 rightraypos3; //오아래

    Vector3 downraypos1; //아래 앞
    Vector3 downraypos2; // 아래 중간
    Vector3 downraypos3; // 아래 뒤

    Vector3 upraypos1; // 위 앞
    Vector3 upraypos2; // 위 중간
    Vector3 upraypos3; // 위 뒤

    Vector3 leftraypos1; // 왼 앞
    Vector3 leftraypos2; // 왼 중간
    Vector3 leftraypos3; // 왼 뒤

    Vector3 downleftraypos; // 좌하단
    Vector3 downrightraypos; // 우하단

    Vector3 rightraypos4; //오중간
    Vector3 rightraypos5; //오아래
    Vector3 leftraypos4; // 왼 중간
    Vector3 leftraypos5; // 왼 뒤

    //레이 선언
    Ray rightray1 = new Ray();
    Ray rightray2 = new Ray();
    Ray rightray3 = new Ray();
    Ray downray1 = new Ray();
    Ray downray2 = new Ray();
    Ray downray3 = new Ray();
    Ray upray1 = new Ray();
    Ray upray2 = new Ray();
    Ray upray3 = new Ray();
    Ray leftray1 = new Ray();
    Ray leftray2 = new Ray();
    Ray leftray3 = new Ray();

    Ray downrightray = new Ray();
    Ray downleftray = new Ray();

    Ray rightray4 = new Ray();
    Ray rightray5 = new Ray();
    Ray leftray4 = new Ray();
    Ray leftray5 = new Ray();

    //이동 여부 결정
    public bool movable_left = true;
    public bool movable_right = true;
    public bool movable_up = true;
    public bool movable_down = true;

    //벽과의 충돌거리
    float xdistance = 0.7f;
    //float xdistance = 0.25f;
    float ydistance = 1.25f;
    //float xydistance = 0.6f;

    ////레이갯수늘려 안되면 x값다시

    //Vector3 downleftdirection;
    //Vector3 downrightdirection;

    void Start()
    {
        //트랜스폼 받아옴
        m_tr = GetComponent<Transform>();
        //downleftdirection.Set(-1, -1, 0); // 대각선 방향 설정
        //downrightdirection.Set(1, -1, 0);
        m_layerMask = (1 << LayerMask.NameToLayer("wall")); // 벽에만 부딪히게 하는 레이어 거르기
        //m_layerMask = 9;
    }

    private void FixedUpdate()
    {

        //레이의 위치 설정
        RayPosSet();

        //시작 지점 세팅
        rightray1.origin = rightraypos1;
        rightray2.origin = rightraypos2;
        rightray3.origin = rightraypos3;
        downray1.origin = downraypos1;
        downray2.origin = downraypos2;
        downray3.origin = downraypos3;
        upray1.origin = upraypos1;
        upray2.origin = upraypos2;
        upray3.origin = upraypos3;
        leftray1.origin = leftraypos1;
        leftray2.origin = leftraypos2;
        leftray3.origin = leftraypos3;

        //downleftray.origin = downleftraypos;
        //downrightray.origin = downrightraypos;

        rightray4.origin = rightraypos4;
        rightray5.origin = rightraypos5;
        leftray4.origin = leftraypos4;
        leftray5.origin = leftraypos5;

        //방향 설정
        rightray1.direction = Vector3.right;
        rightray2.direction = Vector3.right;
        rightray3.direction = Vector3.right;
        downray1.direction = Vector3.down;
        downray2.direction = Vector3.down;
        downray3.direction = Vector3.down;
        upray1.direction = Vector3.up;
        upray2.direction = Vector3.up;
        upray3.direction = Vector3.up;
        leftray1.direction = Vector3.left;
        leftray2.direction = Vector3.left;
        leftray3.direction = Vector3.left;

        //downleftray.direction = downleftdirection;
        //downrightray.direction = downrightdirection;

        rightray4.direction = Vector3.right;
        rightray5.direction = Vector3.right;
        leftray4.direction = Vector3.left;
        leftray5.direction = Vector3.left;

        //벡터값 (1, -1, 0) (-1, -1, 0) 대각선 하면 좌우 막는걸로


        //검출
        if (Physics.Raycast(rightray1, out righthit1, xdistance, m_layerMask)) { }
        if (Physics.Raycast(rightray2, out righthit2, xdistance, m_layerMask)) { }
        if (Physics.Raycast(rightray3, out righthit3, xdistance, m_layerMask)) { }
        //if (Physics.Raycast(downray1, out downhit1)) { }
        if (Physics.Raycast(downray1, out downhit1, ydistance, m_layerMask)) { }
        if (Physics.Raycast(downray2, out downhit2, ydistance, m_layerMask)) { }
        if (Physics.Raycast(downray3, out downhit3, ydistance, m_layerMask)) { }
        //if (Physics.Raycast(downray2, out downhit2)) { }
        //if (Physics.Raycast(downray3, out downhit3)) { }
        if (Physics.Raycast(upray1, out uphit1, ydistance, m_layerMask)) { }
        if (Physics.Raycast(upray2, out uphit2, ydistance, m_layerMask)) { }
        if (Physics.Raycast(upray3, out uphit3, ydistance, m_layerMask)) { }
        if (Physics.Raycast(leftray1, out lefthit1, xdistance, m_layerMask)) { }
        if (Physics.Raycast(leftray2, out lefthit2, xdistance, m_layerMask)) { }
        if (Physics.Raycast(leftray3, out lefthit3, xdistance, m_layerMask)) { }

        if (Physics.Raycast(leftray4, out lefthit4, xdistance, m_layerMask)) { }
        if (Physics.Raycast(leftray5, out lefthit5, xdistance, m_layerMask)) { }
        if (Physics.Raycast(rightray4, out righthit4, xdistance, m_layerMask)) { }
        if (Physics.Raycast(rightray5, out righthit5, xdistance, m_layerMask)) { }

        //if (Physics.Raycast(downleftray, out downlefthit, xdistance, m_layerMask)) { }
        //if (Physics.Raycast(downrightray, out downrighthit, xdistance, m_layerMask)) { }
        //이곳에서 검출되면 movement에서 반응함


        //이동 조건,      거리는 무조건 양수
        //오른쪽
        if ((righthit1.distance < xdistance) || (righthit2.distance < xdistance) || (righthit3.distance < xdistance)
            || (righthit4.distance < xdistance) || (righthit5.distance < xdistance))
        {
            movable_right = false;
            //movable_down = false;       
        }
        else if ((righthit1.distance >= xdistance) && (righthit2.distance >= xdistance) && (righthit3.distance >= xdistance)
            && (righthit4.distance >= xdistance) && (righthit5.distance >= xdistance))
        { movable_right = true; }
        //아래
        if ((downhit1.distance < ydistance) || (downhit2.distance < ydistance) || (downhit3.distance < ydistance))
        { movable_down = false;
            
        }
        else if ((downhit1.distance >= ydistance) && (downhit2.distance >= ydistance) && (downhit3.distance >= ydistance))
        { movable_down = true; }
        //왼쪽
        if ((lefthit1.distance < xdistance) || (lefthit2.distance < xdistance) || (lefthit3.distance < xdistance)
            || (lefthit4.distance < xdistance) || (lefthit5.distance < xdistance))
        {
            movable_left = false;
            //movable_down = false; 
        }
        else if ((lefthit1.distance >= xdistance) && (lefthit2.distance >= xdistance) && (lefthit3.distance >= xdistance)
            && (lefthit4.distance >= xdistance) && (lefthit5.distance >= xdistance))
        { movable_left = true; }
        //위
        if ((uphit1.distance < ydistance) || (uphit2.distance < ydistance) || (uphit3.distance < ydistance))
        { //if (!(uphit1.collider.tag == "Collider"))
            //{
                movable_up = false;
                //print(uphit1.collider);
            } //}
        else if ((uphit1.distance >= ydistance) && (uphit2.distance >= ydistance) && (uphit3.distance >= ydistance))
        { movable_up = true; }

        //if ((downlefthit.distance < xydistance) )
        //{ movable_left = false; }
        //if ((downrighthit.distance < xydistance))
        //{ movable_right = false; }

        //쭉 빈공간일경우 진행 가능케함
        if ((righthit1.collider == null) && (righthit2.collider == null) && (righthit3.collider == null))
            movable_right = true;
        if ((lefthit1.collider == null) && (lefthit2.collider == null) && (lefthit3.collider == null))
            movable_left = true;
        if ((downhit1.collider == null) && (downhit2.collider == null) && (downhit3.collider == null))
            movable_down = true;
        if ((uphit1.collider == null) && (uphit2.collider == null) && (uphit3.collider == null))
            movable_up = true;



        //movabledown && movableright == false면 벽점프 가능

        //확인용 레이 그려줌
        ondrawline();

        //print(lefthit1.distance);
    }

    public void ondrawline()
    {

        //앞
        if (righthit1.collider != null)
            Debug.DrawLine(rightraypos3, rightraypos3 + Vector3.right * xdistance, Color.red);
        else
            Debug.DrawLine(m_tr.position, m_tr.position + m_tr.forward * this.raydistance, Color.white);

        //바닥
        if (downhit1.collider != null)
            Debug.DrawLine(downraypos1, downraypos1 + Vector3.down * ydistance, Color.blue);
        else
            Debug.DrawLine(m_tr.position, m_tr.position + Vector3.down * this.raydistance, Color.white);

        //Debug.DrawLine(downrightraypos, downrightraypos + downrightdirection * downrighthit.distance, Color.green);
        //Debug.DrawLine(downrightraypos, downrightraypos + downrightdirection * xydistance, Color.green);
    }

    //레이의 포지션 세팅
    void RayPosSet()
    {
        //오른 레이
        rightraypos1 = m_tr.position; // 앞 위
        rightraypos2 = m_tr.position; // 앞 중간
        rightraypos3 = m_tr.position; // 앞 아래
        rightraypos1.y = m_tr.position.y + 0.98f;
        rightraypos3.y = m_tr.position.y - 0.98f;

        rightraypos4 = m_tr.position;
        rightraypos5 = m_tr.position;
        rightraypos4.y = m_tr.position.y - 0.49f;
        rightraypos5.y = m_tr.position.y + 0.49f;

        //rightraypos1.x = m_tr.position.x + 0.50f; //중심이 아닌 겉면에서 하기 위한 것
        //rightraypos2.x = m_tr.position.x + 0.50f;
        //rightraypos3.x = m_tr.position.x + 0.50f;

        //왼 레이
        leftraypos1 = m_tr.position; // 앞 위
        leftraypos2 = m_tr.position; // 앞 중간
        leftraypos3 = m_tr.position; // 앞 아래
        leftraypos1.y = m_tr.position.y + 0.98f;
        leftraypos3.y = m_tr.position.y - 0.98f;

        leftraypos4 = m_tr.position;
        leftraypos5 = m_tr.position;
        leftraypos5.y = m_tr.position.y + 0.49f;
        leftraypos4.y = m_tr.position.y - 0.49f;

        //leftraypos1.x = m_tr.position.x - 0.50f;
        //leftraypos2.x = m_tr.position.x - 0.50f;
        //leftraypos3.x = m_tr.position.x - 0.50f;



        //아래 레이
        downraypos1 = m_tr.position; // 아래 앞
        downraypos2 = m_tr.position; // 아래 중간
        downraypos3 = m_tr.position; // 아래 뒤
        downraypos1.x = m_tr.position.x + 0.5f;//0.48
        downraypos3.x = m_tr.position.x - 0.5f;


        //위 레이
        upraypos1 = m_tr.position; // 위 앞
        upraypos2 = m_tr.position; // 위 중간
        upraypos3 = m_tr.position; // 위 뒤
        upraypos1.x = m_tr.position.x + 0.5f;
        upraypos3.x = m_tr.position.x - 0.5f;


        //대각선
        downleftraypos = m_tr.position; // 좌하단
        downrightraypos = m_tr.position; // 우하단
        downrightraypos.y = m_tr.position.y - 0.5f;

    }
}
