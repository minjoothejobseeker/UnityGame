using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TextEditor : MonoBehaviour
{
    Text text;

    GameManager gamemanager;

    public GameObject image;

    
    Image something;

    void Start()
    {
        text = GetComponent<Text>();
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        

        
        //image.SetActive(false);

        something = image.GetComponent<Image>();
        //something = GameObject.FindGameObjectWithTag("diemessage").GetComponent<Image>();
        something.enabled = false;

    }

    
    void Update()
    {
        //알파값 변경
        if (gamemanager.dead)
        {
            SetMessages("파괴, 점수 : " + gamemanager.totalscore);
        }
        if (gamemanager.finishline)
        {
            SetMessages("클리어, 점수 : " + gamemanager.totalscore);
        }
    }

    public void SetMessages(string message)
    {
        
        text.text = message;
        //Invoke("DestroyObject", 3.0f); //함수 시간지연 실행(함수이름, 지연시간(1.0f면 1초 후 실행))
        //image.SetActive(true);
        something.enabled = true;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
        
    }
}
