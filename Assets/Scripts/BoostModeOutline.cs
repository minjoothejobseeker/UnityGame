using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostModeOutline : MonoBehaviour
{
    NewMovement player;

    GameObject boostoutline;
    Vector3 linepos;

    Vector3 playerAngle;

    GameManager gamemanager;

    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
        boostoutline = GameObject.FindGameObjectWithTag("boostoutline");
        boostoutline.GetComponent<LineRenderer>().enabled = false;//라인끔
    }


    void Update()
    {
        //outline(0.03, -0.72, -2)
        //player (0, -1.87, 0)

        //player.transform.position

        if (player.boostmode == true && !gamemanager.dead)
        {
            boostoutline.GetComponent<LineRenderer>().enabled = true; //라인켬            
            if(player.lookright)
            {
                AutoRotation(0);
                //linepos.Set(player.transform.position.x + 0.02f, player.transform.position.y + 1.15f, player.transform.position.z - 2);
                linepos.Set(player.transform.position.x - 2.0f, player.transform.position.y + 1.15f, 0.0f);
                
            }
            else
            {
                AutoRotation(180);
                linepos.Set(player.transform.position.x + 2.0f, player.transform.position.y + 1.15f, 0.0f);
                
            }

            //앞뒤 따라 z포지션 바꿀 것
            boostoutline.transform.position = linepos;

        }
        else
        {
            boostoutline.GetComponent<LineRenderer>().enabled = false;//라인끔

        }
    }

    void AutoRotation(float angle)
    {
        playerAngle = boostoutline.transform.rotation.eulerAngles;
        playerAngle.y = angle;
        boostoutline.transform.rotation = Quaternion.Euler(playerAngle);
    }
}
