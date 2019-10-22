using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookOutline : MonoBehaviour
{
    NewMovement player;

    GameObject hookoutline;
    Vector3 linepos;

    GameManager gamemanager;

    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<NewMovement>();
        hookoutline = GameObject.FindGameObjectWithTag("HookOutLine");
        hookoutline.GetComponent<LineRenderer>().enabled = false;//라인끔
    }

    
    void Update()
    {
        //outline(0.03, -0.72, -2)
        //player (0, -1.87, 0)

        //player.transform.position

        if (player.activatehook == true && !gamemanager.dead)
        {
            hookoutline.GetComponent<LineRenderer>().enabled = true; //라인켬            
            if (player.lookright)
            {
                linepos.Set(player.transform.position.x - 0.05f , player.transform.position.y + 0.85f, player.transform.position.z - 2);
            }
            else
            {
                linepos.Set(player.transform.position.x - 0.9f, player.transform.position.y + 0.85f, player.transform.position.z - 2);
            }
            //linepos.Set(player.transform.position.x + 0.02f, player.transform.position.y + 1.15f, player.transform.position.z - 2);
            hookoutline.transform.position = linepos;
            
        }
        else
        {
            hookoutline.GetComponent<LineRenderer>().enabled = false;//라인끔
            
        }
    }
}
