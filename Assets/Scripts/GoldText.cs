using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldText : MonoBehaviour
{
    Text text;
    GameManager gamemanager;

    void Start()
    {
        text = GetComponent<Text>();
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        //SetMessages("점수 ");
        SetMessages("");
    }

    public void SetMessages(string message)
    {

        text.text = message + gamemanager.gold;
        //Invoke("DestroyObject", 3.0f); //함수 시간지연 실행(함수이름, 지연시간(1.0f면 1초 후 실행))        
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
